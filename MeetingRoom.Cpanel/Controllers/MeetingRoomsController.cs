using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeetingRooms_DAL;
using Microsoft.AspNet.Identity;
using MeetingRoom.Cpanel.Models;
using MeetingRoom.Cpanel.Hubs;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace MeetingRoom.Cpanel.Controllers
{
    public class MeetingRoomsController : Controller
    {
        private Noks_MeetingRoomsEntities db = new Noks_MeetingRoomsEntities();
        #region MeetingRoom_Actions
        // GET: MeetingRooms
        public ActionResult Index()
        {
            return View();
        }
        public string Load_Photo(string photo, int? Id)
        {
            string defualt = "../../uploads/meetingrooms/no_background.jpg";
            string photo_path = Server.MapPath(photo.Replace("../", "").Replace("../", ""));
            if (System.IO.File.Exists(photo_path))
            {
                if (photo_path.Contains("mp4"))
                {
                    return defualt;
                }
                else
                    return photo.Replace("~/", "../../");
            }
            else
            {
                return defualt;
            }
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
                        web_path = Path.Combine("~/Uploads/meetingrooms/", code + ext);

                        var path = Path.Combine(Server.MapPath("~/Uploads/meetingrooms"), code + ext);
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
        public ActionResult DigitalSignage(int? Id)
        {
            ViewBag.Id = Id;
            SP_MeetingRoom_DigitalSignage_Result model = db.SP_MeetingRoom_DigitalSignage(Id).FirstOrDefault();
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return View(model);
            }

        }
        public ActionResult DigitalSignage_Blank(int? Id)
        {
            SP_MeetingRoom_DigitalSignage_Result model = db.SP_MeetingRoom_DigitalSignage(Id).FirstOrDefault();
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                return View(model);
            }

        }
        public PartialViewResult MeetingRooms_List(string Type)
        {
            ViewBag.Type = Type;
            return PartialView("_MeetingRooms_List", db.SP_Search_Meeting_Rooms(null, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList());
        }
        #endregion
        #region Reservations_Actions
        public ActionResult Reservations()
        {
            return View();
        }
        public ActionResult MeetingRoom_Board()
        {
            return View();
        }
        public ActionResult Reservation_Search_List(Nullable<int> room_meeting_Id, string attendees_Name, Nullable<int> host_Id, Nullable<System.DateTime> from_date, Nullable<System.DateTime> from_time, Nullable<System.DateTime> to_date, Nullable<System.DateTime> to_time)
        {
            DateTime? from = null;
            DateTime? to = null;
            if (from_date != null && from_time != null)
            {
                from = new DateTime(from_date.Value.Year, from_date.Value.Month, from_date.Value.Day, from_time.Value.Hour, from_time.Value.Minute, 0);
            }
            
            if(to_date != null && to_time != null)
            {
               to = new DateTime(to_date.Value.Year, to_date.Value.Month, to_date.Value.Day, to_time.Value.Hour, to_time.Value.Minute, 59);
            }

            room_meeting_Id = (room_meeting_Id < 0 ? null : room_meeting_Id);
            host_Id = (host_Id < 0 ? null : host_Id);
            return PartialView("_Reservation_Search_Results", db.SP_Search_Reservation(room_meeting_Id, attendees_Name, host_Id, from, to).ToList());
        }
        public ActionResult Receptionist()
        {
            return RedirectToAction("Reservations");
        }
        public JsonResult Reservations_Count_In_Rooms(Nullable<DateTime> from, Nullable<DateTime> to,
            string department)
        {
            var model = db.SP_Room_Reservations_Count_ByDateRange(from, to, department).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reservations_Hours_Count_In_Rooms(Nullable<DateTime> from, Nullable<DateTime> to,
           string department)
        {
            var model = db.SP_Reservations_Hours_Count_In_Room_ByDateRange(from, to, department).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reservations_Attendees_Count_In_Rooms(Nullable<DateTime> from, Nullable<DateTime> to,
           string department)
        {
            var model = db.SP_Attendees_Count_In_Room_ByDateRange(from, to, department).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult chart_range_list(DateTime? from, DateTime? to, string type, DateTime? selected)
        {
            return PartialView("_chart_range_list", db.SP_reservation_chart_range_list(type, from, to, selected).ToList());
        }
        public PartialViewResult Department_List()
        {
            return PartialView("_Department_List", db.SP_Select_Distinct_Department().ToList());
        }
        
        public JsonResult MeetingRoom_Reservations_On_Current_Day(DateTime? from, DateTime? to)
        {

            return Json(db.SP_Get_MeetingRoom_Reservations_On_Current_Day(DateTime.Now,DateTime.Now).ToList(), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Hosts_DropDown()
        {
            return PartialView("_Hosts_DropDown", db.SP_Get_Hosts().ToList());
        }
        public JsonResult New_Reservation(string created_by, DateTime from, DateTime to, int meetingroom_Id, string subject, string description)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Insert_Reservations_And_Attendees(created_by, from, to, null, null, null, meetingroom_Id, subject, description, true);
                new_result.message = " Reservation inserted successfully";
                new_result.result = true;
                BroadCastData();
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to insert Reservation";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        public void BroadCastData()
        {
            MeetinRoomHub.BroadcastData();
        }
        public PartialViewResult LastReservations_Dashboard()
        {
            return PartialView("_LastReservations_Dashboard",db.SP_Search_Reservation(null,null,null,null,null).ToList());
        }
        public PartialViewResult Canecl_Reservation(int Id)
        {
            return PartialView("_Resevation_Cancellation", Id);
        }
        public JsonResult Confirm_Canecl_Reservation(int Id)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Cancel_Resevation(Id);
                new_result.message = " Reservationis Canceled successfully.";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Cancel Reservation.";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            BroadCastData();
            return Json(new_result, JsonRequestBehavior.AllowGet);         
        }
        #endregion
        #region Setup
        public ActionResult Setup()
        {
            return View("Setup",db.SP_Settings_Select().ToList());
        }
        #endregion
        #region meetingrooms_DAL
        public JsonResult Insert_Meeting_Room(
            string name,
            string description,
            string photo,
            Nullable<int> capacity,
            Nullable<int> overhead_projector,
            Nullable<int> slide_projctor,
            Nullable<int> film_projctor,
            Nullable<int> ifp,
            Nullable<int> video_screen,
            Nullable<int> white_board,
            Nullable<int> microphones,
            Nullable<int> computer,
            Nullable<int> loud_speakers
            )
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Insert_Meeting_Room(name, description, photo, capacity, true, User.Identity.GetUserId(), overhead_projector, slide_projctor, film_projctor, ifp, video_screen, white_board, microphones, computer, loud_speakers);
                new_result.message = name + " is inserted successfully on the database, now you can use it on reservation module.";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to insert " + name + " into database, please re-try to  use it on reservation module.";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DigitalSignage_Calendar(int Id)
        {
            return PartialView("_DigitalSignage_Calendar", db.SP_DigitalSignage_Calendar(Id).ToList());
        }
        public PartialViewResult Search_Meeting_Rooms(
            string name,
            Nullable<int> capacity,
            Nullable<bool> overhead_projector,
            Nullable<bool> slide_projector,
            Nullable<bool> film_projector,
            Nullable<bool> ifp,
            Nullable<bool> video_screen,
            Nullable<bool> white_board,
            Nullable<bool> microphones,
            Nullable<bool> computer,
            Nullable<bool> loud_speaker,
            Nullable<System.DateTime> from,
            Nullable<System.DateTime> to,
            view_types v_type
            )
        {
            string view = "_MeetingRoom_Calendar";
            if (v_type == view_types.Calendar) view = "_MeetingRoom_Calendar";
            else if (v_type == view_types.Grid) view = "_MeetingRoom_Grid";
            else if (v_type == view_types.Table) view = "_Meetingroom_Table";

            bool? checked_b = true;
            bool? unchecked_b = null;

            var model = db.SP_Search_Meeting_Rooms(name, capacity, (overhead_projector == null || overhead_projector == false ? unchecked_b : checked_b),
                (slide_projector == null || slide_projector == false ? unchecked_b : checked_b), (film_projector == null || film_projector == false ? unchecked_b : checked_b),
                (film_projector == null || film_projector == false ? unchecked_b : checked_b), (video_screen == null || video_screen == false ? unchecked_b : checked_b),
                (white_board == null || white_board == false ? unchecked_b : checked_b), (microphones == null || microphones == false ? unchecked_b : checked_b), (computer == null || computer == false ? unchecked_b : checked_b)
                , (loud_speaker == null || loud_speaker == false ? unchecked_b : checked_b), from, to, null).ToList();
            return PartialView(view,model);
        }
        [Authorize]
        public ActionResult Checkin(string Id)
        {
            db.SP_Update_Reservation(null, null, null, null, DateTime.Now, null, null, null, null, null);
            return View();
        }
        public JsonResult Fetch_Meeting_Rooms_Reservations(int index, string attendees_Name, int room_meeting_Id,
            Nullable<int> host_Id, Nullable<System.DateTime> from, Nullable<System.DateTime> to)
        {
            var model = db.SP_Search_Reservation_Scroll(room_meeting_Id, attendees_Name, host_Id, index, from, to).ToList();
            //index++;        
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Load_Timeline_Meeting_Rooms_Reservations(int index, string attendees_Name, int room_meeting_Id,
            Nullable<int> host_Id, Nullable<System.DateTime> from, Nullable<System.DateTime> to, view_types v_type)
        {
            return PartialView("_MeetingRoom_Reservations_Timeline", db.SP_Search_Reservation_Scroll(room_meeting_Id, attendees_Name, host_Id, index, from, to).ToList());
        }

        public JsonResult Search_Meeting_Rooms_Reservation_Calendar(
           string attendees_Name,
           Nullable<int> room_meeting_Id,
           Nullable<int> host_Id,
            Nullable<System.DateTime> from,
            Nullable<System.DateTime> to
           )
        {
            var model = db.SP_Search_Reservation(room_meeting_Id, attendees_Name, host_Id, from, to).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int Id)
        {
            TempData["ID"] = Id;
            if (Id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(Id);
        }
        public PartialViewResult Update_MeetingRoom(int Id)
        {
            var model = db.SP_Get_Meeting_Rooms_By_Id(Id).FirstOrDefault();
            return PartialView("_MeetingRoom_Update", model);
        }
        public JsonResult Confirm_Update(Nullable<int> Id,
           string name,
           string description,
           string photo,
           Nullable<int> capacity,
           Nullable<int> overhead_projector,
           Nullable<int> slide_projctor,
           Nullable<int> film_projctor,
           Nullable<int> ifp,
           Nullable<int> video_screen,
           Nullable<int> white_board,
           Nullable<int> microphones,
           Nullable<int> computer,
           Nullable<int> loud_speakers
           )
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Update_Meeting_Room(Id, name, description, photo, capacity, true, overhead_projector, slide_projctor, film_projctor, ifp, video_screen, white_board, microphones, computer, loud_speakers);
                new_result.message = name + " is Updated successfully on the database";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Update " + name;
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Delete(int Id)
        {
            return PartialView("_MeetingRoom_Delete", Id);
        }
        public JsonResult Confirm_Delete(int Id)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Delete_Meeting_Room(Id);
                new_result.message = "Meeting Room is Deleted successfully on the database";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Delete ";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region setup
      
        public JsonResult Update_Email_Server(string ssl, string user_name, string password, string port, string smtp, string email)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Update_Email_Settings(email, Settings.Encrypt(password), ssl, user_name, smtp, port);
                new_result.message = "Email Server is Updated successfully...!";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Update ";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update_Reservation_Configuration(string unconfirmed_cancellation, string check_in,string week_start,
            string week_end, string day_start,string day_end)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Update_Reservation_Configuration_Settings(unconfirmed_cancellation, check_in, week_start,week_end, day_start, day_end);
                new_result.message = "Reservation Configuration is Updated successfully...!";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Update ";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update_Localization_Format(string activity_time)
        {
            db_trns_result new_result = new db_trns_result();
            try
            {
                db.SP_Update_Localization_Format_Settings(activity_time);
                new_result.message = "Localization Format is Updated successfully...!";
                new_result.result = true;
            }
            catch (Exception e)
            {
                new_result.message = "an error occurred while trying to Update ";
                new_result.advanced = e.Message;
                new_result.result = false;
            }
            return Json(new_result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public string GenerateQrcode(string qrcode)
        {
            string QRCodeImage = "";
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);             
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return QRCodeImage;
        }
    }
    #region models
    public class meetingroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    #endregion
}