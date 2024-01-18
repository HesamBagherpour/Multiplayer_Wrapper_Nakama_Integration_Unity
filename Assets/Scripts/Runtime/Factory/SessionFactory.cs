using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core;
using Runtime.Modules;
using theHesam.NakamaExtension.Runtime.Core;

namespace theHesam.NakamaExtension.Runtime.Factory
{
    public class SessionFactory
    {
        private List<SessionExtended> _EXsessions = new List<SessionExtended>();
        public Action<SessionExtended> OnCreateSession;
        public string latestTagCreated;
        public async UniTask<Tuple<bool, SessionExtended>> CreateSession<T>(string tag,ClientExtended clientExtended ,T config) where T : SessionConfig
        {
            var session = _EXsessions.Find(x => x.tag == tag);
            if (session != null)
                return new Tuple<bool,SessionExtended>(true,session);

            var _session = new SessionExtended();
            var  (_ ,msession ) = await _session.CreateSession(tag,clientExtended,config);
            _EXsessions.Add(msession);
            OnCreateSession?.Invoke(msession);
            latestTagCreated = tag;
            return new Tuple<bool, SessionExtended>(true, msession);
        }
        public async UniTask<Tuple<bool, SessionExtended>> CreateOrGetSession<T>(string tag,ClientExtended clientExtended ,T config) where T : SessionConfig
        {
            
            //TODO check if not exist tag or name - return error if exist
            var _sesssion = _EXsessions.Find(x => x.tag == tag);
            if (_sesssion != null)
                return new Tuple<bool, SessionExtended>(true,_sesssion);
            return await CreateSession(tag,clientExtended, config);
            
        }
        
        
        public async UniTask<SessionExtended> GetSession(string tag)
        {

            if (_EXsessions.Exists(x => x.tag == tag))
            {
                return _EXsessions.Find(x => x.tag == tag);
            }
            else
            {
                await UniTask.WaitUntil(() => latestTagCreated == tag );
                return _EXsessions.Find(x => x.tag == tag);
            }
        }
    }
}