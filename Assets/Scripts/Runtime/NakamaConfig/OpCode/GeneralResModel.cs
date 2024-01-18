using System;

namespace Runtime.NakamaConfig.OpCode
{
    [Serializable]
    public class GeneralResModel<T>
    {
        public string error;
        public T data;

        public GeneralResModel()
        {
        }

        public GeneralResModel(string error)
        {
            this.error = error;
        }

        public GeneralResModel(T data)
        {
            this.data = data;
        }
    }

    [Serializable]
    public class BaseReqModel
    {
        public Action cancel;
    }

    [Serializable]
    public class MatchData
    {
        public string matchId;
        public long serverTime;

        public MatchData(string matchId, long serverTime)
        {
            this.matchId = matchId;
            this.serverTime = serverTime;
        }
    }
}