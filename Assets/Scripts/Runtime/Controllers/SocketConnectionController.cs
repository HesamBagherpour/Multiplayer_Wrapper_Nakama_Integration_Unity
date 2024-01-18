using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core;
using Runtime.NakamaConfig.SocketConfig;
using theHesam.NakamaExtension.Runtime.Core;
using UnityEngine;

namespace Runtime.Controllers
{
    public class SocketConnectionController : MonoBehaviour
    {
        public SocketExtended socketExtended;
        public ClientExtended clientExtended;
        public SessionExtended sessionExtended;
        public SocketConfig socketConfig;
        public Action<SocketExtended> OnConnectSocket;
        public Action<SocketExtended> OnDisconnectSocket;
        public Action<Exception> OnReceivedError;
        public SocketRetryController socketRetryController;
        public SocketPingPongRpc pingPongRpc;
        [SerializeField] public List<string> playerList;

        public void Init(ClientExtended client, SessionExtended sessionExtended, SocketExtended socketExtended, SocketInitialConfig initialConfig)
        {
            this.socketExtended = socketExtended;
            clientExtended = client;
            this.sessionExtended = sessionExtended;
            socketExtended.socket.Connected += SocketOnConnected;
            socketExtended.socket.Closed += SocketOnClosed;
            socketExtended.socket.ReceivedError += SocketOnReceivedError;

            socketRetryController = new SocketRetryController(this, initialConfig.retryCount);

            switch (initialConfig.checkingConnectionType)
            {
                case CheckingConnectionType.None:
                    break;
                case CheckingConnectionType.PingPongRpc:
                    pingPongRpc = new SocketPingPongRpc(this.socketExtended, initialConfig.interval);

                    //pingPongRpc.OnDisconnect += () => { socketRetryController.StartRetry().Forget(); };

                    if (socketExtended.socket.IsConnected)
                    {
                        pingPongRpc.Active();
                    }
                    else
                    {
                        OnConnectSocket += (m) => pingPongRpc.Active();
                    }

                    break;
            }
        }

        public async UniTask<bool> ConnectSocket(SocketExtended socketExtended, SessionExtended sessionExtended, SocketConfig config)
        {
            try
            {
                await socketExtended.socket.ConnectAsync(sessionExtended.Session, config.AppearOnline, config.ConnectionTimeout);
                return true;
            }
            catch (Exception e)
            {
                OnReceivedError?.Invoke(e);
                return false;
            }

            // await socketExtended.socket.UpdateStatusAsync("hesam");
        }

        private void SocketOnReceivedError(Exception obj)
        {
            OnReceivedError?.Invoke(obj);
        }

        private void SocketOnClosed()
        {
            playerList.Clear();
        }

        private void SocketOnConnected()
        {
            OnConnectSocket?.Invoke(socketExtended);
        }
    }
}