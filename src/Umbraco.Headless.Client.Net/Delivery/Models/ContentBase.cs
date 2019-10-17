using System;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    /// <summary>
    /// Base class for Published Content and Media
    /// </summary>
    [JsonObject]
    public abstract class ContentBase : IContentBase
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("_url")]
        public string Url { get; set; }

        [JsonProperty("_level")]
        public int Level { get; set; }

        [JsonProperty("_hasChildren")]
        public bool HasChildren { get; set; }

        [JsonProperty("_createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("_updateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("_creatorName")]
        public string CreatorName { get; set; }

        [JsonProperty("_writerName")]
        public string WriterName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public Guid ParentId { get; set; }

        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
    }
}
