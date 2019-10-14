using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IRelationTypeService
    {
        Task<RelationType> GetByAlias(string alias);
    }
}
