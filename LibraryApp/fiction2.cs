using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static LibraryApp.Program;




namespace LibraryApp
{
    public partial class fiction2 : Form
    {
        AutoCompleteStringCollection source = new AutoCompleteStringCollection()
        {};
        string udk_link = "https://perviy-vestnik.ru/udc/?_openstat=ZGlyZWN0LnlhbmRleC5ydTs1NTg1NzU5NzsxMjYxNjM4MjkwOTt5YW5kZXgucnU6cHJlbWl1bQ&yclid=1244870406504447999";

        public fiction2()
        {
            InitializeComponent();
            List<string> authors = GetAuthors();
            foreach (string author in authors)
            {
                source.Add(author);
            }
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bookSchool = school;
            string id = GetId();
            string author = textBox1.Text;
            string title = textBox2.Text;
            string podz = textBox12.Text != "" ? textBox12.Text : "none";
            string chapterNum = numericUpDown1.Text;
            string town = comboBox1.Text;
            string izd = textBox3.Text; 
            string year = textBox4.Text;
            string charachterInfo = comboBox3.Text;
            string svedIzd = textBox13.Text != "" ? textBox13.Text : "none";
            string pageCount = textBox5.Text;
            string language = comboBox2.Text;
            string genre = comboBox5.Text;
            string appointment = comboBox6.Text;
            bool isBrochure = checkBox1.Checked;
            string bbkNum = label8.Text.Split(' ')[0];
            string bbkTitle = bbkNum != "" ? label8.Text.Replace(bbkNum, "") : "";
            string udkNum = textBox10.Text;
            string udkTitle = textBox11.Text;
            string isbn = textBox6.Text;
            
            string invNumber = "";
            
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
            string money = radioButton2.Checked ? "0" : textBox7.Text;
            string noteNum = radioButton2.Checked ? "д" : textBox9.Text;
            string day = dateTimePicker1.Text.Split(' ')[0].Length != 2 ? "0" + dateTimePicker1.Text.Split(' ')[0] : dateTimePicker1.Text.Split(' ')[0];
            string month = GetNumOfMonths(dateTimePicker1.Text.Split(' ')[1]);
            string note_year = dateTimePicker1.Text.Split(' ')[2];
            string supplyDate = day + "." + month + "." + note_year;
            string sourceFunding = radioButton2.Checked ? "д" : comboBox4.Text;




            if (id != "" && title != "" && invNumber != "" && bookSchool != "" && town != "" && podz != "" && chapterNum != "" && izd != "" && year != "" 
                && charachterInfo != "" && svedIzd != "" && pageCount != "" && language != "" && genre != "" && appointment != "" && bbkNum != "" && bbkTitle != "" 
                && udkNum != "" && udkTitle != "" && isbn != "" && money != "" && noteNum != "" && supplyDate != "" && sourceFunding != "")
            {
                
                if (ExistAuthor(author) == false)
                { 
                    DialogResult dialogResult = MessageBox.Show($"Внести этого автора? \n{author}", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        AddAuthor(author);
                       
                    }
                }
                AddBook(id, author, title, invNumber, bookSchool, town, podz, chapterNum, izd, year, charachterInfo, svedIzd, pageCount, language, genre, appointment, bbkNum, bbkTitle, udkNum, udkTitle, isbn, money, noteNum, supplyDate, sourceFunding);
                UpdateId(id);
                MessageBox.Show("Книга успешной внесена!");
            }
            else
            {
                MessageBox.Show("Заполните все данные!");
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            fiction_bbk bbk = new fiction_bbk();
            bbk.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(udk_link);
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

        public bool ExistAuthor(string author)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT `Автор` FROM `authors`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (author == reader["Автор"].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> GetAuthors()
        {
            List<string> authors = new List<string>();
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT `Автор` FROM `authors`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                authors.Add(reader["Автор"].ToString());
                
            }
            Array.Sort(authors.ToArray());
            return authors;
        }

        public void AddAuthor(string author)
        {
            MakeNonQuery($"INSERT INTO authors(`Автор`) VALUES('{author}')");
        }

        public void AddBook(string id, string author, string title, string invNumber, string bookSchool, string town, string podz, string chapterNum, string izd, string year, string charachterInfo, string svedIzd, string pageCount, string language, string genre, string appointment, string bbkNum, string bbkTitle, string udkNum, string udkTitle, string isbn, string money, string noteNum, string supplyDate, string sourceFunding)
        {
            MakeNonQuery($"INSERT INTO fictionbooks(`id`, `Автор`, `Название`, `Инвентарный_номер`, `Школа`, `Город`, `Подзаголовок`, `Номер_части`, `Издательство`, `Год_издания`, `Вид_информации`, `Сведения_издание`, `Количество_страниц`, `Язык`, `Жанр`, `Назначение`, `ББК_номер`, `ББК_название`, `УДК_номер`, `УДК_название`, `ISBN`, `Цена`, `Номер_накладной`, `Дата_поставки`, `Источник_финансирования`) VALUES('{id}', '{author}', '{title}', '{invNumber}', '{bookSchool}', '{town}', '{podz}', '{chapterNum}', '{izd}', '{year}', '{charachterInfo}', '{svedIzd}', '{pageCount}', '{language}', '{genre}', '{appointment}', '{bbkNum}', '{bbkTitle}', '{udkNum}', '{udkTitle}', '{isbn}', '{money}', '{noteNum}', '{supplyDate}', '{sourceFunding}')");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // номер накладной
            label14.Visible = true;
            textBox9.Visible = true;
            // цена
            label12.Visible = true;
            textBox7.Visible = true;
            // источник финансирования
            label20.Visible = true;
            comboBox4.Visible = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // номер накладной
            label14.Visible = false;
            textBox9.Visible = false;
            // цена
            label12.Visible = false;
            textBox7.Visible = false;
            // источник финансирования
            label20.Visible = false;
            comboBox4.Visible = false;
        }

        public string GetId()
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand schoolByLogPassword = new MySqlCommand($"SELECT `fiction_book_id` FROM `id_params`", db.GetConnection());
            string prevId = (string)schoolByLogPassword.ExecuteScalar().ToString();

            return prevId;
        }

        public void UpdateId(string prevId)
        {
            string newId = (Int64.Parse(prevId) + 1).ToString();
            MakeNonQuery($"UPDATE `id_params` SET `fiction_book_id` = '{newId}'");
        }

        
    }
}
