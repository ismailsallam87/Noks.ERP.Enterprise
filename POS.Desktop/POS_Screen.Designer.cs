namespace POS.Desktop
{
    partial class POS_Screen_frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_extra = new System.Windows.Forms.PictureBox();
            this.btn_shisha = new System.Windows.Forms.PictureBox();
            this.btn_deserts = new System.Windows.Forms.PictureBox();
            this.btn_cocktails = new System.Windows.Forms.PictureBox();
            this.btn_smoothies = new System.Windows.Forms.PictureBox();
            this.btn_fresh = new System.Windows.Forms.PictureBox();
            this.btn_softdrinks = new System.Windows.Forms.PictureBox();
            this.btn_hotdrinks = new System.Windows.Forms.PictureBox();
            this.gbox_items = new System.Windows.Forms.GroupBox();
            this.list_item = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.total_txt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.receipt_item_list = new System.Windows.Forms.ListBox();
            this.txt_taxes = new System.Windows.Forms.Label();
            this.txt_services = new System.Windows.Forms.Label();
            this.txt_amount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_netamount = new System.Windows.Forms.Label();
            this.cancel_btn = new System.Windows.Forms.PictureBox();
            this.btn_print = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_extra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_shisha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_deserts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cocktails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_smoothies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_fresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_softdrinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_hotdrinks)).BeginInit();
            this.gbox_items.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cancel_btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_print)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_extra);
            this.groupBox1.Controls.Add(this.btn_shisha);
            this.groupBox1.Controls.Add(this.btn_deserts);
            this.groupBox1.Controls.Add(this.btn_cocktails);
            this.groupBox1.Controls.Add(this.btn_smoothies);
            this.groupBox1.Controls.Add(this.btn_fresh);
            this.groupBox1.Controls.Add(this.btn_softdrinks);
            this.groupBox1.Controls.Add(this.btn_hotdrinks);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 703);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Categories";
            // 
            // btn_extra
            // 
            this.btn_extra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_extra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_extra.Image = global::POS.Desktop.Properties.Resources.extras_icon;
            this.btn_extra.Location = new System.Drawing.Point(6, 625);
            this.btn_extra.Name = "btn_extra";
            this.btn_extra.Size = new System.Drawing.Size(102, 66);
            this.btn_extra.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_extra.TabIndex = 7;
            this.btn_extra.TabStop = false;
            this.btn_extra.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_shisha
            // 
            this.btn_shisha.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_shisha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_shisha.Image = global::POS.Desktop.Properties.Resources.shisha_icon;
            this.btn_shisha.Location = new System.Drawing.Point(6, 539);
            this.btn_shisha.Name = "btn_shisha";
            this.btn_shisha.Size = new System.Drawing.Size(102, 66);
            this.btn_shisha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_shisha.TabIndex = 6;
            this.btn_shisha.TabStop = false;
            this.btn_shisha.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_deserts
            // 
            this.btn_deserts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_deserts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_deserts.Image = global::POS.Desktop.Properties.Resources.desserts_icon;
            this.btn_deserts.Location = new System.Drawing.Point(6, 452);
            this.btn_deserts.Name = "btn_deserts";
            this.btn_deserts.Size = new System.Drawing.Size(102, 66);
            this.btn_deserts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_deserts.TabIndex = 5;
            this.btn_deserts.TabStop = false;
            this.btn_deserts.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_cocktails
            // 
            this.btn_cocktails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_cocktails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cocktails.Image = global::POS.Desktop.Properties.Resources.smoothies_icon;
            this.btn_cocktails.Location = new System.Drawing.Point(6, 366);
            this.btn_cocktails.Name = "btn_cocktails";
            this.btn_cocktails.Size = new System.Drawing.Size(102, 66);
            this.btn_cocktails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cocktails.TabIndex = 4;
            this.btn_cocktails.TabStop = false;
            this.btn_cocktails.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_smoothies
            // 
            this.btn_smoothies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_smoothies.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_smoothies.Image = global::POS.Desktop.Properties.Resources.cocktails_icon;
            this.btn_smoothies.Location = new System.Drawing.Point(6, 279);
            this.btn_smoothies.Name = "btn_smoothies";
            this.btn_smoothies.Size = new System.Drawing.Size(102, 66);
            this.btn_smoothies.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_smoothies.TabIndex = 3;
            this.btn_smoothies.TabStop = false;
            this.btn_smoothies.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_fresh
            // 
            this.btn_fresh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_fresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fresh.Image = global::POS.Desktop.Properties.Resources.freshjuices_icon;
            this.btn_fresh.Location = new System.Drawing.Point(6, 193);
            this.btn_fresh.Name = "btn_fresh";
            this.btn_fresh.Size = new System.Drawing.Size(102, 66);
            this.btn_fresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_fresh.TabIndex = 2;
            this.btn_fresh.TabStop = false;
            this.btn_fresh.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_softdrinks
            // 
            this.btn_softdrinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_softdrinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_softdrinks.Image = global::POS.Desktop.Properties.Resources.softdrinks_icon;
            this.btn_softdrinks.Location = new System.Drawing.Point(6, 107);
            this.btn_softdrinks.Name = "btn_softdrinks";
            this.btn_softdrinks.Size = new System.Drawing.Size(102, 66);
            this.btn_softdrinks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_softdrinks.TabIndex = 1;
            this.btn_softdrinks.TabStop = false;
            this.btn_softdrinks.Click += new System.EventHandler(this.category_Click);
            // 
            // btn_hotdrinks
            // 
            this.btn_hotdrinks.BackColor = System.Drawing.Color.White;
            this.btn_hotdrinks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btn_hotdrinks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_hotdrinks.Image = global::POS.Desktop.Properties.Resources.hotdrinks_icon;
            this.btn_hotdrinks.Location = new System.Drawing.Point(6, 22);
            this.btn_hotdrinks.Name = "btn_hotdrinks";
            this.btn_hotdrinks.Size = new System.Drawing.Size(102, 66);
            this.btn_hotdrinks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_hotdrinks.TabIndex = 0;
            this.btn_hotdrinks.TabStop = false;
            this.btn_hotdrinks.Click += new System.EventHandler(this.category_Click);
            // 
            // gbox_items
            // 
            this.gbox_items.Controls.Add(this.list_item);
            this.gbox_items.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbox_items.Location = new System.Drawing.Point(136, 12);
            this.gbox_items.Name = "gbox_items";
            this.gbox_items.Size = new System.Drawing.Size(555, 703);
            this.gbox_items.TabIndex = 2;
            this.gbox_items.TabStop = false;
            this.gbox_items.Text = "Items";
            // 
            // list_item
            // 
            this.list_item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_item.Location = new System.Drawing.Point(3, 20);
            this.list_item.Name = "list_item";
            this.list_item.Size = new System.Drawing.Size(549, 680);
            this.list_item.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.total_txt);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.receipt_item_list);
            this.groupBox3.Controls.Add(this.txt_taxes);
            this.groupBox3.Controls.Add(this.txt_services);
            this.groupBox3.Controls.Add(this.txt_amount);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lbl_netamount);
            this.groupBox3.Controls.Add(this.cancel_btn);
            this.groupBox3.Controls.Add(this.btn_print);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(692, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 703);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Receipt";
            // 
            // total_txt
            // 
            this.total_txt.AutoSize = true;
            this.total_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_txt.Location = new System.Drawing.Point(235, 556);
            this.total_txt.Name = "total_txt";
            this.total_txt.Size = new System.Drawing.Size(57, 18);
            this.total_txt.TabIndex = 11;
            this.total_txt.Text = "0 EGP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 555);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total:";
            // 
            // receipt_item_list
            // 
            this.receipt_item_list.Dock = System.Windows.Forms.DockStyle.Top;
            this.receipt_item_list.FormattingEnabled = true;
            this.receipt_item_list.ItemHeight = 18;
            this.receipt_item_list.Location = new System.Drawing.Point(3, 20);
            this.receipt_item_list.Name = "receipt_item_list";
            this.receipt_item_list.Size = new System.Drawing.Size(342, 472);
            this.receipt_item_list.TabIndex = 9;
            // 
            // txt_taxes
            // 
            this.txt_taxes.AutoSize = true;
            this.txt_taxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_taxes.Location = new System.Drawing.Point(235, 537);
            this.txt_taxes.Name = "txt_taxes";
            this.txt_taxes.Size = new System.Drawing.Size(57, 18);
            this.txt_taxes.TabIndex = 8;
            this.txt_taxes.Text = "0 EGP";
            // 
            // txt_services
            // 
            this.txt_services.AutoSize = true;
            this.txt_services.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_services.Location = new System.Drawing.Point(235, 517);
            this.txt_services.Name = "txt_services";
            this.txt_services.Size = new System.Drawing.Size(57, 18);
            this.txt_services.TabIndex = 7;
            this.txt_services.Text = "0 EGP";
            // 
            // txt_amount
            // 
            this.txt_amount.AutoSize = true;
            this.txt_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amount.Location = new System.Drawing.Point(235, 495);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(57, 18);
            this.txt_amount.TabIndex = 6;
            this.txt_amount.Text = "0 EGP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 536);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Taxes 14%:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 517);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Services 12%:";
            // 
            // lbl_netamount
            // 
            this.lbl_netamount.AutoSize = true;
            this.lbl_netamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_netamount.Location = new System.Drawing.Point(6, 498);
            this.lbl_netamount.Name = "lbl_netamount";
            this.lbl_netamount.Size = new System.Drawing.Size(70, 18);
            this.lbl_netamount.TabIndex = 3;
            this.lbl_netamount.Text = "Amount:";
            // 
            // cancel_btn
            // 
            this.cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel_btn.Image = global::POS.Desktop.Properties.Resources.cancel_btn;
            this.cancel_btn.Location = new System.Drawing.Point(51, 636);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.cancel_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cancel_btn.TabIndex = 1;
            this.cancel_btn.TabStop = false;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // btn_print
            // 
            this.btn_print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_print.Image = global::POS.Desktop.Properties.Resources.print;
            this.btn_print.Location = new System.Drawing.Point(185, 636);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(100, 50);
            this.btn_print.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_print.TabIndex = 0;
            this.btn_print.TabStop = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // POS_Screen_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 720);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbox_items);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "POS_Screen_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Screen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.POS_Screen_frm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_extra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_shisha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_deserts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cocktails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_smoothies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_fresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_softdrinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_hotdrinks)).EndInit();
            this.gbox_items.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cancel_btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_print)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbox_items;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox cancel_btn;
        private System.Windows.Forms.PictureBox btn_print;
        private System.Windows.Forms.PictureBox btn_hotdrinks;
        private System.Windows.Forms.PictureBox btn_softdrinks;
        private System.Windows.Forms.PictureBox btn_fresh;
        private System.Windows.Forms.PictureBox btn_smoothies;
        private System.Windows.Forms.PictureBox btn_cocktails;
        private System.Windows.Forms.PictureBox btn_deserts;
        private System.Windows.Forms.PictureBox btn_shisha;
        private System.Windows.Forms.FlowLayoutPanel list_item;
        private System.Windows.Forms.Label txt_taxes;
        private System.Windows.Forms.Label txt_services;
        private System.Windows.Forms.Label txt_amount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_netamount;
        private System.Windows.Forms.ListBox receipt_item_list;
        private System.Windows.Forms.Label total_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox btn_extra;
    }
}