using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nakama;
using Runtime.Core;
using Runtime.NakamaConfig.ClientConfig;
using UnityEngine;

namespace Runtime.Factory
{

    public class ClientFactory
    {

        private List<ClientExtended> _client = new List<ClientExtended>();
        public Action<ClientExtended> OnCreateClint;    


        public async UniTask<Tuple<bool,ClientExtended>> CreateClint(string tag, ServerClientConfigs configs)
        {
            _client = new List<ClientExtended>();
            var client = _client.Find(x => x.tag == tag);
            if (client != null)
            {
                return new Tuple<bool, ClientExtended>(true , client);
            }
            else
            {

                // create new clint 
                client = new ClientExtended(tag, configs);
                _client.Add(await client.Init());
                OnCreateClint?.Invoke(client);
                return new Tuple<bool, ClientExtended>(true, client);
            }

        }
        

    }
}
