using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Headless.Client.Net.Web;
using Umbraco.Headless.Client.Net.Web.Options;
using Umbraco.Headless.Client.Net.Web.Routing;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseUmbracoHeartcoreRouting(this IApplicationBuilder app,
            Action<UmbracoRouterOptions>? configure = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var options = new UmbracoRouterOptions();

            if (configure != null)
                configure(options);

            return UseUmbracoHeartcoreRouting(app, options);
        }

        public static IApplicationBuilder UseUmbracoHeartcoreRouting(this IApplicationBuilder app,
            UmbracoRouterOptions options)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var previewOption = app.ApplicationServices.GetService<IOptions<PreviewOptions>>();
            if (previewOption != null && previewOption.Value.Enabled)
            {
                app.UseMiddleware<PreviewMiddleware>();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("enter-preview", "api/preview",
                        new
                        {
                            controller = "Preview",
                            action = "Index"
                        });
                    endpoints.MapControllerRoute("exit-preview", "api/preview/exit",
                        new
                        {
                            controller = "Preview",
                            action = "Exit"
                        });
                });
            }

            return app.UseRouter(routes =>
            {
                var actionInvokerFactory = routes.ServiceProvider.GetRequiredService<IActionInvokerFactory>();

                var umbracoControllers = new UmbracoControllerTypeCollection(
                    routes.ServiceProvider.GetRequiredService<IActionDescriptorCollectionProvider>(),
                    Options.Options.Create(options));

                routes.Routes.Add(new UmbracoRouter(actionInvokerFactory, umbracoControllers));
            });
        }

        public static void UseRobotsTxt(this IApplicationBuilder app)
        {
            _ = app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/robots.txt", StringComparison.OrdinalIgnoreCase))
                {
                    const string output = "User-agent: *  \nDisallow: /";
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(output).ConfigureAwait(false);
                }
                else
                {
                    await next().ConfigureAwait(false);
                }
            });
        }
    }
}
