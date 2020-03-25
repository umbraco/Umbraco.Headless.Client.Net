using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Umbraco.Headless.Client.Net.Security
{
    public class FuncAccessTokenResolver : IAccessTokenResolver
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<string>> _getToken;

        public FuncAccessTokenResolver(Func<HttpRequestMessage, CancellationToken, Task<string>> getToken)
        {
            _getToken = getToken ?? throw new ArgumentNullException(nameof(getToken));
        }

        public Task<string> GetToken(HttpRequestMessage request, CancellationToken cancellationToken = default) =>
            _getToken(request, cancellationToken);
    }
}
