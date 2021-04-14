using System.ComponentModel.DataAnnotations;

namespace Umbraco.Headless.Client.Net.Web.Options
{
    public class ManagementOptions
    {
        /// <summary>
        /// Get or set the management api key.
        /// If the value is <c>null</c>, the ApiKey from <see cref="HeartcoreOptions"/> is used.
        /// </summary>
        [Required]
        public string ApiKey { get; set; } = null!;
    }
}
