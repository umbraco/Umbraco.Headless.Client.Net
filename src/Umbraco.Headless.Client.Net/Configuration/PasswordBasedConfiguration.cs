namespace Umbraco.Headless.Client.Net.Configuration
{
    public class PasswordBasedConfiguration : HeadlessConfiguration, IPasswordBasedConfiguration
    {
        public PasswordBasedConfiguration(string projectAlias, string username, string password) : base(projectAlias)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }
}
