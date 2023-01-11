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
using static LibraryApp.Program;
using MySql.Data.MySqlClient;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace LibraryApp
{
    public partial class subscription3 : Form
    {
        private static string dbName = @"Data Source = C:\Users\alexc\source\repos\LibraryApp\Students.db";

        public subscription3()
        {
            InitializeComponent();
        }

        // переход к классу
        private void button1_Click(object sender, EventArgs e)
        {
            subscription subscr = (subscription)Application.OpenForms["subscription"];
            subscription sub = new subscription();
            sub.ShowDialog();
        }


        // создание нового класса
        private void button2_Click(object sender, EventArgs e)
        {
            subscription_new_class subscr = (subscription_new_class)Application.OpenForms["subscription_new_class"];
            subscription_new_class sub = new subscription_new_class();
            sub.ShowDialog();  
        }

        // удаление класса
        private void button3_Click(object sender, EventArgs e)
        {
            subscription_delete_class subscr = (subscription_delete_class)Application.OpenForms["subscription_delete_class"];
            subscription_delete_class sub = new subscription_delete_class();
            sub.ShowDialog();
        }

        // перенос на следующий год
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Вы действительно хотите перенести учеников на следующий год?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                MakePlusOneYearForUsers();
                DeleteRetiredClasses();



                MessageBox.Show("Ученики успешно переведены на следующий год!");
            } 
        }

        // показать должников
        private void button5_Click(object sender, EventArgs e)
        {
            subscription_show_debtor sub = new subscription_show_debtor();
            sub.ShowDialog();
        }

        public void MakePlusOneYearForUsers()
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("UPDATE `users` SET `Класс`= CONCAT((Класс + 1), substr(Класс, -1))", db.GetConnection());
            cmd.ExecuteNonQuery();
            db.CloseConnection();
        }

        public void DeleteRetiredClasses()
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand("select substr(`Класс`, 1, LENGTH(`Класс`)-2) FROM `users`", db.GetConnection());
            cmd.ExecuteNonQuery();
            db.CloseConnection();
        }

        
    }
}
