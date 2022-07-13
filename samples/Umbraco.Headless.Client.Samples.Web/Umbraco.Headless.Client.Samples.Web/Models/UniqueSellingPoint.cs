using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Serialization;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class UniqueSellingPoint
    {
        public string Title { get; set; }

        [JsonConverter(typeof(HtmlContentConverter))]
        public IHtmlContent Text { get; set; }
        public MultiUrlPickerLink Link { get; set; }
        public Image Image { get; set; }
    }
}
