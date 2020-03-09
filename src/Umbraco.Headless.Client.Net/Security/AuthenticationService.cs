using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHeadlessConfiguration _configuration;
        private HttpClient _cdnHttpClient;
        private HttpClient _apiHttpClient;
        private UserOAuthEndpoints _apiEndpoints;
        private MemberOAuthEndpoints _cdnEndpoints;

        public AuthenticationService(string projectAlias, HttpClient cdnHttpClient = null,
            HttpClient apiHttpClient = null) : this(new BasicHeadlessConfiguration(projectAlias), cdnHttpClient, apiHttpClient)
        {
        }

        public AuthenticationService(IHeadlessConfiguration configuration, HttpClient cdnHttpClient = null, HttpClient apiHttpClient = null)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _cdnHttpClient = cdnHttpClient;
            _apiHttpClient = apiHttpClient;
        }

        private HttpClient ApiHttpClient => _apiHttpClient ?? (_apiHttpClient = new HttpClient {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)});
        private HttpClient CdnHttpClient => _cdnHttpClient ?? (_cdnHttpClient = new HttpClient {BaseAddress = new Uri(Constants.Urls.BaseCdnUrl)});

        private UserOAuthEndpoints ApiEndpoints => _apiEndpoints ?? (_apiEndpoints = RestService.For<UserOAuthEndpoints>(ApiHttpClient));
        private MemberOAuthEndpoints CdnEndpoints => _cdnEndpoints ?? (_cdnEndpoints = RestService.For<MemberOAuthEndpoints>(CdnHttpClient));

        public async Task<OAuthResponse> AuthenticateUser(string username, string password)
        {
            var formData = GetFormData(username, password);

            return await ApiEndpoints.GetAuthToken(_configuration.ProjectAlias, formData).ConfigureAwait(false);
        }

        public async Task<OAuthResponse> AuthenticateMember(string username, string password)
        {
            var formData = GetFormData(username, password);

            return await CdnEndpoints.GetAuthToken(_configuration.ProjectAlias, formData).ConfigureAwait(false);
        }

        private static Dictionary<string, string> GetFormData(string username, string password)
        {
            return new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };
        }
    }
}
