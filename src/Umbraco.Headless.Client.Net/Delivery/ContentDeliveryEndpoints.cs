using System;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    [Headers(Constants.ApiMinimumVersionHeader)]
    interface ContentDeliveryEndpoints
    {
        [Get("/content?hyperlinks=false")]
        Task<Delivery.Models.RootContent<Delivery.Models.Content>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture);

        [Get("/content/{id}?depth={depth}")]
        Task<Delivery.Models.Content> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int depth);

        [Get("/content/url?url={url}&depth={depth}&hyperlinks=false")]
        Task<Delivery.Models.Content> GetByUrl([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string url, int depth);

        [Get("/content/{id}/ancestors?hyperlinks=false")]
        Task<Delivery.Models.RootContent<Delivery.Models.Content>> GetAncestors([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id);

        [Get("/content/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/{id}/descendants?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent> GetDescendants([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/type?contentType={contentType}&page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent> GetByType([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string contentType, int page, int pageSize);

        [Headers(Constants.Headers.ApiVersion + ":2.1")]
        [Post("/content/filter?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent> Filter([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, [Body] ContentFilter filter, int page, int pageSize);

        [Get("/content/search?term={term}&page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent> Search([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string term, int page, int pageSize);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface TypedContentRootDeliveryEndpoints<T> where T : Delivery.Models.IContent
    {
        [Get("/content?contentType={contentType}&hyperlinks=false")]
        Task<Delivery.Models.RootContent<T>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string contentType);

        [Get("/content/{id}/ancestors?contentType={contentType}&hyperlinks=false")]
        Task<Delivery.Models.RootContent<T>> GetAncestors([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, string contentType);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface TypedPagedContentDeliveryEndpoints<T> where T : Delivery.Models.IContent
    {
        [Get("/content/{id}/children?contentType={contentType}&page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent<T>> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, string contentType, int page, int pageSize);

        [Get("/content/{id}/descendants?contentType={contentType}&page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent<T>> GetDescendants([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, string contentType, int page, int pageSize);

        [Get("/content/type?contentType={contentType}&page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent<T>> GetByType([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string contentType, int page, int pageSize);

        [Headers(Constants.Headers.ApiVersion + ":2.1")]
        [Post("/content/filter?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedContent<T>> Filter([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, [Body] ContentFilter filter, int page, int pageSize);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface TypedContentDeliveryEndpoints<T> where T : Delivery.Models.IContent
    {
        [Get("/content/{id}?depth={depth}&contentType={contentType}&hyperlinks=false")]
        Task<T> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, string contentType, int depth);

        [Get("/content/url?url={url}&depth={depth}&contentType={contentType}&hyperlinks=false")]
        Task<T> GetByUrl([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string url, string contentType, int depth);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface MediaDeliveryEndpoints
    {
        [Get("/media?hyperlinks=false")]
        Task<Delivery.Models.RootMedia<Delivery.Models.Media>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}?hyperlinks=false")]
        Task<Delivery.Models.Media> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedMedia> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, int page, int pageSize);
    }

    [Headers(Constants.ApiMinimumVersionHeader)]
    interface TypedMediaDeliveryEndpoints<T> where T : Delivery.Models.IMedia
    {
        [Get("/media?hyperlinks=false")]
        Task<Delivery.Models.RootMedia<T>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}?hyperlinks=false")]
        Task<T> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<Delivery.Models.PagedMedia<T>> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, int page, int pageSize);
    }
}
