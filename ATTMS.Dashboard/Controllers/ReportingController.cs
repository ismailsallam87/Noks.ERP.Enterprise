using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace ATTMS.Dashboard.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AttendanceSheet()
        {
            return View();
        }
    }
}