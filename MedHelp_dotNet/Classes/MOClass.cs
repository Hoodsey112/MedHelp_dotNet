using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    class MOClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public int id { get; set; }
        public string name { get; set; }
        public int area_id { get; set; }

        //Запрос для получения доступных МО согласно выбранному району
        public static MOClass[] LoadMOList(int area_id)
        {
            try
            {
                List<MOClass> mOs = new List<MOClass>();

                string query = $"select id, name, area_id from medorganisation where deleted = 0 and area_id = {area_id}";

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
                                    mOs.Add(new MOClass { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), area_id = int.Parse(reader["area_id"].ToString()) });
                                }
                            }
                        }

                    }
                }

                if (mOs.Count > 0)
                {
                    MOClass[] ms = new MOClass[mOs.Count];

                    for (int i = 0; i < mOs.Count; i++)
                    {
                        ms[i] = mOs[i];
                    }

                    return ms;
                }
                else return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                return null;
            }
        }

        public static MOClass[] LoadMOList()
        {
            try
            {
                List<MOClass> mOs = new List<MOClass>();

                string query = $"select id, name, area_id from medorganisation where deleted = 0";

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
                                    mOs.Add(new MOClass { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), area_id = int.Parse(reader["area_id"].ToString()) });
                                }
                            }
                        }

                    }
                }

                if (mOs.Count > 0)
                {
                    MOClass[] ms = new MOClass[mOs.Count];

                    for (int i = 0; i < mOs.Count; i++)
                    {
                        ms[i] = mOs[i];
                    }

                    return ms;
                }
                else return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                return null;
            }
        }

        //Запрос для добавления записи в таблицу с МО
        public static void InsertNewMO(string NewMO, int area_id)
        {
            try
            {
                string query = $"insert into medorganisation (name, area_id) value ('{NewMO}', {area_id})";

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
                logger.Error(ex, $"#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
            }
        }

        //Запрос для пометки записи как удаленной
        public static void RemoveMO(int MO_id, int area_id)
        {
            try
            {
                string query = $"UPDATE medorganisation SET deleted = 1 where id = {MO_id} and area_id = {area_id}";

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
                logger.Error(ex, $"#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
            }
        }
    }
}
