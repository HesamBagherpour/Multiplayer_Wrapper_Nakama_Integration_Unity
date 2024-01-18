using UnityEngine;

namespace Runtime.NakamaConfig.RpcConfig
{
    [CreateAssetMenu( menuName = "The_Hesam/Create New RPC Config" , fileName = "RPC config")]
    public class RpcConfig : ScriptableObject
    {

        public string roomToken;
        public MatchLabelFilter roomTokenPayload;
        public string rpcName;
        public int timeOutSec;
        public int maxRetries;
        public int baseDelayMs;
    }
}
