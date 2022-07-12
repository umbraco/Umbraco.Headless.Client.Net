using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Samples.MVC.Models;

namespace Umbraco.Headless.Client.Samples.MVC.ViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly IContentDelivery _contentDelivery;

        public MainNavigationViewComponent(IContentDelivery contentDelivery)
        {
            _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rootContent = await _contentDelivery.GetByUrl("/");
            var children = await _contentDelivery.GetChildren(rootContent.Id);

            return View(from item in children.Content.Items
                select new NavigationItem
                {
                    Title = item.Name,
                    Url = item.Url,
                    IsCurrent = item.Url == Request.Path.ToString()
                });
        }
    }
}
