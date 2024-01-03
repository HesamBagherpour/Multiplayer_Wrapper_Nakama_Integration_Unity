using System;
using System.Threading.Tasks;
using Runtime.Factory;
using Runtime.Modules;
using Runtime.NakamaConfig.ClientConfig;
using UnityEngine;

namespace Runtime.Manager
{
    public class NakamaManager : MonoBehaviour
    {
        public ClientFactory ClientFactory;
        private SessionFactory SessionFactory;
        public static NakamaManager Instance;
        private ServerClientConfigs ServerClientConfigs;
        public SessionConfigDevice SessionConfigDevice;

        private async Task Awake()
        {
            Instance = this;
            ServerClientConfigs = new ServerClientConfigs();
            ClientFactory = new ClientFactory();
            SessionFactory = new SessionFactory();
            var (clinteSuccess, localClient) = await ClientFactory.CreateClint("local", ServerClientConfigs);
            if (clinteSuccess)
            {
                // Do something with the created localClient
                Debug.Log(" you are connected to the local NAKAMA server");
                Modules.SessionConfigDevice sessionConfigDevice = new SessionConfigDevice();
                sessionConfigDevice.UniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
                var (sessionSuccess, sessionClient) =await SessionFactory.CreateSession("localSession",localClient, sessionConfigDevice);
                if (sessionSuccess)
                {
                    Debug.Log("Session Tag : "  + sessionClient.tag);
                    Debug.Log("Session  : "  + sessionClient.Session);
                }
            
            }
            else
            {
                // Handle the case where the client creation was not successful
                // You might want to log an error, throw an exception, or take other appropriate actions
            }

           


        }
    }
}
