using CCMG.Monitoring.Util;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCMG.Monitoring.WinServices
{
    public class ExampleServiceTimer : MicroService, IMicroService
    {
        public void Start()
        {
            StartBase();
            LogUtil.WriteLog("Started\n");
            Timers.Start("Poller", 1000, async () =>
             {
                 await run("a");
                 await run("b");
             },(ex)=>
             {
                 LogUtil.WriteLog(ex.Message);
             });
        }

        public void Stop()
        {
            
            StopBase();
            LogUtil.WriteLog("Stopped\n");
        }

        public async Task run(string str)
        {
            await Task.Run(() =>
            {
                LogUtil.WriteLog(string.Format(Thread.CurrentThread.ManagedThreadId + "Polling at {0}\n", str));
                Thread.Sleep(1000);
            });
        }
    }
}
