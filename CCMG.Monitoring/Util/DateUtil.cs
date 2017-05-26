using System;
using System.Collections.Generic;
using System.Text;

namespace CCMG.Monitoring.Util
{
    public class DateUtil
    {
        public static bool TimeShouldRun(string monitorTime, int runType)
        {
            bool dayOFnow = false;
            switch (runType)
            {
                case 1:
                    //小时
                    dayOFnow = monitorTime.IndexOf(DateTime.Now.ToString("HH")) != -1;
                    break;
                case 2:
                    //星期
                    dayOFnow = monitorTime.IndexOf(DateTime.Now.DayOfWeek.ToString("D")) != -1;
                    break;
                case 3:
                    //日期
                    dayOFnow = monitorTime.IndexOf(DateTime.Now.ToString("dd")) != -1;
                    break;
                default:
                    break;
            }
            return dayOFnow;
        }
    }
}
