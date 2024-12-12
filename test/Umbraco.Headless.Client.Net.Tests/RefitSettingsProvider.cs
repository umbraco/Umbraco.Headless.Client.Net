using Refit;

namespace Umbraco.Headless.Client.Net.Tests
{
    public static class RefitSettingsProvider
    {
        public static RefitSettings GetSettings()
        {
            return new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer() };
        }
    }
}
