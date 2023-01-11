using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SQLite;


namespace LibraryApp
{
    public partial class subscription_new_class : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";

        public subscription_new_class()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stm = "SELECT Фамилия FROM students WHERE Класс=-1";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string classes = cmd.ExecuteScalar().ToString();
            classes = classes.Remove(classes.Length - 1);
            string[] classes1 = classes.Split(' ');
            con.Cancel();
            if (textBox1.Text != "" & classes1.Contains(textBox1.Text))
            {
                MessageBox.Show("Такой класс уже существует!");
            }
            else
            {
                classes = GetClasses();
                if (textBox1.Text != "")
                {
                    string stm1 = $"UPDATE students SET Фамилия='{classes + textBox1.Text + " "}' WHERE Класс=-1";
                    
                    var con1 = new SQLiteConnection(dbName);
                    con1.Open();

                    var cmd1 = new SQLiteCommand(stm1, con1);
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    MessageBox.Show("Класс создан!");
                }
                else 
                {
                    MessageBox.Show("Отсутствует название класса!");
                }
                
            }
            
        }

        public string GetClasses()
        {
            string stm = "SELECT Фамилия FROM students WHERE Класс=-1";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string classes = cmd.ExecuteScalar().ToString();
            con.Close();
            return classes;
        }
    }
}
