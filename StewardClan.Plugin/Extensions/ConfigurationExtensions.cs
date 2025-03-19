using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Steward_Clan.Plugin.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddMergedJsonFile(
            this IConfigurationBuilder builder,
            params List<string> Paths)
        {
            return builder.AddMergedJsonFile(xs =>
            {
                xs.FileProvider = null;
                xs.Paths = Paths;
                xs.Optional = false;
            });
        }
        public static IConfigurationBuilder AddMergedJsonFile(this IConfigurationBuilder builder, Action<MergedJsonConfigurationSource>? configureSource)
            => builder.Add(configureSource);
    }
}
