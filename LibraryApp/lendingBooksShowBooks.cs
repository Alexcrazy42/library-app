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

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users_boks` WHERE `student_id` = '{studentId}' AND RIGHT(book_id, 1) = 'Х'", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string bookName = ReturnScalarValue($"SELECT `Название` FROM `fictionbooks` WHERE `id` = '{reader["book_id"]}'");
                books.Add(new Book(bookName));
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Название", typeof(string));

            for (int i = 0; i < books.Count; i++)
            {
                dt.Rows.Add(books[i].bookName);

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
            contextMenuStrip1.Items.AddRange(new[] { returnBook2 });
            dataGridView2.ContextMenuStrip = contextMenuStrip1;
            returnBook2.Click += ReturnEdBook;

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `users_boks` WHERE `student_id` = '{studentId}' AND RIGHT(book_id, 1) = 'У'", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string bookName = ReturnScalarValue($"SELECT `Автор`, `Название`, `Другие_авторы`, `Год`, `Уровень`, `Назначение`, `Предмет`, `Стартовый_класс`, `Конечный_класс` FROM `edbooks` WHERE `id` = '{reader["book_id"]}'");
                books.Add(new Book(bookName));
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Автор", typeof(string));
            dt.Columns.Add("Название", typeof(string));
            dt.Columns.Add("Другие лица", typeof(string));
            dt.Columns.Add("Год издания", typeof(string));
            dt.Columns.Add("Уровень", typeof(string));
            dt.Columns.Add("Назначение", typeof(string));
            dt.Columns.Add("Предмет", typeof(string));
            dt.Columns.Add("Стартовый класс", typeof(string));
            dt.Columns.Add("Конечный класс", typeof(string));

            for (int i = 0; i < books.Count; i++)
            {
                dt.Rows.Add(books[i].bookName);

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
                        string bookName = dataGridView1[0, info.RowIndex].Value.ToString();
                        string bookId = ReturnScalarValue($"SELECT `id` FROM `fictionbooks` WHERE `Название` = '{bookName}'");
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
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);
            try
            {
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    DialogResult dialogResult = MessageBox.Show($"Ученик действительно вернул книгу?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string bookName = dataGridView1[0, info.RowIndex].Value.ToString();
                        string bookId = ReturnScalarValue($"SELECT `id` FROM `edbooks` WHERE `Название` = '{bookName}'");
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
