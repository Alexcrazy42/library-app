using System;
using System.Windows.Forms;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Data;
using static LibraryApp.Program;
using MySqlX.XDevAPI.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryApp
{
    public partial class educational3 : Form
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

        public educational3()
        {
            InitializeComponent();
            numericUpDown1.ReadOnly = true;
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

        public void DeleteBook(string author, string title, string other, string startYear, string level, string appointment, string obj, string startClass, string endClass)
        {
            string request = $"DELETE FROM edbooks WHERE `Автор` = '{author}' AND `Название` = '{title}' AND `Другие_авторы` = '{other}' AND `Год` = '{startYear}' AND `Уровень` = '{level}' AND `Назначение` = '{appointment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{startClass}' AND `Конечный_класс` = '{endClass}'";
            MakeNonQuery(request);
        }

        public void UpdateCountOfBooks(string author, string title, string other, string startYear, string level, string appointment, string obj, string startClass, string endClass)
        {
            int count = Decimal.ToInt32(numericUpDown1.Value);
            int maxCount = GetMaxCountOfBooks(author, title, other, startYear, level, appointment, obj, startClass, endClass);
            string request = $"UPDATE edbooks SET `Количество` = '{maxCount - count}' WHERE `Автор` = '{author}' AND `Название` = '{title}' AND `Другие_авторы` = '{other}' AND `Год` = '{startYear}' AND `Уровень` = '{level}' AND `Назначение` = '{appointment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{startClass}' AND `Конечный_класс` = '{endClass}'";
            MakeNonQuery(request);
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (dataGridView1.RowCount == 2)
            {
                int count = Decimal.ToInt32(numericUpDown1.Value);
                int maxCount = GetMaxCountOfBooks(author, title, other, startYear, level, appointment, obj, startClass, endClass);
                if (maxCount == count)
                {
                    DialogResult dialogResult = MessageBox.Show($"Вы точно хотите эту книгу?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteBook(author, title, other, startYear, level, appointment, obj, startClass, endClass);
                        MessageBox.Show("Книги успешно списана!");
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show($"Вы точно хотите списать {count} экземляров данной книги?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        UpdateCountOfBooks(author, title, other, startYear, level, appointment, obj, startClass, endClass);
                        MessageBox.Show($"Успешно списано {count} экземпляров книг!");
                    }
                }
            }
            else if (dataGridView1.RowCount == 1)
            {
                MessageBox.Show("Ни одной книги не найдено!");
            }

        }
    }
}
