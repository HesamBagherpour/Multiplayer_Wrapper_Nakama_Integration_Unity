using System;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    [Serializable]
    public class RpcMatchData
    {
        public string matchId;

        public RpcMatchData(string matchId) => this.matchId = matchId;
    }
}