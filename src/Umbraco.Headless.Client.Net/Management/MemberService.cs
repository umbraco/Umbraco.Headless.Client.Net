using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class MemberService : IMemberService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MemberManagementEndpoints _restService;

        public MemberService(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private MemberManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<MemberManagementEndpoints>(_httpClient, _refitSettings));

        public Task<Member> Create(Member member) => Service.Create(_configuration.ProjectAlias, member);

        public Task<Member> Delete(string username) => Service.Delete(_configuration.ProjectAlias, username);

        public Task<Member> GetByUsername(string username) => Service.ById(_configuration.ProjectAlias, username);

        public Task<Member> Update(Member member) =>
            Service.Update(_configuration.ProjectAlias, member.Username, member);

        public Task AddToGroup(string username, string groupName) =>
            Service.AddToGroup(_configuration.ProjectAlias, username, groupName);

        public Task RemoveFromGroup(string username, string groupName) =>
            Service.RemoveFromGroup(_configuration.ProjectAlias, username, groupName);

        public Task ChangePassword(string username, string currentPassword, string newPassword) =>
            Service.ChangePassword(_configuration.ProjectAlias, username, new ChangeMemberPassword { CurrentPassword = currentPassword, NewPassword = newPassword });
    }
}
