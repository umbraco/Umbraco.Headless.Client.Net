using System;
using System.Net.Http;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Security;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentDeliveryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAlias"></param>
        public ContentDeliveryService(string projectAlias) : this(new BasicHeadlessConfiguration(projectAlias))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAlias"></param>
        /// <param name="token"></param>
        public ContentDeliveryService(string projectAlias, string token) : this(new TokenBasedConfiguration(projectAlias, token))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAlias"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public ContentDeliveryService(string projectAlias, string username, string password) : this(new PasswordBasedConfiguration(projectAlias, username, password))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ContentDeliveryService(IHeadlessConfiguration configuration) : this (configuration, new HttpClient { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ContentDeliveryService(IPasswordBasedConfiguration configuration)
        {
            var httpClient = new HttpClient (new AuthenticatedHttpClientHandler(configuration)) { BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ContentDeliveryService(ITokenBasedConfiguration configuration)
        {
            var httpClient = new HttpClient {BaseAddress = new Uri(Constants.Urls.BaseCdnUrl)};
            httpClient.DefaultRequestHeaders.Add(Constants.Headers.ApiKey, configuration.Token);
            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClient"></param>
        public ContentDeliveryService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            Content = new ContentDelivery(configuration, httpClient);
            Media = new MediaDelivery(configuration, httpClient);
        }

        /// <summary>
        /// 
        /// </summary>
        public IContentDelivery Content { get; }

        /// <summary>
        /// 
        /// </summary>
        public IMediaDelivery Media { get; }
    }
}
