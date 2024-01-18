using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Nakama;
using Newtonsoft.Json;
using Runtime.Controllers;
using Runtime.Core;
using Runtime.Manager;
using Runtime.NakamaConfig.Message;
using Runtime.NakamaConfig.OpCode;
using Runtime.NakamaConfig.SocketConfig;
using theHesam.NakamaExtension.Runtime.Core;
using UnityEngine;

namespace theHesam.NakamaExtension.Runtime.Controllers
{
    [RequireComponent(typeof(OpCodeGenerator))]
    public class SocketPingPongConnection : MonoBehaviour
    {
        private ISocket _pingPongSocket;
        private PingPongConfig _pingPongConfig;
        private OpCodeGenerator _opCodeGenerator;
        private bool _receivedMessage;
        private bool _isOpCodeRegistered;
        [SerializeField] public string matchId;
        [SerializeField] public string matchTagName;
        [SerializeField] public string sessionTagName;
        [SerializeField] public string socketTagName;
        [SerializeField] public string clintTagName;
        public Action<bool> socketConnection;
        public Action<bool> socketDisConnect;
        private CancellationTokenSource cancellationToken;
        public bool isDisconnectByUser = false;


        #region Init

        public async UniTask<bool> Init(ClientExtended clientExtended, SessionExtended sessionExtended, SocketExtended socketExtended, MatchExtended matchExtended)
        {
            _pingPongConfig = new PingPongConfig();

            _pingPongConfig.PingPongIntervalSec = 3;
            _pingPongConfig.NetworkCheckIntervalSec = 10;

            Debug.Log("_pingPongConfig || NetworkCheckIntervalSec " + _pingPongConfig.NetworkCheckIntervalSec);


            var clint = clientExtended;
            clintTagName = clint.tag;
            var session = sessionExtended;
            sessionTagName = session.tag;

            var socket = socketExtended;
            socketTagName = socket.tag;
            _pingPongSocket = socket.socket;

            var match = matchExtended;
            matchId = match.matchId;
            matchTagName = match.tag;

            var matchMessage = match.matchMessageController;
            var matchConnection = match.matchConnectionController;
            _opCodeGenerator.Init(
                matchConnection,
                matchMessage.matchOpCodeController);

            return true;
        }

        public async UniTask<bool> Init()
        {
            _pingPongConfig = new PingPongConfig();

            _pingPongConfig.PingPongIntervalSec = 3;
            _pingPongConfig.NetworkCheckIntervalSec = 10;

            Debug.Log("_pingPongConfig || NetworkCheckIntervalSec  start " + _pingPongConfig.NetworkCheckIntervalSec);
            var clint = await NakamaManager.Instance.ClientFactory.GetClintAsync("aws");
            clintTagName = clint.tag;
            var session = await clint.SessionFactory.GetSession("google");
            sessionTagName = session.tag;

            var socket = await session.SocketFactory.GetSocket("chat");
            socketTagName = socket.tag;
            _pingPongSocket = socket.socket;

            var match = await socket.MatchFactory.GetMatch("chat");
            matchId = match.matchId;
            matchTagName = match.tag;

            var matchMessage = match.matchMessageController;
            var matchConnection = match.matchConnectionController;
            _opCodeGenerator.Init(
                matchConnection,
                matchMessage.matchOpCodeController);
            
            _opCodeGenerator.OnReceiveOpCodeMessage += OnReceiveOpCodeMessage;
            _pingPongConfig.PingPongIntervalSec = 3;
            return true;
        }

        private void Onconnected()
        {
            _isOpCodeRegistered = true;
        }

        #endregion

        #region Send_PingPong

        private void SendPingPong()
        {
            var subscription = UniTaskAsyncEnumerable.Interval(TimeSpan.FromSeconds(_pingPongConfig.PingPongIntervalSec))
                .ForEachAsync(_ =>
                {
                    SendPingPongState();
                    Debug.Log("UniTaskAsyncEnumerable");
                }, cancellationToken.Token);
        }

