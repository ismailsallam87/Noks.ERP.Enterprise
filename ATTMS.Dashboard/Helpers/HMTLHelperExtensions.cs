using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ATTMS.Dashboard
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
        public static string currency { get { return "EGP"; } }
        public static string currency_format { get { return "{0:C}"; } }
        public static string datepicker_format { get { return "MM-dd-yyyy"; } }
        public static string date_format { get { return "dd-MM-yyyy"; } }
        public static string Attendance_date_format { get { return "dddd dd-MMMM-yyyy"; } }
        

    }
}

