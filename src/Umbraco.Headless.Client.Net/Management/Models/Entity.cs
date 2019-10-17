using System;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public abstract class Entity : IEntity
    {
        [JsonProperty("_createDate", NullValueHandling = NullValueHandling.Ignore)]
        private DateTime? _createDate;

        [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
        private Guid? _id;

        [JsonProperty("_updateDate", NullValueHandling = NullValueHandling.Ignore)]
        private DateTime? _updateDate;

        [JsonIgnore]
        public DateTime CreateDate
        {
            get => _createDate.GetValueOrDefault();
            set => _createDate = value;
        }

        [JsonProperty("_deleteDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeleteDate { get; set; }

        [JsonIgnore]
        public Guid Id
        {
            get => _id.GetValueOrDefault();
            set => _id = value;
        }

        [JsonIgnore]
        public DateTime UpdateDate
        {
            get => _updateDate.GetValueOrDefault();
            set => _updateDate = value;
        }
    }
}
