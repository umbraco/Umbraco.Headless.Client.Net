using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public interface IRelationService
    {
        Task<Relation> Create(Relation relation);
        Task<Relation> Delete(int id);
        Task<IEnumerable<Relation>> GetByAlias(string alias);
        Task<IEnumerable<Relation>> GetByChildId(Guid childId);
        Task<Relation> GetById(int id);
        Task<IEnumerable<Relation>> ByParentId(Guid parentId);
    }
}
