using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class GridSection
    {
        public int Grid { get; set; }
        public IEnumerable<GridRow> Rows { get; set; }
    }
}
