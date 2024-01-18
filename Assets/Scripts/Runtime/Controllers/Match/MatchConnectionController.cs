using System;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.MatchConfig;
using theHesam.NakamaExtension.Runtime.Controllers.Match;
using theHesam.NakamaExtension.Runtime.Core;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;
using UnityEngine;

namespace Runtime.Controllers.Match
{
    public class MatchConnectionController : MonoBehaviour
    {
        public Action<string, string, SocketExtended> OnMatchConnect;
        private SocketExtended socketExtended;
        public bool isConnected;
        public string matchId;
        private string _matchTag;
        private IMatch _match;
        private MatchConfig _matchConfig;
        public MatchMessageController matchMessageController;


        #region Init

        public void Init(SocketExtended socketExtended, MatchConfig matchConfig)
        {
            this.socketExtended = socketExtended;
            _matchConfig = matchConfig;
        }

        public async UniTask<bool> ConnectMatch(string _matchId, MatchMessageController matchMessageController)
        {
            matchId = _matchId;
            try
            {
                _match = await socketExtended.socket.JoinMatchAsync(matchId);
            }
            catch (Exception e)
            {
                Debug.unityLogger.Log(e.Message);
            }

            if (_match != null)
            {
                isConnected = true;
                string matchTag = _matchConfig.GETMatchName();
                this.matchMessageController = matchMessageController;
                OnMatchConnect?.Invoke(matchId, _matchTag, socketExtended);
                return true;
            }

            return false;
        }

        public void ResetData()
        {
            _match = null;
            matchId = string.Empty;
        }

        public IMatch GetMatch()
        {
            return _match;
        }

        #endregion
    }
}