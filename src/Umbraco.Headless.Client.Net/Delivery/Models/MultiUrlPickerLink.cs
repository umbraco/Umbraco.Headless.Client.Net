namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    /// <summary>
    /// Default model for the Umbraco Multi Url Picker
    /// </summary>
    public class MultiUrlPickerLink
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public MultiUrlPickerLinkType Type { get; set; }
        public string Udi { get; set; }
        public string Url { get; set; }
    }
}
