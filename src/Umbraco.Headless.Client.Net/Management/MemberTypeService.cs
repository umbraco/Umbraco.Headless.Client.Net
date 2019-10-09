using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public class MemberTypeService : IMemberTypeService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private MemberTypeManagementEndpoints _restService;

        public MemberTypeService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private MemberTypeManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<MemberTypeManagementEndpoints>(_httpClient));

        public async Task<MemberType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias);

        public async Task<IEnumerable<MemberType>> GetRoot()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias);
            return root.MemberTypes.Items;
        }
    }
}
