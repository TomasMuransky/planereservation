using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezervaciaMiesteniek
{
    public partial class Change_mail_phone : Form
    {
        private string user_id;
        public Change_mail_phone(string ID)
        {
            InitializeComponent();
            this.user_id = ID;
        }

        private bool phoneReg(string Phone) //regex phone verification
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool mailReg(string emailaddress) //e-mail verification taky mensy hack :) 
        {
            if (emailaddress.Equals(""))
                return false;
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = textBox2.Text;
            string mail = textBox1.Text;
            Console.WriteLine("mail " + mail);
            Console.WriteLine("phone " + phone);
            if (!phone.Equals(""))
            {
                if(phoneReg(phone))
                {
                    Change_users_settings ch_phone = new Change_users_settings();
                    ch_phone.change_phone(user_id, phone);
                }
            }

            if (!mail.Equals(""))
            {
                if (mailReg(mail))
                {
                    Change_users_settings ch_mail = new Change_users_settings();
                    ch_mail.change_mail(user_id, mail);
                }
            }
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
