using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Headless.Client.Samples.Web.Mvc;

namespace Umbraco.Headless.Client.Samples.Web.Controllers
{
    public sealed class DefaultUmbracoController : UmbracoController
    {
        private readonly IViewEngine _viewEngine;

        public DefaultUmbracoController(UmbracoContext umbracoContext, ICompositeViewEngine viewEngine) : base(umbracoContext)
        {
            _viewEngine = viewEngine ?? throw new ArgumentNullException(nameof(viewEngine));
        }

        public IActionResult Index()
        {
            var viewName = UmbracoContext.Content.ContentTypeAlias;
            var result = _viewEngine.GetView("", viewName, true);
            if(!result.Success)
                result = _viewEngine.FindView(ControllerContext, viewName, true);
            if (!result.Success)
                viewName = "Index";

            return View(viewName, UmbracoContext.Content);
        }
    }
}
