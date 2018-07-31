using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.DAL;
using System.Drawing.Printing;

namespace POS.Desktop
{
    public partial class Receipt_Frm : Form
    {
        private POS.DAL.Noks_POSEntities db = new Noks_POSEntities();
        Invoice current_invoice;
        public Receipt_Frm(long rec_no)
        {
            InitializeComponent();

            current_invoice = db.Invoices.Where(m => m.ID == rec_no).FirstOrDefault();
            re_calculate_invoice();
            lbl_cashier.Text = "Cashier:" + Helpers.Session.Cashier;
            lbl_rec_no.Text = "Rec#:" + rec_no.ToString();
            lbl_date.Text = DateTime.Now.ToString();
            
        }
        private void re_calculate_invoice()
        {
            double amount = 0;
            double total_services = 0;
            double total_taxes = 0;
            double total_amount = 0;
            for (int i = 0; i < current_invoice.Invoice_Items.Count; i++)
            {
                long Item_PriceList_ID = current_invoice.Invoice_Items.ToList()[i].Item_PriceList_ID;
                Item_PriceList rec_item = db.Item_PriceList.Where(m => m.ID == Item_PriceList_ID).FirstOrDefault();
                string receipt_item = (i + 1).ToString() + "- " + current_invoice.Invoice_Items.ToList()[i].Amount + "    " + rec_item.Item_Measures.Item.Name + "          ";
                list_price.Items.Add(current_invoice.Invoice_Items.ToList()[i].Unit_Selling_Price.ToString("N2") + " EGP");
                receipt_item_list.Items.Add(receipt_item);
                amount += (double)current_invoice.Invoice_Items.ToList()[i].Total_Selling_Price;
            }
            total_services += amount * .12;
            total_taxes += (total_services + amount) * .14;
            total_amount = amount + total_services + total_taxes;
            txt_amount.Text = amount.ToString("N2") + " EGP";
            txt_services.Text = total_services.ToString("N2") + " EGP";
            txt_taxes.Text = total_taxes.ToString("N2") + " EGP";
            total_txt.Text = total_amount.ToString("N2") + " EGP";

            current_invoice.Total_Amount = Convert.ToDecimal(amount + total_services + total_taxes);
            current_invoice.Net_Amount = Convert.ToDecimal(amount);
            current_invoice.POS_Session_ID = Convert.ToInt64(Helpers.Session.Session_ID);
        }
        private void Receipt_Frm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintScreen();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("210 x 297 mm", 315, 600);
            printPreviewDialog1.ShowDialog();
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void PrintScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            //13369376
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13360000);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
