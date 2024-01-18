using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core;
using Runtime.NakamaConfig.SocketConfig;
using theHesam.NakamaExtension.Runtime.Core;

namespace theHesam.NakamaExtension.Runtime.Factory
{
    public class SocketFactory
    {
        // List to keep track of created SocketExtended instances
        private readonly List<SocketExtended> _socketList;

        // Keeps track of the most recent tag for which a socket was created
        private string _latestTagCreated;

        // Reference to the latest created SocketExtended instance
        private SocketExtended _socketExtended;

        // Constructor to initialize the list
        public SocketFactory()
        {
            _socketList = new List<SocketExtended>();
        }

        // Event that notifies external classes when a new SocketExtended is created
        public Action<SocketExtended> OnCreateSocket;

        // Method to create a new SocketExtended or return an existing one
        public async UniTask<Tuple<bool, SocketExtended>> CreateSocket(string tag, ClientExtended clientExtended, SocketConfig config)
        {
            // Check if a socket with the given tag already exists
            _socketExtended = _socketList.Find(x => x.tag == tag);
            if (_socketExtended != null)
                return new Tuple<bool, SocketExtended>(true, _socketExtended);

            // Create a new SocketExtended and initialize it
            _socketExtended = new SocketExtended(tag, clientExtended, config);
            _socketExtended = await _socketExtended.Init();

            // Add the new socket to the list, invoke the event, and update the latestTagCreated
            _socketList.Add(_socketExtended);
            OnCreateSocket?.Invoke(_socketExtended);
            _latestTagCreated = tag;

            // Return a Tuple indicating successful creation and the new SocketExtended instance
            return new Tuple<bool, SocketExtended>(true, _socketExtended);
        }

        // Method to create a new SocketExtended or return an existing one
        public async UniTask<Tuple<bool, SocketExtended>> CreateOrGetSocket(string tag, ClientExtended clientExtended, SocketConfig config)
        {
            // Check if a socket with the given tag already exists
            var socket = _socketList.Find(x => x.tag == tag);
            if (socket != null)
                return new Tuple<bool, SocketExtended>(true, socket);

            // If the socket doesn't exist, invoke CreateSocket to create a new one
            return await CreateSocket(tag, clientExtended, config);
        }

        // Method to get a socket with the specified tag from the list
        public async UniTask<SocketExtended> GetSocket(string tag)
        {
            // If the socket with the tag already exists, return it
            if (_socketList.Exists(x => x.tag == tag))
            {
                return _socketList.Find(x => x.tag == tag);
            }
            else
            {
                // If the socket doesn't exist, wait until the latestTagCreated matches the specified tag
                await UniTask.WaitUntil(() => _latestTagCreated == tag);

                // Return the socket with the specified tag
                return _socketList.Find(x => x.tag == tag);
            }
        }
    }
}
