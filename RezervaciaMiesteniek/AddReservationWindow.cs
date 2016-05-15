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
    public partial class AddReservationWindow : Form
    {
        private DateTime selectedDate;
        private string from;
        private string startTime;

        public AddReservationWindow()
        {
            InitializeComponent();
            selectedDate = DateTime.Now;
          //  setFromAndTime();
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

            Console.WriteLine(from);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedDate = dateTimePicker1.Value;
            Console.WriteLine(selectedDate.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFromAndTime();
            Console.WriteLine(startTime);
        }

        private void AddReservationWindow_Shown(object sender, EventArgs e)
        {
            setFromAndTime();
        }
    }
}
