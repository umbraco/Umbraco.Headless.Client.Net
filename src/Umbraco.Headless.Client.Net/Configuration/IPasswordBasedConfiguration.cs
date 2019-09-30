namespace Umbraco.Headless.Client.Net.Configuration
{
    public interface IPasswordBasedConfiguration : IHeadlessConfiguration
    {
        string Username { get; }
        string Password { get; }
    }
}
