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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LibraryApp
{
    public partial class lendingFictionBook : Form
    {
        public lendingFictionBook()
        {
            InitializeComponent();
        }

        private void lendingBooks3_Load(object sender, EventArgs e)
        {
            label1.Text = student["Фамилия"] + " " + student["Имя"] + " " + student["Отчество"] + " " + student["Класс"];
        }

        

        private void textBoxInvNum_TextChanged(object sender, EventArgs e)
        {
            string invNumber = textBoxInvNum.Text;
            string stm = $"SELECT `Название`, `Автор`, `Год_издания`, `Цена` FROM `fictionbooks` WHERE `Инвентарный_номер` = '{invNumber}'";

            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(stm, db.GetConnection());

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            adapter.SelectCommand = cmd;
            dataGridView1.DataSource = dt;
            db.CloseConnection();
        }

        private void buttonLendFictionBook_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 2)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы точно хотите выдать ученику эту книгу?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //MakeNonQuery($"DELETE FROM `fictionbooks` WHERE `Инвентарный_номер` = {textBox5.Text} AND `Школа` = {school}");
                    string FictionbookId = GetScalarValueFromDB($"SELECT `id` FROM fictionbooks WHERE `Инвентарный_номер` = {textBoxInvNum.Text}");
                    MakeNonQuery($"INSERT INTO users_boks(`student_id`, `book_id`) VALUES ('{Program.studentId}', '{FictionbookId}')");
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
