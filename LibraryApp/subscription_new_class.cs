using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using static LibraryApp.Program;

namespace LibraryApp
{
    public partial class subscription_new_class : Form
    {
        

        public subscription_new_class()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT EXISTS(SELECT `Класс` FROM `443_классы` WHERE `Класс` = '{textBox1.Text}')", db.GetConnection());
            var s = ((Int64)cmd.ExecuteScalar()).ToString();
            

            
            
            
            if (s == "1")
            {
                MessageBox.Show("Такой класс уже существует!");
            }
            else
            {
                
                if (textBox1.Text != "")
                {
                    MakeNonQuery($"INSERT INTO `{school}_классы` (`Класс`) VALUES ('{textBox1.Text}')");
                    
                    MessageBox.Show("Класс создан!");
                }
                else
                {
                    MessageBox.Show("Отсутствует название класса!");
                }
                
            }
            
            
        }

        private void subscription_new_class_Load(object sender, EventArgs e)
        {

        }
    }
}
