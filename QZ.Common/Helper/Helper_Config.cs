using Microsoft.Extensions.Configuration;
using System.IO;

namespace QZ.Common.Helper
{
    /// <summary>
    /// 加载配置文件
    /// </summary>
    public class Helper_Config
    {
        private static IConfiguration configuration;

        /// <summary>
        /// 加载 Json 配置文件
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (configuration != null)
                {
                    return configuration;
                }
                configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
                return configuration;
            }
            set => configuration = value;
        }
    }
}
