using System;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management;

namespace Umbraco.Headless.Client.Net.Web.Builders
{
    internal class UmbracoHeadlessBuilder : IUmbracoHeadlessBuilder
    {
        private readonly IServiceCollection _services;
        private readonly string _projectAlias;

        public UmbracoHeadlessBuilder(IServiceCollection services, string projectAlias)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _projectAlias = projectAlias ?? throw new ArgumentNullException(nameof(projectAlias));
        }

        public IUmbracoHeadlessBuilder AddManagementService(string apiKey,
            Action<HeadlessConfiguration> configure = null)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));

            var configuration = new ApiKeyBasedConfiguration(_projectAlias, apiKey);

            if (configure != null)
                configure(configuration);

            return AddManagementService(configuration);
        }

        public IUmbracoHeadlessBuilder AddManagementService(string username, string password, Action<HeadlessConfiguration> configure = null)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var configuration = new PasswordBasedConfiguration(_projectAlias, username, password);

            if (configure != null)
                configure(configuration);

            return AddManagementService(configuration);
        }

        public IUmbracoHeadlessBuilder AddManagementService(IApiKeyBasedConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            return AddManagementService(new ContentManagementService(configuration));
        }

        public IUmbracoHeadlessBuilder AddManagementService(IPasswordBasedConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            return AddManagementService(new ContentManagementService(configuration));
        }

        private IUmbracoHeadlessBuilder AddManagementService(IContentManagementService managementService)
        {
            _services.AddSingleton(managementService);
            _services.AddSingleton(managementService.Content);
            _services.AddSingleton(managementService.DocumentType);
            _services.AddSingleton(managementService.Forms);
            _services.AddSingleton(managementService.Language);
            _services.AddSingleton(managementService.Media);
            _services.AddSingleton(managementService.MediaType);
            _services.AddSingleton(managementService.Member);
            _services.AddSingleton(managementService.MemberGroup);
            _services.AddSingleton(managementService.MemberType);
            _services.AddSingleton(managementService.Relation);
            _services.AddSingleton(managementService.RelationType);

            return this;
        }
    }
}
