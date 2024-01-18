using System.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.SocketConfig;
using theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking;

namespace theHesam.NakamaExtension.Runtime.Core
{
    public class SocketExtended
    {
        // Represents the Nakama socket connection
        public ISocket socket;

        // Represents an extended Nakama client
        public ClientExtended ClientExtended;

        // String for representing a tag for the socket
        public string tag;

        // An instance of the Nakama SocketConfig class, representing configuration settings for the socket
        public SocketConfig SocketConfig;
        public MatchFactory MatchFactory = new MatchFactory();


        // Constructor to initialize the SocketExtended instance with a tag, extended client, and socket configuration
        public SocketExtended(string tag, ClientExtended clientExtended, SocketConfig socketConfig)
        {
            this.tag = tag;
            this.ClientExtended = clientExtended;
            SocketConfig = socketConfig;
        }

        // Method for initializing the socket by creating a new Nakama socket using the NewSocket method from the Nakama client
        // It returns a Task<SocketExtended> to allow asynchronous initialization
        public async Task<SocketExtended> Init()
        {
            socket = ClientExtended.Client.NewSocket(true);
            return this;
        }
    }
}