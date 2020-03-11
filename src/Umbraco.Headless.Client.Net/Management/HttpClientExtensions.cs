using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Umbraco.Headless.Client.Net.Management
{
    internal static class HttpClientExtensions
    {
        public static async Task<T> PostMultipartAsync<T>(this HttpClient client, IContentSerializer contentSerializer,
            string url, string projectAlias, object data, IDictionary<string, MultipartItem> files)
        {
            var content = await CreateContent<T>(contentSerializer, projectAlias, data, files).ConfigureAwait(false);

            var response = await client.PostAsync(url, content).ConfigureAwait(false);
            return await contentSerializer.DeserializeAsync<T>(response.Content).ConfigureAwait(false);
        }

        public static async Task<T> PutMultipartAsync<T>(this HttpClient client, IContentSerializer contentSerializer,
            string url, string projectAlias, object data, IDictionary<string, MultipartItem> files)
        {
            var content = await CreateContent<T>(contentSerializer, projectAlias, data, files).ConfigureAwait(false);

            var response = await client.PutAsync(url, content).ConfigureAwait(false);
            return await contentSerializer.DeserializeAsync<T>(response.Content).ConfigureAwait(false);
        }

        private static async Task<MultipartFormDataContent> CreateContent<T>(IContentSerializer contentSerializer, string projectAlias, object data,
            IDictionary<string, MultipartItem> files)
        {
            var content = new MultipartFormDataContent
            {
                Headers =
                {
                    {Constants.Headers.ProjectAlias, projectAlias}
                },
            };
            var postData = await contentSerializer.SerializeAsync(data).ConfigureAwait(false);
            content.Add(postData, "content");
            foreach (var file in files)
                content.Add(file.Value.ToContent(), file.Key, file.Value.FileName);
            return content;
        }
    }
}
