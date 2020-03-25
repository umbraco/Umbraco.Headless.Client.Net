using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    class ResetMemberPassword
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
