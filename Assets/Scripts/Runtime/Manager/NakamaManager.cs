using theHesam.NakamaExtension.Runtime.Factory;
using UnityEngine;

namespace theHesam.NakamaExtension.Runtime.Manager
{
    public class NakamaManager : MonoBehaviour
    {
        public ClientFactory ClientFactory;
        public static NakamaManager Instance;
        private void Awake()
        {
            Instance = this;
            ClientFactory = new ClientFactory();
        }


    }
}