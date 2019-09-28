using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class FakeHeadlessConfiguration : IHeadlessConfiguration
    {
        public string ProjectAlias => "headless-with-cdn";
    }
}
