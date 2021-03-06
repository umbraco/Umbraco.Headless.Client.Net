using System;
using System.Net;
using Umbraco.Headless.Client.Net.Collections;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Configuration
{
    public class HeadlessConfiguration : IStronglyTypedHeadlessConfiguration
    {
        public HeadlessConfiguration(string projectAlias)
        {
            ProjectAlias = projectAlias ?? throw new ArgumentNullException(nameof(projectAlias));
            ElementModelTypes = new TypeList<IElement>();
            ContentModelTypes = new TypeList<IContent>();
            MediaModelTypes = new TypeList<IMedia>
            {
                typeof(File),
                typeof(Folder),
                typeof(Image)
            };
        }

        /// <inheritdoc />
        public string ProjectAlias { get; }

        /// <inheritdoc />
        public ITypeList<IElement> ElementModelTypes { get; }

        /// <inheritdoc />
        public ITypeList<IContent> ContentModelTypes { get; }

        /// <inheritdoc />
        public ITypeList<IMedia> MediaModelTypes { get; }

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
