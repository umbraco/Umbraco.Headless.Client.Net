using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class MemberTypeService : IMemberTypeService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MemberTypeManagementEndpoints _restService;

        public MemberTypeService(IHeadlessConfiguration configuration, HttpClient httpClient,
            RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private MemberTypeManagementEndpoints Service =>
            _restService ??= RestService.For<MemberTypeManagementEndpoints>(_httpClient, _refitSettings);

        public async Task<IEnumerable<MemberType>> GetAll()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias);
            return root.MemberTypes.Items;
        }

        public async Task<MemberType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias);
    }
}
