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
    public partial class MainForm : Form
    {
        private string userID; //sem sa ulozi id prihlaseneho uzivatela
        private DialogResult result;

        public MainForm(string id)
        {
            InitializeComponent();
            this.userID = id;
            string[] name = new string[2];
            LoginUser log = new LoginUser();
            name = log.getUserName(userID);
            label1.Text = name[0];
            label2.Text = name[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result = MessageBox.Show("You are shure to remove reservation?", "varning", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                System.Console.WriteLine("akcia potvrdena");
                //z tade sa zavola funkcia na odstranenie rezervacie
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //z tade sa zavola formular na pridanie rezervacie
            AddReservationWindow addReservation = new AddReservationWindow();
            this.Hide();
            addReservation.ShowDialog();
            this.Show();
        }
    }
}
