using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Management;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    public static class HeadlessClientServices
    {
        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            string projectAlias, string apiKey = null)
        {
            services.TryAddSingleton(string.IsNullOrEmpty(apiKey)
                ? new ContentDeliveryService(projectAlias)
                : new ContentDeliveryService(projectAlias, apiKey));
            return services;
        }

       public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IHeadlessConfiguration configuration)
        {
            services.TryAddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.TryAddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.TryAddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.TryAddSingleton(new ContentManagementService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.TryAddSingleton(new ContentManagementService(configuration));
            return services;
        }
    }
}
