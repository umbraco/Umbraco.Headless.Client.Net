using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Samples.Web.Models;
using Umbraco.Headless.Client.Samples.Web.Mvc;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly ContentDeliveryService _contentDeliveryService;
        private readonly UmbracoCache _umbracoCache;

        public MainNavigationViewComponent(ContentDeliveryService contentDeliveryService, UmbracoCache umbracoCache)
        {
            _contentDeliveryService = contentDeliveryService ?? throw new ArgumentNullException(nameof(contentDeliveryService));
            _umbracoCache = umbracoCache ?? throw new ArgumentNullException(nameof(umbracoCache));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rootContent = await _umbracoCache.GetContentByUrl("/");
            var children = await _contentDeliveryService.Content.GetChildren(rootContent.Id);

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
