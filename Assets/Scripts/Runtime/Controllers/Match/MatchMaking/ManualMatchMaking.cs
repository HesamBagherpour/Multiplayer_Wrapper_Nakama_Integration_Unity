using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;

namespace theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking
{
    public class ManualMatchMaking: IMatchMaking<ManualMatchMakingConfig>
    {
        public async UniTask<string> StartMatchMaking(ManualMatchMakingConfig matchMakingConfig)
        {
            return JsonConvert.SerializeObject(new { matchId = matchMakingConfig.matchId});
        }
    }
}