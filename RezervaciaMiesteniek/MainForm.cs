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

        private void button3_Click(object sender, EventArgs e) //remove reservation
        {
            if (listBox1.SelectedItem != null)
            {
                result = MessageBox.Show("Are you sure to remove reservation?", "warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //z tade sa zavola funkcia na odstranenie rezervacie
                    string ticketStr = listBox1.GetItemText(listBox1.SelectedItem);
                    RemoveReservation rem = new RemoveReservation();
                    rem.removeReservation(ticketStr,userID);
                    /********************************************************************/
                    listBox1.Items.Clear();
                    AddTicketToListbox tickets = new AddTicketToListbox();
                    List<string> list = tickets.getTicketsList(userID);
                    for (int i = 0; i < list.Count; i++)
                    {
                        listBox1.Items.Add(list[i]);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //z tade sa zavola formular na pridanie rezervacie
            this.Hide();
            AddReservationWindow addReservation = new AddReservationWindow(userID);
            addReservation.ShowDialog();
            this.Show();
            /******************************************/
            listBox1.Items.Clear();
            AddTicketToListbox tickets = new AddTicketToListbox();
            List<string> list = tickets.getTicketsList(userID);
            for (int i = 0; i < list.Count; i++)
            {
                listBox1.Items.Add(list[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Change_password_form change = new Change_password_form(userID);
            change.ShowDialog();
            this.Show();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AddTicketToListbox tickets = new AddTicketToListbox();
            List<string> list = tickets.getTicketsList(userID);
            for (int i = 0; i < list.Count; i++)
            {
                listBox1.Items.Add(list[i]);
            }
        }

        private void button5_Click(object sender, EventArgs e) //change mail/phone
        {
            Change_mail_phone ch_mail_phone = new Change_mail_phone(userID);
            this.Hide();
            ch_mail_phone.ShowDialog();
            this.Show();
        }
    }
}
