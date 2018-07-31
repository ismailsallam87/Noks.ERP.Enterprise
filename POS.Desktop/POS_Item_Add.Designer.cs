namespace POS.Desktop
{
    partial class POS_Item_Add
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.login_btn = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_item = new System.Windows.Forms.TextBox();
            this.lbl_price = new System.Windows.Forms.Label();
            this.lbl_item = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Yellow;
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(117, 93);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(166, 31);
            this.btn_cancel.TabIndex = 13;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // login_btn
            // 
            this.login_btn.BackColor = System.Drawing.Color.YellowGreen;
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.Location = new System.Drawing.Point(324, 93);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(166, 31);
            this.login_btn.TabIndex = 12;
            this.login_btn.Text = "+";
            this.login_btn.UseVisualStyleBackColor = false;
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_password.Location = new System.Drawing.Point(117, 49);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(73, 23);
            this.txt_password.TabIndex = 11;
            // 
            // txt_item
            // 
            this.txt_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_item.Location = new System.Drawing.Point(117, 12);
            this.txt_item.Name = "txt_item";
            this.txt_item.ReadOnly = true;
            this.txt_item.Size = new System.Drawing.Size(373, 23);
            this.txt_item.TabIndex = 10;
            // 
            // lbl_price
            // 
            this.lbl_price.AutoSize = true;
            this.lbl_price.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_price.Location = new System.Drawing.Point(13, 50);
            this.lbl_price.Name = "lbl_price";
            this.lbl_price.Size = new System.Drawing.Size(46, 18);
            this.lbl_price.TabIndex = 9;
            this.lbl_price.Text = "QTY:";
            // 
            // lbl_item
            // 
            this.lbl_item.AutoSize = true;
            this.lbl_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_item.Location = new System.Drawing.Point(13, 13);
            this.lbl_item.Name = "lbl_item";
            this.lbl_item.Size = new System.Drawing.Size(45, 18);
            this.lbl_item.TabIndex = 8;
            this.lbl_item.Text = "Item:";
            // 
            // POS_Item_Add
            // 
            this.AcceptButton = this.login_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(507, 130);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_item);
            this.Controls.Add(this.lbl_price);
            this.Controls.Add(this.lbl_item);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "POS_Item_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_item;
        private System.Windows.Forms.Label lbl_price;
        private System.Windows.Forms.Label lbl_item;
    }
}