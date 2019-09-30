using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    public interface IMediaDelivery
    {
        Task<IEnumerable<Media>> GetRoot();
        Task<IEnumerable<T>> GetRoot<T>() where T : IContentBase;
        Task<Media> GetById(Guid id);
        Task<T> GetById<T>(Guid id) where T : IContentBase;
        Task<PagedMedia> GetChildren(Guid id, int page = 1, int pageSize = 10);
        Task<PagedMedia<T>> GetChildren<T>(Guid id, int page = 1, int pageSize = 10) where T : IContentBase;
    }
}
