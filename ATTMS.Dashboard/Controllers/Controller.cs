
using ATTMS.Dashboard.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ATTMS.Dashboard.Controllers
{
    public class Controller : System.Web.Mvc.Controller
    {

        #region Public properies

        protected enum NotificationType
        {
            Quotation=1,
            Feedback=2,
            JobTApply=3
        }
        //public string CompanyArName { get { return db.SettingTbls.Single(s => s.Set_name == "ReportArH1").Set_Value; } }
        //public string CompanyArSection { get { return db.SettingTbls.Single(s => s.Set_name == "ReportArH2").Set_Value; } }
        //public string CompanyEnName { get { return db.SettingTbls.Single(s => s.Set_name == "ReportEnH1").Set_Value; } }
        //public string CompanyEnSection { get { return db.SettingTbls.Single(s => s.Set_name == "ReportEnH2").Set_Value; } }
        //public string BranchArName { get { return db.SettingTbls.Single(s => s.Set_name == "ReportArH3").Set_Value; } }
        //public string BranchEnName { get { return db.SettingTbls.Single(s => s.Set_name == "ReportEnH3").Set_Value; } }


        public string Language { get { return CultureHelper.GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name); } }
        public CultureInfo CurrentCulture { get { return Thread.CurrentThread.CurrentCulture; } }
        /// <summary>
        /// Gets current ASP logged user ID
        /// </summary>
      

        #endregion

        /// <summary>
        /// Changing culture
        /// </summary>
        /// <param name="callback">Current callback</param>
        /// <param name="state">Current state</param>
        /// <returns>System.Web.Mvc.Controller.BeginExecuteCore() function</returns>
      
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures  
            System.Globalization.CultureInfo ci = CultureInfo.CreateSpecificCulture(cultureName);// new System.Globalization.CultureInfo("ar-EG", false);

            DateTimeFormatInfo d = new DateTimeFormatInfo();
            d.DateSeparator = " ";
            d.FullDateTimePattern = "dd MMM yyy HH:mm:ss tt";
            d.TimeSeparator = ":";
            d.LongTimePattern = "HH:mm:ss tt";

            ci.DateTimeFormat = d;

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            var call = ci.OptionalCalendars;

            return base.BeginExecuteCore(callback, state);
        }
    }
}