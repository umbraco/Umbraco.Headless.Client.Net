using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    public static class UmbracoWebStartup
    {
        public static IServiceCollection AddUmbracoHeadlessWebEngine(this IServiceCollection services,
            IConfiguration headlessConfiguration = null)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<UmbracoCache>();
            services.TryAddScoped<UmbracoContext>();
            services.TryAddSingleton<UmbracoControllerTypeCollection>();

            return services;
        }

        public static IApplicationBuilder UseUmbracoHeadlessWebEngine(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
                var umbracoControllers = app.ApplicationServices.GetRequiredService<UmbracoControllerTypeCollection>();

                routes.Routes.Add(new UmbracoRouter(routes.DefaultHandler, umbracoControllers));

                routes.MapRoute(
                    name: "default",
                    template: $"{{controller={UmbracoRouter.DefaultUmbracoControllerName}}}/{{action={UmbracoRouter.DefaultControllerAction}}}/{{id?}}");
            });

        }
    }
}
