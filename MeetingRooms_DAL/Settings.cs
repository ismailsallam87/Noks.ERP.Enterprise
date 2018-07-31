
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingRooms_DAL;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class Settings
{
    private static Noks_MeetingRoomsEntities db = new Noks_MeetingRoomsEntities();
    private static string _SSL;
    public static string SSL
    {
        get
        {
            //if (_SSL == null)
            //{
                _SSL = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "SSL").FirstOrDefault());
                return _SSL;
            //}
            //else
            //{
            //    return _SSL;
            //}
        }
        set
        {
            if (_SSL != value)
            {
                _SSL = value;
                db.SP_Settings_Update("SSL", "Emailing", _SSL);
            }
        }
    }

    private static string _Email;
    public static string Email
    {
        get
        {
            //if (_Email == null)
            //{
                _Email = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "Email").FirstOrDefault());
                return _Email;
            //}
            //else
            //{
            //    return _Email;
            //}
        }
        set
        {
            if (_Email != value)
            {
                _Email = value;
                db.SP_Settings_Update("Email", "Emailing", _Email);
            }
        }
    }

    private static string _Password;
    public static string Password
    {
        get
        {
            //if (_Password == null)
            //{
                _Password = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "Password").FirstOrDefault());
                return Decrypt(_Password);
            //}
            //else
            //{
            //    return Decrypt(_Password);
            //}
        }
        set
        {
            if (_Password != value)
            {
                _Password = value;
                db.SP_Settings_Update("Password", "Emailing", Encrypt(_Password));
            }
        }
    }

    private static string _Port;
    public static string Port
    {
        get
        {
            //if (_Port == null)
            //{
                _Port = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "Port").FirstOrDefault());
                return _Port;
            //}
            //else
            //{
            //    return _Port;
            //}
        }
        set
        {
            if (_Port != value)
            {
                _Port = value;
                db.SP_Settings_Update("Port", "Emailing", _Port);
            }
        }
    }

    private static string _SMTP_SERVER;
    public static string SMTP_SERVER
    {
        get
        {
            //if (_SMTP_SERVER == null)
            //{
                _SMTP_SERVER = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "SMTP_SERVER").FirstOrDefault());
                return _SMTP_SERVER;
            //}
            //else
            //{
            //    return _SMTP_SERVER;
            //}
        }
        set
        {
            if (_SMTP_SERVER != value)
            {
                _SMTP_SERVER = value;
                db.SP_Settings_Update("SMTP_SERVER", "Emailing", _SMTP_SERVER);
            }
        }
    }

    private static string _User_Name;
    public static string User_Name
    {
        get
        {
            //if (_User_Name == null)
            //{
                _User_Name = (db.SP_Settings_Select_ByKey_AndGroup("Emailing", "User_Name").FirstOrDefault());
                return _User_Name;
            //}
            //else
            //{
            //    return _User_Name;
            //}
        }
        set
        {
            if (_User_Name != value)
            {
                _User_Name = value;
                db.SP_Settings_Update("User_Name", "Emailing", _User_Name);
            }
        }
    }

    private static string _Activity_Time;
    public static string Activity_Time
    {
        get
        {
            //if (_Activity_Time == null)
            //{
                _Activity_Time = (db.SP_Settings_Select_ByKey_AndGroup("ReservationConfig", "Activity_Time").FirstOrDefault());
                return _Activity_Time;
            //}
            //else
            //{
            //    return _Activity_Time;
            //}
        }
        set
        {
            if (_Activity_Time != value)
            {
                _Activity_Time = value;
                db.SP_Settings_Update("Activity_Time", "ReservationConfig", _Activity_Time);
            }
        }
    }

    private static string _Check_In_Policy;
    public static string Check_In_Policy
    {
        get
        {
            //if (_Check_In_Policy == null)
            //{
                _Check_In_Policy = (db.SP_Settings_Select_ByKey_AndGroup("ReservationConfig", "Check_In_Policy").FirstOrDefault());
                return _Check_In_Policy;
            //}
            //else
            //{
            //    return _Check_In_Policy;
            //}
        }
        set
        {
            if (_Check_In_Policy != value)
            {
                _Check_In_Policy = value;
                db.SP_Settings_Update("Check_In_Policy", "ReservationConfig", _Check_In_Policy);
            }
        }
    }

    private static string _UnConfirmed_Cancellation;
    public static string UnConfirmed_Cancellation
    {
        get
        {
            //if (_UnConfirmed_Cancellation == null)
            //{
                _UnConfirmed_Cancellation = (db.SP_Settings_Select_ByKey_AndGroup("ReservationConfig", "UnConfirmed_Cancellation").FirstOrDefault());
                return _UnConfirmed_Cancellation;
            //}
            //else
            //{
            //    return _UnConfirmed_Cancellation;
            //}
        }
        set
        {
            if (_UnConfirmed_Cancellation != value)
            {
                _UnConfirmed_Cancellation = value;
                db.SP_Settings_Update("UnConfirmed_Cancellation", "ReservationConfig", _UnConfirmed_Cancellation);
            }
        }
    }

    private static string _API_URL;
    public static string API_URL
    {
        get
        {
            //if (_API_URL == null)
            //{
                _API_URL = (db.SP_Settings_Select_ByKey_AndGroup("Config", "API_URL").FirstOrDefault());
                return _API_URL;
            //}
            //else
            //{
            //    return _API_URL;
            //}
        }
        set
        {
            if (_API_URL != value)
            {
                _API_URL = value;
                db.SP_Settings_Update("API_URL", "Config", _API_URL);
            }
        }
    }

    private static string _Cpanel_URL;
    public static string Cpanel_URL
    {
        get
        {
            //if (_Cpanel_URL == null)
            //{
                _Cpanel_URL = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Cpanel_URL").FirstOrDefault());
                return _Cpanel_URL;
            //}
            //else
            //{
            //    return _Cpanel_URL;
            //}
        }
        set
        {
            if (_Cpanel_URL != value)
            {
                _Cpanel_URL = value;
                db.SP_Settings_Update("Cpanel_URL", "Config", _Cpanel_URL);
            }
        }
    }

    private static string _Day_End;
    public static string Day_End
    {
        get
        {
            //if (_Day_End == null)
            //{
                _Day_End = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Day_End").FirstOrDefault());
                return _Day_End;
            //}
            //else
            //{
            //    return _Day_End;
            //}
        }
        set
        {
            if (_Day_End != value)
            {
                _Day_End = value;
                db.SP_Settings_Update("Day_End", "Config", _Day_End);
            }
        }
    }

    private static string _Day_Start;
    public static string Day_Start
    {
        get
        {
            //if (_Day_Start == null)
            //{
                _Day_Start = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Day_Start").FirstOrDefault());
                return _Day_Start;
            //}
            //else
            //{
            //    return _Day_Start;
            //}
        }
        set
        {
            if (_Day_Start != value)
            {
                _Day_Start = value;
                db.SP_Settings_Update("Day_Start", "Config", _Day_Start);
            }
        }
    }

    private static string _Encryption_Key;
    public static string Encryption_Key
    {
        get
        {
            //if (_Encryption_Key == null)
            //{
                _Encryption_Key = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Encryption_Key").FirstOrDefault());
                return _Encryption_Key;
            //}
            //else
            //{
            //    return _Encryption_Key;
            //}
        }
        set
        {
            if (_Encryption_Key != value)
            {
                _Encryption_Key = value;
                db.SP_Settings_Update("Encryption_Key", "Config", _Encryption_Key);
            }
        }
    }

    private static string _Timing_Step;
    public static string Timing_Step
    {
        get
        {
            //if (_Timing_Step == null)
            //{
                _Timing_Step = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Timing_Step").FirstOrDefault());
                return _Timing_Step;
            //}
            //else
            //{
            //    return _Timing_Step;
            //}
        }
        set
        {
            if (_Timing_Step != value)
            {
                _Timing_Step = value;
                db.SP_Settings_Update("Timing_Step", "Config", _Timing_Step);
            }
        }
    }

    private static string _Week_End_Day;
    public static string Week_End_Day
    {
        get
        {
            //if (_Week_End_Day == null)
            //{
                _Week_End_Day = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Week_End_Day").FirstOrDefault());
                return _Week_End_Day;
            //}
            //else
            //{
            //    return _Week_End_Day;
            //}
        }
        set
        {
            if (_Week_End_Day != value)
            {
                _Week_End_Day = value;
                db.SP_Settings_Update("Week_End_Day", "Config", _Week_End_Day);
            }
        }
    }

    private static string _Week_Start_Day;
    public static string Week_Start_Day
    {
        get
        {
            //if (_Week_Start_Day == null)
            //{
                _Week_Start_Day = (db.SP_Settings_Select_ByKey_AndGroup("Config", "Week_Start_Day").FirstOrDefault());
                return _Week_Start_Day;
            //}
            //else
            //{
            //    return _Week_Start_Day;
            //}
        }
        set
        {
            if (_Week_Start_Day != value)
            {
                _Week_Start_Day = value;
                db.SP_Settings_Update("Week_Start_Day", "Config", _Week_Start_Day);
            }
        }
    }
    
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = Helper.EncryptionKey;
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());               
            }
        }
        return cipherText;
    }
    public static string Encrypt(string clearText)
    {
        string EncryptionKey = Helper.EncryptionKey;
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
       
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
}
