using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class MemberServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public MemberServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Create_ReturnsCreatedMember()
        {
            var member = new Member();

            var httpClient = GetMockedHttpClient(HttpMethod.Post, "/member", MemberServiceJson.Create);
            var service = CreateService(httpClient);

            var result = await service.Create(member);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedMember()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Delete, "/member/test", MemberServiceJson.Delete);
            var service = CreateService(httpClient);

            var result = await service.Delete("test");

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-08-15T08:08:38.4549432Z").ToUniversalTime(), result.DeleteDate.GetValueOrDefault().ToUniversalTime());
        }

        [Fact]
        public async Task Update_ReturnsUpdatedMember()
        {
            var member = new Member
            {
                Id = new Guid("65f0f5ae-e367-4656-b92b-84029e6a5711"),
                Username = "test"
            };

            var httpClient = GetMockedHttpClient(HttpMethod.Put, "/member/test", MemberServiceJson.Update);
            var service = CreateService(httpClient);

            var result = await service.Update(member);

            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetByUsername_ReturnsMember()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Get, "/member/test", MemberServiceJson.ByUsername);
            var service = CreateService(httpClient);

            var result = await service.GetByUsername("test");

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-08-15T10:08:51.463Z").ToUniversalTime(),
                result.CreateDate.ToUniversalTime());
            Assert.Equal("test", result.Name);
            Assert.Equal(DateTime.Parse("2019-08-16T12:21:56.0878793Z").ToUniversalTime(),
                result.UpdateDate.ToUniversalTime());
            Assert.Null(result.DeleteDate);
            Assert.Equal("Member", result.MemberTypeAlias);
            Assert.Equal(new Guid("65f0f5ae-e367-4656-b92b-84029e6a5711"), result.Id);
            Assert.Equal("A comment", result.Comments);
            Assert.Equal("test@example.org", result.Email);
            Assert.True(result.IsApproved);
            Assert.False(result.IsLockedOut);
            Assert.Equal("test", result.Username);
            Assert.Equal(0, result.FailedPasswordAttempts);
            Assert.Null(result.LastLockoutDate);
            Assert.Null(result.LastLoginDate);
            Assert.Null(result.LastPasswordChangeDate);
            Assert.Collection(result.Groups, group => Assert.Equal("Test", group));
            Assert.Collection(result.Properties,
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("fullName", alias);
                    Assert.Equal("John Smith", value);
                }
            );
        }

        [Fact]
        public void AddToGroup_ReturnsOk()
        {
            _mockHttp.Expect(HttpMethod.Put, "/member/test/groups/myGroup");

            var httpClient = new HttpClient(_mockHttp) {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)};
            var service = CreateService(httpClient);

            service.AddToGroup("test", "myGroup");

            _mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void RemoveFromGroup_ReturnsOk()
        {
            _mockHttp.Expect(HttpMethod.Delete, "/member/test/groups/myGroup");

            var httpClient = new HttpClient(_mockHttp) {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)};
            var service = CreateService(httpClient);

            service.RemoveFromGroup("test", "myGroup");

            _mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public void ChangePassword_CallsEndpoint()
        {
            _mockHttp.Expect(HttpMethod.Post, "/member/test/password")
                .WithContent("{\"CurrentPassword\":\"myPassword\",\"NewPassword\":\"myNewPassword\"}");

            var httpClient = new HttpClient(_mockHttp) {BaseAddress = new Uri(Constants.Urls.BaseApiUrl)};
            var service = CreateService(httpClient);

            service.ChangePassword("test", "myPassword", "myNewPassword");

            _mockHttp.VerifyNoOutstandingExpectation();
        }

        private HttpClient GetMockedHttpClient(HttpMethod method, string url, string jsonResponse)
        {
            _mockHttp.When(method, url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }

        private MemberService CreateService(HttpClient client) =>
            new MemberService(_configuration, client, new RefitSettings());
    }

}
