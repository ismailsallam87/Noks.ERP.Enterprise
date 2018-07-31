using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingRoom.API.Helpers
{
    public class Helper
    {
        public string EncryptionKey
        {
            get
            {
                return "MAKV2SPBNI99212";
            }
        }
        public static string Domain { get { return "http://192.168.1.111:8035"; } }
    }
}