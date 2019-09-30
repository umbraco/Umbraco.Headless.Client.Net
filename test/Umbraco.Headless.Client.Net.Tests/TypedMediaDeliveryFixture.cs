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

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            return client;
        }
    }
}
