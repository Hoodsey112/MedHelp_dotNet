using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    public class MKBClass
    {
        public string DiagID { get; set; }
        public string DiagName { get; set; }

        public static MKBClass[] LoadMKB()
        {
            List<MKBClass> mkb = new List<MKBClass>();
            string query = $"select DiagID, DiagName from mkb";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                mkb.Add(new MKBClass { DiagID = reader["DiagID"].ToString(), DiagName = reader["DiagName"].ToString()});
                            }
                        }
                    }

                }
            }

            if (mkb.Count > 0)
            {
                MKBClass[] _mkb = new MKBClass[mkb.Count];

                for (int i = 0; i < mkb.Count; i++)
                {
                    _mkb[i] = mkb[i];
                }

                return _mkb;
            }
            else return null;
        }
    }
}
