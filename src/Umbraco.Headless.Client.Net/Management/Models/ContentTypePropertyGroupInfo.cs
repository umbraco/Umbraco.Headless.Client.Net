using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class ContentTypePropertyGroupInfo<TPropertyInfoType> where TPropertyInfoType : ContentTypePropertyInfo
    {
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public IEnumerable<TPropertyInfoType> Properties { get; set; }
    }
}