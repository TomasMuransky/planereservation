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
        private String Admin;
        
        public AdminForm()
        {
            InitializeComponent();
            AddPassengerToComboBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
