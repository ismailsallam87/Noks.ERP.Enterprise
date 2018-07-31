using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace ATTMS.Dashboard.Controllers
{
    
    public class LookupsController : Controller
    {
        Noks_ATT_MSEntities db = new Noks_ATT_MSEntities();
        // GET: Lookups
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Update_Job_Title(int Id)
        {
            var model = db.SP_Get_Job_Title_By_Id(Id).FirstOrDefault();
            return PartialView("_Jop_Title_Update", model);
        }
        public ActionResult Confirm_Update(int Id,
        string Title  )
        {
            string message;
                try
            {
                db.SP_Update_Job_Title(Id, Title);
             message = Title + " is Updated successfully on the database";
            }
            catch (Exception e)
            {
                 message = "an error occurred while trying to Update " + Title;
             }
            return Content(message);
        }
        public JsonResult Job_Title_Create(string title)
        {
            return Json(db.SP_Job_Title_Create(title).ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Job_title_Data_Table()
        {
            List<Sp_Select_Job_Title_Result> job_title = db.Sp_Select_Job_Title().ToList();

             
            return PartialView("_Job_title_Data_Table", job_title);
        }

    }
}