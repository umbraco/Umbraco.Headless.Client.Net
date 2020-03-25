using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Umbraco.Headless.Client.Net.Security
{
    interface UserOAuthEndpoints
    {
        [Post("/oauth/token")]
        Task<OAuthResponse> GetAuthToken([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);
    }

    interface MemberOAuthEndpoints
    {
        [Post("/member/oauth/token")]
        Task<OAuthResponse> GetAuthToken([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);
    }
}
