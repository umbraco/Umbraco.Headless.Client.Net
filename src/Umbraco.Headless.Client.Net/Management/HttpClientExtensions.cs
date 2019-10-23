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
            var content = await CreateContent<T>(contentSerializer, projectAlias, data, files);

            var response = await client.PostAsync(url, content);
            return await contentSerializer.DeserializeAsync<T>(response.Content);
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
            var postData = await contentSerializer.SerializeAsync(data);
            content.Add(postData, "content");
            foreach (var file in files)
                content.Add(file.Value.ToContent(), file.Key, file.Value.FileName);
            return content;
        }
    }
}
