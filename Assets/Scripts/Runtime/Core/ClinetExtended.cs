using Cysharp.Threading.Tasks;
using Nakama;
using theHesam.NakamaExtension.Runtime.Factory;
using theHesam.NakamaExtension.Runtime.NakamaConfig.ClientConfig;
using UnityEngine;

namespace Runtime.Core
{
    public class ClientExtended
    {
        public string tag;
        public IClient Client;
        public SessionFactory SessionFactory;
        private readonly ServerClientConfigs _clientConfig;
        

        public async UniTask<ClientExtended> Init()
        {
            Debug.Log("clintItem");
            Client = new Client(_clientConfig.scheme, _clientConfig.host,
                _clientConfig.port,
                _clientConfig.serverKey,
                UnityWebRequestAdapter.Instance,
                _clientConfig.autoRefreshSession);

            //Debug.Log(await CallRpcAsync());
            SessionFactory = new SessionFactory();

            return this;
        }

        public ClientExtended(string tag, ServerClientConfigs serverClientConfigs)
        {
            this.tag = tag;
            this._clientConfig = serverClientConfigs;
            SessionFactory = new SessionFactory();
            
            
        }

        public async UniTask<string> CallRpcAsync()
        {
            var responce = await Client.RpcAsync("defaulthttpkey", "handShakefromClint");
            return responce.Payload;
        }
    }
}
