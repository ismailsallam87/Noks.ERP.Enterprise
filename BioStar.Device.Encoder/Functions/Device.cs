using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;

namespace Device.Encoder.Functions
{
    public class Device
    {
        public Device(int _gMachineNumber, string _lpszIPAddress, string _lpszDevId, int _PortNo, int _devPassword)
        {
            
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
        private bool Open()
        {
            return true;
        }
        private bool Close()
        {
            return true;
        }
    }
}
