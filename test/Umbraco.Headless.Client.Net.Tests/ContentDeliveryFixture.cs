using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class ContentDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _contentBaseUrl = $"{Constants.Urls.BaseCdnUrl}/content";

        public ContentDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Can_Retrieve_Root_Content()
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}", ContentDeliveryJson.GetRoot));
            var contentItems = await service.Content.GetRoot();
            Assert.NotNull(contentItems);
            Assert.NotEmpty(contentItems);
            Assert.Single(contentItems);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task Can_Retrieve_Content_By_Id(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/*", ContentDeliveryJson.GetContentById));
            var content = await service.Content.GetById(contentId);
            Assert.NotNull(content);
            Assert.NotEmpty(content.Properties);
            Assert.Equal(16, content.Properties.Count);
        }

        [Theory]
        [InlineData("/home/products/unicorn")]
        public async Task Can_Retrieve_Content_By_Url(string url)
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/url", ContentDeliveryJson.GetByUrlHomeProductsUnicorn));
            var content = await service.Content.GetByUrl(url);
            Assert.NotNull(content);
            Assert.NotEmpty(content.Properties);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task Can_Retrieve_Children_By_ParentId(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/{id}/children", ContentDeliveryJson.GetChildrenByParentId));
            var children = await service.Content.GetChildren(contentId);
            Assert.NotNull(children);
            Assert.NotNull(children.Content);
            Assert.NotEmpty(children.Content.Items);
            Assert.Equal(5, children.TotalItems);
            Assert.Equal(1, children.TotalPages);
            Assert.Equal(1, children.Page);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1", 1)]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1", 2)]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1", 3)]
        public async Task Can_Retrieve_Descendants_By_Id(string id, int page)
        {
            var contentId = Guid.Parse(id);
            var json = page == 1
                ? ContentDeliveryJson.GetDescendantsPageOne
                : (page == 2 ? ContentDeliveryJson.GetDescendantsPageTwo : ContentDeliveryJson.GetDescendantsPageThree);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/{id}/descendants", json));
            var descendants = await service.Content.GetDescendants(contentId, page: page);
            Assert.NotNull(descendants);
            Assert.NotNull(descendants.Content);
            Assert.NotEmpty(descendants.Content.Items);
            Assert.Equal(page, descendants.Page);
            Assert.Equal(3, descendants.TotalPages);
            Assert.Equal(23, descendants.TotalItems);
        }

        [Theory]
        [InlineData("product")]
        public async Task Can_Retrieve_Content_By_Type(string contentType)
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/type?contentType={contentType}", ContentDeliveryJson.GetByType));
            var ofType = await service.Content.GetByType(contentType);
            Assert.NotNull(ofType);
            Assert.NotNull(ofType.Content);
            Assert.NotEmpty(ofType.Content.Items);
            Assert.Equal(1, ofType.TotalPages);
            Assert.Equal(8, ofType.TotalItems);
        }

        [Theory]
        [InlineData("72346384-fc5e-4a6e-a07d-559eec11dcea")]
        public async Task Can_Retrieve_Ancestors_By_Id(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/{id}/ancestors", ContentDeliveryJson.GetAncestors));
            var ancestors = await service.Content.GetAncestors(contentId);
            Assert.NotNull(ancestors);
            Assert.NotEmpty(ancestors);
            Assert.Equal(2, ancestors.Count());
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }
    }
}
