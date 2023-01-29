using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static LibraryApp.Program;

namespace LibraryApp
{
    public partial class educational2 : Form
    {

        public educational2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string author = comboBoxAuthor.Text;
            string editor = textBoxEditor.Text;
            string title = textBoxTitle.Text;
            string number = numericUpDownNumber.Text;
            string otherAuthors = textBoxOthersAuthors.Text != "" ? textBoxOthersAuthors.Text : "none";
            string state = comboBoxState.Text;
            string money = textBoxMoney.Text;
            string place = comboBoxPlace.Text;
            string izd = comboBoxIzd.Text;
            string year = textBoxYear.Text;
            string language = comboBoxLanguage.Text;
            string sourceFinancing = comboBoxSourceFinancing.Text;
            string note = textBoxNote.Text != "" ? textBoxNote.Text : "none";
            string level = comboBoxLevel.Text;
            string appoinment = comboBoxAppointment.Text;
            string obj = comboBoxObject.Text;
            string startClass = textBoxStartClass.Text;
            string endClass = textBoxEndClass.Text;
            string count = textBoxCount.Text;
            string id = GetId();


            if (author != "" && editor != "" && title != "" && number != "" && otherAuthors != "" && state != "" && money != "" && place != "" && izd != ""
                && year != "" && language != "" && sourceFinancing != "" && note != "" && level != "" && appoinment != "" && obj != "" && startClass != "" 
                && endClass != "" && count != "") 
            {
                
                string request = $"INSERT INTO edbooks(`Автор`, `Редактор`, `Название`, `Номер_части`, `Другие_авторы`, `Состояние`, `Цена`, `Место_издания`, `Издательство`, `Год`, `Язык`, `Источник_финансирования`, `Примечание`, `Уровень`, `Назначение`, `Предмет`, `Стартовый_класс`, `Конечный_класс`, `Количество`, `id`) VALUES('{author}', '{editor}', '{title}', '{number}', '{otherAuthors}', '{state}', '{money}', '{place}', '{izd}', '{year}', '{language}', '{sourceFinancing}', '{note}', '{level}', '{appoinment}', '{obj}', '{startClass}', '{endClass}', '{count}', '{id}')";
                MakeNonQuery(request);
                UpdateId();
                MessageBox.Show("Книга успешно внесена!");
            }
            else
            {
                MessageBox.Show("Заполните все данные!");
            }
            

        }

        public string GetId()
        {
            string prevId = GetScalarValueFromDB($"SELECT `ed_book_id` FROM `id_params`") + "У";

            return prevId;
        }

        public void UpdateId()
        {
            MakeNonQuery($"UPDATE `id_params` SET `ed_book_id` = `ed_book_id` + 1");

        }

    }
}
