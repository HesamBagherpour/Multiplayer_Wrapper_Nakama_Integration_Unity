using Runtime.NakamaConfig.MatchConfig;

namespace Runtime.Controllers.Match.MatchMaking.Model
{
    public class RpcMatchMakingConfig : MatchMakingGeneralModel
    {
        public override MatchMakingType matchMakingType => MatchMakingType.RPC;
        public RpcConfig rpcConfig;
    }
}