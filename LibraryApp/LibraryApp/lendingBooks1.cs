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
    public partial class lendingBooks1 : Form
    {
        public lendingBooks1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lendingBooks2 lend2 = (lendingBooks2)Application.OpenForms["lendingBooks2"];
            lendingBooks2 l2 = new lendingBooks2();
            l2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lendingBooks3 lend3 = (lendingBooks3)Application.OpenForms["lendingBooks3"];
            lendingBooks3 l3 = new lendingBooks3();
            l3.ShowDialog();
        }
    }
}
