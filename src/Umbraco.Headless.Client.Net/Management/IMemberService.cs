using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IMemberService
    {
        Task<Member> Create(Member member);
        Task<Member> Delete(string username);
        Task<Member> GetByUsername(string username);
        Task<Member> Update(Member member);
        Task AddToGroup(string username, string groupName);
        Task RemoveFromGroup(string username, string groupName);
        Task ChangePassword(string username, string currentPassword, string newPassword);
    }
}
