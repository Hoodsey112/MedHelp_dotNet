using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    public class MKBClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string DiagID { get; set; }
        public string DiagName { get; set; }

        public static MKBClass[] LoadMKB()
        {
            try
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
                                    mkb.Add(new MKBClass { DiagID = reader["DiagID"].ToString(), DiagName = reader["DiagName"].ToString() });
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static List<MKBClass> LoadMKB_L()
        {
            try
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
                                    mkb.Add(new MKBClass { DiagID = reader["DiagID"].ToString(), DiagName = reader["DiagName"].ToString() });
                                }
                            }
                        }

                    }
                }
            
                return mkb;
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
