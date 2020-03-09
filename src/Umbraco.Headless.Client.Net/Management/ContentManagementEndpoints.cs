using System;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    [Headers(Constants.ApiMinimumVersionHeader)]
    interface ContentManagementEndpoints
    {
        [Post("/content")]
        Task<Management.Models.Content> Create([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Management.Models.Content content);

        [Get("/content/{id}/children?page={page}&pageSize={pageSize}")]
        Task<Management.Models.PagedContent> Children([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, int page, int pageSize);

        [Delete("/content/{id}")]
        Task<Management.Models.Content> Delete([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id);

        [Get("/content")]
        Task<Management.Models.RootContentCollection> GetRoot([Header(Constants.Headers.ProjectAlias)]
            string projectAlias);

        [Get("/content/{id}")]
        Task<Management.Models.Content> ById([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id);

        [Put("/content/{id}/publish?culture={culture}")]
        Task<Management.Models.Content> Publish([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, string culture);

        [Put("/content/{id}")]
        Task<Management.Models.Content> Update([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, Management.Models.Content content);

        [Put("/content/{id}/unpublish?culture={culture}")]
        Task<Management.Models.Content> Unpublish([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, string culture);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MediaManagementEndpoints
    {
        [Post("/media")]
        Task<Management.Models.Media> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, Management.Models.Media media);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}")]
        Task<Management.Models.PagedMedia> Children([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, int page, int pageSize);

        [Delete("/media/{id}")]
        Task<Management.Models.Media> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media")]
        Task<Management.Models.RootMediaCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}")]
        Task<Management.Models.Media> ById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Put("/media/{id}")]
        Task<Management.Models.Media> Update([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, Management.Models.Media media);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MemberManagementEndpoints
    {
        [Post("/member")]
        Task<Member> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, Member member);

        [Delete("/member/{username}")]
        Task<Member> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username);

        [Get("/member/{username}")]
        Task<Member> ById([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username);

        [Put("/member/{username}")]
        Task<Member> Update([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username, Member member);

        [Put("/member/{username}/groups/{groupName}")]
        Task AddToGroup([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username, string groupname);

        [Delete("/member/{username}/groups/{groupName}")]
        Task RemoveFromGroup([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username, string groupname);

        [Post("/member/{username}/password")]
        [Headers(Constants.Headers.ApiVersion + "2.2")]
        Task ChangePassword([Header(Constants.Headers.ProjectAlias)] string projectAlias, string username, ChangeMemberPassword model);
    }

    class ChangeMemberPassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface DocumentTypeManagementEndpoints
    {
        [Get("/content/type")]
        Task<RootDocumentTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/content/type/{alias}")]
        Task<DocumentType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MediaTypeManagementEndpoints
    {
        [Get("/media/type")]
        Task<RootMediaTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/type/{alias}")]
        Task<MediaType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MemberTypeManagementEndpoints
    {
        [Get("/member/type")]
        Task<RootMemberTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/member/type/{alias}")]
        Task<MemberType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MemberGroupManagementEndpoints
    {
        [Get("/member/group")]
        Task<RootMemberGroupCollection> GetAll([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/member/group/{name}")]
        Task<MemberGroup> GetByName([Header(Constants.Headers.ProjectAlias)] string projectAlias, string name);

        [Post("/member/group")]
        Task<MemberGroup> Create([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Body] MemberGroup group);

        [Delete("/member/group/{name}")]
        Task<MemberGroup> Delete([Header(Constants.Headers.ProjectAlias)] string projectAlias, string name);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
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

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface RelationTypeManagementEndpoints
    {
        [Get("/relation/type")]
        Task<RootRelationTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/relation/type/{alias}")]
        Task<RelationType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface RelationManagementEndpoints
    {
        [Get("/relation/{id}")]
        Task<Relation> ById([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, int id);

        [Get("/relation/{alias}")]
        Task<RootRelationCollection> ByAlias([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, string alias);

        [Get("/relation/child/{childId}")]
        Task<RootRelationCollection> ByChildId([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid childId);

        [Get("/relation/parent/{parentId}")]
        Task<RootRelationCollection> ByParentId([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid parentId);

        [Post("/relation")]
        Task<Relation> Create([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Relation relation);

        [Delete("/relation/{id}")]
        Task<Relation> Delete([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, int id);
    }

    [Headers(Constants.Headers.ApiVersion + "2.1")]
    interface FormManagementEndpoints
    {
        [Get("/forms")]
        Task<RootFormCollection> GetRoot([Header(Constants.Headers.ProjectAlias)]
            string projectAlias);

        [Get("/forms/{id}")]
        Task<Form> ById([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id);

        [Post("/forms/{id}/entries")]
        Task SubmitEntry([Header(Constants.Headers.ProjectAlias)]
            string projectAlias, Guid id, [Body] object data);
    }
}
