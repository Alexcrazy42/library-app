using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LibraryApp
{
    public partial class educational2 : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\EdBooks.db";

        public educational2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & textBox5.Text != "" &
                textBox6.Text != "" & textBox7.Text != "" & textBox9.Text != "" & textBox10.Text != "" &
                textBox11.Text != "" & textBox12.Text != "")
            {
                string bookName = textBox1.Text;
                string author = textBox2.Text;
                string subject = textBox3.Text;
                string publish = textBox4.Text;
                string clas = textBox5.Text;
                string FPU = textBox6.Text;
                string edLevel = textBox7.Text;
                string termUse = textBox9.Text;
                string amount = textBox11.Text;
                string year = textBox12.Text;
                string series = textBox13.Text;
                string[] array = { bookName, author, subject, publish, clas, FPU, edLevel, termUse, amount, year, series };
                InsertInDB(array);
            }
            else
            {
                MessageBox.Show("Заполните все данные!");
            }
            

        }


        private void InsertInDB(string[] array)
        {
            

            SQLiteConnection con = new SQLiteConnection(dbName);
            con.Open();
            string query = $"INSERT INTO EdBooks (bookName, author, subject, publish, clas, FPU, edLevel, termUse, amount, year, series) VALUES ('{array[0]}', '{array[1]}', '{array[2]}', '{array[3]}', '{array[4]}','{array[5]}','{array[6]}','{array[7]}','{array[8]}','{array[9]}','{array[10]}');";
            
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        
    }
}
