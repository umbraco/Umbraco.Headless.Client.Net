namespace Umbraco.Headless.Client.Net.Configuration
{
    public interface IApiKeyBasedConfiguration : IHeadlessConfiguration
    {
        string Token { get; }
    }
}
