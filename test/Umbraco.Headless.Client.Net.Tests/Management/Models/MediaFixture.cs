using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management.Models
{
    public class MediaFixture
    {
        [Fact]
        public void SerializeAsJson()
        {
            var media = new Media
            {
                MediaTypeAlias = "Image",
                Name = "han-solo.png",
                Properties = { { "umbracoFile", new { src = "han-solo.png" }} },
            };

            var json = JsonConvert.SerializeObject(media, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            Assert.Equal(MediaJson.Json, json);
        }

        [Fact]
        public void GetValue_AliasExist_ReturnsValue()
        {
            var media = new Media
            {
                Properties = { { "alt", "Han Solo" } },
            };

            var value = media.GetValue("alt");

            Assert.Equal("Han Solo", value);
        }

        [Fact]
        public void GetValue_AliasDoesNotExist_ReturnsNull()
        {
            var media = new Media();

            var value = media.GetValue("alt");

            Assert.Null(value);
        }

        [Fact]
        public void GetValue_WithDefaultValueAliasDoesNotExist_ReturnsDefaultValue()
        {
            var media = new Media();

            var value = media.GetValue("alt", "Default value");

            Assert.Equal("Default value", value);
        }

        [Fact]
        public void SetValue_SetsValue()
        {
            var media = new Media();

            media.SetValue("alt", "Han Solo");

            Assert.Equal("Han Solo", media.Properties["alt"]);
        }
    }
}
