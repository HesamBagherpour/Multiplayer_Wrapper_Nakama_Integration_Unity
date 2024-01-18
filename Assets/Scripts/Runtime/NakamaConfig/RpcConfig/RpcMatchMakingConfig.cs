using Runtime.NakamaConfig.MatchConfig;

namespace Runtime.NakamaConfig.RpcConfig
{
    public class RpcMatchMakingConfig : MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.RPC;
        public MatchConfig.RpcConfig rpcConfig;
    }
}