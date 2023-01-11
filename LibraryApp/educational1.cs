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
    public partial class educational1 : Form
    {
        public educational1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            educational2 edu2 = (educational2)Application.OpenForms["educational2"];
            educational2 ed2 = new educational2();
            ed2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            educational3 edu3 = (educational3)Application.OpenForms["educational3"];
            educational3 ed3 = new educational3();
            ed3.ShowDialog();
        }
    }
}
