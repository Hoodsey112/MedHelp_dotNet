using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    class ClassCase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public int id { get; set; }
        public string name { get; set; }

        //Запрос для получения списка классификаций
        public static ClassCase[] LoadListCase()
        {
            try
            {
                string query = "select id, name from classCase where deleted = 0";
                List<ClassCase> ClassCaseList = new List<ClassCase>();

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
                                    ClassCaseList.Add(new ClassCase { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString() });
                                }
                            }
                        }

                    }
                }

                if (ClassCaseList.Count > 0)
                {
                    ClassCase[] cases = new ClassCase[ClassCaseList.Count];

                    for (int i = 0; i < ClassCaseList.Count; i++)
                    {
                        cases[i] = ClassCaseList[i];
                    }

                    return cases;
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
