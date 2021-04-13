using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Web.Builders;

namespace Umbraco.Headless.Client.Net.Web
{
    public static class HeadlessClientServices
    {
        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services,
            IConfiguration configuration, string projectAlias, Action<HeadlessConfiguration>? configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (projectAlias == null) throw new ArgumentNullException(nameof(projectAlias));
            if (string.IsNullOrWhiteSpace(projectAlias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectAlias));

            var headlessConfiguration = new HeadlessConfiguration(projectAlias);

            if (configure != null)
                configure(headlessConfiguration);

            return AddUmbracoHeadless(services, configuration, headlessConfiguration);
        }

        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services,
            IConfiguration configuration, string projectAlias, string apiKey, Action<HeadlessConfiguration>? configure = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (projectAlias == null) throw new ArgumentNullException(nameof(projectAlias));
            if (string.IsNullOrWhiteSpace(projectAlias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectAlias));

            var headlessConfiguration = new ApiKeyBasedConfiguration(projectAlias, apiKey);

            if (configure != null)
                configure(headlessConfiguration);

            return AddUmbracoHeadless(services, configuration, headlessConfiguration);
        }

        public static IUmbracoHeadlessBuilder AddUmbracoHeadless(this IServiceCollection services,
            IConfiguration configuration, IHeadlessConfiguration headlessConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (headlessConfiguration == null) throw new ArgumentNullException(nameof(headlessConfiguration));

            var contentDeliveryService = new ContentDeliveryService(headlessConfiguration, client => { });

            services
                .AddHttpContextAccessor()
                .AddScoped<IUmbracoContext, UmbracoContext>()
                .AddSingleton<UmbracoControllerTypeCollection>()
                .AddSingleton<IContentDeliveryService>(contentDeliveryService)
                .AddSingleton(contentDeliveryService)
                .AddSingleton(contentDeliveryService.Content)
                .AddSingleton(contentDeliveryService.Media);

            return new UmbracoHeadlessBuilder(services, configuration, headlessConfiguration);
        }
    }
}
