using System;

namespace Runtime.NakamaConfig.MatchConfig
{
    [Serializable]
    public class RpcConfig
    {
        public string rpcName;
        public int timeOutSec;
        public int maxRetries;
        public int baseDelayMs;
        
        public RpcConfig(string rpcName, int timeOutSec, int maxRetries, int baseDelayMs)
        {
            this.rpcName = rpcName;
            this.timeOutSec = timeOutSec;
            this.maxRetries = maxRetries;
            this.baseDelayMs = baseDelayMs;
        }
        public RpcConfig(string rpcName)
        {
            this.rpcName = rpcName;            
            timeOutSec = 20;      
            maxRetries = 3;      
            baseDelayMs = 100;    
        }
    }
}