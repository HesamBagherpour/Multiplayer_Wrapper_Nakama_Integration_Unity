using System;

namespace Runtime.NakamaConfig.MatchConfig
{
    [Serializable]
    public class MatchConfig
    {
        private string _matchName;
        public MatchConfig()
        {
        }
        public MatchConfig(string matchName)
        {
            this._matchName = matchName;
        }
        public string GETMatchName()
        {
            return _matchName;
        }
    }
}