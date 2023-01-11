using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace LibraryApp
{
    public partial class educational3 : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\EdBooks.db";

        public educational3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox11.Text != "" & textBox12.Text != "")
            {
                string bookName = textBox1.Text;
                string author = textBox2.Text;
                string subject = textBox3.Text;
                string clas = textBox5.Text;
                string year = textBox12.Text;
                string count = textBox11.Text;
                string message = $"Вы действительно хотите изменить запись?\nНазвание книги: {bookName}\nАвтор: {author}\nПредмет: {subject}\nИздательство: {1}\nКласс: {clas}\nФПУ: {1}\nУровень образования: {1}\nСрок использования:{1}\nГод: {year}\nСерия: {1}";
                string caption = "Подтверждение удаления учебной книги";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    SQLiteConnection con = new SQLiteConnection(dbName);
                    con.Open();
                    string query = $"UPDATE EdBooks SET amount='{count}' WHERE bookName='{bookName}' AND author='{author}' AND subject='{subject}' AND clas='{clas}' AND year='{year}';";
                    
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            else
            {
                MessageBox.Show("Заполните все данные!");
            }
        }

        private string GetValueDB()
        {
            return "";
        }
       
    }
}
