using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class LanguageServiceFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();

        public LanguageServiceFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }

        [Fact]
        public async Task GetRoot_ReturnsAllLanguages()
        {
            var service = new LanguageService(_configuration,
                GetMockedHttpClient("/language", LanguageServiceJson.AtRoot));

            var language = await service.GetRoot();

            Assert.NotNull(language);
            Assert.Equal(2, language.Count());
        }

        [Fact]
        public async Task ByIsoCode_ReturnsSingleLanguage()
        {
            var service = new LanguageService(_configuration,
                GetMockedHttpClient("/language/da", LanguageServiceJson.ByIsoCode));

            var language = await service.GetByIsoCode("da");

            Assert.NotNull(language);
            Assert.Equal("da", language.IsoCode);
            Assert.Equal("Danish", language.CultureName);
            Assert.False(language.IsDefault);
            Assert.False(language.IsMandatory);
            Assert.False(language.DeleteDate.HasValue);
            Assert.Equal(Guid.Parse("7618cae0-b093-4455-82f2-4aba0a09cd47"), language.Id);
        }

        [Fact]
        public async Task Create_ReturnsCreatedLanguage()
        {
            var service = new LanguageService(_configuration,
                GetMockedHttpClient("/language", LanguageServiceJson.Create));

            var language = await service.Create(new Language { IsoCode = "da", FallbackLanguage = "en-GB" });

            Assert.NotNull(language);
            Assert.Equal("da", language.IsoCode);
            Assert.Equal("Danish", language.CultureName);
            Assert.False(language.IsDefault);
            Assert.False(language.IsMandatory);
            Assert.False(language.DeleteDate.HasValue);
            Assert.Equal(Guid.Parse("7618cae0-b093-4455-82f2-4aba0a09cd47"), language.Id);
        }

        [Fact]
        public async Task Update_ReturnsDeletedLanguage()
        {
            var service = new LanguageService(_configuration,
                GetMockedHttpClient("/language/da", LanguageServiceJson.Delete));

            var language = await service.Update("da",
                new Language
                {
                    Id = Guid.Parse("7618cae0-b093-4455-82f2-4aba0a09cd47"),
                    IsoCode = "da",
                    FallbackLanguage = "en-GB"
                }
            );

            Assert.NotNull(language);
            Assert.Equal("da", language.IsoCode);
            Assert.Equal("Danish", language.CultureName);
            Assert.False(language.IsDefault);
            Assert.False(language.IsMandatory);
            Assert.False(language.DeleteDate.HasValue);
            Assert.Equal(Guid.Parse("7618cae0-b093-4455-82f2-4aba0a09cd47"), language.Id);
        }

        [Fact]
        public async Task Delete_ReturnsDeletedLanguage()
        {
            var service = new LanguageService(_configuration,
                GetMockedHttpClient("/language/da", LanguageServiceJson.Delete));

            var language = await service.Delete("da");

            Assert.NotNull(language);
            Assert.Equal("da", language.IsoCode);
            Assert.Equal("Danish", language.CultureName);
            Assert.False(language.IsDefault);
            Assert.False(language.IsMandatory);
            Assert.False(language.DeleteDate.HasValue);
            Assert.Equal(Guid.Parse("7618cae0-b093-4455-82f2-4aba0a09cd47"), language.Id);
        }

        private HttpClient GetMockedHttpClient(string url, string jsonResponse)
        {
            _mockHttp.When(url).Respond("application/json", jsonResponse);
            var client = new HttpClient(_mockHttp) { BaseAddress = new Uri(Constants.Urls.BaseApiUrl) };
            return client;
        }
    }
}
