using System;
using Microsoft.AspNetCore.Mvc;

namespace Umbraco.Headless.Client.Net.Web
{
    public class UmbracoRouterOptions
    {
        public UmbracoRouterOptions SetDefaultController<T>() where T : Controller
        {
            DefaultController = typeof(T);

            return this;
        }

        /// <summary>
        /// The default controller to call if no custom controller is found.
        /// </summary>
        public Type DefaultController { get; private set; } = typeof(UmbracoDefaultController);

        /// <summary>
        /// Name of the action to call.
        /// Note: Changing this will also change the name of the action to call for controllers marked with <see cref="UmbracoControllerAttribute"/>.
        /// </summary>
        public string DefaultActionName { get; set; } = nameof(UmbracoDefaultController.Index);
    }
}
