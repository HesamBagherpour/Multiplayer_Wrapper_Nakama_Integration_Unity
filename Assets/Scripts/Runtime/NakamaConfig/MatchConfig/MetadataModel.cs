using System.Runtime.Serialization;
using theHesam.NakamaExtension.Runtime.NakamaConfig.ClientConfig;

namespace Runtime.NakamaConfig.MatchConfig
{
    public class MetadataModel
    {
        [DataMember(Name = "playerStateJoin")] public ClientState PlayerState;
        [DataMember(Name = "playerAvatar")] public string PlayerAvatar;
        [DataMember(Name = "avatarType")] public string AvatarType;
        public MetadataModel(ClientState playerState ,string playerAvatar , string avatarType)
        {
            PlayerState = playerState;
            PlayerAvatar = playerAvatar;
            AvatarType = avatarType;
        }
        public MetadataModel()
        {
            PlayerState = 0;
            PlayerAvatar = "";
            AvatarType = "";
        }
    }
}