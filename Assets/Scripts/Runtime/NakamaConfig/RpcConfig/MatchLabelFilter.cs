using Newtonsoft.Json;
using UnityEngine;

namespace Runtime.NakamaConfig.RpcConfig
{
    [CreateAssetMenu( menuName = "The_Hesam/Nakama/Create New MatchLabelFilter  Config" , fileName = "MatchLabelFilter config")]
    public class MatchLabelFilter : ScriptableObject
    {
        [JsonProperty("roomToken")] public string RoomToken;
    }
}