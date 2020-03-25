using System.Threading.Tasks;

namespace Umbraco.Headless.Client.Net.Security
{
    /// <summary>
    /// Service for authenticating members and Backoffice users.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate a Backoffice user using username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<OAuthResponse> AuthenticateUser(string username, string password);

        /// <summary>
        /// Authenticate a member using username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<OAuthResponse> AuthenticateMember(string username, string password);
    }
}
