using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Models;
using Microsoft.AspNet.Identity;
namespace ATTMS.Dashboard.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private Noks_ATT_MSEntities db = new Noks_ATT_MSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pending_Requests()
        {
            var aspuser = User.Identity.GetUserId();
            var model = db.SP_Pending_Requests(aspuser).ToList();
            return PartialView("_Pending_Requests", model);
        }

    }
}
