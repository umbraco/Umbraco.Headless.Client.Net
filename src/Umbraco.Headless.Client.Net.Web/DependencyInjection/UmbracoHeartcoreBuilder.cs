using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Web;
using Umbraco.Headless.Client.Net.Web.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public class UmbracoHeartcoreBuilder
    {
        internal UmbracoHeartcoreBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }

        public UmbracoHeartcoreBuilder AddManagement(Action<ManagementOptions>? configure = null, Action<HttpClient>? configureHttpClient = null)
        {
            var builder = Services.AddOptions<ManagementOptions>()
                .BindConfiguration("heartcore:management")
                .ValidateDataAnnotations();

            if (configure != null) builder.Configure(configure);

            builder.Configure<IOptions<HeartcoreOptions>>((opts, config) =>
            {
                if (string.IsNullOrEmpty(opts.ApiKey) && config.Value.ApiKey != null)
                    opts.ApiKey = config.Value.ApiKey;
            });

            Services
                .AddSingleton(provider =>
                {
                    var managementOptions = provider.GetRequiredService<IOptions<ManagementOptions>>();
                    var heartcoreOptions = provider.GetRequiredService<IOptions<HeartcoreOptions>>();

                    var apiKeyBasedConfiguration = CreateConfiguration(heartcoreOptions.Value, managementOptions.Value.ApiKey);
                    return new ContentManagementService(apiKeyBasedConfiguration, configureHttpClient);
                })
                .AddSingleton<IContentManagementService>(provider => provider.GetRequiredService<ContentManagementService>())
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Content)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().DocumentType)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Forms)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Language)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Media)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().MediaType)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Member)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().MemberGroup)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().MemberType)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().Relation)
                .AddSingleton(provider => provider.GetRequiredService<ContentManagementService>().RelationType);

            return this;
        }

        public UmbracoHeartcoreBuilder AddPreview(Action<PreviewOptions>? configure = null, Action<HttpClient>? configureHttpClient = null)
        {
            var builder = Services.AddOptions<PreviewOptions>()
                .BindConfiguration("heartcore:preview")
                .ValidateDataAnnotations();

            if (configure != null) builder.Configure(configure);

            var random = new Random();
            byte[] bytes = new byte[32];
            random.NextBytes(bytes);
            var defaultSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature);

            builder.Configure<IOptions<HeartcoreOptions>>((opts, config) =>
            {
                opts.Enabled = true;

                if (string.IsNullOrEmpty(opts.ApiKey) && config.Value.ApiKey != null)
                    opts.ApiKey = config.Value.ApiKey;

                opts.SigningCredentials ??= defaultSigningCredentials;
            });


            Services
                .AddScoped<PreviewMiddleware>()
                .AddSingleton(provider =>
                {
                    var previewOptions = provider.GetRequiredService<IOptions<PreviewOptions>>();
                    var heartcoreOptions = provider.GetRequiredService<IOptions<HeartcoreOptions>>();

                    var apiKeyBasedConfiguration = CreateConfiguration(heartcoreOptions.Value, previewOptions.Value.ApiKey);
                    return new ContentPreviewService(apiKeyBasedConfiguration, configureHttpClient);
                })
                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IUmbracoContext>().IsInPreviewMode
                        ? (IContentDeliveryService) provider.GetRequiredService<ContentPreviewService>()
                        : provider.GetRequiredService<ContentDeliveryService>()))

                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IUmbracoContext>().IsInPreviewMode
                        ? provider.GetRequiredService<ContentPreviewService>().Content
                        : provider.GetRequiredService<ContentDeliveryService>().Content))

                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IUmbracoContext>().IsInPreviewMode
                        ? provider.GetRequiredService<ContentPreviewService>().Media
                        : provider.GetRequiredService<ContentDeliveryService>().Media));

            return this;
        }


        private static ApiKeyBasedConfiguration CreateConfiguration(HeartcoreOptions options, string apiKey) =>
            new(options.ProjectAlias, apiKey)
            {
                ApiExceptionDelegate = options.ApiExceptionDelegate,
                ElementModelTypes = {options.ElementModelTypes},
                ContentModelTypes = {options.ContentModelTypes},
                MediaModelTypes = {options.MediaModelTypes}
            };
    }
}
