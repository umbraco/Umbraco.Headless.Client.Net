using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management
{
    public class DocumentTypeServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public DocumentTypeServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task AtRoot_ReturnsAllDocumentTypes()
        {
            var service = new DocumentTypeService(_configuration,
                GetMockedHttpClient("/content/type", DocumentTypeServiceJson.GetRoot));

            var documentTypes = await service.GetAll();

            Assert.NotNull(documentTypes);
            Assert.Equal(11, documentTypes.Count());
        }

        [Fact]
        public async Task ByAlias_ReturnsSingleDocumentType()
        {
            var service = new DocumentTypeService(_configuration,
                GetMockedHttpClient("/content/type/products", DocumentTypeServiceJson.ByAlias));

            var documentType = await service.GetByAlias("products");

            Assert.NotNull(documentType);
            Assert.True(documentType.AllowCultureVariant);
            Assert.Equal("products", documentType.Alias);
            Assert.Equal("Products", documentType.Name);
            Assert.Equal(DateTime.Parse("2019-06-17T13:46:22.327Z").ToUniversalTime(), documentType.CreateDate.ToUniversalTime());
            Assert.Equal(DateTime.Parse("2019-06-17T13:46:22.327Z").ToUniversalTime(), documentType.UpdateDate.ToUniversalTime());
            Assert.Equal(Guid.Parse("500624d1-57b9-4a63-b78c-1b991f3e26a1"), documentType.Id);
            Assert.Collection(documentType.Compositions,
                x => Assert.Equal("navigationBase", x),
                x => Assert.Equal("contentBase", x));
            Assert.Collection(documentType.Groups,
                group =>
                {
                    Assert.Equal("Navigation & SEO", group.Name);
                    Assert.Equal(0, group.SortOrder);
                    Assert.Collection(group.Properties, prop =>
                        {
                            Assert.False(prop.AllowCultureVariant);
                            Assert.Equal("seoMetaDescription", prop.Alias);
                            Assert.Equal(
                                "A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130 and 155 characters",
                                prop.Description);
                            Assert.Equal("Description", prop.Label);
                            Assert.Equal("Umbraco.TextArea", prop.PropertyEditorAlias);
                            Assert.Equal(0, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.True(prop.Validation.Required);
                        },
                        prop =>
                        {
                            Assert.False(prop.AllowCultureVariant);
                            Assert.Equal("keywords", prop.Alias);
                            Assert.Equal(
                                "Keywords that describe the content of the page. This is considered optional since most modern search engines don't use this anymore",
                                prop.Description);
                            Assert.Equal("Keywords", prop.Label);
                            Assert.Equal("Umbraco.Tags", prop.PropertyEditorAlias);
                            Assert.Equal(1, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);
                        },
                        prop => {
                            Assert.True(prop.AllowCultureVariant);
                            Assert.Equal("umbracoNavihide", prop.Alias);
                            Assert.Equal("If you don't want this page to appear in the navigation, check this box",
                                prop.Description);
                            Assert.Equal("Hide in Navigation", prop.Label);
                            Assert.Equal("Umbraco.TrueFalse", prop.PropertyEditorAlias);
                            Assert.Equal(2, prop.SortOrder);
                            Assert.Null(prop.Validation.Regex);
                            Assert.False(prop.Validation.Required);}
                    );
                },
                group =>
                {
                    Assert.Equal("Content", group.Name);
                    Assert.Equal(1, group.SortOrder);
                },
                group =>
                {
                    Assert.Equal("Shop", group.Name);
                    Assert.Equal(2, group.SortOrder);
                }
            );
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }
    }
}
