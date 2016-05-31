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
        
        
        public AdminForm()
        {
            InitializeComponent();
            AddPassengerToComboBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string info = comboBox1.SelectedItem.ToString();
            string[] parts = info.Split(' '); // [2] 
            comboBox2.Items.Clear();
            AddUserToAdminComboBox adU = new AddUserToAdminComboBox();
            List <string> list = adU.add_ticket_to_admin_comobobox(parts[2]);
            for (int i = 0; i< list.Count; i++)
            {
                comboBox2.Items.Add(list[i]);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void AddPassengerToComboBox()
        {
            AddUserToAdminComboBox adm = new AddUserToAdminComboBox();
            List<string> list = adm.ReadUsers();
            for(int i=0;i<list.Count;i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }
    }


}
