using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public abstract class ContentType<TPropertyInfoType> : Entity where TPropertyInfoType : ContentTypePropertyInfo
    {
        public string Alias { get; set; }
        public IEnumerable<string> Compositions { get; set; }
        public string Description { get; set; }
        public IEnumerable<ContentTypePropertyGroupInfo<TPropertyInfoType>> Groups { get; set; }
        public string Name { get; set; }
    }
}
