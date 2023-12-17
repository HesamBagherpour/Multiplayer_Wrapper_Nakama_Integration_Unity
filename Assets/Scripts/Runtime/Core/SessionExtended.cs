using System;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Modules;
using Runtime.NakamaConfig.SessionConfig;
using UnityEngine;

namespace Runtime.Core
{
    public class SessionExtended
    {
        public string tag;
        public ISession Session;
        public ServerSessionConfigs SessionConfigs;
        public ClientExtended ClientExtended;

        public async UniTask<Tuple<bool, SessionExtended>> CreateSession<T>
            (string tag, ClientExtended clientExtended, T sessionConfig)
            where T : SessionConfig
        {

            switch (typeof(T))
            {
                case
                    var cl when cl == typeof(SessionConfigDevice):
                {
                    
                    SessionConfigDevice s = sessionConfig as SessionConfigDevice;
                    Session = await clientExtended.Client.AuthenticateDeviceAsync(s.UniqueIdentifier);
                    this.tag = tag;
                    break;
                }
                case
                    var cl when cl == typeof(SessionConfigEmail):
                {
                    SessionConfigEmail s = sessionConfig as SessionConfigEmail;
                    Session = await clientExtended.Client.AuthenticateEmailAsync(s.username, s.password);
                    this.tag = tag;
                    break;
                }
            }

            return new Tuple<bool, SessionExtended>(true, this);
        }
    }



}