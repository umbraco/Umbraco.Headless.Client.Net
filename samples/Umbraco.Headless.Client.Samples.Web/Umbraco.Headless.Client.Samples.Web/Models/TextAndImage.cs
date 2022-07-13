using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Serialization;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class TextAndImage : Element
    {
        public string Title { get; set; }

        [JsonConverter(typeof(HtmlContentConverter))]
        public IHtmlContent Text { get; set; }
        public Image Image { get; set; }
        public bool ShowLargeImage { get; set; }
    }
}
