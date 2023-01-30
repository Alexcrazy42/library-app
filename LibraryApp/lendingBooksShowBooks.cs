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
    public partial class lendingBooksShowBooks : Form
    {
        

        public struct Book
        {
            public string bookName;
            public Book(string _bookName)
            {
                bookName = _bookName;
            }
        }


        


        public lendingBooksShowBooks()
        {
            InitializeComponent();
            label1.Text = student["Фамилия"] + " " + student["Имя"] + " " + student["Отчество"] + " " + student["Класс"];
            FillFictionBooks();
            FillEdBooks();


        }

        public string ReturnScalarValue(string request)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(request, db.GetConnection());
            string s = (string)cmd.ExecuteScalar();
            db.CloseConnection();
            return s;
        }

        public void FillFictionBooks()
        {
            DB db = new DB();
            db.OpenConnection();
            dataGridView1.ReadOnly = true;
            List<Book> books = new List<Book>();
            ToolStripMenuItem returnBook1 = new ToolStripMenuItem("Вернуть книгу художественной литературы");
            contextMenuStrip1.Items.AddRange(new[] { returnBook1 });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            returnBook1.Click += ReturnFictionBook;


            DataTable dt = new DataTable();

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users_boks` WHERE `student_id` = '{studentId}' AND RIGHT(book_id, 1) = 'Х'", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DB db1 = new DB();
                db1.OpenConnection();
                string query = $"SELECT `Название`, `Автор`, `Инвентарный_номер` FROM `fictionbooks` WHERE `id` = '{reader["book_id"]}'";
                MySqlCommand cmdInner = new MySqlCommand(query, db1.GetConnection());
                DataTable dt1 = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdInner);
                adapter.Fill(dt1);

                adapter.SelectCommand = cmdInner;
                dt.Merge(dt1);
                db1.CloseConnection();
            }

            dataGridView1.DataSource = dt;
        }


        public void FillEdBooks()
        {
            DB db = new DB();
            db.OpenConnection();
            dataGridView2.ReadOnly = true;
            List<Book> books = new List<Book>();
            ToolStripMenuItem returnBook2 = new ToolStripMenuItem("Вернуть книгу учебной литературы");
            contextMenuStrip2.Items.AddRange(new[] { returnBook2 });
            dataGridView2.ContextMenuStrip = contextMenuStrip2;
            returnBook2.Click += ReturnEdBook;


            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users_boks` WHERE `student_id` = '{studentId}' AND RIGHT(book_id, 1) = 'У'", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();

            

            while (reader.Read())
            {
                DB db1 = new DB();
                db1.OpenConnection();
                string query = $"SELECT `Название`, `Автор`, `Другие_авторы`, `Год`, `Уровень`, `Назначение`, `Предмет`, `Стартовый_класс`, `Конечный_класс` FROM `edbooks` WHERE `id` = '{reader["book_id"]}'";
                

                MySqlCommand cmdInner = new MySqlCommand(query, db1.GetConnection());
                DataTable dt1 = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdInner);
                adapter.Fill(dt1);

                adapter.SelectCommand = cmdInner;
                dt.Merge(dt1);
                db1.CloseConnection();
            }


            

            

            dataGridView2.DataSource = dt;
        }



        public void ReturnFictionBook(object sender, EventArgs e)
        {
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);
            try
            {
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    DialogResult dialogResult = MessageBox.Show($"Ученик действительно вернул книгу?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string author = dataGridView1[1, info.RowIndex].Value.ToString();
                        string bookName = dataGridView1[0, info.RowIndex].Value.ToString();
                        string invNumber = dataGridView1[2, info.RowIndex].Value.ToString();
                        string bookId = ReturnScalarValue($"SELECT `id` FROM `fictionbooks` WHERE `Инвентарный_номер` = '{invNumber}'");
                        string studId = studentId;

                        MakeNonQuery($"DELETE FROM `users_boks` WHERE `book_id` = '{bookId}' AND `student_id` = '{studId}'");
                        MessageBox.Show("Книга успешно возвращена!");
                        dataGridView1.Rows.RemoveAt(info.RowIndex);
                        dataGridView1.Refresh();

                    }
                }
                else
                {
                    MessageBox.Show("Выбрана не строка!");
                }
            }
            catch
            {
                MessageBox.Show("Щелчок по пустому месту!");
            }

        }


        public void ReturnEdBook(object sender, EventArgs e)
        {
            var point = dataGridView2.PointToClient(contextMenuStrip2.Bounds.Location);
            var info = dataGridView2.HitTest(point.X, point.Y);
            try
            {
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    DialogResult dialogResult = MessageBox.Show($"Ученик действительно вернул книгу?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        
                        string bookName = dataGridView2[0, info.RowIndex].Value.ToString();
                        string author = dataGridView2[1, info.RowIndex].Value.ToString();
                        string otherAuthors = dataGridView2[2, info.RowIndex].Value.ToString();
                        string year = dataGridView2[3, info.RowIndex].Value.ToString();
                        string level = dataGridView2[4, info.RowIndex].Value.ToString();
                        string appoinment = dataGridView2[5, info.RowIndex].Value.ToString();
                        string obj = dataGridView2[6, info.RowIndex].Value.ToString();
                        string firstClass = dataGridView2[7, info.RowIndex].Value.ToString();
                        string endClass = dataGridView2[8, info.RowIndex].Value.ToString();
                        
                        string bookId = ReturnScalarValue($"SELECT `id` FROM `edbooks` WHERE `Автор` = '{author}' AND `Название` = '{bookName}' AND `Другие_авторы` = '{otherAuthors}' AND `Год` = '{year}' AND `Уровень` = '{level}' AND `Назначение` = '{appoinment}' AND `Предмет` = '{obj}' AND `Стартовый_класс` = '{firstClass}' AND `Конечный_класс` = '{endClass}'");
                        string studId = studentId;

                        MakeNonQuery($"DELETE FROM `users_boks` WHERE `book_id` = '{bookId}' AND `student_id` = '{studId}'");
                        MessageBox.Show("Книга успешно возвращена!");
                        dataGridView2.Rows.RemoveAt(info.RowIndex);
                        dataGridView2.Refresh();
                        

                    }
                }
                else
                {
                    MessageBox.Show("Выбрана не строка!");
                }
            }
            catch
            {
                MessageBox.Show("Щелчок по пустому месту!");
            }

        }


    }
}
