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
using MySql.Data.MySqlClient;
using static LibraryApp.Program;


namespace LibraryApp
{
    public partial class subscription_delete_class : Form
    {
        

        public subscription_delete_class()
        {
            InitializeComponent();
            UpdateClassesInComboBox();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.Text != "" & comboBox1.Text != "Нет классов")
            {
                DialogResult dialogResult = MessageBox.Show($"Вы точно хотите удалить {comboBox1.Text} класс?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if(CheckEmptyClass(comboBox1.Text))
                    {
                        MakeNonQuery($"DELETE FROM `{school}_классы` WHERE `Класс`='{comboBox1.Text}'");
                        UpdateClassesInComboBox();
                        comboBox1.Refresh();
                        InitDebrors();
                        MessageBox.Show("Класс успешно удален!");
                    }
                    else
                    {
                        MessageBox.Show("Нельзя удалить класс, в котором находятся дети!");
                    }
                    

                }   
            }
            else
            {
                MessageBox.Show("Не выбран класс!");
            }
            
        }

        private void UpdateClassesInComboBox()
        {
            var classes = new List<string>();
            comboBox1.Items.Clear();
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{school}_классы`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                classes.Add(reader["Класс"].ToString());
            }

            classes = classes.OrderBy(x => int.Parse(x.Substring(0, x.Length - 1))).ToList();
            foreach (string i in classes)
            {
                comboBox1.Items.Add(i);
            }

            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Text = "Нет классов";
            }

            comboBox1.Text = "";
            db.CloseConnection();
        }

        private void InitDebrors()
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Взятые_книги"].ToString() != "")
                {
                    MakeNonQuery($"UPDATE `users` SET `Класс`='Д' WHERE `id` = '{reader["id"].ToString()}'");
                }
            }
            db.CloseConnection();
        }

        private bool CheckEmptyClass(string clas)
        {
            DB db = new DB();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users` WHERE `Класс` = '{clas}'", db.GetConnection());
            db.CloseConnection();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
