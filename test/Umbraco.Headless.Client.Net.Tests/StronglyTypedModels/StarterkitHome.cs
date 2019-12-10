using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Tests.StronglyTypedModels
{
    [ContentModel("home")]
    public class StarterkitHome : Content
    {
        public string HeroHeader { get; set; }
        public string HeroDescription { get; set; }
        public string HeroCTACaption { get; set; }
        public Products HeroCtalink { get; set; }
        public JObject BodyText { get; set; }
        public string FooterHeader { get; set; }
        public string FooterCtaCaption { get; set; }
        public Blog FooterCtalink { get; set; }
        public string FooterAddress { get; set; }
        public Image HeroBackgroundImage { get; set; }
        public string Font { get; set; }
        public string ColorTheme { get; set; }
        public string SiteName { get; set; }
        public Image SiteLogo { get; set; }
    }

    public class Products : TitleContent
    {
        public string DefaultCurrency { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        [JsonProperty("_links")]
        public LinkCollection Links { get; set; }
    }

    public class Product : Content
    {
        public string ProductName { get; set; }
        public string Price { get; set; }
        public List<string> Category { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public Image Photos { get; set; }
        public List<Feature> Features { get; set; }
        public JObject BodyText { get; set; }
    }

    /// <summary>
    /// Nested Content
    /// </summary>
    public class Feature
    {
        public string ContentTypeAlias { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDetails { get; set; }
    }

    public class Blog : TitleContent
    {
        public int HowManyPostsShouldBeShown { get; set; }
        public string DisqusShortname { get; set; }
    }

    public class BlogPost : NavigationBase
    {
        public string PageTitle { get; set; }
        public JObject BodyText { get; set; }
        public List<string> Categories { get; set; }
        public string Except { get; set; }
    }

    public class TitleContent : NavigationBase
    {
        public string PageTitle { get; set; }
        public JObject BodyText { get; set; }
    }

    public class NavigationBase : Content
    {
        public string SeoMetaDescription { get; set; }
        public List<string> Keywords { get; set; }
        public bool UmbracoNaviHide { get; set; }
    }
}
