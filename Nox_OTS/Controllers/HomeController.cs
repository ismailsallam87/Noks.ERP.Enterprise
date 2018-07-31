using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTS.DAL;
namespace Nox_OTS.Controllers
{
    public class HomeController : Controller
    {
        private OTSEntities db = new OTSEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View("Dashboard",null);
        }
      
        public ActionResult Region_Tasks_Tabs_content(int? ID)
        {
            try
            {
  List<SP_select_Areas_Tasks_Result> Parent_Menu = db.SP_select_Areas_Tasks(ID).ToList();
  return PartialView("_Tasks", Parent_Menu);
            }
            catch (Exception ex)
            {

                throw;
            }
          
          
        }

        public ActionResult Display_Region_Tasks()
        {
            List<SP_Select_Areas_Has_Tasks_Result> model = db.SP_Select_Areas_Has_Tasks().ToList();
            return PartialView("_Display_Region_Tasks",model);
        }

        public ActionResult Widget_Display_Tasks()
        {
            var model = db.SP_Widget_Display_Tasks().ToList();
            return PartialView("_Widget_Display_Tasks", model);
        }
        public ActionResult Widget_Unfinshed_Tasks()
        {
            int count = Convert.ToInt32(db.SP_Widget_UnFinished_Tasks().FirstOrDefault().ToString());
            return PartialView("_Widget_Unfinshed_Tasks", count);
        }
        public ActionResult Widget_Runners()
        {
            int count = Convert.ToInt32(db.SP_Widget_Runners_In_Progress().FirstOrDefault().ToString());

            return PartialView("_Widget_Runners", count);
        }
        
        public ActionResult Widget_Zones()
        {
            int count=Convert.ToInt32(db.SP_Widget_Zones_Covered().FirstOrDefault().ToString());
            
            return PartialView("_Widget_Zones", count);
        }

        public ActionResult Widget_Delivers_Tasks()
        {
            int count = Convert.ToInt32(db.SP_Widget_Tasks().FirstOrDefault().ToString());
            
            return PartialView("_Widget_Delivers_Tasks", count);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}