
using MySql.Data.MySqlClient;
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
    public partial class lendingBooks2 : Form
    {

        public lendingBooks2()
        {
            InitializeComponent();
            
        }

        private void lendingBooks2_Load(object sender, EventArgs e)
        {
            List<string> classes = new List<string>();
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


            dataGridView1.ReadOnly = true;

            ToolStripMenuItem showBooks = new ToolStripMenuItem("Посмотреть взятые учеником книги");
            ToolStripMenuItem lendBooks = new ToolStripMenuItem("Выдать ученику художественную литературу");
            ToolStripMenuItem lendEdBooks = new ToolStripMenuItem("Выдать ученику учебную литературу");
            contextMenuStrip1.Items.AddRange(new[] { showBooks, lendBooks, lendEdBooks });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            lendBooks.Click += LendFictionBook;
            showBooks.Click += ShowBooks;
            lendEdBooks.Click += LendEdBook;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.OpenConnection();
            if (comboBox1.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT Фамилия, Имя, Отчество FROM `users` WHERE `Школа` = '{school}' AND `Класс` = '{comboBox1.Text}'", db.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                db.CloseConnection();
                
            }
            else
            {
                MessageBox.Show("Выберите класс!");
            }

            
        }


        private void LendEdBook(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);


            try
            {


                string surname = dataGridView1[0, info.RowIndex].Value.ToString();
                string name = dataGridView1[1, info.RowIndex].Value.ToString();
                string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();


                if (surname != "" && name != "" && patronymic != "" && comboBox1.Text != "")
                {

                    student["Фамилия"] = surname;
                    student["Имя"] = name;
                    student["Отчество"] = patronymic;
                    student["Класс"] = comboBox1.Text;
                    studentId = GetScalarValueFromDB($"SELECT id FROM `users` WHERE `Фамилия` = '{surname}' AND `Имя` = '{name}' AND `Отчество` = '{patronymic}' AND `Класс` = '{comboBox1.Text}' AND `Школа` = '{school}'");
                    lendingEdBook leb = new lendingEdBook();
                    leb.ShowDialog();


                }
                else
                {
                    MessageBox.Show("Выбрана пустая строка либо не выбран класс!");
                }
            }
            catch
            {
                MessageBox.Show("Щелчок по пустому месту!");
            }
        }


        private void LendFictionBook(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);


            try
            {


                string surname = dataGridView1[0, info.RowIndex].Value.ToString();
                string name = dataGridView1[1, info.RowIndex].Value.ToString();
                string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();


                if (surname != "" && name != "" && patronymic != "" && comboBox1.Text != "")
                {

                    student["Фамилия"] = surname;
                    student["Имя"] = name;
                    student["Отчество"] = patronymic;
                    student["Класс"] = comboBox1.Text;
                    studentId = GetScalarValueFromDB($"SELECT id FROM `users` WHERE `Фамилия` = '{surname}' AND `Имя` = '{name}' AND `Отчество` = '{patronymic}' AND `Класс` = '{comboBox1.Text}' AND `Школа` = '{school}'");
                    lendingFictionBook lfb = new lendingFictionBook();
                    lfb.ShowDialog();


                }
                else
                {
                    MessageBox.Show("Выбрана пустая строка либо не выбран класс!");
                }
            }
            catch
            {
                MessageBox.Show("Щелчок по пустому месту!");
            }

            

            
        }

        private void ShowBooks(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);


            
                string surname = dataGridView1[0, info.RowIndex].Value.ToString();
                string name = dataGridView1[1, info.RowIndex].Value.ToString();
                string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();



                if (surname != "" && name != "" && patronymic != "" && comboBox1.Text != "")
                {
                    student["Фамилия"] = surname;
                    student["Имя"] = name;
                    student["Отчество"] = patronymic;
                    student["Класс"] = comboBox1.Text;
                    
                    studentId = GetScalarValueFromDB($"SELECT id FROM `users` WHERE `Фамилия` = '{surname}' AND `Имя` = '{name}' AND `Отчество` = '{patronymic}' AND `Класс` = '{comboBox1.Text}' AND `Школа` = '{school}'");
                    lendingBooksShowBooks lbsb = new lendingBooksShowBooks();
                    lbsb.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Выбрана пустая строка либо не выбран класс!");
                }
            try
            {
            }
            catch
            {
                MessageBox.Show("Щелчок по пустому месту!");
            }


            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            db.OpenConnection();
            if (comboBox1.Text != "")
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT Фамилия, Имя, Отчество FROM `users` WHERE `Школа` = '{school}' AND `Класс` = '{comboBox1.Text}'", db.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                db.CloseConnection();

            }
            else
            {
                MessageBox.Show("Выберите класс!");
            }
            comboBox1.SelectionLength = 0;

        }
    }
}
