using OTS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nox_OTS.Models
{
    public class TaskItems
    {
        public int TaskId { get; set; }
        public List<SP_Select_TaskItems_By_TaskID_Result> Items { get; set; }
    }
}