using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using static LibraryApp.Program;

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

            ToolStripMenuItem deleteStudent = new ToolStripMenuItem("Удалить ученика из класса");
            contextMenuStrip1.Items.AddRange(new[] { deleteStudent});
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            deleteStudent.Click += DeleteStudent;
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

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;
                
                DialogResult dialogResult = MessageBox.Show($"Вы действительно собираетесь обновить класс из Excel-таблицы?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string stm1 = $"DELETE FROM `users` WHERE Класс='{subscription.selectedClass}'";
                    MakeNonQuery(stm1);

                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;
                    string[] first_array = new string[4];

                    
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        Excel.Application ObjWorkExcel = new Excel.Application();
                        Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
                        var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
                        string[,] list = new string[lastCell.Row, lastCell.Column];
                        for (int i = 0; i < lastCell.Row; i++)
                        {

                            for (int j = 0; j < lastCell.Column; j++)
                            {
                                list[i, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text.ToString();
                            }

                        }
                        for (int i = 1; i < lastCell.Row; i++)
                        {
                            string[] array = new string[lastCell.Column];
                            for (int j = 0; j < lastCell.Column; j++)
                            {
                                array[j] = list[i, j];
                            }




                            string stm = $"INSERT INTO `users` (Фамилия, Имя, Отчество, Дата_Рождения, Класс) VALUES('{array[0]}', '{array[1]}', '{array[2]}', '{array[3]}', '{subscription.selectedClass}')";
                            MakeNonQuery(stm);


                        }
                        ObjWorkBook.Close(false, Type.Missing, Type.Missing);
                        ObjWorkExcel.Quit();
                        this.Close();
                        MessageBox.Show("Класс успешно обновлен!");
                    }


                    
                // отмена обновления класса через excel
                }
                else
                {
                    this.Close();
                }
                
                
            }

            


            
            


            
            




        }

        void DeleteStudent(object sender, EventArgs e)
        {
            
            var point = dataGridView1.PointToClient(contextMenuStrip1.Bounds.Location);
            var info = dataGridView1.HitTest(point.X, point.Y);
            
            
            
            string surname = dataGridView1[0, info.RowIndex].Value.ToString();
            string name = dataGridView1[1, info.RowIndex].Value.ToString();
            string patronymic = dataGridView1[2, info.RowIndex].Value.ToString();
            string birth = dataGridView1[3, info.RowIndex].Value.ToString();
            if (surname != "" & name != "")
            {
                DialogResult dialogResult = MessageBox.Show($"Вы действительно хотите удалить ученика?\nФамилия: {surname}\nИмя: {name}\nОтчество: {patronymic}\nДата рождения: {birth}", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                    string stm = $"DELETE FROM `users` WHERE Фамилия='{surname}' AND Имя='{name}' AND Отчество='{patronymic}' AND Дата_Рождения='{birth}' AND Класс='{subscription.selectedClass}'";


                    MakeNonQuery(stm);

                    dataGridView1.Rows.RemoveAt(info.RowIndex);
                    dataGridView1.Refresh();
                    MessageBox.Show("Ученик успешно удален!");
                }
                
            }
            else
            {
                MessageBox.Show("Вы выбрали пустую строку!");
            }

        }

    }
}
