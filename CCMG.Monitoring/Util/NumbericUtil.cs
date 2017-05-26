using System;
using System.Collections.Generic;
using System.Text;

namespace CCMG.Monitoring.Util
{
    public static class NumbericUtil
    {
        /// <summary>
        /// 获取警告值
        /// </summary>
        /// <param name="goal"></param>
        /// <param name="threshold"></param>
        /// <param name="radix"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static decimal GetWarningValue(decimal? goal, decimal threshold, decimal? radix, char? symbol)
        {
            if (goal == null || radix == null || symbol == null)
                return threshold;

            decimal? returnValue = null;
            switch (symbol)
            {
                case '-':

                    break;
                case '+':
                    break;
                case '/':
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue.Value;
        }

        /// <summary>
        /// 警报等级 一般:90%-95%和105%-110%,警告:85%-90%和110%-115%,中等:70%-85%和115%-130%,
        /// 严重:50%-70%和131%-150%,灾难:-50%和151%-
        /// </summary>
        /// <param name="thresholdStadard"></param>
        /// <param name="thresholdReal"></param>
        /// <returns></returns>
        public static int CaculateAlarmLevel(decimal thresholdStadard, decimal? thresholdReal)
        {
            if (thresholdReal == null) return 0;
            decimal rate = Math.Round(thresholdReal.Value / thresholdStadard, 3);
            if ((rate > 0.9M && rate <= 0.95M) || (rate > 1.05M && rate <= 1.1M))
            {
                //一般
                return 1;
            }
            if ((rate > 0.85M && rate <= 0.9M) || (rate > 1.1M && rate <= 1.15M))
            {
                //警告
                return 2;
            }
            if ((rate > 0.7M && rate <= 0.85M) || (rate > 1.15M && rate <= 1.3M))
            {
                //中等
                return 3;
            }
            if ((rate > 0.5M && rate <= 0.7M) || (rate > 1.3M && rate <= 1.5M))
            {
                //严重
                return 4;
            }
            if (rate < 0.5M || rate > 1.5M)
            {
                //灾难
                return 5;
            }
            return 0;
        }
    }
}
