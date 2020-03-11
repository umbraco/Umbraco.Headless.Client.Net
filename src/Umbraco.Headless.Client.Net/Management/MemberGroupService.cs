using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class MemberGroupService : IMemberGroupService
    {
        private readonly IHeadlessConfiguration _headlessConfiguration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MemberGroupManagementEndpoints _restService;

        public MemberGroupService(IHeadlessConfiguration headlessConfiguration, HttpClient httpClient,
            RefitSettings refitSettings)
        {
            _headlessConfiguration = headlessConfiguration ?? throw new ArgumentNullException(nameof(headlessConfiguration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private MemberGroupManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<MemberGroupManagementEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<MemberGroup>> GetAll()
        {
            var result = await Service.GetAll(_headlessConfiguration.ProjectAlias).ConfigureAwait(false);
            return result.MemberGroups.Items;
        }

        public async Task<MemberGroup> GetByName(string name)
            => await Service.GetByName(_headlessConfiguration.ProjectAlias, name).ConfigureAwait(false);

        public async Task<MemberGroup> Create(MemberGroup memberGroup)
            => await Service.Create(_headlessConfiguration.ProjectAlias, memberGroup).ConfigureAwait(false);

        public async Task<MemberGroup> Delete(string name)
            => await Service.GetByName(_headlessConfiguration.ProjectAlias, name).ConfigureAwait(false);
    }
}
