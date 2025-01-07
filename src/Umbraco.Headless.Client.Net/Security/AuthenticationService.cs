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
        private readonly RefitSettings _refitSettings;

        public AuthenticationService(string projectAlias, RefitSettings refitSettings, HttpClient cdnHttpClient = null,
            HttpClient apiHttpClient = null) : this(new HeadlessConfiguration(projectAlias), refitSettings, cdnHttpClient, apiHttpClient)
        {
        }

        public AuthenticationService(IHeadlessConfiguration configuration, RefitSettings refitSettings, HttpClient cdnHttpClient = null, HttpClient apiHttpClient = null)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _refitSettings = refitSettings;
            _cdnHttpClient = cdnHttpClient;
            _apiHttpClient = apiHttpClient;
        }

        private HttpClient ApiHttpClient => _apiHttpClient ?? (_apiHttpClient = new HttpClient {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)});
        private HttpClient CdnHttpClient => _cdnHttpClient ?? (_cdnHttpClient = new HttpClient {BaseAddress = new Uri(Constants.Urls.BaseCdnUrl)});

        private UserOAuthEndpoints ApiEndpoints => _apiEndpoints ?? (_apiEndpoints = RestService.For<UserOAuthEndpoints>(ApiHttpClient, _refitSettings));
        private MemberOAuthEndpoints CdnEndpoints => _cdnEndpoints ?? (_cdnEndpoints = RestService.For<MemberOAuthEndpoints>(CdnHttpClient, _refitSettings));

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
