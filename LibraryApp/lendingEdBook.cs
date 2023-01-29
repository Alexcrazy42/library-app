
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LibraryApp.Program;

namespace LibraryApp
{
    public partial class lendingEdBook : Form
    {

        string author = "";
        string title = "";
        string other = "";
        string startYear = "";
        string level = "";
        string appointment = "";
        string obj = "";
        string startClass = "";
        string endClass = "";

        public lendingEdBook()
        {
            InitializeComponent();
            label10.Text = student["Фамилия"] + " " + student["Имя"] + " " + student["Отчество"] + " " + student["Класс"];
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            author = textBoxAuthor.Text;
            title = textBoxTitle.Text;
            other = textBoxOther.Text == "" ? "none" : textBoxOther.Text;
            startYear = textBoxStartYear.Text;
            level = comboBoxLevel.Text;
            appointment = comboBoxAppointment.Text;
            obj = comboBoxObject.Text;
            startClass = textBoxStartClass.Text;
            endClass = textBoxEndClass.Text;
            FindBook(author, title, other, startYear, level, appointment, obj, startClass, endClass);
            numericUpDown1.Maximum = GetMaxCountOfBooks(author, title, other, startYear, level, appointment, obj, startClass, endClass);
        }


        public void FindBook(string author, string title, string other, string startYear, string level, string appointment, string obj, string startClass, string endClass)
        {
            DB db = new DB();
            db.OpenConnection();
            string query = $"SELECT `Автор`, `Название`, `Год`, `Издательство` FROM edbooks WHERE `Автор` = '{author}' AND `Название` = '{title}' AND `Другие_авторы` = '{other}' AND `Год` = '{startYear}' AND `Уровень` = '{level}' AND `Назначение` = '{appointment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{startClass}' AND `Конечный_класс` = '{endClass}'";

            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public int GetMaxCountOfBooks(string author, string title, string other, string startYear, string level, string appointment, string obj, string startClass, string endClass)
        {
            DB db = new DB();
            db.OpenConnection();
            string query = $"SELECT `Количество` FROM edbooks WHERE `Автор` = '{author}' AND `Название` = '{title}' AND `Другие_авторы` = '{other}' AND `Год` = '{startYear}' AND `Уровень` = '{level}' AND `Назначение` = '{appointment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{startClass}' AND `Конечный_класс` = '{endClass}'";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            int maxCount = Convert.ToInt32(cmd.ExecuteScalar());
            return maxCount;
        }

        public void buttonGive_Click(object sender, EventArgs e)
        {
            

            if (dataGridView1.RowCount == 2)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы точно хотите выдать ученику эту книгу?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string edBookId = GetScalarValueFromDB($"SELECT id FROM edbooks WHERE `Автор` = '{author}' AND `Название` = '{title}' AND `Другие_авторы` = '{other}' AND `Год` = '{startYear}' AND `Уровень` = '{level}' AND `Назначение` = '{appointment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{startClass}' AND `Конечный_класс` = '{endClass}'");
                    MakeNonQuery($"INSERT INTO users_boks(`student_id`, `book_id`) VALUES ('{Program.studentId}', '{edBookId}')");
                    MessageBox.Show("Книга успешно выдана!");
                    this.Close();
                }

            }
            else if (dataGridView1.RowCount == 1)
            {
                MessageBox.Show("Ни одной книги с таким инвентарным номером не найдено!");
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }

        }

    }
}
