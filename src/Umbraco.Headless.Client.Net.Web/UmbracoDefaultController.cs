using System;
using Microsoft.AspNetCore.Mvc;

namespace Umbraco.Headless.Client.Net.Web
{
    /// <summary>
    /// The default Umbraco Headless controller.
    /// Used when no custom controller can be found for a request.
    /// </summary>
    [UmbracoController]
    public sealed class UmbracoDefaultController : Controller
    {
        private readonly IUmbracoContext _umbracoContext;

        public UmbracoDefaultController(IUmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public IActionResult Index()
        {
            var content = _umbracoContext.CurrentContent;

            return View($"../{content!.ContentTypeAlias}/Index", content);
        }
    }
}
