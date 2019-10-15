using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    /// <summary>
    /// The saved state of a content item
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(UpperSnakeCaseNamingStrategy))]
    public enum ContentSavedState
    {
        /// <summary>
        /// The item isn't created yet
        /// </summary>
        NotCreated = 1,

        /// <summary>
        /// The item is saved but isn't published
        /// </summary>
        Draft = 2,

        /// <summary>
        /// The item is published and there are no pending changes
        /// </summary>
        Published = 3,

        /// <summary>
        /// The item is published and there are pending changes
        /// </summary>
        PublishedPendingChanges = 4
    }
}
