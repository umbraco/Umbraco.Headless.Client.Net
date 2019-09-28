using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class TypedMediaDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _mediaBaseUrl = $"{Constants.Urls.BaseCdnUrl}/media";

        public TypedMediaDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }
    }
}
