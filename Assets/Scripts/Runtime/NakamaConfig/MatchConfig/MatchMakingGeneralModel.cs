using Runtime.Core;
using theHesam.NakamaExtension.Runtime.Core;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public abstract class MatchMakingGeneralModel
    {
        public ClientExtended client;
        public SocketExtended socket;
        public SessionExtended session;
        public string matchTag;
        public MatchFilter matchFilter;
        //public Action<MatchExtended> doneAction;
        public abstract MatchMakingType matchMakingType { get; }
    }

    public enum MatchMakingType
    {
        MatchMaking,
        MatchListing,
        Auto
    }
}