using System;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Web;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web.Controllers
{
    [UmbracoController]
    public class TextPageController : Controller
    {
        private readonly IUmbracoContext _umbracoContext;

        public TextPageController(IUmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public IActionResult Index()
        {
            var content = (Textpage) _umbracoContext.CurrentContent;

            return View(content);
        }
    }
}
