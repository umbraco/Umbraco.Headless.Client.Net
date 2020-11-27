using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IContentDelivery _contentDelivery;

        public FooterViewComponent(IContentDelivery contentDelivery)
        {
            _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var content = await _contentDelivery.GetByUrl<Frontpage>("/");

            return View(new FooterViewModel
            {
                Title = content.FooterTitle,
                Links = content.FooterLinks
            });
        }
    }
}
