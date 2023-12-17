using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.NakamaConfig.ClientConfig;
using UnityEngine;

namespace Runtime.Core
{
    public class ClientExtended
    {
        public string tag;
        public IClient Client;
        // public Session factory 
        private readonly ServerClientConfigs _clientConfigs;

        public async UniTask<ClientExtended> Init()
        {
            
            Debug.Log("clintItem ");

            Client = new Client(_clientConfigs.scheme, _clientConfigs.host, _clientConfigs.port,
                _clientConfigs.serverKey, UnityWebRequestAdapter.Instance, _clientConfigs.autoRefreshSession);
            Debug.Log(Client);
                //Debug.Log(await CallRpcAsync());
            return this;
        }

        public ClientExtended(string tag, ServerClientConfigs serverClientConfigs)
        {
            this.tag = tag;
            this._clientConfigs = serverClientConfigs;
            
            
        }

        public async UniTask<string> CallRpcAsync()
        {
            var responce = await Client.RpcAsync("defaulthttpkey", "handShakefromClint");
            return responce.Payload;
        }
    }
}
