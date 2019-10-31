using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Samples.Console
{
    public class ContentDeliveryConfiguration : IHeadlessConfiguration
    {
        public ContentDeliveryConfiguration(string projectAlias)
        {
            ProjectAlias = projectAlias;
        }

        public string ProjectAlias { get; }
    }
}
