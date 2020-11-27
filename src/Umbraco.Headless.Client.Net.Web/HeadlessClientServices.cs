using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Web.Builders;

namespace Umbraco.Headless.Client.Net.Web
{
    public static class HeadlessClientServices
    {
        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services, string projectAlias,
            Action<HeadlessConfiguration> configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (projectAlias == null) throw new ArgumentNullException(nameof(projectAlias));

            var configuration = new HeadlessConfiguration(projectAlias);

            if (configure != null)
                configure(configuration);

            return AddUmbracoHeadless(services, configuration);
        }

        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services, string projectAlias,
            string apiKey, Action<HeadlessConfiguration> configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (projectAlias == null) throw new ArgumentNullException(nameof(projectAlias));
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));

            var configuration = new ApiKeyBasedConfiguration(projectAlias, apiKey);

            if (configure != null)
                configure(configuration);

            return AddUmbracoHeadless(services, configuration);
        }

        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services,
            IHeadlessConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var contentDeliveryService = new ContentDeliveryService(configuration, client => { });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUmbracoContext, UmbracoContext>();
            services.AddSingleton<UmbracoControllerTypeCollection>();

            services.AddSingleton<IContentDeliveryService>(contentDeliveryService);
            services.AddSingleton(contentDeliveryService.Content);
            services.AddSingleton(contentDeliveryService.Media);

            return new UmbracoHeadlessBuilder(services, configuration.ProjectAlias);
        }
    }
}
