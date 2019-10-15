using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Headless.Client.Net.Management.Models;
using Xunit;

namespace Umbraco.Headless.Client.Net.Tests.Management.Models
{
    public class ContentFixture
    {
        [Fact]
        public void SerializeAsJson()
        {
            var content = new Content
            {
                ContentTypeAlias = "product",
                Name = {{"en-US", "Biker Jacket"}, {"da", "Biker Jakke"}},
                Properties =
                {
                    {
                        "productName", new Dictionary<string, object>
                        {
                            {"en-US", "Biker Jacket"},
                            {"da", "Biker Jakke"},
                        }
                    },
                    {
                        "price", new Dictionary<string, object>
                        {
                            {"en-US", "199"},
                            {"da", "1399"}
                        }
                    },
                    {
                        "description", new Dictionary<string, object>
                        {
                            {
                                "en-US",
                                "Donec rutrum congue leo eget malesuada. Vivamus suscipit tortor eget felis porttitor volutpat."
                            },
                            {
                                "da",
                                "Donec rutrum congue leo eget malesuada. Vivamus suscipit tortor eget felis porttitor volutpat."
                            }
                        }
                    },
                    {
                        "sku", new Dictionary<string, object>
                        {
                            {"$invariant", "UMB-BIKER-JACKET"}
                        }
                    },
                    {
                        "photos", new Dictionary<string, object>
                        {
                            {"$invariant", "umb://media/55514845b8bd487cb3709724852fd6bb"}
                        }
                    },
                    {
                        "features", new Dictionary<string, object>
                        {
                            {
                                "en-US", new[]
                                {
                                    new Dictionary<string, object>
                                    {
                                        {"key", new Guid("6a47c043-7699-49c6-a0ed-b335b135ea2b")},
                                        {"name", "Free shipping"},
                                        {"ncContentTypeAlias", "feature"},
                                        {"featureName", "Free shipping"},
                                        {"featureDetails", "Isn't that awesome - you only pay for the product"}
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"key", new Guid("075c935f-4987-4a46-bed9-3286c27f0121")},
                                        {"name", "1 Day return policy"},
                                        {"ncContentTypeAlias", "feature"},
                                        {"featureName", "1 Day return policy"},
                                        {"featureDetails", "You'll need to make up your mind fast"}
                                    },
                                    new Dictionary<string, object>
                                    {
                                        {"key", new Guid("457e0ee3-c066-46fa-b379-a083b38aaa20")},
                                        {"name", "100 Years warranty"},
                                        {"ncContentTypeAlias", "feature"},
                                        {"featureName", "100 Years warranty"},
                                        {"featureDetails", "But if you're satisfied it'll last a lifetime"}
                                    }
                                }
                            }
                        }
                    }
                },
            };

            var json = JsonConvert.SerializeObject(content, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            Assert.Equal(ContentJson.Json, json);
        }

        [Fact]
        public void GetValue_WithoutCultureAndAliasExist_ReturnsValue()
        {
            var content = new Content
            {
                Properties =
                {
                    {
                        "sku", new Dictionary<string, object>
                        {
                            {"$invariant", "UMB-BIKER-JACKET"}
                        }
                    }
                }
            };

            var value = content.GetValue("sku");

            Assert.Equal("UMB-BIKER-JACKET", value);
        }

        [Fact]
        public void GetValue_WithoutCultureAndAliasDoesNotExist_ReturnsNull()
        {
            var content = new Content();

            var value = content.GetValue("sku");

            Assert.Null(value);
        }

        [Fact]
        public void GetValue_WithCultureAndCultureHasValue_ReturnsValue()
        {
            var content = new Content
             {
                 Properties =
                 {
                     {
                         "productName", new Dictionary<string, object>
                         {
                             {"en-US", "Biker Jacket"}
                         }
                     }
                 }
             };

            var value = content.GetValue("productName", "en-US");

            Assert.Equal("Biker Jacket", value);
        }

        [Fact]
        public void GetValue_WithCultureAndCultureHasNoValue_ReturnsNull()
        {
            var content = new Content
            {
                Properties =
                {
                    {
                        "productName", new Dictionary<string, object>
                        {
                            {"en-US", "Biker Jacket"}
                        }
                    }
                }
            };

            var value = content.GetValue("productName", "da");

            Assert.Null(value);
        }

        [Fact]
        public void SetValue_WithoutCulture_SetsInvariantValue()
        {
            var content = new Content();

            content.SetValue("sku", "UMB-BIKER-JACKET");

            Assert.Equal("UMB-BIKER-JACKET", content.Properties["sku"]["$invariant"]);
        }

        [Fact]
        public void SetValue_WithCulture_SetsCultureValue()
        {
            var content = new Content();

            content.SetValue("productName", "Biker Jacket", "en-US");

            Assert.Equal("Biker Jacket", content.Properties["productName"]["en-US"]);
        }
    }
}
