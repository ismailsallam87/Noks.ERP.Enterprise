﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using zkemkeeper;
using System.Text;

namespace ZKT.Device.Encoder
{
   
    public class SDKHelper
    {
        public string Real_Device_State { get; set; }
        public CZKEMClass axCZKEM1 = new CZKEMClass();

        public List<Employee> employeeList = new List<Employee>();
        public List<BioTemplate> bioTemplateList = new List<BioTemplate>();

        public List<string> biometricTypes = new List<string>();

        private static bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private static int iMachineNumber = 1;
        private static int idwErrorCode = 0;
        private static int iDeviceTpye = 1;
        bool bAddControl = true;        //Get all user's ID

        #region UserBioTypeClass

        private string _biometricType = string.Empty;
        private string _biometricVersion = string.Empty;

        private SupportBiometricType _supportBiometricType = new SupportBiometricType();

        public const string PersBioTableName = "Pers_Biotemplate";

        public const string PersBioTableFields = "*";

        public SupportBiometricType supportBiometricType
        {
            get { return _supportBiometricType; }
        }

        public string biometricType
        {
            get { return _biometricType; }
        }

        public class Employee
        {
            public string pin { get; set; }
            public string name { get; set; }
            public string password { get; set; }
            public int privilege { get; set; }
            public string cardNumber { get; set; }
        }

        public class SupportBiometricType
        {
            public bool fp_available { get; set; }
            public bool face_available { get; set; }
            public bool fingerVein_available { get; set; }
            public bool palm_available { get; set; }
        }

        public class BioTemplate
        {
            /// <summary>
            /// is valid,0:invalid,1:valid,default=1
            /// </summary>
            private int validFlag = 1;
            public virtual int valid_flag
            {
                get { return validFlag; }
                set { validFlag = value; }
            }

            /// <summary>
            /// is duress,0:not duress,1:duress,default=0
            /// </summary>
            public virtual int is_duress { get; set; }

            /// <summary>
            /// Biometric Type
            /// 0： General
            /// 1： Finger Printer
            /// 2： Face
            /// 3： Voiceprint
            /// 4： Iris
            /// 5： Retina
            /// 6： Palm prints
            /// 7： FingerVein
            /// 8： Palm Vein
            /// </summary>
            public virtual int bio_type { get; set; }

            /// <summary>
            /// template version
            /// </summary>
            public virtual string version { get; set; }

            /// <summary>
            /// data format
            /// ZK\ISO\ANSI 
            /// 0： ZK
            /// 1： ISO
            /// 2： ANSI
            /// </summary>
            public virtual int data_format { get; set; }

            /// <summary>
            /// template no
            /// </summary>
            public virtual int template_no { get; set; }

            /// <summary>
            /// template index
            /// </summary>
            public virtual int template_no_index { get; set; }

            /// <summary>
            /// template data
            /// </summary>
            public virtual string template_data { get; set; }

            /// <summary>
            /// pin
            /// </summary>
            public virtual string pin { get; set; }
        }

        public class BioType
        {
            public string name { get; set; }
            public int value { get; set; }

            public override string ToString()
            {
                return name;
            }
        }
        #endregion

        #region ConnectDevice

        public bool GetConnectState()
        {
            return bIsConnected;
        }

        public void SetConnectState(bool state)
        {
            bIsConnected = state;
            //connected = state;
        }

        public int GetMachineNumber()
        {
            return iMachineNumber;
        }

        public void SetMachineNumber(int Number)
        {
            iMachineNumber = Number;
        }

        public int sta_ConnectTCP(string ip, string port, string commKey)
        {
            if (ip == "" || port == "" || commKey == "")
            {
                return -1;// ip or port is null
            }

            if (Convert.ToInt32(port) <= 0 || Convert.ToInt32(port) > 65535)
            {
                //"*Port illegal!"
                return -1;
            }

            if (Convert.ToInt32(commKey) < 0 || Convert.ToInt32(commKey) > 999999)
            {
                //"*CommKey illegal!"
                return -1;
            }

            int idwErrorCode = 0;

            axCZKEM1.SetCommPassword(Convert.ToInt32(commKey));

            if (bIsConnected == true)
            {
                axCZKEM1.Disconnect();
                sta_UnRegRealTime();
                SetConnectState(false);
                //"Disconnect with device !";
                //connected = false;
                return -2; //disconnect
            }

            if (axCZKEM1.Connect_Net(ip, Convert.ToInt32(port)) == true)
            {
                SetConnectState(true);
                //"Connect with device !";

                //get Biotype
                sta_getBiometricType();

                return 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Unable to connect the device,ErrorCode=" + idwErrorCode.ToString();
                return idwErrorCode;
            }
        }

        public int sta_ConnectRS(string deviceid, string port, string baudrate, string commkey)
        {
            if (deviceid == "" || port == "" || baudrate == "" || commkey == "")
            {
                //"*Device ID, Port, Baudrate, Comm Key cannot be null !";
                return -1;
            }

            if (Convert.ToInt32(deviceid) < 0 || Convert.ToInt32(deviceid) > 256)
            {
                //"*Device illegal!";
                return -1;
            }

            if (Convert.ToInt32(commkey) < 0 || Convert.ToInt32(commkey) > 999999)
            {
                //"*CommKey illegal!";
                return -1;
            }

            int idwErrorCode = 0;

            int iDeviceID = Convert.ToInt32(deviceid);
            int iPort = 0;
            int iBaudrate = Convert.ToInt32(baudrate);
            int iCommkey = Convert.ToInt32(commkey);

            for (iPort = 1; iPort < 10; iPort++)
            {
                if (port.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            axCZKEM1.SetCommPassword(iCommkey);

            if (bIsConnected == true)
            {
                axCZKEM1.Disconnect();
                sta_UnRegRealTime();
                SetConnectState(false);
                //"Disconnect with device !");
                return -2; //disconnect
            }

            if (axCZKEM1.Connect_Com(iPort, iDeviceID, iBaudrate) == true)
            {
                SetConnectState(true);
                sta_RegRealTime();

                //get Biotype
                sta_getBiometricType();
                //"Connect with device !");
                return 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Unable to connect the device,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
        }

        public int sta_ConnectUSB( string deviceid, string commkey)
        {
            if (deviceid == "" || commkey == "")
            {
                //"*deviceid, commkey cannot be null !");
                return -1;
            }

            if (Convert.ToInt32(deviceid) < 0 || Convert.ToInt32(deviceid) > 256)
            {
                //"*Device illegal!");
                return -1;
            }

            if (Convert.ToInt32(commkey) < 0 || Convert.ToInt32(commkey) > 999999)
            {
                //"*CommKey illegal!");
                return -1;
            }

            int idwErrorCode = 0;
            int iPort = 0;
            int iBaudrate = 115200;
            int iDeviceID = Convert.ToInt32(deviceid);
            int iCommkey = Convert.ToInt32(commkey);
            string sCom = "";

            if (iDeviceID == 0 || iDeviceID > 255)
            {
                //"*The Device ID is invalid !");
                return -1;
            }

            SearchforUSBCom usbcom = new SearchforUSBCom();
            bool bSearch = usbcom.SearchforCom(ref sCom);//modify by Darcy on Nov.26 2009
            if (bSearch == false)
            {
                //"*Can not find the virtual serial port that can be used !");
                return -1;
            }

            for (iPort = 1; iPort < 10; iPort++)
            {
                if (sCom.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            axCZKEM1.SetCommPassword(iCommkey);

            if (bIsConnected == true)
            {
                axCZKEM1.Disconnect();
                sta_UnRegRealTime();
                SetConnectState(false);
                //"Disconnect with device !");
                return -2; //disconnect
            }

            if (axCZKEM1.Connect_Com(iPort, iDeviceID, iBaudrate) == true)
            {
                SetConnectState(true);
                sta_RegRealTime();

                //Get BioType
                sta_getBiometricType();

                //"Connect with device !");
                return 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Unable to connect the device,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
        }

        public int sta_GetDeviceInfo(out string sFirmver, out string sMac, out string sPlatform, out string sSN, out string sProductTime, out string sDeviceName, out int iFPAlg, out int iFaceAlg, out string sProducter)
        {
            int iRet = 0;

            sFirmver = "";
            sMac = "";
            sPlatform = "";
            sSN = "";
            sProducter = "";
            sDeviceName = "";
            iFPAlg = 0;
            iFaceAlg = 0;
            sProductTime = "";
            string strTemp = "";

            if (GetConnectState() == false)
            {
                //"*Please connect first!";
                return -1024;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            axCZKEM1.GetSysOption(GetMachineNumber(), "~ZKFPVersion", out strTemp);
            if(!string.IsNullOrEmpty(strTemp))
            iFPAlg = Convert.ToInt32(strTemp);

            axCZKEM1.GetSysOption(GetMachineNumber(), "ZKFaceVersion", out strTemp);
            if (!string.IsNullOrEmpty(strTemp))
                iFaceAlg = Convert.ToInt32(strTemp);

            /*
            axCZKEM1.GetDeviceInfo(GetMachineNumber(), 72, ref iFPAlg);
            axCZKEM1.GetDeviceInfo(GetMachineNumber(), 73, ref iFaceAlg);
            */

            axCZKEM1.GetVendor(ref sProducter);
            axCZKEM1.GetProductCode(GetMachineNumber(), out sDeviceName);
            axCZKEM1.GetDeviceMAC(GetMachineNumber(), ref sMac);
            axCZKEM1.GetFirmwareVersion(GetMachineNumber(), ref sFirmver);

            /*
            if (sta_GetDeviceType() == 1)
            {
                axCZKEM1.GetDeviceFirmwareVersion(GetMachineNumber(), ref sFirmver);
            }
             */
            ////"[func GetDeviceFirmwareVersion]Temporarily unsupported");

            axCZKEM1.GetPlatform(GetMachineNumber(), ref sPlatform);
            axCZKEM1.GetSerialNumber(GetMachineNumber(), out sSN);
            axCZKEM1.GetDeviceStrInfo(GetMachineNumber(), 1, out sProductTime);

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            //"Get the device info successfully";
            iRet = 1;
            return iRet;
        }

        public int sta_GetCapacityInfo(out int adminCnt, out int userCount, out int fpCnt, out int recordCnt, out int pwdCnt, out int oplogCnt, out int faceCnt)
        {
            int ret = 0;

            adminCnt = 0;
            userCount = 0;
            fpCnt = 0;
            recordCnt = 0;
            pwdCnt = 0;
            oplogCnt = 0;
            faceCnt = 0;

            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 2, ref userCount);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 1, ref adminCnt);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 3, ref fpCnt);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 4, ref pwdCnt);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 5, ref oplogCnt);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 6, ref recordCnt);
            axCZKEM1.GetDeviceStatus(GetMachineNumber(), 21, ref faceCnt);

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            //"Get the device capacity successfully";

            ret = 1;
            return ret;
        }

        public void sta_DisConnect()
        {
            if (GetConnectState() == true)
            {
                axCZKEM1.Disconnect();
                sta_UnRegRealTime();
            }
        }

        #endregion

        #region DeviceType

        public int sta_GetDeviceType()
        {
            string sPlatform = "";
            int iFaceDevice = 0;

            if (axCZKEM1.IsTFTMachine(GetMachineNumber()))
            {
                axCZKEM1.GetDeviceInfo(GetMachineNumber(), 75, ref iFaceDevice);
                axCZKEM1.GetPlatform(GetMachineNumber(), ref sPlatform);
                if (sPlatform.Contains("ZMM"))
                {
                    return 1;//new firmware device
                }
                else if (iFaceDevice == 1)
                {
                    return 2;//face serial
                }
                else
                {
                    return 3;//color device
                }
            }
            else
            {
                return 4;//black&whith device
            }

        }

        #endregion

        #region RealTimeEvent

        public void sta_UnRegRealTime()
        {
            this.axCZKEM1.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
            this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
            this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
            this.axCZKEM1.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
            this.axCZKEM1.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
            this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
            this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
            this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
            this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
            this.axCZKEM1.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
            this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
            this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
            this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
            this.axCZKEM1.OnAttTransaction -= new zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(axCZKEM1_OnAttTransaction);
            this.axCZKEM1.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);
            this.axCZKEM1.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(axCZKEM1_OnEnrollFinger);

        }

        public int sta_RegRealTime()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!";
                return -1024;
            }

            int ret = 0;

