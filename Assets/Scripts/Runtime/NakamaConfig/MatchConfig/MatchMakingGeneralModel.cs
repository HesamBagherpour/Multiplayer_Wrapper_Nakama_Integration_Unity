using Runtime.Core;
using theHesam.NakamaExtension.Runtime.Core;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public abstract class MatchMakingGeneralModel
    {
        public ClientExtended Client;
        public SessionExtended Session;
        public MatchFilter MatchFilter;
        public abstract MatchMakingType MatchMakingType { get; }
    }

    public enum MatchMakingType
    {
        MatchMaking,
        MatchListing
    }
}