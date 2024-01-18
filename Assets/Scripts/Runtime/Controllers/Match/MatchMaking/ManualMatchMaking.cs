using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Runtime.NakamaConfig.MatchConfig;

namespace Runtime.Controllers.Match.MatchMaking
{
    public class ManualMatchMaking: IMatchMaking<ManualMatchMakingConfig>
    {
        public async UniTask<string> StartMatchMaking(ManualMatchMakingConfig matchMakingConfig)
        {
            return JsonConvert.SerializeObject(new { matchId = matchMakingConfig.matchId});
        }
    }
}