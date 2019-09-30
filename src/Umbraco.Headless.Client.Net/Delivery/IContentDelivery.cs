using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    public interface IContentDelivery
    {
        Task<IEnumerable<Content>> GetRoot(string culture = "en-us");
        Task<IEnumerable<T>> GetRoot<T>(string culture = "en-us") where T : IContentBase;
        Task<Content> GetById(Guid id, string culture = "en-us", int depth = 1);
        Task<T> GetById<T>(Guid id, string culture = "en-us", int depth = 1) where T : IContentBase;
        Task<Content> GetByUrl(string url, string culture = "en-us", int depth = 1);
        Task<T> GetByUrl<T>(string url, string culture = "en-us", int depth = 1) where T : IContentBase;
        Task<PagedContent> GetChildren(Guid id, string culture = "en-us", int page = 1, int pageSize = 10);
        Task<PagedContent<T>> GetChildren<T>(Guid id, string culture = "en-us", int page = 1, int pageSize = 10) where T : IContentBase;
        Task<PagedContent> GetDescendants(Guid id, string culture = "en-us", int page = 1, int pageSize = 10);
        Task<PagedContent<T>> GetDescendants<T>(Guid id, string culture = "en-us", int page = 1, int pageSize = 10) where T : IContentBase;
        Task<IEnumerable<Content>> GetAncestors(Guid id, string culture = "en-us");
        Task<IEnumerable<T>> GetAncestors<T>(Guid id, string culture = "en-us") where T : IContentBase;
    }
}
