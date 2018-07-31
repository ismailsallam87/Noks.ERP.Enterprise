using OTS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nox_OTS.Models
{
    public class TaskStatusViewModel
    {
        public List<SP_Select_Users_Result> Users { get; set; }
        public List<SP_Select_Remained_Task_Status_ByTaskId_Result> RemainedStatuses { get; set; }

        public List<SP_Select_Task_Status_ByTaskId_Result> Statuses { get; set; }
        public int TaskId { get; set; }
        public string IsTaskClosed { get; set; }//

    }
}