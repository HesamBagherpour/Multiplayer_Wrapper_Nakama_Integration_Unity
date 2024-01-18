namespace theHesam.NakamaExtension.Runtime.NakamaConfig.ClientConfig
{
    public  class ServerClientConfigs
    {

        public string scheme = "http";
        public string host = "localhost";
        public int port = 7350;
        public string serverKey = "defaultkey";
        public bool autoRefreshSession = true;

        public ServerClientConfigs(string scheme, string host, int port, string serverKey, bool autoRefreshSession)
        {
            this.scheme = scheme;
            this.host = host;
            this.port = port;
            this.serverKey = serverKey;
            this.autoRefreshSession = autoRefreshSession;
        }

        public ServerClientConfigs()
        {
            this.scheme = "http";
            this.host = "localhost";
            this.port = 7350;
            this.serverKey = "defaultkey";
            this.autoRefreshSession = true;
        }
    }
}
