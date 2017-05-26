using System;
using CCMG.Monitoring.Util;

namespace CCMG.Test
{
    class Program
    {
        public const string sqlStr = @"
                select convert(varchar(10),mtsendtime,23),(select sendstarttime from InterList where interid =[interid]) 每日下发时间,
                (select count(distinct phone) from useruplink where interid = [interid] and datediff(day,uplinktime,[nowtime])>=1 and datediff(day,isnull(untime,getdate()+1),[nowtime])<0) 老用户,
                count(distinct case when dntime is null and convert(varchar(10),uplinktime,23)<> convert(varchar(10),[nowtime],23) then mtid else null end ) 老用户mt,
                count(distinct case when StateCode='success' and convert(varchar(10),uplinktime,23)<> convert(varchar(10),[nowtime],23) then mtid else null end ) 老用户dn,
                count(case when dntime is null and convert(varchar(10),untime,23)= convert(varchar(10),[nowtime],23) then 1 else null end ) 退订人数,
                ((select count(distinct phone) from useruplink where interid = @interid and datediff(day,uplinktime,[nowtime])=0) 新增用户,
                 count(distinct case when dntime is null and convert(varchar(10),uplinktime,23)= convert(varchar(10),[nowtime],23) then mtid else null end ) 新用户mt,
                count(distinct case when StateCode='success' and convert(varchar(10),uplinktime,23)= convert(varchar(10),[nowtime],23) then mtid else null end ) 新用户dn  
                from useruplink u
                inner join mtrecords mt on mt.upid = u.upid
-		a[1].Groups	{System.Text.RegularExpressions.GroupCollection}	System.Text.RegularExpressions.GroupCollection
                where u.interid = [interid]  and 
                convert(varchar(10),mtsendtime,23)=convert(varchar(10),[nowtime],23) 
                and mt.mtid is not null
                group by convert(varchar(10),mtsendtime,23)
                order by convert(varchar(10),mtsendtime,23)";

        static void Main(string[] args)
        {
            var a = RegexUtil.RegCollection("[columns=uul.appId 产品号,uul.uid 渠道号,convert(varchar(10),uul.uplinktime,23) 日期,count(uul.upid) 今日新增订阅VERIFY][conditions=appid=2886 and uid=1364 and convert(varchar(10),uul.uplinktime,23)=convert(varchar(10),getdate(),23)][groupby=group by uul.appId,uul.uid,convert(varchar(10),uul.uplinktime,23)][orderby=order by uul.appid,uul.uid]");


        }
    }
}