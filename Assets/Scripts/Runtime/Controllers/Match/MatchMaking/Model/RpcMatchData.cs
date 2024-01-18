using System;

namespace Infinite8.NakamaExtension.Controllers.Match.MatchMaking.Model
{
    [Serializable]
    public class RpcMatchData
    {
        public string matchId;

        public RpcMatchData(string matchId) => this.matchId = matchId;
    }
}