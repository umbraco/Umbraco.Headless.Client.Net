namespace Umbraco.Headless.Client.Net
{
    public static class Constants
    {
        public const string ApiVersion = "2";

        public static class Urls
        {
            public const string BaseCdnUrl = "https://cdn.umbraco.io";
            public const string BaseApiUrl = "https://api.umbraco.io";
            public const string BaseMediaUrl = "https://media.umbraco.io";
            public const string BasePreviewUrl = "https://preview.umbraco.io";
            public const string BaseGraphqlUrl = "https://graphql.umbraco.io";
        }

        public static class Headers
        {
            public const string AcceptLanguage = "accept-language";
            public const string ApiKey = "api-key";
            public const string ApiVersion = "api-version";
            public const string ProjectName = "umb-project-name";
        }

        public static class Querystrings
        {
            public const string AcceptLanguage = "culture";
            public const string ApiVersion = "api-version";
            public const string ProjectName = "umb-project-name";
        }
    }
}
