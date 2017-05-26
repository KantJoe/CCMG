using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace CCMG.Monitoring.BLL
{
    public class MonitorBll
    {
        public IDbConnection conn { get; set; }

        public MonitorBll(IDbConnection _con)
        {
            conn = _con;
        }

        public int AddEvent(dynamic eventModel)
        {
            IDbTransaction tran = conn.BeginTransaction();
            try
            {
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }
            return 0;
        }

        public int UpdateEvent()
        {

            return 0;
        }
    }
}
