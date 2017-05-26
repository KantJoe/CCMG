using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CCMG.Monitoring.Util
{
    public class LogUtil
    {
        private static ReaderWriterLockSlim lockObject = new ReaderWriterLockSlim();

        private static string fileName = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "{0}.txt");

        public static void WriteLog(string log)
        {
            if (string.IsNullOrEmpty(log)) return;

            log = string.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log);

            try
            {
                lockObject.TryEnterWriteLock(-1);

                File.AppendAllText(string.Format(fileName, DateTime.Now.ToString("yyyyMMddHH")), log);
            }
            finally
            {
                lockObject.ExitWriteLock();
            }
        }
    }
}
