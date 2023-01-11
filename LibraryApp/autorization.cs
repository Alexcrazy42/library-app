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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibraryApp
{
    public partial class autorization : Form
    {
        public autorization()
        {
            InitializeComponent();
        }

        private void AutorizationButton_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            
              
            DB db = new DB();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM `librarian` WHERE `Логин` = @ul AND `Пароль` = @up", db.GetConnection());
            cmd.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
            cmd.Parameters.Add("@up", MySqlDbType.VarChar).Value = password;
            db.CloseConnection();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                db.OpenConnection();
                MySqlCommand schoolByLogPassword = new MySqlCommand($"SELECT * FROM `librarian` WHERE `Логин` = @ul AND `Пароль` = @up", db.GetConnection());
                schoolByLogPassword.Parameters.Add("@ul", MySqlDbType.VarChar).Value = login;
                schoolByLogPassword.Parameters.Add("@up", MySqlDbType.VarChar).Value = password;
                string userCount = (string)schoolByLogPassword.ExecuteScalar();

                school = Convert.ToString(userCount); 
                this.Hide();
                Form1 fm1 = new Form1();
                fm1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
            
        }
    }
}
