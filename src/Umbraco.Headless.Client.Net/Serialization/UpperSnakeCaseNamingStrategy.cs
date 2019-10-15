using Newtonsoft.Json.Serialization;

namespace Umbraco.Headless.Client.Net.Serialization
{
    internal class UpperSnakeCaseNamingStrategy : SnakeCaseNamingStrategy
    {
        protected override string ResolvePropertyName(string name) =>
            base.ResolvePropertyName(name).ToUpperInvariant();
    }
}
