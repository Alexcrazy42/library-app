using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class fiction1 : Form
    {
        public fiction1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fiction2 fict2 = (fiction2)Application.OpenForms["fiction2"];
            fiction2 f2 = new fiction2();
            f2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fiction3 fict3 = (fiction3)Application.OpenForms["fiction3"];
            fiction3 f3 = new fiction3();
            f3.ShowDialog();
        }

        
    }
}
