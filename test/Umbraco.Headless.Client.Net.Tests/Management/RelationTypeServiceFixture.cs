using System;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Shared.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class RelationTypeServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public RelationTypeServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task ByAlias_ReturnsSingleRelationType()
        {
            var service = new RelationTypeService(_configuration,
                GetMockedHttpClient("/relation/type/relateDocumentOnCopy", RelationTypeServiceJson.ByAlias));

            var relationType = await service.GetByAlias("relateDocumentOnCopy");

            Assert.NotNull(relationType);
            Assert.Equal("relateDocumentOnCopy", relationType.Alias);
            Assert.Equal(UmbracoObjectTypes.Unknown, relationType.ChildObjectType);
            Assert.Equal(Guid.Parse("4cbeb612-e689-3563-b755-bf3ede295433"), relationType.Id);
            Assert.True(relationType.IsBidirectional);
            Assert.Equal("Relate Document On Copy", relationType.Name);
            Assert.Equal(UmbracoObjectTypes.Unknown, relationType.ParentObjectType);
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }
    }
}
