using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using theHesam.NakamaExtension.Runtime.Core;
using UnityEngine;

namespace Runtime.Controllers
{
    public class SocketPingPongRpc
    {
        private SocketExtended socket;

        //public event Action OnRetrying;
        public event Action OnDisconnect;

        private int checkingInterval;
        private float lastPacketReceivedTime;
        private bool waitForResponse;
        private bool pauseChecking;

        private CancellationTokenSource cancellationTokenSource;

        public Action<Exception> ReceivedError;
        public Action Closed;
        public Action<IMatchPresenceEvent> ReceivedMatchPresence;
        public Action<IMatchState> ReceivedMatchState;

        public SocketPingPongRpc(SocketExtended socket, int checkingInterval)
        {
            this.socket = socket;
            this.checkingInterval = checkingInterval;
            cancellationTokenSource = new CancellationTokenSource();

            socket.socket.ReceivedMatchState += (m) => { ReceivedMatchState?.Invoke(m); };
            socket.socket.ReceivedMatchPresence += (m) => { ReceivedMatchPresence?.Invoke(m); };
            socket.socket.Closed += () => { Closed?.Invoke(); };
            socket.socket.ReceivedError += (m) => { ReceivedError?.Invoke(m); };
        }

        public void Active()
        {
            Debug.Log("RPC PING PONG CONNECT");
            ReceivedMatchState += (m) => { ResetTime(); };
            ReceivedMatchPresence += (m) => { ResetTime(); };
            Closed += Deactivate;
            ReceivedError += (m) => { Deactivate(); };
            pauseChecking = false;
            ResetTime();
            StartPinPong().Forget();
        }


        public void Deactivate()
        {
            Debug.Log("RPC PING PONG DISCONNECT");
            pauseChecking = true;
            cancellationTokenSource.Cancel();
            ReceivedMatchState -= (m) => { ResetTime(); };
            ReceivedMatchPresence -= (m) => { ResetTime(); };
            Closed -= Deactivate;
            ReceivedError -= (m) => { Deactivate(); };
        }

        private void ResetTime()
        {
            lastPacketReceivedTime = Time.time;
        }

        private async UniTaskVoid StartPinPong()
        {
            while (!pauseChecking)
            {
                if (waitForResponse)
                {
                    Debug.Log("3");
                    ResetTime();
                }
                else if (lastPacketReceivedTime + checkingInterval < Time.time)
                {
                    waitForResponse = true;
                    bool successful = await SendPingPong();
                    if (!successful)
                    {
                        OnDisconnect?.Invoke();
                        Deactivate();
                    }

                    waitForResponse = false;
                    ResetTime();
                }

                if (!pauseChecking)
                    await UniTask.Delay(checkingInterval * 1000);
            }
        }

        public async UniTask<bool> SendPingPong()
        {
            Task<IApiRpc> result;
            result = socket.socket.RpcAsync("ppr");
            DateTime startTime = DateTime.Now;
            while (!result.IsCompleted)
            {
                if (startTime.AddSeconds(3) < DateTime.Now)
                {
                    return false;
                }

                await UniTask.Yield();
            }

            return true;
        }
    }
}