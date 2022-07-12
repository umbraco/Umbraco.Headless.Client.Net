using System.Collections.Generic;

namespace Umbraco.Headless.Client.Samples.MinimalAPI.Models
{
    public class UniqueSellingPointsViewModel
    {
        public string Title { get; set; }
        public IEnumerable<UniqueSellingPoint> UniqueSellingPoints { get; set; }
    }
}
