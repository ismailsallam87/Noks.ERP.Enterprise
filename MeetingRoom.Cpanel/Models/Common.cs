using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingRoom.Cpanel.Models
{
    public class Common
    {
    }
    public class db_trns_result
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string advanced { get; set; }
    }
    public enum view_types
    {
        Table, Grid, Calendar, Timeline
    }
    public class CreateNew_ViewModel{
        public string Title { get; set; }
        public string EntityTitle { get; set; }
        public string Message { get; set; }
        public string Target { get; set; }
        public string ButtonName { get; set; }
    }
}