using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.OpCode;
using theHesam.NakamaExtension.Runtime.Core;
using UnityEngine;

namespace Runtime.Controllers.Match
{
    public class MatchMessageController : MonoBehaviour
    {
        private bool _isConnected;
        private SocketExtended socketExtended;
        private string _matchId;
        public Action<IMatchState> OnReceiveMatchState;
        private List<OpCodeCompModel> _opCodes;
        private long _lastReceivedGameState;
        private IMatchState _currentMatchState;
        public Action OnJoinPlayer;
        public MatchOpCodeController matchOpCodeController;
        public event Action<IMatchPresenceEvent> OnSocketReceivedMatchPresence;

        public void Init(SocketExtended socketExtended, string matchId)
        {
            _opCodes = new List<OpCodeCompModel>();
            this.socketExtended = socketExtended;
            _matchId = matchId;
            matchOpCodeController = new MatchOpCodeController();
            matchOpCodeController.Init(this);

            this.socketExtended.socket.ReceivedMatchState += SocketOnReceivedMatchState;
            this.socketExtended.socket.ReceivedMatchPresence += SocketOnReceivedMatchPresence;
        }

        private void SocketOnReceivedMatchPresence(IMatchPresenceEvent obj)
        {
            OnSocketReceivedMatchPresence?.Invoke(obj);
        }

        private void SocketOnReceivedMatchState(IMatchState matchState)
        {
            if (matchState.MatchId != _matchId)
                return;

            _lastReceivedGameState = DateTimeOffset.Now.ToUnixTimeSeconds();
            _currentMatchState = matchState;
            if (matchOpCodeController.opCodeCallbacks.ContainsKey(matchState.OpCode))
            {
                matchOpCodeController.opCodeCallbacks[matchState.OpCode].Invoke(matchState.OpCode,
                    matchOpCodeController.opCodeKeyByValue.ContainsKey(matchState.OpCode)
                        ? matchOpCodeController.opCodeKeyByValue[matchState.OpCode]
                        : null,
                    matchState);
            }

            OnReceiveMatchState?.Invoke(matchState);
        }

        public void SetMatchId(string matchId)
        {
            _matchId = matchId;
        }

        public void ResetData()
        {
            this.socketExtended.socket.ReceivedMatchState -= SocketOnReceivedMatchState;
            this.socketExtended.socket.ReceivedMatchPresence -= SocketOnReceivedMatchPresence;
        }

        #region SendMatchState

        public async UniTask SendMatchState(long opCode, string state,
            IEnumerable<IUserPresence> presences = null)
        {
            if (!socketExtended.socket.IsConnected)
                return;

            try
            {
                await socketExtended.socket.SendMatchStateAsync(_matchId, opCode, state, presences);
            }
            catch (Exception e)
            {
                Debug.unityLogger.Log("SendMatchState  error : " + e);
                throw;
            }
        }

        public async UniTask SendMatchState(long opCode, ArraySegment<byte> state, IEnumerable<IUserPresence> presences = null)
        {
            if (!socketExtended.socket.IsConnected)
                return;
            try
            {
                await socketExtended.socket.SendMatchStateAsync(_matchId, opCode, state, presences);
            }
            catch (Exception e)
            {
                Debug.unityLogger.Log("SendMatchState  error :  " + e);
                throw;
            }
        }

        public async UniTask SendMatchState(long opCode, byte[] state,
            IEnumerable<IUserPresence> presences = null)
        {
            if (!socketExtended.socket.IsConnected)
                return;
            try
            {
                await socketExtended.socket.SendMatchStateAsync(_matchId, opCode, state, presences);
            }
            catch (Exception e)
            {
                Debug.unityLogger.Log("SendMatchState  error : " + e);
                throw;
            }
        }

        #endregion
    }
}