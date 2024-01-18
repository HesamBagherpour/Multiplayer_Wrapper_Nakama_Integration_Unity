using Runtime.NakamaConfig.MatchConfig;

namespace Runtime.Controllers.Match.MatchMaking.Model
{
    public class ManualMatchMakingConfig: MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.Manual;
        public string matchId;
    }
}