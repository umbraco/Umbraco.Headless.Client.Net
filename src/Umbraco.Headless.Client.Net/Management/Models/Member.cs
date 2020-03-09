using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class Member : Entity
    {
        // we don't want links to show up in the properties dictionary
        // and we don't really need them so we just map them to this field
        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        private object _links;

        public Member()
        {
            Properties = new Dictionary<string, object>();
        }

        public string Comments { get; set; }
        public string Email { get; set; }

        [JsonProperty("_failedPasswordAttempts")]
        public int FailedPasswordAttempts { get; set; }

        [JsonProperty("_groups")]
        public IEnumerable<string> Groups { get; set; }

        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? LastLockoutDate { get; set; }

        [JsonProperty("_lastLoginDate")]
        public DateTime? LastLoginDate { get; set; }

        [JsonProperty("_lastPasswordChangeDate")]
        public DateTime? LastPasswordChangeDate { get; set; }

        public string MemberTypeAlias { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; }

        public object GetValue(string alias, object defaultValue = null)
        {
            if (Properties.TryGetValue(alias, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public void SetValue(string alias, object value)
        {
            Properties[alias] = value;
        }
    }
}
