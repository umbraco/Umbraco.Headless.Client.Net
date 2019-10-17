using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IMemberTypeService
    {
        Task<IEnumerable<MemberType>> GetAll();
        Task<MemberType> GetByAlias(string alias);
    }
}
