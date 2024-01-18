using System;

namespace Runtime.NakamaConfig.Message
{
    [Serializable]
    public class MultiPlayerMessage<T>
    {
        public string uuid;
        public string needLastStateUserId;
        public T message;
    }
}