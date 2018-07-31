using System;
using System.Data;
using System.Text;

namespace ZKT.Device.Encoder.Functions
{
    public class Device
    {
        public SDKHelper SDK = new SDKHelper();
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
                if (SDK.GetConnectState())
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
                SDK.sta_DisConnect();
            }

        }
        public void Connect()
        {
            string password = "Admin";
            int ret = SDK.sta_ConnectTCP(lpszIPAddress.Trim(), PortNo.ToString(), devPassword.ToString());
            //int ret = SDK.sta_ConnectTCP(lpszIPAddress.Trim(), PortNo.ToString(), password);

            if (SDK.GetConnectState())
            {
                SDK.sta_getBiometricType();
            }
            if (ret == 1)
            {
                getCapacityInfo();
                getDeviceInfo();
                Console.WriteLine("Connected");
            }
            else if (ret == -2)
            {
                Console.WriteLine("Disconnected");
            }
        }
        public DataTable readAttLog(string fromTime, string toTime, bool timePeriod)
        {
            if (timePeriod == true)
            {
                this.Connect();
                DataTable dt_periodLog = new DataTable("dt_periodLog");
                dt_periodLog.Columns.Add("User ID", System.Type.GetType("System.String"));
                dt_periodLog.Columns.Add("Verify Date", System.Type.GetType("System.String"));
                dt_periodLog.Columns.Add("Verify Type", System.Type.GetType("System.Int32"));
                dt_periodLog.Columns.Add("Verify State", System.Type.GetType("System.Int32"));
                dt_periodLog.Columns.Add("WorkCode", System.Type.GetType("System.Int32"));
                SDK.sta_readLogByPeriod(dt_periodLog, fromTime, toTime);
                return dt_periodLog;
            }
            else
            {
                this.Connect();
                DataTable dt_period = new DataTable("dt_period");
                dt_period.Columns.Add("User ID", System.Type.GetType("System.String"));
                dt_period.Columns.Add("Verify Date", System.Type.GetType("System.String"));
                dt_period.Columns.Add("Verify Type", System.Type.GetType("System.Int32"));
                dt_period.Columns.Add("Verify State", System.Type.GetType("System.Int32"));
                dt_period.Columns.Add("WorkCode", System.Type.GetType("System.Int32"));
                SDK.sta_readAttLog(dt_period);
                return dt_period;
            }
        }
        public void delAttLog_Click(string fromTime, string toTime, bool timePeriod)
        {
            if (timePeriod == true)
            {
                SDK.sta_DeleteAttLogByPeriod(fromTime, toTime);
            }
            else
            {
                SDK.sta_DeleteAttLog();
            }
        }
        public void readnewlog_Click()
        {
            DataTable dt_newLog = new DataTable("dt_periodLog");
            dt_newLog.Columns.Add("User ID", System.Type.GetType("System.String"));
            dt_newLog.Columns.Add("Verify Date", System.Type.GetType("System.String"));
            dt_newLog.Columns.Add("Verify Type", System.Type.GetType("System.Int32"));
            dt_newLog.Columns.Add("Verify State", System.Type.GetType("System.Int32"));
            dt_newLog.Columns.Add("WorkCode", System.Type.GetType("System.Int32"));
            SDK.sta_ReadNewAttLog(dt_newLog);
        }
        public void deloldlog_Click(string fromTime)
        {
            SDK.sta_DelOldAttLogFromTime(fromTime);
        }
        #region members
        public int adminCnt = 0;
        public int userCount = 0;
        public int fpCnt = 0;
        public int recordCnt = 0;
        public int pwdCnt = 0;
        public int oplogCnt = 0;
        public int faceCnt = 0;
        public string sFirmver = "";
        public string sMac = "";
        public string sPlatform = "";
        public string sSN = "";
        public string sProductTime = "";
        public string sDeviceName = "";
        public int iFPAlg = 0;
        public int iFaceAlg = 0;
        public string sProducter = "";

        #endregion
        #region private_functions
        private bool Open()
        {
            try
            {
                return SDK.sta_ConnectTCP(lpszIPAddress, PortNo.ToString(), devPassword.ToString()) == 1;
            }
            catch
            {
                return false;
            }
        }
        private void getCapacityInfo()
        {
            SDK.sta_GetCapacityInfo(out adminCnt, out userCount, out fpCnt, out recordCnt, out pwdCnt, out oplogCnt, out faceCnt);
        }
        private void getDeviceInfo()
        {
            SDK.sta_GetDeviceInfo(out sFirmver, out sMac, out sPlatform, out sSN, out sProductTime, out sDeviceName, out iFPAlg, out iFaceAlg, out sProducter);
        }
        #endregion
    }
}
