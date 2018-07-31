using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTS.DAL;
using System.Security.Cryptography;

namespace Nox_OTS.Controllers
{
    public class UsersController : Controller
    {
        private OTSEntities db = new OTSEntities();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult UserList_Search(string iD, string email, string userName, string role_ID, string View)
        {
            string partial_view = "_Users_GridView";
            if (View == "Grid") { partial_view = "_Users_GridView"; }
            else if (View == "Table") { partial_view = "_Users_TableView"; }

            List<SP_Users_Select_Result> list = db.SP_Users_Select((string.IsNullOrEmpty(iD) || iD.Trim() == "" ? null : iD), (string.IsNullOrEmpty(email) || email.Trim() == "" ? null : email), (string.IsNullOrEmpty(userName) || userName.Trim() == "" ? null : userName), (string.IsNullOrEmpty(role_ID) || role_ID.Trim() == "" ? null : role_ID)).ToList();
            return PartialView(partial_view, list);
        }
        public JsonResult User_Create(string email, string userName, string roles_ID, string password, Nullable<bool> locked)
        {
            string securityStamp = "";
            string HashedPassword = HashPassword(password, ref securityStamp);
            return Json(db.SP_Users_Create(email, email, roles_ID, HashedPassword, securityStamp, locked), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateLockStatus(string userId, bool locked)
        {
            return Json(db.SP_Users_Lock(userId, locked).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public string Roles_MultiChoic_List()
        {
            return db.SP_Roles_UI_Array_Select().FirstOrDefault();
        }
        #region utilities
        public static string HashPassword(string password, ref string SecurityStamp)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            SecurityStamp = Convert.ToBase64String(salt);
            return Convert.ToBase64String(dst);
        }
        public string LoadUser_Img(string userId)
        {
            return "../../assets/img/not-found.png";
        }
        #endregion
    }
}