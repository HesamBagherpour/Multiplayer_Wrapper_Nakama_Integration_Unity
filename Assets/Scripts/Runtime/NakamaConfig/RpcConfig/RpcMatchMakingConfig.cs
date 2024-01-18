using Runtime.NakamaConfig.MatchConfig;

namespace Runtime.NakamaConfig.RpcConfig
{
    public class RpcMatchMakingConfig : MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.RPC;
        public theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig.RpcConfig rpcConfig;
    }
}