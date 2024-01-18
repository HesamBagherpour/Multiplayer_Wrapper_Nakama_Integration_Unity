using System;
using Runtime.Controllers;
using Runtime.Controllers.Match;
using Runtime.Controllers.Match.MatchMaking;
using Runtime.Controllers.Match.MatchMaking.Model;
using Runtime.Controllers.Session;
using Runtime.Core;
using Runtime.Factory;
using Runtime.Manager;
using Runtime.Modules;
using Runtime.NakamaConfig.ClientConfig;
using Runtime.NakamaConfig.MatchConfig;
using Runtime.NakamaConfig.OpCode;
using Runtime.NakamaConfig.SocketConfig;
using theHesam.NakamaExtension.Runtime.Controllers.Channel;
using theHesam.NakamaExtension.Runtime.Controllers.Match.MatchMaking;
using theHesam.NakamaExtension.Runtime.Core;
using UnityEngine;

namespace theHesam.NakamaExtension.Sample.Samples.NakamaSample.Scripts
{
    public class ChatConnection : MonoBehaviour
    {
        // Start is called before the first frame update
        public MatchMessageController _matchMessageController;
        public MatchConnectionController _matchConnectionController;

        public string clientTag = "aws";
        public string sessionTag = "google";
        public string matchTag = "chat";

        [SerializeField] private ServerClientConfigs serverClientChatConfigs;

        private void Start()
        {
            var uniqueIdentifier = Guid.NewGuid().ToString();
            SessionConfigDevice sessionConfig = new SessionConfigDevice(uniqueIdentifier);
            SocketConfig socketConfig = new SocketConfig();
            MatchMakingGeneralModel matchMakingConfig = new RpcMatchMakingConfig();
            MatchFactory matchFactory = new MatchFactory();
            ChannelsFactory channelsFactory = new ChannelsFactory();
            MatchConfig matchConfig = new MatchConfig(matchTag);
            Init(clientTag, sessionTag, matchConfig.GETMatchName(),
                serverClientChatConfigs, sessionConfig, socketConfig, matchMakingConfig, matchFactory, channelsFactory, matchConfig);
        }

        async void Init<T>(string clientTagName, string sessionTagName, string socketTagName, ServerClientConfigs clientConfig,
            T config, SocketConfig socketConfig, MatchMakingGeneralModel matchMakingConfig, MatchFactory matchFactory, ChannelsFactory channelsFactory, MatchConfig matchConfig)
            where T : SessionConfig
        {
            (_, ClientExtended client) = await NakamaManager.Instance.ClientFactory.CreateClient(clientTagName, clientConfig); // create client
            (_, SessionExtended session) = await client.SessionFactory.CreateSession(sessionTagName, client, config); // create session 
            (_, SocketExtended chatSocket) = await session.SocketFactory.CreateSocket(socketTagName, client, socketConfig); // create  socket 
            if (TryGetComponent<SessionConnectionController>(out SessionConnectionController sessionConnectionController))
            {
                sessionConnectionController.Init(client, session, true, 2, false);
                session.setSessionConnectionController(sessionConnectionController);
                session.sessionConnectionController = sessionConnectionController;
            } // setup session Controller 

            if (TryGetComponent<MatchConnectionController>(out MatchConnectionController matchConnectionController))
            {
                matchConnectionController.Init(chatSocket, matchConfig);
            } // setup Match  Controller 

            if (TryGetComponent<ChannelMessageController>(out ChannelMessageController channelConnectionController))
            {
                channelConnectionController.Init(chatSocket.socket);
            } // setup channel  Controller 

            if (TryGetComponent<SocketConnectionController>(out SocketConnectionController socketConnectionController))
            {
                //socketConnectionController.Init(client, session, chatSocket, new SocketInitialConfig(CheckingConnectionType.PingPongMatch));
                await socketConnectionController.ConnectSocket(chatSocket, session, socketConfig);
            } // setup socket  Controller 

            (_, GeneralResModel<MatchExtended> match) =
                await chatSocket.MatchFactory.CreateMatch(matchTag, client, session, matchMakingConfig, _matchMessageController, _matchConnectionController); // create Match
            if (TryGetComponent<MatchMessageController>(out MatchMessageController matchMessageController))
            {
                matchMessageController.Init(chatSocket, match.data.matchId);
            }

            await matchConnectionController.ConnectMatch(match.data.matchId, this._matchMessageController);


            #region test

            // if (TryGetComponent<SocketPingPongConnection>(out SocketPingPongConnection socketPingPongConnection))
            // {
            //     return;
            //     await socketPingPongConnection.Init(chatSocket);
            //     chatSocket.socket.ReceivedMatchState += delegate(IMatchState state)
            //     {
            //         socketPingPongConnection.Raise(state);
            //     };
            //     socketPingPongConnection.StartPingPong();
            // }


            //
            // // if (TryGetComponent<OpCodeGenerator>(out OpCodeGenerator opCodeGenerator))
            // // {
            // //     _opCodeGenerator = opCodeGenerator;
            // //     _opCodeGenerator.Init(session.Session.Username,match.data.matchId);
            // //     matchConnectionController.OnMatchConnect?.Invoke(matchConnectionController._matchId);
            // // }
            // //
            //
            // // Debug.unityLogger.Log(await client.client.RpcAsync(session.Session,"test"));
            // //
            // ChannelConfig channelConfig = new ChannelConfig(true ,true,"Chat",ChannelType.Group);
            // var (_, channel) = await channelsFactory.CreateChannel(session.Session.UserId,chatSocket,channelConfig);

            // var content = new Dictionary<string, string> {{"hello", "world"}};
            // await channelConnectionController.SendMessages(channel.Id, content);
            //SendChannelMessage(channel.Id, channelConnectionController);


            // var opCode = 300;
            // var newState = new Dictionary<string, string> {{"test", "test"}}.ToJson();
            // await chatSocket.socket.SendMatchStateAsync(match.data.matchId,opCode,newState);


            //
            // private async void SendChannelMessage(string channelId , ChannelMessageController channelConnectionController)
            // {
            //     
            //     var content = new Dictionary<string, string> {{"hello", "world"}};
            //     await channelConnectionController.SendMessages(channelId, content);
            // }

            #endregion
        }
    }
}