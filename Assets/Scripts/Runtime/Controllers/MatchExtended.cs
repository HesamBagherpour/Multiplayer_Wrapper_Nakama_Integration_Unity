using Runtime.Controllers.Match;
using Runtime.Core;
using theHesam.NakamaExtension.Runtime.Core;

namespace Runtime.Controllers
{
    public class MatchExtended
    {
        public SocketExtended SocketExtended;
        public ClientExtended ClientExtended;
        public SessionExtended SessionExtended;
        public string tag;
        public string matchId;
        public long serverTime;
        public MatchMessageController matchMessageController;
        public MatchConnectionController matchConnectionController;
        
        public MatchExtended()
        {
        }

        public MatchExtended(string tag, string matchId , long serverTime, ClientExtended clientExtended, SessionExtended sessionExtended, MatchMessageController matchMessage , MatchConnectionController  matchConnection) {

            this.ClientExtended = clientExtended;
            this.SessionExtended = sessionExtended;
            this.serverTime = serverTime;
            this.tag = tag;
            this.matchId = matchId;
            matchMessageController = matchMessage;
            matchConnectionController = matchConnection;


        }
        public MatchExtended(string tag, string matchId, ClientExtended clientExtended, SessionExtended sessionExtended) {

            this.ClientExtended=clientExtended;
            this.SessionExtended=sessionExtended;
            this.tag=tag;
            this.matchId=matchId;
        }
    }
}