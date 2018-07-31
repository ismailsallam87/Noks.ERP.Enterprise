using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace Noks_Meetingroom_Addons_2013_2016
{
    public partial class Ribbon_Icon
    {
        private void Ribbon_Icon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        
        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            Booking_frm frm = new Noks_Meetingroom_Addons_2013_2016.Booking_frm();
            frm.ShowDialog();
        }
    }
}
