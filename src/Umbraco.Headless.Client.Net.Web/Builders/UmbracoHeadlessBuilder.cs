using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Management;

namespace Umbraco.Headless.Client.Net.Web.Builders
{
    internal class UmbracoHeadlessBuilder : IUmbracoHeadlessBuilder
    {
        private readonly IServiceCollection _services;
        private readonly IHeadlessConfiguration _headlessConfiguration;
        private readonly IConfiguration _configuration;

        public UmbracoHeadlessBuilder(IServiceCollection services, IConfiguration configuration, IHeadlessConfiguration headlessConfiguration)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _headlessConfiguration = headlessConfiguration ?? throw new ArgumentNullException(nameof(headlessConfiguration));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IUmbracoHeadlessBuilder AddManagement(string apiKey,
            Action<HeadlessConfiguration>? configure = null)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(apiKey));

            var configuration = new ApiKeyBasedConfiguration(_headlessConfiguration.ProjectAlias, apiKey);

            if (configure != null)
                configure(configuration);

            return AddManagement(configuration);
        }

        public IUmbracoHeadlessBuilder AddManagement(string username, string password, Action<HeadlessConfiguration>? configure = null)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));

            var configuration = new PasswordBasedConfiguration(_headlessConfiguration.ProjectAlias, username, password);

            if (configure != null)
                configure(configuration);

            return AddManagement(configuration);
        }

        public IUmbracoHeadlessBuilder AddManagement(IApiKeyBasedConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            return AddManagementService(new ContentManagementService(configuration));
        }

        public IUmbracoHeadlessBuilder AddManagement(IPasswordBasedConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            return AddManagementService(new ContentManagementService(configuration));
        }

        public IUmbracoHeadlessBuilder AddPreview(string apiKey,  Action<PreviewOptions>? configureOptions = null)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(apiKey));

            var configuration = new ApiKeyBasedConfiguration(_headlessConfiguration.ProjectAlias, apiKey);
            if (configuration is HeadlessConfiguration headlessConfiguration)
            {
                configuration.ApiExceptionDelegate = headlessConfiguration.ApiExceptionDelegate;
            }
            if (_headlessConfiguration is IStronglyTypedHeadlessConfiguration stronglyTypedHeadlessConfiguration)
            {
                foreach (var type in stronglyTypedHeadlessConfiguration.ContentModelTypes)
                    configuration.ContentModelTypes.Add(type);
                foreach (var type in stronglyTypedHeadlessConfiguration.ElementModelTypes)
                    configuration.ElementModelTypes.Add(type);
                foreach (var type in stronglyTypedHeadlessConfiguration.MediaModelTypes)
                    configuration.MediaModelTypes.Add(type);
            }

            return AddPreview(configuration, configureOptions);
        }

        public IUmbracoHeadlessBuilder AddPreview(IApiKeyBasedConfiguration configuration, Action<PreviewOptions>? configureOptions = null)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var previewService = new ContentPreviewService(configuration);

            _services
                .Configure<PreviewOptions>(_configuration.GetSection("heartcore").GetSection("preview"))
                .Configure<PreviewOptions>(options => options.Enabled = true)
                .AddSingleton<PreviewMiddleware>()
                .AddSingleton(previewService)
                .AddSingleton<IPreviewAccessor, PreviewAccessor>()

                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IPreviewAccessor>().IsPreview
                        ? (IContentDeliveryService) previewService
                        : provider.GetRequiredService<ContentDeliveryService>()))

                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IPreviewAccessor>().IsPreview
                        ? previewService.Content
                        : provider.GetRequiredService<ContentDeliveryService>().Content))

                .Replace(ServiceDescriptor.Scoped(provider =>
                    provider.GetRequiredService<IPreviewAccessor>().IsPreview
                        ? previewService.Media
                        : provider.GetRequiredService<ContentDeliveryService>().Media));

            if(configureOptions != null)
                _services.Configure(configureOptions);

            return this;
        }

        private IUmbracoHeadlessBuilder AddManagementService(IContentManagementService managementService)
        {
            _services
                .AddSingleton(managementService)
                .AddSingleton(managementService.Content)
                .AddSingleton(managementService.DocumentType)
                .AddSingleton(managementService.Forms)
                .AddSingleton(managementService.Language)
                .AddSingleton(managementService.Media)
                .AddSingleton(managementService.MediaType)
                .AddSingleton(managementService.Member)
                .AddSingleton(managementService.MemberGroup)
                .AddSingleton(managementService.MemberType)
                .AddSingleton(managementService.Relation)
                .AddSingleton(managementService.RelationType);

            return this;
        }
    }
}
