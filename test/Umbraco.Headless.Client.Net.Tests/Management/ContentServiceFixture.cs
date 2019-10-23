using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class ContentServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public ContentServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task Create_ReturnsCreatedContent()
        {
            var content = new Content();

            var httpClient = GetMockedHttpClient(HttpMethod.Post, "/content", ContentServiceJson.Create);
            var service = CreateService(httpClient);

            var result = await service.Create(content);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_WithFiles_SendsMultipartRequest()
        {
            var content = new Content();
            content.SetValue("myFile", "han-solo.png", new StreamPart(Stream.Null, "han-solo.png", "image/png"));

            _mockHttp.Expect(HttpMethod.Post, "/content")
                .With(x =>
                {
                    if (x.Content is MultipartFormDataContent formDataContent)
                    {
                        Assert.Collection(formDataContent,
                            part =>
                            {
                                Assert.IsType<StringContent>(part);
                                Assert.Equal("content", part.Headers.ContentDisposition.Name);
                            },
                            part =>
                            {
                                Assert.IsType<StreamContent>(part);
                                Assert.Equal("myFile.$invariant", part.Headers.ContentDisposition.Name);
                            }
                        );
                        return true;
                    }

                    return false;
                })
                .Respond("application/json", ContentServiceJson.Create);

            var client = new HttpClient(_mockHttp)
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl)
            };
            var service = CreateService(client);

            var result = await service.Create(content);

            Assert.NotNull(result);
            _mockHttp.VerifyNoOutstandingExpectation();
        }


        [Fact]
        public async Task Update_ReturnsUpdatedContent()
        {
            var content = new Content
            {
                Id = new Guid("1a927846-6d11-4188-8966-aa39e5d67db5")
            };

            var httpClient = GetMockedHttpClient(HttpMethod.Put, "/content/1a927846-6d11-4188-8966-aa39e5d67db5", ContentServiceJson.Create);
            var service = CreateService(httpClient);

            var result = await service.Update(content);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_WithFiles_SendsMultipartRequest()
        {
            var content = new Content
            {
                Id = new Guid("1a927846-6d11-4188-8966-aa39e5d67db5")
            };
            content.SetValue("myFile", "han-solo.png", new StreamPart(Stream.Null, "han-solo.png", "image/png"), "en");

            _mockHttp.Expect(HttpMethod.Put, "/content/1a927846-6d11-4188-8966-aa39e5d67db5")
                .With(x =>
                {
                    if (x.Content is MultipartFormDataContent formDataContent)
                    {
                        Assert.Collection(formDataContent,
                            part =>
                            {
                                Assert.IsType<StringContent>(part);
                                Assert.Equal("content", part.Headers.ContentDisposition.Name);
                            },
                            part =>
                            {
                                Assert.IsType<StreamContent>(part);
                                Assert.Equal("myFile.en", part.Headers.ContentDisposition.Name);
                            }
                        );
                        return true;
                    }

                    return false;
                })
                .Respond("application/json", ContentServiceJson.Create);

            var client = new HttpClient(_mockHttp)
            {
                BaseAddress = new Uri(Constants.Urls.BaseApiUrl)
            };
            var service = CreateService(client);

            var result = await service.Update(content);

            Assert.NotNull(result);
            _mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task Delete_ReturnsDeletedContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Delete, "/content/05a38d71-0ae8-48d6-8215-e0cb857a31a8", ContentServiceJson.Delete);
            var service = CreateService(httpClient);

            var result = await service.Delete(new Guid("05a38d71-0ae8-48d6-8215-e0cb857a31a8"));

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-08-22T12:11:34.4136405Z").ToUniversalTime(), result.DeleteDate.GetValueOrDefault().ToUniversalTime());
        }

        [Fact]
        public async Task GetById_ReturnsContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Get, "/content/05a38d71-0ae8-48d6-8215-e0cb857a31a8", ContentServiceJson.ById);
            var service = CreateService(httpClient);

            var result = await service.GetById(new Guid("05a38d71-0ae8-48d6-8215-e0cb857a31a8"));

            Assert.NotNull(result);
            Assert.Equal(DateTime.Parse("2019-06-17T13:46:24.497Z").ToUniversalTime(),
                result.CreateDate.ToUniversalTime());
            Assert.Collection(result.CurrentVersionState,
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("en-US", culture);
                    Assert.Equal(ContentSavedState.Draft, value);
                },
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("da", culture);
                    Assert.Equal(ContentSavedState.Published, value);
                }
            );
            Assert.Collection(result.Name,
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("en-US", culture);
                    Assert.Equal("Biker Jacket", value);
                },
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("da", culture);
                    Assert.Equal("Biker Jakke", value);
                }
            );
            Assert.Collection(result.UpdateDate,
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("en-US", culture);
                    Assert.Equal(DateTime.Parse("2019-06-26T22:51:22.48Z").ToUniversalTime(),
                        value.GetValueOrDefault().ToUniversalTime());
                },
                pair =>
                {
                    var (culture, value) = pair;
                    Assert.Equal("da", culture);
                    Assert.Equal(DateTime.Parse("2019-06-26T22:38:16.617Z").ToUniversalTime(),
                        value.GetValueOrDefault().ToUniversalTime());
                }
            );
            Assert.Null(result.DeleteDate);
            Assert.False(result.HasChildren);
            Assert.Equal(2, result.Level);
            Assert.Equal("product", result.ContentTypeAlias);
            Assert.Equal(new Guid("ec4aafcc-0c25-4f25-a8fe-705bfae1d324"), result.ParentId);
            Assert.Equal(7, result.SortOrder);
            Assert.Collection(result.Properties,
                pair =>
                {
                    var (alias, cultures) = pair;
                    Assert.Equal("productName", alias);
                    Assert.Collection(cultures,
                        cultureValue =>
                        {
                            var (culture, value) = cultureValue;
                            Assert.Equal("en-US", culture);
                            Assert.Equal("Biker Jacket", value);
                        },
                        cultureValue =>
                        {
                            var (culture, value) = cultureValue;
                            Assert.Equal("da", culture);
                            Assert.Equal("Biker Jakke", value);
                        }
                    );
                },
                _ => { },
                _ => { },
                pair =>
                {
                    var (alias, cultures) = pair;
                    Assert.Equal("sku", alias);
                    Assert.Equal("UMB-BIKER-JACKET", cultures["$invariant"]);
                },
                _ => { },
                pair =>
                {
                    var (alias, cultures) = pair;
                    Assert.Equal("features", alias);
                    Assert.Collection(cultures,
                        cultureValue =>
                        {
                            var (culture, value) = cultureValue;
                            Assert.Equal("en-US", culture);
                            Assert.IsAssignableFrom<JArray>(value);
                        },
                        _ => {}
                    );
                }
            );
        }

        [Fact]
        public async Task GetRoot_ReturnsContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Get, "/content", ContentServiceJson.AtRoot);
            var service = CreateService(httpClient);

            var result = await service.GetRoot();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetChildren_ReturnsContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Get, "/content/ec4aafcc-0c25-4f25-a8fe-705bfae1d324/children?page=2&pageSize=5", ContentServiceJson.Children);
            var service = CreateService(httpClient);

            var result = await service.GetChildren(new Guid("ec4aafcc-0c25-4f25-a8fe-705bfae1d324"), 2, 5);

            Assert.NotNull(result);
            Assert.Equal(2, result.Page);
            Assert.Equal(5, result.PageSize);
            Assert.Equal(8, result.TotalItems);
            Assert.Equal(2, result.TotalPages);
            Assert.Equal(3, result.Content.Items.Count());
        }

        [Fact]
        public async Task Publish_ReturnsPublishedContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Put, "/content/262beb70-53a6-49b8-9e98-cfde2e85a78e/publish?culture=da", ContentServiceJson.Publish);
            var service = CreateService(httpClient);

            var result = await service.Publish(new Guid("262beb70-53a6-49b8-9e98-cfde2e85a78e"), "da");

            Assert.NotNull(result);
        }


        [Fact]
        public async Task Unpublish_ReturnsUnpublishedContent()
        {
            var httpClient = GetMockedHttpClient(HttpMethod.Put, "/content/262beb70-53a6-49b8-9e98-cfde2e85a78e/unpublish?culture=en-US", ContentServiceJson.Unpublish);
            var service = CreateService(httpClient);

            var result = await service.Unpublish(new Guid("262beb70-53a6-49b8-9e98-cfde2e85a78e"), "en-US");

            Assert.NotNull(result);
        }

        private HttpClient GetMockedHttpClient(HttpMethod method, string url, string jsonResponse)
        {
            _mockHttp.When(method, url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }

        private ContentService CreateService(HttpClient client) =>
            new ContentService(_configuration, client, new RefitSettings());
    }
}
