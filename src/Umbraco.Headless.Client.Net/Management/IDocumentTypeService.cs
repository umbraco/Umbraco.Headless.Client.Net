using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IDocumentTypeService
    {
        Task<IEnumerable<DocumentType>> GetAll();
        Task<DocumentType> GetByAlias(string alias);
    }
}
