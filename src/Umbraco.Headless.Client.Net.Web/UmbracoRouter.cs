using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Umbraco.Headless.Client.Net.Delivery;

namespace Umbraco.Headless.Client.Net.Web
{
    internal class UmbracoRouter : IRouter
    {
        private readonly IActionInvokerFactory _actionInvokerFactory;
        private readonly UmbracoControllerTypeCollection _umbracoControllers;

        public UmbracoRouter(IActionInvokerFactory actionInvokerFactory,
            UmbracoControllerTypeCollection umbracoControllers)
        {
            _actionInvokerFactory = actionInvokerFactory ?? throw new ArgumentNullException(nameof(actionInvokerFactory));
            _umbracoControllers = umbracoControllers ?? throw new ArgumentNullException(nameof(umbracoControllers));
        }

        public VirtualPathData? GetVirtualPath(VirtualPathContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var umbracoContext = context.HttpContext.RequestServices.GetRequiredService<IUmbracoContext>();
            var contentDelivery = context.HttpContext.RequestServices.GetRequiredService<IContentDelivery>();

            try
            {
                var content = await contentDelivery.GetByUrl(context.HttpContext.Request.Path)
                    .ConfigureAwait(false);

                if (content != null)
                {
                    umbracoContext.CurrentContent = content;

                    var actionDescriptor =
                        _umbracoControllers.FindActionDescriptor(umbracoContext.CurrentContent.ContentTypeAlias);

                    if (actionDescriptor == null) throw new InvalidOperationException();

                    context.Handler = httpContext =>
                    {
                        var routeValues = new RouteValueDictionary(actionDescriptor.RouteValues);
                        var routeData = httpContext.GetRouteData();
                        routeData.PushState(this, routeValues, null);

                        var actionContext = new ActionContext(context.HttpContext, routeData, actionDescriptor);
                        var invoker = _actionInvokerFactory.CreateInvoker(actionContext);

                        return invoker.InvokeAsync();
                    };
                }
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
            }
        }
    }
}
