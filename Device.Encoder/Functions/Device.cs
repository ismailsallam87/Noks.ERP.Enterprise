using Device.Encoder.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Device.Encoder.Functions
{
    public class Device : IDevice
    {
        public Device(int _gMachineNumber, string _lpszIPAddress, string _lpszDevId, int _PortNo, int _devPassword)
        {
            gMachineNumber = _gMachineNumber;
            lpszIPAddress = _lpszIPAddress;
            lpszDevId = _lpszDevId;
            PortNo = _PortNo;
            devPassword = _devPassword;
        }
        public bool TestConnection()
        {
            try
            {
                if (this.Open())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                this.Close();
            }

        }
        #region Public_Functions
        public bool OpenDoor()
        {
            Open();
            int vErrorCode = 0; Boolean vRet;
            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                _Exception = util.gstrNoDevice;
                Close();
                return vRet;
            }

            vRet = SBXPCDLL.SetDoorStatus(gMachineNumber, 3);
            if (!vRet)
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
            }

            SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
            Close();
            return vRet;
        }
        public bool CloseDoor()
        {
            int vErrorCode = 0;
            Boolean vRet;
            this.Open();
            // Application.DoEvents();

            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                _Exception = util.gstrNoDevice;
                return false;
            }

            vRet = SBXPCDLL.SetDoorStatus(gMachineNumber, 1);
            if (!vRet)
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
            }
            SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
            this.Close();
            return vRet;
        }
        public bool ControlUser(string EnrollNumber, string BackupNumber, bool Enable)
        {
            int vEnrollNumber;
            int vEMachineNumber;
            int vFingerNumber;
            Boolean vRet;
            byte vFlag;
            int vErrorCode = 0;
            Open();
            vEMachineNumber = gMachineNumber;
            vEnrollNumber = Convert.ToInt32(EnrollNumber == "" ? "0" : EnrollNumber);
            vFingerNumber = Convert.ToInt32(BackupNumber == "" ? "0" : BackupNumber);
            vFlag = Enable ? (byte)1 : (byte)0;

            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                _Exception = util.gstrNoDevice;
                Close();
                return false;
            }

            vRet = SBXPCDLL.EnableUser(gMachineNumber, vEnrollNumber, vEMachineNumber, vFingerNumber, vFlag);

            if (!vRet)
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
            }

            SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
            Close();
            return vRet;
        }
        public UserInfo GetUserByCard(int num)
        {
            string vName;
            Open();
            gbytEnrollData = new Byte[DATASIZE * 5];
            gbytEnrollData1 = new Byte[DATASIZE * 5];
            gTemplngEnrollData = new int[DATASIZE];
            gTempEnrollName = new int[NAMESIZE];
            ascii = new ASCIIEncoding();
            UserInfo user = new UserInfo();
            GCHandle gh; Boolean vRet; int vErrorCode = 0;
            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                _Exception = util.gstrNoDevice;
                return null;
            }
            vRet = SBXPCDLL.ReadAllUserID(gMachineNumber);
            if (!vRet)
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
                SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
                return null;
            }
            int vEnrollNumber = 0, vEMachineNumber = 0, vFingerNumber = 0, vPrivilege = 0, vEnable = 0;
            Dictionary<int, UserInfo> UserInfosMap = new Dictionary<int, UserInfo>();
            while (true)
            {
                vRet = SBXPCDLL.GetAllUserID(gMachineNumber, out vEnrollNumber, out vEMachineNumber, out vFingerNumber, out vPrivilege, out vEnable);
                vRet = SBXPCDLL.GetUserName1(gMachineNumber, vEnrollNumber, out vName);
                
                if (!vRet) break;
                if (vFingerNumber >= 50) //50 - name, 51 - photo for SB3600 only
                    continue;

                gh = GCHandle.Alloc(gTemplngEnrollData, GCHandleType.Pinned);
                IntPtr AddrOfTemplngEnrollData = gh.AddrOfPinnedObject();
                gh.Free();

                if (!vRet)
                {
                    //lblMessage.Text = "GetEnrollData Fail, id = " + vEnrollNumber + ", Backup number = " + vFingerNumber;
                    // Application.DoEvents();
                    continue;
                }

                if (!UserInfosMap.ContainsKey(vEnrollNumber))
                    UserInfosMap[vEnrollNumber] = new UserInfo();
                UserInfo ui = UserInfosMap[vEnrollNumber];
                ui.Name = vName;
                ui.Id = (uint)vEnrollNumber;
                ui.Level = (UserLevel)vPrivilege;
                ui.Enabled = true;

                if (vFingerNumber == 10)
                {
                    string s = ((uint)glngEnrollPData).ToString();
                    uint dwPwd = 0;
                    int nShift = 0;
                    foreach (var c in s)
                    {
                        dwPwd |= ((uint)((int)c - (int)'0' + 1) & 0x000F) << nShift;
                    }

                    ui.Password = dwPwd;
                }
                else if (vFingerNumber == 15)
                {
                    ui.Password = (uint)glngEnrollPData;
                }
                else if (vFingerNumber == 11)
                {
                    ui.Card = (uint)glngEnrollPData;
                }
                else if (vFingerNumber >= 0 && vFingerNumber <= 9)
                {
                    byte[] by = new byte[DATASIZE * 4];
                    unsafe
                    {
                        fixed (byte* pby_ = &by[0])
                        {
                            byte* pby = pby_;
                            fixed (int* pi_ = &gTemplngEnrollData[0])
                            {
                                int* pi = pi_;
                                for (int k = 0; k < DATASIZE; k++)
                                {
                                    *(int*)pby = *pi;
                                    pby += 4;
                                    pi++;
                                }
                            }
                        }

                    }

                    if (ui.finegers == null)
                        ui.finegers = new List<byte[]>();
                    ui.finegers.Add(by);
                }
                else continue;
                if (ui.Id == num)
                {
                    user = ui;
                    break;
                }
            }
            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0);
            this.Close();
            return user;
        }
        public bool EnrollUser(uint EnrollmentNo, UserLevel Level, uint CardId, string UserName, uint Password, bool enabled)
        {
            int vErrorCode = 0;

            UserInfo u = new UserInfo();
            u.Password = Password;
            u.Card = CardId;
            u.Name = UserName;
            u.Id = EnrollmentNo;
            u.Level = Level;
            u.Enabled = enabled;
            this.Open();
            bool vRet = SBXPCDLL.EnableDevice(1, 1);
            vRet = SBXPCDLL.SetEnrollData1(this.gMachineNumber, (int)EnrollmentNo, 11, 1, IntPtr.Zero, (int)CardId);

            if (vRet)
            {
                vRet = SBXPCDLL.SetUserName1(gMachineNumber, (int)u.Id, u.Name);
                if (!vRet)
                {
                    SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                    _Exception = _Exception + ", Name Error : " + util.ErrorPrint(vErrorCode);
                }
            }
            else
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = _Exception + ", Card Error : " + util.ErrorPrint(vErrorCode);
                //Application.DoEvents();
            }
            this.Close();
            return true;
        }
        public bool ResetAllUsers()
        {

            return true;
        }
        public bool DeleteUser(int EnrollNumber)
        {
            this.Open();
            SBXPCDLL.EnableDevice(1, 1);
            bool result = SBXPCDLL.DeleteEnrollData(this.gMachineNumber, EnrollNumber, this.gMachineNumber, 11);
            SBXPCDLL.EnableDevice(1, 1);
            this.Close();
            return true;
        }

        public void AddNewUser()
        {


            var u = new UserInfo() { Card = 007698984, Enabled = true, Level = UserLevel.Manager, Name = "User Test", Id = 30 };
            string msg = "Transferring User Data, Id = " + u.Id;
            Boolean vRet;
            int vErrorCode = 0;
            var kk = Open();
            vRet = SBXPCDLL.SetEnrollData1(gMachineNumber,
                            (int)u.Id, (int)u.Id, (int)u.Level, IntPtr.Zero, (int)u.Card.Value);
            if (vRet)
            {
                vRet = SBXPCDLL.SetUserName1(gMachineNumber, (int)u.Id, u.Name);
                if (!vRet)
                {
                    SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                    _Exception = msg + ", Name Error : " + util.ErrorPrint(vErrorCode);
                }
            }
            else
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = msg + ", Card Error : " + util.ErrorPrint(vErrorCode);
                //Application.DoEvents();
            }
            Close();
        }
        #endregion
        #region data_members
        int gMachineNumber = 1;
        string lpszIPAddress = "192.168.0.224";
        string lpszDevId = "1";
        int PortNo = 5005;
        int devPassword = 12345678;
        public string _Exception { get; set; }
        int DATASIZE = (1404 + 12) / 4;
        int NAMESIZE = 54;
        int[] gTemplngEnrollData;
        Byte[] gbytEnrollData;
        Byte[] gbytEnrollData1;
        int[] gTempEnrollName;
        int glngEnrollPData;
        ASCIIEncoding ascii;
        #endregion
        #region private_functions
        /// <summary>
        /// Open Connection
        /// </summary>
        /// <returns></returns>
        bool Open()
        {
            bool IsConnected = false;
            //		Dim vRet As Boolean
            if (lpszDevId.Length == 16)
            {
                int nError = 0;
                gMachineNumber = SBXPCDLL.ConnectP2p(lpszDevId, lpszIPAddress, PortNo, devPassword, out nError);
                if (gMachineNumber != 0)
                {
                    IsConnected = true;
                    if (nError == 4)
                        _Exception = "Relayed Connection!";
                    else if (nError == 5)
                        _Exception = "Direct Local Connection!";


                }
                else
                {
                    if (nError == 1)
                        _Exception = "Cannot Connect To Server!";
                    else if (nError == 2)
                        _Exception = "Device Not Found!";
                    else if (nError == 3)
                        _Exception = "Password Mismatched!";
                    else
                        _Exception = "Unknown Error!";
                }
            }
            else
            {
                if (SBXPCDLL.ConnectTcpip(gMachineNumber, lpszIPAddress, PortNo, devPassword))
                {
                    IsConnected = true;
                }
            }
            return IsConnected;

        }
        /// <summary>
        /// Close Connection
        /// </summary>
        void Close()
        {
            SBXPCDLL.EnableDevice(gMachineNumber, 1);
            SBXPCDLL.Disconnect(gMachineNumber);
        }
        #endregion

        internal bool PowerOff()
        {
            Boolean vRet;
            int vErrorCode = 0;
            Open();
            vRet = SBXPCDLL.PowerOffDevice(gMachineNumber);
            if (vRet)
            {
                _Exception = "Success!";
                Close();
                return true;
            }
            else
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
                Close();
                return false;
            }
        }
        internal bool PowerOn()
        {
            Boolean vRet;
            int vErrorCode = 0;
            Open();
            vRet = SBXPCDLL.PowerOnAllDevice(gMachineNumber);
            if (vRet)
            {
                _Exception = "Success!";
                Close();
                return true;
            }
            else
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
                Close();
                return false;
            }
        }
        internal string GetCurrentStatus()
        {
            StringBuilder Message = new StringBuilder();
            Open();
            uint vValue = 0;
            int vErrorCode = 0;
            Boolean vRet;
            vRet = SBXPCDLL.EnableDevice(gMachineNumber, 0); // 0 : false
            if (!vRet)
            {
                _Exception = util.gstrNoDevice;
                return _Exception;
            }

           
            if (vRet)
            {
                for(int i = 1; i < 9; i++)
                {
                    vRet = SBXPCDLL.GetDeviceStatus(gMachineNumber, i, out vValue);
                    switch (i)
                    {
                        case 1:
                            Message.AppendLine("(1) = Manager count = " + vValue);
                            break;
                        case 2:
                            Message.AppendLine("(2) = User count = " + vValue);
                            break;
                        case 3:
                            Message.AppendLine("(3) = Fp count = " + vValue);
                            break;
                        case 4:
                            Message.AppendLine("(4) = Password count = " + vValue);
                            break;
                        case 5:
                            Message.AppendLine("(5) = SLog count = " + vValue);
                            break;
                        case 6:
                            Message.AppendLine("(6) = GLog count = " + vValue);
                            break;
                        case 7:
                            Message.AppendLine("(7) = Card count = " + vValue);
                            break;
                        case 8:
                            Message.AppendLine("(8) = Alarm status = " + vValue);
                            break;
                    }
                }
                int DoorStatus = 0;
                vRet = SBXPCDLL.GetDoorStatus(gMachineNumber, out DoorStatus);
                if (vRet)
                {
                    if (DoorStatus == 1)
                        Message.AppendLine("Door Status: Uncond Door Open State!");
                    else if (DoorStatus == 2)
                        Message.AppendLine("Door Status: Uncond Door Close State!");
                    else if (DoorStatus == 3)
                        Message.AppendLine("Door Status: Door Open State!");
                    else if (DoorStatus == 4)
                        Message.AppendLine("Door Status: Auto Recover State!");
                    else if (DoorStatus == 5)
                        Message.AppendLine("Door Status: Door Close State!");
                    else if (DoorStatus == 6)
                        Message.AppendLine("Door Status: Watching for Close State!");
                    else if (DoorStatus == 7)
                        Message.AppendLine("Door Status: Illegal Open State!");
                    else
                        Message.AppendLine("Door Status: User State!");
                }

            }
            else
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                _Exception = util.ErrorPrint(vErrorCode);
            }

            Close();
            return Message.ToString();
        }
        internal bool ResetUsers(int gMachineNumber)
        {
            this.Open();
            bool result =SBXPCDLL.EmptyEnrollData(gMachineNumber);
            this.Close();
            return result;
        }
        #region Not_Tested
        public BindingList<GLogInfo> GetAllLog(bool chkReadMark,bool chkAndDelete)
        {
            BindingList<GLogInfo> glogs_ = new BindingList<GLogInfo>();
            Boolean vRet;
            int vErrorCode = 0;

            vRet =Open();
            if (!vRet)
            {
                Console.WriteLine("Error occurred while trying to connect to the Device.");
                return null;
            }

            vRet = SBXPCDLL.ReadGeneralLogData(gMachineNumber, chkReadMark ? (byte)1 : (byte)0);
            if (!vRet)
            {
                SBXPCDLL.GetLastError(gMachineNumber, out vErrorCode);
                Console.WriteLine(util.ErrorPrint(vErrorCode));
                return null;
            }
            else
            {
                if (chkAndDelete)
                    SBXPCDLL.EmptyGeneralLogData(gMachineNumber);
            }
            if (vRet)
            {

                while (true)
                {
                    GLogInfo gi = new GLogInfo();
                    vRet = SBXPCDLL.GetGeneralLogData(gMachineNumber,
                                                    out gi.tmno,
                                                    out gi.seno,
                                                    out gi.smno,
                                                    out gi.vmode,
                                                    out gi.yr,
                                                    out gi.mon,
                                                    out gi.day,
                                                    out gi.hr,
                                                    out gi.min,
                                                    out gi.sec);
                    if (!vRet) break;
                    glogs_.Add(gi);
                }
                Console.WriteLine("ReadGeneralLogData OK");
                SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
                Close();
                return glogs_;

            }
            else
            {
                SBXPCDLL.EnableDevice(gMachineNumber, 1); // 1 : true
                return null;
            }
            
        }
        #endregion
    }
}
