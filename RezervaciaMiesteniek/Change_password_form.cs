using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezervaciaMiesteniek
{
    public partial class Change_password_form : Form
    {
        private string userID;
        public Change_password_form(string ID)
        {
            this.userID = ID;
            InitializeComponent();
            textBox1.PasswordChar = '\u2022'; //nastavenie symbolu 
            textBox2.PasswordChar = '\u2022'; //nastavenie symbolu 
            textBox3.PasswordChar = '\u2022'; //nastavenie symbolu 

            label4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change_users_settings change_settings = new Change_users_settings();

            if (textBox2.Text.Equals(textBox3.Text) && !textBox2.Equals("") && change_settings.check_password(userID, textBox1.Text))
            {
                label4.Text = "";
                change_settings.change_password(userID, textBox2.Text);
                this.Dispose();
            }
            else
            {
                label4.Text = "The password you entered does´t match";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
