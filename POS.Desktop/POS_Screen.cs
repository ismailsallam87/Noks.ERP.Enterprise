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
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace POS.Desktop
{
    public partial class POS_Screen_frm : Form
    {
        Invoice current_invoice = new Invoice();
        private POS.DAL.Noks_POSEntities db = new Noks_POSEntities();
        public POS_Screen_frm()
        {
            InitializeComponent();
        }

        private void POS_Screen_frm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        #region data_methods
        private void LoadCategories()
        {

        }
        private void load_Items(int Category)
        {

        }
        #endregion

        private void category_Click(object sender, EventArgs e)
        {
            PictureBox category = (PictureBox)sender;
            list_item.Controls.Clear();

            if (btn_deserts.Name != category.Name)
            {
                btn_deserts.BackColor = Color.White;
                btn_deserts.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_deserts.BackColor = Color.Tomato;
                btn_deserts.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Deserts | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(6).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }

            if (btn_shisha.Name != category.Name)
            {
                btn_shisha.BackColor = Color.White;
                btn_shisha.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_deserts.BackColor = Color.Tomato;
                btn_shisha.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Shisha | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(7).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }
            if (btn_fresh.Name != category.Name)
            {
                btn_fresh.BackColor = Color.White;
                btn_fresh.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_fresh.BackColor = Color.Tomato;
                btn_fresh.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Fresh Juices | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(3).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }

            if (btn_hotdrinks.Name != category.Name)
            {
                btn_hotdrinks.BackColor = Color.White;
                btn_hotdrinks.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_hotdrinks.BackColor = Color.Tomato;
                btn_hotdrinks.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Hot Drinks | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(1).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }

            if (btn_softdrinks.Name != category.Name)
            {
                btn_softdrinks.BackColor = Color.White;
                btn_softdrinks.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_softdrinks.BackColor = Color.Tomato;
                btn_softdrinks.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Soft Drinks | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(2).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }


            if (btn_smoothies.Name != category.Name)
            {
                btn_smoothies.BackColor = Color.White;
                btn_smoothies.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_smoothies.BackColor = Color.Tomato;
                btn_smoothies.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Smoothies | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(5).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }


            if (btn_cocktails.Name != category.Name)
            {
                btn_cocktails.BackColor = Color.White;
                btn_cocktails.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_cocktails.BackColor = Color.Tomato;
                btn_cocktails.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Cocktails | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(4).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }
            if (btn_extra.Name != category.Name)
            {
                btn_extra.BackColor = Color.White;
                btn_extra.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                //btn_deserts.BackColor = Color.Tomato;
                btn_extra.BorderStyle = BorderStyle.FixedSingle;
                gbox_items.Text = "Extras | Items";
                List<Items_by_Category_Result> items = db.Items_by_Category(8).ToList();
                foreach (Items_by_Category_Result item in items)
                {
                    Button new_item = new Button();
                    new_item.Tag = item.ID;
                    new_item.Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
                    new_item.Width = 160;
                    new_item.Height = 75;
                    new_item.Click += item_click;
                    list_item.Controls.Add(new_item);
                }
            }

        }
        private void item_click(object sender, EventArgs e)
        {
            Button item_btn = (Button)sender;
            Items_by_ID_Result item = db.Items_by_ID(Convert.ToInt32(item_btn.Tag)).FirstOrDefault();
            POS_Item_Add new_item_frm = new POS.Desktop.POS_Item_Add();
            new_item_frm.Controls[3].Text = item.Code + "|" + item.Name + "\r\n" + item.Selling_Price.ToString("N2") + " EGP";
            new_item_frm.Controls[2].Text = "1";
            new_item_frm.Controls[1].Click += add_item;
            new_item_frm.Controls[1].Tag = item.ID;
            new_item_frm.ShowDialog();

        }

        private void add_item(object sender, EventArgs e)
        {
            receipt_item_list.Items.Clear();
            Button adding_btn_item = (Button)sender;
            int Item_Id = Convert.ToInt32(adding_btn_item.Tag);
            Items_by_ID_Result selected_item = db.Items_by_ID(Item_Id).FirstOrDefault();
            current_invoice.Invoice_Items.Add(new Invoice_Items { Amount = 1, Discount = 0, Total_Selling_Price = selected_item.Selling_Price, Unit_Selling_Price = selected_item.Selling_Price, Unit_Cost = selected_item.Cost, Item_PriceList_ID = selected_item.price_list_id });
            re_calculate_invoice();
        }

        private void re_calculate_invoice()
        {
            receipt_item_list.Items.Clear();
            double amount = 0;
            double total_services = 0;
            double total_taxes = 0;
            double total_amount = 0;
            for (int i = 0; i < current_invoice.Invoice_Items.Count; i++)
            {
                long Item_PriceList_ID = current_invoice.Invoice_Items.ToList()[i].Item_PriceList_ID;
                Item_PriceList rec_item = db.Item_PriceList.Where(m => m.ID == Item_PriceList_ID).FirstOrDefault();
                string receipt_item = (i + 1).ToString() + "- " + current_invoice.Invoice_Items.ToList()[i].Amount + "    " + rec_item.Item_Measures.Item.Name + "          " + current_invoice.Invoice_Items.ToList()[i].Unit_Selling_Price.ToString("N2") + " EGP";
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

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            current_invoice = new Invoice();
            re_calculate_invoice();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                current_invoice.Created_At = DateTime.Now;
                db.Invoices.Add(current_invoice);
                db.SaveChanges();
                Receipt_Frm new_rec = new Receipt_Frm(current_invoice.ID);
                new_rec.ShowDialog();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show("An error occurred while trying to save the invoice, please try again or contact your administrator...!");
            }
        }
    }
}
