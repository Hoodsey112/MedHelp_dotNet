using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    class ConnectionClass
    {
        public static MySqlConnection GetStringConnection()
        {
            MySqlConnection sqlConnection = new MySqlConnection($"server={Properties.Settings.Default.server};user id={Properties.Settings.Default.login}; password = \"{Properties.Settings.Default.password}\"; database={Properties.Settings.Default.dataBase}");
            return sqlConnection;
        }

        public static MySqlConnection GetStringConnectionForComboBox(string server, string login, string password)
        {
            MySqlConnection sqlConnection = new MySqlConnection($"server={server};user id={login}; password = \"{password}\"");
            return sqlConnection;
        }
    }
}
