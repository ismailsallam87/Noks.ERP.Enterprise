using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Microsoft.AspNet.Identity.Owin;
using ATTMS.Dashboard.Models;
using Microsoft.AspNet.Identity;

namespace ATTMS.Dashboard.Controllers
{
    public class SetupController : Controller
    {
        private Noks_ATT_MSEntities db = new Noks_ATT_MSEntities();
        public SetupController()
        {               
        }

        public ActionResult Holidays()
        {
            return View();
        }

        public ActionResult Create_Holiday(string Holiday_Name,DateTime Holiday_Start_Date,DateTime Holiday_End_Date)
        {
            string result ;
            try
            {
                //db.SP_Add_Holiday(Holiday_Name, Holiday_Start_Date, Holiday_End_Date);
                result = "Holiday Created Successfully";
            }
            catch (Exception ex)
            {
             string s=   ex.ToString();
                result = "Error Occured";
            }
            return Content(""+ result);
        }
         
        public ActionResult MonthManagement()
        {
            return View();
        }
        public PartialViewResult Select_opened_Month()
        {
            return PartialView("_Month_TableView", db.SP_Select_Opened_Month().FirstOrDefault());
        }
        public PartialViewResult Close_Month(int Id)
        {
            return PartialView("_Month_Close", Id);
        }
        public string Confirm_Close_Month(int Id)
        {
            string message;
            try
            {
                db.SP_Close_Month(Id);
                message = "Month is Closed successfully on the database";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to Close Month ";
            }
            return message;
        }

        public string Confirm_Open_Month(string code,DateTime from,DateTime to)
        {
            string message;
            try
            {
                string created_by = User.Identity.GetUserId();
                db.SP_Open_Month(code, from, to,created_by);
                message = "Month is Opened successfully on the database";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to open Month ";
            }
            return message;
        }
        public ActionResult Requests()
        {
            return View();
        }
        public PartialViewResult Requests_Select(string view)
        {
            string viewName = "_Request_Tableview";
            if (view == "grid")
            {
                viewName = "_Request_Gridview";
            }
            return PartialView(viewName, db.SP_Select_Requests().ToList());
        }
        public JsonResult Approve_Request(int id, bool approved)
        {
            string message;
            bool reject = false;
            try
            {
                if (approved)
                    reject = false;
                else
                    reject = true;
                db.SP_Update_Request_Approvement(id, approved, reject);
                message = "Request is Updated successfully on the database";
            }
            catch 
            {
                message = "an error occurred while trying to Update Request";
            }
            return Json(message,JsonRequestBehavior.AllowGet);
        }
        public ActionResult WorkDays()
        {
            return View();
        }
        public PartialViewResult WorkDays_Select()
        {
            return PartialView("_WorkDays",db.SP_WorkDays_Select().ToList());
        }
        public string Is_Work_Day( string chkids, string ids)
        {
            string message;
            try
            {
                db.SP_Update_Work_days(chkids,ids);
                message = " is Updated successfully...";
            }
            catch (Exception e)
            { 
                message = "an error occurred while trying to Update ";
            }
            return message;
        }
        // GET: Setup
        public ActionResult Lookups()
        {
            return View();
        }
        public ActionResult Change_Status(bool status,int Id)
        {
            return Content("");
        }
        public ActionResult Confirm_Shift_Update(int Id,string Title, Nullable<System.TimeSpan> check_In, Nullable<System.TimeSpan> check_Out)
        {
            string message;
            try
            {
                db.SP_Update_Shift(Id , Title, check_In, check_Out);
                message = Title + " is Updated successfully on the database";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to Update " + Title;
            }
            return Content(message);
        }
        public ActionResult Confirm_Insert_Shift_Days(string days, int shiftId, string ifChecked)
        {
            string message;
            try
            {
                char[] commaSeparator = new char[] {','};
                string[] dayss = days.Split(commaSeparator, StringSplitOptions.None);               
                string[] daysschecked = ifChecked.Split(commaSeparator, StringSplitOptions.None);
                for(int i = 0; i<7; i++)
                {
                    db.SP_Insert_Shift_Days(dayss[i], bool.Parse(daysschecked[i]), shiftId);                   
                }
                message = "Shift Days is Updated successfully on the database";
            }
            catch
            {
                message = "an error occurred while trying to Update Shift Days";
            }
            return Content(message);
        }
        public PartialViewResult Shift_Days(int Id)
        {
            WorkDaysViewModel WorkDaysViewModel = new WorkDaysViewModel();
            WorkDaysViewModel.ShiftId = Id;
            WorkDaysViewModel.DefaultDays = db.SP_WorkDays_Select().ToList();
            WorkDaysViewModel.ShiftDays = db.SP_Select_Shift_Days(Id).ToList();
            return PartialView("_Shift_WorkDays", WorkDaysViewModel);
        }
        public string confirm_Update_Shift_Days(int Id, string chkids, string ids)
        {
            string message;
            try
            {
                db.SP_Update_Shift_Days(Id,chkids, ids);
                message = " is Updated successfully...";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to Update ";
            }
            return message;
        }
        #region departments

