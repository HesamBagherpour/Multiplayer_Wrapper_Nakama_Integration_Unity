using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Runtime.Controllers.Match;
using Runtime.Core;
using Runtime.NakamaConfig.OpCode;
using theHesam.NakamaExtension.Runtime.Controllers.Match;
using theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking;
using theHesam.NakamaExtension.Runtime.Core;
using theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig;
using UnityEngine;
using ManualMatchMakingConfig = theHesam.NakamaExtension.Runtime.NakamaConfig.MatchConfig.ManualMatchMakingConfig;

namespace theHesam.NakamaExtension.Runtime.Factory
{
    public class MatchFactory
    {
        private List<MatchExtended> _matchExtended = new List<MatchExtended>();
        public Action<MatchExtended> OnCreateMatch;
        public string latestTagCreated;

        public async UniTask<Tuple<bool, GeneralResModel<MatchExtended>>> CreateMatch(
            string tag, 
            ClientExtended clientExtended,
            SessionExtended sessionExtended, 
            MatchMakingGeneralModel matchMakingConfig, 
            MatchMessageController matchMessageController,
            MatchConnectionController matchConnectionController)
        {
            string matchMakingResponse = null;

            switch (matchMakingConfig.MatchMakingType)
            {
                case MatchMakingType.MatchListing:
                    Debug.unityLogger.Log("MatchFactory | MatchListing | MatchMakingType.Manual");
                    matchMakingResponse = await new ManualMatchMaking().StartMatchMaking((ManualMatchMakingConfig)matchMakingConfig);
                    break;
                case MatchMakingType.MatchMaking:
                    Debug.unityLogger.Log("MatchFactory | MatchMaking | MatchMakingType.RPC");
                    matchMakingResponse = await new RpcMatchMaking().StartMatchMaking((RpcMatchMakingConfig) matchMakingConfig);
                    break;
            }

            if (matchMakingResponse == null)
                return new Tuple<bool, GeneralResModel<MatchExtended>>(false, null);

            MatchData matchIdData = JsonConvert.DeserializeObject<MatchData>(matchMakingResponse);
            // MatchData matchIdData = new MatchData(matchId);

            MatchExtended matchExtendedData = new MatchExtended(tag, matchIdData.matchId,matchIdData.serverTime, clientExtended, sessionExtended, matchMessageController, matchConnectionController);
            _matchExtended.Add(matchExtendedData);
            OnCreateMatch?.Invoke(matchExtendedData);
            latestTagCreated = tag;
            return new Tuple<bool, GeneralResModel<MatchExtended>>(true, new GeneralResModel<MatchExtended>(matchExtendedData));
        }
        //Get - Get or Create 

        public async UniTask<Tuple<bool, GeneralResModel<MatchExtended>>> CreateOrGetClint(string tag, ClientExtended clientExtended,
            SessionExtended sessionExtended, MatchMakingGeneralModel matchMakingConfig, MatchMessageController matchMessageController, MatchConnectionController matchConnectionController)
        {
            var match = _matchExtended.Find(x => x.tag == tag);
            if (match != null)
                return new Tuple<bool, GeneralResModel<MatchExtended>>(true, new GeneralResModel<MatchExtended>(match));

            return await CreateMatch(tag, clientExtended, sessionExtended, matchMakingConfig, matchMessageController, matchConnectionController);
        }


        public async UniTask<MatchExtended> GetMatch(string tag)
        {
            if (_matchExtended.Exists(x => x.tag == tag))
            {
                return _matchExtended.Find(x => x.tag == tag);
            }
            else
            {
                await UniTask.WaitUntil(() => latestTagCreated == tag);
                return _matchExtended.Find(x => x.tag == tag);
            }
        }
    }
}