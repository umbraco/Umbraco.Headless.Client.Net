namespace Umbraco.Headless.Client.Net.Delivery
{
    public interface IContentDeliveryService
    {
        /// <summary>
        /// Gets the Content part of the Content Delivery API
        /// </summary>
        IContentDelivery Content { get; }

        /// <summary>
        /// Gets the Media part of the Content Delivery API
        /// </summary>
        IMediaDelivery Media { get; }
    }
}
