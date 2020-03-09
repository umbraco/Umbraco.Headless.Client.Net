using System;
using System.Net.Http;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// Service class for interacting with the Content Delivery API
    /// </summary>
    public class ContentDeliveryService
    {
        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="projectAlias">Alias of the Project</param>
        public ContentDeliveryService(string projectAlias) : this(new BasicHeadlessConfiguration(projectAlias))
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
        public ContentDeliveryService(string projectAlias, HttpClient httpClient) : this(new BasicHeadlessConfiguration(projectAlias), httpClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IHeadlessConfiguration"/></param>
        public ContentDeliveryService(IHeadlessConfiguration configuration) : this (configuration, new HttpClient { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) })
        { }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IPasswordBasedConfiguration"/></param>
        public ContentDeliveryService(IPasswordBasedConfiguration configuration)
        {
            var authenticationService = new AuthenticationService(configuration);
            var tokenResolver = new UserPasswordAccessTokenResolver(configuration.Username, configuration.ProjectAlias, authenticationService);
            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler(tokenResolver))
            {
                BaseAddress = new Uri(Constants.Urls.BaseCdnUrl)
            };

            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// Initializes a new instance of the ContentDeliveryService class
        /// </summary>
        /// <param name="configuration">Reference to the <see cref="IApiKeyBasedConfiguration"/></param>
        public ContentDeliveryService(IApiKeyBasedConfiguration configuration)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.Urls.BaseCdnUrl),
                DefaultRequestHeaders = { { Constants.Headers.ApiKey, configuration.Token } }
            };

            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
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
            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// Gets the Content part of the Content Delivery API
        /// </summary>
        public IContentDelivery Content { get; }

        /// <summary>
        /// Gets the Media part of the Content Delivery API
        /// </summary>
        public IMediaDelivery Media { get; }
    }
}
