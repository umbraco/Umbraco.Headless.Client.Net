using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Samples.Web.Serialization;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class TextAndImage
    {
        public string Title { get; set; }

        [JsonConverter(typeof(HtmlContentConverter))]
        public IHtmlContent Text { get; set; }
        public string ImageUrl { get; set; }
        public bool ShowLargeImage { get; set; }
    }
}
