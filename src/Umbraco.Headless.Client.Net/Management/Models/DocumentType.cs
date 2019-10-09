namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class DocumentType : ContentType<DocumentTypePropertyInfo>
    {
        public bool AllowCultureVariant { get; set; }
    }
}