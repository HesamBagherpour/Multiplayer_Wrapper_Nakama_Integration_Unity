using System;

namespace Runtime.NakamaConfig.MatchConfig
{
    [Serializable]
    public class MatchFilter
    {
        public string matchModuleName;
        public int limit;
        public bool authoritative;
        public MatchFilterLabel label;
        public int? minSize;
        public int? maxSize;
        public string query;

        public MatchFilter(string matchModuleName, int limit, bool authoritative, MatchFilterLabel label, int? minSize, int? maxSize, string query)
        {
            this.matchModuleName = matchModuleName;
            this.limit = limit;
            this.authoritative = authoritative;
            this.label = label;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.query = query;
        }
        public MatchFilter(string matchModuleName, MatchFilterLabel label)
        {
            this.matchModuleName = matchModuleName;
            this.label = label;
            authoritative = true;
            limit = 1;
            minSize = null;
            maxSize = null;
            query = null;
        }
        
    }

    [Serializable]
    public class MatchFilterLabel
    {
        public bool open;
        public string roomToken;

        public MatchFilterLabel(bool open, string roomToken)
        {
            this.open = open;
            this.roomToken = roomToken;
        }
    }
}