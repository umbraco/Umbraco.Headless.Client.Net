using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    public class MemberGroupService : IMemberGroupService
    {
        private readonly IHeadlessConfiguration _headlessConfiguration;
        private readonly HttpClient _httpClient;
        private MemberGroupManagementEndpoints _restService;

        public MemberGroupService(IHeadlessConfiguration headlessConfiguration, HttpClient httpClient)
        {
            _headlessConfiguration = headlessConfiguration ?? throw new ArgumentNullException(nameof(headlessConfiguration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private MemberGroupManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<MemberGroupManagementEndpoints>(_httpClient));

        public async Task<MemberGroup> GetByName(string name)
            => await Service.GetByName(_headlessConfiguration.ProjectAlias, name);

        public async Task<MemberGroup> Create(MemberGroup memberGroup)
            => await Service.Create(_headlessConfiguration.ProjectAlias, memberGroup);

        public async Task<MemberGroup> Delete(string name)
            => await Service.GetByName(_headlessConfiguration.ProjectAlias, name);
    }
}
