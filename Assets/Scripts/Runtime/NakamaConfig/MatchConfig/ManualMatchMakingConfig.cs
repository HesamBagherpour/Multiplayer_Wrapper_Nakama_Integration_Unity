using Runtime.NakamaConfig.MatchConfig;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public class ManualMatchMakingConfig: MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.Manual;
        public string matchId;
    }
}