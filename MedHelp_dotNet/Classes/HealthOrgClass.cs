using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    public class HealthOrgClass
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public int area_id { get; set; }

        //Запрос на получение списка оздоровительных организаций
        public static HealthOrgClass[] LoadHealthOrgList(int area_id)
        {
            List<HealthOrgClass> healthClass = new List<HealthOrgClass>();
            string query = $"select id, FullName, ShortName, Address, area_id from childrenshealthorganization where deleted = 0 and area_id = {area_id}";

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
                                healthClass.Add(new HealthOrgClass { id = int.Parse(reader["id"].ToString()), FullName = reader["FullName"].ToString(), ShortName = reader["ShortName"].ToString(), Address = reader["Address"].ToString(), area_id = int.Parse(reader["area_id"].ToString()) });
                            }
                        }
                    }

                }
            }

            if (healthClass.Count > 0)
            {
                HealthOrgClass[] ho = new HealthOrgClass[healthClass.Count];

                for (int i = 0; i < healthClass.Count; i++)
                {
                    ho[i] = healthClass[i];
                }

                return ho;
            }
            else return null;
        }

        public static HealthOrgClass LoadHealthOrgList(int id, int area_id)
        {
            HealthOrgClass healthClass = new HealthOrgClass();
            string query = $"select id, FullName, ShortName, Address, area_id from childrenshealthorganization where deleted = 0 and area_id = {area_id} and id = {id}";

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
                                healthClass.id = int.Parse(reader["id"].ToString());
                                healthClass.FullName = reader["FullName"].ToString();
                                healthClass.ShortName = reader["ShortName"].ToString();
                                healthClass.Address = reader["Address"].ToString();
                                healthClass.area_id = int.Parse(reader["area_id"].ToString());
                            }
                        }
                    }

                }
            }

            return healthClass;
        }

            //Запрос на добавление новой ДОО в таблицу
            public static void InsertHealthOrg(string FullName, string ShortName, string Address, int area_id)
        {
            string query = $"insert into childrenshealthorganization (FullName, ShortName, Address, area_id) value ('{FullName}', '{ShortName}', '{Address}', {area_id})";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        //Запрос на установку пометки удалено для записи в таблице
        public static void RemoveHealthOrg(int id, int area_id)
        {
            string query = $"update childrenshealthorganization set deleted = 1 where area_id = {area_id} and id = {id}";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        //Запрос для редактирования записи в таблице ДОО
        public static void EditHealthOrg(int id, int area_id, string FullName, string ShortName, string Address, int area_id_new)
        {
            string query = "";

            if (area_id == area_id_new) query = $"update childrenshealthorganization set FullName = '{FullName}', ShortName = '{ShortName}', Address = '{Address}'  where area_id = {area_id} and id = {id}";
            else query = $"update childrenshealthorganization set FullName = '{FullName}', ShortName = '{ShortName}', Address = '{Address}', area_id = {area_id_new}  where area_id = {area_id} and id = {id}";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
