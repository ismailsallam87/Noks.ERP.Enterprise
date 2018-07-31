using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Models;
using System.Net;
using Microsoft.AspNet.Identity;
using ATTMS.Dashboard.Resources;
using ATTMS.Dashboard.Models;

namespace ATTMS.Dashboard.Controllers
{
    public class EmployeesController : Controller
    {
        private Noks_ATT_MSEntities db = new Noks_ATT_MSEntities();
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult Create_Leave_Details(int leave_request_ref_Id,int requested_for_employee_Id, DateTime req_from, DateTime req_to)
        {
            var aspuser = User.Identity.GetUserId();
            //db.SP_Insert_Leave_Details(leave_request_ref_Id, requested_for_employee_Id, req_from, req_to, aspuser);//, null,null , null, null, null, null);              
            
            return Content("Leave  Inserted Successfully");
        }
        public ActionResult Manager_DownList()
        {
            List<SP_select_Employee_Result> Manager = db.SP_select_Employee().ToList();
            return PartialView("_Manager_ddl", Manager);
        }
        public ActionResult coach_DownList()
        {
            List<SP_select_Employee_Result> Coach = db.SP_select_Employee().ToList();
            return PartialView("_coach_ddl", Coach);
        }
        public ActionResult Leave_Ref_Drop_DownList()
        {
            List<SP_Select_Leave_request_ref_Result> Leave_request_ref = db.SP_Select_Leave_request_ref().ToList();
            return PartialView("_Leave_request_ref_ddl", Leave_request_ref);
        }
        public ActionResult Document_Ref_Drop_DownList(int? selectedElement)
        {
            ViewBag.selectedElement = selectedElement;
            List<SP_Select_Document_Reference_Result> Documnet_ref = db.SP_Select_Document_Reference().ToList();
            return PartialView("_Document_Ref_Drop_DownList", Documnet_ref);
        }
        public ActionResult Employee_Drop_DownList()
        {
            List<SP_select_Employee_Result> Employee = db.SP_select_Employee().ToList();
            return PartialView("_Employee_Drop_Down", Employee);
        }
        public ActionResult Department_Drop_DownList()
        {
           List< SP_Select_Departments_Result>  Department= db.SP_Select_Departments().ToList();
            return PartialView("_Department_Drop_Down", Department);
        }
        public ActionResult Contract_Create(int Employee_id, int Job_Title_id, string Employee_Name, string Employee_Mid_Name, string Employee_last_Name, DateTime datapicker2, DateTime datapicker3_Exp_Start
            , DateTime datapicker4, DateTime datapicker5, DateTime datapicker6_Exp_End, Decimal contract_net_salary,
            Decimal contract_basic_salary, Decimal contract_gross_salary, bool chk_terminated, bool chk_renewable)
        {
            string result;
            var aspuser = User.Identity.GetUserId();
            try
            {
                db.SP_Insert_Contract_Details(Employee_id, Job_Title_id, Employee_Name, Employee_Mid_Name, Employee_last_Name,
                    aspuser, datapicker2, datapicker3_Exp_Start, datapicker4, datapicker5, datapicker6_Exp_End, contract_net_salary,
                    contract_basic_salary, contract_gross_salary, chk_terminated, chk_renewable);
                result = "Contracty added Successfully";
            }
            catch (Exception ex)
            {
                string sss = ex.ToString();
                result = "Error Ocurred";
            }
            return Content(""+ result);
        }

        public ActionResult transfer_Employee_Deparment(int? Employee_id, int? Department_id, int? Job_Title_id, int? Coach_Title_id, DateTime Joined_At_dtp, string notes)
        { 
            string result;
            try
            {
                db.SP_Employee_Department_Transfer( Employee_id, Job_Title_id, Joined_At_dtp, notes, Coach_Title_id  );
                result = "Employee Transfared Successfully";
            }
            catch (Exception ex)
            {
                string sss = ex.ToString();
                result = "Error Ocurred";
            }
            return Content("" + result);
        }

