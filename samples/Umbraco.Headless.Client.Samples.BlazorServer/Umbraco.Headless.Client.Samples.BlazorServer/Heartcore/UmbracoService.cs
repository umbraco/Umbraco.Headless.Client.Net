using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.BlazorServer.Heartcore
{
    public class UmbracoService
    {
        private readonly ContentDeliveryService _contentDelivery;


        public UmbracoService(ContentDeliveryService contentDelivery)
        {
            _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
        }


        public async Task<Content> GetRoot()
        {
            var rootContentItems = await _contentDelivery.Content.GetRoot();
            return rootContentItems.FirstOrDefault();
        }

    }
}
