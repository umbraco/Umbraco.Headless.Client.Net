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
        public IViewComponentResult Invoke(string title, IEnumerable<UniqueSellingPoint> contents)
        {
            return View(new UniqueSellingPointsViewModel
            {
                Title = title,
                UniqueSellingPoints = contents
            });
        }
    }
}
