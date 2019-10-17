using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IMediaService
    {
        Task<Media> Create(Media media);
        Task<Media> Delete(Guid id);
        Task<Media> GetById(Guid id);
        Task<PagedMedia> GetChildren(Guid id, int page = 1, int pageSize = 10);
        Task<IEnumerable<Media>> GetRoot();
        Task<Media> Update(Media media);
    }
}
