namespace Umbraco.Headless.Client.Net.Configuration
{
    public class ApiKeyBasedConfiguration : HeadlessConfiguration, IApiKeyBasedConfiguration
    {
        public ApiKeyBasedConfiguration(string projectAlias, string token) : base(projectAlias)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
