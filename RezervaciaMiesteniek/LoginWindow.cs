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
    public partial class LoginWindow : Form
    {
        private string password, login;

        public LoginWindow()
        {
            InitializeComponent();
            textBox2.PasswordChar = '\u2022'; //nastavenie simbolu hesla
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e) //prihlasenie
        {
            string userID;
            login = textBox1.Text;
            password = textBox2.Text;

            LoginUser user = new LoginUser();

            if (user.loginVerification(login, password)) //prihlasenie sa 
            {
                userID = user.getUserId(login); //ziskanie id pouzivatela
                if (user.isAdmin(userID))
                {
                    AdminForm adw = new AdminForm(userID);
                    this.Hide();
                    adw.ShowDialog();
                }
                MainForm mainform = new MainForm(userID);
                this.Hide();
                mainform.ShowDialog();
                this.Show();
                textBox2.Text = "";
                label1.Text = "";
            }
            else
            {
                label1.Text = "wrong username or password";
            }


        }

        private void button2_Click(object sender, EventArgs e) //nova registracia
        {
            AddUserWindow addUser = new AddUserWindow();
            this.Hide();
            addUser.ShowDialog();
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            Loading load = new Loading();
            load.ShowDialog();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
