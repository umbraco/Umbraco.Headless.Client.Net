using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IMemberGroupService
    {
        Task<IEnumerable<MemberGroup>> GetAll();
        Task<MemberGroup> GetByName(string name);
        Task<MemberGroup> Create(MemberGroup memberGroup);
        Task<MemberGroup> Delete(string name);
    }
}
