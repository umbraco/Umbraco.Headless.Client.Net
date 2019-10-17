namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MemberTypePropertyInfo : ContentTypePropertyInfo
    {
        public bool IsSensitive { get; set; }
        public bool MemberCanEdit { get; set; }
        public bool MemberCanView { get; set; }
    }
}
