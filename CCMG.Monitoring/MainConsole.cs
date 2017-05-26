using CCMG.Monitoring.Util;
using CCMG.Monitoring.WinServices;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMG.Monitoring
{
    public class MainConsole : MicroService,IMicroService
    {
        public void Start()
        {
            StartBase();
            LogUtil.WriteLog("Started\n");

            Timers.Start("Kant", int.Parse(Configs.Config["interval"]),  () =>
            {
                Task t1 = Task.Factory.StartNew(delegate { new InterTerminateService().MainControl(); }) ;
                Task.WaitAll(t1);
            }, (ex) =>
            {
                LogUtil.WriteLog(ex.Message);
            });
        }

        public void Stop()
        {
            StopBase();
            LogUtil.WriteLog("Stopped\n");
        }
    }
}
