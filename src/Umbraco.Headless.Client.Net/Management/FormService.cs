using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class FormService : IFormService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private FormManagementEndpoints _restService;

        public FormService(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private FormManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<FormManagementEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<Form>> GetAll()
        {
            var forms = await Service.GetRoot(_configuration.ProjectAlias).ConfigureAwait(false);
            return forms.Forms.Items;
        }

        public Task<Form> GetById(Guid id) => Service.ById(_configuration.ProjectAlias, id);
        public Task SubmitEntry(Guid id, object data) => Service.SubmitEntry(_configuration.ProjectAlias, id, data);
    }
}
