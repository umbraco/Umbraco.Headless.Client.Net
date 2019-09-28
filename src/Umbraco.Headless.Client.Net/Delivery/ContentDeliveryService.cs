using System;
using System.Net.Http;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Delivery
{
    public class ContentDeliveryService
    {
        private readonly IHeadlessConfiguration _configuration;

        public ContentDeliveryService(IHeadlessConfiguration configuration)
        {
            _configuration = configuration;
            var httpClient = new HttpClient{ BaseAddress = new Uri(Constants.Urls.BaseCdnUrl) };
            Content = new ContentDelivery(httpClient);
            Media = new MediaDelivery(httpClient);
        }

        public ContentDeliveryService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            Content = new ContentDelivery(httpClient);
            Media = new MediaDelivery(httpClient);
        }

        public IContentDelivery Content { get; }

        public IMediaDelivery Media { get; }
        
    }
}
