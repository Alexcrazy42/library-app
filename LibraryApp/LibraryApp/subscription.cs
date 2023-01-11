using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace LibraryApp
{
    public partial class subscription : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";
        public static string classes;
        public static string selectedClass;
        public static DataTable dt;

        public subscription()
        {
            InitializeComponent();
            string stm = "SELECT Фамилия FROM students WHERE Класс=-1";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            classes = cmd.ExecuteScalar().ToString();
            //classes = classes.Remove(classes.Length - 1);
            MessageBox.Show(classes);
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
           
            if (comboBox1.Text != "" & comboBox1.Text != "Нет классов")
            {
                selectedClass = comboBox1.Text;
                SQLiteConnection con = new SQLiteConnection(dbName);
                string query = $"SELECT Фамилия, Имя, Отчество, Дата_Рождения FROM students WHERE Класс='{selectedClass}' ORDER BY Фамилия, Имя, Отчество;";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                dt = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);

                subscription2 sub2 = new subscription2();
                subscription2 subscr2 = (subscription2)Application.OpenForms["subscription2"];
                sub2.ShowDialog();
                
                
            }
            else
            {
                MessageBox.Show("Не выбрано ни одного класса!");
            }
            
        }

        
    }
}
