using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Umbraco.Headless.Client.Net.Security
{
    /// <summary>
    /// Used to retrieve access tokens for requests to the APIs.
    /// </summary>
    public interface IAccessTokenResolver
    {
        /// <summary>
        /// Get an access token for the current request.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> that's about te be sent.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An access token if one exists otherwise null.</returns>
        Task<string> GetToken(HttpRequestMessage request, CancellationToken cancellationToken = default);
    }
}
