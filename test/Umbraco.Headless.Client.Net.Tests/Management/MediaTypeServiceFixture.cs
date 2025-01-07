using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class MediaTypeServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public MediaTypeServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task AtRoot_ReturnsAllMediaTypes()
        {
            var httpClient = GetMockedHttpClient("/media/type", MediaTypeServiceJson.GetRoot);
            var service = CreateService(httpClient);

            var mediaTypes = await service.GetAll();

            Assert.NotNull(mediaTypes);
            Assert.Equal(3, mediaTypes.Count());
        }

        [Fact]
        public async Task ByAlias_ReturnsSingleMediaType()
        {
            var httpClient = GetMockedHttpClient("/media/type/Image", MediaTypeServiceJson.ByAlias);
            var service = CreateService(httpClient);

            var mediaType = await service.GetByAlias("Image");

            Assert.NotNull(mediaType);
            Assert.Equal("Image", mediaType.Alias);
            Assert.Equal("Image", mediaType.Name);
            Assert.Equal(DateTime.Parse("2019-06-17T13:40:01.45Z").ToUniversalTime(), mediaType.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-06-17T13:40:01.45Z").ToUniversalTime(), mediaType.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("cc07b313-0843-4aa8-bbda-871c8da728c8"), mediaType.Id);
            Assert.Empty(mediaType.Compositions);
            Assert.Collection(mediaType.Groups,
                group =>
                {
                    Assert.Equal("Image", group.Name);
                    Assert.Equal(1, group.SortOrder);
                    Assert.Collection(group.Properties, prop =>
                        {
                            Assert.Equal("umbracoFile", prop.Alias);
                            Assert.Equal("Upload image", prop.Label);
                            Assert.Equal("Umbraco.ImageCropper", prop.PropertyEditorAlias);
                            Assert.Equal(0, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.True(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoWidth", prop.Alias);
                            Assert.Equal("in pixels", prop.Description);
                            Assert.Equal("Width", prop.Label);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(1, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoHeight", prop.Alias);
                            Assert.Equal("in pixels", prop.Description);
                            Assert.Equal("Height", prop.Label);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(2, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoBytes", prop.Alias);
                            Assert.Equal("in bytes", prop.Description);
                            Assert.Equal("Size", prop.Label);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(3, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.Equal("umbracoExtension", prop.Alias);
                            Assert.Equal("Type", prop.Label);
                            Assert.Equal("Umbraco.Label", prop.PropertyEditorAlias);
                            Assert.Equal(4, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        }
                    );
                }
            );
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }

        private MediaTypeService CreateService(HttpClient client) =>
            new MediaTypeService(_configuration, client, RefitSettingsProvider.GetSettings());
    }
}
