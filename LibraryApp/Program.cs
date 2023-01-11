using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryApp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new autorization());
        }

        public static class Data
        {
            public static string Value { get; set; }
        }

        public static class UDK
        {
            public static string Value { get; set; }
        }

        public static string school;
        public static Dictionary<string, string> student = new Dictionary<string, string>()
        {
            {"Фамилия", ""},
            {"Имя", ""},
            {"Отчество", ""},
        };
        public static string studentId;

        public static void MakeNonQuery(string request)
        {
            DB db = new DB();
            db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(request, db.GetConnection());
            cmd.ExecuteNonQuery();
            db.CloseConnection();
        }



    }
}
