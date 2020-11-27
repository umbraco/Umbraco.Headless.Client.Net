using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;

namespace Umbraco.Headless.Client.Net.Web
{
    public static class UmbracoWebStartup
    {
        public static IApplicationBuilder UseUmbracoHeadlessRouter(this IApplicationBuilder app,
            Action<UmbracoRouterOptions> configure = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var options = new UmbracoRouterOptions();

            if (configure != null)
                configure(options);

            return UseUmbracoHeadlessRouter(app, options);
        }

        public static IApplicationBuilder UseUmbracoHeadlessRouter(this IApplicationBuilder app, UmbracoRouterOptions options)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) throw new ArgumentNullException(nameof(options));

            return app.UseRouter(routes =>
            {
                var umbracoControllers = routes.ServiceProvider.GetRequiredService<UmbracoControllerTypeCollection>();
                var actionInvokerFactory = routes.ServiceProvider.GetRequiredService<IActionInvokerFactory>();

                umbracoControllers.Initialize(options);

                routes.Routes.Add(new UmbracoRouter(actionInvokerFactory, umbracoControllers));
            });
        }
    }
}
