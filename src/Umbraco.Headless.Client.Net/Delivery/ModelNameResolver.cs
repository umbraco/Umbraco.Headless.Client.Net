using System;
using System.Collections.Concurrent;
using System.Reflection;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class ModelNameResolver
    {
        private readonly ConcurrentDictionary<Type, string> _cache = new ConcurrentDictionary<Type, string>();

        public string GetElementModelAlias(Type type) =>
            GetModelAlias(type, () => type.GetCustomAttribute<ElementModelAttribute>()?.ContentTypeAlias);

        public string GetContentModelAlias(Type type) =>
            GetModelAlias(type, () => type.GetCustomAttribute<ContentModelAttribute>()?.ContentTypeAlias);

        public string GetMediaModelAlias(Type type) =>
            GetModelAlias(type, () => type.GetCustomAttribute<MediaModelAttribute>()?.MediaTypeAlias);

        private string GetModelAlias(Type type, Func<string> customResolve)
        {
            return _cache.GetOrAdd(type, _ =>
            {

                var alias = customResolve();
                if (alias != null)
                    return alias;

                var className = type.Name;
                if (className.IndexOf("Model", StringComparison.Ordinal) > -1)
                {
                    className = className.Substring(0, className.IndexOf("Model", StringComparison.Ordinal));
                }

                // test for default casing
                if (className.Length > 1)
                    className = $"{className.Substring(0, 1).ToLower()}{className.Substring(1)}";

                return className;
            });
        }
    }
}
