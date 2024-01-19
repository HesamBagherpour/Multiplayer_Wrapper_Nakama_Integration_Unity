using Runtime.NakamaConfig.MatchConfig;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;

namespace Runtime.NakamaConfig.RpcConfig
{
    public class RpcMatchMakingConfig : MatchMakingGeneralModel
    {
        public override MatchMakingType MatchMakingType => MatchMakingType.MatchMaking;
        public theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig.RpcConfig rpcConfig;
    }
}