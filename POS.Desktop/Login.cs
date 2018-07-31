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
namespace POS.Desktop
{
    public partial class Login : Form
    {
        private Noks_POSEntities db = new Noks_POSEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (
                txt_username.Text != null && txt_username.Text != "" 
                &&
                txt_password.Text != null && txt_password.Text != ""
                )
            {
                long? session_id = db.POS_Log(txt_username.Text, txt_password.Text).FirstOrDefault();
                if (session_id > 0)
                {
                    POS.Desktop.Helpers.Session.Session_ID = session_id;
                    POS.Desktop.Helpers.Session.Cashier = txt_username.Text;
                    POS_Midifrm newMidi = new POS_Midifrm();
                    newMidi.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid User Name or Password !");
                }
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
