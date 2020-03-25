using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Umbraco.Headless.Client.Net.Tests")]

namespace Umbraco.Headless.Client.Net
{
    public static class Constants
    {
        /* A note on api versions and the version http header:
         * When a new version is released on Umbraco Cloud, new endpoints
         * will be available from that version forward.
         * In this library we mark the methods that support newly released features/endpoints
         * with the version from when they were available.
         * Ie. Forms was introduced in 2.1, so those endpoints send that version header.
         * All existing (original) endpoints will send the minimum version header as they are on
         * the minimum version, which is 2.0.
         */

        //Minimum version for this Library
        public const string ApiMinimumVersion = "2.0";
        public const string ApiMinimumVersionHeader = Headers.ApiVersion + ": " + ApiMinimumVersion;
        //Current version for this library
        public const string ApiVersion = "2.2";
        public const string ApiVersionHeader = Headers.ApiVersion + ": " + ApiVersion;

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
            public const string ProjectAlias = "umb-project-alias";
        }

        public static class Querystrings
        {
            public const string AcceptLanguage = "culture";
            public const string ApiVersion = "api-version";
            public const string ProjectAlias = "umb-project-alias";
        }
    }
}
