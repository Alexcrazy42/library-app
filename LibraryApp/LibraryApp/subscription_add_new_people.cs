using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace LibraryApp
{
    public partial class subscription_add_new_people : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";
        public string dateNow;
        public subscription_add_new_people()
        {
            InitializeComponent();
            dateNow = DateTime.Now.ToString().Split(' ')[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string[] FIO = textBox1.Text.Split(' ');
                if (FIO.Length < 3)
                {
                    MessageBox.Show("Проверьте правильность заполнения ФИО!");
                }
                else
                {
                    string surname = FIO[0];
                    string name = FIO[1];
                    string patronymic = FIO[2];
                    string day = dateTimePicker1.Text.Split(' ')[0];
                    if (day.Length != 2)
                    {
                        day = "0" + day;
                    }
                    string month = GetNumOfMonths(dateTimePicker1.Text.Split(' ')[1]);
                    string year = dateTimePicker1.Text.Split(' ')[2];
                    string dateOfBirth = day + "." + month + "." + year;

                    //DateTime dateTime = new DateTime();
                    if (dateNow != dateOfBirth)
                    {
                        string stm = $"INSERT INTO students (Фамилия, Имя, Отчество, Дата_Рождения, Класс) VALUES('{surname}', '{name}', '{patronymic}', '{dateOfBirth}', '{subscription.selectedClass}')";
                        var con = new SQLiteConnection(dbName);
                        con.Open();
                        var cmd = new SQLiteCommand(stm, con);
                        cmd.ExecuteNonQuery();
                        con.Dispose();
                        MessageBox.Show("Ученик успешно внесен");
                    }
                    else
                    {
                        MessageBox.Show("Вы не выбрали дату рождения ученика!");
                    }



                    
                    
                    

                }
                
                
                
            }
            else
            {
                MessageBox.Show("Не введены фамилия, имя, отчество!");
            }
        }

        public string GetNumOfMonths(string month)
        {
            var months = new Dictionary<string, string>()
            {
                { "января", "01"},
                { "февраля", "01"},
                { "марта", "03"},
                { "апреля", "04"},
                { "мая", "05"},
                { "июня", "06"},
                { "июля", "07"},
                { "августа", "08"},
                { "сентября", "09"},
                { "октября", "10"},
                { "ноября", "11"},
                { "декабря", "12"}
            };
            return months[month];
        }
    }
}
