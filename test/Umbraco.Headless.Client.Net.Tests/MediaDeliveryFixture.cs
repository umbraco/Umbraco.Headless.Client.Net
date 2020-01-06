using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class MediaDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly FakeHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _mediaBaseUrl = $"{Constants.Urls.BaseCdnUrl}/media";

        public MediaDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Can_Retrieve_Root_Media()
        {
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}", MediaDeliveryJson.GetRoot));
            var mediaItems = await service.Media.GetRoot();
            Assert.NotNull(mediaItems);
            Assert.NotEmpty(mediaItems);
            Assert.Equal(4, mediaItems.Count());
        }

        [Theory]
        [InlineData("6d986832-fb11-4d65-b2ae-0d7742f27a19")]
        public async Task Can_Retrieve_Media_Folder_By_Id(string id)
        {
            var mediaId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}/*", MediaDeliveryJson.GetMediaByIdFolder));
            var media = await service.Media.GetById(mediaId);
            Assert.NotNull(media);
            Assert.NotEmpty(media.Properties);
        }

        [Theory]
        [InlineData("3c485159-99a6-44e4-a5f4-576253e0fda5")]
        public async Task Can_Retrieve_Media_Item_By_Id(string id)
        {
            var mediaId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}/*", MediaDeliveryJson.GetMediaByIdMediaItem));
            var media = await service.Media.GetById(mediaId);
            Assert.NotNull(media);
        }

        [Theory]
        [InlineData("6d986832-fb11-4d65-b2ae-0d7742f27a19")]
        public async Task Can_Retrieve_Children_By_ParentId(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}/{id}/children", MediaDeliveryJson.GetChildrenByParentId));
            var children = await service.Media.GetChildren(contentId);
            Assert.NotNull(children);
            Assert.NotNull(children.Media);
            Assert.NotEmpty(children.Media.Items);
            Assert.Equal(1, children.TotalItems);
            Assert.Equal(1, children.TotalPages);
            Assert.Equal(1, children.Page);
        }

        [Fact]
        public async Task Can_Deserialize_To_Strongly_Models()
        {
           var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}/6d986832-fb11-4d65-b2ae-0d7742f27a19/children", MediaDeliveryJson.GetChildrenByParentId));
            var mediaItems = await service.Media.GetChildren(new Guid("6d986832-fb11-4d65-b2ae-0d7742f27a19"));

            Assert.Collection(mediaItems.Media.Items,
                content => Assert.IsType<Image>(content)
            );
        }


        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }
    }
}
