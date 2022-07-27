using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    /// <summary>
    /// Interface exposing methods available for the Redirect part of the Content Delivery API
    /// https://cdn.umbraco.io/media
    /// </summary>
    public interface IRedirectDelivery
    {
        /// <summary>
        /// Gets all the redirects defined in the backoffice
        /// </summary>
        /// <param name="culture">Content culture (Optional)</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns><see cref="PagedRedirect"/></returns>
        Task<PagedRedirect> GetAll(string culture = null, int page = 1, int pageSize = 10);
    }
}
