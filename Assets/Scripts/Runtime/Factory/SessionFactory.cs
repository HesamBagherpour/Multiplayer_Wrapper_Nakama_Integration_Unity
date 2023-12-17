using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core;
using Runtime.Modules;
using UnityEngine;

namespace Runtime.Factory
{
    public class SessionFactory
    {
        private List<SessionExtended> _sessions = new List<SessionExtended>();
        public Action<SessionExtended> OnCreateSession;
        public string latestTagCreated;
        public async UniTask<Tuple<bool, SessionExtended>> CreateSession<T>(string tag,ClientExtended clientExtended,T config)where T : SessionConfig
        {
         
            var session = _sessions.Find(x => x.tag == tag);
            if (session != null)
            {
                return new Tuple<bool,SessionExtended>(true,session);
            }

            else
            {

                var _session = new SessionExtended();
                var  (_ ,msession ) = await _session.CreateSession(tag,clientExtended,config);
                _sessions.Add(msession);
                OnCreateSession?.Invoke(msession);
                latestTagCreated = tag;
                return new Tuple<bool, SessionExtended>(true, msession);
            }
   
        }

    }
}