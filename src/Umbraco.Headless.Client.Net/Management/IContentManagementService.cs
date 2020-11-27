namespace Umbraco.Headless.Client.Net.Management
{
    public interface IContentManagementService
    {
        IContentService Content { get; }
        IDocumentTypeService DocumentType { get; }
        IFormService Forms { get; }
        ILanguageService Language { get; }
        IMediaService Media { get; }
        IMediaTypeService MediaType { get; }
        IMemberService Member { get; }
        IMemberGroupService MemberGroup { get; }
        IMemberTypeService MemberType { get; }
        IRelationService Relation { get; }
        IRelationTypeService RelationType { get; }
    }
}
