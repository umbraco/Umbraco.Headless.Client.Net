using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// Service class for interacting with the Content Delivery API
    /// </summary>
    public class ContentDeliveryService : IContentDeliveryService
    {
        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        public ContentDeliveryService(string projectAlias) : this(new HeadlessConfiguration(projectAlias))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="apiKey">Api Key</param>
        public ContentDeliveryService(string projectAlias, string apiKey) : this(new ApiKeyBasedConfiguration(projectAlias, apiKey))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="username">Umbraco Backoffice Username</param>
        /// <param name="password">Umbraco Backoffice User Password</param>
        public ContentDeliveryService(string projectAlias, string username, string password) : this(new PasswordBasedConfiguration(projectAlias, username, password))
        { }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <remarks>
        /// When passing in your own HttpClient you are responsible for setting the authentication headers
        /// </remarks>
        /// <param name="projectAlias">Alias of the Project</param>
        /// <param name="httpClient">Reference to the <see cref="HttpClient"/></param>
        public ContentDeliveryService(string projectAlias, HttpClient httpClient) : this(new HeadlessConfiguration(projectAlias), httpClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IHeadlessConfiguration"/></param>
        public ContentDeliveryService(IHeadlessConfiguration configuration) : this (configuration, _ => {})
        { }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IPasswordBasedConfiguration"/></param>
        public ContentDeliveryService(IPasswordBasedConfiguration configuration) : this(configuration, _ => {})
        {
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        public ContentDeliveryService(IApiKeyBasedConfiguration configuration) : this(configuration, _ => {})
        {
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IHeadlessConfiguration"/></param>
        /// <param name="configureHttpClient">A delegate to configure the <see cref="HttpClient"/>.</param>
        public ContentDeliveryService(IHeadlessConfiguration configuration, Action<HttpClient> configureHttpClient)
        {
            HttpMessageHandler httpMessageHandler = null;
            if (configuration is IPasswordBasedConfiguration passwordBasedConfiguration)
            {
                var authenticationService = new AuthenticationService(configuration);
                var tokenResolver = new UserPasswordAccessTokenResolver(passwordBasedConfiguration.Username,
                    passwordBasedConfiguration.ProjectAlias, authenticationService);
                httpMessageHandler = new AuthenticatedHttpClientHandler(tokenResolver);
            }

            var httpClient = httpMessageHandler == null ? new HttpClient() : new HttpClient(httpMessageHandler, true);

            httpClient.BaseAddress = new Uri(Constants.Urls.BaseCdnUrl);
            httpClient.DefaultRequestHeaders.UserAgent.Add(GetProductInfoHeader());

            if (configuration is IApiKeyBasedConfiguration apiKeyBasedConfiguration)
                httpClient.DefaultRequestHeaders.Add(Constants.Headers.ApiKey, apiKeyBasedConfiguration.Token);

            if (configureHttpClient != null)
                configureHttpClient(httpClient);

            var modelNameResolver = new ModelNameResolver();

            var refitSettings = CreateRefitSettings(configuration, modelNameResolver);
            Content = new ContentDelivery(configuration, httpClient, refitSettings, modelNameResolver);
            Media = new MediaDelivery(configuration, httpClient, refitSettings);
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <remarks>
        /// When passing in your own HttpClient you are responsible for setting the authentication headers
        /// </remarks>
        /// <param name="configuration">Reference to the <see cref="IHeadlessConfiguration"/></param>
        /// <param name="httpClient">Reference to the <see cref="HttpClient"/></param>
        public ContentDeliveryService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            var modelNameResolver = new ModelNameResolver();
            var refitSettings = CreateRefitSettings(configuration, modelNameResolver);

            Content = new ContentDelivery(configuration, httpClient, refitSettings, modelNameResolver);
            Media = new MediaDelivery(configuration, httpClient, refitSettings);
        }

        /// <summary>
        /// Gets the Content part of the Content Delivery API
        /// </summary>
        public IContentDelivery Content { get; }

        /// <summary>
        /// Gets the Media part of the Content Delivery API
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

        private static ProductInfoHeaderValue GetProductInfoHeader() =>
            new ProductInfoHeaderValue("UmbracoHeartcoreNetClient", Constants.GetVersion());
    }
}
