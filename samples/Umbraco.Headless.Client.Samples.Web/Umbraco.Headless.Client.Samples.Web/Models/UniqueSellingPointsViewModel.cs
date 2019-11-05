using System.Collections.Generic;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class UniqueSellingPointsViewModel
    {
        public string Title { get; set; }
        public IEnumerable<UniqueSellingPoint> UniqueSellingPoints { get; set; }
    }
}
