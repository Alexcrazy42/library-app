using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class subscription2 : Form
    {
        public subscription2()
        {
            InitializeComponent();
            
            dataGridView1.DataSource = subscription.dt;
            this.Text = subscription.selectedClass + " класс";
            dataGridView1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            subscription_add_new_people subscr = (subscription_add_new_people)Application.OpenForms["subscription_add_new_people"];
            subscription_add_new_people sub = new subscription_add_new_people();
            sub.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void subscription2_Load(object sender, EventArgs e)
        {

        }
    }
}
