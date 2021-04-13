using System;
using Microsoft.IdentityModel.Tokens;

namespace Umbraco.Headless.Client.Net.Web
{
    public class PreviewOptions
    {
        private SecurityKey? _signingKey;

        internal bool Enabled { get; set; }
        public TimeSpan? MaxAge { get; set; }
        public string? Secret { get; set; }

        public SecurityKey SigningKey
        {
            get
            {
                var random = new Random();
                byte[] bytes = new byte[32];
                random.NextBytes(bytes);
                return _signingKey ??= new SymmetricSecurityKey(bytes);
            }
            set => _signingKey = value;
        }

        public string SecurityAlgorithms { get; set; } =
            Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;
    }
}
