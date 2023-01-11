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
    public partial class fiction3 : Form
    {


        public fiction3()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string invNumber = textBox5.Text;
            string stm = $"SELECT `Название` FROM `fictionbooks` WHERE `Инвентарный_номер` = '{invNumber}'";
            
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(stm, db.GetConnection());

            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            adapter.SelectCommand = cmd;
            dataGridView1.DataSource = dt;
            db.CloseConnection();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 2)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы точно хотите удалить эту книгу?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MakeNonQuery($"DELETE FROM `fictionbooks` WHERE `Инвентарный_номер` = {textBox5.Text}");
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Книга успешно удалена!");
                }

            }
            else if (dataGridView1.RowCount == 1)
            {
                MessageBox.Show("Ни одной книги с таким инвентарным номером не найдено!");
            }

        }
    }
}
