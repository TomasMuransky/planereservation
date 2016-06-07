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
            add_planes_to_combobox();
            label1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string info = comboBox1.SelectedItem.ToString();
            string[] parts = info.Split(' '); // [2] 
            comboBox2.Items.Clear();
            AddUserToAdminComboBox adU = new AddUserToAdminComboBox();
            List <string> list = adU.add_ticket_to_admin_combobox(parts[2]);
            for (int i = 0; i< list.Count; i++)
            {
                comboBox2.Items.Add(list[i]);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //remove reservation
        {
            DialogResult result = MessageBox.Show("Are you sure that you want to remove your reservation?", "warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (comboBox2.SelectedItem != null)
                {
                    string info = comboBox2.SelectedItem.ToString();
                    string[] parts = info.Split(' '); //ticket ID = [2]; passenger ID= [5]; plane ID = [8]; seat ID [11]

                    Work_with_planes work = new Work_with_planes();
                    work.remove_reservation_from_admin(parts[5], parts[2], parts[8], parts[11]);
                }
                /****************************************/
                if (comboBox1.SelectedItem != null) //ubdate tickets
                {
                    string info2 = comboBox1.SelectedItem.ToString();
                    string[] parts2 = info2.Split(' '); // [2] 
                    comboBox2.Items.Clear();
                    AddUserToAdminComboBox adU = new AddUserToAdminComboBox();
                    List<string> list = adU.add_ticket_to_admin_combobox(parts2[2]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        comboBox2.Items.Add(list[i]);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e) //remove user
        {
            DialogResult result = MessageBox.Show("Are you sure that you want to remove the selected user?", "you might regret it ", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (comboBox1.SelectedItem != null)
                {
                    string source = comboBox1.SelectedItem.ToString();
                    string[] parts = source.Split(' '); //passenger id=[2];
                    Work_with_planes work = new Work_with_planes();
                    work.remove_user_from_admin(parts[2]);
                    /************************************/
                    comboBox2.Items.Clear();
                    AddPassengerToComboBox();
                }
            }
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
            comboBox1.Items.Clear();
            for(int i=0;i<list.Count;i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }

        private void add_planes_to_combobox()
        {
            comboBox3.Items.Clear();
            Work_with_planes planes = new Work_with_planes();
            List<string> list = planes.add_plane_list_listbox();
            for (int i = 0; i < list.Count; i++)
            {
                comboBox3.Items.Add(list[i]);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string info = comboBox3.SelectedItem.ToString();
            string[] parts = info.Split(' '); // id = [2];
            Work_with_planes work = new Work_with_planes();
            int cout = work.get_count_of_reservated_seats(parts[2]);
            label1.Text = "resevated seats: " + cout + "-80";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (comboBox3.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Are you sure that you want to remove the fly?", "warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string info = comboBox3.SelectedItem.ToString();
                    string[] parts = info.Split(' '); // id = [2];
                    Work_with_planes work = new Work_with_planes();
                    work.remove_fly(parts[2]);
                    add_planes_to_combobox();
                }
            }
            
            
        }
    }


}
