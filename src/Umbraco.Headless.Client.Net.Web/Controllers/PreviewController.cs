using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Headless.Client.Net.Delivery;

namespace Umbraco.Headless.Client.Net.Web.Controllers
{
    public class PreviewController : Controller
    {
        private readonly IOptions<PreviewOptions> _previewOptions;
        private readonly ContentPreviewService _previewService;

        public PreviewController(IOptions<PreviewOptions> previewOptions, ContentPreviewService previewService)
        {
            _previewOptions = previewOptions ?? throw new ArgumentNullException(nameof(previewOptions));
            _previewService = previewService ?? throw new ArgumentNullException(nameof(previewService));
        }

        public async Task<IActionResult> Index(string secret, string slug)
        {
            if (_previewOptions.Value.Secret != secret)
                return Unauthorized("Invalid token");

            var content = await _previewService.Content.GetByUrl(slug);

            if (content == null)
                return Unauthorized("Invalid slug");

            Response.EnterPreview(_previewOptions.Value);

            return RedirectPreserveMethod(content.Url);
        }

        public IActionResult Exit()
        {
            Response.ExitPreview();

            return RedirectPreserveMethod("/");
        }
    }
}
