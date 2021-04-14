using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Web;
using Umbraco.Headless.Client.Net.Web.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static UmbracoHeartcoreBuilder AddUmbracoHeartcore(this IServiceCollection services,
            Action<HeartcoreOptions>? configure = null, Action<HttpClient>? configureClient = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var builder = services.AddOptions<HeartcoreOptions>()
                .BindConfiguration("heartcore")
                .ValidateDataAnnotations();

            if (configure != null) builder.Configure(configure);

            services
                .AddHttpContextAccessor()
                .AddScoped<IUmbracoContext, UmbracoContext>()
                .AddSingleton<IHeadlessConfiguration>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<HeartcoreOptions>>();

                    var headlessConfiguration = string.IsNullOrEmpty(options.Value.ApiKey)
                        ? new HeadlessConfiguration(options.Value.ProjectAlias)
                        {
                            ApiExceptionDelegate = options.Value.ApiExceptionDelegate,
                            ElementModelTypes = {options.Value.ElementModelTypes},
                            ContentModelTypes = {options.Value.ContentModelTypes},
                            MediaModelTypes = {options.Value.MediaModelTypes}
                        }
                        : new ApiKeyBasedConfiguration(options.Value.ProjectAlias, options.Value.ApiKey)
                        {
                            ApiExceptionDelegate = options.Value.ApiExceptionDelegate,
                            ElementModelTypes = {options.Value.ElementModelTypes},
                            ContentModelTypes = {options.Value.ContentModelTypes},
                            MediaModelTypes = {options.Value.MediaModelTypes}
                        };

                    return headlessConfiguration;
                })
                .AddSingleton(provider =>
                {
                    var headlessConfiguration = provider.GetRequiredService<IHeadlessConfiguration>();
                    return new ContentDeliveryService(headlessConfiguration, configureClient);
                })
                .AddSingleton<IContentDeliveryService>(provider => provider.GetRequiredService<ContentDeliveryService>())
                .AddSingleton(provider => provider.GetRequiredService<ContentDeliveryService>().Content)
                .AddSingleton(provider => provider.GetRequiredService<ContentDeliveryService>().Media);

            return new UmbracoHeartcoreBuilder(services);
        }
    }
}
