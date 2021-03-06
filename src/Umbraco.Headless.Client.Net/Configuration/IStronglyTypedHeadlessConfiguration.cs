using Umbraco.Headless.Client.Net.Collections;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Configuration
{
    public interface IStronglyTypedHeadlessConfiguration : IHeadlessConfiguration
    {
        /// <summary>
        /// A list of strongly typed Element Type models,
        /// </summary>
        ITypeList<IElement> ElementModelTypes { get; }

        /// <summary>
        /// A list of strongly typed Content Type models,
        /// </summary>
        ITypeList<IContent> ContentModelTypes { get; }

        /// <summary>
        /// A list of strongly typed Media Type models,
        /// </summary>
        ITypeList<IMedia> MediaModelTypes { get; }
    }
}
