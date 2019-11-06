using Microsoft.AspNetCore.Html;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class TextAndImage
    {
        public string Title { get; set; }
        public IHtmlContent Text { get; set; }
        public string ImageUrl { get; set; }
        public bool ShowLargeImage { get; set; }
    }
}
