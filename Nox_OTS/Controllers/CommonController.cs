using Microsoft.Office.Interop.Excel;
using Nox_OTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToExcel;
using OTS.DAL;
using System.IO;
using static Nox_OTS.Models.Item_Weight;
using Microsoft.AspNet.Identity;

namespace Nox_OTS.Controllers
{
    public class CommonController : Controller
    {
        OTSEntities db = new OTSEntities();
        // GET: Common
        public PartialViewResult Breedcrumb(string view)
        {
            List<BreedCrumb_Elements> list = new List<BreedCrumb_Elements>();
            if (view == "UserManagement")
            {
                list.Add(new BreedCrumb_Elements { Name = "Dashboard", URL = Url.Action("Index", "Home"), DisplayOrder = list.Count + 1 });
                list.Add(new BreedCrumb_Elements { Name = "User Management", URL = Url.Action("Index", "UserManagement"), DisplayOrder = list.Count + 1 });
            }
            else if (view == "Samples_DataTable")
            {
                list.Add(new BreedCrumb_Elements { Name = "Dashboard", URL = Url.Action("Index", "Home"), DisplayOrder = list.Count + 1 });
                list.Add(new BreedCrumb_Elements { Name = "DataTable Samples", URL = Url.Action("DataTable", "Samples"), DisplayOrder = list.Count + 1 });
            }
            else if (view == "ZoneManagement")
            {
                list.Add(new BreedCrumb_Elements { Name = "Dashboard", URL = Url.Action("Index", "Home"), DisplayOrder = list.Count + 1 });
                list.Add(new BreedCrumb_Elements { Name = "Zones & Areas Management", URL = Url.Action("Zones", "Settings"), DisplayOrder = list.Count + 1 });
            }
            else if (view == "ProjectsManagement")
            {
                list.Add(new BreedCrumb_Elements { Name = "Dashboard", URL = Url.Action("Index", "Home"), DisplayOrder = list.Count + 1 });
                list.Add(new BreedCrumb_Elements { Name = "Projects Management", URL = Url.Action("Projects", "Settings"), DisplayOrder = list.Count + 1 });
            }
            return PartialView("_Breedcrumb", list);
        }

      
    }
}