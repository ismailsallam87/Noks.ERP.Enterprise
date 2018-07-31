using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTS.DAL;
namespace Nox_OTS.Controllers
{
    public class RunnersController : Controller
    {
        private OTSEntities db = new OTSEntities();
        // GET: Runners
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Runner_Select(string id, string email, string userName, string phoneNumber)
        {
            List<Runner_Select_Result> list = db.Runner_Select((id.Trim() == "" ||string.IsNullOrEmpty(id)?null:id), (email.Trim() == "" || string.IsNullOrEmpty(email) ? null : email), (userName.Trim() == "" || string.IsNullOrEmpty(userName) ? null : userName), (phoneNumber.Trim() == "" || string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber)).ToList();
            return PartialView("_Runner_Tbl", list);
        }
        public ActionResult FreeRunners()
        {
            ViewBag.Filter = "FreeRunners";
            return View("Index", null);
        }
    }
}