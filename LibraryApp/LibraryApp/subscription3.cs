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
    public partial class subscription3 : Form
    {
        

        public subscription3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            subscription subscr = (subscription)Application.OpenForms["subscription"];
            subscription sub = new subscription();
            sub.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subscription_new_class subscr = (subscription_new_class)Application.OpenForms["subscription_new_class"];
            subscription_new_class sub = new subscription_new_class();
            sub.ShowDialog();




            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            subscription_delete_class subscr = (subscription_delete_class)Application.OpenForms["subscription_delete_class"];
            subscription_delete_class sub = new subscription_delete_class();
            sub.ShowDialog();
        }
    }
}
