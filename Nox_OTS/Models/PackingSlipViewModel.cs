using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OTS.DAL;

namespace Nox_OTS.Models
{
    public class PackingSlipViewModel
    {
        public string BarcodeImage { get; set; }
        public string IsClosed { get; set; }
        public SP_Select_Task_By_TaskID_Result Task { get; set; }
        public List<SP_Select_TaskItems_By_TaskID_Result> TaskItems { get; set; }
    }
}