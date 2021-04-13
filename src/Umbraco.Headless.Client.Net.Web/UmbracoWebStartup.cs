using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Headless.Client.Net.Web.Controllers;

namespace Umbraco.Headless.Client.Net.Web
{
    public static class UmbracoWebStartup
    {
        public static IApplicationBuilder UseUmbracoHeadlessRouter(this IApplicationBuilder app,
            Action<UmbracoRouterOptions>? configure = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var options = new UmbracoRouterOptions();

            if (configure != null)
                configure(options);

            return UseUmbracoHeadlessRouter(app, options);
        }

        public static IApplicationBuilder UseUmbracoHeadlessRouter(this IApplicationBuilder app,
            UmbracoRouterOptions options)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var previewOption = app.ApplicationServices.GetService<IOptions<PreviewOptions>>();
            if (previewOption?.Value.Enabled == true)
            {
                app.UseMiddleware<PreviewMiddleware>();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("preview", "api/preview/{action}",
                        new
                        {
                            controller = "Preview",
                            action = "Index"
                        });
                });
            }

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
