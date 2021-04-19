using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using Umbraco.Headless.Client.Net.Collections;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Web.Options
{
    public class HeartcoreOptions
    {
        /// <summary>
        /// Get or set the project alias.
        /// </summary>
        [Required]
        public string ProjectAlias { get; set; } = null!;

        /// <summary>
        /// Get or set the api key.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// A list of strongly typed Element Type models,
        /// </summary>
        public ITypeList<IElement> ElementModelTypes { get; } = new TypeList<IElement>();

        /// <summary>
        /// A list of strongly typed Content Type models,
        /// </summary>
        public ITypeList<IContent> ContentModelTypes { get; } = new TypeList<IContent>();

        /// <summary>
        /// A list of strongly typed Media Type models,
        /// </summary>
        public ITypeList<IMedia> MediaModelTypes { get; } = new TypeList<IMedia>();

        /// <summary>
        /// Discover and add models from assembly.
        /// </summary>
        /// <param name="assembly">The assembly to discover models in.</param>
        public void AddModels(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            foreach (var type in assembly.GetExportedTypes())
            {
                if (typeof(IElement).IsAssignableFrom(type))
                    ElementModelTypes.Add(type);

                if (typeof(IContent).IsAssignableFrom(type))
                    ContentModelTypes.Add(type);

                if(typeof(IMedia).IsAssignableFrom(type))
                    MediaModelTypes.Add(type);
            }
        }

        /// <summary>
        /// A delegate that is called if the APIs return a non success error code.
        /// Can be used to handle, log or modify the exception before it is thrown.
        /// The default sets <see cref="ApiExceptionContext.IsExceptionHandled"/> to <c>true</c> for not found requests.
        /// </summary>
        public Action<ApiExceptionContext> ApiExceptionDelegate { get; set; } = context =>
        {
            context.IsExceptionHandled = context.Exception.StatusCode == HttpStatusCode.NotFound;
        };
    }
}
