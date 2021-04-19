using System;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public interface IContent : IContentBase
    {
        string ContentTypeAlias { get; set; }
    }

    public interface IMedia : IContentBase
    {
        string MediaTypeAlias { get; set; }
    }

    public interface IContentBase
    {
        Guid Id { get; set; }
        string Url { get; set; }
        int Level { get; set; }
        bool HasChildren { get; set; }
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
        string CreatorName { get; set; }
        string WriterName { get; set; }
        string Name { get; set; }
        Guid? ParentId { get; set; }
        int SortOrder { get; set; }
    }
}
