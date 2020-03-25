using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Security
{
    public class UserPasswordAccessTokenResolver : IAccessTokenResolver
    {
        private readonly string _username;
        private readonly string _password;
        private readonly IAuthenticationService _authenticationService;
        private string _token;
        private DateTime _tokenExpirationTime;

        public UserPasswordAccessTokenResolver(string username, string password, IAuthenticationService authenticationService)
        {
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        public async Task<string> GetToken(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _tokenExpirationTime)
                return await Task.FromResult(_token).ConfigureAwait(false);

            var response = await _authenticationService.AuthenticateUser(_username, _password).ConfigureAwait(false);

            _token = response.AccessToken;
            _tokenExpirationTime = DateTime.UtcNow
                .AddSeconds(response.ExpiresIn)
                .Subtract(new TimeSpan(0, 0, 5, 0));

            return response.AccessToken;
        }
    }
}
