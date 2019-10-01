using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Security;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class OAuthTokenFixture
    {
        [Fact]
        public async Task Can_Retrieve_oAuth_Token()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("/oauth/token").Respond("application/json", OAuthJson.GetToken);
            var client = new HttpClient(mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            var service = RestService.For<OAuthEndpoints>(client);


            var formData = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", "test@test.com"},
                {"password", "}8N7aOE8#@"}
            };

            var response = await service.GetAuthToken(formData);
            Assert.NotNull(response);
            Assert.Equal("bearer", response.TokenType);
            Assert.Equal(86399, response.ExpiresIn);
            Assert.False(string.IsNullOrEmpty(response.AccessToken));
        }
    }

    interface OAuthEndpoints
    {
        [Post("/oauth/token")]
        Task<OAuthResponse> GetAuthToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);
    }
}
