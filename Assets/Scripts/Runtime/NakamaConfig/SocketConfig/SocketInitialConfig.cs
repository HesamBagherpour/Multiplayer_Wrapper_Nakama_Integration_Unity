namespace Runtime.NakamaConfig.SocketConfig
{
    public class SocketInitialConfig
    {
        public CheckingConnectionType checkingConnectionType;

        public int interval;
        public int retryCount;

        public SocketInitialConfig(CheckingConnectionType checkingConnectionType, int interval, int retryCount)
        {
            this.checkingConnectionType = checkingConnectionType;
            this.interval = interval;
            this.retryCount = retryCount;
        }
    }

    public enum CheckingConnectionType
    {
        None,
        PingPongMatch,
        PingPongRpc
    }
}