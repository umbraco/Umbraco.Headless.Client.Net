using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Tests.StronglyTypedModels;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class TypedContentDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _contentBaseUrl = $"{Constants.Urls.BaseCdnUrl}/content";

        public TypedContentDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task Can_Retrieve_Typed_Content_By_Id(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/*", ContentDeliveryJson.GetContentById));
            var content = await service.Content.GetById<StarterkitHome>(contentId);
            Assert.NotNull(content);
            Assert.NotNull(content.HeroCtalink);
            Assert.NotNull(content.HeroCtalink.Links);
            Assert.NotNull(content.HeroBackgroundImage);
            Assert.False(string.IsNullOrEmpty(content.HeroHeader));
        }

        [Theory]
        [InlineData("ec4aafcc-0c25-4f25-a8fe-705bfae1d324")]
        public async Task Can_Retrieve_Content_Children_Typed(string id)
        {
            var parentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/*", ContentDeliveryJson.GetChildrenOfProducts));
            var pagedContent = await service.Content.GetChildren<Product>(parentId);
            Assert.NotNull(pagedContent);
            Assert.NotNull(pagedContent.Content);
            Assert.NotEmpty(pagedContent.Content.Items);
            foreach (var contentItem in pagedContent.Content.Items)
            {
                Assert.NotNull(contentItem);
                Assert.False(string.IsNullOrEmpty(contentItem.ProductName));
            }
        }

        [Theory]
        [InlineData("product")]
        public async Task Can_Retrieve_Content_By_Type_Typed(string contentType)
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/type?contentType={contentType}", ContentDeliveryJson.GetByType));
            var pagedContent = await service.Content.GetByType<Product>();
            Assert.NotNull(pagedContent);
            Assert.NotNull(pagedContent.Content);
            Assert.NotEmpty(pagedContent.Content.Items);
            Assert.Equal(1, pagedContent.TotalPages);
            Assert.Equal(8, pagedContent.TotalItems);
            foreach (var contentItem in pagedContent.Content.Items)
            {
                Assert.NotNull(contentItem);
                Assert.False(string.IsNullOrEmpty(contentItem.ProductName));
            }
        }

        [Theory]
        [InlineData("product")]
        public async Task Can_Retrieve_Content_By_TypeAlias_Typed(string contentTypeAlias)
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/type?contentType={contentTypeAlias}", ContentDeliveryJson.GetByType));
            var pagedContent = await service.Content.GetByTypeAlias<Product>(contentTypeAlias);
            Assert.NotNull(pagedContent);
            Assert.NotNull(pagedContent.Content);
            Assert.NotEmpty(pagedContent.Content.Items);
            Assert.Equal(1, pagedContent.TotalPages);
            Assert.Equal(8, pagedContent.TotalItems);
            foreach (var contentItem in pagedContent.Content.Items)
            {
                Assert.NotNull(contentItem);
                Assert.False(string.IsNullOrEmpty(contentItem.ProductName));
            }
        }

        [Theory]
        [InlineData("description", "Donec", ContentFilterMatch.Contains)]
        public async Task Can_Retrieve_Filtered_Content_As_Typed_Object(string alias, string value, ContentFilterMatch match)
        {
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient($"{_contentBaseUrl}/filter", ContentDeliveryJson.Filter));
            var filters = new List<ContentFilterProperties>();
            filters.Add(new ContentFilterProperties(alias, value, match));
            var contentFilter = new ContentFilter(filters.ToArray());
            var pagedContent = await service.Content.Filter<Product>(contentFilter);

            Assert.NotNull(pagedContent);
            Assert.NotNull(pagedContent.Content);
            Assert.NotEmpty(pagedContent.Content.Items);
            Assert.Equal(1, pagedContent.TotalPages);
            Assert.Equal(1, pagedContent.TotalItems);
            foreach (var contentItem in pagedContent.Content.Items)
            {
                Assert.NotNull(contentItem);
                Assert.False(string.IsNullOrEmpty(contentItem.ProductName));
            }
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetById_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetById<Product>(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("/my-page/")]
        public async Task GetByUrl_WhenNotFound_ReturnsNull(string url)
        {
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetByUrl<Product>(url);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetChildren_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetChildren<Product>(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetAncestors_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetAncestors<Product>(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetDescendants_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetDescendants<Product>(contentId);

            Assert.Null(content);
        }

        private HttpClient GetMockedHttpClient()
        {
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }
    }
}
