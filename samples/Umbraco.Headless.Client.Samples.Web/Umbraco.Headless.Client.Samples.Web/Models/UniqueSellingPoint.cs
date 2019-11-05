using Microsoft.AspNetCore.Html;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class UniqueSellingPoint
    {
        public string Title { get; set; }
        public IHtmlContent Text { get; set; }
        public MultiUrlPickerLink Link { get; set; }
        public string ImageUrl { get; set; }
    }
}