        public ActionResult Save_edited_Document(int employee_document_Id, int document_ref_id, DateTime signed_at, DateTime expires_at, string notes,string copy_Path )
        {
            try
            {
                db.SP_Update_Document_Detail(employee_document_Id, 1, signed_at, expires_at, notes, copy_Path);
            }
            catch (Exception ex)
            {
                throw;
            }             
            return Content("Document Inserted Successfully");
        }
        public ActionResult create_Document(int Employee_id , int document_ref_id , DateTime signed_at , DateTime expires_at ,string  notes ,bool expired , string copy_Path ,bool purged ,bool  renwed )
        {
            var aspuser = User.Identity.GetUserId();
            db.SP_Insert_Document_Details(Employee_id,  document_ref_id,  signed_at,  expires_at,   notes,  expired,  copy_Path,  purged,   renwed);             
            return Content("Document Inserted Successfully");
        }      
        //Contract_Create
        public ActionResult Fill_Job_Title(int department_Id,int employee_Id)
        {
            Coach_And_JonTitle_ViewModel Coach_and_Job_Title = new Coach_And_JonTitle_ViewModel();
            Coach_and_Job_Title.Job_Title = db.SP_Select_Job_Title_By_Dept_id(department_Id).ToList();
           Coach_and_Job_Title.Coach = db.SP_select_Coach(department_Id, employee_Id).ToList();
              //List< SP_Select_Job_Title_By_Dept_id_Result> Department = db.SP_Select_Job_Title_By_Dept_id(department_Id).ToList();
               return PartialView("_Job_Title_Drop_Down", Coach_and_Job_Title);
        } 
        public ActionResult Confirm_Delete_Shift(int Id)
        {
            string new_result;
            try
            {
                db.SP_Employee_Delete(Id);
                new_result = " is Deleted successfully on the database";
            }
            catch (Exception e)
            {
                new_result = "an error occurred while trying to Delete ";

            }
            return Content(new_result);
        }
        public PartialViewResult Delete(int Id)
        {
            return PartialView("_Employee_Delete", Id);
        }
        public ActionResult Edit_Employee(int ID)
        {
            SP_Edit_Employee_Result employee = db.SP_Edit_Employee(ID).ToList().FirstOrDefault();
            return PartialView("_Edit_Pop_UP", employee);
        }       
        public ActionResult Edit_Document(int ID)
        {
            SP_Edit_Employee_Document_Result Document = db.SP_Edit_Employee_Document(ID).ToList().FirstOrDefault();
            return PartialView("_Edit_Document_Pop", Document);
        }
        public ActionResult Details(int Id)
        {
            return View(db.SP_Employee_Select(Id,null,null,null,null,null,null).FirstOrDefault());
        }
        public PartialViewResult Employee_List_Select(bool? only_free,int? department_Id,int? selectedEelement)
        {
            ViewBag.selectedEelement = selectedEelement;
            return PartialView("_Employee_List", db.SP_Employee_List_Select(only_free, department_Id,null).ToList());
        }
        public PartialViewResult Employees_Select(string view,string name=null , string photo=null,string code=null,string ssn=null,string mobile=null,string social_insurance_code=null)
        {
            string viewName = "_Employee_Tableview";
            if (view == "grid")
            {
                viewName = "_Employee_Gridview";
            }
            return PartialView(viewName, db.SP_Employee_Select(null,name,photo,code,ssn,mobile,social_insurance_code).ToList());
        }      
        public JsonResult Employee_Create(
            string name, 
            string photo, 
            string code, 
            string user_Id, 
            string ssn, 
            string mobile, 
            string social_insurance_code)
        {
            return Json(db.SP_Employee_Create(name, photo, code, (user_Id == "-1" || user_Id == ""? null : user_Id), ssn, mobile, social_insurance_code).ToList(), JsonRequestBehavior.AllowGet);
        }
      public JsonResult Employee_Edit( string name,string photo,string code,string ssn,string mobile,string social_insurance_code,int id)
       {
            try
            {
                var result = db.SP_Employee_Edit(id, name, photo, code, ssn, mobile, social_insurance_code).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            } 
       }
        public PartialViewResult Employee_Details_Attendance_Sheet(int? Id,DateTime? from,DateTime? to,Nullable<int> monthly_master_ref_Id, string UserId,bool? Is_Current)
        {
            if(Id > 0)
            {
                ViewBag.ListOfEmployees = false;
            }
            else
            {
                Id = null;
                ViewBag.ListOfEmployees = true;
            }
            if (monthly_master_ref_Id <= 0)
                monthly_master_ref_Id = null;

            return PartialView("_Employee_Details_Attendance_Sheet", db.SP_Employee_Attendance_Sheet(from, to, Id, null, Is_Current).ToList());
        }
        public PartialViewResult Employee_Details_Contracts(int Id)
        {
            var contracts = db.SP_Employee_Contracts(Id).Where(c => c.end_at == null).ToList();
            if (contracts.Count == 0)
            {
                ViewBag.Visible = "hidden";
            }
            else
            {
                ViewBag.Visible = "visible";
            }
            return PartialView("_Employee_Details_Contracts", db.SP_Employee_Contracts(Id).ToList());
        }
        public PartialViewResult Employee_Details_Requests(int Id)
        {
            var contracts = db.SP_Employee_Contracts(Id).Where(c => c.end_at == null).ToList();        
            if (contracts.Count == 0)
            {
                ViewBag.Visible = "hidden";
            }
            else
            {
                ViewBag.Visible = "visible";
            }
            var model = db.SP_Employee_Leave_Requests(null, Id).ToList();
            return PartialView("_Employee_Details_Requests",model);
        }
        //Asem Department
        public PartialViewResult Transfer_Employee_Department(int Id)
        {
            var model = new List<SP_Select_Employee_Transfered_Result>() ;
            try
            {
 model = db.SP_Select_Employee_Transfered(Id).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }  

            return PartialView("_Employee_deparment", model);
        }

