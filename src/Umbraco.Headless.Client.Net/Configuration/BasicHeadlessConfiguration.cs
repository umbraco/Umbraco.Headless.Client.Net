namespace Umbraco.Headless.Client.Net.Configuration
{
    internal class BasicHeadlessConfiguration : IHeadlessConfiguration
    {
        public BasicHeadlessConfiguration(string projectAlias)
        {
            ProjectAlias = projectAlias;
        }

        public string ProjectAlias { get; }
    }
}
