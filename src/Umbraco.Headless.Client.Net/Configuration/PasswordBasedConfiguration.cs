namespace Umbraco.Headless.Client.Net.Configuration
{
    public class PasswordBasedConfiguration : IPasswordBasedConfiguration
    {
        public PasswordBasedConfiguration(string projectAlias, string username, string password)
        {
            ProjectAlias = projectAlias;
            Username = username;
            Password = password;
        }

        public string ProjectAlias { get; }

        public string Username { get; }

        public string Password { get; }
    }
}
