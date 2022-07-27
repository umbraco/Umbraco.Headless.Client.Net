using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class RedirectDelivery : DeliveryBase, IRedirectDelivery
    {
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private RedirectDeliveryEndpoints _service;

        public RedirectDelivery(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings) : base(configuration)
        {
            _httpClient = httpClient;
            _refitSettings = refitSettings;
        }

        private RedirectDeliveryEndpoints Service =>
            _service ?? (_service = RestService.For<RedirectDeliveryEndpoints>(_httpClient, _refitSettings));

        public async Task<PagedRedirect> GetAll(string culture = null, int page = 1, int pageSize = 10)
        {
            var result = await Get(x => x.GetAll(Configuration.ProjectAlias, culture, page, pageSize)).ConfigureAwait(false);
            return result;
        }
        private async Task<T> Get<T>(Func<RedirectDeliveryEndpoints, Task<ApiResponse<T>>> getResponse)
        {
            using (var response = await getResponse(Service).ConfigureAwait(false))
                return GetResponse(response);
        }
    }
}
