using System;
using Nakama;

namespace theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig
{
    public interface MatchStateExtended
    {
        public event Action<IMatchState> OnReceivedChannelMessage;
        public void Raise(IMatchState channelMessage);
    }
}