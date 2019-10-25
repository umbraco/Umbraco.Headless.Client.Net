using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class LanguageService : ILanguageService
    {
        private readonly IHeadlessConfiguration _headlessConfiguration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private LanguageManagementEndpoints _restService;

        public LanguageService(IHeadlessConfiguration headlessConfiguration, HttpClient httpClient,
            RefitSettings refitSettings)
        {
            _headlessConfiguration = headlessConfiguration ?? throw new ArgumentNullException(nameof(headlessConfiguration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private LanguageManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<LanguageManagementEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<Language>> GetAll()
        {
            var languages = await Service.GetRoot(_headlessConfiguration.ProjectAlias);
            return languages.Languages.Items;
        }

        public async Task<Language> GetByIsoCode(string isoCode)
            => await Service.ByIsoCode(_headlessConfiguration.ProjectAlias, isoCode);

        public async Task<Language> Create(Language language)
            => await Service.Create(_headlessConfiguration.ProjectAlias, language);

        public async Task<Language> Update(string isoCode, Language language)
            => await Service.Update(_headlessConfiguration.ProjectAlias, isoCode, language);

        public async Task<Language> Delete(string isoCode)
            => await Service.Delete(_headlessConfiguration.ProjectAlias, isoCode);
    }
}
