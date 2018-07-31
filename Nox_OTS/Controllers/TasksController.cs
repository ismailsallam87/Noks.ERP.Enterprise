using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTS.DAL;
using Microsoft.AspNet.Identity;
using Nox_OTS.Models;
using System.Data.Entity.Validation;
using static Nox_OTS.Models.Item_Weight;
using System.Data.OleDb;
using System.Data;
using LinqToExcel;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using GenCode128;

namespace Nox_OTS.Controllers
{
    public class TasksController : Controller
    {
        private OTSEntities db = new OTSEntities();
        // GET: Tasks  
        public JsonResult All_Status()
        {
            var model = "";// db.SP_All_Status().ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Area_Project_Status_Filter(string AreaIds, string ProjectIds  ,string statusIds)
        {
            var model = db.sp_Tasks_Opened_Now((AreaIds == null || string.IsNullOrEmpty(AreaIds) ? null : AreaIds), ProjectIds == null || string.IsNullOrEmpty(ProjectIds) ? null : ProjectIds, statusIds == null || string.IsNullOrEmpty(statusIds) ? null : statusIds).ToList();

            return PartialView("_Area_Project_Status_filter",model);
        }

        public JsonResult Kanban_Data(string AreaIds, string ProjectIds, string statusIds)

        {
            //var model = db.SP_Kanban_Data().ToList();
            
            var model = db.sp_Tasks_Opened_Now((AreaIds == null || string.IsNullOrEmpty(AreaIds) ? null : AreaIds), ProjectIds == null || string.IsNullOrEmpty(ProjectIds) ? null : ProjectIds, statusIds == null || string.IsNullOrEmpty(statusIds) ? null : statusIds).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Map_View()
        {
            return View();
        }
        public JsonResult Get_Areas_For_Map_Marker(string AreaIds, string ProjectIds, string statusIds)
        {
            var model = db.sp_Tasks_Opened_Now((AreaIds == null || string.IsNullOrEmpty(AreaIds) ?null:AreaIds), ProjectIds==null||string.IsNullOrEmpty(ProjectIds)?null:ProjectIds,statusIds==null||string.IsNullOrEmpty(statusIds)?null:statusIds).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Table_View()
        {
            var model = db.sp_Tasks_Opened_Now(null,null,null).ToList();
            return View("Table_View",model);
        }
        public ActionResult Kanban_View()
        {
            return View();
        }      
        public ActionResult Task_Status_This_Day()
        {
            return View();
        }
        public ActionResult Dyn_Tasks_Status()
        {
            return View();
        }
        public PartialViewResult Update(int Id)
        {
            var model = db.SP_Select_Task_By_TaskID(Id).FirstOrDefault();
            return PartialView("_Update_Task", model);
        }
        public string Confirm_Update(int Id,string Task_Title, DateTime DueDate, string Task_External_Ref, string Task_Internal_Ref,
            int project, int area, string Task_Authorized_Person, string ShippingAddress, string Task_Authorized_Phone,
            string Task_Authorized_Email, string Task_Account_Name)
        {
            var task = db.Tasks.FirstOrDefault(t => t.ID == Id);
            task.Due_Date = DueDate;
            task.Project_ID = project;
            task.Task_Area = area;
            task.Task_Account_Name = Task_Account_Name;
            task.Task_Authorized_Email = Task_Authorized_Email;
            task.Task_Authorized_Person = Task_Authorized_Person;
            task.Task_Authorized_Phone = Task_Authorized_Phone; 
            task.Task_Shipping_Address = ShippingAddress;
            task.Task_External_Ref = Task_External_Ref;
            task.Task_Internal_Ref = Task_Internal_Ref;
            task.Task_Title = Task_Title;
            try
            {
                db.SaveChanges();
                return "A new Task Updated Successfully.";
            }
            catch 
            {
                return "An error occurred while trying to uodate Task !";
            }
         }
        public PartialViewResult Delete(int Id)
        {
            return PartialView("_Task_Delete", Id);
        }
        public JsonResult Confirm_Delete(int Id)
        {
            string message = "";
           try
            {
                db.SP_Delete_Task(Id);
                message= "Task is Deleted successfully on the database";
            }
            catch (Exception e)
            {
                message = "an error occurred while trying to Delete Task";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Task_Select(
            Nullable<long> iD, 
            Nullable<int> project_ID, 
            string task_Title, 
            string task_External_Ref, 
            string task_Internal_Ref, 
            Nullable<bool> task_Is_Closed, 
            string task_Created_By, 
            string task_Account_Name, 
            Nullable<int> task_Area, 
            Nullable<System.DateTime> due_Date_From, 
            Nullable<System.DateTime> due_Date_To, 
            string task_Shipping_Address, 
            string task_Authorized_Person, 
            string task_Authorized_Email, 
            string task_Authorized_Mobile, 
            Nullable<bool> task_Deleted,
            string view)
        {
            string partial_view = "_Tasks_GridView";
            if (view == "Grid")
            {
                partial_view = "_Tasks_GridView";
            }
            else if (view == "Table")
            {
                partial_view = "_Tasks_TableView";
            }

            iD = (iD <= 0 ? null : iD);
            project_ID = (project_ID <= 0 ? null : project_ID);
            task_Area = (task_Area <= 0 ? null : task_Area);

            List<Task_Select_Result> Task_tbl = db.Task_Select(
                iD, project_ID, 
                (string.IsNullOrEmpty(task_Title) || task_Title.Trim() == "" ? null : task_Title), 
                (string.IsNullOrEmpty(task_External_Ref) || task_External_Ref.Trim() == "" ? null : task_External_Ref), 
                (string.IsNullOrEmpty(task_Internal_Ref) || task_Internal_Ref.Trim() == "" ? null : task_Internal_Ref), 
                task_Is_Closed, 
                (string.IsNullOrEmpty(task_Created_By) || task_Created_By.Trim() == "" ? null : task_Created_By), 
                (string.IsNullOrEmpty(task_Account_Name) || task_Account_Name.Trim() == "" ? null : task_Account_Name), task_Area, due_Date_From, due_Date_To, 
                (string.IsNullOrEmpty(task_Shipping_Address) || task_Shipping_Address.Trim() == "" ? null : task_Shipping_Address), 
                (string.IsNullOrEmpty(task_Authorized_Person) || task_Authorized_Person.Trim() == "" ? null : task_Authorized_Person), 
                (string.IsNullOrEmpty(task_Authorized_Email) || task_Authorized_Email.Trim() == "" ? null : task_Authorized_Email), 
                (string.IsNullOrEmpty(task_Authorized_Mobile) || task_Authorized_Mobile.Trim() == "" ? null : task_Authorized_Mobile), task_Deleted).ToList();

            return PartialView(partial_view, Task_tbl);
        }
         public PartialViewResult Task_Filter(Nullable<int> project,Nullable<int> status, string accountName, Nullable<int> area, 
             Nullable<System.DateTime> from, Nullable<System.DateTime> to, string view)
        {
            string partial_view = "_Tasks_GridView";
            if (view == "Grid")
            {
                partial_view = "_Tasks_GridView";
            }
            else if (view == "Table")
            {
                partial_view = "_Tasks_TableView";
            }           
            project = (project <= 0 ? null : project);
            area = (area <= 0 ? null : area);
            status = (status <= 0 ? null : status);
            List<Task_Select_Result> Task_tbl = db.Task_Select(null,project,null,null,null,null,null,
                (string.IsNullOrEmpty(accountName) || accountName.Trim() == "" ? null : accountName),area,from,to,
                 null,null,null,null,null).ToList();
            return PartialView(partial_view, Task_tbl);
        }

        public ActionResult Packing_Slip()
        {
            return View();
        }
        public ActionResult PackingSlip(string taskIds, string statusIds)
        {
            var tasks = db.SP_Select_Tasks_By_StatusId_And_TaskIds(statusIds, taskIds).ToList();
            List<PackingSlipViewModel> model = new List<PackingSlipViewModel>();
            foreach (var task in tasks)
            {
                PackingSlipViewModel newTask = new PackingSlipViewModel();
                newTask.Task = db.SP_Select_Task_By_TaskID(task.ID).FirstOrDefault();
                newTask.TaskItems = db.SP_Select_TaskItems_By_TaskID(task.ID).ToList();

                if(newTask.Task.Task_Is_Closed)
                {
                    newTask.IsClosed = "Closed";
                }
                else
                {
                    newTask.IsClosed = "Opened";
                }
                string txtWeight = "2";
                const int MaxLength = 10;
                // Genarate barcode 
                Image myimg = Code128Rendering.MakeBarcodeImage(newTask.Task.ID.ToString(), int.Parse(txtWeight), true);
                var imgName = newTask.Task.Task_Internal_Ref;
                if (imgName.Length > MaxLength)
                {
                    imgName = imgName.Substring(0, MaxLength);  // Image name trim  
                }
                // save image in folder location different type (.bmp/ .png / .jpeg)  
                string ImageFolder = Server.MapPath("~/Images");
                myimg.Save(ImageFolder + "/" + imgName.Trim() + ".bmp");

                //image to displa in view  
                var virtualPath = string.Format("~/Images/{0}.bmp", imgName.Trim());
                ViewBag.Path = virtualPath;
                newTask.BarcodeImage = virtualPath;
                model.Add(newTask);
            }
            //return PartialView("_Invoice", model);
           return View(model);
        }
        public PartialViewResult PackingSlipForTask(string taskIds)
        {
            var tasks = db.SP_Select_Tasks_By_StatusId_And_TaskIds("", taskIds).ToList();
            List<PackingSlipViewModel> model = new List<PackingSlipViewModel>();
            foreach (var task in tasks)
            {
                PackingSlipViewModel newTask = new PackingSlipViewModel();
                newTask.Task = db.SP_Select_Task_By_TaskID(task.ID).FirstOrDefault();
                newTask.TaskItems = db.SP_Select_TaskItems_By_TaskID(task.ID).ToList();

                if (newTask.Task.Task_Is_Closed)
                {
                    newTask.IsClosed = "Closed";
                }
                else
                {
                    newTask.IsClosed = "Opened";
                }
                string txtWeight = "2";
                const int MaxLength = 10;
                // Genarate barcode 
                Image myimg = Code128Rendering.MakeBarcodeImage(newTask.Task.ID.ToString(), int.Parse(txtWeight), true);
                var imgName = newTask.Task.Task_Internal_Ref;
                if (imgName.Length > MaxLength)
                {
                    imgName = imgName.Substring(0, MaxLength);  // Image name trim  
                }
                // save image in folder location different type (.bmp/ .png / .jpeg)  
                string ImageFolder = Server.MapPath("~/Images");
                myimg.Save(ImageFolder + "/" + imgName.Trim() + ".bmp");

                //image to displa in view  
                var virtualPath = string.Format("~/Images/{0}.bmp", imgName.Trim());
                ViewBag.Path = virtualPath;
                newTask.BarcodeImage = virtualPath;
                model.Add(newTask);
            }
            return PartialView("_Invoice", model);
            //return View(model);
        }
        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase Template,string recieved_email, string recieved_from,
           DateTime recieved_date, string bulk_name)
        {
            List<OTS.DAL.Task> data = new List<OTS.DAL.Task>();
            if (Template != null)
            {
                if (Template.ContentType == "application/vnd.ms-excel" || Template.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = Template.FileName;
                    string targetpath = Server.MapPath("~/Content/Docs/");
                    Template.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        //connectionString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathToExcelFile + ";Extended Properties=Excel 12.0;";
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }
                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");
                    System.Data.DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var TaskSheet = from a in excelFile.Worksheet<Models.Tasks>(sheetName) select a;

                    Bulk_Tasks bulk_task = new Bulk_Tasks();
                    bulk_task.Bulk_Name = bulk_name;
                    bulk_task.Bulk_Project = 1;
                    bulk_task.Bulk_Received_By_Email = recieved_email;
                    bulk_task.Bulk_Received_By_Name = recieved_from;
                    bulk_task.Bulk_Received_Date = DateTime.Now.AddDays(-1);
                    bulk_task.Bulk_Total_Tasks = TaskSheet.Count();
                    bulk_task.Bulk_Uploaded_By = User.Identity.Name;
                    bulk_task.Bulk_Uploaded_Date = recieved_date;
                    bulk_task.Sync = false;
                    db.Bulk_Tasks.Add(bulk_task);
                    db.SaveChanges();
                    //TaskSheet = Ta;
                    //foreach (var a in TaskSheet)
                    //{
                    int new_task_count = 0;
                    string wieght = Item_Weights.Weight.ToString();
                    for (int i = 1; i < dtable.Rows.Count; i++)
                    {
                        try
                        {
                            string date = dtable.Rows[i][1].ToString();
                            string area = dtable.Rows[i][4].ToString();
                            // tasks.Add(new Tasks { Date = DateTime.Parse(dtable.Rows[i][1].ToString()), AccountName = dt.Rows[i][2].ToString() });

                            Guid guid = Guid.NewGuid();
                            OTS.DAL.Task task = new OTS.DAL.Task();
                            var task_area = db.Areas.Where(r => r.Title == area).Select(r => r.ID).FirstOrDefault();
                            if (task_area <= 0)
                                task.Task_Area = 1;
                            else
                                task.Task_Area = task_area;

                            task.Deleted = false;
                            task.Due_Date = DateTime.Parse(date).AddDays(1);
                            task.Project_ID = 1;
                            task.Task_Authorized_Phone = dtable.Rows[i][6].ToString();
                            task.Task_Account_Name = dtable.Rows[i][2].ToString();//a.AccountName;
                            task.Task_Title = db.get_task_title().FirstOrDefault();
                            task.Task_Shipping_Address = dtable.Rows[i][3].ToString();//a.Address;
                            task.Task_Is_Closed = false;
                            task.Task_Internal_Ref = guid.ToString();
                            task.Task_External_Ref = dtable.Rows[i][8].ToString(); //a.Order;
                            task.Task_Created_By = User.Identity.GetUserId();
                            task.Task_Authorized_Person = dtable.Rows[i][5].ToString();//a.AuthorizedName;
                            task.Task_Authorized_Email = "";
                            //task.Description = dtable.Rows[i][10].ToString();
                            string order = dtable.Rows[i][8].ToString();
                            var task_isfound = db.Tasks.Where(t => t.Task_External_Ref == order).Select(t => t).FirstOrDefault();
                            if (task_isfound == null)
                            {
                                db.Tasks.Add(task);
                                new_task_count++;
                                data.Add(task);
                                db.SaveChanges();
                                Task_Items task_items = new Task_Items();
                                task_items.Task_ID = task.ID;
                                task_items.Item_SN = dtable.Rows[i][7].ToString();
                                task_items.Item_Title = dtable.Rows[i][10].ToString();
                                task_items.Item_Desc = "";
                                task_items.Item_Weight = "1Kg";//Item_Weights.Weight.ToString() + "Kg";
                                task_items.Deleted = false;
                                task_items.Created_At = DateTime.Now;
                                task_items.Created_By = User.Identity.GetUserId();
                                db.Task_Items.Add(task_items);
                                db.SaveChanges();
                            }
                            else
                            { }
                    }
                        catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
                    var bulk = db.Bulk_Tasks.Select(b => b).OrderByDescending(b => b.ID).FirstOrDefault();
                    bulk.Sync = true;
                    bulk.Sync_At = DateTime.Now;
                    bulk.Bulk_New_Tasks = new_task_count;
                    db.Entry(bulk).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");   //data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Unfinished()
        {
            ViewBag.Filter = "Unfinished";
            return View("Index", null);
        }
        public ActionResult Delivered()
        {
            ViewBag.Filter = "Delivered";
            return View("Index", null);
        }
        public ActionResult Bulk()
        {
            return View();
        }
        public string Task_Create(string Task_Title,DateTime DueDate,string Task_External_Ref,string Task_Internal_Ref,
            int project,int area,string Task_Authorized_Person,string ShippingAddress,string Task_Authorized_Phone,
            string Task_Authorized_Email,string Task_Account_Name)
        {
            db.Tasks.Add(new Task
            {
                Due_Date = DueDate,
                Deleted = false,
                Project_ID = project,
                Task_Area = area,
                Task_Account_Name = Task_Account_Name,
                Task_Authorized_Email = Task_Authorized_Email,
                Task_Authorized_Person = Task_Authorized_Person,
                Task_Authorized_Phone = Task_Authorized_Phone,
                Task_Created_By = User.Identity.GetUserId(),
                Task_Is_Closed = false,
                Task_Shipping_Address = ShippingAddress,
                Task_External_Ref = Task_External_Ref,
                Task_Internal_Ref = Task_Internal_Ref,
                Task_Title = Task_Title
            });
            try
            {
                db.SaveChanges();
                return "A new Task Created Successfully.";
            }
            catch(Exception e)
            {

                db.Tasks.Local.Remove(db.Tasks.Local[0]);
                return "An error occurred while trying to save a new Task !";
            }
        }
        public PartialViewResult Load_Task_Status_View(int taskId,bool? isTaskClosed)
        {

            string visible = "visible";
            if (isTaskClosed == null || isTaskClosed == true) 
                visible = "hidden";
            else
                visible = "visible";
            TaskStatusViewModel model = new TaskStatusViewModel();
            model.TaskId = taskId;
            model.IsTaskClosed = visible;
            model.RemainedStatuses = db.SP_Select_Remained_Task_Status_ByTaskId(taskId).ToList();
            model.Users = db.SP_Select_Users().ToList();
            model.Statuses = db.SP_Select_Task_Status_ByTaskId(taskId).ToList();
            return PartialView("_Task_Status_View", model);
        }
        public string Add_Task_Status_ByTaskId(int status_Id, int taskId, DateTime actual_start, string orderd_by, string orderd_to)
        {
            try
            {
                db.SP_Add_Task_Status_ByTaskId(status_Id, taskId, actual_start, User.Identity.GetUserId(), orderd_to);
                return "A new Status Created Successfully.";
            }
            catch (Exception e)
            {

                db.Tasks.Local.Remove(db.Tasks.Local[0]);
                return "An error occurred while trying to save a new Task !";
            }
        }
        public PartialViewResult Loadzone_Img(string title)
        {
            return PartialView("_Zone_Img", title);
        }
        //public ActionResult Task_Items_View(int taskId,string view)
        //{
        //    string partial_view = "_Items_GridView";
        //    if (view == "Grid")
        //    {
        //        partial_view = "_Items_GridView";
        //    }
        //    else if (view == "Table")
        //    {
        //        partial_view = "_Items_TableView";
        //    }

        //    TaskItems model = new Models.TaskItems();
        //    model.Items = db.SP_Select_TaskItems_By_TaskID(taskId).ToList();
        //    model.TaskId = taskId;
        //    return PartialView(partial_view, model);
        //}
        //public ActionResult TaskItems(int taskId)
        //{
        //    TaskItems model = new Models.TaskItems();
        //    model.Items = db.SP_Select_TaskItems_By_TaskID(taskId).ToList();
        //    model.TaskId = taskId;
        //    return View(model);
        //}
        public string Create_Item(byte statusId, string notes, string weight,int taskId, string desc, string itemSn, string title)
        {
            db.Task_Items.Add(new Task_Items
            {
                Task_ID = taskId,
                Item_SN = itemSn,
                Item_Title = title,
                Item_Desc = desc,
                Item_Weight = weight,
                Item_Delivery_Status = statusId,
                Item_Notes = notes,
                Created_By = User.Identity.GetUserId(),
                Created_At = DateTime.Now,
                Deleted = false,
            });
            try
            {
                db.SaveChanges();
                return "A new Item Created Successfully.";
            }
            catch
            {

                db.Task_Items.Local.Remove(db.Task_Items.Local[0]);
                return "An error occurred while trying to save a new Item !";
            }
        }
        public PartialViewResult Update_Item(int Id)
        {
            var model = db.Task_Items.FirstOrDefault(i => i.ID == Id);
            return PartialView("_Update_Item", model);
        }
        public string Confirm_Update_Item(int Id, byte statusId, string notes, string weight, string desc, string itemSn, string title)
        {
            var item = db.Task_Items.FirstOrDefault(t => t.ID == Id);
            item.Item_Delivery_Status = statusId;
            item.Item_Notes = notes;
            item.Item_Weight = weight;
            item.Item_Desc = desc;
            item.Item_SN = itemSn;
            item.Item_Title = title;
            try
            {
                db.SaveChanges();
                return "A new Item Updated Successfully.";
            }
            catch
            {
                return "An error occurred while trying to uodate Item !";
            }
        }
        public PartialViewResult Delete_Item(int Id)
        {
            return PartialView("_Item_Delete", Id);
        }
        public JsonResult Confirm_Delete_Item(int Id)
        {
            string message = "";
            try
            {
                var item = db.Task_Items.FirstOrDefault(i => i.ID == Id);
                item.Deleted = true;
                db.SaveChanges();
                message = "Item is Deleted successfully on the database";
            }
            catch
            {
                message = "an error occurred while trying to Delete Item";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}