        private void SendPingPongState()
        {
            if (!_isOpCodeRegistered) return;
            string opCodeKey = "PingPong";
            MultiPlayerMessage<PingPongMessage> packet = new MultiPlayerMessage<PingPongMessage>();
            packet.message = new PingPongMessage();
            packet.uuid = _opCodeGenerator.GetOpCode(0, opCodeKey).Uuid;

            _opCodeGenerator.SendMatchState(_opCodeGenerator.GetOpCode(0, opCodeKey).OpCode,
                JsonConvert.SerializeObject(packet));
        }


        public void StopPingPong()
        {
            cancellationToken.Cancel();
        }

        #endregion

        #region start_PingPong

        public void StartPingPong()
        {
            SendPingPong();
        }

        #endregion

        #region receiveMessage

        private void OnReceiveOpCodeMessage(long opCode, string key, string uuid, IMatchState state)
        {
            var packet = JsonConvert.DeserializeObject<MultiPlayerMessage<PingPongMessage>>(Encoding.UTF8.GetString(state.State));
            if (packet != null && packet.message.StateMessage == null)
            {
                _receivedMessage = true;
                _pingPongConfig.LastReceivedGameState = DateTimeOffset.Now.ToUnixTimeSeconds();
                Debug.Log(" _pingPongConfig   ||  LastReceivedGameState   " + _pingPongConfig.LastReceivedGameState);
                socketConnection?.Invoke(true);
            }
            // if (packet != null && packet.needLastStateUserId != null)
            // {
            //     // send Last State if there is not any state 
            //     // or new player jut Join
            // }
        }

        #endregion

        #region Unity

        private void Awake()
        {
            _opCodeGenerator = GetComponent<OpCodeGenerator>();
            cancellationToken = new CancellationTokenSource();
        }

        private void Start()
        {
            _opCodeGenerator.Onconnected += Onconnected;
            _pingPongConfig.PingPongIntervalSec = 3;
            _pingPongConfig.NetworkCheckIntervalSec = 10;
        }


        // private async Task Start()
        // {
        //     
        //     _opCodeGenerator.Onconnected +=Onconnected;
        //     var a = await Init();
        //     StartPingPong();
        //     CheckNetworkInterval();
        // }

        #endregion

        #region Editor

#if UNITY_EDITOR

        [ContextMenu("init  PingPong  Socket ")]
        public async void PingPongInit()
        {
            Debug.Log("Ping Pong Init ");
            var a = await Init();
            Debug.Log("Ping Pong Init  task  : " + a);
        }

        [ContextMenu("start  PingPong  Socket ")]
        public void PingPongStart()
        {
            StartPingPong();
            CheckNetworkInterval();
        }


        [ContextMenu("Stop PingPong  Socket ")]
        public void CancellationPingPong()
        {
            StopPingPong();
        }

        [ContextMenu("CheckNetworkInterval ")]
        public void CheckNetworkIntervalCall()
        {
            CheckNetworkInterval();
        }

#endif

        #endregion

        #region DisconnectInterval

        private async UniTaskVoid CheckNetworkInterval()
        {
            await UniTask.DelayFrame(9000);

            var a = UniTaskAsyncEnumerable
                .Interval(TimeSpan.FromSeconds(_pingPongConfig.NetworkCheckIntervalSec))
                .ForEachAsync(_ =>
                {
                    var lastReceivedState = DateTimeOffset.Now.ToUnixTimeSeconds() - _pingPongConfig.LastReceivedGameState;
                    Debug.Log("CheckNetworkInterval  || lastReceivedState   " + lastReceivedState);
                    Debug.Log("CheckNetworkInterval  || NetworkCheckIntervalSec   " + _pingPongConfig.NetworkCheckIntervalSec);
                    if (lastReceivedState < _pingPongConfig.NetworkCheckIntervalSec || !_receivedMessage ||
                        isDisconnectByUser) return;
                    Debug.Log(" CheckNetworkInterval ||  you are DisConnect By User || isDisConnectByUser : " + isDisconnectByUser);
                    Disconnect();
                });
        }

        private void Disconnect()
        {
            socketDisConnect?.Invoke(true);
            //socket?.CloseAsync();
        }

        #endregion
    }
}