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
    public partial class subscription_delete_class : Form
    {
        
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";

        public subscription_delete_class()
        {
            InitializeComponent();
            string stm = "SELECT Фамилия FROM students WHERE Класс=-1";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string classes = cmd.ExecuteScalar().ToString();
            classes = classes.Remove(classes.Length - 1);
            string[] classes1 = classes.Split(' ');
            if (classes.Length > 0)
            {
                foreach (string c in classes1)
                {
                    comboBox1.Items.Add(c);
                }
            }
            else
            {
                comboBox1.Text = "Нет классов";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stm = "SELECT Фамилия FROM students WHERE Класс=-1";

            var con = new SQLiteConnection(dbName);
            con.Open();
            var cmd = new SQLiteCommand(stm, con);
            string classes = cmd.ExecuteScalar().ToString();
            
            con.Dispose(); 
            if (comboBox1.Text != "")
            {
                classes = classes.Replace(comboBox1.Text + " ", "");
                string stm1 = $"UPDATE students SET Фамилия = '{classes}' WHERE Класс=-1";
                
                var con1 = new SQLiteConnection(dbName);
                con1.Open();
                var cmd1 = new SQLiteCommand(stm1, con1);
                cmd1.ExecuteNonQuery();
                con1.Dispose();
                MessageBox.Show("Класс успешно удален!");
            }
            else
            {
                MessageBox.Show("Не выбран класс!");
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
