using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Umbraco.Headless.Client.Net.Web.Options
{
    public class PreviewOptions
    {
        internal bool Enabled { get; set; }

        /// <summary>
        /// Get or set the preview api key.
        /// If the value is <c>null</c>, the ApiKey from <see cref="HeartcoreOptions"/> is used.
        /// </summary>
        [Required]
        public string ApiKey { get; set; } = null!;

        /// <summary>
        /// Get or set the max age for the preview cookie to be valid.
        /// If <c>null</c>, the cookie will be valid for the current session.
        /// </summary>
        public TimeSpan? MaxAge { get; set; }

        /// <summary>
        /// Get or set the preview secret used to enter preview mode.
        /// </summary>
        [Required]
        public string Secret { get; set; } = null!;

        /// <summary>
        /// Get or set the <see cref="SigningCredentials"/> used to sign the preview cookie.
        /// The default is a random symmetric key that is generated each time the application starts.
        /// </summary>
        [Required]
        public SigningCredentials? SigningCredentials { get; set; }
    }
}
