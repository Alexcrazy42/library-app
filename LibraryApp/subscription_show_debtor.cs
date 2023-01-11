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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static LibraryApp.Program;


namespace LibraryApp
{
    public partial class subscription_show_debtor : Form
    {
        public subscription_show_debtor()
        {
            InitializeComponent();
            DB db = new DB();
            db.OpenConnection();
            string query = $"SELECT Фамилия, Имя, Отчество, Дата_Рождения FROM `users` WHERE `Класс` = 'Д' ORDER BY Фамилия, Имя, Отчество;";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;


            dataGridView1.ReadOnly = true;

            ToolStripMenuItem showBooks = new ToolStripMenuItem("Посмотреть взятые учеником книги");
            contextMenuStrip1.Items.AddRange(new[] { showBooks});
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            showBooks.Click += ShowBooks;
        }


        public void ShowBooks(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);



            string surname = dataGridView1[0, info.RowIndex].Value.ToString();
            string name = dataGridView1[1, info.RowIndex].Value.ToString();
            string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();




            if (surname != "" && name != "" && patronymic != "")
            {
                student["Фамилия"] = surname;
                student["Имя"] = name;
                student["Отчество"] = patronymic;
                student["Класс"] = "Должник";

                DB db = new DB();
                db.OpenConnection();
                MySqlCommand cmd = new MySqlCommand($"SELECT id FROM `users` WHERE `Фамилия` = '{surname}' AND `Имя` = '{name}' AND `Отчество` = '{patronymic}' AND `Школа` = '{school}'", db.GetConnection());
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
