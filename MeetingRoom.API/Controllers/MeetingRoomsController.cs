using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MeetingRooms_DAL;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using MeetingRoom.API.Models;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MeetingRoom.API.Controllers
{
    public class MeetingRoomsController : ApiController
    {
        Noks_MeetingRoomsEntities db = new Noks_MeetingRoomsEntities();
        //private string cpanel_url = "http://23.97.137.147:8087"; //"http://192.168.1.200:8035/";
        private string cpanel_url = "http://10.96.62.250";//change for Emaar local server
        [HttpGet]
        [Route("MeetingRooms/FilterRooms")]
        public dynamic FilterRooms_ByRengeDate(Nullable<DateTime> from, Nullable<DateTime> to)
        {
            var meetingRooms = db.SP_Search_Meeting_Rooms(null, 0, false, false, false, false, false, false, false, false, false, from, to, false)
                .Select(m => new { Id = m.Id, Name = m.Name }).ToList();
            if (meetingRooms == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Meeting Rooms found")),
                };
                throw new HttpResponseException(response);
            }
            return meetingRooms;
        }
        [HttpGet]
        [Route("MeetingRooms/MeetingRoom_DigitalSignange")]
        public dynamic MeetingRoom_DigitalSignange(int id)
        {
            var data = db.SP_Select_MeetingRoom_DigitalSignange(id).FirstOrDefault();
            if (data == null)
                return NotFound();
            return Ok(data);
        }
        [HttpGet]
        [Route("MeetingRooms/GetMeetingRooms")]
        public dynamic GetMeetingRooms()
        {
            var data = db.SP_Select_MeetingRooms().ToList();
            if (data == null)
                return NotFound();
            return Ok(data);
        }
        private static string code = "";
        [HttpGet]
        [Route("MeetingRooms/ConfirmReservation")]
        public void Confirm_Reservation([FromUri] string created_by, [FromUri] string code, [FromUri] string attendees_list,
             [FromUri]string subject, [FromUri]string description)
        {
            db.SP_Insert_Reservations_Person_And_Attendees(code, DateTime.Now, created_by, attendees_list);
            db.SP_Update_Reservation_Confirmation(code, subject, description);
            BroadcastData();
        }
        public async void BroadcastData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(cpanel_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("MeetingRooms/BroadCastData");
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
        [HttpGet]
        [Route("MeetingRooms/NewReservation")]
        public dynamic New_Reservation([FromUri] string created_by, [FromUri] Nullable<DateTime> from,
            [FromUri] Nullable<DateTime> to, [FromUri]Nullable<int> meetingroom_Id)
        {
            code = db.SP_Insert_Reservations_And_Attendees(created_by, from, to, null, null, null, meetingroom_Id, "Reservation Not Confirmed Yet", "Reservation Not Confirmed Yet", false).FirstOrDefault();
            // BroadcastData();
            return code;
        }
        [HttpGet]
        [Route("MeetingRooms/Verivication")]
        public dynamic Verivication([FromUri]string DecryptText)
        {
            VerificationInfo info = new VerificationInfo();
            info = Decrypt(DecryptText);
            return info;
        }

        private VerificationInfo Decrypt(string DecryptText)
        {
            VerificationInfo VerificationInfo = new VerificationInfo();
            try
            {
                string EncryptText = "";
                string EncryptionKey = Helper.EncryptionKey;
                DecryptText = Regex.Replace(DecryptText, @"\s", "");
                byte[] cipherBytes = Convert.FromBase64String(DecryptText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
                    );
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        EncryptText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                String[] info = EncryptText.Split(',');

                if (info[0].Length > 0)
                    VerificationInfo.UserName = info[0];
                else
                    VerificationInfo.UserName = "Invalid Parameter";

                if (info[1].Length > 0)
                    VerificationInfo.Password = info[1];
                else
                    VerificationInfo.Password = "Invalid Parameter";

                if (info[2].Length > 0)
                    VerificationInfo.Company = info[2];
                else
                    VerificationInfo.Company = "Invalid Parameter";
                if (info[0].Length > 0 && info[1].Length > 0 && info[2].Length > 0)
                {
                    VerificationInfo.Result = 1;
                    VerificationInfo.Message = "Text Derypted Successfully";
                }
                else
                {
                    VerificationInfo.Result = -1;
                    VerificationInfo.Message = "Invalid Text";
                }
                return VerificationInfo;
            }
            catch
            {
                VerificationInfo.UserName = "Invalid Parameter";
                VerificationInfo.Password = "Invalid Parameter";
                VerificationInfo.Company = "Invalid Parameter";
                VerificationInfo.Result = -1;
                VerificationInfo.Message = "Invalid Text";
                return VerificationInfo;
            }
        }

        [HttpGet]
        [Route("MeetingRooms/DigitalSignage")]
        public dynamic digitalsignage([FromUri]int roomId, [FromUri]string verficationcode)
        {
            digitalsignage model = new digitalsignage();
            model.intervalTimes = new List<status>();
            model.intervalTimes.Add(new status { interval = "09:00 AM to 10:00 AM", text = "Avaialble" });
            model.intervalTimes.Add(new status { interval = "10:00 AM to 11:30 AM", text = "Occupied" });
            model.intervalTimes.Add(new status { interval = "11:30 AM to 12:40 AM", text = "Avaialble" });
            model.intervalTimes.Add(new status { interval = "12:40 AM to 13:30 AM", text = "Occupied" });
            model.intervalTimes.Add(new status { interval = "13:30 AM to 15:30 AM", text = "Occupied" });
            model.intervalTimes.Add(new status { interval = "15:30 AM to 16:15 AM", text = "Occupied" });
            model.intervalTimes.Add(new status { interval = "16:15 AM to 17:00 AM", text = "Avaialble" });

            model.Room = new Room();
            model.Room.Id = 1;
            model.Room.Name = "Room A";
            model.Background = Helper.Domain + "/Uploads/meetingrooms/0b1f44e6-6503-4ffa-a2b0-710445724070.jpg";
            model.status = new status { interval = "10:00 AM to 11:30 AM", text = "Occupied" };

            return model;
        }
        [HttpGet]
        [Route("MeetingRooms/SendEmail")]
        public dynamic Send_Emails([FromUri] string subject, [FromUri] string body, [FromUri]string recievers_list)
        {
            try
            {
                recievers_list = Regex.Replace(recievers_list, @"\s", "");
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Settings.Email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                string[] ToMuliId = recievers_list.Split(';');
                foreach (string toMail in ToMuliId)
                {
                    mailMessage.To.Add(new MailAddress(toMail));
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Settings.SMTP_SERVER;
                smtp.EnableSsl = bool.Parse(Settings.SSL);
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = Settings.User_Name;
                NetworkCred.Password = Settings.Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(Settings.Port);
                smtp.Send(mailMessage);
                return "Message Sent Successfully";
            }
            catch
            {
                return "Message not Sent Yet";
            }
        }


        [HttpGet]
        [Route("MeetingRooms/GetMeetingRoomReservationsToday")]
        public dynamic Get_MeetingRoom_ReservationAndAttendees_In_CurrentTime_ById_And_TodayReservations(int id)
        {
            Digital_Signage_Information NewInfo = new Digital_Signage_Information();
            //GET meeting Room Details
            SP_Select_MeetingRoom_DigitalSignange_Result meetingroom_header = db.SP_Select_MeetingRoom_DigitalSignange(id).FirstOrDefault();
            NewInfo.Background_URL = meetingroom_header.Background_URL;
            NewInfo.MeetingRoom_Title = meetingroom_header.MeetingRoom_Title;
            NewInfo.MeetingRoom_Valid = meetingroom_header.MeetingRoom_Valid;
            //Get Meeting Rooms Status
            SP_Select_MeetingRoom_Reservation_And_Users_In_CurrentTime_ById_And_Today_Reservations_Result meetingroom_data = db.SP_Select_MeetingRoom_Reservation_And_Users_In_CurrentTime_ById_And_Today_Reservations(id).FirstOrDefault();
            if(meetingroom_data == null)
            {
                NewInfo.CurrentMeetingStatus = "Available";
            }
            else
            {
                NewInfo.CurrentMeetingDescription = meetingroom_data.CurrentMeetingDescription;
                NewInfo.CurrentMeetingDuration = meetingroom_data.CurrentMeetingDuration;
                NewInfo.CurrentMeetingLeft = meetingroom_data.CurrentMeetingLeft;
                NewInfo.CurrentMeetingStatus = meetingroom_data.CurrentMeetingStatus;
                NewInfo.CurrentMeetingTitle = meetingroom_data.CurrentMeetingTitle;
            }
            
            //Get Meeting Room , Events
            NewInfo.TodayMeetings = new List<Controllers.MeetingRoomsController.Meetings>();
            List<SP_DigitalSignage_Calendar_Result> events_list = db.SP_DigitalSignage_Calendar(id).ToList();
            foreach (SP_DigitalSignage_Calendar_Result e in events_list)
            {
                Meetings new_meeting = new Meetings { Duration = e.Start_From.ToShortTimeString() + "-" + e.End_At.ToShortTimeString(), Name = e.Subject };
                NewInfo.TodayMeetings.Add(new_meeting);
            }
            //Get Current Users 
            NewInfo.CurrentUsers = new List<Controllers.MeetingRoomsController.Attendees>();
            List<SP_DigitalSignage_Current_Attendees_Result> attendees = db.SP_DigitalSignage_Current_Attendees(id).ToList();
            foreach (SP_DigitalSignage_Current_Attendees_Result a in attendees)
            {
                NewInfo.CurrentUsers.Add(new Controllers.MeetingRoomsController.Attendees { ImgUrl = a.ImgUrl, Name = a.Name });
            }
            if (NewInfo == null)
                return NotFound();
            return Ok(NewInfo);
        }

        public class Digital_Signage_Information
        {
            public string MeetingRoom_Title { get; set; }
            public bool MeetingRoom_Valid { get; set; }
            public string Background_URL { get; set; }
            public string CurrentMeetingStatus { get; set; } // Busy or Available
            public string CurrentMeetingTitle { get; set; }
            public string CurrentMeetingDuration { get; set; }
            public string CurrentMeetingLeft { get; set; }
            public string CurrentMeetingDescription { get; set; }
            public List<Attendees> CurrentUsers { get; set; }
            public string TodayDate { get { return DateTime.Now.ToString("dd-MM-yyyy"); } }
            public string TodayTime { get { return DateTime.Now.ToString("hh:mm:ss"); } }
            public List<Meetings> TodayMeetings { get; set; }
            public int Interval_Seconds { get {
                    int _interval = 15;
                    Noks_MeetingRoomsEntities db = new Noks_MeetingRoomsEntities();
                    string value = db.SP_Settings_Select_ByKey_AndGroup("DG", "DG_Interval_Sec").FirstOrDefault();
                    if(value != "" && !string.IsNullOrEmpty(value))
                    {
                        try
                        {
                            _interval = Convert.ToInt32(value);
                        }
                        catch
                        {
                            _interval = 15;
                        }
                    }
                    return _interval; }
            }
        }
        public class Attendees
        {
            public string Name { get; set; }
            public string ImgUrl { get; set; }
        }
        public class Meetings
        {
            public string Name { get; set; }
            public string Duration { get; set; }
        }
    }
}

