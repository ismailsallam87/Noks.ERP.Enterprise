using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Device.Enrollment.Migration;
using Models;
using Device.Encoder;
using Device.Encoder.Data;
using System.ComponentModel;

namespace Device.Sync
{
    class Program
    {
        private static Models.Noks_ATT_MSEntities db = new Noks_ATT_MSEntities();
        static void Main(string[] args)
        {
            string Device_Id = "6";
            string Extract_Path = @"C:\Device.Enrollment.Migration\" + Device_Id;
            string Command = ""; //1 = Enroll Migration
            Console.WriteLine("Please Insert Your Command:");
            Command = "2"; //Console.ReadLine();
            switch (Command)
            {
                case "1":
                    {
                        Device.Enrollment.Migration.Migration newMigration = new Migration("", Extract_Path);
                        List<string> files = newMigration.Get_Files();
                        Console.WriteLine("Start Extract Enrollments....");
                        List<Enrollment.Migration.Enrollment> enrollments = newMigration.Extract_Enrollments();
                        Console.WriteLine("Extracting Process end with: " + enrollments.Count() + " employees");
                        foreach (Enrollment.Migration.Enrollment enrollment in enrollments)
                        {
                            Console.WriteLine("Start Migrating " + enrollment.Name);
                            try
                            {
                                //db.enroll_employee_device(enrollment.Name, enrollment.Enrollment_No, Convert.ToInt16(Device_Id));
                                Console.WriteLine("Enrolled successfully...");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An error occurred, emoloyee escaped....");
                            }

                        }
                        break;
                    }
                case "2":
                    {
                        List<SP_Select_Devices_Result> device_list = db.SP_Select_Devices().ToList();
                        foreach (SP_Select_Devices_Result device in device_list)
                        {
                            Device.Encoder.Functions.Device newDevice = new Encoder.Functions.Device(device.Device_No, device.Device_IP, device.Device_ID.ToString(), Convert.ToInt32(device.Device_Port), Convert.ToInt32(device.Device_Password));
                            Console.WriteLine("Trying to connect:" + device.Device_IP);
                            if (newDevice.TestConnection())
                            {
                                Console.WriteLine(device.Device_IP + " Device Connected...");
                                BindingList<GLogInfo> glogs_ = newDevice.GetAllLog(false, false);
                                Console.WriteLine("(" + glogs_.Count + ") Logs Found, do you want to proceed? Y,N");
                                string request_answer = "Y";// Console.ReadLine();
                                if (request_answer == "Y")
                                {
                                    foreach (GLogInfo log in glogs_)
                                    {
                                        Console.WriteLine("Log:" + log.photo + " is under processing....");
                                        DateTime Log_Date = new DateTime(log.yr, log.mon, log.day, log.hr, log.min, log.sec);
                                        if ((bool)db.SP_Insert_New_Log(Convert.ToInt16(Device_Id), log.enroll, log.verify_mode, Log_Date).FirstOrDefault())
                                        {
                                            Console.WriteLine(device.Device_IP + "Done Successfully...");
                                        }
                                        else
                                        {
                                            Console.WriteLine("An error occurred while trying to Sync the Log !");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(device.Device_IP + " Device is not connected");
                            }
                        }
                        break;
                    }
            }
        }
    }
}
