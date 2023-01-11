using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApp.Program;

namespace LibraryApp
{
    public partial class fiction_udk : Form
    {
        public fiction_udk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UDK.Value = treeView1.SelectedNode.Text;
            this.Close();
        }
    }
}
