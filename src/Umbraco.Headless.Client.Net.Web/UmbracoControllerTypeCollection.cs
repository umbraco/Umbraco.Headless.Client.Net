using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Umbraco.Headless.Client.Net.Web
{
    internal sealed class UmbracoControllerTypeCollection
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorProviderContext;
        private readonly Lazy<ILookup<string, ControllerActionDescriptor>> _umbracoControllerDescriptors;
        private ControllerActionDescriptor _defaultControllerDescriptor;
        private UmbracoRouterOptions _options;

        public UmbracoControllerTypeCollection(IActionDescriptorCollectionProvider actionDescriptorProviderContext)
        {
            _actionDescriptorProviderContext = actionDescriptorProviderContext ?? throw new ArgumentNullException(nameof(actionDescriptorProviderContext));

            _umbracoControllerDescriptors = new Lazy<ILookup<string, ControllerActionDescriptor>>(() =>
            {
                var umbracoControllerDescriptors = new List<ControllerActionDescriptor>();
                var descriptors = actionDescriptorProviderContext.ActionDescriptors.Items.OfType<ControllerActionDescriptor>();

                foreach (var descriptor in descriptors)
                {
                    var attribute = descriptor.ControllerTypeInfo.GetCustomAttribute<UmbracoControllerAttribute>();
                    if (attribute == null) continue;

                    var controllerName = attribute.ContentTypeAlias ?? descriptor.ControllerName;
                    descriptor.ControllerName = controllerName;

                    umbracoControllerDescriptors.Add(descriptor);
                }

                return umbracoControllerDescriptors.ToLookup(x => x.ControllerName,
                    StringComparer.InvariantCultureIgnoreCase);
            });
        }

        public void Initialize(UmbracoRouterOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (_defaultControllerDescriptor == null)
            {
                _defaultControllerDescriptor = _actionDescriptorProviderContext.ActionDescriptors
                    .Items.OfType<ControllerActionDescriptor>()
                    .FirstOrDefault(x => x.ControllerTypeInfo == options.DefaultController);
            }
        }

        public ActionDescriptor FindActionDescriptor(string controllerName)
        {
            return TryFind(controllerName) ?? _defaultControllerDescriptor;
        }

        private ActionDescriptor TryFind( string controllerName)
        {
            var foundDescriptors = _umbracoControllerDescriptors.Value[controllerName];

            return foundDescriptors.FirstOrDefault(x =>
                x.ActionName.Equals(_options.DefaultActionName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
