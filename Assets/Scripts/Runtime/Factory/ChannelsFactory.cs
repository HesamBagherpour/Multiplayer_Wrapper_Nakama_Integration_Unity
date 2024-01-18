using System;
using Cysharp.Threading.Tasks;
using Nakama;
using theHesam.NakamaExtension.Runtime.Core;
using theHesam.NakamaExtension.Runtime.NakamaConfig.ChannelConfig;

namespace theHesam.NakamaExtension.Runtime.Factory
{
    public class ChannelsFactory
    {
        public async UniTask<Tuple<bool, IChannel>> CreateChannel(string userId ,SocketExtended socketExtended ,ChannelConfig config)
        {
            IChannel chatChannel = await socketExtended.socket.JoinChatAsync(userId, ChannelType.DirectMessage,config.Persistence,config.Hidden);
            return new Tuple<bool, IChannel>(true, chatChannel);
        }
    }
  
}