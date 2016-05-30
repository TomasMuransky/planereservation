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
    public partial class AdminForm : Form
    {
        private string passengerID;
        private DialogResult result;

        public AdminForm()
        {
            InitializeComponent();
            AddPassengerToComboBox();
            AddPassengerReservationsToComboBox();
            this.passengerID = passengerID;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                result = MessageBox.Show("Are you sure that you want to remove this reservation?", "warning", MessageBoxButtons.YesNo);


                if (result == DialogResult.Yes)
                {

                    string ticketStr = comboBox2.GetItemText(comboBox2.SelectedItem);
                    RemoveReservation rem = new RemoveReservation();
                    rem.removeReservation(ticketStr, passengerID);
                    /********************************************************************/
                    comboBox2.Items.Clear();
                    AddUserReservationsToComboBox adm = new AddUserReservationsToComboBox();
                    List<string> list = adm.ReadUserReservations();
                    for (int i = 0; i < list.Count; i++)
                    {
                        comboBox2.Items.Add(list[i]);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*if (comboBox1.SelectedItem != null)
            {
                result = MessageBox.Show("Are you sure that you want to remove this passenger?", "warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                   
                    string ticketStr = comboBox1.GetItemText(comboBox1.SelectedItem);
                    RemoveUser rem1 = new RemoveUser();
                    rem1.removeUser(ticketStr, passengerID);
                    /********************************************************************/
                    /*comboBox1.Items.Clear();
                    AddUserToAdminComboBox adm = new AddUserToAdminComboBox();
                    List<string> list1 = adm.ReadUsers();
                    for (int i = 0; i < list1.Count; i++)
                    {
                        comboBox1.Items.Add(list1[i]);
                    }
                }
            }*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox2.Text);
        }
        
        private void AddPassengerToComboBox()
        {
            comboBox1.Items.Clear();
            AddUserToAdminComboBox adm = new AddUserToAdminComboBox();
            List<string> list = adm.ReadUsers();
            for (int i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }

        private void AddPassengerReservationsToComboBox()
        {
            comboBox2.Items.Clear();
            AddUserReservationsToComboBox adm = new AddUserReservationsToComboBox();
            List<string> list1 = adm.ReadUserReservations();
            for (int i = 0; i < list1.Count; i++)
            {
                comboBox2.Items.Add(list1[i]);
            }
        }
    }


}
