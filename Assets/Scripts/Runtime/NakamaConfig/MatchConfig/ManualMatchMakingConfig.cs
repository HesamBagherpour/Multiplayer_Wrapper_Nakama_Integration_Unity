using Runtime.NakamaConfig.MatchConfig;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public class ManualMatchMakingConfig: MatchMakingGeneralModel
    {
        public override MatchMakingType MatchMakingType => MatchMakingType.MatchListing;
        public string matchId;
    }
}