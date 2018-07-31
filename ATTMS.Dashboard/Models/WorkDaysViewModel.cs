using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace ATTMS.Dashboard.Models
{
    public class WorkDaysViewModel
    {
        public List<SP_WorkDays_Select_Result> DefaultDays { get; set; }
        public List<SP_Select_Shift_Days_Result> ShiftDays { get; set; }
        public int ShiftId { get; set; }
    }
}