using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    public class ClientClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public int id { get; set; }
        public string FIO { get; set; }
        public DateTime birthDate { get; set; }
        public string sex { get; set; }
        public string Address { get; set; }

        public static DataTable LoadClientData()
        {
            try
            {
                DataTable ClientTable = new DataTable();
                string query = $"select id, FIO, birthDate, if(sex = 1, 'Мужской', if(sex = 2, 'Женский', 'Не указан')) sex, Address from client where deleted = 0";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        ClientTable.Load(sqlCommand.ExecuteReader());
                    }
                }

                return ClientTable;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void InsertClientData(string FIO, DateTime birthDate, string Address, int sex)
        {
            try
            {
                string query = $"insert into client (FIO, birthDate, Address, sex) value ('{FIO}', '{birthDate.ToString("yyyy-MM-dd")}', '{Address}', {sex})";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void EditClientData(string FIO, DateTime birthDate, string Address, int sex, int id)
        {
            try
            {
                string query = $"UPDATE client SET FIO = '{FIO}', birthDate = '{birthDate.ToString("yyyy-MM-dd")}', Address = '{Address}', sex = {sex} where id = {id}";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
