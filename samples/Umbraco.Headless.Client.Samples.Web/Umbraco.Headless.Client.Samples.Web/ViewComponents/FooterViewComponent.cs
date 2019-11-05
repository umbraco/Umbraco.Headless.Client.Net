using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Samples.Web.Models;
using Umbraco.Headless.Client.Samples.Web.Mvc;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly UmbracoContext _umbracoContext;

        public FooterViewComponent(UmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var content = (Content) await _umbracoContext.Cache.GetContentByUrl("/");

            return View(new FooterViewModel
            {
                Title = content.Value<string>("footerTitle"),
                Links = content.Value<IEnumerable<MultiUrlPickerLink>>("footerLinks")
            });
        }
    }
}
