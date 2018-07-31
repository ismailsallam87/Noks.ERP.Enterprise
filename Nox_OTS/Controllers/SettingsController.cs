using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTS.DAL;
using Microsoft.AspNet.Identity;
namespace Nox_OTS.Controllers
{
    public class SettingsController : Controller
    {
        private OTSEntities db = new OTSEntities();
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Zones()
        {
            return View();
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult CoveredZones()
        {
            ViewBag.Filtered = "Covered";
            return View("Zones", null);
        }
        #region Select_Fn
        public PartialViewResult Zones_Select(Nullable<int> iD, string title,string View)
        {
            string partial_view = "_Zones_GridView";
            if (View == "Grid") { partial_view = "_Zones_GridView"; }
            else if (View == "Table") { partial_view = "_Zones_TableView"; }

            return PartialView(partial_view, db.Zones_Select((iD == null ||iD <= 0 ? null : iD), (string.IsNullOrEmpty(title) || title.Trim() == "" ? null : title)).ToList());
        }
        public string Zone_Create(string Name)
        {
            try
            {
                db.Zones.Add(new Zone { Title = Name });
                db.SaveChanges();
                return "A new Zone Created Successfully.";
            }
            catch
            {

                db.Zones.Local.Remove(db.Zones.Local[0]);
                return "An error occurred while trying to save a new Zone !";
            }
        }
        public string Zone_Edit(int Id,string Name)
        {
            Zone zone = db.Zones.Where(m => m.ID == Id).FirstOrDefault();
            zone.Title = Name;
            try
            {
                db.SaveChanges();
                return "Selected Zone is Updated Successfully.";
            }
            catch
            {

                db.Zones.Local.Remove(db.Zones.Local[0]);
                return "An error occurred while trying to update a selected Zone !";
            }
        }
        public PartialViewResult Areas_Select(int ZoneId)
        {
            ViewBag.ZoneID = ZoneId;
            return PartialView("_Areas", db.Areas.Where(m => m.Zone_ID == ZoneId).ToList());
        }
        public PartialViewResult Areas_Tbl_Select(int ZoneId)
        {
            ViewBag.ZoneID = ZoneId;
            return PartialView("_Areas_Tbl", db.Areas.Where(m => m.Zone_ID == ZoneId).ToList());
        }
        public string Area_Create(int Zone_ID, string AreaName)
        {
            try
            {
                db.Areas.Add(new Area { Zone_ID= Zone_ID,Title = AreaName, });
                db.SaveChanges();
                return "A new area Created Successfully.";
            }
            catch
            {

                db.Areas.Local.Remove(db.Areas.Local[0]);
                return "An error occurred while trying to save a new area !";
            }
        }
        public PartialViewResult Load_Zones_Edit_View(int id)
        {
            return PartialView("_Zone_Edit", db.Zones.Where(m => m.ID == id).FirstOrDefault());
        }
        public PartialViewResult Project_Select(Nullable<int> project_ID, string project_Name, Nullable<System.DateTime> project_Start_At_From, Nullable<System.DateTime> project_End_At_To, string project_Start_By, Nullable<bool> deleted,string View)
        {
            string partial_view = "_Projects_GridView";
            if (View == "Grid") { partial_view = "_Projects_GridView"; }
            else if (View == "Table") { partial_view = "_Projects_TableView"; }
            return PartialView(partial_view, db.Projects_Tbl_Select(project_ID, project_Name, project_Start_At_From, project_End_At_To, project_Start_By, deleted).ToList());
        }
        public PartialViewResult Loadzone_Img (string title)
        {
            return PartialView("_Zone_Img", title);
        }
        public string Areas_Delete(int areaId)
        {
            try
            {
                db.Areas.Remove(db.Areas.Where(m => m.ID == areaId).FirstOrDefault());
                db.SaveChanges();
                return "The Selected area is Deleted Successfully.";
            }
            catch
            {
                return "An error occurred while trying to delete the Selected area !";
            }
        }
        public string Zone_Delete(int ZoneId)
        {
            try
            {
                db.Zones.Remove(db.Zones.Where(m => m.ID == ZoneId).FirstOrDefault());
                db.SaveChanges();
                return "The Selected Zone is Deleted Successfully.";
            }
            catch
            {
                return "An error occurred while trying to delete the Selected Zone !";
            }
        }
        public string Project_Create(string Name, DateTime StartDate)
        {
            try
            {
                db.Projects.Add(new Project { Created_At=DateTime.Now,Project_Start_By=User.Identity.GetUserId(),Deleted=false,Project_Start_Date= StartDate ,Project_Name= Name});
                db.SaveChanges();
                return "A new Prject Created Successfully.";
            }
            catch
            {

                db.Projects.Local.Remove(db.Projects.Local[0]);
                return "An error occurred while trying to save a Project area !";
            }
        }
        #endregion
        #region 
        public ActionResult Intial_Menu(string UserId)
        {
            List<SP_Menu_Parents_Select_Result> parents = db.SP_Menu_Parents_Select(UserId).ToList();
            return PartialView("_Sidebar_Menu", parents);
        }
        public ActionResult Menu_SubElements(int parentId, string UserId)
        {
            List<SP_Menu_Configuration_Select_Result> parents = db.SP_Menu_Configuration_Select(UserId, parentId).ToList();
            return PartialView("_Menu_SubElements", parents);
        }
        #endregion
    }
}