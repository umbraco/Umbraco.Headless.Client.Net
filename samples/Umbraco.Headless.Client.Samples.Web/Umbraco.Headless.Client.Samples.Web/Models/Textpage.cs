using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class Textpage : Content, IHideInNavigation
    {
        public string HeroTitle { get; set; }
        public string HeroSubtitle { get; set; }
        public Image HeroImage { get; set; }

        public IEnumerable<Element> Elements { get; set; }

        [JsonProperty("umbracoNaviHide")]
        public bool HideInNavigation { get; set; }
    }
}
