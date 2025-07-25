using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Lib
{
    public class ConfigUtil
    {
        public IConfigurationRoot ConfigAppSettings()
        {
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);
            string envNameWin = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);
            if (!envNameWin.IsNullOrWhiteSpace())
                envName = envNameWin;

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{envName}.json", true, true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }
    }
}
