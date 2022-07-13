using System;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Web;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class PreviewViewComponent : ViewComponent
    {
        private readonly IUmbracoContext _umbracoContext;

        public PreviewViewComponent(IUmbracoContext umbracoContext)
        {
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public IViewComponentResult Invoke()
        {
            return _umbracoContext.IsInPreviewMode ? View() : Content("");
        }
    }
}
