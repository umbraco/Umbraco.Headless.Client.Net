using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Umbraco.Headless.Client.Net.Security
{
    public interface IOAuthEndpoints
    {
        [Post("/oauth/token")]
        Task<OAuthResponse> GetAuthToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);
    }
}
