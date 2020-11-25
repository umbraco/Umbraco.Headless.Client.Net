using Refit;

namespace Umbraco.Headless.Client.Net.Configuration
{
    /// <summary>
    /// Contextual information about the API exception.
    /// </summary>
    public class ApiExceptionContext
    {
        /// <summary>
        /// The exception.
        /// </summary>
        public ApiException Exception { get; set; }

        /// <summary>
        /// Determines if the exception should be thrown or not.
        /// If <c>true</c> the exception will not be thrown, otherwise it will.
        /// </summary>
        public bool IsExceptionHandled { get; set; }
    }
}
