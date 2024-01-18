using System;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.ChannelConfig;
using theHesam.NakamaExtension.Runtime.Core;

namespace Runtime.Factory
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