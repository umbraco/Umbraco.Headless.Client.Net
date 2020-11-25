using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Tests
{
    public class FakeHeadlessConfiguration : HeadlessConfiguration
    {
        public FakeHeadlessConfiguration() : base("headless-with-cdn")
        {
        }
    }
}
