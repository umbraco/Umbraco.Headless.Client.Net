using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IRelationTypeService
    {
        Task<IEnumerable<RelationType>> GetAll();
        Task<RelationType> GetByAlias(string alias);
    }
}
