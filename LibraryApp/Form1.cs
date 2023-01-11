using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using static LibraryApp.Program;
using System.Drawing;

namespace LibraryApp
{
    public partial class Form1 : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";



        public Form1()
        {
            InitializeComponent();
            label2.Text = school;

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            



            subscription3 subscr = (subscription3)Application.OpenForms["subscription3"];
            subscription3 sub = new subscription3();
            sub.ShowDialog();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            fiction1 fic = (fiction1)Application.OpenForms["fiction1"];
            fiction1 f = new fiction1();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            educational1 ed = new educational1();
            ed.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            lendingBooks2 lend = new lendingBooks2();
            lend.ShowDialog();
        }

       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Вы действительно хотите выйти из приложения?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
