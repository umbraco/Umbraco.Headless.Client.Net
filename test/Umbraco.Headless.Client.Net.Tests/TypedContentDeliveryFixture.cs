using RichardSzalay.MockHttp;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class TypedContentDeliveryFixture
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IHeadlessConfiguration _configuration = new FakeHeadlessConfiguration();
        private readonly string _contentBaseUrl = $"{Constants.Urls.BaseCdnUrl}/content";

        public TypedContentDeliveryFixture()
        {
            _mockHttp = new MockHttpMessageHandler();
        }
    }
}
