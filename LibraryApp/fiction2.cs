using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;
using static LibraryApp.Program;




namespace LibraryApp
{
    public partial class fiction2 : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Books.db";
        AutoCompleteStringCollection source = new AutoCompleteStringCollection()
        {};
        string udk_link = "https://perviy-vestnik.ru/udc/?_openstat=ZGlyZWN0LnlhbmRleC5ydTs1NTg1NzU5NzsxMjYxNjM4MjkwOTt5YW5kZXgucnU6cHJlbWl1bQ&yclid=1244870406504447999";

        public fiction2()
        {
            InitializeComponent();
            string authors = GetAuthors();
            string[] authorsArr = authors.Split('s');
            Array.Sort(authorsArr);
            foreach (string author in authorsArr)
            {
                source.Add(author);
            }
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string author = textBox1.Text;
            string title = textBox2.Text; 
            string town = comboBox1.Text!="" ? comboBox1.Text.Split(':')[1] : "";
            string izd = textBox3.Text; 
            string year = textBox4.Text;
            string count = textBox5.Text;
            string bbk = label8.Text;
            string isbn = textBox6.Text;
            string money = radioButton2.Checked ? "д" : textBox7.Text;
            string invNumber = "";
            string udk_num = textBox10.Text;
            string udk_title = textBox11.Text;
            if (textBox8.Text != "")
            {
                if (radioButton2.Checked && textBox8.Text[textBox8.Text.Length - 1] != 'Д')
                {
                    MessageBox.Show("Инвентарный номер книги дарения должен содержать букву Д на конце!");
                }
                else
                {
                    invNumber = textBox8.Text;
                }
            }
            string note = radioButton2.Checked ? "д" : textBox9.Text;
            string day = dateTimePicker1.Text.Split(' ')[0].Length != 2 ? "0" + dateTimePicker1.Text.Split(' ')[0] : dateTimePicker1.Text.Split(' ')[0];
            string month = GetNumOfMonths(dateTimePicker1.Text.Split(' ')[1]);
            string note_year = dateTimePicker1.Text.Split(' ')[2];
            string noteDate = radioButton2.Checked ? "д" :  day + "." + month + "." + note_year;


            if (author != "" && title != "" && town != "" && izd != "" && year != "" && count != "" && bbk != "" &&  isbn != "" 
                && money != "" && invNumber != "" && note != "" && noteDate != "" && udk_num != "" && udk_title != "")
            {
                string authors = GetAuthors();
                if (!authors.Contains(author))
                {

                    string editAuthor = authors + author + "s";
                    DialogResult dialogResult = MessageBox.Show($"Внести этого автора? \n{author}", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        EditAuthors(editAuthor);
                       
                    }
                }
                MessageBox.Show("Все хорошо");
            }
            else
            {
                MessageBox.Show("Заполните все данные!");
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fiction_bbk f_bbk = (fiction_bbk)Application.OpenForms["fiction_bbk"];
            fiction_bbk bbk = new fiction_bbk();
            bbk.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(udk_link);
            //fiction_udk f_udk = (fiction_udk)Application.OpenForms["fiction_udk"];
            //fiction_udk udk = new fiction_udk();
            //udk.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label8.Text = Data.Value;
        }

        

        public string GetNumOfMonths(string month)
        {
            var months = new Dictionary<string, string>()
            {
                {"января", "01"},
                {"февраля", "01"},
                {"марта", "03"},
                {"апреля", "04"},
                {"мая", "05"},
                {"июня", "06"},
                {"июля", "07"},
                {"августа", "08"},
                {"сентября", "09"},
                {"октября", "10"},
                {"ноября", "11"},
                {"декабря", "12"}
            };
            return months[month];
        }

        public string GetAuthors()
        {
            string stm = $"SELECT Значение FROM constParam WHERE Ключ = 'Авторы'";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            string authors = cmd.ExecuteScalar().ToString();
            con.Close();
            return authors;
        }

        public void EditAuthors(string str)
        {
            string stm = $"UPDATE constParam SET Значение = '{str}' WHERE Ключ = 'Авторы'";

            var con = new SQLiteConnection(dbName);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            label14.Visible = true;
            label15.Visible = true;
            textBox9.Visible = true;
            dateTimePicker1.Visible = true;
            label12.Visible = true;
            textBox7.Visible = true;
            label20.Visible = true;
            comboBox4.Visible = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            label14.Visible = false;
            label15.Visible = false;
            textBox9.Visible = false;
            dateTimePicker1.Visible = false;
            label12.Visible = false;
            textBox7.Visible = false;
            label20.Visible = false;
            comboBox4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label9.Text = UDK.Value;
        }

        
    }
}
