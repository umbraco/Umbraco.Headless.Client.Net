using System;
using Microsoft.AspNetCore.Http;

namespace Umbraco.Headless.Client.Net.Web
{
    public interface IPreviewAccessor
    {
        bool IsPreview { get; set; }
    }

    internal class PreviewAccessor : IPreviewAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PreviewAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public bool IsPreview
        {
            get => true.Equals(_httpContextAccessor.HttpContext.Items[nameof(PreviewAccessor)]);
            set => _httpContextAccessor.HttpContext.Items[nameof(PreviewAccessor)] = value;
        }
    }
}
