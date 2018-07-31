using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Noks_Meetingroom_Addons_2013_2016
{
    partial class MeetingRoom_Reservation
    {
        private static string domain = "http://23.97.137.147:8087/";
        //private static string domain = "http://10.96.62.250:8080/"; // for emaar local
        private static List<meetingroom> room_list;
        private static string timeZone = "Egypt Standard Time";

        #region Form Region Factory 
        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass(Microsoft.Office.Tools.Outlook.FormRegionMessageClassAttribute.Appointment)]
        [Microsoft.Office.Tools.Outlook.FormRegionName("Noks_Meetingroom_Addons_2013_2016.MeetingRoom_Reservation")]
        public partial class MeetingRoom_ReservationFactory
        {
            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void MeetingRoom_ReservationFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void MeetingRoom_Reservation_FormRegionShowing(object sender, System.EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now.AddHours(1);
            comboBox1.DataSource = room_list;
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
        }

        private async void fitch_rooms()
        {
            string from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
            string to = dateTimePicker2.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker4.Value.TimeOfDay;
            string url = domain + "/MeetingRooms/FilterRooms?from=" + from + "&to=" + to;          

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
        // Occurs when the form region is closed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void MeetingRoom_Reservation_FormRegionClosed(object sender, System.EventArgs e)
        {
        }
        private static string code = "";
        private static string from = "";
        private static string to = "";
        private static int meetingroom_Id = 0;
        private static string created_by = "";
        private static string subject = "";
        private static string description = "";
        private static string attendees_list = "";
        private static Outlook.AppointmentItem appointment;
 
        private async void button1_Click(object sender, EventArgs e)
        {
            appointment = (Outlook.AppointmentItem)this.OutlookItem;
            created_by = appointment.SendUsingAccount.UserName;            
            ((Outlook.ItemEvents_10_Event)appointment).Send += new Outlook.ItemEvents_10_SendEventHandler(AppointmentItemSendedHandler);
             meetingroom_Id = Convert.ToInt32(comboBox1.SelectedValue);
             from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
             to = dateTimePicker2.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker4.Value.TimeOfDay;
            
            string url = domain + "/MeetingRooms/NewReservation?from=" + from + "&to=" + to + "&created_by=" + created_by +
                "&meetingroom_Id=" + meetingroom_Id ;            
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
            appointment.Location = comboBox1.Text;
            BroadcastData();
            filterroom();
        }
        public async void BroadcastData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("MeetingRooms/BroadCastData");
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
        
        static void AppointmentItemSendedHandler(ref bool isSended)
        {
            description = appointment.Body;
            subject = appointment.Subject;
            attendees_list = appointment.RequiredAttendees+";"+appointment.OptionalAttendees;
            attendees_list = Regex.Replace(attendees_list, @"\s", "");
            created_by = appointment.SendUsingAccount.UserName;
            string url = domain + "/MeetingRooms/ConfirmReservation?&created_by=" + created_by + "&subject=" + subject +
                "&description=" + description+ "&attendees_list=" + attendees_list + "&code="+ code;         
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
            html.AppendLine("Your Meeting Code :" + code);
            return html;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            filterroom();
        }

        private void filterroom()
        {
            from = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker3.Value.TimeOfDay;
            to = dateTimePicker2.Value.Date.ToString("MM/dd/yyyy ") + dateTimePicker4.Value.TimeOfDay;
            if (DateTime.Parse(from) > DateTime.Parse(to))
            {
                const string message = "From Date < To Date";
                const string caption = "Form Closing";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                fitch_rooms();
                if (comboBox1.Items.Count > 0)
                    comboBox1.DataSource = null;
                comboBox1.DataSource = room_list;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Name";
            }
        }
    }
    #region models
    public class filter_room
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
    public class meetingroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    #endregion
}
