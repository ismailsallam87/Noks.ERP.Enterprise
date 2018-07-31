using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

namespace Noks_Meetingroom_Addons_2013_2016
{
    public partial class Booking_frm : Form  
    {
        #region members
        private static string code = "";
        private static string from = "";
        private static string to = "";
        private static int meetingroom_Id = 0;
        private static string created_by = "";
        private static string subject = "";
        private static string description = "";
        private static string attendees_list = "";
        private static Microsoft.Office.Interop.Outlook.AppointmentItem appointment;
        private static string domain = "http://localhost/MeetingRoom.API/";
        private static List<meetingroom> room_list;
        private static string timeZone = "Egypt Standard Time";
        private static bool Projector = false;
        private static bool Screen = false;
        private static bool IFP = false;
        private static bool Whiteboard = false;
        private static int capacity = 0;
        private static string meetingroom_name = "";
        #endregion
        public Booking_frm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Booking_frm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now.AddHours(1);
            capacity_txt.Text = "1";
            checkedListBox1.Items.Add("Projector", false);
            checkedListBox1.Items.Add("Screen", false);
            checkedListBox1.Items.Add("IFP", false);
            checkedListBox1.Items.Add("Whiteboard", false);

            meeting_room_btn_1.Hide();
            meeting_room_btn_2.Hide();
            meeting_room_btn_3.Hide();
            meeting_room_btn_4.Hide();
            meeting_room_btn_5.Hide();
            meeting_room_btn_6.Hide();
            meeting_room_btn_7.Hide();
            meeting_room_btn_8.Hide();
            meeting_room_btn_9.Hide();
            meeting_room_btn_10.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            meeting_room_btn_1.Hide();
            meeting_room_btn_2.Hide();
            meeting_room_btn_3.Hide();
            meeting_room_btn_4.Hide();
            meeting_room_btn_5.Hide();
            meeting_room_btn_6.Hide();
            meeting_room_btn_7.Hide();
            meeting_room_btn_8.Hide();
            meeting_room_btn_9.Hide();
            meeting_room_btn_10.Hide();

