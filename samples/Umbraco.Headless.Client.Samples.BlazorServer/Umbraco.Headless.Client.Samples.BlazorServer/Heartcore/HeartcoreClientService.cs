using Microsoft.Extensions.DependencyInjection;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Management;

namespace Umbraco.Headless.Client.Samples.BlazorServer.Heartcore
{
    public static class HeartcoreClientService
    {

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
    string projectAlias, string apiKey = null)
        {
            services.AddSingleton
                (string.IsNullOrEmpty(apiKey)
                ? new ContentDeliveryService(projectAlias)
                : new ContentDeliveryService(projectAlias, apiKey));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
             IHeadlessConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentManagementService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentManagementService(configuration));
            return services;
        }
    }
}
