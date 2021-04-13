using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Headless.Client.Net.Web.Builders;

namespace Umbraco.Headless.Client.Net.Web
{
    public class PreviewMiddleware : IMiddleware
    {
        private readonly IOptions<PreviewOptions> _options;
        private readonly IPreviewAccessor _previewAccessor;
        internal const string CookieName = "__umbraco_preview";

        public PreviewMiddleware(IOptions<PreviewOptions> options, IPreviewAccessor previewAccessor)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _previewAccessor = previewAccessor ?? throw new ArgumentNullException(nameof(previewAccessor));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var jwt = context.Request.Cookies[CookieName];
            if (jwt != null)
            {
                var key = _options.Value.SigningKey;
                var handler = new JsonWebTokenHandler();

                TokenValidationResult result =
                    handler.ValidateToken(jwt, new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = key
                    });

                if (result.IsValid)
                {
                    _previewAccessor.IsPreview = true;
                }
                else
                {
                    context.Response.ExitPreview();
                }
            }

            await next(context).ConfigureAwait(false);
        }
    }
}
