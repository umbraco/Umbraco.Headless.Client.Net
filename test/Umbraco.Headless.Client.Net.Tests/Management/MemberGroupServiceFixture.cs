using System;
using System.Linq;
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
    public class MemberGroupServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public MemberGroupServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task GetAll_ReturnsAllMemberGroups()
        {
            var service = new MemberGroupService(_configuration,
                GetMockedHttpClient("/member/group", MemberGroupServiceJson.GetAll));

            var documentTypes = await service.GetAll();

            Assert.NotNull(documentTypes);
            Assert.Equal(2, documentTypes.Count());
        }

        [Fact]
        public async Task ByName_ReturnsSingleMemberGroup()
        {
            var httpClient = GetMockedHttpClient("/member/group/My Group", MemberGroupServiceJson.ByName);
            var service = CreateService(httpClient);

            var memberGroup = await service.GetByName("My Group");

            Assert.NotNull(memberGroup);
            Assert.Equal("My Group", memberGroup.Name);
            Assert.False(memberGroup.DeleteDate.HasValue);
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("0d76ca0d-3106-4e03-84d3-f2619b2ebbd0"), memberGroup.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedMemberGroup()
        {
            var httpClient = GetMockedHttpClient("/member/group", MemberGroupServiceJson.Create);
            var service = CreateService(httpClient);

            var memberGroup = await service.Create(new MemberGroup { Name = "My Group"});

            Assert.NotNull(memberGroup);
            Assert.Equal("My Group", memberGroup.Name);
            Assert.False(memberGroup.DeleteDate.HasValue);
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("0d76ca0d-3106-4e03-84d3-f2619b2ebbd0"), memberGroup.Id);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedMemberGroup()
        {
            var httpClient = GetMockedHttpClient("/member/group/My Group", MemberGroupServiceJson.Delete);
            var service = CreateService(httpClient);

            var memberGroup = await service.Delete("My Group");

            Assert.NotNull(memberGroup);
            Assert.Equal("My Group", memberGroup.Name);
            Assert.True(memberGroup.DeleteDate.HasValue);
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-08-15T12:35:12.43256464Z").ToUniversalTime(), memberGroup.DeleteDate.Value.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-08-15T12:32:14.3112288Z").ToUniversalTime(), memberGroup.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("0d76ca0d-3106-4e03-84d3-f2619b2ebbd0"), memberGroup.Id);
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }

        private MemberGroupService CreateService(HttpClient client) =>
            new MemberGroupService(_configuration, client, new RefitSettings());
    }
}
