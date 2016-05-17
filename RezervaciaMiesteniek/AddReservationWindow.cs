using System;
using System.Collections;
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
    public partial class AddReservationWindow : Form
    {
        private string passengerID;
        private DateTime selectedDate;
        private string from; //miesto odletu
        private string startTime; //cas odletu
        private string planeID;

        public AddReservationWindow(string ID)
        {
            InitializeComponent();
            selectedDate = DateTime.Now;
            setFromAndTime();
            this.passengerID = ID;
            AddTicket ticket = new AddTicket();
            planeID = ticket.getPlaneID(selectedDate, from, startTime);
            setSeatsMenu(planeID);
        }

        private void setSeatsMenu(string pl_ID)
        {
            comboBox3.Items.Clear();
            AddTicket ticket = new AddTicket();
            List<string> seatsList = ticket.getSeatsList(pl_ID);
            for (int i = 0; i < seatsList.Count; i++)
            {
                comboBox3.Items.Add(seatsList[i]);
            }
            if (seatsList[0] != null)
                comboBox3.SelectedIndex = 0;
        }
        
        private void setFromAndTime()
        {
            if (comboBox1.SelectedItem.ToString().Equals("Kosice - Bratislava"))
            {
                from = "Kosice";
            }
            else
            {
                from = "Bratislava";
            }

            if (comboBox2.SelectedItem.ToString().Equals("7:00"))
            {
                startTime = "07:00:00";
            }
            else
            {
                startTime = "19:00:00";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFromAndTime();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedDate = dateTimePicker1.Value;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFromAndTime();
        }

 
    }
}
