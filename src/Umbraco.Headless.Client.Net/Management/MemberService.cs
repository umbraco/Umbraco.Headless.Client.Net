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
        private MemberManagementEndpoints _restService;

        public MemberService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private MemberManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<MemberManagementEndpoints>(_httpClient));

        public Task<Member> Create(Member member) => Service.Create(_configuration.ProjectAlias, member);

        public Task<Member> Delete(string username) => Service.Delete(_configuration.ProjectAlias, username);

        public Task<Member> GetByUsername(string username) => Service.ById(_configuration.ProjectAlias, username);

        public Task<Member> Update(Member member) =>
            Service.Update(_configuration.ProjectAlias, member.Username, member);

        public Task AddToGroup(string username, string groupname) =>
            Service.AddToGroup(_configuration.ProjectAlias, username, groupname);

        public Task RemoveFromGroup(string username, string groupname) =>
            Service.RemoveFromGroup(_configuration.ProjectAlias, username, groupname);
    }
}
