using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    public class AreaClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public int id { get; set; }
        public string name { get; set; }

        //Запрос для добавления записи в таблицу с районами
        public static void InsertArea(string NewArea)
        {
            try
            {
                string query = $"insert into area (name) value ('{NewArea}')";

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

        //Запрос для пометки записи как удаленной
        public static void RemoveArea(int area_id)
        {
            try
            {
                string query = $"update area set deleted = 1 where id = {area_id}";

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

        //Запрос для получения списка районов
        public static AreaClass[] LoadListArea()
        {
            try
            {
                string query = "select id, name from area where deleted = 0";
                List<AreaClass> AreaList = new List<AreaClass>();

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
                                    AreaList.Add(new AreaClass { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString() });
                                }
                            }
                        }

                    }
                }

                if (AreaList.Count > 0)
                {
                    AreaClass[] areas = new AreaClass[AreaList.Count];

                    for (int i = 0; i < AreaList.Count; i++)
                    {
                        areas[i] = AreaList[i];
                    }

                    return areas;
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
    }
}
