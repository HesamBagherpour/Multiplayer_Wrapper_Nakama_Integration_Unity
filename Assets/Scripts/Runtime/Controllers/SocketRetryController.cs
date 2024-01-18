using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Controllers
{
    public class SocketRetryController
    {
        private int retryCount;
        private SocketConnectionController socketConnectionController;

        public event Action OnTryingFailed;
        public event Action OnTryingSucceed;

        public SocketRetryController(SocketConnectionController socketConnectionController, int retryCount)
        {
            this.retryCount = retryCount;
            this.socketConnectionController = socketConnectionController;
        }

        public async UniTask StartRetry()
        {
            int counter = 0;

            while (counter < retryCount)
            {
                await socketConnectionController.ConnectSocket(socketConnectionController.socketExtended, socketConnectionController.sessionExtended, socketConnectionController.socketConfig);

                if (socketConnectionController.socketExtended.socket.IsConnected)
                {
                    Debug.Log("OnTrying Reconnect Succeed ");
                    OnTryingSucceed?.Invoke();
                    return;
                }

                counter++;
            }

            if (!socketConnectionController.socketExtended.socket.IsConnected)
            {
                Debug.Log("OnTrying Reconnect Failed ");
                OnTryingFailed?.Invoke();
            }
        }
    }
}