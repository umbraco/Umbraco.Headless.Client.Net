using System.Linq;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Configuration
{
    internal static class HeadlessConfigurationExtensions
    {
        public static JsonConverter[] GetJsonConverters(this IHeadlessConfiguration configuration)
        {
            if (configuration is IStronglyTypedHeadlessConfiguration stronglyTypedHeadlessConfiguration)
            {
                return new JsonConverter[]
                {
                    new ContentConverter(
                        stronglyTypedHeadlessConfiguration.ContentModelTypes.ToDictionary(ContentDelivery.GetAliasFromClassName)
                    ),

                    new MediaConverter(
                        stronglyTypedHeadlessConfiguration.MediaModelTypes.ToDictionary(MediaDelivery.GetAliasFromClassName)
                    )
                };
            }

            return new JsonConverter[0];
        }
    }
}
