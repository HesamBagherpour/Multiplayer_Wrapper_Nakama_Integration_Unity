using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Nakama;

namespace Runtime.Utilities
{
    public static class NakamaRpc
    {
        public static async UniTask<string> SendRpc<T>(
            IClient clint,
            ISession session,
            string rpcName,
            float timeoutSec = 0,
            string payload = null,
            RetryConfiguration retryConfiguration = null,
            CancellationToken canceller = default(CancellationToken))
        {
            List<CancellationToken> cancellationTokenList = new List<CancellationToken>();
            cancellationTokenList.Add(canceller);

            CancellationTokenSource timeoutToken = null;
            if (timeoutSec > 0)
            {
                timeoutToken = new CancellationTokenSource();
                timeoutToken.CancelAfter(TimeSpan.FromSeconds(timeoutSec));
                cancellationTokenList.Add(timeoutToken.Token);
            }

            var linkedTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenList.ToArray());
            var res = await clint.RpcAsync(
                session,
                rpcName,
                payload, canceller: linkedTokenSource.Token);
            return res.Payload;
        }
    }
}