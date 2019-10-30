using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    public class UmbracoRouter : IRouter
    {
        private readonly UmbracoControllerTypeCollection _umbracoControllers;
        private readonly IRouter _next;

        //TODO: Make this part of IOptions
        public const string DefaultUmbracoControllerName = "DefaultUmbraco";
        public const string DefaultControllerAction = "Index";

        public UmbracoRouter(IRouter defaultRouteHandler, UmbracoControllerTypeCollection umbracoControllers)
        {
            _umbracoControllers = umbracoControllers ?? throw new ArgumentNullException(nameof(umbracoControllers));
            _next = defaultRouteHandler ?? throw new ArgumentNullException(nameof(defaultRouteHandler));
        }

        public async Task RouteAsync(RouteContext context)
        {
            var umbracoContext = context.HttpContext.RequestServices.GetRequiredService<UmbracoContext>();
            if (await umbracoContext.RouteUmbracoContentAsync(context.HttpContext))
            {
                //set controller/action
                var controllerName = _umbracoControllers.GetControllerName(umbracoContext.Content.ContentTypeAlias);

                context.RouteData.Values["controller"] = controllerName;
                context.RouteData.Values["action"] =
                    _umbracoControllers.GetControllerActionName(controllerName,
                        umbracoContext.Content.ContentTypeAlias);
            }

            //continue with default
            await _next.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context) =>
            _next.GetVirtualPath(context);
    }
}
