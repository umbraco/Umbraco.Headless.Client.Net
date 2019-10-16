using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class MediaServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public MediaServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Create_ReturnsCreatedMedia()
        {
            var media = new Media();

            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Post, "/media", MediaServiceJson.Create));

            var result = await service.Create(media);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedMedia()
        {
            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Delete,  "/media/62981fa6-1d34-45c9-9efe-1c811892342b", MediaServiceJson.Delete));

            var result = await service.Delete(new Guid("62981fa6-1d34-45c9-9efe-1c811892342b"));

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-08-12T15:36:23.3756254+02:00").ToUniversalTime(), result.DeleteDate.GetValueOrDefault().ToUniversalTime());
        }

        [Fact]
        public async Task Update_ReturnsUpdatedMedia()
        {
            var media = new Media
            {
                Id = new Guid("62981fa6-1d34-45c9-9efe-1c811892342b")
            };

            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Put, "/media/62981fa6-1d34-45c9-9efe-1c811892342b", MediaServiceJson.Update));

            var result = await service.Update(media);

            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetById_ReturnsMedia()
        {
            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Get, "/media/662af6ca-411a-4c93-a6c7-22c4845698e7", MediaServiceJson.ById));

            var result = await service.GetById(new Guid("662af6ca-411a-4c93-a6c7-22c4845698e7"));

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-06-17T13:46:42.203Z").ToUniversalTime(),
                result.CreateDate.ToUniversalTime());
            Assert.Equal("Umbraco Campari Meeting Room", result.Name);
            Assert.Equal(DateTime.Parse("2019-06-17T13:46:42.203Z").ToUniversalTime(),
                result.UpdateDate.ToUniversalTime());
            Assert.Null(result.DeleteDate);
            Assert.False(result.HasChildren);
            Assert.Equal(new Guid("662af6ca-411a-4c93-a6c7-22c4845698e7"), result.Id);
            Assert.Equal(2, result.Level);
            Assert.Equal("Image", result.MediaTypeAlias);
            Assert.Equal(new Guid("b6f11172-373f-4473-af0f-0b0e5aefd21c"), result.ParentId);
            Assert.Equal(0, result.SortOrder);
            Assert.Collection(result.Properties,
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("umbracoFile", alias);
                    Assert.IsAssignableFrom<JToken>(value);
                },
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("umbracoWidth", alias);
                    Assert.Equal("1600", value);
                },
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("umbracoHeight", alias);
                    Assert.Equal("1067", value);
                },
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("umbracoBytes", alias);
                    Assert.Equal("759116", value);
                },
                pair =>
                {
                    var (alias, value) = pair;
                    Assert.Equal("umbracoExtension", alias);
                    Assert.Equal("jpg", value);
                }
            );
        }

        [Fact]
        public async Task GetRoot_ReturnsMedia()
        {
            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Get, "/media", MediaServiceJson.AtRoot));

            var result = await service.GetRoot();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetChildren_ReturnsMedia()
        {
            var service = new MediaService(_configuration,
                GetMockedHttpClient(HttpMethod.Get, "/media/b6f11172-373f-4473-af0f-0b0e5aefd21c/children?page=1&pageSize=10", MediaServiceJson.Children));

            var result = await service.GetChildren(new Guid("b6f11172-373f-4473-af0f-0b0e5aefd21c"), 1, 10);

            Assert.NotNull(result);
            Assert.Equal(1, result.Page);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(1, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Single(result.Media.Items);
        }

        private HttpClient GetMockedHttpClient(HttpMethod method, string url, string jsonResponse)
        {
            _mockHttp.When(method, url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }
    }

}
