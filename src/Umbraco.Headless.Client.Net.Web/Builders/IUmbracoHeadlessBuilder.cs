using System;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Web.Builders
{
    public interface IUmbracoHeadlessBuilder
    {
        IUmbracoHeadlessBuilder AddManagement(string apiKey, Action<HeadlessConfiguration>? configure = null);
        IUmbracoHeadlessBuilder AddManagement(string username, string password, Action<HeadlessConfiguration>? configure = null);
        IUmbracoHeadlessBuilder AddManagement(IApiKeyBasedConfiguration configuration);
        IUmbracoHeadlessBuilder AddManagement(IPasswordBasedConfiguration configuration);

        IUmbracoHeadlessBuilder AddPreview(string apiKey, Action<PreviewOptions>? configureOptions = null);
        IUmbracoHeadlessBuilder AddPreview(IApiKeyBasedConfiguration configuration, Action<PreviewOptions>? configureOptions = null);
    }
}
