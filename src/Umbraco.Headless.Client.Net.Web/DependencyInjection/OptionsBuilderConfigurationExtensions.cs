#if !NET5_0
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class OptionsBuilderConfigurationExtensions
    {
        public static OptionsBuilder<TOptions> BindConfiguration<TOptions>(
            this OptionsBuilder<TOptions> optionsBuilder,
            string configSectionPath,
            Action<BinderOptions>? configureBinder = null)
            where TOptions : class
        {
            if (optionsBuilder == null) throw new ArgumentNullException(nameof(optionsBuilder));
            if (configSectionPath == null) throw new ArgumentNullException(nameof(configSectionPath));
            optionsBuilder.Configure<IConfiguration>((opts, config) =>
                (string.Equals("", configSectionPath, StringComparison.OrdinalIgnoreCase)
                    ? config
                    : config.GetSection(configSectionPath)).Bind(opts, configureBinder));
            return optionsBuilder;
        }
    }
}
#endif
