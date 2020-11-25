using System;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public interface IElement
    {
        string ContentTypeAlias { get; set; }
        Guid Id { get; set; }
    }
}
