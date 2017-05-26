using CCMG.Monitoring.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CCMG.Monitoring.WinServices
{
    public class InterTerminateService
    {
        public void MainControl()
        {
            RunWatch();
        }

        public void RunWatch()
        {
            IDbConnection conn = new SqlConnection(Configs.Config["connstr"]);
            foreach (dynamic item in conn.Query<dynamic>("select * from M_MModule where M_Module=2 and M_TaskID='6'"))
            {

                try
                {
                    if (!DateUtil.TimeShouldRun(item.M_Time, item.M_RunType)) continue;
                    if (item.M_LastTime != null && item.M_LastTime.AddMinutes(item.M_Last) > DateTime.Now) continue;
                    bool isSuccess = false;
                    string sql = RegexUtil.RegReplaceStr(item.M_Condition, sqlStr, "", out isSuccess);
                    dynamic data = conn.QuerySingle<dynamic>(sql);

                    EventProcess(conn, data, item);
                }
                catch (Exception ex)
                {
                    LogUtil.WriteLog(ex.StackTrace + Environment.NewLine + ex.Message);
                }
            }
        }

        public void EventProcess(IDbConnection conn, dynamic data,dynamic module)
        {

        }

        /// <summary>
        /// 监控的标准sql
        /// [xxx]参数:配合mudule.condition
        /// </summary>
        public const string sqlStr = @"
                select convert(varchar(10),mtsendtime,23) 日期,(select sendstarttime from InterList where interid =[interid]) 每日下发时间,
                (select count(distinct phone) from useruplink where interid = [interid] and datediff(day,uplinktime,[nowtime])>=1 and datediff(day,isnull(untime,getdate()+1),[nowtime])<0) 老用户,
                count(distinct case when dntime is null and convert(varchar(10),uplinktime,23)<> convert(varchar(10),[nowtime],23) then mtid else null end ) 老用户mt,
                count(distinct case when StateCode='success' and convert(varchar(10),uplinktime,23)<> convert(varchar(10),[nowtime],23) then mtid else null end ) 老用户dn,
                count(case when dntime is null and convert(varchar(10),untime,23)= convert(varchar(10),[nowtime],23) then 1 else null end ) 退订人数,
                (select count(distinct phone) from useruplink where interid = [interid] and datediff(day,uplinktime,[nowtime])=0) 新增用户,
                 count(distinct case when dntime is null and convert(varchar(10),uplinktime,23)= convert(varchar(10),[nowtime],23) then mtid else null end ) 新用户mt,
                count(distinct case when StateCode='success' and convert(varchar(10),uplinktime,23)= convert(varchar(10),[nowtime],23) then mtid else null end ) 新用户dn  
                from useruplink u
                inner join mtrecords mt on mt.upid = u.upid
                where u.interid = [interid]  and 
                convert(varchar(10),mtsendtime,23)=convert(varchar(10),[nowtime],23) 
                and mt.mtid is not null
                group by convert(varchar(10),mtsendtime,23)
                order by convert(varchar(10),mtsendtime,23)";
    }
}