            if (axCZKEM1.RegEvent(GetMachineNumber(), 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            {
                //common interface
                this.axCZKEM1.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                this.axCZKEM1.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
                this.axCZKEM1.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);

                //only for color device
                this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                this.axCZKEM1.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);

                //only for black&white device
                this.axCZKEM1.OnAttTransaction -= new zkemkeeper._IZKEMEvents_OnAttTransactionEventHandler(axCZKEM1_OnAttTransaction);
                this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                this.axCZKEM1.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);
                this.axCZKEM1.OnEnrollFinger += new zkemkeeper._IZKEMEvents_OnEnrollFingerEventHandler(axCZKEM1_OnEnrollFinger);


                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*RegEvent failed,ErrorCode: " + idwErrorCode.ToString();
                }
                else
                {
                    //"*No data from terminal returns!";
                }
            }
            return ret;
        }

        //When you are enrolling your finger,this event will be triggered.
        void axCZKEM1_OnEnrollFingerEx(string EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
        {
            if (ActionResult == 0)
            {
                //"Enroll finger succeed. UserID=" + EnrollNumber.ToString() + "...FingerIndex=" + FingerIndex.ToString();
            }
            else
            {
                //"Enroll finger failed. Result=" + ActionResult.ToString();
            }
            throw new NotImplementedException();
        }

        //Door sensor event
        void axCZKEM1_OnDoor(int EventType)
        {
            //"Door opened" + "...EventType=" + EventType.ToString();

            throw new NotImplementedException();
        }

        //When the dismantling machine or duress alarm occurs, trigger this event.
        void axCZKEM1_OnAlarm(int AlarmType, int EnrollNumber, int Verified)
        {
            //"Alarm triggered" + "...AlarmType=" + AlarmType.ToString() + "...EnrollNumber=" + EnrollNumber.ToString() + "...Verified=" + Verified.ToString();

            throw new NotImplementedException();
        }

        //When you swipe a card to the device, this event will be triggered to show you the card number.
        void axCZKEM1_OnHIDNum(int CardNumber)
        {
            //"Card event" + "...Cardnumber=" + CardNumber.ToString();

            throw new NotImplementedException();
        }

        //When you have enrolled a new user,this event will be triggered.
        void axCZKEM1_OnNewUser(int EnrollNumber)
        {
            //"Enroll a　new user" + "...UserID=" + EnrollNumber.ToString();

            throw new NotImplementedException();
        }

        //When you have deleted one one fingerprint template,this event will be triggered.
        void axCZKEM1_OnDeleteTemplate(int EnrollNumber, int FingerIndex)
        {
            //"Delete a finger template" + "...UserID=" + EnrollNumber.ToString() + "..FingerIndex=" + FingerIndex.ToString();

            throw new NotImplementedException();
        }

        //When you have enrolled your finger,this event will be triggered and return the quality of the fingerprint you have enrolled
        void axCZKEM1_OnFingerFeature(int Score)
        {
            //"Press finger score=" + Score.ToString();

            throw new NotImplementedException();
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered,only for color device
        void axCZKEM1_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            string time = Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second;

            //"Verify OK.UserID=" + EnrollNumber + " isInvalid=" + IsInValid.ToString() + " state=" + AttState.ToString() + " verifystyle=" + VerifyMethod.ToString() + " time=" + time;

            throw new NotImplementedException();
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered,only for black%white device
        private void axCZKEM1_OnAttTransaction(int EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second)
        {
            string time = Year + "-" + Month + "-" + Day + " " + Hour + ":" + Minute + ":" + Second;
            //"Verify OK.UserID=" + EnrollNumber.ToString() + " isInvalid=" + IsInValid.ToString() + " state=" + AttState.ToString() + " verifystyle=" + VerifyMethod.ToString() + " time=" + time;

            throw new NotImplementedException();
        }

        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
        void axCZKEM1_OnVerify(int UserID)
        {
            if (UserID != -1)
            {
                //"User fingerprint verified... UserID=" + UserID.ToString();
            }
            else
            {
                //"Failed to verify... ";
            }

            throw new NotImplementedException();
        }

        //When you place your finger on sensor of the device,this event will be triggered
        void axCZKEM1_OnFinger()
        {
            //"OnFinger event ";

            throw new NotImplementedException();
        }

        //When you have written into the Mifare card ,this event will be triggered.
        void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            if (iActionResult == 0)
            {
                //"Write Mifare Card OK" + "...EnrollNumber=" + iEnrollNumber.ToString() + "...TmpLength=" + iLength.ToString();
            }
            else
            {
                //"...Write Failed";
            }
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            if (iActionResult == 0)
            {
                //"Empty Mifare Card OK...";
            }
            else
            {
                //"Empty Failed...";
            }
        }

        //When you press the keypad,this event will be triggered.
        void axCZKEM1_OnKeyPress(int iKey)
        {
            //"RTEvent OnKeyPress Has been Triggered, Key: " + iKey.ToString();
        }

        //When you are enrolling your finger,this event will be triggered.
        void axCZKEM1_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
        {
            if (ActionResult == 0)
            {
                //"Enroll finger succeed. UserID=" + EnrollNumber + "...FingerIndex=" + FingerIndex.ToString();
            }
            else
            {
                //"Enroll finger failed. Result=" + ActionResult.ToString();
            }
            throw new NotImplementedException();
        }

        #endregion

        #region UserMng

        #region UserInfo

        public int sta_DeleteEnrollData(int cbUseID, int cbBackupDE)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUseID.ToString().Trim() == "" || cbBackupDE.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string sUserID = cbUseID.ToString().Trim();
            int iBackupNumber = Convert.ToInt32(cbBackupDE.ToString().Trim());

            if (axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, sUserID, iBackupNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"SSR_DeleteEnrollData,UserID=" + sUserID + " BackupNumber=" + iBackupNumber.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //if (idwErrorCode == 0 && iBackupNumber == 11)
                    //"SSR_DeleteEnrollData,UserID=" + sUserID + " BackupNumber=" + iBackupNumber.ToString());
                //else
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_DelUserTmp(int cbUseID, int cbFingerIndex)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUseID.ToString().Trim() == "" || cbFingerIndex.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string sUserID = cbUseID.ToString().Trim();
            int iFingerIndex = Convert.ToInt32(cbFingerIndex.ToString().Trim());

            if (axCZKEM1.SSR_DelUserTmpExt(iMachineNumber, sUserID, iFingerIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"SSR_DelUserTmpExt,UserID:" + sUserID + " FingerIndex:" + iFingerIndex.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_OnlineEnroll(string txtUserID, int cbFingerIndex, int cbFlag)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtUserID.ToString().Trim() == "" || cbFingerIndex.ToString().Trim() == "" || cbFlag.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iPIN2Width = 0;
            int iIsABCPinEnable = 0;
            int iT9FunOn = 0;
            string strTemp = "";
            axCZKEM1.GetSysOption(GetMachineNumber(), "~PIN2Width", out strTemp);
            iPIN2Width = Convert.ToInt32(strTemp);
            axCZKEM1.GetSysOption(GetMachineNumber(), "~IsABCPinEnable", out strTemp);
            iIsABCPinEnable = Convert.ToInt32(strTemp);
            axCZKEM1.GetSysOption(GetMachineNumber(), "~T9FunOn", out strTemp);
            iT9FunOn = Convert.ToInt32(strTemp);

            /*
            axCZKEM1.GetDeviceInfo(iMachineNumber, 76, ref iPIN2Width);
            axCZKEM1.GetDeviceInfo(iMachineNumber, 77, ref iIsABCPinEnable);
            axCZKEM1.GetDeviceInfo(iMachineNumber, 78, ref iT9FunOn);
             */

            if (txtUserID.Length > iPIN2Width)
            {
                //"*User ID error! The max length is " + iPIN2Width.ToString());
                return -1022;
            }

            if (iIsABCPinEnable == 0 || iT9FunOn == 0)
            {
                if (txtUserID.ToString().Substring(0, 1) == "0")
                {
                    //"*User ID error! The first letter can not be as 0");
                    return -1022;
                }

                foreach (char tempchar in txtUserID.ToString().ToCharArray())
                {
                    if (!(char.IsDigit(tempchar)))
                    {
                        //"*User ID error! User ID only support digital");
                        return -1022;
                    }
                }
            }

            int idwErrorCode = 0;
            string sUserID = txtUserID.ToString().Trim();
            int iFingerIndex = Convert.ToInt32(cbFingerIndex.ToString().Trim());
            int iFlag = Convert.ToInt32(cbFlag.ToString().Trim());

            axCZKEM1.CancelOperation();
            //If the specified index of user's templates has existed ,delete it first
            axCZKEM1.SSR_DelUserTmpExt(iMachineNumber, sUserID, iFingerIndex);
            if (axCZKEM1.StartEnrollEx(sUserID, iFingerIndex, iFlag))
            {
                //"Start to Enroll a new User,UserID=" + sUserID + " FingerID=" + iFingerIndex.ToString() + " Flag=" + iFlag.ToString());
                if (axCZKEM1.StartIdentify())
                {
                    //"Enroll a new User,UserID" + sUserID);
                }
                ;//After enrolling templates,you should let the device into the 1:N verification condition
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_SetUserInfo(string txtUserID, string txtName, int cbPrivilege, string txtCardnumber, string txtPassword)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtUserID.Trim() == "" || cbPrivilege.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iPrivilege = cbPrivilege;

            bool bFlag = false;
            if (iPrivilege == 5)
            {
                //"*User Defined Role is Error! Please Register again!");
                return -1023;
            }

            /*
            if(iPrivilege == 4)
            {
                axCZKEM1.IsUserDefRoleEnable(iMachineNumber, 4, out bFlag);

                if (bFlag == false)
                {
                    //"*User Defined Role is unable!");
                    return -1023;
                }
            }
             */
            ////"[func IsUserDefRoleEnable]Temporarily unsupported");

            int iPIN2Width = 0;
            int iIsABCPinEnable = 0;
            int iT9FunOn = 0;
            string strTemp = "";
            axCZKEM1.GetSysOption(GetMachineNumber(), "~PIN2Width", out strTemp);
            iPIN2Width = Convert.ToInt32(strTemp);
            axCZKEM1.GetSysOption(GetMachineNumber(), "~IsABCPinEnable", out strTemp);
            iIsABCPinEnable = Convert.ToInt32(strTemp);
            axCZKEM1.GetSysOption(GetMachineNumber(), "~T9FunOn", out strTemp);
            iT9FunOn = Convert.ToInt32(strTemp);
            /*
            axCZKEM1.GetDeviceInfo(iMachineNumber, 76, ref iPIN2Width);
            axCZKEM1.GetDeviceInfo(iMachineNumber, 77, ref iIsABCPinEnable);
            axCZKEM1.GetDeviceInfo(iMachineNumber, 78, ref iT9FunOn);
            */

            if (txtUserID.Length > iPIN2Width)
            {
                //"*User ID error! The max length is " + iPIN2Width.ToString());
                return -1022;
            }

            if (iIsABCPinEnable == 0 || iT9FunOn == 0)
            {
                if (txtUserID.Substring(0, 1) == "0")
                {
                    //"*User ID error! The first letter can not be as 0");
                    return -1022;
                }

                foreach (char tempchar in txtUserID.ToCharArray())
                {
                    if (!(char.IsDigit(tempchar)))
                    {
                        //"*User ID error! User ID only support digital");
                        return -1022;
                    }
                }
            }

            int idwErrorCode = 0;
            string sdwEnrollNumber = txtUserID.Trim();
            string sName = txtName.Trim();
            string sCardnumber = txtCardnumber.Trim();
            string sPassword = txtPassword.Trim();

            bool bEnabled = true;
            /*if (iPrivilege == 4)
            {
                bEnabled = false;
                iPrivilege = 0;
            }
            else
            {
                bEnabled = true;
            }*/

            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
            {
                //"Set user information successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_GetUserInfo(string txtUserID, string txtName, string cbPrivilege, string txtCardnumber, string txtPassword)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtUserID.Trim() == "")
            {
                //"*Please input user id first!");
                return -1023;
            }

            int iPIN2Width = 0;
            string strTemp = "";
            axCZKEM1.GetSysOption(GetMachineNumber(), "~PIN2Width", out strTemp);
            iPIN2Width = Convert.ToInt32(strTemp);

            if (txtUserID.Length > iPIN2Width)
            {
                //"*User ID error! The max length is " + iPIN2Width.ToString());
                return -1022;
            }

            int idwErrorCode = 0;
            int iPrivilege = 0;
            string strName = "";
            string strCardno = "";
            string strPassword = "";
            bool bEnabled = false;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_GetUserInfo(iMachineNumber, txtUserID.Trim(), out strName, out strPassword, out iPrivilege, out bEnabled))//upload the user's information(card number included)
            {
                axCZKEM1.GetStrCardNumber(out strCardno);
                if (strCardno.Equals("0"))
                {
                    strCardno = "";
                }
                txtName = strName;
                txtPassword = strPassword;
                txtCardnumber = strCardno;
                cbPrivilege = iPrivilege.ToString();
                //"Get user information successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //modify by Leonard 2017/12/18
                txtName = " ";
                txtPassword = " ";
                txtCardnumber = " ";
                cbPrivilege = "5";
                //"The User is not exist");
                //end by Leonard
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_GetHIDEventCardNum()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwErrorCode = 0;
            string strHIDEventCardNum = "";

            if (axCZKEM1.GetHIDEventCardNumAsStr(out strHIDEventCardNum))
            {
                //"GetHIDEventCardNumAsStr! HIDCardNum=" + strHIDEventCardNum);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_SetUserValidDate(int cbExpires, string txtID2, DateTime dtStartDate, DateTime dtEndDate, string txtCount)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtID2.Trim() == "" || cbExpires.ToString().Trim() == "")
            {
                //"*Please input user ID or Exprires first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string idwUserID = txtID2.Trim();  //string
            string sdwStartDate = dtStartDate.ToString().Trim();
            string sdwEndDate = dtEndDate.ToString().Trim();
            int expires = Convert.ToInt32(cbExpires);
            int validcount = 0;
            int iCount = 0;

            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Today;

            sdwStartDate = dtStartDate.ToString().Trim() + " 00:00:00";
            sdwEndDate = dtEndDate.ToString().Trim() + " 23:59:59";

            switch (expires)
            {
                case 0:
                    break;
                case 1:
                    if (dtStartDate.ToString().Trim() == "" || dtEndDate.ToString().Trim() == "")
                    {
                        //"*Please input StartDate or EndDate first!");
                        return -1022;
                    }

                    validcount = 0;

                    break;

                case 2:
                    if (txtCount.Trim() == "")
                    {
                        //"*Please input data first!");
                        return -1022;
                    }

                    if (txtCount.Trim() == "" || iCount < 0 || iCount > 10000)
                    {
                        //"*The Count is error!");
                        return -1022;
                    }

                    validcount = Convert.ToInt32(txtCount.Trim());

                    sdwStartDate = currentTime.Year.ToString() + "-" + currentTime.Month.ToString() + "-" + currentTime.Day.ToString() + " 00:00:00";
                    sdwEndDate = currentTime.Year.ToString() + "-" + currentTime.Month.ToString() + "-" + currentTime.Day.ToString() + " 23:59:59";

                    break;

                case 3:
                    if (dtStartDate.ToString().Trim() == "" || dtEndDate.ToString().Trim() == "" || txtCount.Trim() == "")
                    {
                        //"*Please input data first!");
                        return -1022;
                    }

                    if (iCount < 0 || iCount > 10000)
                    {
                        //"*The Count is error!");
                        return -1022;
                    }

                    validcount = Convert.ToInt32(txtCount.Trim());

                    break;

                default:
                    //"*Expires Error,please input again!");
                    return -1022;

            }


            if (axCZKEM1.SetUserValidDate(iMachineNumber, idwUserID, expires, validcount, sdwStartDate, sdwEndDate))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Successfully set the ValidDate!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //txtCount = " ";
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }


            ////"[func SetUserValidDate]Temporarily unsupported");

            return 1;
        }

        public int sta_GetUserValidDate(int cbExpires, string txtID2, DateTime dtStartDate, DateTime dtEndDate, string txtCount)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtID2.Trim() == "")
            {
                //"*Please input user ID first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string sUserID = txtID2.Trim();
            int iExpires = 0;
            int validcount = 0;
            string sStartDate = null;
            string sEndDate = null;

            if (axCZKEM1.GetUserValidDate(iMachineNumber, sUserID, out iExpires, out validcount, out sStartDate, out sEndDate))
            {
                switch (iExpires)
                {
                    case 0: cbExpires = 0; break;
                    case 1: cbExpires = 1; break;
                    case 2: cbExpires = 2; break;
                    case 3: cbExpires = 3; break;
                }
                dtStartDate =Convert.ToDateTime(sStartDate);
                dtEndDate =Convert.ToDateTime(sEndDate);
                txtCount = validcount.ToString();
                //"Get user valid date successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //add by Leonard 2017/12/19
                if (idwErrorCode == -4996)          //User is exsit but not set ValideDate
                {
                    cbExpires = 0;
                    dtStartDate = DateTime.Now;
                    dtEndDate = DateTime.Now;
                    txtCount = "0";
                    //"*Operation failed,This User Don't Set ValideDate Or LimitCounts.");
                }
                else if (idwErrorCode == -4993)     //User is not exist
                {
                    cbExpires = 0;
                    dtStartDate = DateTime.Now;
                    dtEndDate = DateTime.Now;
                    txtCount = "0";
                    //"*Operation failed,This User is not exist.");
                }
                else
                {
                    txtCount = " ";
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                }
                //end add
            }

            ////"[func GetUserValidDate]Temporarily unsupported");

            return 1;
        }

        public int sta_GetUserVerifyStyle(int cbUserID7, int cbVerifyStyle)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUserID7.ToString().Trim() == "")
            {
                //"*Please input user ID first!");
                return -1023;
            }

            int iVerifyStyle = 0;
            byte bReserved;
            string sUserID = cbUserID7.ToString().Trim();

            if (axCZKEM1.GetUserInfoEx(GetMachineNumber(), Convert.ToInt32(sUserID), out iVerifyStyle, out bReserved))
            {
                switch (iVerifyStyle)
                {
                    case 0:   cbVerifyStyle = 0; break;
                    case 128: cbVerifyStyle = 1; break;
                    case 129: cbVerifyStyle = 2; break;
                    case 130: cbVerifyStyle = 3; break;
                    case 131: cbVerifyStyle = 4; break;
                    case 132: cbVerifyStyle = 5; break;
                    case 133: cbVerifyStyle = 6; break;
                    case 134: cbVerifyStyle = 7; break;
                    case 135: cbVerifyStyle = 8; break;
                    case 136: cbVerifyStyle = 9; break;
                    case 137: cbVerifyStyle = 10; break;
                    case 138: cbVerifyStyle = 11; break;
                    case 139: cbVerifyStyle = 12; break;
                    case 140: cbVerifyStyle = 13; break;
                    case 141: cbVerifyStyle = 14; break;
                    case 142: cbVerifyStyle = 15; break;
                }
                //"Get user verify style successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            /*
            if (axCZKEM1.GetUserVerifyStyle(1, sUserID, out iVerifyStyle, out bReserved))
            {
                switch (iVerifyStyle)
                {
                    case 0: cbVerifyStyle.SelectedIndex = 0; break;
                    case 128: cbVerifyStyle.SelectedIndex = 1; break;
                    case 129: cbVerifyStyle.SelectedIndex = 2; break;
                    case 130: cbVerifyStyle.SelectedIndex = 3; break;
                    case 131: cbVerifyStyle.SelectedIndex = 4; break;
                    case 132: cbVerifyStyle.SelectedIndex = 5; break;
                    case 133: cbVerifyStyle.SelectedIndex = 6; break;
                    case 134: cbVerifyStyle.SelectedIndex = 7; break;
                    case 135: cbVerifyStyle.SelectedIndex = 8; break;
                    case 136: cbVerifyStyle.SelectedIndex = 9; break;
                    case 137: cbVerifyStyle.SelectedIndex = 10; break;
                    case 138: cbVerifyStyle.SelectedIndex = 11; break;
                    case 139: cbVerifyStyle.SelectedIndex = 12; break;
                    case 140: cbVerifyStyle.SelectedIndex = 13; break;
                    case 141: cbVerifyStyle.SelectedIndex = 14; break;
                    case 142: cbVerifyStyle.SelectedIndex = 15; break;
                }
                //"Get user verify style successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            */

            ////"[func GetUserVerifyStyle]Temporarily unsupported");
            return 1;
        }

        public int sta_SetUserVerifyStyle(int cbUserID7, int cbVerifyStyle)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUserID7.ToString().Trim() == "" || cbVerifyStyle.ToString() == "")
            {
                //"*Please input the UserID or VerifyStyle!");
                return -1023;
            }
            int idwErrorCode = 0;

            byte bReserved = 0;
            string sUserID = cbUserID7.ToString().Trim();

            int iVerifyStyle = 0;
            switch (cbVerifyStyle)
            {
                case 0: iVerifyStyle = 0; break;
                case 1: iVerifyStyle = 128; break;
                case 2: iVerifyStyle = 129; break;
                case 3: iVerifyStyle = 130; break;
                case 4: iVerifyStyle = 131; break;
                case 5: iVerifyStyle = 132; break;
                case 6: iVerifyStyle = 133; break;
                case 7: iVerifyStyle = 134; break;
                case 8: iVerifyStyle = 135; break;
                case 9: iVerifyStyle = 136; break;
                case 10: iVerifyStyle = 137; break;
                case 11: iVerifyStyle = 138; break;
                case 12: iVerifyStyle = 139; break;
                case 13: iVerifyStyle = 140; break;
                case 14: iVerifyStyle = 141; break;
                case 15: iVerifyStyle = 142; break;
            }

            if (axCZKEM1.SetUserInfoEx(GetMachineNumber(), Convert.ToInt32(sUserID), iVerifyStyle, ref bReserved))
            {
                //"Set verify style successfully!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            /*
            if (axCZKEM1.SetUserVerifyStyle(1, sUserID, iVerifyStyle, ref bReserved))
            {
                //"Set verify style successfully!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            */
            ////"[func SetUserVerifyStyle]Temporarily unsupported");


            return 1;
        }

        #endregion

        #region UesrFP
        public int sta_GetAllUserFPInfo(int prgSta, List<string> lvUserInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            string sEnrollNumber = "";
            bool bEnabled = false;
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            string sFPTmpData = "";
            string sCardnumber = "";
            int idwFingerIndex = 0;
            int iFlag = 0;
            int iFPTmpLength = 0;
            int i = 0;
            int num = 0;
            int iFpCount = 0;
            int index = 0;
            int xx = 1;

            lvUserInfo.Clear();

            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory  except fingerprint Templates
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                axCZKEM1.GetStrCardNumber(out sCardnumber);//get the card number from the memory             

                lvUserInfo.Add(sEnrollNumber);

                if (bEnabled == true)
                {
                    lvUserInfo[index] = lvUserInfo[index] + ";true";
                }
                else
                {
                    lvUserInfo[index] = lvUserInfo[index] + ";false";
                }
                lvUserInfo[index] = lvUserInfo[index] + ";"+sName;
                lvUserInfo[index] = lvUserInfo[index] + ";" + sCardnumber;
                lvUserInfo[index] = lvUserInfo[index] + ";" + sPassword;
                
                i = 0;
                xx = 1;
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sEnrollNumber, idwFingerIndex, out iFlag, out sFPTmpData, out iFPTmpLength))//get the corresponding templates string and length from the memory
                    {
                        if (xx == 1)
                        {
                            lvUserInfo[index] = lvUserInfo[index] + ";" + idwFingerIndex.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + iFlag.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + sFPTmpData.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + iPrivilege.ToString();
                        }
                        else
                        {
                            lvUserInfo.Add(sEnrollNumber);
                            if (bEnabled == true)
                            {
                                lvUserInfo[index] = lvUserInfo[index] + ";true";
                            }
                            else
                            {
                                lvUserInfo[index] = lvUserInfo[index] + ";false";
                            }

                            lvUserInfo[index] = lvUserInfo[index] + ";" + sName;
                            lvUserInfo[index] = lvUserInfo[index] + ";" + sCardnumber;
                            lvUserInfo[index] = lvUserInfo[index] + ";" + sPassword;
                            lvUserInfo[index] = lvUserInfo[index] + ";" + idwFingerIndex.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + iFlag.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + sFPTmpData.ToString();
                            lvUserInfo[index] = lvUserInfo[index] + ";" + iPrivilege.ToString();
                        }

                        index++;
                        xx = 0;
                        iFpCount++;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (i == 10)
                {
                    lvUserInfo[index] = lvUserInfo[index] + ";";
                    lvUserInfo[index] = lvUserInfo[index] + ";";
                    lvUserInfo[index] = lvUserInfo[index] + ";";
                    lvUserInfo[index] = lvUserInfo[index] + ";" + iPrivilege;
                    
                    index++;
                }
                num++;
                prgSta = num % 100;
            }
            prgSta = 100;
            //"Download user count : " + num.ToString() + " ,  fingerprint count : " + iFpCount.ToString());
            axCZKEM1.EnableDevice(iMachineNumber, true);
            return 1;
        }

        public int sta_SetAllUserFPInfo(int prgSta, List<string> lvUserInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (lvUserInfo.Count == 0)
            {
                //"*There is no data can upload!");
                return -1023;
            }

            string sEnrollNumber = "";
            string sEnabled = "";
            bool bEnabled = false;

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            string sFPTmpData = "";
            string sCardnumber = "";
            int idwFingerIndex = 0;
            string sdwFingerIndex = "";
            int iFlag = 0;
            string sFlag = "";
            int num = 0;

            prgSta = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvUserInfo.Count; i++)
            {
                sEnrollNumber = lvUserInfo[i].Split(';')[0];
                sEnabled = lvUserInfo[i].Split(';')[1];
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }
                sName = lvUserInfo[i].Split(';')[2];
                sCardnumber = lvUserInfo[i].Split(';')[3];
                sPassword = lvUserInfo[i].Split(';')[4];
                sdwFingerIndex = lvUserInfo[i].Split(';')[5];
                sFlag = lvUserInfo[i].Split(';')[6];
                sFPTmpData = lvUserInfo[i].Split(';')[7];
                iPrivilege = Convert.ToInt32(lvUserInfo[i].Split(';')[8]);

                if (sCardnumber != "" && sCardnumber != "0")
                {
                    axCZKEM1.SetStrCardNumber(sCardnumber);
                }
                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the device
                {
                    if (sdwFingerIndex != "" && sFlag != "" && sFPTmpData != "")
                    {
                        idwFingerIndex = Convert.ToInt32(sdwFingerIndex);
                        iFlag = Convert.ToInt32(sFlag);
                        axCZKEM1.SetUserTmpExStr(iMachineNumber, sEnrollNumber, idwFingerIndex, iFlag, sFPTmpData);//upload templates information to the device
                    }
                    num++;
                    prgSta = num % 100;
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    //"*Upload user " + sEnrollNumber + " error,ErrorCode=!" + idwErrorCode.ToString());
                    //axCZKEM1.EnableDevice(iMachineNumber, true);
                    //return -1022;
                }

            }
            prgSta = 100;

            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            //"Upload user successfully");

            return 1;
        }

        public int sta_batch_SetAllUserFPInfo(int prgSta, List<string> lvUserInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (lvUserInfo.Count == 0)
            {
                //"*There is no data can upload!");
                return -1023;
            }

            string sEnrollNumber = "";
            string sEnabled = "";
            bool bEnabled = false;

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            string sFPTmpData = "";
            string sCardnumber = "";
            int idwFingerIndex = 0;
            string sdwFingerIndex = "";
            int iFlag = 0;
            string sFlag = "";
            int num = 0;

            prgSta = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.BeginBatchUpdate(iMachineNumber, 1))//create memory space for batching data
            {
                for (int i = 0; i < lvUserInfo.Count; i++)
                {
                    sEnrollNumber = lvUserInfo[i].Split(';')[0];
                    sEnabled = lvUserInfo[i].Split(';')[1];
                    if (sEnabled == "true")
                    {
                        bEnabled = true;
                    }
                    else
                    {
                        bEnabled = false;
                    }
                    sName = lvUserInfo[i].Split(';')[2];
                    sCardnumber = lvUserInfo[i].Split(';')[3];
                    sPassword = lvUserInfo[i].Split(';')[4];
                    sdwFingerIndex = lvUserInfo[i].Split(';')[5];
                    sFlag = lvUserInfo[i].Split(';')[6];
                    sFPTmpData = lvUserInfo[i].Split(';')[7];
                    iPrivilege = Convert.ToInt32(lvUserInfo[i].Split(';')[8]);

                    if (sCardnumber != "" && sCardnumber != "0")
                    {
                        axCZKEM1.SetStrCardNumber(sCardnumber);
                    }
                    if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the device
                    {
                        if (sdwFingerIndex != "" && sFlag != "" && sFPTmpData != "")
                        {
                            idwFingerIndex = Convert.ToInt32(sdwFingerIndex);
                            iFlag = Convert.ToInt32(sFlag);
                            axCZKEM1.SetUserTmpExStr(iMachineNumber, sEnrollNumber, idwFingerIndex, iFlag, sFPTmpData);//upload templates information to the device
                        }
                        num++;
                        prgSta = num % 100;
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Upload user " + sEnrollNumber + " error,ErrorCode=!" + idwErrorCode.ToString());
                        //axCZKEM1.EnableDevice(iMachineNumber, true);
                        //return -1022;
                    }
                }
            }
            prgSta = 100;
            axCZKEM1.BatchUpdate(iMachineNumber);//upload all the information in the memory
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            //"Upload user successfully in batch");
            return 1;
        }

        #endregion

        #region UserFace
        public int sta_GetAllUserFaceInfo(int prgSta, List<string> lvUserInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            string sEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            int iFaceIndex = 50;//the only possible parameter value
            string sTmpData = "";
            int iLength = 0;
            int num = 0;
            int index = 0;

            lvUserInfo.Clear();

            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                if (axCZKEM1.GetUserFaceStr(iMachineNumber, sEnrollNumber, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                {
                    lvUserInfo.Add(sEnrollNumber);

                    if (bEnabled == true)
                    {
                        lvUserInfo[index] = lvUserInfo[index]+";true";
                    }
                    else
                    {
                        lvUserInfo[index] = lvUserInfo[index] + ";false";
                    }

                    lvUserInfo[index] = lvUserInfo[index] + ";"+ sName;
                    lvUserInfo[index] = lvUserInfo[index] + ";" + sPassword;
                    lvUserInfo[index] = lvUserInfo[index] + ";" + iPrivilege;
                    lvUserInfo[index] = lvUserInfo[index] + ";" + iLength;
                    lvUserInfo[index] = lvUserInfo[index] + ";" + sTmpData;
                    index++;
                    num++;
                }
                prgSta = num % 100;
            }
            prgSta = 100;
            //"Download user  face count : " + num.ToString());
            axCZKEM1.EnableDevice(iMachineNumber, true);
            return 1;
        }

        //Upload user's face templates to device
        public int sta_SetAllUserFaceInfo(int prgSta, List<string> lvUserInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (lvUserInfo.Count == 0)
            {
                //"*There is no data can upload!");
                return -1023;
            }

            string sEnrollNumber = "";
            string sEnabled = "";
            bool bEnabled = false;

            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            string sTmpData = "";
            int iLength = 0;
            int iFaceIndex = 50;
            int num = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvUserInfo.Count; i++)
            {
                sEnrollNumber = lvUserInfo[i].Split(';')[0];
                sEnabled = lvUserInfo[i].Split(';')[1];
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }
                sName = lvUserInfo[i].Split(';')[2];
                sPassword = lvUserInfo[i].Split(';')[3];
                iPrivilege = Convert.ToInt32(lvUserInfo[i].Split(';')[4]);
                iLength = Convert.ToInt32(lvUserInfo[i].Split(';')[5]);
                sTmpData = lvUserInfo[i].Split(';')[6];


                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the device
                {
                    axCZKEM1.SetUserFaceStr(iMachineNumber, sEnrollNumber, iFaceIndex, sTmpData, iLength);//upload face templates information to the device
                    num++;
                    prgSta = num % 100;
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    //"*Operation failed,ErrorCode=!" + idwErrorCode.ToString());
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    return -1022;
                }
            }
            prgSta = 100;

            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            //"Upload face successfully");
            return 1;
        }
        #endregion

        #region UserPhoto

        public int sta_DownloadAllUserPhoto( string txtAllPhotoPath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }


            if (txtAllPhotoPath.Trim() == "")
            {
                //"*Select photo path first.");
                return -1023;
            }

            int ret = 0;
            string photoPath = txtAllPhotoPath.Trim();

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetAllUserPhoto(GetMachineNumber(), photoPath))
            {
                ret = 1;
                //"Get All User Photo From the Device!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Download all user photo failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func GetAllUserPhoto]Temporarily unsupported");

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_UploadAllUserPhoto( string txtAllPhotoPath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtAllPhotoPath.Trim() == "")
            {
                //"*Select photo path first.");
                return -1023;
            }

            int ret = 0;
            string photoPath = txtAllPhotoPath.Trim();
            string fileName = "";


            //string[] gg = System.IO.Directory.GetDirectories(photoPath);
            //int num = gg.Length;

            //if (num <= 0)
            //{
            //    //"*There is no picture can update.");
            //    return -1023;
            //}

            DirectoryInfo Directory = new DirectoryInfo(photoPath);
            if (Directory.GetFiles().Length + Directory.GetDirectories().Length == 0)
            {
                //"*There is no picture can update.");
                return -1023;
            }

            axCZKEM1.EnableDevice(iMachineNumber, false);

            foreach (FileInfo NextFile in Directory.GetFiles())
            {
                fileName = photoPath + NextFile.Name;

                if (NextFile.Name.IndexOf(".jpg") > 0)
                {
                    if (axCZKEM1.UploadUserPhoto(GetMachineNumber(), fileName))
                    {
                        axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                        ret = 1;
                        //"Upload user photo to device successfully, file name : " + NextFile.Name);
                    }
                    else
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        ret = idwErrorCode;

                        if (idwErrorCode != 0)
                        {
                            //"*Upload user photo failed,ErrorCode: " + idwErrorCode.ToString());
                        }
                        else
                        {
                            //"No data from terminal returns!");
                        }
                    }
                }
                else
                {
                    if (NextFile.Name.IndexOf("Thumbs.db") < 0)
                    {
                        //"*Data format error,file name : " + NextFile.Name);
                    }
                }
            }

            ////"[func UploadUserPhoto]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_uploadOneUserPhoto( string fullName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.UploadUserPhoto(GetMachineNumber(), fullName))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                ret = 1;
                //"Upload User Photo To the Device succeed!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Upload user photo failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func UploadUserPhoto]Temporarily unsupported");
            //fullName);
            if (axCZKEM1.SendFile(GetMachineNumber(), fullName))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                ret = 1;
                //"Upload User Photo To the Device succeed!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Upload user photo failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_downloadOneUserPhoto( string userID, string path)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (path == "")
            {
                //"*Select photo path first.");
                return -1023;
            }

            if (userID == "")
            {
                //"*Input User ID first.");
                return -1022;
            }

            int ret = 0;
            string photoName = userID + ".jpg";

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.DownloadUserPhoto(GetMachineNumber(), photoName, path))
            {
                ret = 1;
                //"Download User Photo from the Device succeed!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"Download user photo failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func DownloadUserPhoto]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_DeleteOneUserPhoto( string userID)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (userID == "")
            {
                //"*Input User ID first.");
                return -1022;
            }

            int ret = 0;
            string photoName = userID + ".jpg";

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.DeleteUserPhoto(GetMachineNumber(), photoName))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                ret = 1;
                //"Delate User Photo in the Device succeed!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Delete user photo failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func DeleteUserPhoto]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        #endregion

        #region SMS

        public int sta_GetSMS(string txtSMSID, int cbTag, string txtValidMin, DateTime dtStartTime, string txtContent)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSMSID.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iSMSID = Convert.ToInt32(txtSMSID.ToString().Trim());
            int iTag = 0;
            int iValidMins = 0;
            string sStartTime = "";
            string sContent = "";

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetSMS(iMachineNumber, iSMSID, ref iTag, ref iValidMins, ref sStartTime, ref sContent))
            {
                switch (iTag)
                {
                    case 253: cbTag= 0; break;
                    case 254: cbTag= 1; break;
                    case 255: cbTag= 2; break;
                }

                txtSMSID = iSMSID.ToString();
                cbTag = iTag;
                txtValidMin = iValidMins.ToString();
                dtStartTime =Convert.ToDateTime(sStartTime);
                txtContent = sContent;
                //"Get SMS successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_SetSMS(string txtSMSID, int cbTag, string txtValidMin, DateTime dtStartTime, string txtContent)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSMSID.ToString().Trim() == "" || cbTag.ToString().Trim() == "" || txtValidMin.ToString().Trim() == "" || dtStartTime.ToString().Trim() == "" || txtContent.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            if (Convert.ToInt32(txtSMSID.Trim()) <= 0)
            {
                //"*SMS ID error!");
                return -1023;
            }

            if (Convert.ToInt32(txtValidMin.Trim()) < 0 || Convert.ToInt32(txtValidMin.Trim()) > 65535)
            {
                //"*Expired time error!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iSMSID = Convert.ToInt32(txtSMSID.Trim());
            int iTag = 0;
            int iValidMins = Convert.ToInt32(txtValidMin.Trim());
            string sStartTime = dtStartTime.ToString().Trim();
            string sContent = txtContent.Trim();
            string sTag = cbTag.ToString().Trim();

            for (iTag = 253; iTag <= 255; iTag++)
            {
                if (sTag.IndexOf(iTag.ToString()) > -1)
                {
                    break;
                }
            }

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SetSMS(iMachineNumber, iSMSID, iTag, iValidMins, sStartTime, sContent))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set the short message,you should refresh the data of the device
                //"Successfully set SMS! SMSType=" + iTag.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_SetUserSMS(string txtSMSID, int cbUserID)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSMSID.Trim() == "" || cbUserID.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iSMSID = Convert.ToInt32(txtSMSID.Trim());
            int iTag = 0;
            int iValidMins = 0;
            string sStartTime = "";
            string sContent = "";
            string sEnrollNumber = cbUserID.ToString().Trim();

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetSMS(iMachineNumber, iSMSID, ref iTag, ref iValidMins, ref sStartTime, ref sContent) == false)
            {
                //"*The SMSID doesn't exist!!");
                axCZKEM1.EnableDevice(iMachineNumber, true);
                return -1022;
            }

            if (iTag != 254)
            {
                //"*The SMS does not Personal SMS,please set it as Personal SMS first!!");
                axCZKEM1.EnableDevice(iMachineNumber, true);
                return -1022;
            }

            if (axCZKEM1.SSR_SetUserSMS(iMachineNumber, sEnrollNumber, iSMSID))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                //"Successfully set user SMS! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_DelSMS(string txtSMSID)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSMSID.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iSMSID = Convert.ToInt32(txtSMSID.Trim());

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.DeleteSMS(iMachineNumber, iSMSID))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                //"Successfully delete SMS! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_DelUserSMS(string txtSMSID, int cbUserID)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSMSID.Trim() == "" || cbUserID.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iSMSID = Convert.ToInt32(txtSMSID.Trim());
            string sEnrollNumber = cbUserID.ToString().Trim();

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_DeleteUserSMS(iMachineNumber, sEnrollNumber, iSMSID))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                //"Successfully delete user SMS! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_ClearSMS()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwErrorCode = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.ClearSMS(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                //"Successfully clear all the SMS! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_ClearUserSMS()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwErrorCode = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.ClearUserSMS(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                //"Successfully clear all the user SMS! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }
        #endregion

        #region Workcode
        public int sta_GetWorkCode( string txtWorkcodeID, string txtWorkcodeName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtWorkcodeID.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iWorkcodeID = Convert.ToInt32(txtWorkcodeID.Trim());
            string sName = "";

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_GetWorkCode(iWorkcodeID, out sName))
            {
                txtWorkcodeName = sName;
                //"Get workcode successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_SetWorkCode( string txtWorkcodeID, string txtWorkcodeName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtWorkcodeID.Trim() == "" || txtWorkcodeName.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iTmpID = 0;
            int iWorkcodeID = Convert.ToInt32(txtWorkcodeID.Trim());
            string sName = txtWorkcodeName.Trim();
            /*
            axCZKEM1.SSR_GetWorkCodeIDByName(iMachineNumber, sName, out iTmpID);
           
            
            if (iTmpID > 0)
            {
                //"*Workcode name duplicated!");
                return -1022;
            }
            

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_SetWorkCode(iWorkcodeID, sName))
            {
                //"Successfully set workcode");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            */
            ////"[func SSR_GetWorkCodeIDByName]Temporarily unsupported");
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_SetWorkCode(iWorkcodeID, sName))
            {
                //"Successfully set workcode");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_DelWorkCode( string txtWorkcodeID)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtWorkcodeID.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iWorkcodeID = Convert.ToInt32(txtWorkcodeID.Trim());

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_DeleteWorkCode(iWorkcodeID))
            {
                //"Successfully delete workcode");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_ClearWorkCode()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwErrorCode = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SSR_ClearWorkCode())
            {
                //"Successfully clear all workcode");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }
        #endregion

        #region user role

        public string[] sApp = new string[]
        {
            "usermng",
            "access",
            "iccardmng",
            "comset",
            "sysset",
            "myset",
            "datamng",
            "udiskmng",
            "logquery",
            "printset",
            "sms",
            "workcode",
            "autotest",
            "sysinfo"
        };

        public string[] sFunUserMng = new string[]
        {
            "adduser",
            "userlist",
            "userliststyle"
        };

        public string[] sFunAccess = new string[]
        {
            "timezone",
            "holiday",
            "group",
            "unlockcomb",
            "accparam",
            "duressalarm",
            "antipassbackset"
        };

        public string[] sFunICCard = new string[]
        {
            "enrollnumcard",
            "enrollfpcard",
            "clearcard",
            "copycard",
            "setcardparam"
        };

        public string[] sFunComm = new string[]
        {
            "netset",
            "serialset",
            "linkset",
            "mobilenet",
            "wifiset",
            "admsset",
            "wiegandset"
        };

        public string[] sFunSystem = new string[]
        {
            "timeset",
            "attparam",
            "fpparam",
            "restoreset",
            "udiskupgrade",
        };

        public string[] sFunPersonalize = new string[]
        {
            "displayset",
            "voiceset",
            "bellset",
            "shortcutsset",
            "statemodeset",
            "autopowerset"
        };

        public string[] sFunDataMng = new string[]
        {
            "cleardata",
            "backupdata",
            "restoredata"
        };

        public string[] sFunUSBMng = new string[]
        {
            "udiskupload",
            "udiskdownload",
            "udiskset"
        };

        public string[] sFunAttSearch = new string[]
        {
            "attlog",
            "attpic",
            "blacklistpic"
        };

        public string[] sFunPrint = new string[]
        {
            "printinfoset",
            "printfuncset"
        };

        public string[] sFunSMS = new string[]
        {
            "addsms",
            "smslist"
        };

        public string[] sFunWorkCode = new string[]
        {
            "addworkcode",
            "workcodelist",
            "workcodesetting"
        };

        public string[] sFunAutoTest = new string[]
        {
            "alltest",
            "screentest",
            "voicetest",
            "keytest",
            "fptest",
            "realtimetest",
            "cameratest"
        };

        public string[] sFunSysInfo = new string[]
        {
            "datacapacity",
            "devinfo",
            "firmwareinfo"
        };

        public int sta_GetUserRole(int cbUserRole, int[] iAppState, int[] iFunUserMng, int[] iFunAccess, int[] iFunICCard, int[] iFunComm, int[] iFunSystem, int[] iFunPersonalize, int[] iFunDataMng, int[] iFunUSBMng, int[] iFunAttSearch, int[] iFunPrint, int[] iFunSMS, int[] iFunWorkCode, int[] iFunAutoTest, int[] iFunSysInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUserRole.ToString() == "")
            {
                //"*Please input user role!");
                return -1023;
            }

            int iPrivilege = cbUserRole;
            /*
                        bool bFlag = false;
                        if (iPrivilege == 2)
                        {
                            axCZKEM1.IsUserDefRoleEnable(iMachineNumber, 4, out bFlag);

                            if (bFlag == false)
                            {
                                //"*User Defined Role is unable!");
                                return -1023;
                            }
                        }
            */
            int idwErrorCode = 0;
            string sAppName = "";
            string sFunName = "";
            int i = 0, j = 1;
            int l = 0, k = 1;
            int iUserRole = 0;
            /*
                        switch (cbUserRole.SelectedIndex)
                        {
                            case 0: iUserRole = 1; break;
                            case 1: iUserRole = 2; break;
                            case 2: iUserRole = 4; break;
                        }

                        axCZKEM1.EnableDevice(iMachineNumber, false);

                        if (axCZKEM1.GetAppOfRole(iMachineNumber, iUserRole, out sAppName))
                        {
                            if (axCZKEM1.GetFunOfRole(iMachineNumber, iUserRole, out sFunName))
                            {
                                string[] sTmp = Regex.Split(sAppName, "\r\n", RegexOptions.None);
                                string[] sTmp1 = Regex.Split(sFunName, "\r\n", RegexOptions.None);

                                for (l = 1; l < sTmp.Length; l++)
                                {
                                    for (k = 0; k < sApp.Length; k++)
                                    {
                                        if (string.Compare(sTmp[l].ToString(), sApp[k].ToString()) == 0)
                                        {
                                            iAppState[k] = 1;
                                            switch (k)
                                            {
                                                case 0:
                                                    {
                                                        for (i = 0; i < sFunUserMng.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunUserMng[i].ToString()) == 0)
                                                                {
                                                                    iFunUserMng[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 1:
                                                    {
                                                        for (i = 0; i < sFunAccess.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunAccess[i].ToString()) == 0)
                                                                {
                                                                    iFunAccess[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        for (i = 0; i < sFunICCard.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunICCard[i].ToString()) == 0)
                                                                {
                                                                    iFunICCard[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        for (i = 0; i < sFunComm.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunComm[i].ToString()) == 0)
                                                                {
                                                                    iFunComm[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        for (i = 0; i < sFunSystem.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunSystem[i].ToString()) == 0)
                                                                {
                                                                    iFunSystem[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 5:
                                                    {
                                                        for (i = 0; i < sFunPersonalize.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunPersonalize[i].ToString()) == 0)
                                                                {
                                                                    iFunPersonalize[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 6:
                                                    {
                                                        for (i = 0; i < sFunDataMng.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunDataMng[i].ToString()) == 0)
                                                                {
                                                                    iFunDataMng[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 7:
                                                    {
                                                        for (i = 0; i < sFunUSBMng.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunUSBMng[i].ToString()) == 0)
                                                                {
                                                                    iFunUSBMng[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 8:
                                                    {
                                                        for (i = 0; i < sFunAttSearch.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunAttSearch[i].ToString()) == 0)
                                                                {
                                                                    iFunAttSearch[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 9:
                                                    {
                                                        for (i = 0; i < sFunPrint.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunPrint[i].ToString()) == 0)
                                                                {
                                                                    iFunPrint[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 10:
                                                    {
                                                        for (i = 0; i < sFunSMS.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunSMS[i].ToString()) == 0)
                                                                {
                                                                    iFunSMS[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 11:
                                                    {
                                                        for (i = 0; i < sFunWorkCode.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunWorkCode[i].ToString()) == 0)
                                                                {
                                                                    iFunWorkCode[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 12:
                                                    {
                                                        for (i = 0; i < sFunAutoTest.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunAutoTest[i].ToString()) == 0)
                                                                {
                                                                    iFunAutoTest[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                case 13:
                                                    {
                                                        for (i = 0; i < sFunSysInfo.Length; i++)
                                                        {
                                                            for (j = 1; j < sTmp1.Length; j++)
                                                            {
                                                                if (string.Compare(sTmp1[j].ToString(), sFunSysInfo[i].ToString()) == 0)
                                                                {
                                                                    iFunSysInfo[i] = 1;
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    }
                                                default: break;
                                            }
                                            break;
                                        }
                                    }
                                }
                                axCZKEM1.RefreshData(iMachineNumber);//After you have set user short message,you should refresh the data of the device
                                //"Get user role successfully! ");
                            }
                            else
                            {
                                axCZKEM1.GetLastError(ref idwErrorCode);
                                //"*Get sub menu failed,ErrorCode=" + idwErrorCode.ToString());

                                return 1;
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            //"*Get top menu failed,ErrorCode=" + idwErrorCode.ToString());
                        }
                        */
            //"[func GetAppOfRole]Temporarily unsupported");
            //axCZKEM1.EnableDevice(iMachineNumber, true);

            return 1;
        }

        public int sta_SetUserRole(int cbUserRole, int[] iAppState, int[] iFunUserMng, int[] iFunAccess, int[] iFunICCard, int[] iFunComm, int[] iFunSystem, int[] iFunPersonalize, int[] iFunDataMng, int[] iFunUSBMng, int[] iFunAttSearch, int[] iFunPrint, int[] iFunSMS, int[] iFunWorkCode, int[] iFunAutoTest, int[] iFunSysInfo)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbUserRole.ToString() == "")
            {
                //"*Please input user role!");
                return -1023;
            }

            int iPrivilege = cbUserRole;
            /*
                        bool bFlag = false;
                        if (iPrivilege == 2)
                        {
                            axCZKEM1.IsUserDefRoleEnable(iMachineNumber, 4, out bFlag);

                            if (bFlag == false)
                            {
                                //"*User Defined Role is unable!");
                                return -1023;
                            }
                        }
            */
            int idwErrorCode = 0;
            int iUserRole = 0;
            int dd = 0;

            /*
            //SDK支持
            switch (cbUserRole.SelectedIndex)
            {
                case 0: iUserRole = 1; break;
                case 1: iUserRole = 2; break;
                case 2: iUserRole = 4; break;
            }
           
            for (int i = 0; i < iFunUserMng.Length; i++)
            {
                if (iFunUserMng[i] == 1)
                {
                    if (!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[0], sFunUserMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set User Mgt menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[0], sFunUserMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set User Mgt menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
            }

            for (int i = 0; i < iFunAccess.Length; i++)
            {
                if (iFunAccess[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[1], sFunAccess[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Access Control menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[1], sFunAccess[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Access Control menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
            }

            for (int i = 0; i < iFunICCard.Length; i++)
            {
                if (iFunICCard[i] == 1)
                {
                    if (!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[2], sFunICCard[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set IC Card menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[2], sFunICCard[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set IC Card menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
            }

            for (int i = 0; i < iFunComm.Length; i++)
            {
                if (iFunComm[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[3], sFunComm[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Comm menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[3], sFunComm[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Comm menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
            }

            for (int i = 0; i < iFunSystem.Length; i++)
            {
                if (iFunSystem[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[4], sFunSystem[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set System menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[4], sFunSystem[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set System menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunPersonalize.Length; i++)
            {
                if (iFunPersonalize[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[5], sFunPersonalize[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Personalize menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[5], sFunPersonalize[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Personalize menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunDataMng.Length; i++)
            {
                if (iFunDataMng[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[6], sFunDataMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Data Mgt menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[6], sFunDataMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Data Mgt menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunUSBMng.Length; i++)
            {
                if (iFunUSBMng[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[7], sFunUSBMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set USB Manager menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[7], sFunUSBMng[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set USB Manager menu failed,menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunAttSearch.Length; i++)
            {
                if (iFunAttSearch[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[8], sFunAttSearch[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Attendance menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[8], sFunAttSearch[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Attendance menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunPrint.Length; i++)
            {
                if (iFunPrint[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[9], sFunPrint[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Print menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[9], sFunPrint[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Print menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunSMS.Length; i++)
            {
                if (iFunSMS[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[10], sFunSMS[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Short Message menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[10], sFunSMS[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Short Message menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd ++;
                    }
                }
            }

            for (int i = 0; i < iFunWorkCode.Length; i++)
            {
                if (iFunWorkCode[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[11], sFunWorkCode[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Work Code menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[11], sFunWorkCode[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Work Code menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunAutoTest.Length; i++)
            {
                if (iFunAutoTest[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[12], sFunAutoTest[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Auto Test menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[12], sFunAutoTest[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set Auto Test menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            for (int i = 0; i < iFunSysInfo.Length; i++)
            {
                if (iFunSysInfo[i] == 1)
                {
                    if(!axCZKEM1.SetPermOfAppFun(iMachineNumber, iUserRole, sApp[13], sFunSysInfo[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set System Info menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
                else
                {
                    if (!axCZKEM1.DeletePermOfAppFun(iMachineNumber, iUserRole, sApp[13], sFunSysInfo[i]))
                    {
                        axCZKEM1.GetLastError(ref idwErrorCode);
                        //"*Set System Info menu failed,sub menu index=" + i.ToString() + ",ErrorCode=" + idwErrorCode.ToString());
                        dd++;
                    }
                }
            }

            if (dd == 0)
            {
                //"Set User Role successfully~");
            }
            */
            //"[func SetPermOfAppFun]Temporarily unsupported");
            return 1;
        }
        #endregion
        #region UserBio

        /*
        public void connectDevice(string ip, int port, int commKey)
        {
            
            axCZKEM1.SetCommPassword(commKey);
            connected = axCZKEM1.Connect_Net(ip, port);
            if (connected)
            {
                sta_getBiometricType();
            }
        }

        public void disconnectDevice()
        {
            if (connected) axCZKEM1.Disconnect();
        }
        */

        private string sta_getSysOptions(string option)
        {
            string value = string.Empty;
            axCZKEM1.GetSysOption(iMachineNumber, option, out value);
            return value;
        }

        /// <summary>
        /// get version
        /// </summary>
        /// <returns></returns>
        public void sta_getBiometricVersion()
        {
            string result = string.Empty;
            _biometricVersion = sta_getSysOptions("BiometricVersion");
        }

        /// <summary>
        /// get support type
        /// </summary>
        /// <returns></returns>
        public void sta_getBiometricType()
        {
            string result = string.Empty;
            result = sta_getSysOptions("BiometricType");
            if (!string.IsNullOrEmpty(result))
            {
                _supportBiometricType.fp_available = result[1] == '1';
                _supportBiometricType.face_available = result[2] == '1';
                if (result.Length >= 9)
                {
                    _supportBiometricType.fingerVein_available = result[7] == '1';
                    _supportBiometricType.palm_available = result[8] == '1';
                }
            }
            _biometricType = result;
        }

        public List<Employee> sta_getEmployees()
        {
            if (!GetConnectState())
            {
                return new List<Employee>();
            }
            List<Employee> employees = new List<Employee>();

            string empnoStr = string.Empty;
            string name = string.Empty;
            string pwd = string.Empty;
            int pri = 0;
            bool enable = true;
            string cardNum = string.Empty;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            try
            {
                axCZKEM1.ReadAllUserID(iMachineNumber);

                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out empnoStr, out name, out pwd, out pri, out enable))
                {
                    cardNum = "";
                    if (axCZKEM1.GetStrCardNumber(out cardNum))
                    {
                        if (string.IsNullOrEmpty(cardNum))
                            cardNum = "";
                    }
                    if (!string.IsNullOrEmpty(name))
                    {
                        int index = name.IndexOf("\0");
                        if (index > 0)
                        {
                            name = name.Substring(0, index);
                        }
                    }

                    Employee emp = new Employee();
                    emp.pin = empnoStr;
                    emp.name = name;
                    emp.privilege = pri;
                    emp.password = pwd;
                    emp.cardNumber = cardNum;

                    employees.Add(emp);
                }
            }
            catch
            {

            }
            finally
            {
                axCZKEM1.EnableDevice(iMachineNumber, true);
            }
            return employees;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bioTemplate"></param>
        private void sta_getBioTemplateFromBuffer(string buffer, ref BioTemplate bioTemplate)
        {
            string temp;
            for (int i = 1; i <= 10; i++)
            {
                if (buffer.IndexOf(',') > 0)
                {
                    temp = buffer.Substring(0, buffer.IndexOf(','));
                }
                else
                {
                    temp = buffer;
                }

                switch (i)
                {
                    case 1:
                        bioTemplate.pin = temp;
                        break;
                    case 2:
                        bioTemplate.valid_flag = int.Parse(temp);
                        break;
                    case 3:
                        bioTemplate.is_duress = int.Parse(temp);
                        break;
                    case 4:
                        bioTemplate.bio_type = int.Parse(temp);
                        break;
                    case 5:
                        bioTemplate.version = temp;
                        break;
                    case 6:
                        bioTemplate.version = bioTemplate.version + "." + temp;
                        break;
                    case 7:
                        bioTemplate.data_format = int.Parse(temp);
                        break;
                    case 8:
                        bioTemplate.template_no = int.Parse(temp);
                        break;
                    case 9:
                        bioTemplate.template_no_index = int.Parse(temp);
                        break;
                    case 10:
                        bioTemplate.template_data = temp;
                        break;
                }

                buffer = buffer.Substring(buffer.IndexOf(',') + 1);
            }
        }

        /// <summary>
        /// get template
        /// </summary>
        /// <param name="aBioType">
        /// <returns></returns>
        private List<string> sta_batchDownloadBioTemplates(int procBar, int aBioType)
        {
            int tempNum = 50;
            int tn = 2;
            List<string> bufferList = new List<string>();
            foreach (Employee e in employeeList)
            {
                string filter;
                if (aBioType == 1)
                    filter = string.Format("Type={0}", aBioType);
                else
                    filter = string.Format("Pin={0}\tType={1}", e.pin, aBioType);

                int dataCount = axCZKEM1.SSR_GetDeviceDataCount(PersBioTableName, filter, string.Empty);
                int nC = aBioType == 1 ? 1 : aBioType == 2 ? 12 : aBioType == 7 ? 3 : aBioType == 8 ? 6 : 0;

                string strOffBuffer = string.Empty;
                int eBufferSize = 0;
                bool result = true;
                int n = 0;

                while (true)
                {
                    n = 0;
                    strOffBuffer = string.Empty;
                    eBufferSize = dataCount * 3500 * nC;
                    string option = string.Empty;
                    try
                    {
                        result = axCZKEM1.SSR_GetDeviceData(iMachineNumber, out strOffBuffer, eBufferSize,
                            PersBioTableName, PersBioTableFields, filter, option);
                    }
                    catch
                    {
                        result = false;
                        int errorCode = 0;
                        axCZKEM1.GetLastError(ref errorCode);
                    }
                    if (result) break;
                    if (!result && n == 2) break;
                    n++;
                }
                if (result)
                {
                    bufferList.Add(strOffBuffer);
                    if ((bufferList.Count / tempNum) >= 0)
                    {
                        procBar = tn;
                        tn += tn;
                        if (tn >= 90)
                            tn = 90;
                        tempNum = tempNum + 50;
                    }
                }
                if (aBioType == 1)   //表示指纹模板获取
                {
                    break;
                }
            }
            return bufferList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aBioType"></param>
        /// <returns></returns>
        public List<BioTemplate> sta_BatchGetBioTemplates(int procBar, int aBioType)
        {
            List<BioTemplate> bioTemplateList = new List<BioTemplate>();
            List<string> bufferList = sta_batchDownloadBioTemplates(procBar, aBioType);
            for (int i = 0; i < bufferList.Count; i++)
            {
                string[] buffers = bufferList[i].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 1; j < buffers.Length; j++)
                {
                    BioTemplate bioTemplate = new BioTemplate();
                    sta_getBioTemplateFromBuffer(buffers[j], ref bioTemplate);
                    bioTemplateList.Add(bioTemplate);
                }
            }

            return bioTemplateList;
        }

        private string sta_AssemblesAllUserBioTemplateInfo(List<BioTemplate> bioTemplateList, int aBioType, string version)
        {
            List<BioTemplate> uploadBioTemplateList = bioTemplateList.FindAll(t => t.bio_type == aBioType && t.version.Equals(version));

            string bioTemplateVersion = string.Empty;
            string eMajorVer = string.Empty;
            string eMinorVer = string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (BioTemplate template in uploadBioTemplateList)
            {
                bioTemplateVersion = template.version;
                if (bioTemplateVersion.IndexOf('.') < 0) bioTemplateVersion = bioTemplateVersion + ".0";
                eMajorVer = bioTemplateVersion.Substring(0, bioTemplateVersion.IndexOf('.'));
                eMinorVer = bioTemplateVersion.Substring(bioTemplateVersion.IndexOf('.') + 1);
                result.Append(string.Format("Pin={0}\tValid={1}\tDuress={2}\tType={3}\tMajorVer={4}\tMinorVer={5}\tFormat={6}\tNo={7}\tIndex={8}\tTmp={9}\r\n",
                    template.pin, template.valid_flag, template.is_duress, template.bio_type, eMajorVer, eMinorVer, template.data_format, template.template_no,
                    template.template_no_index, template.template_data));
            }
            return result.ToString();
        }

        public void sta_setBioTemplates(List<BioTemplate> bioTemplateList, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(_biometricVersion)) sta_getBiometricVersion();
            if (string.IsNullOrEmpty(_biometricType)) sta_getBiometricType();
            string[] versions = _biometricVersion.Split(':');

            StringBuilder errorMsg = new StringBuilder();
            for (int i = 0; i < _biometricType.Length; i++)
            {
                if (_biometricType[i] == '1')
                {
                    string buffer = sta_AssemblesAllUserBioTemplateInfo(bioTemplateList, i, versions[i]);
                    try
                    {
                        int errorCode = 0;
                        bool result = true;
                        if (!string.IsNullOrEmpty(buffer))
                        {
                            result = axCZKEM1.SSR_SetDeviceData(1, PersBioTableName, buffer, string.Empty);
                        }

                        if (!result)
                        {
                            axCZKEM1.GetLastError(ref errorCode);
                            errorMsg.Append(string.Format(" errorcode={0} ", errorCode));
                        }
                    }
                    catch (Exception e)
                    {
                        errorMsg.Append(e.Message);
                    }
                }
            }
            axCZKEM1.RefreshData(iMachineNumber);
            axCZKEM1.EnableDevice(iMachineNumber, true);
        }

        public void sta_setEmployees(List<Employee> employees)
        {
            axCZKEM1.EnableDevice(1, false);
            try
            {
                bool batchUpdate = axCZKEM1.BeginBatchUpdate(iMachineNumber, 1);
                foreach (Employee emp in employees)
                {
                    axCZKEM1.SetStrCardNumber(emp.cardNumber);
                    axCZKEM1.SSR_SetUserInfo(iMachineNumber, emp.pin, emp.name, emp.password, emp.privilege, true);
                }
                if (batchUpdate)
                {
                    axCZKEM1.BatchUpdate(iMachineNumber);
                    batchUpdate = false;
                }
            }
            catch
            { }
            finally
            {
                axCZKEM1.EnableDevice(iMachineNumber, true);
            }
        }

        #endregion

        public int sta_GetAllUserID(bool bEnable, int cbUserID, int cbUserID1, int cbUserID2, int cbUserID3, int cbUserID4, int txtID2, int cbUserID7)
        {
            List<string> listOfUsers = new List<string>();
            if (GetConnectState() == false)
            {
                return -1024;
            }

            string sEnrollNumber = "";
            bool bEnabled = false;
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;

            if (bAddControl == true || bEnable == true)
            {
                axCZKEM1.EnableDevice(iMachineNumber, false);
                axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    listOfUsers.Add(sEnrollNumber);
                    listOfUsers.Add(sEnrollNumber);
                    listOfUsers.Add(sEnrollNumber);
                    listOfUsers.Add(sEnrollNumber);
                    listOfUsers.Add(sEnrollNumber);
                    //txtID2 = sEnrollNumber;
                    listOfUsers.Add(sEnrollNumber);
                }

                axCZKEM1.EnableDevice(iMachineNumber, true);
            }

            bAddControl = false;
            bEnable = false;

            return 1;
        }

        #endregion

        #region PersonalizeMng
        public int sta_GetAllBellData(DataTable dt_allBell)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            int weekDay;
            int Index;
            int Enable;
            int Hour;
            int min;
            int voice;
            int way;
            int inerbelldelay;
            int extbelldelay;

            if (axCZKEM1.ReadAllBellSchData(GetMachineNumber()))//read all the bell schedual data records to the memory
            {
                while (axCZKEM1.GetEachBellInfo(GetMachineNumber(), out weekDay, out Index, out Enable, out Hour, out min, out voice, out way, out inerbelldelay, out extbelldelay))
                {
                    DataRow dr = dt_allBell.NewRow();
                    dr["ID"] = Index;
                    dr["Enable"] = Enable;
                    dr["Time"] = Hour + ":" + min;
                    dr["WaveIndex"] = voice;
                    dr["BellType"] = way;
                    if (way == 0)
                    {
                        dr["InerDelay"] = inerbelldelay;
                        dr["ExtDelay"] = 0;
                    }
                    else if (way == 1)
                    {
                        dr["InerDelay"] = 0;
                        dr["ExtDelay"] = extbelldelay;
                    }
                    else
                    {
                        dr["InerDelay"] = inerbelldelay;
                        dr["ExtDelay"] = extbelldelay;
                    }

                    dr["Mon"] = (weekDay & (1 << 0)) > 0 ? 1 : 0;
                    dr["Tue"] = (weekDay & (1 << 1)) > 0 ? 1 : 0;
                    dr["Wed"] = (weekDay & (1 << 2)) > 0 ? 1 : 0;
                    dr["Thu"] = (weekDay & (1 << 3)) > 0 ? 1 : 0;
                    dr["Fri"] = (weekDay & (1 << 4)) > 0 ? 1 : 0;
                    dr["Sat"] = (weekDay & (1 << 5)) > 0 ? 1 : 0;
                    dr["Sun"] = (weekDay & (1 << 6)) > 0 ? 1 : 0;
                    dt_allBell.Rows.Add(dr);
                }
                ret = 1;
                //"Get bell successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Read all bell schedual data failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        public int sta_setBellInfo(int weekday, int index, int Enable, int Hour, int min, int voice, int bellway, int inerbelldelay, int extbelldelay)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int iCurBellCount, tmpWeekday = 0, tmpEnable = 0, tmpHour = 0, tmpMin = 0, tmpVoice = 0, tmpBellway = 0, tmpInerbelldelay = 0, tmpExtbelldelay = 0;

            if (axCZKEM1.GetBellSchDataEx(GetMachineNumber(), tmpWeekday, index, out tmpEnable, out tmpHour, out tmpMin, out tmpVoice, out tmpBellway, out tmpInerbelldelay, out tmpExtbelldelay) == false)
            {
                axCZKEM1.GetDayBellSchCount(GetMachineNumber(), out iCurBellCount);
                if (iCurBellCount >= 64)
                {
                    //"*The bell count is 64!!!");
                    return -1023;
                }
            }

            int iIsSupportExAlarm = 0;

            axCZKEM1.GetDeviceInfo(GetMachineNumber(), 79, ref iIsSupportExAlarm);

            if (iIsSupportExAlarm <= 0 && (bellway == 1 || bellway == 2))
            {
                //"*The Device does not support external bell!");
                return -1022;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device


            if (axCZKEM1.SetBellSchDataEx(GetMachineNumber(), weekday, index, Enable, Hour, min, voice, bellway, inerbelldelay, extbelldelay))
            {
                axCZKEM1.RefreshData(GetMachineNumber());
                ret = 1;
                //"Set Bell Schedule successfully!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Set bell info failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        private void getShortkeyName(string shortkeyList, List<string> cb_stKey)
        {
            int tmpIndex = 0;
            string shortkeyName = "";

            tmpIndex = shortkeyList.IndexOf("\r\n", 1);
            shortkeyName = shortkeyList.Substring(0, tmpIndex);
            shortkeyList = shortkeyList.Substring(tmpIndex + 2);

            cb_stKey.Add(shortkeyName);

            if (shortkeyList.Length > 2)
                getShortkeyName(shortkeyList, cb_stKey);
        }

        private void getFuncName(string funcList, List<string> cb_function)
        {

            int tmpIndex1 = 0;
            int tmpIndex2 = 0;
            string funcName = "";
            int funcID = 0;

            //tmpIndex1 = shortkeyList.IndexOf("\r\n", 1);
            //tmpIndex2 = shortkeyList.IndexOf(",", 1);
            //funcID = Convert.ToInt32(shortkeyList.Substring(0, tmpIndex2));
            //funcName = shortkeyList.Substring(tmpIndex2 + 1, tmpIndex1 - tmpIndex2 - 1);
            //shortkeyList = shortkeyList.Substring(tmpIndex1 + 2);

            tmpIndex1 = funcList.IndexOf("\r\n", 1);
            tmpIndex2 = funcList.IndexOf(",", 1);
            funcName = funcList.Substring(tmpIndex2 + 1, tmpIndex1 - tmpIndex2 - 1);
            funcList = funcList.Substring(tmpIndex1 + 2);

            cb_function.Add(funcName);

            if (funcList.Length > 2)
                getFuncName(funcList, cb_function);
        }

        public int sta_getAllShortkeyFunctionName(List<string> cb_stKey, List<string> cb_function)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            string shortkeyList = "";
            int shortkeyBufSize = 1024;
            string functionList = "";
            int funtionBufSize = 1024;

            if (axCZKEM1.GetAllSFIDName(GetMachineNumber(), out shortkeyList, shortkeyBufSize, out functionList, funtionBufSize))
            {
                cb_stKey.Clear();
                shortkeyList = shortkeyList.Substring(shortkeyList.IndexOf("\r\n", 1) + 2);
                getShortkeyName(shortkeyList, cb_stKey);

                cb_function.Clear();
                cb_function.Add("state key");
                functionList = functionList.Substring(functionList.IndexOf("\r\n", 1) + 2);
                getFuncName(functionList, cb_function);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Read all ShortkeyName FunctionName data failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        public int sta_getShortkeyByID(int ShortKeyID, ref string ShortKeyName, ref string FunctionName, ref int ShortKeyFun, ref int stateCode, ref string stateName, ref string description, ref int intAutoChange, ref string strAutoChangeTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            if (axCZKEM1.GetShortkey(GetMachineNumber(), ShortKeyID, ref ShortKeyName, ref FunctionName, ref ShortKeyFun, ref stateCode, ref stateName, ref description, ref intAutoChange, ref strAutoChangeTime))
            {
                //"Get shortkey successfully. Name:" + ShortKeyName + ",FunctionName:" + FunctionName);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*GetShortkeyByID failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    ret = -1;
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        public int sta_setShortkey(int ShortKeyID, string ShortKeyName, string FunctionName, int ShortKeyFun, int stateCode, string stateName, string description, int intAutoChange, string strAutoChangeTime)
        {

            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            if (axCZKEM1.SetShortkey(GetMachineNumber(), ShortKeyID, ShortKeyName, FunctionName, ShortKeyFun, stateCode, stateName, description, intAutoChange, strAutoChangeTime))
            {
                //"Set shortkey successfully. Name:" + ShortKeyName + ",FunctionName:" + FunctionName);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*SetShortkey failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }


            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }


        public int sta_uploadAdvertisePicture(string pictureFile, string pictureName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            if (axCZKEM1.UploadPicture(GetMachineNumber(), pictureFile, pictureName))
            {
                ret = 1;
                //"Update a advertise picture!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Upload advertise picture failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        public int sta_uploadWallpaper( string pictureFile, string pictureName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;
            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            if (axCZKEM1.UploadTheme(GetMachineNumber(), pictureFile, pictureName))
            {
                ret = 1;
                //"Update a wallpaper!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Upload wallpaper failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device
            return ret;
        }

        #endregion

        #region DataMng

        #region  AttLogMng

        public int sta_readAttLog( DataTable dt_log)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            if (axCZKEM1.ReadGeneralLogData(GetMachineNumber()))
            {
                while (axCZKEM1.SSR_GetGeneralLogData(GetMachineNumber(), out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    DataRow dr = dt_log.NewRow();
                    dr["User ID"] = sdwEnrollNumber;
                    dr["Verify Date"] = idwYear + "-" + idwMonth + "-" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond;
                    dr["Verify Type"] = idwVerifyMode;
                    dr["Verify State"] = idwInOutMode;
                    dr["WorkCode"] = idwWorkcode;
                    dt_log.Rows.Add(dr);
                }
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Read attlog failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_readLogByPeriod( DataTable dt_logPeriod, string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;


            if (axCZKEM1.ReadTimeGLogData(GetMachineNumber(), fromTime, toTime))
            {
                while (axCZKEM1.SSR_GetGeneralLogData(GetMachineNumber(), out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    DataRow dr = dt_logPeriod.NewRow();
                    dr["User ID"] = sdwEnrollNumber;
                    dr["Verify Date"] = idwYear + "-" + idwMonth + "-" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond;
                    dr["Verify Type"] = idwVerifyMode;
                    dr["Verify State"] = idwInOutMode;
                    dr["WorkCode"] = idwWorkcode;
                    dt_logPeriod.Rows.Add(dr);
                }
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Read attlog by period failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }


            ////"[func ReadTimeGLogData]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_DeleteAttLog()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device


            if (axCZKEM1.ClearGLog(GetMachineNumber()))
            {
                axCZKEM1.RefreshData(GetMachineNumber());
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Delete attlog, ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_DeleteAttLogByPeriod( string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device


            if (axCZKEM1.DeleteAttlogBetweenTheDate(GetMachineNumber(), fromTime, toTime))
            {
                axCZKEM1.RefreshData(GetMachineNumber());
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Delete attlog by period failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func DeleteAttlogBetweenTheDate]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_DelOldAttLogFromTime( string fromTime)
        {
            if (GetConnectState() == false)
            {
                //"Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device


            if (axCZKEM1.DeleteAttlogByTime(GetMachineNumber(), fromTime))
            {
                axCZKEM1.RefreshData(GetMachineNumber());
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Delete old attlog from time failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func DeleteAttlogByTime]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_ReadNewAttLog( DataTable dt_logNew)
        {
            if (GetConnectState() == false)
            {
                //"Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);//disable the device

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;


            if (axCZKEM1.ReadNewGLogData(GetMachineNumber()))
            {
                while (axCZKEM1.SSR_GetGeneralLogData(GetMachineNumber(), out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    DataRow dr = dt_logNew.NewRow();
                    dr["User ID"] = sdwEnrollNumber;
                    dr["Verify Date"] = idwYear + "-" + idwMonth + "-" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond;
                    dr["Verify Type"] = idwVerifyMode;
                    dr["Verify State"] = idwInOutMode;
                    dr["WorkCode"] = idwWorkcode;
                    dt_logNew.Rows.Add(dr);
                }
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Read attlog by period failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            ////"[func ReadNewGLogData]Temporarily unsupported");
            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }
        #endregion

        #region  AttPhotoMng
        public int sta_GetAllAttPhoto(string photoPath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 0, "", "", out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                int len = AllPhotoName.Length;
                char[] allphotoname = AllPhotoName.ToCharArray();

                string finalPath = photoPath + "ALL\\";
                System.IO.Directory.CreateDirectory(finalPath);

                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t" && allphotoname[j].ToString() != "\n")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData),file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"Get All ATT photo succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_GetAllAttPhotoByTimePeriod(string photoPath, string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            //fromTime + "-----" + toTime);
            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 1, fromTime, toTime, out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                int len = AllPhotoName.Length;
                char[] allphotoname = AllPhotoName.ToCharArray();

                string finalPath = photoPath + "ALL" + "-From" + fromTime.Replace(":", ".") + "-To" + toTime.Replace(":", ".") + "\\";
                System.IO.Directory.CreateDirectory(finalPath);

                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t" && allphotoname[j].ToString() != "\n")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData), file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"GetAllAttPhotoByTimePeriod succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_GetAllPassPhoto(string photoPath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 0, "", "", out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                string[] Allphotoname = AllPhotoName.Split('\n');
                int len = Allphotoname[0].Length;
                char[] allphotoname = Allphotoname[0].ToCharArray();

                string finalPath = photoPath + "PASS\\";
                System.IO.Directory.CreateDirectory(finalPath);

                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData), file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"Get All PASS photo succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_GetPassPhotoByTimePeriod( string photoPath, string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 1, fromTime, toTime, out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                string[] Allphotoname = AllPhotoName.Split('\n');
                int len = Allphotoname[0].Length;
                char[] allphotoname = Allphotoname[0].ToCharArray();

                string finalPath = photoPath + "PASS" + "-From" + fromTime.Replace(":", ".") + "-To" + toTime.Replace(":", ".") + "\\";
                System.IO.Directory.CreateDirectory(finalPath);

                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData), file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"GetPassPhotoByTimePeriod succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_GetAllBadPhoto( string photoPath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 0, "", "", out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                string[] Allphotoname = AllPhotoName.Split('\n');
                int len = Allphotoname[1].Length;
                char[] allphotoname = Allphotoname[1].ToCharArray();

                string finalPath = photoPath + "BAD\\";
                System.IO.Directory.CreateDirectory(finalPath);


                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData), file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"Get All BAD photo succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_GetBadPhotoByTimePeriod( string photoPath, string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);


            string AllPhotoName = "";
            if (!axCZKEM1.GetPhotoNamesByTime(GetMachineNumber(), 1, fromTime, toTime, out AllPhotoName))
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get photo name failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }
            else
            {
                int Photolenth = 0;
                byte[] PhotoData = new byte[20480];
                string photoname = "";
                int j = 0;
                string[] Allphotoname = AllPhotoName.Split('\n');
                int len = Allphotoname[1].Length;
                char[] allphotoname = Allphotoname[1].ToCharArray();

                string finalPath = photoPath + "BAD" + "-From" + fromTime.Replace(":", ".") + "-To" + toTime.Replace(":", ".") + "\\";
                System.IO.Directory.CreateDirectory(finalPath);

                for (j = 0; j < len; j++)
                {
                    if (allphotoname[j].ToString() != "\t")
                    {
                        photoname += allphotoname[j].ToString();
                    }
                    else if (photoname != "")
                    {
                        photoname += ".jpg";
                        if (axCZKEM1.GetPhotoByName(GetMachineNumber(), photoname, out PhotoData[0], out Photolenth))
                        {
                            //convert byte to image and save
                            using (Stream file = File.Create(finalPath + photoname))
                            {
                                CopyStream(new MemoryStream(PhotoData), file);
                            }
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            ret = idwErrorCode;

                            if (idwErrorCode != 0)
                            {
                                //"*Get photo failed,ErrorCode: " + idwErrorCode.ToString());
                            }
                            else
                            {
                                //"No data from terminal returns!");
                            }
                            break;
                        }
                        photoname = "";
                    }
                }
                //"GetBadPhotoByTimePeriod succeed.");
                ret = 1;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }

        public int sta_ClearAllAttPhoto( int iFlag, string fromTime, string toTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int ret = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.ClearPhotoByTime(iMachineNumber, iFlag, fromTime, toTime))
            {
                //"Clear capture picture OK");
            }
            else
            {
                int errorcode = -1;
                axCZKEM1.GetLastError(ref errorcode);
                //"Clear capture picture Failed" + errorcode.ToString());
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);//enable the device

            return ret;
        }
        #endregion

        #region OPLOG
        public int sta_GetOplog( DataTable dt_Oplog)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;
            int iSuperLogCount = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            //if (axCZKEM1.ReadSuperLogData(GetMachineNumber()))
            if (axCZKEM1.ReadAllSLogData(GetMachineNumber()))
            {
                int idwTMachineNumber = 0;
                int iParams1 = 0;
                int iParams2 = 0;
                int idwManipulation = 0;
                int iParams3 = 0;

                int iParams4 = 0;
                int iYear = 0;
                int iMonth = 0;
                int iDay = 0;
                int iHour = 0;
                int iMin = 0;
                int iSencond = 0;
                int iAdmin = 0;

                string sUser = null;
                string sAdmin = null;
                string sTime = null;

                //while (axCZKEM1.SSR_GetSuperLogData(GetMachineNumber(), out idwTMachineNumber, out sAdmin, out sUser,
                //    out idwManipulation, out sTime, out iParams1, out iParams2, out iParams3))
                while (axCZKEM1.GetSuperLogData2(GetMachineNumber(), ref idwTMachineNumber, ref iAdmin, ref iParams4, ref iParams1, ref iParams2, ref idwManipulation, ref iParams3, ref iYear, ref iMonth, ref iDay, ref iHour, ref iMin, ref iSencond))
                {
                    iSuperLogCount++;
                    DataRow dr = dt_Oplog.NewRow();
                    dr["Count"] = iSuperLogCount;
                    dr["MachineNumber"] = GetMachineNumber();
                    dr["Admin"] = iAdmin;
                    //dr["UserPIN2"] = sUser;
                    dr["Operation"] = idwManipulation;
                    sTime = iYear + "-" + iMonth + "-" + iDay + " " + iHour + ":" + iMin + ":" + iSencond;
                    dr["DateTime"] = sTime;
                    dr["Param1"] = iParams1;
                    dr["Param2"] = iParams2;
                    dr["Param3"] = iParams3;
                    dr["Param4"] = iParams4;
                    dt_Oplog.Rows.Add(dr);
                }

                //"Down oplog success.");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                ret = idwErrorCode;

                if (idwErrorCode != 0)
                {
                    //"*Get OPLOG failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);

            return ret;
        }

        public int sta_ClearOplog()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearSLog(GetMachineNumber()))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All operation logs have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"ClearOplog failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }
        #endregion

        #region ClearData
        public int sta_ClearAdmin()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearAdministrators(GetMachineNumber()))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All administrator have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"*ClearAdmin failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }

        public int sta_ClearAllLogs()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearData(GetMachineNumber(), 1))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All AttLogs have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"*ClearAllLogs failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }

        public int sta_ClearAllFps()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearData(GetMachineNumber(), 2))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All fp templates have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"*ClearAllFps failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }

        public int sta_ClearAllUsers()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearData(GetMachineNumber(), 5))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All users have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"*ClearAllUsers failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }

        public int sta_ClearAllData()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }
            int ret = 0;

            axCZKEM1.EnableDevice(GetMachineNumber(), false);

            if (axCZKEM1.ClearKeeperData(GetMachineNumber()))
            {
                axCZKEM1.RefreshData(GetMachineNumber());//the data in the device should be refreshed
                //"All Data have been cleared from teiminal!");
                ret = 1;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode != 0)
                {
                    //"*ClearAllData failed,ErrorCode=" + idwErrorCode.ToString());
                }
                else
                {
                    //"No data from terminal returns!");
                }
                ret = idwErrorCode;
            }

            axCZKEM1.EnableDevice(GetMachineNumber(), true);
            return ret;
        }
        #endregion


        #endregion

        #region AccessMng

        #region TimeZone
        public int sta_GetTZInfo( string txtTZIndex, DateTime dtSUNs, DateTime dtMONs, DateTime dtTUEs, DateTime dtWENs, DateTime dtTHUs, DateTime dtFRIs, DateTime dtSATs, DateTime dtSUNe, DateTime dtMONe, DateTime dtTUEe, DateTime dtWENe, DateTime dtTHUe, DateTime dtFRIe, DateTime dtSATe)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtTZIndex.Trim() == "")
            {
                //"*Please input TimeZoneIndex first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iTimeZoneID = Convert.ToInt32(txtTZIndex.Trim());

            if (iTimeZoneID <= 0 || iTimeZoneID > 50)
            {
                //"*Timezone index error!");
                return -1022;
            }

            string sTimeZone = "";

            if (axCZKEM1.GetTZInfo(iMachineNumber, iTimeZoneID, ref sTimeZone))
            {
                string[] array = new string[sTimeZone.Length / 2];
                int i, j = 0;
                for (i = 0; (i + 2) <= sTimeZone.Length && sTimeZone.Length >= i;)
                {
                    array[j] = sTimeZone.Substring(i, 2);
                    j++;
                    i = i + 2;
                }

                dtSUNs =Convert.ToDateTime(array[0] + ":" + array[1]);
                dtSUNe = Convert.ToDateTime(array[2] + ":" + array[3]);

                dtMONs =Convert.ToDateTime(array[4] + ":" + array[5]);
                dtMONe =Convert.ToDateTime(array[6] + ":" + array[7]);

                dtTUEs =Convert.ToDateTime(array[8] + ":" + array[9]);
                dtTUEe =Convert.ToDateTime(array[10] + ":" + array[11]);

                dtWENs =Convert.ToDateTime(array[12] + ":" + array[13]);
                dtWENe =Convert.ToDateTime(array[14] + ":" + array[15]);

                dtTHUs =Convert.ToDateTime(array[16] + ":" + array[17]);
                dtTHUe =Convert.ToDateTime(array[18] + ":" + array[19]);

                dtFRIs =Convert.ToDateTime(array[20] + ":" + array[21]);
                dtFRIe =Convert.ToDateTime(array[22] + ":" + array[23]);

                dtSATs =Convert.ToDateTime(array[24] + ":" + array[25]);
                dtSATe =Convert.ToDateTime(array[26] + ":" + array[27]);

                //"Get TZ info successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode == -2 || idwErrorCode == -12008)
                {
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString() + ",Over TimeZoneIndex limits!");
                }
                else
                {
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                }
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_SetTZInfo( string txtTZIndex, DateTime dtSUNs, DateTime dtMONs, DateTime dtTUEs, DateTime dtWENs, DateTime dtTHUs, DateTime dtFRIs, DateTime dtSATs, DateTime dtSUNe, DateTime dtMONe, DateTime dtTUEe, DateTime dtWENe, DateTime dtTHUe, DateTime dtFRIe, DateTime dtSATe)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtTZIndex.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            if (dtSUNs.ToString().Trim() == "" || dtMONs.ToString().Trim() == "" || dtTUEs.ToString().Trim() == "" || dtWENs.ToString().Trim() == "" || dtTHUs.ToString().Trim() == "" || dtFRIs.ToString().Trim() == "" || dtSATs.ToString().Trim() == "" || dtSUNe.ToString().Trim() == "" || dtMONe.ToString().Trim() == "" || dtTUEe.ToString().Trim() == "" || dtWENe.ToString().Trim() == "" || dtTHUe.ToString().Trim() == "" || dtFRIe.ToString().Trim() == "" || dtSATe.ToString().Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iTimeZoneID = Convert.ToInt32(txtTZIndex.Trim());

            if (iTimeZoneID <= 0 || iTimeZoneID > 50)
            {
                //"*Timezone index error!");
                return -1022;
            }

            int idwErrorCode = 0;

            DateTime dSUNs = dtSUNs;
            DateTime dMONs = dtMONs;
            DateTime dTUEs = dtTUEs;
            DateTime dWENs = dtWENs;
            DateTime dTHUs = dtTHUs;
            DateTime dFRIs = dtFRIs;
            DateTime dSATs = dtSATs;

            DateTime dSUNe = dtSUNe;
            DateTime dMONe = dtMONe;
            DateTime dTUEe = dtTUEe;
            DateTime dWENe = dtWENe;
            DateTime dTHUe = dtTHUe;
            DateTime dFRIe = dtFRIe;
            DateTime dSATe = dtSATe;

            string sSunTime = dSUNs.Hour.ToString("00") + dSUNs.Minute.ToString("00") + dSUNe.Hour.ToString("00") + dSUNe.Minute.ToString("00");
            string sMonTime = dMONs.Hour.ToString("00") + dMONs.Minute.ToString("00") + dMONe.Hour.ToString("00") + dMONe.Minute.ToString("00");
            string sTueTime = dTUEs.Hour.ToString("00") + dTUEs.Minute.ToString("00") + dTUEe.Hour.ToString("00") + dTUEe.Minute.ToString("00");
            string sWenTime = dWENs.Hour.ToString("00") + dWENs.Minute.ToString("00") + dWENe.Hour.ToString("00") + dWENe.Minute.ToString("00");
            string sThuTime = dTHUs.Hour.ToString("00") + dTHUs.Minute.ToString("00") + dTHUe.Hour.ToString("00") + dTHUe.Minute.ToString("00");
            string sFriTime = dFRIs.Hour.ToString("00") + dFRIs.Minute.ToString("00") + dFRIe.Hour.ToString("00") + dFRIe.Minute.ToString("00");
            string sSatTime = dSATs.Hour.ToString("00") + dSATs.Minute.ToString("00") + dSATe.Hour.ToString("00") + dSATe.Minute.ToString("00");

            string sTimeZone = sSunTime + sMonTime + sTueTime + sWenTime + sThuTime + sFriTime + sSatTime;

            if (axCZKEM1.SetTZInfo(iMachineNumber, iTimeZoneID, sTimeZone))
            {
                //the data in the device should be refreshed
                axCZKEM1.RefreshData(iMachineNumber);
                //"Successfully set the TimeZone" + iTimeZoneID + "！");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode == -12008)
                {
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString() + ",Over TimeZoneIndex limits!");
                }
                else if (idwErrorCode == 4)
                {
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString() + ",TimeZone Format Error!");
                }
                else
                {
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                }
            }
            return 1;
        }
        #endregion

        #region GroupTimeZone
        //Get the time zones used by specified group and other interrelated information.
        public int sta_GetGroupTZ(string txtACGroupNo, string txtTZIndex1, string txtTZIndex2, string txtTZIndex3, string cboACValidHoliday, List<string> cbVerifyStyle)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtACGroupNo.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iGroupNo = Convert.ToInt32(txtACGroupNo.Trim());

            if (iGroupNo < 1 || iGroupNo > 99)
            {
                //"*Group ID error!");
                return -1022;
            }

            int idwErrorCode = 0;
            int iValidHoliday = 0;
            int iVerifyStyle = 0;
            int iTZ1 = 0;
            int iTZ2 = 0;
            int iTZ3 = 0;

            if (axCZKEM1.SSR_GetGroupTZ(iMachineNumber, iGroupNo, ref iTZ1, ref iTZ2, ref iTZ3, ref iValidHoliday, ref iVerifyStyle))
            {
                txtACGroupNo = iGroupNo.ToString();
                txtTZIndex1 = iTZ1.ToString();
                txtTZIndex2 = iTZ2.ToString();
                txtTZIndex3 = iTZ3.ToString();
                cboACValidHoliday = iValidHoliday.ToString();
                //cbVerifyStyle.SelectedIndex = iVerifyStyle;
                //"Get group TZ successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }

            return 1;
        }

        //Set the time zones used by specified group and other interrelated information.
        public int sta_SetGroupTZ(string txtACGroupNo, string txtTZIndex1, string txtTZIndex2, string txtTZIndex3, List<string> cboACValidHoliday, List<string> cbVerifyStyle)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtACGroupNo.Trim() == "" || txtTZIndex1.Trim() == "" || txtTZIndex2.Trim() == "" || txtTZIndex3.Trim() == "" || cboACValidHoliday.Count ==0 || cbVerifyStyle.Count == 0)
            {
                //"*Please input data first!");
                return -1023;
            }

            int iGroupNo = Convert.ToInt32(txtACGroupNo.Trim());

            if (iGroupNo < 1 || iGroupNo > 99)
            {
                //"*Group ID error!");
                return -1022;
            }

            int iTZ1 = Convert.ToInt32(txtTZIndex1.Trim());
            int iTZ2 = Convert.ToInt32(txtTZIndex2.Trim());
            int iTZ3 = Convert.ToInt32(txtTZIndex3.Trim());

            if (iTZ1 < 0 || iTZ1 > 50 || iTZ2 < 0 || iTZ2 > 50 || iTZ3 < 0 || iTZ3 > 50)
            {
                //"*Timezone index error!");
                return -1022;
            }

            int idwErrorCode = 0;


            int iValidHoliday = Convert.ToInt32(cboACValidHoliday.Count);
            int iVerifyStyle = cbVerifyStyle.Count;

            if (axCZKEM1.SSR_SetGroupTZ(iMachineNumber, iGroupNo, iTZ1, iTZ2, iTZ3, iValidHoliday, iVerifyStyle))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Set GroupTZ, GroupNo:" + iGroupNo.ToString() + " VerifyStyle:" + iVerifyStyle.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }
        #endregion

        #region UnlockGroup
        public int sta_GetUnLockGroup( string cboACComNo, string cboGroup1, string cboGroup2, string cboGroup3, string cboGroup4, string cboGroup5)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboACComNo.Trim() == "")
            {
                //"*Please input params first!");
                return -1023;
            }
            int idwErrorCode = 0;

            int iComNo = Convert.ToInt32(cboACComNo.Trim());
            int iGroup1 = 0;
            int iGroup2 = 0;
            int iGroup3 = 0;
            int iGroup4 = 0;
            int iGroup5 = 0;

            if (axCZKEM1.SSR_GetUnLockGroup(iMachineNumber, iComNo, ref iGroup1, ref iGroup2, ref iGroup3, ref iGroup4, ref iGroup5))
            {
                cboGroup1=(iGroup1.ToString());
                cboGroup2=(iGroup2.ToString());
                cboGroup3=(iGroup3.ToString());
                cboGroup4=(iGroup4.ToString());
                cboGroup5=(iGroup5.ToString());
                //"Get unlock group successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_SetUnLockGroup(string cboACComNo, string cboGroup1,string cboGroup2,string cboGroup3, string cboGroup4, string cboGroup5)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboACComNo.Trim() == "" || cboGroup1.Trim() == "" || cboGroup2.Trim() == "" || cboGroup3.Trim() == "" || cboGroup4.Trim() == "" || cboGroup5.Trim() == "")
            {
                //"*Please input the five groups first!");
                return -1023;
            }
            int idwErrorCode = 0;

            int iComNo = Convert.ToInt32(cboACComNo.Trim());
            int iGroup1 = Convert.ToInt32(cboGroup1.Trim());
            int iGroup2 = Convert.ToInt32(cboGroup2.Trim());
            int iGroup3 = Convert.ToInt32(cboGroup3.Trim());
            int iGroup4 = Convert.ToInt32(cboGroup4.Trim());
            int iGroup5 = Convert.ToInt32(cboGroup5.Trim());

            if (axCZKEM1.SSR_SetUnLockGroup(iMachineNumber, iComNo, iGroup1, iGroup2, iGroup3, iGroup4, iGroup5))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"SetUnlockGroups, Groups:" + iGroup1.ToString() + ":" + iGroup2.ToString() + ":" + iGroup3.ToString() + ":" + iGroup4.ToString() + ":" + iGroup5.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //if (idwErrorCode == -2001)
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString() + ". Group Not Exist");
                //else
                    //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        #endregion

        #region UserGroup
        public int sta_GetUserGroup(string cboUAUserIDGroup, string txtGroupNo1)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboUAUserIDGroup == "")
            {
                //"*Please input data first!");
                return -1023;
            }
            int idwErrorCode = 0;

            int iUserID = Convert.ToInt32(cboUAUserIDGroup.Trim());
            int iUserGrp = 0;

            if (axCZKEM1.GetUserGroup(iMachineNumber, iUserID, ref iUserGrp))
            {
                txtGroupNo1 = iUserGrp.ToString();
                //"Get user group successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_SetUserGroup(string cboUAUserIDGroup, string txtGroupNo1)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboUAUserIDGroup == "" || txtGroupNo1.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }
            int idwErrorCode = 0;

            int iUserID = Convert.ToInt32(cboUAUserIDGroup.Trim());
            int iUserGrp = Convert.ToInt32(txtGroupNo1.Trim());

            if (axCZKEM1.SetUserGroup(iMachineNumber, iUserID, iUserGrp))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Set User Group, UserID:" + iUserID.ToString() + ", Group No:" + iUserGrp.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        #endregion

        #region UserTimeZone
        public int sta_GetUserTZStr(string cboUAUserIDTZ,int cbUserTZtype, string txtUTZIndex1, string txtUTZIndex2, string txtUTZIndex3)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboUAUserIDTZ.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int idwErrorCode = 0;
            int iUserID = Convert.ToInt32(cboUAUserIDTZ.Trim());
            string sTZs = null;

            /*
            int [] iTZs = new int[4];
            axCZKEM1.GetUserTZs(iMachineNumber, iUserID, ref iTZs[0]);
            //iTZs[0].ToString() + iTZs[1].ToString() + iTZs[2].ToString() + iTZs[3].ToString());
            */

            if (axCZKEM1.GetUserTZStr(iMachineNumber, iUserID, ref sTZs))//TZs is in the form of string.
            {
                string[] s = sTZs.Split(new char[] { ':' });
                //sTZs);
                txtUTZIndex1 = s[0];
                txtUTZIndex2 = s[1];
                txtUTZIndex3 = s[2];
                if (s[3] == "")
                {
                    cbUserTZtype = 0;
                }
                else
                {
                    cbUserTZtype = Convert.ToInt32(s[3]);
                }

                if (cbUserTZtype == 0)
                {
                    txtUTZIndex1 = "";
                    txtUTZIndex2 = "";
                    txtUTZIndex3 = "";
                }
                else
                {
                    if (s[0] == "")
                    {
                        txtUTZIndex1 = "0";
                    }
                    if (s[1] == "")
                    {
                        txtUTZIndex2 = "0";
                    }
                    if (s[2] == "")
                    {
                        txtUTZIndex3 = "0";
                    }
                }

                //"Get user TZ successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_SetUserTZStr(string cboUAUserIDTZ,string cbUserTZtype,string txtUTZIndex1,string txtUTZIndex2,string txtUTZIndex3)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboUAUserIDTZ.Trim() == "" || cbUserTZtype.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iTZ1 = 0;
            int iTZ2 = 0;
            int iTZ3 = 0;

            if (cbUserTZtype == "")
            {
                txtUTZIndex1 = "";
                txtUTZIndex2 = "";
                txtUTZIndex3 = "";
            }
            else
            {
                if (txtUTZIndex1.Trim() == "" || txtUTZIndex2.Trim() == "" || txtUTZIndex3.Trim() == "")
                {
                    //"*Please input TZ first!");
                    return -1023;
                }

                iTZ1 = Convert.ToInt32(txtUTZIndex1.Trim());
                iTZ2 = Convert.ToInt32(txtUTZIndex2.Trim());
                iTZ3 = Convert.ToInt32(txtUTZIndex3.Trim());

                if (iTZ1 < 0 || iTZ1 > 50 || iTZ2 < 0 || iTZ2 > 50 || iTZ3 < 0 || iTZ3 > 50)
                {
                    //"*Timezone index error!");
                    return -1022;
                }
            }

            int idwErrorCode = 0;

            int iUserID = Convert.ToInt32(cboUAUserIDTZ.Trim());
            string sTZs = iTZ1.ToString() + ":" + iTZ2.ToString() + ":" + iTZ3.ToString() + ":" + cbUserTZtype.ToString();

            if (axCZKEM1.SetUserTZStr(iMachineNumber, iUserID, sTZs))//TZs is in strings.
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Set user TZ successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }
        #endregion

        #region Check Use GroupTimeZone or UserTimeZone
        public int sta_UseGroupTimeZone(string cboUAUserIDTZ, string lbUserTimezoneType)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (cboUAUserIDTZ.Trim() == "")
            {
                //"*Please input the UserID first!");
                return -1023;
            }
            int idwErrorCode = 0;

            int iUserID = Convert.ToInt32(cboUAUserIDTZ.Trim());
            string sTZs = "";

            if (axCZKEM1.GetUserTZStr(iMachineNumber, iUserID, ref sTZs))
            {
                if (axCZKEM1.UseGroupTimeZone())
                {
                    lbUserTimezoneType = "Using Group TimeZone";
                    //"Using Group TimeZone");
                }
                else
                {
                    lbUserTimezoneType = "Not Using Group TimeZone";
                    //"Not Using Group TimeZone");
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }
        #endregion

        #region UserIDTimer not the stander interface on SDK.
        //Add the esxited userid to DropDownLists.
        bool bIDTimerAddControl = true;
        public int sta_UserIDTimer(bool bEnable, List<string> cboUAUserIDGroup, List<string> cboUAUserIDTZ)
        {
            if (GetConnectState() == false)
            {
                return -1024;
            }

            if (bIDTimerAddControl == true || bEnable == true)
            {
                string sEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                cboUAUserIDGroup.Clear();
                cboUAUserIDTZ.Clear();

                axCZKEM1.EnableDevice(iMachineNumber, false);
                axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                {
                    cboUAUserIDGroup.Add(sEnrollNumber);
                    cboUAUserIDTZ.Add(sEnrollNumber);
                }

                axCZKEM1.EnableDevice(iMachineNumber, true);
            }

            bIDTimerAddControl = false;
            bEnable = false;

            return 1;

        }
        #endregion

        #region controldevice
        public int sta_ACUnlock(string txtDelay)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtDelay.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            if (Convert.ToInt32(txtDelay.Trim()) < 0 || Convert.ToInt32(txtDelay.Trim()) > 10)
            {
                //"*Delay error!");
                return -1022;
            }

            int idwErrorCode = 0;

            int iDelay = Convert.ToInt32(txtDelay.Trim());//time to delay

            if (axCZKEM1.ACUnlock(iMachineNumber, iDelay))
            {

                //"ACUnlock, Dalay Seconds:" + iDelay.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_CloseAlarm()
        {
            /*
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }
            int idwErrorCode = 0;

            if(axCZKEM1.CloseAlarm(iMachineNumber))
            {
                //"Close alarm successful");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
             * */
            //"[func CloseAlarm]Temporarily unsupported");
            return 1;
        }
        #endregion

        #region get and set wiegandfmt
        public int sta_GetWiegandFmt( string txtShowWiegandFmt)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            string sWiegandFmt = "";

            int idwErrorCode = 0;

            if (axCZKEM1.GetWiegandFmt(iMachineNumber, ref sWiegandFmt))
            {
                txtShowWiegandFmt = sWiegandFmt;
                //"Operation Successed！");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        public int sta_SetWiegandFmt( string txtSetWiegandFmt)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            int idwErrorCode = 0;

            string sWiegandFmt = txtSetWiegandFmt.Trim();

            if (axCZKEM1.SetWiegandFmt(iMachineNumber, sWiegandFmt))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Operation Successed！");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }
        #endregion

        #region Holiday
        public int sta_SetHoliday( string txtHolidayId, DateTime dtStartDate, DateTime dtEndDate, string txtTimeZoneId)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtHolidayId.Trim() == "" || txtTimeZoneId.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iHolidayId = Convert.ToInt32(txtHolidayId.Trim());

            if (iHolidayId < 1 || iHolidayId > 99)
            {
                //"*Holiday ID error");
                return -1022;
            }

            int iTimezomeId = Convert.ToInt32(txtTimeZoneId.Trim());
            if (iTimezomeId < 1 || iTimezomeId > 50)
            {
                //"*Timezone index error!");
                return -1023;
            }

            int idwErrorCode = 0;

            DateTime dStartDate = dtStartDate;
            DateTime dEndDate = dtEndDate;
            int iSMonth = dStartDate.Month;
            int iSDay = dStartDate.Day;
            int iEMonth = dEndDate.Month;
            int iEDay = dEndDate.Day;

            if (axCZKEM1.SSR_SetHoliday(iMachineNumber, iHolidayId, iSMonth, iSDay, iEMonth, iEDay, iTimezomeId))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Operation Successed！");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;

        }

        public int sta_GetHoliday( string txtHolidayId, DateTime dtStartDate, DateTime dtEndDate, string txtTimeZoneId)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (txtHolidayId.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }

            int iHolidayId = Convert.ToInt32(txtHolidayId.Trim());

            if (iHolidayId < 1 || iHolidayId > 99)
            {
                //"*Holiday ID error");
                return -1022;
            }

            int idwErrorCode = 0;

            int iSMonth = 0;
            int iSDay = 0;
            int iEMonth = 0;
            int iEDay = 0;
            int iTimeZoneId = 0;

            if (axCZKEM1.SSR_GetHoliday(iMachineNumber, iHolidayId, ref iSMonth, ref iSDay, ref iEMonth, ref iEDay, ref iTimeZoneId))
            {
                dtStartDate = Convert.ToDateTime(iSMonth.ToString() + " " + iSDay.ToString());
                dtEndDate =Convert.ToDateTime(iEMonth.ToString() + " " + iEDay.ToString());

                txtTimeZoneId = iTimeZoneId.ToString();

                //"Get holiday successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }
        #endregion

        #region Set&Get SystemOption
        public int sta_SetNONCTimeZone( int parName, string parm)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            if (parm.Trim() == "")
            {
                //"*Please input data first!");
                return -1023;
            }
            int idwErrorCode = 0;

            string par = parm.Trim();
            //int tmpPar = 0;
            string strTmpPar = "";
            string strParName = "";
            if (Convert.ToInt32(par) < 0 || Convert.ToInt32(par) > 50)
            {
                //"*The timezone index error!");
                return -1022;
            }

            if (Convert.ToInt32(par) != 0)
            {
                if (parName == 81)
                {
                    strParName = "~DCTZ";

                }
                else if (parName == 80)
                {
                    strParName = "~DOTZ";
                }
                else
                {
                    return -1020;
                }

                axCZKEM1.GetSysOption(iMachineNumber, strParName, out strTmpPar);
                if (strTmpPar.Equals(par))
                {
                    //"*The NO and NC can not be same!");
                    return -1021;
                }
            }
            if (parName == 81)
            {
                strParName = "~DOTZ";

            }
            else if (parName == 80)
            {
                strParName = "~DCTZ";
            }

            if (axCZKEM1.SetSysOption(iMachineNumber, strParName, par))
            {
                //"Operation Successed!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }

            return 1;
        }

        public int sta_GetNONCTimeZone( int parName, string parm)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect the device first!");
                return -1024;
            }

            int idwErrorCode = 0;

            //int par = 0;
            /*
            if (axCZKEM1.GetDeviceInfo(iMachineNumber, parName, ref par))
            {
                parm = par.ToString();
                //"Get NO/NC TZ successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
             */
            string strParName = "";
            string par = "";

            if (parName == 81)
            {
                strParName = "~DOTZ";
            }
            else if (parName == 80)
            {
                strParName = "~DCTZ";
            }

            if (axCZKEM1.GetSysOption(iMachineNumber, strParName, out par))
            {
                parm = par.ToString();
                //"Get NO/NC TZ successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
                return idwErrorCode;
            }
            return 1;
        }

        #endregion

        #endregion
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
        #region OtherMng

        #region sync time
        //Synchronize the device time as the computer's.
        public int sta_SYNCTime(string lbDeviceTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (axCZKEM1.SetDeviceTime(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Successfully SYNC the PC's time to device!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_GetDeviceTime(string lbDeviceTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;

            if (axCZKEM1.GetDeviceTime(iMachineNumber, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond))//show the time
            {
                lbDeviceTime = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                //"Get devie time successfully");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_SetDeviceTime(DateTime dtDeviceTime)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            DateTime date = dtDeviceTime;
            int idwYear = Convert.ToInt32(date.Year.ToString());
            int idwMonth = Convert.ToInt32(date.Month.ToString());
            int idwDay = Convert.ToInt32(date.Day.ToString());
            int idwHour = Convert.ToInt32(date.Hour.ToString());
            int idwMinute = Convert.ToInt32(date.Minute.ToString());
            int idwSecond = Convert.ToInt32(date.Second.ToString());

            if (axCZKEM1.SetDeviceTime2(iMachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"Successfully set the time");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }
        #endregion

        #region wav

        public int sta_btnPlayWavByIndex(string cbWavIndex)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (cbWavIndex.Trim() == "")
            {
                //"Position(Wav Index) cannot be null!");
                return -1023;
            }

            int iIndex = Convert.ToInt32(cbWavIndex.Trim());
            if (axCZKEM1.PlayVoiceByIndex(iIndex))
            {
                //"PlayWavByIndex " + iIndex.ToString());
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        #endregion

        #region control

        public int sta_btnRestartDevice()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }



            if (axCZKEM1.RestartDevice(iMachineNumber))
            {
                sta_DisConnect();
                //"The device will restart");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_btnPowerOffDevice()
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (axCZKEM1.PowerOffDevice(iMachineNumber))
            {
                sta_DisConnect();
                //"Power off device");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        #endregion

        #region update
        public int sta_btnUpdateFirmware( string txtFirmwareFile)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            int idwErrorCode = 0;
            string sFirmwareFile = txtFirmwareFile.Trim();
            if (axCZKEM1.UpdateFirmware(sFirmwareFile))
            {
                //"UpdateFirmware,Name=" + sFirmwareFile);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }
        #endregion

        #region R/W file

        public int sta_btnSendFile(string txtSendFileName)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtSendFileName.Trim() == "")
            {
                //"*Please input the FileName  first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string sFileName = txtSendFileName.Trim();

            if (axCZKEM1.SendFile(iMachineNumber, sFileName))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                //"SendFile " + sFileName + " To the Device! ");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        public int sta_btnReadFile(string txtReadFileName, string txtFilePath)
        {
            if (GetConnectState() == false)
            {
                //"*Please connect first!");
                return -1024;
            }

            if (txtFilePath.Trim() == "" || txtReadFileName.Trim() == "")
            {
                //"*Please input the FileName and FilePath first!");
                return -1023;
            }

            int idwErrorCode = 0;
            string sFileName = txtReadFileName.Trim();
            string sFilePath = txtFilePath.Trim();

            if (axCZKEM1.ReadFile(iMachineNumber, sFileName, sFilePath))
            {
                //"ReadFile " + sFileName + " To " + sFilePath);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                //"*Operation failed,ErrorCode=" + idwErrorCode.ToString());
            }

            return 1;
        }

        #endregion

        #endregion
    }
}
