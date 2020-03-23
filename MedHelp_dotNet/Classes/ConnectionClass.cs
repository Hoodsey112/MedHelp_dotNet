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
            MySqlConnection sqlConnection = new MySqlConnection($"server=localhost;user id=root; password = \"\"; database=medhelp_omo");
            return sqlConnection;
        }
    }
}
