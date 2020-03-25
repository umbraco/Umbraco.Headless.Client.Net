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

        /// <summary>
        /// Change a members password.
        /// </summary>
        /// <param name="username">Username for the member.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        Task ChangePassword(string username, string currentPassword, string newPassword);

        /// <summary>
        /// Create a reset token that can be used to reset the members password.
        /// </summary>
        /// <param name="username">Username for the member.</param>
        /// <returns>An object containing the token, expiration time and the member.</returns>
        Task<MemberResetPasswordToken> CreateResetPasswordToken(string username);

        /// <summary>
        /// Resets the members password using a reset token obtained via. <see cref="CreateResetPasswordToken"/>.
        /// </summary>
        /// <param name="username">Username for the member.</param>
        /// <param name="token">The reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        Task ResetPassword(string username, string token, string newPassword);
    }
}
