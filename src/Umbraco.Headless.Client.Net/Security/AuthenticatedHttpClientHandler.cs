using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Security
{
    /// <summary>
    /// HttpClientHandler which handles retrieving and using an oAuth token
    /// for use with the Content Delivery and Content Management APIs
    /// </summary>
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly IPasswordBasedConfiguration _configuration;
        private DateTime _tokenExpirationTime;
        private string _token;

        public AuthenticatedHttpClientHandler(IPasswordBasedConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth == null)
            {
                var token = await GetToken(_configuration.Username, _configuration.Password).ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task<string> GetToken(string username, string password)
        {
            if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _tokenExpirationTime)
            {
                return await Task.FromResult(_token).ConfigureAwait(false);
            }

            var formData = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };

            var response = await RestService.For<OAuthEndpoints>(Constants.Urls.BaseApiUrl)
                .GetAuthToken(_configuration.ProjectAlias, formData).ConfigureAwait(false);

            _token = response.AccessToken;
            _tokenExpirationTime = DateTime.UtcNow
                .AddSeconds(response.ExpiresIn)
                .Subtract(new TimeSpan(0, 0, 5, 0));

            return _token;
        }
    }
}
