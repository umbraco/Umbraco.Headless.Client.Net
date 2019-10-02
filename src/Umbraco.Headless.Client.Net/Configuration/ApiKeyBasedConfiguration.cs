namespace Umbraco.Headless.Client.Net.Configuration
{
    public class ApiKeyBasedConfiguration : IApiKeyBasedConfiguration
    {
        public ApiKeyBasedConfiguration(string projectAlias, string token)
        {
            ProjectAlias = projectAlias;
            Token = token;
        }
        public string ProjectAlias { get; }
        public string Token { get; }
    }
}
