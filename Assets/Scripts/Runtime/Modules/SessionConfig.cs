using UnityEngine.Device;

namespace Runtime.Modules
{
    public class SessionConfig
    {

    }
    
    public class SessionConfigDevice: SessionConfig
    {
        public  string UniqueIdentifier;

        public SessionConfigDevice()
        {
            UniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        }

        public SessionConfigDevice(string uniqueIdentifier)
        {
            this.UniqueIdentifier = uniqueIdentifier;
        }
    }
    
    public class SessionConfigEmail: SessionConfig
    {
        public string username;
        public string password;
        public SessionConfigEmail()
        {
     
        }
    }
}