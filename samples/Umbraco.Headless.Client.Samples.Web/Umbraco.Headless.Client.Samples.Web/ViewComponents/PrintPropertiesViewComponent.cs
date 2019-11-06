using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.ViewComponents
{
    public class PrintPropertiesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Content content)
        {
            var sb = new StringBuilder("<div style=\"display: grid; grid-template-columns: auto 1fr; grid-gap: 20px;\">");

            foreach (var property in content.Properties)
            {
                sb.Append("<div>");
                sb.Append(property.Key);
                sb.Append("</div>");
                sb.Append("<div>");
                sb.Append(property.Value);
                sb.Append("</div>");
            }

            sb.Append("</div>");
            return new HtmlContentViewComponentResult(new HtmlString(sb.ToString()));
        }
    }
}
