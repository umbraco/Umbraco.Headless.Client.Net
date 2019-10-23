using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class MediaService : IMediaService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MediaManagementEndpoints _restService;

        public MediaService(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private MediaManagementEndpoints Service =>
            _restService ??= RestService.For<MediaManagementEndpoints>(_httpClient, _refitSettings);

        public async Task<Media> Create(Media media)
        {
            if (media.Files.Count > 0)
            {
                var content = new MultipartFormDataContent
                {
                    Headers =
                    {
                        {Constants.Headers.ProjectAlias, _configuration.ProjectAlias}
                    },
                };
                var postData = await _refitSettings.ContentSerializer.SerializeAsync(media);
                content.Add(postData, "content");
                foreach (var file in media.Files)
                    content.Add(file.Value.ToContent(), file.Key, file.Value.FileName);

                var response = await _httpClient.PostAsync("/media", content);
                return await _refitSettings.ContentSerializer.DeserializeAsync<Media>(response.Content);
            }

            return await Service.Create(_configuration.ProjectAlias, media);
        }

        public Task<Media> Delete(Guid id) => Service.Delete(_configuration.ProjectAlias, id);

        public Task<Media> GetById(Guid id) => Service.ById(_configuration.ProjectAlias, id);

        public Task<PagedMedia> GetChildren(Guid id, int page, int pageSize) =>
            Service.Children(_configuration.ProjectAlias, id, page, pageSize);

        public async Task<IEnumerable<Media>> GetRoot()
        {
            var media = await Service.GetRoot(_configuration.ProjectAlias);
            return media.Media.Items;
        }

        public Task<Media> Update(Media media) => Service.Update(_configuration.ProjectAlias, media.Id, media);
    }
}
