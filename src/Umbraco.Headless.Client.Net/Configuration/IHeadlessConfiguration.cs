using Umbraco.Headless.Client.Net.Collections;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Configuration
{
    public interface IHeadlessConfiguration
    {
        string ProjectAlias { get; }
        ITypeList<IContent> ContentModelTypes { get; }
    }
}
