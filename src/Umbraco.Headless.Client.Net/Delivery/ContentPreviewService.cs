using System;
using System.Net.Http;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// Service class for interacting with the Preview API.
    /// The Preview API retrieves drafts of what is available in the Content Delivery API.
    /// </summary>
    public class ContentPreviewService : IContentDeliveryService
    {
        /// <summary>
        /// Initializes a new instance of the ContentPreviewService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="apiKey">Api Key</param>
        public ContentPreviewService(string projectAlias, string apiKey) : this(new ApiKeyBasedConfiguration(projectAlias, apiKey))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentPreviewService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        public ContentPreviewService(IApiKeyBasedConfiguration configuration)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.Urls.BasePreviewUrl),
                DefaultRequestHeaders = { { Constants.Headers.ApiKey, configuration.Token } }
            };

            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// Gets the Content part of the Preview API
        /// </summary>
        public IContentDelivery Content { get; }

        /// <summary>
        /// Gets the Media part of the Preview API
        /// </summary>
        public IMediaDelivery Media { get; }
    }
}
