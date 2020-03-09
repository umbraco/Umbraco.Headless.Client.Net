using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MemberResetPasswordToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("_embedded")]
        [JsonConverter(typeof(NestedPropertyConverter), "member")]
        public Member Member { get; set; }
    }
}
