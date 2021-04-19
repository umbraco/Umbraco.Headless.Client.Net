using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
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
        public ContentPreviewService(IApiKeyBasedConfiguration configuration) : this(configuration, _ => { })
        { }

        /// <summary>
        /// Initializes a new instance of the ContentPreviewService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        /// <param name="configureHttpClient">A delegate to configure the <see cref="HttpClient"/>.</param>
        public ContentPreviewService(IApiKeyBasedConfiguration configuration, Action<HttpClient> configureHttpClient = null)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.Urls.BasePreviewUrl),
                DefaultRequestHeaders = { { Constants.Headers.ApiKey, configuration.Token } }
            };
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("UmbracoHeartcoreNetClient", Constants.GetVersion()));

            if (configureHttpClient != null) configureHttpClient(httpClient);

            var modelNameResolver = new ModelNameResolver();
            var refitSettings = CreateRefitSettings(configuration, modelNameResolver);

            Content = new ContentDelivery(configuration, httpClient, refitSettings, modelNameResolver);
            Media = new MediaDelivery(configuration, httpClient, refitSettings);
        }

        /// <summary>
        /// Gets the Content part of the Preview API
        /// </summary>
        public IContentDelivery Content { get; }

        /// <summary>
        /// Gets the Media part of the Preview API
        /// </summary>
        public IMediaDelivery Media { get; }

        private static RefitSettings CreateRefitSettings(IHeadlessConfiguration configuration, ModelNameResolver modelNameResolver)
        {
            return new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = configuration.GetJsonConverters(modelNameResolver)
                })
            };
        }
    }
}