        public PartialViewResult Employee_Details_Documents(int Id)
        { 
            return PartialView("_Employee_Details_Documents", db.SP_Employee_Documents(Id).ToList());
        }
        public PartialViewResult Terminate_And_End_Contract(int? Id)
        {
            return PartialView("_Terminate_And_End_Contracts", Id);
        }
        public string Confirm_Terminate_And_End_Contract(int Id,DateTime endAt ,bool isTerminate)
        {
            string new_result;
            try
            {
                db.SP_Terminate_And_End_Contract(Id,endAt,isTerminate);
                new_result = "Contract Terminated and Ended successfully on the database";
            }
            catch (Exception e)
            {
                new_result = "an error occurred while trying to Terminate and End Contract ";

            }
            return new_result;
        }
        public string Load_Employee_Photo(string photo, int? Id)
        {
            string defualt = "../../uploads/employee/no_avatar.jpg";

            if (photo ==null)
            { 
                return defualt;
               
            }
            else
            {
                string photo_path = Server.MapPath(photo.Replace("../", "").Replace("../", ""));
                return photo.Replace("~/", "../../");
            }
          
            //if (System.IO.File.Exists(photo_path))
            //{
            //    return photo.Replace("~/", "../../");
            //}
            //else
            //{
            //    return defualt;
            //}
        }
        [HttpPost]
        public JsonResult upload_img()
        {
            try
            {
                string web_path = "";
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string code = Guid.NewGuid().ToString();
                        // get a stream
                        var stream = fileContent.InputStream;
                        string ext = GetDocumentExt(fileContent.FileName);
                        // and optionally write the file to disk
                        web_path = Path.Combine("~/Uploads/employee/", code + ext);

                        var path = Path.Combine(Server.MapPath("~/Uploads/employee"), code+ext);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                return Json(web_path, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Image Upload failed", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult upload_document()
        {
            try
            {
                string web_path = "";
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string code = Guid.NewGuid().ToString();
                        // get a stream
                        var stream = fileContent.InputStream;
                        string ext = GetDocumentExt(fileContent.FileName);
                        // and optionally write the file to disk
                        web_path = Path.Combine("~/Uploads/documents/", code + ext);

                        var path = Path.Combine(Server.MapPath("~/Uploads/documents"), code + ext);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                return Json(web_path, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Image Upload failed", JsonRequestBehavior.AllowGet);
            }
        }
        private string GetDocumentExt(string File)
        {
            if (File.LastIndexOf('.') > 0)
            {
                string Ext = File.Substring(File.LastIndexOf('.'));
                return Ext;
            }
            else
            {
                return "";
            }
        }
    }
}