using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Headless.Client.Net.Web.Options;

namespace Umbraco.Headless.Client.Net.Web
{
    public class PreviewMiddleware : IMiddleware
    {
        private readonly ILogger<PreviewMiddleware> _logger;
        private readonly IOptions<PreviewOptions> _options;
        private readonly IUmbracoContext _umbracoContext;
        internal const string CookieName = "__umbraco_preview";

        public PreviewMiddleware(ILogger<PreviewMiddleware> logger, IOptions<PreviewOptions> options,
            IUmbracoContext umbracoContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _umbracoContext = umbracoContext ?? throw new ArgumentNullException(nameof(umbracoContext));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var jwt = context.Request.Cookies[CookieName];
            if (jwt != null)
            {
                var key = _options.Value.SigningCredentials.Key;
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
                    _umbracoContext.IsInPreviewMode = true;
                }
                else
                {
                    _logger.LogDebug(result.Exception, "Preview cookie was not valid, exiting preview");
                    context.Response.ExitPreview();
                }
            }

            await next(context).ConfigureAwait(false);
        }
    }
}
