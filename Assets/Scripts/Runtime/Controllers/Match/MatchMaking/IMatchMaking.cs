using Cysharp.Threading.Tasks;

namespace theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking
{
    public interface IMatchMaking<in T>
    {
        UniTask<string> StartMatchMaking(T config);
    }
}