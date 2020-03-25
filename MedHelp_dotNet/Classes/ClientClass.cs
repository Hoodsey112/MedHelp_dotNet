using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    public class ClientClass
    {
        public int id { get; set; }
        public string FIO { get; set; }
        public DateTime birthDate { get; set; }
        public string Address { get; set; }

        public static DataTable LoadClientData()
        {
            DataTable ClientTable = new DataTable();
            string query = $"select FIO, birthDate, Address from client where deleted = 0";

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
    }
}
