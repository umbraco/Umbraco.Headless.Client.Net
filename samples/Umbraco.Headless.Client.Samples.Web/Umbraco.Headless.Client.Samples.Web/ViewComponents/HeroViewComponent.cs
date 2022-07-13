using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string subTitle, Image image = null)
        {
            return View(new HeroViewModel
            {
                Title = title,
                SubTitle = subTitle,
                ImageUrl = image?.Url
            });
        }
    }
}
