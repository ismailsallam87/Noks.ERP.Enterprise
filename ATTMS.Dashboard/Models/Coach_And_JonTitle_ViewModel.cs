using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace ATTMS.Dashboard.Models
{
    public class Coach_And_JonTitle_ViewModel
    {
        public List<SP_select_Coach_Result> Coach { get; set; }
        public List<SP_Select_Job_Title_By_Dept_id_Result> Job_Title { get; set; }
     
    }
}