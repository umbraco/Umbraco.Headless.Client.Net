using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    [ViewComponent(Name = "textAndImage")]
    public class TextAndImageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Element element)
        {
            return View(new TextAndImage
            {
                ImageUrl = element.Value<Image>("image")?.Url,
                Text = element.Value<IHtmlContent>("text"),
                Title = element.Value<string>("title"),
                ShowLargeImage = element.Value<bool>("showLargeImage")
            });
        }
    }
}
