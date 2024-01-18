using Runtime.NakamaConfig.MatchConfig;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public class RpcMatchMakingConfig : MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.MatchMaking;
        public RpcConfig rpcConfig;
    }
}