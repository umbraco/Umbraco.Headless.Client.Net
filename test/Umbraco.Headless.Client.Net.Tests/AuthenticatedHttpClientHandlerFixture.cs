using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class AuthenticatedHttpClientHandlerFixture
    {
        [Fact]
        public async Task Can_Retrieve_User_oAuth_Token()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect("/")
                .WithHeaders("Authorization", "Bearer zX8UgaHFfOQcHqew4KMbWzo9fQ2fSbdCwMhgwl7hALL2lm2APy9juXh6tJoweort6CVZq5vZ8CQYyGlJqlJ-hz_LyAQChYHhTxIk2N62B9i47vIetAFOYJN5nQbPC8v2qS8fA8zZ1N9UJxbCHiPQhJIQBgg0kZkauHcRdinZP9TJcJZCdFQPCEh1iSCVk9K-NmA6j_hGoqs3EhKuNBjazYb6tiINUgpWJ8gvlPRlqFODL0MNUDqVJc0hMHqbyaacrfcfJy0o3-59SnpK2OMqHqB_DNe5-2oAOXbFG7-j8CfQtpM7SIzb1nmQahyOjpTkVEmkYCOsr2sKGUDz0N7cAiWHvzjJfnog1nJM8pn-dF22N1x9TuoFd_jfuUQcOkgexQllJiZAA2dd_P7o-EqtkU4NKr-X3JNHPxd00PwcZ40Uzt53PmJA8jdnh25GYlugogvCsNGzYEwJo31VB-1pnHmckV_sK8IpFc9LM8zN40WIEFM4TGvRX4XBEX9ntSu0Rke43IBZHVULPEp0nTquI_amZ7zUGiDewRJTkEFCT8FLnuWMQ09q5lO0BbQ49G1Yp88Uy3XJfVd1NPTiHQfe1mm-hiHhVGHINna-UADE4Iis8tuKFNxj6zZkqXj2k8iaQRrKPZ7zfb8yWQr2-O9lCoqk8mL2q7jGXuJr93tQlW8f8B8FIyc9rF5bMAskyQK4SRezVpWgwYi_w6oktAlvb5UqeZgS2xSKcgadtr1_dMo")
                .Respond(HttpStatusCode.OK);

            var authMockHttp = new MockHttpMessageHandler();
            authMockHttp.Expect("/oauth/token")
                .WithFormData("grant_type", "password")
                .WithFormData("username", "test@test.com")
                .WithFormData("password", "}8N7aOE8#@")
                .Respond("application/json", OAuthJson.GetToken);

            var configuration = new PasswordBasedConfiguration("myProject", "test@test.com", "}8N7aOE8#@");
            var apiClient = new HttpClient(authMockHttp) {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)};
            var authenticationService = new AuthenticationService(configuration, apiHttpClient: apiClient);

            var handler = new AuthenticatedHttpClientHandler(
                new UserPasswordAccessTokenResolver(configuration.Username, configuration.Password, authenticationService),
                mockHttp
            );

            var client = new HttpClient(handler) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            var service = RestService.For<TestEndpoints>(client);

            await service.Get();

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task Can_Retrieve_Member_oAuth_Token()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect("/")
                .WithHeaders("Authorization", "Bearer zX8UgaHFfOQcHqew4KMbWzo9fQ2fSbdCwMhgwl7hALL2lm2APy9juXh6tJoweort6CVZq5vZ8CQYyGlJqlJ-hz_LyAQChYHhTxIk2N62B9i47vIetAFOYJN5nQbPC8v2qS8fA8zZ1N9UJxbCHiPQhJIQBgg0kZkauHcRdinZP9TJcJZCdFQPCEh1iSCVk9K-NmA6j_hGoqs3EhKuNBjazYb6tiINUgpWJ8gvlPRlqFODL0MNUDqVJc0hMHqbyaacrfcfJy0o3-59SnpK2OMqHqB_DNe5-2oAOXbFG7-j8CfQtpM7SIzb1nmQahyOjpTkVEmkYCOsr2sKGUDz0N7cAiWHvzjJfnog1nJM8pn-dF22N1x9TuoFd_jfuUQcOkgexQllJiZAA2dd_P7o-EqtkU4NKr-X3JNHPxd00PwcZ40Uzt53PmJA8jdnh25GYlugogvCsNGzYEwJo31VB-1pnHmckV_sK8IpFc9LM8zN40WIEFM4TGvRX4XBEX9ntSu0Rke43IBZHVULPEp0nTquI_amZ7zUGiDewRJTkEFCT8FLnuWMQ09q5lO0BbQ49G1Yp88Uy3XJfVd1NPTiHQfe1mm-hiHhVGHINna-UADE4Iis8tuKFNxj6zZkqXj2k8iaQRrKPZ7zfb8yWQr2-O9lCoqk8mL2q7jGXuJr93tQlW8f8B8FIyc9rF5bMAskyQK4SRezVpWgwYi_w6oktAlvb5UqeZgS2xSKcgadtr1_dMo")
                .Respond(HttpStatusCode.OK);

            var authMockHttp = new MockHttpMessageHandler();
            authMockHttp.Expect("/member/oauth/token")
                .WithFormData("grant_type", "password")
                .WithFormData("username", "janedoe")
                .WithFormData("password", "g908&fgou#Pu9{@e")
                .Respond("application/json", OAuthJson.GetToken);

            var cdnClient = new HttpClient(authMockHttp) {BaseAddress = new Uri(Constants.Urls.BaseCdnUrl)};
            var authenticationService = new AuthenticationService("myProject", cdnHttpClient: cdnClient);

            var handler = new AuthenticatedHttpClientHandler(
                new FuncAccessTokenResolver(async (_, __) => (await authenticationService.AuthenticateMember("janedoe", "g908&fgou#Pu9{@e")).AccessToken),
                mockHttp
            );

            var client = new HttpClient(handler) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            var service = RestService.For<TestEndpoints>(client);

            await service.Get();

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }

    interface TestEndpoints
    {
        [Get("/")]
        Task Get();
    }
}
