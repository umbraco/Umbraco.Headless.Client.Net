using System.Net;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal abstract class DeliveryBase
    {
        protected DeliveryBase(IHeadlessConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IHeadlessConfiguration Configuration { get; }

        protected T GetResponse<T>(ApiResponse<T> response)
        {
            if (response.IsSuccessStatusCode)
                return response.Content;

            if (Configuration is HeadlessConfiguration headlessConfiguration)
            {
                if (headlessConfiguration.ApiExceptionDelegate != null)
                {
                    var context = new ApiExceptionContext
                    {
                        Exception = response.Error
                    };
                    headlessConfiguration.ApiExceptionDelegate(context);

                    if (context.IsExceptionHandled || context.Exception == null) return default;
                }
            }

            throw response.Error;
        }
    }
}