        public PartialViewResult Update_Shift(int Id)
        {
            var model = db.SP_Shifts_Select(Id).FirstOrDefault();
            return PartialView("_Shift_Update", model);
        }

        public ActionResult Confirm_Department_Update(int Id, string Departmnet )
        {
            string message;
            try
            {
                db.SP_Update_Department(Id, Departmnet);
                message = Departmnet + " is Updated successfully on the database";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to Update " + Departmnet;
            }
            return Content(message);
        }

        #region departments

        public PartialViewResult Update_Department(int Id)
        {
            var model = db.SP_Department_Select(null,Id).FirstOrDefault();
            return PartialView("_Department_Update", model);
        }
        public ActionResult Departments()
        {
            return View();
        }
        public PartialViewResult Delete(int Id)
        {
            return PartialView("_Shift_Delete", Id);
        }

        public PartialViewResult Delete_Shift(int Id)
        {
            return PartialView("_Department_Delete", Id);
        }


        public ActionResult Confirm_Delete_Shift(int Id)
        {
            string new_result;

            try
            {
                db.SP_Shift_Delete(Id);
                new_result = " is Deleted successfully on the database";
            }
            catch (Exception e)
            {
                new_result = "an error occurred while trying to Delete ";

            }
            return Content(new_result);
        }

