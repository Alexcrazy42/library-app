
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
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `{school}_классы`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Класс"].ToString());
            }

            dataGridView1.ReadOnly = true;

            ToolStripMenuItem showBooks = new ToolStripMenuItem("Посмотреть взятые учеником книги");
            ToolStripMenuItem lendBooks = new ToolStripMenuItem("Выдать ученику книгу");
            contextMenuStrip1.Items.AddRange(new[] { showBooks, lendBooks });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            lendBooks.Click += LendBooks;
            showBooks.Click += ShowBooks;

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

        private void LendBooks(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);



            string surname = dataGridView1[0, info.RowIndex].Value.ToString();
            string name = dataGridView1[1, info.RowIndex].Value.ToString();
            string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();


            if (surname!= "" && name!="" && patronymic!="" && comboBox1.Text!="")
            {
                student["Фамилия"] = surname;
                student["Имя"] = name;
                student["Отчество"] = patronymic;
                student["Класс"] = comboBox1.Text;
                lendingBooks3 lb3 = new lendingBooks3();
                lb3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выбрана пустая строка либо не выбран класс!");
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

                DB db = new DB();
                db.OpenConnection();
                MySqlCommand cmd = new MySqlCommand($"SELECT id FROM `users` WHERE `Фамилия` = '{surname}' AND `Имя` = '{name}' AND `Отчество` = '{patronymic}' AND `Класс` = '{comboBox1.Text}' AND `Школа` = '{school}'", db.GetConnection());
                studentId = (string)cmd.ExecuteScalar();

                lendingBooksShowBooks lbsb = new lendingBooksShowBooks();
                lbsb.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выбрана пустая строка либо не выбран класс!");
            }


            
        }
    }
}
