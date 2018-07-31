using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingRoom.API;

namespace MeetingRoom.API.Models
{
    public class digitalsignage
    {
        public string Logo { get { return Helper.Domain+ "/Images/Logo.png"; } }
        public string Background { get; set; }
        public Room Room { get; set; }
        public status status { get; set; }
        public List<status> intervalTimes { get; set; }
    }
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get { return "#FF5733"; } }
        public string Font { get { return "Arial"; } }
    }
    public class status
    {
        public string color { get { return "#F73603"; } }
        public string text { get; set; }//Ocuppied , Available
        public string interval { get; set; } //start_from to end_at
    }
}