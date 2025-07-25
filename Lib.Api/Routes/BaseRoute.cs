using Microsoft.Extensions.Configuration;

namespace Lib.Api.Routes
{
    /// <summary>
    /// API Name (API Url 定義於 appsettings.json)
    /// </summary>
    public class ApiName
    {
        public const string OPState = "OPState";
        public const string Hr = "Hr";
    }

    public class BaseRoute
    {
        /// <summary>
        /// API Service Url
        /// </summary>
        public static string Service(string name)
        {
            ConfigUtil configUtil = new ConfigUtil();
            IConfigurationRoot configuration = configUtil.ConfigAppSettings();
            return configuration.GetValue<string>($"ApiUrl:{name}");
        }
    }
}
