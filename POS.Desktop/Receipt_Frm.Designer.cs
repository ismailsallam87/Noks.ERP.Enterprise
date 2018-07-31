namespace POS.Desktop
{
    partial class Receipt_Frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receipt_Frm));
            this.button1 = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_receipt_header = new System.Windows.Forms.Label();
            this.lbl_tel = new System.Windows.Forms.Label();
            this.lbl_rec_no = new System.Windows.Forms.Label();
            this.lbl_date = new System.Windows.Forms.Label();
            this.lbl_cashier = new System.Windows.Forms.Label();
            this.lbl_sharp = new System.Windows.Forms.Label();
            this.lbl_item = new System.Windows.Forms.Label();
            this.lbl_qty = new System.Windows.Forms.Label();
            this.lbl_price = new System.Windows.Forms.Label();
            this.receipt_item_list = new System.Windows.Forms.ListBox();
            this.total_txt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_taxes = new System.Windows.Forms.Label();
            this.txt_services = new System.Windows.Forms.Label();
            this.txt_amount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_netamount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.list_price = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(7, 581);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(260, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.ShowIcon = false;
            this.printPreviewDialog1.UseAntiAlias = true;
            this.printPreviewDialog1.Visible = false;
            this.printPreviewDialog1.Load += new System.EventHandler(this.printPreviewDialog1_Load);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::POS.Desktop.Properties.Resources.rockcafe_logo;
            this.pictureBox1.Location = new System.Drawing.Point(-101, -48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(316, 182);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_receipt_header
            // 
            this.lbl_receipt_header.Font = new System.Drawing.Font("Microsoft MHei", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_receipt_header.Location = new System.Drawing.Point(-1, 72);
            this.lbl_receipt_header.Name = "lbl_receipt_header";
            this.lbl_receipt_header.Size = new System.Drawing.Size(257, 20);
            this.lbl_receipt_header.TabIndex = 2;
            this.lbl_receipt_header.Text = "ROCK CAFE";
            this.lbl_receipt_header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_tel
            // 
            this.lbl_tel.AutoSize = true;
            this.lbl_tel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tel.Location = new System.Drawing.Point(118, 16);
            this.lbl_tel.Name = "lbl_tel";
            this.lbl_tel.Size = new System.Drawing.Size(124, 15);
            this.lbl_tel.TabIndex = 3;
            this.lbl_tel.Text = "TEL:01277222111";
            // 
            // lbl_rec_no
            // 
            this.lbl_rec_no.AutoSize = true;
            this.lbl_rec_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rec_no.Location = new System.Drawing.Point(118, 36);
            this.lbl_rec_no.Name = "lbl_rec_no";
            this.lbl_rec_no.Size = new System.Drawing.Size(44, 15);
            this.lbl_rec_no.TabIndex = 4;
            this.lbl_rec_no.Text = "Rec#:";
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(118, 53);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(12, 15);
            this.lbl_date.TabIndex = 5;
            this.lbl_date.Text = "-";
            // 
            // lbl_cashier
            // 
            this.lbl_cashier.AutoSize = true;
            this.lbl_cashier.Font = new System.Drawing.Font("Microsoft MHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cashier.Location = new System.Drawing.Point(7, 112);
            this.lbl_cashier.Name = "lbl_cashier";
            this.lbl_cashier.Size = new System.Drawing.Size(47, 15);
            this.lbl_cashier.TabIndex = 6;
            this.lbl_cashier.Text = "Cashier:";
            // 
            // lbl_sharp
            // 
            this.lbl_sharp.AutoSize = true;
            this.lbl_sharp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sharp.Location = new System.Drawing.Point(2, 137);
            this.lbl_sharp.Name = "lbl_sharp";
            this.lbl_sharp.Size = new System.Drawing.Size(15, 13);
            this.lbl_sharp.TabIndex = 7;
            this.lbl_sharp.Text = "#";
            // 
            // lbl_item
            // 
            this.lbl_item.AutoSize = true;
            this.lbl_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_item.Location = new System.Drawing.Point(52, 137);
            this.lbl_item.Name = "lbl_item";
            this.lbl_item.Size = new System.Drawing.Size(31, 13);
            this.lbl_item.TabIndex = 8;
            this.lbl_item.Text = "Item";
            // 
            // lbl_qty
            // 
            this.lbl_qty.AutoSize = true;
            this.lbl_qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_qty.Location = new System.Drawing.Point(18, 137);
            this.lbl_qty.Name = "lbl_qty";
            this.lbl_qty.Size = new System.Drawing.Size(32, 13);
            this.lbl_qty.TabIndex = 9;
            this.lbl_qty.Text = "QTY";
            // 
            // lbl_price
            // 
            this.lbl_price.AutoSize = true;
            this.lbl_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_price.Location = new System.Drawing.Point(196, 137);
            this.lbl_price.Name = "lbl_price";
            this.lbl_price.Size = new System.Drawing.Size(36, 13);
            this.lbl_price.TabIndex = 10;
            this.lbl_price.Text = "Price";
            // 
            // receipt_item_list
            // 
            this.receipt_item_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receipt_item_list.FormattingEnabled = true;
            this.receipt_item_list.ItemHeight = 15;
            this.receipt_item_list.Location = new System.Drawing.Point(1, 153);
            this.receipt_item_list.Name = "receipt_item_list";
            this.receipt_item_list.Size = new System.Drawing.Size(189, 274);
            this.receipt_item_list.TabIndex = 11;
            // 
            // total_txt
            // 
            this.total_txt.AutoSize = true;
            this.total_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_txt.Location = new System.Drawing.Point(177, 495);
            this.total_txt.Name = "total_txt";
            this.total_txt.Size = new System.Drawing.Size(57, 18);
            this.total_txt.TabIndex = 20;
            this.total_txt.Text = "0 EGP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 494);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 18);
            this.label4.TabIndex = 19;
            this.label4.Text = "Total:";
            // 
            // txt_taxes
            // 
            this.txt_taxes.AutoSize = true;
            this.txt_taxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_taxes.Location = new System.Drawing.Point(177, 476);
            this.txt_taxes.Name = "txt_taxes";
            this.txt_taxes.Size = new System.Drawing.Size(57, 18);
            this.txt_taxes.TabIndex = 18;
            this.txt_taxes.Text = "0 EGP";
            // 
            // txt_services
            // 
            this.txt_services.AutoSize = true;
            this.txt_services.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_services.Location = new System.Drawing.Point(177, 456);
            this.txt_services.Name = "txt_services";
            this.txt_services.Size = new System.Drawing.Size(57, 18);
            this.txt_services.TabIndex = 17;
            this.txt_services.Text = "0 EGP";
            // 
            // txt_amount
            // 
            this.txt_amount.AutoSize = true;
            this.txt_amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amount.Location = new System.Drawing.Point(177, 434);
            this.txt_amount.Name = "txt_amount";
            this.txt_amount.Size = new System.Drawing.Size(57, 18);
            this.txt_amount.TabIndex = 16;
            this.txt_amount.Text = "0 EGP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "Taxes 14%:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Services 12%:";
            // 
            // lbl_netamount
            // 
            this.lbl_netamount.AutoSize = true;
            this.lbl_netamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_netamount.Location = new System.Drawing.Point(6, 437);
            this.lbl_netamount.Name = "lbl_netamount";
            this.lbl_netamount.Size = new System.Drawing.Size(70, 18);
            this.lbl_netamount.TabIndex = 13;
            this.lbl_netamount.Text = "Amount:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 536);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(301, 18);
            this.label3.TabIndex = 21;
            this.label3.Text = "Thank You";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft MHei", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 513);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(273, 22);
            this.label5.TabIndex = 22;
            this.label5.Text = "Coffee solves everything";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // list_price
            // 
            this.list_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_price.FormattingEnabled = true;
            this.list_price.ItemHeight = 15;
            this.list_price.Location = new System.Drawing.Point(193, 153);
            this.list_price.Name = "list_price";
            this.list_price.Size = new System.Drawing.Size(78, 274);
            this.list_price.TabIndex = 23;
            // 
            // Receipt_Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(308, 621);
            this.ControlBox = false;
            this.Controls.Add(this.list_price);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.total_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_taxes);
            this.Controls.Add(this.txt_services);
            this.Controls.Add(this.txt_amount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_netamount);
            this.Controls.Add(this.receipt_item_list);
            this.Controls.Add(this.lbl_price);
            this.Controls.Add(this.lbl_qty);
            this.Controls.Add(this.lbl_item);
            this.Controls.Add(this.lbl_sharp);
            this.Controls.Add(this.lbl_cashier);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.lbl_rec_no);
            this.Controls.Add(this.lbl_tel);
            this.Controls.Add(this.lbl_receipt_header);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Receipt_Frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt Preview";
            this.Load += new System.EventHandler(this.Receipt_Frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_receipt_header;
        private System.Windows.Forms.Label lbl_tel;
        private System.Windows.Forms.Label lbl_rec_no;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.Label lbl_cashier;
        private System.Windows.Forms.Label lbl_sharp;
        private System.Windows.Forms.Label lbl_item;
        private System.Windows.Forms.Label lbl_qty;
        private System.Windows.Forms.Label lbl_price;
        private System.Windows.Forms.ListBox receipt_item_list;
        private System.Windows.Forms.Label total_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txt_taxes;
        private System.Windows.Forms.Label txt_services;
        private System.Windows.Forms.Label txt_amount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_netamount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox list_price;
    }
}