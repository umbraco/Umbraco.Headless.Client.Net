namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class ContentTypePropertyInfo
    {
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string PropertyEditorAlias { get; set; }
        public int SortOrder { get; set; }
        public ContentTypePropertyInfoValidation Validation { get; set; }
    }
}