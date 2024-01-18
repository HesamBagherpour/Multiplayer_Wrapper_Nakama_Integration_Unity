using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.MatchConfig;
using Runtime.NakamaConfig.OpCode;
using theHesam.NakamaExtension.Runtime.Core;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;

namespace Runtime.Factory
{
    public class MatchMakingFactory
    {
        
        private List<MatchmakerTicketExtended> _i8Match = new List<MatchmakerTicketExtended>();
        public Action<IMatchmakerTicket> OnCreateMatch;
        public string latestTagCreated;
        public string currentMatchmakingTicket;
        public async UniTask<Tuple<bool,GeneralResModel<MatchmakerTicketExtended>>> CreateMatchMaking(string tag,SocketExtended socketExtended,MatchMakingConfig matchMakingConfig) 
        {
            IMatchmakerTicket matchmakerTicket = await socketExtended.socket.AddMatchmakerAsync(matchMakingConfig.query,
                    matchMakingConfig.minPlayers, matchMakingConfig.maxPlayers, matchMakingConfig.matchmakingProperties);
            currentMatchmakingTicket = matchmakerTicket.Ticket;
            MatchmakerTicketExtended matchmakerTicketExtended = new MatchmakerTicketExtended();
            matchmakerTicketExtended.MatchmakerTicket = matchmakerTicket;
            matchmakerTicketExtended.tag = tag;
            _i8Match.Add(matchmakerTicketExtended);
            latestTagCreated = tag;
            return new Tuple<bool, GeneralResModel<MatchmakerTicketExtended>>(true, new GeneralResModel<MatchmakerTicketExtended>(matchmakerTicketExtended));
        }
        public async UniTask<Tuple<bool, GeneralResModel<MatchmakerTicketExtended>>> CreateOrGetClint(string tag, SocketExtended socketExtended ,MatchMakingConfig matchMakingConfig)
        {
            var match = _i8Match.Find(x => x.tag == tag);
            if (match != null)
                return new Tuple<bool,  GeneralResModel<MatchmakerTicketExtended>>(true,new GeneralResModel<MatchmakerTicketExtended>(match));
            return await CreateMatchMaking(tag,socketExtended , matchMakingConfig);
        }
        public async UniTask<MatchmakerTicketExtended> GetMatchmakerTicket(string tag)
        {

            if (_i8Match.Exists(x => x.tag == tag))
            {
                return _i8Match.Find(x => x.tag == tag);
            }
            else
            {
                await UniTask.WaitUntil(() => latestTagCreated == tag );
                return _i8Match.Find(x => x.tag == tag);
            }
        }
    }


}