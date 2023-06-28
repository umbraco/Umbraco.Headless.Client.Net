using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class GridArea
    {
        public int Grid { get; set; }
        public IEnumerable<GridControl> Controls { get; set; }
    }
}
