using Microsoft.Extensions.Configuration;
using System.IO;

namespace CCMG.Monitoring.Util
{
    public class Configs
    {
        private static IConfigurationRoot _config { get; set; }
        public static IConfigurationRoot Config
        {
            get
            {
                if (_config == null)
                    _config=new Configs().Build();
                return _config;
            }
            set
            {
                _config = value;
            }
        }

        public IConfigurationRoot Build(string configPath="")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(string.IsNullOrEmpty(configPath) ?Directory.GetCurrentDirectory():configPath)
                .AddJsonFile("appsetting.json",optional:true,reloadOnChange:true);
            Config = builder.Build();

            return Config;
        }
    }
}
