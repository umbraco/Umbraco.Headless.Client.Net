using System;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{

     interface ContentManagementEndpoints
     {
         [Post("/content")]
         Task<Content> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, Content content);

         [Get("/content/{id}/children?page={page}&pageSize={pageSize}")]
         Task<PagedContent> Children([Header(Constants.Headers.ProjectAlias)]
             string projectAlias, Guid id, int page, int pageSize);

         [Delete("/content/{id}")]
         Task<Content> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

         [Get("/content")]
         Task<RootContentCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

         [Get("/content/{id}")]
         Task<Content> ById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

         [Put("/content/{id}/publish?culture={culture}")]
         Task<Content> Publish([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, string culture);

         [Put("/content/{id}")]
         Task<Content> Update([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, Content content);

         [Put("/content/{id}/unpublish?culture={culture}")]
         Task<Content> Unpublish([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, string culture);
     }

     interface MediaManagementEndpoints
     {
        [Post("/media")]
        Task<Media> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, Media media);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}")]
        Task<PagedMedia> Children([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, int page, int pageSize);

        [Delete("/media/{id}")]
        Task<Media> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media")]
        Task<RootMediaCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}")]
        Task<Media> ById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Put("/media/{id}")]
        Task<Media> Update([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, Media media);
    }

    interface DocumentTypeManagementEndpoints
    {
        [Get("/content/type")]
        Task<RootDocumentTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/content/type/{alias}")]
        Task<DocumentType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    interface MediaTypeManagementEndpoints
    {
        [Get("/media/type")]
        Task<RootMediaTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/type/{alias}")]
        Task<MediaType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    interface MemberTypeManagementEndpoints
    {
        [Get("/member/type")]
        Task<RootMemberTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/member/type/{alias}")]
        Task<MemberType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    interface MemberGroupManagementEndpoints
    {
        [Get("/member/group/{name}")]
        Task<MemberGroup> GetByName([Header(Constants.Headers.ProjectAlias)] string projectAlias, string name);

        [Post("/member/group")]
        Task<MemberGroup> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Body] MemberGroup group);

        [Delete("/member/group/{name}")]
        Task<MemberGroup> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, string name);
    }

    interface LanguageManagementEndpoints
    {
        [Get("/language")]
        Task<RootLanguageCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/language/{isoCode}")]
        Task<Language> ByIsoCode([Header(Constants.Headers.ProjectAlias)] string projectAlias, string isoCode);

        [Post("/language")]
        Task<Language> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Body] Language language);

        [Put("/language/{isoCode}")]
        Task<Language> Update([Header(Constants.Headers.ProjectAlias)] string projectAlias, string isoCode, [Body] Language language);

        [Delete("/language/{isoCode}")]
        Task<Language> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, string isoCode);
    }

    interface RelationTypeManagementEndpoints
    {
        [Get("/relation/type/{alias}")]
        Task<RelationType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    interface RelationManagementEndpoints
    {
        [Get("/relation/{id}")]
        Task<Relation> ById([Header(Constants.Headers.ProjectAlias)] string projectAlias, int id);

        [Get("/relation/{alias}")]
        Task<RootRelationCollection> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);

        [Get("/relation/child/{childId}")]
        Task<RootRelationCollection> ByChildId([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid childId);

        [Get("/relation/parent/{parentId}")]
        Task<RootRelationCollection> ByParentId([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid parentId);

        [Post("/relation")]
        Task<Relation> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, Relation relation);

        [Delete("/relation/{id}")]
        Task<Relation> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, int id);
    }
}
