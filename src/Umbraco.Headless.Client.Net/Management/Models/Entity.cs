using System;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public abstract class Entity : IEntity
    {
        [JsonProperty("_createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("_deleteDate")]
        public DateTime? DeleteDate { get; set; }

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("_updateDate")]
        public DateTime UpdateDate { get; set; }
    }
}