using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Umbraco.Headless.Client.Net.Web
{
    public static class HttpResponseExtensions
    {
        public static void EnterPreview(this HttpResponse response, PreviewOptions options)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var isDevelopment = response.HttpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = options.MaxAge.HasValue ? DateTime.UtcNow.Add(options.MaxAge.Value) : null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(options.SigningKey, options.SecurityAlgorithms),
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            response.Cookies.Append(PreviewMiddleware.CookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = isDevelopment == false,
                Path = "/",
                SameSite = isDevelopment ? SameSiteMode.None : SameSiteMode.Lax,
                MaxAge = options.MaxAge
            });
        }

        public static void ExitPreview(this HttpResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            response.Cookies.Delete(PreviewMiddleware.CookieName);
        }
    }
}
