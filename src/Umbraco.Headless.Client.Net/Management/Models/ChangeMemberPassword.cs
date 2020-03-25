using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    class ChangeMemberPassword
    {
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
