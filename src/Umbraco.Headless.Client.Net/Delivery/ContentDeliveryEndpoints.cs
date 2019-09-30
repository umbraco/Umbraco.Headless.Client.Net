using System;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Delivery.Models.Hal;

namespace Umbraco.Headless.Client.Net.Delivery
{
    interface ContentDeliveryEndpoints
    {
        [Get("/content?hyperlinks=false")]
        Task<RootContent<Content>> GetRoot([Header(Constants.Headers.AcceptLanguage)] string culture);

        [Get("/content/{id}?depth={depth}")]
        Task<Content> GetById([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int depth);

        [Get("/content/url?url={url}&depth={depth}&hyperlinks=false")]
        Task<Content> GetByUrl([Header(Constants.Headers.AcceptLanguage)] string culture, string url, int depth);

        [Get("/content/{id}/ancestors?hyperlinks=false")]
        Task<RootContent<Content>> GetAncestors([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id);

        [Get("/content/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedContent> GetChildren([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/{id}/descendants?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedContent> GetDescendants([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);
    }

    interface TypedContentRootDeliveryEndpoints<T> where T : IContentBase
    {
        [Get("/content?hyperlinks=false")]
        Task<RootContent<T>> GetRoot([Header(Constants.Headers.AcceptLanguage)] string culture);

        [Get("/content/{id}/ancestors?hyperlinks=false")]
        Task<RootContent<T>> GetAncestors([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id);
    }

    interface TypedPagedContentDeliveryEndpoints<T> where T : IContentBase
    {
        [Get("/content/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedContent<T>> GetChildren([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);

        [Get("/content/{id}/descendants?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedContent<T>> GetDescendants([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int page, int pageSize);
    }

    public interface TypedContentDeliveryEndpoints<T> where T : IContentBase
    {
        [Get("/content/{id}?depth={depth}&hyperlinks=false")]
        Task<T> GetById([Header(Constants.Headers.AcceptLanguage)] string culture, Guid id, int depth);

        [Get("/content/url?url={url}&depth={depth}&hyperlinks=false")]
        Task<T> GetByUrl([Header(Constants.Headers.AcceptLanguage)] string culture, string url, int depth);
    }

    interface MediaDeliveryEndpoints
    {
        [Get("/media?hyperlinks=false")]
        Task<RootMedia<Media>> GetRoot();

        [Get("/media/{id}?hyperlinks=false")]
        Task<Media> GetById(Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedMedia> GetChildren(Guid id, int page, int pageSize);
    }

    interface TypedMediaDeliveryEndpoints<T> where T : IContentBase
    {
        [Get("/media?hyperlinks=false")]
        Task<RootMedia<T>> GetRoot();

        [Get("/media/{id}?hyperlinks=false")]
        Task<T> GetById(Guid id);

        [Get("/media/{id}/children?page={page}&pageSize={pageSize}&hyperlinks=false")]
        Task<PagedMedia<T>> GetChildren(Guid id, int page, int pageSize);
    }
}
