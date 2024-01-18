using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core;
using theHesam.NakamaExtension.Runtime.NakamaConfig.ClientConfig;

namespace theHesam.NakamaExtension.Runtime.Factory
{
    public class ClientFactory 
    {
        private List<ClientExtended> _clients = new List<ClientExtended>();
        public Action<ClientExtended> OnCreateClint;
        public Dictionary<string, Action<ClientExtended>> ClientFactoryCallBack = new Dictionary<string, Action<ClientExtended>>();

        public string latestTagCreated;
        
        public async UniTask<Tuple<bool, ClientExtended>> CreateClient(string tag, ServerClientConfigs config)
        {
            _clients = new List<ClientExtended>();
            var client = _clients.Find(x => x.tag == tag);
            if (client != null)
                return new Tuple<bool, ClientExtended>(true,client);
            
            client = new ClientExtended(tag, config);
            _clients.Add(await client.Init());
            OnCreateClint?.Invoke(client);
            latestTagCreated = tag;
            return new Tuple<bool, ClientExtended>(true, client);
        }
        public async UniTask<Tuple<bool, ClientExtended>> CreateOrGetClint(string tag, ServerClientConfigs config)
        {
            //TODO check if not exist tag or name - return error if exist
            var client = _clients.Find(x => x.tag == tag);
            if (client != null)
                return new Tuple<bool, ClientExtended>(true,client);
            return await CreateClient(tag, config);

        }
        public ClientExtended GetClint(string tag)
        {
            return _clients.Find(x => x.tag == tag);
        }
        
        //get clint as Dictionary callBack 
        public async UniTask<ClientExtended> GetClintAsync(string tag)
        {

            if (_clients.Exists(x => x.tag == tag))
            {
                return _clients.Find(x => x.tag == tag);
            }
            else
            {
                await UniTask.WaitUntil(() => latestTagCreated == tag );
               return _clients.Find(x => x.tag == tag);
            }
        }
    }
}