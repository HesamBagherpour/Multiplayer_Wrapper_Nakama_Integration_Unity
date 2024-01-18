﻿using Nakama;

namespace Runtime.NakamaConfig.ChannelConfig
{
    public class ChannelConfig
    {
        public bool Persistence;
        public bool Hidden;
        public string ChannelTag;
        public ChannelType ChannelType;

        public ChannelConfig(bool persistence, bool hidden, string channelTag, ChannelType channelType)
        {
            Persistence = persistence;
            Hidden = hidden;
            ChannelTag = channelTag;
            ChannelType = channelType;
        }
    }
}