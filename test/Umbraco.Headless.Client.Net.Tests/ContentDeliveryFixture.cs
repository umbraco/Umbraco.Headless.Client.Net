using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Tests.StronglyTypedModels;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class ContentDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly FakeHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
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
            Assert.Equal("/home/", content.Urls["en-us"]);
            Assert.Equal("/hjem/", content.Urls["da"]);
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

        [Fact]
        public async Task Can_Filter()
        {
            var filter = new ContentFilter("product",
                new[] {new ContentFilterProperties("productName", "Jacket", ContentFilterMatch.Contains)});

            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient(HttpMethod.Post, $"{_contentBaseUrl}/filter", ContentDeliveryJson.Search));//result is the same as with search so reusing the json
            var result = await service.Content.Filter(filter);

            Assert.NotNull(result);
            Assert.NotNull(result.Content);
            Assert.NotEmpty(result.Content.Items);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.TotalItems);
        }

        [Fact]
        public async Task Can_Search()
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/search?term=jacket", ContentDeliveryJson.Search));
            var result = await service.Content.Search("jacket");
            Assert.NotNull(result);
            Assert.NotNull(result.Content);
            Assert.NotEmpty(result.Content.Items);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.TotalItems);
        }

        [Fact]
        public async Task Can_Deserialize_To_Strongly_Models()
        {
            _configuration.ContentModelTypes.Add<StarterkitHome>();
            _configuration.ContentModelTypes.Add<Products>();
            _configuration.ContentModelTypes.Add<Blog>();

            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_contentBaseUrl}/ca4249ed-2b23-4337-b522-63cabe5587d1/ancestors", ContentDeliveryJson.GetAncestors));
            var contentItems = await service.Content.GetAncestors(new Guid("ca4249ed-2b23-4337-b522-63cabe5587d1"));

            Assert.Collection(contentItems,
                content => Assert.IsType<Blog>(content),
                content => Assert.IsType<StarterkitHome>(content)
            );
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetById_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetById(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("/my-page/")]
        public async Task GetByUrl_WhenNotFound_ReturnsNull(string url)
        {
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetByUrl(url);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetChildren_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetChildren(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetAncestors_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetAncestors(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task GetDescendants_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Content.GetDescendants(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task Can_Override_Error_Handling(string id)
        {
            var contentId = Guid.Parse(id);
            ApiException exception = null;

            _mockHttp.When(HttpMethod.Get, $"{_contentBaseUrl}/{id}?depth=1")
                .Respond(HttpStatusCode.InternalServerError);

            _configuration.ApiExceptionDelegate = context =>
            {
                exception = context.Exception;
            };

            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var thrown = await Assert.ThrowsAsync<ApiException>(() => service.Content.GetById(contentId));

            Assert.NotNull(exception);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.Same(exception, thrown);
        }

        [Theory]
        [InlineData("ca4249ed-2b23-4337-b522-63cabe5587d1")]
        public async Task Can_Skip_ApiExceptions(string id)
        {
            var contentId = Guid.Parse(id);

            _mockHttp.When(HttpMethod.Get, $"{_contentBaseUrl}/{id}?depth=1")
                .Respond(HttpStatusCode.InternalServerError);

            _configuration.ApiExceptionDelegate = context =>
            {
                context.IsExceptionHandled = true;
            };

            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            await service.Content.GetById(contentId);
        }

        [Fact]
        public async Task Can_Get_AllRedirects()
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{Constants.Urls.BaseCdnUrl}/redirect", ContentDeliveryJson.GetAllRedirect));

            var redirect = await service.Redirect.GetAll();

            Assert.NotNull(redirect);

            Assert.Equal(redirect.Redirects["/root-new/"], new[] { "/root" });
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

        private HttpClient GetMockedHttpClient(HttpMethod method, string url, string jsonResponse)
        {
            _mockHttp.When(method, url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }
    }
}
