using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Windows.Forms;

namespace MedHelp_dotNet.Classes
{
    class ConnectionClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static MySqlConnection GetStringConnection()
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection($"server={Properties.Settings.Default.server};user id={Properties.Settings.Default.login}; password = \"{Properties.Settings.Default.password}\"; database={Properties.Settings.Default.dataBase}");
                return sqlConnection;
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static MySqlConnection GetStringConnectionForComboBox(string server, string login, string password)
        {
            try
            {
                MySqlConnection sqlConnection = new MySqlConnection($"server={server};user id={login}; password = \"{password}\"");
                return sqlConnection;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
