using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class RelationService : IRelationService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private RelationManagementEndpoints _restService;

        public RelationService(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private RelationManagementEndpoints Service =>
            _restService ??= RestService.For<RelationManagementEndpoints>(_httpClient, _refitSettings);

        public async Task<Relation> Create(Relation relation) =>
            await Service.Create(_configuration.ProjectAlias, relation);

        public async Task<Relation> Delete(int id) => await Service.Delete(_configuration.ProjectAlias, id);


        public async Task<IEnumerable<Relation>> GetByAlias(string alias)
        {
            var collection = await Service.ByAlias(_configuration.ProjectAlias, alias);
            return collection.Relations.Items;
        }

        public async Task<IEnumerable<Relation>> GetByChildId(Guid childId)
        {
            var collection = await Service.ByChildId(_configuration.ProjectAlias, childId);
            return collection.Relations.Items;
        }

        public async Task<Relation> GetById(int id) => await Service.ById(_configuration.ProjectAlias, id);

        public async Task<IEnumerable<Relation>> ByParentId(Guid parentId)
        {
            var collection = await Service.ByParentId(_configuration.ProjectAlias, parentId);
            return collection.Relations.Items;
        }
    }
}
