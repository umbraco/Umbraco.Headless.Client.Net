using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class UniqueSellingPointsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, IEnumerable<Content> contents)
        {
            return View(new UniqueSellingPointsViewModel
            {
                Title = title,
                UniqueSellingPoints = from c in contents
                    select new UniqueSellingPoint
                    {
                        Link = c.Value<MultiUrlPickerLink>("link"),
                        Text = c.Value<IHtmlContent>("text"),
                        Title = c.Value<string>("title"),
                        ImageUrl = c.Value<Image>("image")?.Url
                    }
            });
        }
    }
}
