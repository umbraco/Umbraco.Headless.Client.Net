using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    interface ContentManagementEndpoints
    {
    }

    interface DocumentTypeManagementEndpoints
    {
        [Get("/content/type")]
        Task<RootDocumentTypeCollection> GetRoot([Header(Constants.Headers.ProjectAlias)] string projectAlias);

        [Get("/content/type/{alias}")]
        Task<DocumentType> ByAlias([Header(Constants.Headers.ProjectAlias)] string projectAlias, string alias);
    }
}
