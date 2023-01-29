using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using static LibraryApp.Program;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Core;
using System.Text;
using System.Threading;

namespace LibraryApp
{
    public partial class subscription_add_new_people : Form
    {
        public string dateNow;

        public subscription_add_new_people()
        {
            InitializeComponent();
            dateNow = DateTime.Now.ToString().Split(' ')[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int studId = ReturnStudentId($"SELECT `stud_id` FROM `id_params`");
            

            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "")
            {
                string surname = textBox1.Text;
                string name = textBox2.Text;
                string patronymic = textBox3.Text;
                string day = dateTimePicker1.Text.Split(' ')[0];
                if (day.Length != 2)
                {
                    day = "0" + day;
                }
                string month = GetNumOfMonths(dateTimePicker1.Text.Split(' ')[1]);
                string year = dateTimePicker1.Text.Split(' ')[2];
                string dateOfBirth = day + "." + month + "." + year;

                if (dateNow != dateOfBirth)
                {
                    string stm = $"INSERT INTO `users` (`Фамилия`, `Имя`, `Отчество`, `Дата_Рождения`, `Класс`, `Взятые_книги`, `Логин`, `Пароль`, `Школа`, `id`) VALUES('{surname}', '{name}', '{patronymic}', '{dateOfBirth}', '{subscription2.selectedClass}', '','{surname}', '{GenerateUniquePassword()}', '{school}', '{(studId+1).ToString()}')";
                    
                    DB db = new DB();
                    db.OpenConnection();
                    MySqlCommand cmd = new MySqlCommand(stm, db.GetConnection());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Ученик успешно внесен");
                    string request = $"UPDATE `id_params` SET `stud_id` = {studId + 1}";
                    MakeNonQuery(request);
                        
                }
                else
                {
                    MessageBox.Show("Вы не выбрали дату рождения ученика!");
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

        public int ReturnStudentId(string request)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(request, db.GetConnection());
            int s = (int)cmd.ExecuteScalar();
            db.CloseConnection();
            return s;
        }

        public bool IsUniquePassword(string password)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT `Пароль` FROM `users`", db.GetConnection());
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(password == reader["Пароль"].ToString())
                {
                    return false;
                }
            }
            return true;
        }

        public string GeneratePassword()
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            int count = 0;
            while (count < 8)
            {
                count++;
                res += valid[rnd.Next(valid.Length)];
            }
            return res;
        }

        public string GenerateUniquePassword()
        {
            string password;

            do
            {
                password = GeneratePassword();
            } while (!IsUniquePassword(password));
            

            return password;
        }





    }
}
