using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Device.Encoder.Data
{
    public class UserInfo
    {
        public uint Id;
        public UserLevel Level;
        public bool Enabled;
        public string Name;
        public string Department;
        public List<byte[]> finegers;
        public uint? Card;
        public uint? Password;

        public UserInfo()
        {

        }
        public UserInfo(XElement u)
        {
            Id = uint.Parse((from n in u.Elements() where n.Name == "id" select n).FirstOrDefault().Value); //Already validated
            Level = UserLevel.User;
            var e = from n in u.Elements() where n.Name == "level" select n;
            if (e.Count() > 0)
            {
                string s = e.FirstOrDefault().Value;
                if (String.Compare(s, "manager", true) == 0)
                    Level = UserLevel.Manager;
                else if (String.Compare(s, "super-manager", true) == 0)
                    Level = UserLevel.SuperManager;
            }

            Enabled = true;
            e = from n in u.Elements() where n.Name == "enabled" select n;
            if (e.Count() > 0)
            {
                string s = e.FirstOrDefault().Value;
                int n;
                if (int.TryParse(s, out n))
                    Enabled = n != 0;
            }

            Name = null;
            e = from n in u.Elements() where n.Name == "name" select n;
            if (e.Count() > 0)
                Name = e.FirstOrDefault().Value;

            Department = null;
            e = from n in u.Elements() where n.Name == "department" select n;
            if (e.Count() > 0)
                Department = e.FirstOrDefault().Value;

            finegers = new List<byte[]>();
            e = from n in u.Elements() where n.Name == "finger" select n;
            if (e.Count() > 0)
            {
                foreach (var ee in e)
                {
                    byte[] data = Convert.FromBase64String(ee.Value);
                    if (data.Length == (1404 + 12))
                        finegers.Add(data);
                }
            }

            Card = null;
            e = from n in u.Elements() where n.Name == "card" select n;
            if (e.Count() > 0)
            {
                string s = e.FirstOrDefault().Value;
                uint n;
                if (uint.TryParse(s, out n))
                {
                    Card = n;
                }
            }

            Password = null;
            e = from n in u.Elements() where n.Name == "password" select n;
            if (e.Count() > 0)
            {
                string s = e.FirstOrDefault().Value;

                uint dwPwd = 0;
                int nShift = 0;
                foreach (var c in s)
                {
                    dwPwd |= ((uint)((int)c - (int)'0' + 1) & 0x000F) << nShift;
                }

                Password = dwPwd;
            }
        }
    }
    public class GLogInfo
    {
        public int tmno;
        public int smno, seno;
        public int vmode;
        public int yr, mon, day, hr, min, sec;

        public string photo { get { return (tmno == -1) ? "No Photo" : Convert.ToString(tmno); } }
        public int enroll { get { return seno; } }
        public int machine { get { return smno; } }

        public string verify_mode
        {
            get
            {
                string attend_status = "";
                switch ((vmode >> 8) & 0xFF)
                {
                    case 0: attend_status = "_DutyOn"; break;
                    case 1: attend_status = "_DutyOff"; break;
                    case 2: attend_status = "_OverOn"; break;
                    case 3: attend_status = "_OverOff"; break;
                    case 4: attend_status = "_GoIn"; break;
                    case 5: attend_status = "_GoOut"; break;
                }

                string antipass = "";
                switch ((vmode >> 16) & 0xFFFF)
                {
                    case 1: antipass = "(AP_In)"; break;
                    case 3: antipass = "(AP_Out)"; break;
                }


                int vm = vmode & 0xFF;
                string str = "--";
                switch (vm)
                {
                    case 1: str = "Fp"; break;
                    case 2: str = "Password"; break;
                    case 3: str = "Card"; break;
                    case 4: str = "FP+Card"; break;
                    case 5: str = "FP+Pwd"; break;
                    case 6: str = "Card+Pwd"; break;
                    case 7: str = "FP+Card+Pwd"; break;
                    case 10: str = "Hand Lock"; break;
                    case 11: str = "Prog Lock"; break;
                    case 12: str = "Prog Open"; break;
                    case 13: str = "Prog Close"; break;
                    case 14: str = "Auto Recover"; break;
                    case 20: str = "Lock Over"; break;
                    case 21: str = "Illegal Open"; break;
                    case 22: str = "Duress alarm"; break;
                    case 23: str = "Tamper detect"; break;
                    case 51: str = "Fp"; break;
                    case 52: str = "Password"; break;
                    case 53: str = "Card"; break;
                    case 101: str = "Fp"; break;
                    case 102: str = "Password"; break;
                    case 103: str = "Card"; break;
                    case 151: str = "Fp"; break;
                    case 152: str = "Password"; break;
                    case 153: str = "Card"; break;
                }

                if ((1 <= vm && vm <= 7) ||
                    (51 <= vm && vm <= 53) ||
                    (101 <= vm && vm <= 103) ||
                    (151 <= vm && vm <= 153))
                {
                    str = str + attend_status;
                }

                str += antipass;

                return str;
            }
        }

        public string logtime { get { return string.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}", yr, mon, day, hr, min, sec); } }
    }
}
