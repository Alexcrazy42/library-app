using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using static LibraryApp.Program;
using System.Collections.Generic;

namespace LibraryApp
{
    public partial class subscription : Form
    {

        
        public static string selectedClass;
        public static DataTable dt;

        public subscription()
        {
            var classes = new List<string>();
            InitializeComponent();
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{school}_классы`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //comboBox1.Items.Add();
                classes.Add(reader["Класс"].ToString());
            }

            classes = classes.OrderBy(x => int.Parse(x.Substring(0, x.Length - 1))).ToList();
            foreach(string i in classes)
            {
                comboBox1.Items.Add(i);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (comboBox1.Text != "" & comboBox1.Text != "Нет классов")
            {

                DB db = new DB();
                db.OpenConnection();
                selectedClass = comboBox1.Text;
                string query = $"SELECT Фамилия, Имя, Отчество, Дата_Рождения FROM `users` WHERE `Класс` = '{selectedClass}' ORDER BY Фамилия, Имя, Отчество;";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                dt = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                adapter.SelectCommand = cmd;

                //dt = new DataTable();
                //adapter.Fill(dt);
                db.CloseConnection();

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
