using System;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Samples.Web.Mvc;

namespace Umbraco.Headless.Client.Samples.Web.Controllers
{
    public abstract class UmbracoController : Controller, IUmbracoController
    {
        public UmbracoController(UmbracoContext umbracoContext)
        {
            UmbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        protected UmbracoContext UmbracoContext { get; }
    }
}
