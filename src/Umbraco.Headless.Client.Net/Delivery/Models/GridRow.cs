using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class GridRow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GridArea> Areas { get; set; }
    }
}
