using System;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    [Serializable]
    public class MatchInfoRpc
    {
        public MatchInfo matchInfo;
    }
    [Serializable]
    public class MatchInfo
    {
        public string matchId;
        public bool authoritative;
        public string size;
        public string label;
        public long serverTime;
    }
}