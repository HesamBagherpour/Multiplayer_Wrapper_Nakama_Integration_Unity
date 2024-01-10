using System;

namespace Runtime.NakamaConfig.SocketConfig
{
    // Marks the class as serializable, allowing instances to be used in Unity's serialization system
    [Serializable]
    public class SocketConfig
    {
        // Boolean indicating whether the client should appear online
        public readonly bool AppearOnline;

        // Integer representing the connection timeout in seconds
        public readonly int ConnectionTimeout;

        // Default constructor initializes AppearOnline to false and ConnectionTimeout to 20 seconds
        // This constructor is called when creating an instance of SocketConfig
        public SocketConfig()
        {
            AppearOnline = false;
            ConnectionTimeout = 20;
        }
    }
}