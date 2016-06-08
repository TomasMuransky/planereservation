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
    public partial class AddUserWindow : Form
    {
        public AddUserWindow()
        {
            InitializeComponent();
            textBox2.PasswordChar = '\u2022';
            textBox3.PasswordChar = '\u2022';
            /***************************/
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            /***************************/
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


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string password_werification = textBox3.Text;
            string email = textBox4.Text;
            string phone = textBox5.Text;
            string first_name = textBox6.Text;
            string last_name = textBox7.Text;
            /***************************************/
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            /**************************************/

            if (!login.Equals("") && !password.Equals("") && password.Equals(password_werification) &&
                mailReg(email) && phoneReg(phone) && !first_name.Equals("") && !last_name.Equals(""))
            {
               
                DataBase data = new DataBase();
                if (data.isLoginUnique(login)) //oveerenie jedinecnosti loginu
                {
                    data.addNewRegistreation(login, password, email, phone, first_name, last_name);  //registravia noveho klienta
                    this.Dispose();
                }
                else
                {
                    label5.Text = "login is not unique";
                }

               

               
            }
            else //tu budu funkcie vypisujuce chybove hlasky
            {
                if (login.Equals(""))
                    label5.Text = "username is required";
                if (!password.Equals(password_werification))
                    label7.Text = "password verification failed";
                if (password.Equals(""))
                    label6.Text = "password is required";
                if (!phoneReg(phone))
                    label12.Text = "wrong telephone number format";
                if (!mailReg(email))
                    label8.Text = "wrong e-mail adress format";
                if (first_name.Equals(""))
                    label13.Text = "first name is required";
                if (last_name.Equals(""))
                    label14.Text = "last name is required";
               
            }
            
        }
    }
}
