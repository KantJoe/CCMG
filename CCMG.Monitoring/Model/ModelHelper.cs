using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq.Expressions;

namespace CCMG.Monitoring.Model
{
    public static class ModelHelper
    {

        public static bool Update<T>(this ModelBase<T> model,params Expression<Func<T,bool>>[] fun)
        {
            return false;
        }

        public static ModelBase<T> Insert<T>(this ModelBase<T> model)
        {

            return null;
        }

        public static bool Delete<T>(this ModelBase<T> model)
        {

            return false;
        }
    }
}
