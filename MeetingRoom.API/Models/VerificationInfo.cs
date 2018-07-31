using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingRoom.API.Models
{
    public class VerificationInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
    }
}