using System;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Headless.Client.Net.Web.Controllers;

namespace Umbraco.Headless.Client.Net.Web.Options
{
    public class UmbracoRouterOptions
    {
        /// <summary>
        /// Set the default controller.
        /// </summary>
        /// <typeparam name="T">The controller type.</typeparam>
        /// <returns></returns>
        public UmbracoRouterOptions SetDefaultController<T>() where T : Controller
        {
            DefaultController = typeof(T);

            return this;
        }

        /// <summary>
        /// Set the default action name.
        /// Note: Changing this will also change the name of the action to call for controllers marked with <see cref="UmbracoControllerAttribute"/>.
        /// </summary>
        /// <param name="name">the action name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public UmbracoRouterOptions SetDefaultActionName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            DefaultActionName = name;

            return this;
        }

        /// <summary>
        /// The default controller to call if no custom controller is found.
        /// </summary>
        public Type DefaultController { get; private set; } = typeof(UmbracoDefaultController);

        /// <summary>
        /// Name of the action to call.
        /// </summary>
        public string DefaultActionName { get; private set; } = nameof(UmbracoDefaultController.Index);
    }
}
