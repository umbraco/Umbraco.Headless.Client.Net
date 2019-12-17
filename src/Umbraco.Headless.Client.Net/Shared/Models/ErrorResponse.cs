using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    public class ErrorResponse
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}
