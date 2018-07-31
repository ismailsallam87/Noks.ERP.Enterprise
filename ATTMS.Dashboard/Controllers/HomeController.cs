using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Device.Enrollment.Migration;
using ATTMS.Dashboard.Helpers;

namespace ATTMS.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Migration mig = new Migration(Server.MapPath("/uploads/ENROLLDB.ZIP"), Server.MapPath("/uploads/extract_files"));
            //mig.IsZIP();
            //mig.Get_Files();
            //return Json(mig.Extract_Enrollments(),JsonRequestBehavior.AllowGet);

            ////mig.Extract();
            return RedirectToAction("Index", "Dashboard");
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
        public ActionResult SetCulture(string culture, string returnUrl = "/")
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            SetInCookies(culture);
            return Redirect(returnUrl);
        }
        public ActionResult SetCultureIndex(int id)
        {
            // Validate input
            var culture = id==0?"en-US":"ar-SA";
            // Save culture in a cookie
            SetInCookies(culture);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult English()
        {
         return  SetCultureIndex(0);
        }
        public ActionResult Arabic()
        {
            return SetCultureIndex(2);
        }
        private void SetInCookies(string culture)
        {
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture")
                {
                    Value = culture,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
        }

        /*
         * emplement in layout
          @if (culture == "ar")
                            {
                                <li>
                                    <a href="/home/SetCulture?culture=en-US&returnUrl=@Request.Url.AbsoluteUri"> @Resources.Resource.ToEnglish</a>
                                </li>
                            }
                            else
                            {
                                <li>
                                    <a href="/home/SetCulture?culture=ar-EG&returnUrl=@Request.Url.AbsoluteUri"> @Resources.Resource.ToArabic</a>
                                </li>
                            }
         */
        /*
          <li><a href="~/Lang/Arabic">@gloplization.Langs.Resource1.Arabic</a></li>
                   <li><a href="~/Lang/English">@gloplization.Langs.Resource1.English</a></li>
                   */
    }
}