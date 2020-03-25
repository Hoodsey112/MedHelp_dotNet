using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    public class AreaClass
    {
        public int id { get; set; }
        public string name { get; set; }

        //Запрос для добавления записи в таблицу с районами
        public static void InsertArea(string NewArea)
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

        //Запрос для пометки записи как удаленной
        public static void RemoveArea(int area_id)
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

        //Запрос для получения списка районов
        public static AreaClass[] LoadListArea()
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
    }
}
