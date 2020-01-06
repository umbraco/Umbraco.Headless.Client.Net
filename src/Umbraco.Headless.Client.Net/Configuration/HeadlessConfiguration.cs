using System;
using Umbraco.Headless.Client.Net.Collections;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Configuration
{
    public class HeadlessConfiguration : IStronglyTypedHeadlessConfiguration
    {
        public HeadlessConfiguration(string projectAlias)
        {
            ProjectAlias = projectAlias ?? throw new ArgumentNullException(nameof(projectAlias));
            ContentModelTypes = new TypeList<IContent>();
            MediaModelTypes = new TypeList<IMedia>
            {
                typeof(File),
                typeof(Folder),
                typeof(Image)
            };
        }

        public string ProjectAlias { get; }
        public ITypeList<IContent> ContentModelTypes { get; }
        public ITypeList<IMedia> MediaModelTypes { get; }
    }
}
