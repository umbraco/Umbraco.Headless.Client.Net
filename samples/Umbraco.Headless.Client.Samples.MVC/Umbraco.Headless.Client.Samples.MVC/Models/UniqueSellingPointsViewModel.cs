using System.Collections.Generic;

namespace Umbraco.Headless.Client.Samples.MVC.Models
{
    public class UniqueSellingPointsViewModel
    {
        public string Title { get; set; }
        public IEnumerable<UniqueSellingPoint> UniqueSellingPoints { get; set; }
    }
}
