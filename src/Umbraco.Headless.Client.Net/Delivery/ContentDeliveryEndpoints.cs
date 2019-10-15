using System;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    interface ContentDeliveryEndpoints
    {
        [Get("/content?hyperlinks=false")]
        Task<RootPublishedContent<PublishedContent>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture);

        [Get("/content/{id}?depth={depth}")]
        Task<PublishedContent> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int depth);

        [Get("/content/url?url={url}&depth={depth}&hyperlinks=false")]
        Task<PublishedContent> GetByUrl([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string url, int depth);

        [Get("/content/{id}/ancestors?hyperlinks=false")]
        Task<RootPublishedContent<PublishedContent>> GetAncestors([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id);

        [Get("/content/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedContent> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/{id}/descendants?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedContent> GetDescendants([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);
    }

    interface TypedContentRootDeliveryEndpoints<T> where T : IPublishedContent
    {
        [Get("/content?hyperlinks=false")]
        Task<RootPublishedContent<T>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture);

        [Get("/content/{id}/ancestors?hyperlinks=false")]
        Task<RootPublishedContent<T>> GetAncestors([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id);
    }

    interface TypedPagedContentDeliveryEndpoints<T> where T : IPublishedContent
    {
        [Get("/content/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedContent<T>> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/{id}/descendants?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedContent<T>> GetDescendants([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);
    }

    interface TypedContentDeliveryEndpoints<T> where T : IPublishedContent
    {
        [Get("/content/{id}?depth={depth}&hyperlinks=false")]
        Task<T> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int depth);

        [Get("/content/url?url={url}&depth={depth}&hyperlinks=false")]
        Task<T> GetByUrl([Header(Constants.Headers.ProjectAlias)] string projectAlias, [Header(Constants.Headers.AcceptLanguage)] string culture, string url, int depth);
    }

    interface MediaDeliveryEndpoints
    {
        [Get("/media?hyperlinks=false")]
        Task<RootPublishedMedia<PublishedMedia>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}?hyperlinks=false")]
        Task<PublishedMedia> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedMedia> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, int page, int pageSize);
    }

    interface TypedMediaDeliveryEndpoints<T> where T : IPublishedMedia
    {
        [Get("/media?hyperlinks=false")]
        Task<RootPublishedMedia<T>> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/media/{id}?hyperlinks=false")]
        Task<T> GetById([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedPublishedMedia<T>> GetChildren([Header(Constants.Headers.ProjectAlias)] string projectAlias, Guid id, int page, int pageSize);
    }
}
