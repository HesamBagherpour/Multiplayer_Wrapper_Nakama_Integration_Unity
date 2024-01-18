using Nakama;
using theHesam.NakamaExtension.Runtime.Core;

namespace Runtime.Core
{
    public class MatchmakerTicketExtended
    {
        public SocketExtended SocketExtended;
        public ClientExtended ClientExtended;
        public SessionExtended SessionExtended;
        public IMatchmakerTicket MatchmakerTicket;
        public string tag;
        public string matchId;

        public MatchmakerTicketExtended()
        {
        }

        public MatchmakerTicketExtended(SocketExtended socketExtended, ClientExtended clientExtended, SessionExtended sessionExtended, IMatchmakerTicket matchmakerTicket, string tag, string matchId)
        {
            this.SocketExtended = socketExtended;
            this.ClientExtended = clientExtended;
            this.SessionExtended = sessionExtended;
            MatchmakerTicket = matchmakerTicket;
            this.tag = tag;
            this.matchId = matchId;
        }
    }
}