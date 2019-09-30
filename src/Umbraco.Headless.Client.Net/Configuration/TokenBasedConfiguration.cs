namespace Umbraco.Headless.Client.Net.Configuration
{
    public class TokenBasedConfiguration : ITokenBasedConfiguration
    {
        public TokenBasedConfiguration(string projectAlias, string token)
        {
            ProjectAlias = projectAlias;
            Token = token;
        }
        public string ProjectAlias { get; }
        public string Token { get; }
    }
}