        public ActionResult Confirm_Delete(int Id)
        {
            string new_result;

            try
            {
                db.SP_Department_Delete(Id);
                new_result = " is Deleted successfully on the database";
            }                     
            catch (Exception e)
            {
                new_result = "an error occurred while trying to Delete ";
           
            }
            return Content(new_result);
        }
        public PartialViewResult Departments_Select(string title, string view)
        {
            string viewName = "_Department_TableView";
            if (view == "grid")
            {
                viewName = "_Department_Gridview";
            }
           
                return PartialView(viewName, db.SP_Department_Select((title == null || title.Trim() == "" ? null : title), null).ToList());

           
      
        }
        public JsonResult Department_Create(string title)
        {
            return Json(db.SP_Department_Create(title).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Department_Details(int Id)
        {
            return View(db.SP_Department_Select(null, Id).FirstOrDefault());
        }
        public PartialViewResult Department_JobTitles_Select(int Id)
        {
            return PartialView("_Department_JobTitles", db.SP_Department_JobTitles_Select(Id).ToList());
        }
        public PartialViewResult Department_Employees_Select(int Id)
        {
            return PartialView("_Department_Employees", db.SP_Department_Employees_Select(Id).ToList());
        }
        public JsonResult Department_JobTitle_Create(
            Nullable<int> department_Id,
            string code,
            Nullable<int> job_title_Id,
            Nullable<bool> is_manager,
            Nullable<bool> multiple_available)
        {
            return Json(db.SP_Department_JobTitle_Create(department_Id, code, job_title_Id, is_manager, multiple_available).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Department_employee_Create(
       Nullable<int> department_Id,
       Nullable<int> department_job_ref,
       Nullable<int> employee_Id,
       Nullable<System.DateTime> joined_at,
       string notes,
       Nullable<int> employee_coach_Id)
        {
            return Json(db.SP_Department_employee_Create(department_Id, department_job_ref, employee_Id, joined_at, notes, employee_coach_Id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Job_Titles_List_Select(int? department_Id)
        {
            return PartialView("_JobTitles_List", db.SP_JobTitle_List_Select(department_Id).ToList());
        }
        public PartialViewResult load_jobtitles_for_department(int department_Id)
        {
            return PartialView("_Department_Jobtitle_List", db.SP_Department_JobTitles_Select(department_Id).ToList());
        }
        #endregion
        #region Shifts
        public ActionResult Shifts()
        {
            return View();
        }
        public PartialViewResult Shifts_Select(string view)
        {
            string viewName = "_Shifts_TableView";
            if (view == "grid")
            {
                viewName = "_Shifts_Gridview";
            }
            return PartialView(viewName, db.SP_Shifts_Select(null).ToList());
        }
        public JsonResult Shift_Create(
            string title,
            Nullable<System.TimeSpan> check_in,
            Nullable<System.TimeSpan> check_out)
        {
            int workedhours = Convert.ToInt16(check_out.Value.TotalHours - check_in.Value.TotalHours);
            if (workedhours < 0)
            {
                workedhours = 24 + workedhours;
            }
            return Json(db.SP_Shift_Create(title, check_in, check_out, workedhours).ToList(), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Shift_Employees(int Id)
        {
            ViewBag.shiftId = Id;
            return PartialView("_shiftEmployees_Modal", db.SP_Employees_For_Shift().ToList());
        }
        public ActionResult Shift_Details(int Id)
        {
            ViewBag.RuleTypeList = db.SP_Shift_Rule_Type_List().ToList();
            return View(db.SP_Shifts_Select(Id).FirstOrDefault());
        }
        public PartialViewResult Shift_Details_Rules(int Id)
        {
            return PartialView("_Shift_Details_Rules", db.SP_Shift_Rules(Id).ToList());
        }
        public JsonResult Join_Shift(int emp_id, int shift_id, int day, int month, int year)
        {
            DateTime join_date = new DateTime(year, month + 1, day);
            return Json(db.SP_Shift_Employee_Join(emp_id, shift_id, join_date).ToList(), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Shift_employee_list(int Id)
        {
            return PartialView("_Shift_Details_Employees", db.SP_Employee_List_Select(null, null, Id).ToList());
        }
        public PartialViewResult Shift_Stat_Details(int Id, TimeSpan check_in, TimeSpan check_out)
        {
            ViewBag.CheckIn = check_in;
            ViewBag.CheckOut = check_out;
            return PartialView("_Shift_Details_Stats", db.SP_Shift_Stat_Details(Id).FirstOrDefault());
        }
        public JsonResult Submit_Shift_Rule(
            string title,
            Nullable<System.TimeSpan> range_from,
            Nullable<System.TimeSpan> range_to,
            Nullable<int> rate,
            string color,
            Nullable<int> type_Id,
            Nullable<int> shift_Id)
        {
            return Json(db.SP_Shift_Rule_Submit(title, range_from, range_to, rate, color, type_Id, shift_Id).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult WorkingHours_Shift_Graph(int ShiftId)
        {
            //var months = new[] {
            //    "January","February","March","April"
            //};
            string months = "[" + '"' + "jan" + '"' + ", " + '"' + "feb" + '"' + ", " + '"' + "mar" + '"' + ", " + '"' + "apr" + '"' + "]";
            string data = "[17, 21, 19, 24]";
            var json_data = new {
                months = months,
                data = data
            };
            return Json(json_data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Attendance
        public PartialViewResult monthly_master_ref_List(bool? Is_Current)
        {
            return PartialView("_monthly_master_ref_List", db.SP_monthly_master_ref_List(Is_Current).ToList());
        }
        public JsonResult Range_Of_master_record(int? monthly_master_ref_Id)
        {
            SP_monthly_master_ref_List_Result selected_record;
            master_range_date selected_range = new master_range_date();
            if (monthly_master_ref_Id != null && monthly_master_ref_Id > 0)
            {
                selected_record = db.SP_monthly_master_ref_List(null).ToList().Where(m => m.Id == monthly_master_ref_Id).FirstOrDefault();
            }
            else
            {
                selected_record = db.SP_monthly_master_ref_List(true).ToList().FirstOrDefault();
            }

            if (selected_record != null)
            {
                selected_range.from = selected_record.start_from.ToString(HMTLHelperExtensions.Attendance_date_format);
                selected_range.to = selected_record.end_at.ToString(HMTLHelperExtensions.Attendance_date_format);
            }
            else
            {
                DateTime start_on = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime end_at = start_on.AddMonths(1);
                selected_range.from = start_on.ToString(HMTLHelperExtensions.Attendance_date_format);
                selected_range.to = end_at.ToString(HMTLHelperExtensions.Attendance_date_format);
            }
            return Json(selected_range, JsonRequestBehavior.AllowGet);
        }
        public class master_range_date
        {
            public string from { get; set; }
            public string to { get; set; }
        }
        #endregion
        #region Devices
        public ActionResult Devices()
        {
            return View();
        }
        public PartialViewResult Devices_Select(string view)
        {
            string viewName = "_Devices_TableView";
            if (view == "grid")
            {
                viewName = "_Devices_Gridview";
            }
            try
            {
                var reslt = db.SP_Devices_Select().ToList();
                return PartialView(viewName, reslt);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public JsonResult Device_Create(string Device_Name,int Device_No,int Device_ID,string Device_IP,string Device_Port,string Device_Password)
        {
            return Json(db.SP_Device_Create(Device_Name, Device_No, Device_ID, Device_IP, Device_Port, Device_Password).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        public JsonResult tableJsonData()
        {

            // Creat basic JSON object with data for table
            var tableData = new[]
            {
                new [] { "Tiger Nixon", "System Architect", "Edinburgh", "61", "2011/04/25", "$320,800" },
                new [] { "Garrett Winters", "Accountant", "Tokyo", "63", "2011/07/25", "$170,750" },
                new [] { "Ashton Cox", "Junior Technical Author", "San Francisco", "66", "2009/01/12", "$86,000" },
                new [] { "Cedric Kelly", "Senior Javascript Developer", "Edinburgh", "22", "2012/03/29", "$433,060" },
                new [] { "Airi Satou", "Accountant", "Tokyo", "33", "2008/11/28", "$162,700" },
                new [] { "Brielle Williamson", "Integration Specialist", "New York", "61", "2012/12/02", "$372,000" },
                new [] { "Herrod Chandler", "Sales Assistant", "San Francisco", "59", "2012/08/06", "$137,500" },
                new [] { "Rhona Davidson", "Integration Specialist", "Tokyo", "55", "2010/10/14", "$327,900" },
                new [] { "Colleen Hurst", "Javascript Developer", "San Francisco", "39", "2009/09/15", "$205,500" },
                new [] { "Sonya Frost", "Software Engineer", "Edinburgh", "23", "2008/12/13", "$103,600" },
                new [] { "Jena Gaines", "Office Manager", "London", "30", "2008/12/19", "$90,560" },
                new [] { "Quinn Flynn", "Support Lead", "Edinburgh", "22", "2013/03/03", "$342,000" }
            };

            var dataTable =
                new { data = tableData }
            ;

            // Return JSON object
            return Json(dataTable, JsonRequestBehavior.AllowGet);

        }
    }
}
#endregion