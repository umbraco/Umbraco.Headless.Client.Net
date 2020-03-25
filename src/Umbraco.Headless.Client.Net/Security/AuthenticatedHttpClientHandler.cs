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
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly IAccessTokenResolver _accessTokenResolver;

        [Obsolete("Use the overload that takes an ITokenResolver")]
        public AuthenticatedHttpClientHandler(IPasswordBasedConfiguration configuration)
        {
            var authenticationService = new AuthenticationService(configuration);
            _accessTokenResolver = new UserPasswordAccessTokenResolver(configuration.Username, configuration.Password, authenticationService);

        }

        public AuthenticatedHttpClientHandler(IAccessTokenResolver accessTokenResolver, HttpMessageHandler handler = null) : base(handler)
        {
            _accessTokenResolver = accessTokenResolver ?? throw new ArgumentNullException(nameof(accessTokenResolver));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth == null)
            {
                var token = await _accessTokenResolver.GetToken(request, cancellationToken).ConfigureAwait(false);
                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
