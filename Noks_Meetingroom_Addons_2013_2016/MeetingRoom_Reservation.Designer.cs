namespace Noks_Meetingroom_Addons_2013_2016
{
    [System.ComponentModel.ToolboxItemAttribute(false)]
    partial class MeetingRoom_Reservation : Microsoft.Office.Tools.Outlook.FormRegionBase
    {
        public MeetingRoom_Reservation(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            : base(Globals.Factory, formRegion)
        {
            this.InitializeComponent();
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1261, 55);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(233, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check Meeting Rooms Availability";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Noks_Meetingroom_Addons_2013_2016.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(38, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "From:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(233, 69);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(267, 26);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(195, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "To:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(233, 115);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(267, 26);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.Value = new System.DateTime(2018, 3, 6, 11, 30, 33, 0);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.LightCyan;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(233, 203);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(400, 28);
            this.comboBox1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Meeting Rooms:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(651, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 29);
            this.button1.TabIndex = 7;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightCyan;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(233, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(400, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Filter";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker3.Location = new System.Drawing.Point(506, 68);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(127, 26);
            this.dateTimePicker3.TabIndex = 2;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker4.Location = new System.Drawing.Point(506, 115);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(127, 26);
            this.dateTimePicker4.TabIndex = 4;
            // 
            // MeetingRoom_Reservation
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dateTimePicker4);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "MeetingRoom_Reservation";
            this.Size = new System.Drawing.Size(1261, 326);
            this.FormRegionShowing += new System.EventHandler(this.MeetingRoom_Reservation_FormRegionShowing);
            this.FormRegionClosed += new System.EventHandler(this.MeetingRoom_Reservation_FormRegionClosed);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Form Region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private static void InitializeManifest(Microsoft.Office.Tools.Outlook.FormRegionManifest manifest, Microsoft.Office.Tools.Outlook.Factory factory)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeetingRoom_Reservation));
            Microsoft.Office.Tools.Outlook.FormRegionCustomAction formRegionCustomActionImpl1 = factory.CreateFormRegionCustomAction();
            formRegionCustomActionImpl1.Name = "formRegionCustomAction1";
            formRegionCustomActionImpl1.Title = "Reserve meeting room";
            manifest.CustomActions.Add(formRegionCustomActionImpl1);
            manifest.FormRegionName = "Book Meeting Room";
            manifest.Icons.Default = ((System.Drawing.Icon)(resources.GetObject("MeetingRoom_Reservation.Manifest.Icons.Default")));
            manifest.Icons.Page = global::Noks_Meetingroom_Addons_2013_2016.Properties.Resources.favicon;
            manifest.Icons.Window = ((System.Drawing.Icon)(resources.GetObject("MeetingRoom_Reservation.Manifest.Icons.Window")));
            manifest.ShowReadingPane = false;
            manifest.Title = "Booking Meeting Room";
            manifest.Version = "v1.0";

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;

        public partial class MeetingRoom_ReservationFactory : Microsoft.Office.Tools.Outlook.IFormRegionFactory
        {
            public event Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler FormRegionInitializing;

            private Microsoft.Office.Tools.Outlook.FormRegionManifest _Manifest;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MeetingRoom_ReservationFactory()
            {
                this._Manifest = Globals.Factory.CreateFormRegionManifest();
                MeetingRoom_Reservation.InitializeManifest(this._Manifest, Globals.Factory);
                this.FormRegionInitializing += new Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler(this.MeetingRoom_ReservationFactory_FormRegionInitializing);
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Microsoft.Office.Tools.Outlook.FormRegionManifest Manifest
            {
                get
                {
                    return this._Manifest;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.IFormRegion Microsoft.Office.Tools.Outlook.IFormRegionFactory.CreateFormRegion(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            {
                MeetingRoom_Reservation form = new MeetingRoom_Reservation(formRegion);
                form.Factory = this;
                return form;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            byte[] Microsoft.Office.Tools.Outlook.IFormRegionFactory.GetFormRegionStorage(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
            {
                throw new System.NotSupportedException();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            bool Microsoft.Office.Tools.Outlook.IFormRegionFactory.IsDisplayedForItem(object outlookItem, Microsoft.Office.Interop.Outlook.OlFormRegionMode formRegionMode, Microsoft.Office.Interop.Outlook.OlFormRegionSize formRegionSize)
            {
                if (this.FormRegionInitializing != null)
                {
                    Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs cancelArgs = Globals.Factory.CreateFormRegionInitializingEventArgs(outlookItem, formRegionMode, formRegionSize, false);
                    this.FormRegionInitializing(this, cancelArgs);
                    return !cancelArgs.Cancel;
                }
                else
                {
                    return true;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.FormRegionKindConstants Microsoft.Office.Tools.Outlook.IFormRegionFactory.Kind
            {
                get
                {
                    return Microsoft.Office.Tools.Outlook.FormRegionKindConstants.WindowsForms;
                }
            }
        }
    }

    partial class WindowFormRegionCollection
    {
        internal MeetingRoom_Reservation MeetingRoom_Reservation
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.GetType() == typeof(MeetingRoom_Reservation))
                        return (MeetingRoom_Reservation)item;
                }
                return null;
            }
        }
    }
}
