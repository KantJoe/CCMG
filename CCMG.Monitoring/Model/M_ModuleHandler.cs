using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CCMG.Monitoring.Model
{
    public class M_ModuleHandler:ModelBase<M_ModuleHandler>
    {
        [Key]
        public string H_ID { get; set; }

        public  int fk_MModuleID{ get; set; }

        public string H_Createor  { get; set; }

        public DateTime? H_CreateTime { get; set; }

        public string H_LastUp { get; set; }

        public DateTime? H_LastUpTime { get; set; }

        public string H_Content { get; set; }

        public int H_State { get; set; }

        public int H_TimeLast { get; set; }

        public int H_Status { get; set; }

        public bool? H_Disable { get; set; }
    }
}
