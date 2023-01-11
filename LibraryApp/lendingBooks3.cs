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
    public partial class lendingBooks3 : Form
    {
        public lendingBooks3()
        {
            InitializeComponent();
        }

        private void lendingBooks3_Load(object sender, EventArgs e)
        {
            label1.Text = student["Фамилия"] + " " + student["Имя"] + " " + student["Отчество"] + " " + student["Класс"];
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
