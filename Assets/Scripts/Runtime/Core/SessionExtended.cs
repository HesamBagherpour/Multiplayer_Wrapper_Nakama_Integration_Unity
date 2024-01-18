using System;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Controllers.Session;
using Runtime.Core;
using Runtime.Modules;
using theHesam.NakamaExtension.Runtime.Factory;

namespace theHesam.NakamaExtension.Runtime.Core
{
    public  class SessionExtended
    {
        public string tag;
        public ISession Session;
        public SocketFactory SocketFactory;
        public SessionConfig sessionConfig;
        public ClientExtended ClientExtended;
        public SessionConnectionController sessionConnectionController;
        
        public async UniTask<Tuple<bool, SessionExtended>> CreateSession<T>(string tag,ClientExtended clientExtended ,T sessionConfig) where T : SessionConfig
        {
            SocketFactory = new SocketFactory();
            switch (typeof(T))
            {
                case
                    var cl when cl== typeof(SessionConfigDevice):{
                    SessionConfigDevice s = sessionConfig as SessionConfigDevice;
                    Session = await clientExtended.Client.AuthenticateDeviceAsync(s.UniqueIdentifier);
                    this.tag = tag;
                    break;
                }
                case
                    var cl when cl == typeof(SessionConfigEmail):{
                    SessionConfigEmail s = sessionConfig as SessionConfigEmail;
                    Session = await clientExtended.Client.AuthenticateEmailAsync(s.username,s.password);
                    this.tag = tag;
                    break;
                }
            }
            return new Tuple<bool, SessionExtended>(true, this);
        }

        public void setSessionConnectionController(SessionConnectionController sessionConnectionController)
        {
            this.sessionConnectionController = sessionConnectionController;
        }




    }
    

}