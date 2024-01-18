using Cysharp.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using Runtime.Utilities;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;

namespace theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking
{
    public class RpcMatchMaking : IMatchMaking<RpcMatchMakingConfig>
    {
        public async UniTask<string> StartMatchMaking(RpcMatchMakingConfig matchMakingConfig)
        {
            // var payload = (JsonConvert.SerializeObject(matchMakingConfig.matchFilter));
            var payload = matchMakingConfig.matchFilter.ToJson();
            return await NakamaRpc.SendRpc<string>(matchMakingConfig.client.Client, matchMakingConfig.session.Session, matchMakingConfig.rpcConfig.rpcName,
                matchMakingConfig.rpcConfig.timeOutSec,
                payload, new RetryConfiguration(matchMakingConfig.rpcConfig.baseDelayMs,
                    matchMakingConfig.rpcConfig.maxRetries));
        }
    }
}