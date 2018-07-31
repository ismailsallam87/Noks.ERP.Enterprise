using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Desktop
{
    public partial class Printer_Frm : Form
    {
        public Printer_Frm()
        {
            InitializeComponent();
        }

        private void Printer_Frm_Load(object sender, EventArgs e)
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                listBox1.Items.Add(printer);
            }
        }
    }
}