            from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker2.Value.TimeOfDay;
            to = dateTimePicker3.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
            Projector = checkedListBox1.CheckedItems.Contains("Projector");
            Screen = checkedListBox1.CheckedItems.Contains("Screen");
            IFP = checkedListBox1.CheckedItems.Contains("IFP");
            Whiteboard = checkedListBox1.CheckedItems.Contains("Whiteboard");
            try
            {
                capacity = Convert.ToInt32(capacity_txt.Text);
            }
            catch
            {
                capacity_txt.Text = "1";
                capacity = Convert.ToInt32(capacity_txt.Text);
            }
            if (DateTime.Parse(from) > DateTime.Parse(to))
            {
                const string message = "Please select a logical date range, that the From is less than To";
                const string caption = "Error on Dates";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                fitch_rooms();
                for(int i=0;i < room_list.Count;i++)
                {
                    if(i == 0)
                    {
                        meeting_room_btn_1.Text = room_list[i].Name;
                        meeting_room_btn_1.Show();
                    }
                    else if (i == 1)
                    {
                        meeting_room_btn_2.Text = room_list[i].Name;
                        meeting_room_btn_2.Show();
                    }
                    else if (i == 2)
                    {
                        meeting_room_btn_3.Text = room_list[i].Name;
                        meeting_room_btn_3.Show();
                    }
                    else if (i == 3)
                    {
                        meeting_room_btn_4.Text = room_list[i].Name;
                        meeting_room_btn_4.Show();
                    }
                    else if (i == 4)
                    {
                        meeting_room_btn_5.Text = room_list[i].Name;
                        meeting_room_btn_5.Show();
                    }
                    else if (i == 5)
                    {
                        meeting_room_btn_6.Text = room_list[i].Name;
                        meeting_room_btn_6.Show();
                    }
                    else if (i == 6)
                    {
                        meeting_room_btn_7.Text = room_list[i].Name;
                        meeting_room_btn_7.Show();
                    }
                    else if (i == 7)
                    {
                        meeting_room_btn_8.Text = room_list[i].Name;
                        meeting_room_btn_8.Show();
                    }
                    else if (i == 8)
                    {
                        meeting_room_btn_9.Text = room_list[i].Name;
                        meeting_room_btn_9.Show();
                    }
                    else if (i == 9)
                    {
                        meeting_room_btn_10.Text = room_list[i].Name;
                        meeting_room_btn_10.Show();
                    }

                }
            }
        }
        private async void fitch_rooms()
        {
            string from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
            string to = dateTimePicker2.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker4.Value.TimeOfDay;
            string url = domain + "/MeetingRooms/FilterRooms?from=" + from + "&to=" + to + "&Projector=" + Projector + "&white_board=" + Whiteboard + "&video_screen=" + Screen + "&ifp=" + IFP + "&capacity=" + capacity;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                room_list = new List<meetingroom>();
                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;
                    // by calling .Result you are synchronously reading the result
                    room_list = await response.Content.ReadAsAsync<List<meetingroom>>();
                }
            }
        }

        private void meeting_room_btn_1_Click(object sender, EventArgs e)
        {
            Button btn =(Button)sender;
            meetingroom_name = btn.Text;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Outlook.Application outlookApplication = new Microsoft.Office.Interop.Outlook.Application(); ;
            Microsoft.Office.Interop.Outlook.AppointmentItem appointment = (Microsoft.Office.Interop.Outlook.AppointmentItem)outlookApplication.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olAppointmentItem);
            created_by = appointment.SendUsingAccount.UserName;
            ((Microsoft.Office.Interop.Outlook.ItemEvents_10_Event)appointment).Send += new Microsoft.Office.Interop.Outlook.ItemEvents_10_SendEventHandler(MailItemSendedHandler);
            meetingroom_Id = Convert.ToInt32(meetingroom_name);
            from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
            to = dateTimePicker2.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker4.Value.TimeOfDay;

            string url = domain + "/MeetingRooms/NewReservation?from=" + from + "&to=" + to + "&created_by=" + created_by +
                "&meetingroom_Id=" + meetingroom_Id;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;
                    // by calling .Result you are synchronously reading the result
                    code = await response.Content.ReadAsAsync<string>();
                }
            }
            StringBuilder html = new StringBuilder();
            html = buildhtml();
            TimeSpan ConvertZoneDays = new TimeSpan(2, 0, 0);
            appointment.Body = html.ToString();
            appointment.Start = DateTime.Parse(dateTimePicker1.Value.Date.ToString("MM/dd/yyyy"));
            appointment.End = DateTime.Parse(dateTimePicker2.Value.Date.ToString("MM/dd/yyyy"));
            TimeZoneInfo newTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            appointment.StartUTC = TimeZoneInfo.ConvertTime(DateTime.Parse(from), newTimeZone);
            appointment.StartUTC = appointment.StartUTC.Subtract(ConvertZoneDays);
            appointment.EndUTC = TimeZoneInfo.ConvertTime(DateTime.Parse(to), newTimeZone);
            appointment.EndUTC = appointment.EndUTC.Subtract(ConvertZoneDays);
            appointment.Location = meetingroom_name;
        }
        static void MailItemSendedHandler(ref bool isSended)
        {
            description = appointment.Body;
            subject = appointment.Subject;
            attendees_list = appointment.RequiredAttendees + ";" + appointment.OptionalAttendees;
            //attendees_list = Regex.Replace(attendees_list, @"\s", "");
            created_by = appointment.SendUsingAccount.UserName;
            string url = domain + "/MeetingRooms/ConfirmReservation?&created_by=" + created_by + "&subject=" + subject +
                "&description=" + description + "&attendees_list=" + attendees_list + "&code=" + code;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
        private StringBuilder buildhtml()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("This's is just a demonstration to provide the client that we can reserve a meeting room from outlook");
            html.AppendLine("By using Noks Smart Meeting room Plugins on Outlook or Chrome,");
            html.AppendLine("You can search and reserve your available meeting rooms");
            html.AppendLine("<img src='http://www.noktechnology.com/images/logo.png' />");
            html.AppendLine("for any further information just call us: +201068589694");
            html.AppendLine("Code:" + code);
            return html;
        }
    }
}
