using System;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public interface IEntity
    {
        DateTime CreateDate { get; set; }
        Guid Id { get; set; }
    }
}
