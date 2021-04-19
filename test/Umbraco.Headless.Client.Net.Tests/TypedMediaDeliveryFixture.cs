using System;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class TypedMediaDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _mediaBaseUrl = $"{Constants.Urls.BaseCdnUrl}/media";

        public TypedMediaDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Theory]
        [InlineData("3c485159-99a6-44e4-a5f4-576253e0fda5")]
        public async Task Can_Retrieve_Typed_MediaFile_By_Id(string id)
        {
            var mediaId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}/*", MediaDeliveryJson.GetMediaByIdMediaItem));
            var media = await service.Media.GetById<Image>(mediaId);
            Assert.NotNull(media);
            Assert.NotNull(media.File);
            Assert.Null(media.File.Crops);
            Assert.Null(media.File.FocalPoint);
            Assert.Equal("/media/l3ifqpfg/chewbacca.png", media.File.Src);
            Assert.Equal("png", media.Extension);
            Assert.Equal(288031, media.Bytes);
            Assert.Equal(450, media.Width);
            Assert.Equal(600, media.Height);
        }

        [Fact]
        public async Task Can_Retrieve_Root_Media_as_Typed_Folders()
        {
            // NOTE: One remark here, as we currently don't have a ContentType filter for these types of calls
            // this will try to deserialize the entire list to the specified type.
            var service = new ContentDeliveryService(_configuration,
                GetMockedHttpClient($"{_mediaBaseUrl}", MediaDeliveryJson.GetRoot));
            var mediaItems = await service.Media.GetRoot<Folder>();
            Assert.NotNull(mediaItems);
            Assert.NotEmpty(mediaItems);

            foreach (var folder in mediaItems)
            {
                Assert.Equal("Folder", folder.MediaTypeAlias);
            }
        }

        [Theory]
        [InlineData("6d986832-fb11-4d65-b2ae-0d7742f27a19")]
        public async Task GetById_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Media.GetById<Folder>(contentId);

            Assert.Null(content);
        }

        [Theory]
        [InlineData("6d986832-fb11-4d65-b2ae-0d7742f27a19")]
        public async Task GetChildren_WhenNotFound_ReturnsNull(string id)
        {
            var contentId = Guid.Parse(id);
            var service = new ContentDeliveryService(_configuration, GetMockedHttpClient());

            var content = await service.Media.GetChildren<Folder>(contentId);

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
