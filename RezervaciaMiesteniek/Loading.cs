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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Shown(object sender, EventArgs e)
        {
            AutoInsertPlane insertPlane = new AutoInsertPlane();
            insertPlane.autoInset();
            this.Dispose();
        }
    }
}
