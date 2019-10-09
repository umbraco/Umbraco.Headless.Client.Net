using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IMediaTypeService
    {
        Task<MediaType> GetByAlias(string alias);
        Task<IEnumerable<MediaType>> GetRoot();
    }
}
