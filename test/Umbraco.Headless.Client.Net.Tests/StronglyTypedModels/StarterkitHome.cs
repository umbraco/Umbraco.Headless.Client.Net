using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Tests.StronglyTypedModels
{
    public class StarterkitHome : ContentBase
    {
        public string HeroHeader { get; set; }
        public string HeroDescription { get; set; }
        public string HeroCTACaption { get; set; }
        
        public HeroCtalink HeroCtalink { get; set; }
    }
}
