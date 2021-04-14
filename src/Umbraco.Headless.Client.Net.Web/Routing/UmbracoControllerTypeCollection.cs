using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Umbraco.Headless.Client.Net.Web.Options;

namespace Umbraco.Headless.Client.Net.Web.Routing
{
    internal sealed class UmbracoControllerTypeCollection
    {
        private readonly Lazy<ILookup<string, ControllerActionDescriptor>> _umbracoControllerDescriptors;
        private ControllerActionDescriptor? _defaultControllerDescriptor;
        private readonly IOptions<UmbracoRouterOptions>? _options;

        public UmbracoControllerTypeCollection(IActionDescriptorCollectionProvider actionDescriptorProviderContext, IOptions<UmbracoRouterOptions> options)
        {
            if (actionDescriptorProviderContext == null) throw new ArgumentNullException(nameof(actionDescriptorProviderContext));

            _options = options ?? throw new ArgumentNullException(nameof(options));

            _umbracoControllerDescriptors = new Lazy<ILookup<string, ControllerActionDescriptor>>(() =>
            {
                var umbracoControllerDescriptors = new List<ControllerActionDescriptor>();
                var descriptors = actionDescriptorProviderContext.ActionDescriptors
                    .Items.OfType<ControllerActionDescriptor>();

                foreach (var descriptor in descriptors)
                {
                    var attribute = descriptor.ControllerTypeInfo.GetCustomAttribute<UmbracoControllerAttribute>();
                    if (attribute == null) continue;

                    var controllerName = attribute.ContentTypeAlias ?? descriptor.ControllerName;
                    descriptor.ControllerName = controllerName;

                    umbracoControllerDescriptors.Add(descriptor);
                }

                _defaultControllerDescriptor = actionDescriptorProviderContext.ActionDescriptors
                    .Items.OfType<ControllerActionDescriptor>()
                    .FirstOrDefault(x => x.ControllerTypeInfo == options.Value.DefaultController);

                return umbracoControllerDescriptors.ToLookup(x => x.ControllerName,
                    StringComparer.InvariantCultureIgnoreCase);
            });
        }

        public ActionDescriptor? FindActionDescriptor(string controllerName)
        {
            return TryFind(controllerName) ?? _defaultControllerDescriptor;
        }

        private ActionDescriptor? TryFind(string controllerName)
        {
            if (_options == null) throw new InvalidOperationException();

            var foundDescriptors = _umbracoControllerDescriptors.Value[controllerName];

            return foundDescriptors.FirstOrDefault(x =>
                x.ActionName.Equals(_options.Value.DefaultActionName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
