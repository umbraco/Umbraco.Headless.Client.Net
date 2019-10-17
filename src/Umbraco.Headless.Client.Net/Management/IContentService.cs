using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IContentService
    {
        Task<Content> Create(Content content);
        Task<Content> Delete(Guid id);
        Task<Content> GetById(Guid id);
        Task<PagedContent> GetChildren(Guid id, int page = 1, int pageSize = 10);
        Task<IEnumerable<Content>> GetRoot();
        Task<Content> Publish(Guid id, string culture = "*");
        Task<Content> Update(Content content);
        Task<Content> Unpublish(Guid id, string culture = "*");
    }
}
