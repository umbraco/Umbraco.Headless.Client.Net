using System;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Web.Builders
{
    public interface IUmbracoHeadlessBuilder
    {
        IUmbracoHeadlessBuilder AddManagementService(string apiKey, Action<HeadlessConfiguration> configure = null);
        IUmbracoHeadlessBuilder AddManagementService(string username, string password, Action<HeadlessConfiguration> configure = null);
        IUmbracoHeadlessBuilder AddManagementService(IApiKeyBasedConfiguration configuration);
        IUmbracoHeadlessBuilder AddManagementService(IPasswordBasedConfiguration configuration);
    }
}
