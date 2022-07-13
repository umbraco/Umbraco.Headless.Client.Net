using System.Collections.Generic;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public class FooterViewModel
    {
        public string Title { get; set; }
        public IEnumerable<MultiUrlPickerLink> Links { get; set; }
    }
}
