using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    /// <summary>
    /// Used to resolve all controler types of type UmbracoController
    /// </summary>
    public sealed class UmbracoControllerTypeCollection
    {
        public UmbracoControllerTypeCollection(IActionDescriptorCollectionProvider actionDescriptorProviderContext)
        {
            if (actionDescriptorProviderContext == null) throw new ArgumentNullException(nameof(actionDescriptorProviderContext));

            _umbracoControllerDescriptors = new Lazy<ControllerActionDescriptor[]>(() =>
            {
                return actionDescriptorProviderContext.ActionDescriptors.Items
                    .OfType<ControllerActionDescriptor>()
                    .Where(x => typeof(IUmbracoController).IsAssignableFrom(x.ControllerTypeInfo))
                    .ToArray();
            });
        }

        private readonly Lazy<ControllerActionDescriptor[]> _umbracoControllerDescriptors;

        public IEnumerable<TypeInfo> UmbracoControllerTypes => _umbracoControllerDescriptors.Value.Select(x => x.ControllerTypeInfo);

        public string GetControllerName(string name)
        {
            var found = _umbracoControllerDescriptors.Value.FirstOrDefault(t => string.Equals(t.ControllerName, name, StringComparison.InvariantCultureIgnoreCase));
            return found?.ControllerName ?? UmbracoRouter.DefaultUmbracoControllerName;
        }

        public bool ContainsControllerName(string name) =>
            _umbracoControllerDescriptors.Value.Any(t => string.Equals(t.ControllerName, name, StringComparison.InvariantCultureIgnoreCase));

        public string GetControllerActionName(string controllerName, string templateAlias)
        {
            var found = _umbracoControllerDescriptors.Value
                .FirstOrDefault(t => string.Equals(t.ControllerName, controllerName, StringComparison.InvariantCultureIgnoreCase) && string.Equals(t.ActionName, templateAlias, StringComparison.InvariantCultureIgnoreCase));
            return found?.ActionName ?? UmbracoRouter.DefaultControllerAction;
        }
    }
}
