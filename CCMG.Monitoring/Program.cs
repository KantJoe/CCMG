using CCMG.Monitoring.Util;
using CCMG.Monitoring.WinServices;
using Dapper;
using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CCMG.Monitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceRunner<MainConsole>.Run(config =>
            {


                var name = config.GetDefaultName();
                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory((extraArguments) =>
                    {
                        return new MainConsole();
                    });

                    serviceConfig.OnStart((service, extraParams) =>
                    {
                        Console.WriteLine("Service {0} started", name);
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        Console.WriteLine("Service {0} stopped", name);
                        service.Stop();
                    });

                    serviceConfig.OnError(e =>
                    {
                        Console.WriteLine("Service {0} errored with exception : {1}", name, e.Message);
                    });
                });
            });
        }

        public static void Test()
        {

        }
    }
}



//1.restore -r win7-x64

//2.publish -r win7-x64 -f  netcoreapp1.1

//Run the service with action:install and it will install the service.

//Run the service with action:uninstall and it will uninstall the service.

//Run the service with action:start and it will start the service.

//Run the service with action:stop and it will stop the service.

//Run the service with username:YOUR_USERNAME, password:YOUR_PASSWORD and action:install which installs it for the given account.

//Run the service with description:YOUR_DESCRIPTION and it setup description for the service.

//Run the service with displayName:YOUR_DISPLAY_NAME and it setup Display name for the service.

//Run the service with name:YOUR_NAME and it setup name for the service.