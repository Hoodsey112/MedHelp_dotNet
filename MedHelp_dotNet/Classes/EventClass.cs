using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet.Classes
{
    public class EventClass
    {
        public int id { get; set; }
        public string areaName { get; set; }
        public string medOrgName { get; set; }
        public DateTime eventDate { get; set; }
        public string DOOName { get; set; }
        public string DOOAddress { get; set; }
        public string clientFIO { get; set; }
        public DateTime clientBirthDate { get; set; }
        public string clientAge { get; set; }
        public string clientAddress { get; set; }
        public string relaxName { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string HelpName { get; set; }
        public string DiagName { get; set; }
        public string DiagID { get; set; }
        public string Speciality { get; set; }
        public string Department { get; set; }
        public string TransfertedDepart { get; set; }
        public string TransfertedDate { get; set; }
        public string HealthStatus { get; set; }

        public static void InsertEvent(int area_id, int medOrg_id, DateTime eventDate, int doo_id, int client_id, int relax_id, DateTime TreatmentDate, string HelpName, string DiagName, string DiagID, string Speciality, string Department, string TransfertedDepart, DateTime TransfertedDate, string HealthStatus)
        {
            string query = "insert into event (area_id, medOrg_id, eventDate, doo_id, client_id, relax_id, TreatmentDate, HelpName, DiagName, DiagID, Speciality, Department, TransfertedDepart, TransfertedDate, HealthStatus)" +
                         $" value ({area_id}, {medOrg_id}, '{eventDate.ToString("yyyy-MM-dd")}', {doo_id}, {client_id}, {relax_id}, '{TreatmentDate.ToString("yyyy-MM-dd")}', '{HelpName}', '{DiagName}', '{DiagID}', '{Speciality}', '{Department}', '{TransfertedDepart}', '{(TransfertedDate.ToString("yyyy-MM-dd") == DateTime.MinValue.ToString("yyyy-MM-dd") ? "0000-00-00" : TransfertedDate.ToString("yyyy - MM - dd"))}', '{HealthStatus}')";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        /*public static EventClass[] LoadEvent()
        {
            List<EventClass> events = new List<EventClass>();
            string query = "select e.id," +
                                 " a.name as AreaName," +
                                 " mo.name as medOrgName," +
                                 " e.eventDate," +
                                 " doo.FullName as DOOName," +
                                 " doo.Address as DOOAddress," +
                                 " c.FIO as clientFIO," +
                                 " c.birthDate as clientBirthDate," +
                                 " CONCAT((YEAR(current_date())-YEAR(c.birthDate)) - if(DAYOFYEAR(current_date()) > DAYOFYEAR(c.birthDate),0,1),' лет '," +
                                 " (IF(MONTH(CURDATE()) - MONTH(c.birthDate) < 0, MONTH(CURDATE()) - MONTH(c.birthDate) + 12, MONTH(CURDATE()) - MONTH(c.birthDate))),' мес.') AS clientAge," +
                                 " r.Name as relaxName," +
                                 " e.TreatmentDate," +
                                 " e.HelpName," +
                                 " e.DiagName," +
                                 " e.DiagID," +
                                 " e.Speciality," +
                                 " e.Department," +
                                 " e.TransfertedDepartment," +
                                 " e.TransfertedDate," +
                                 " e.HealthStatus" +
                          " from event as e" +
                          " join area as a on e.area_id = a.id" +
                          " join medorganisation as mo on e.medOrg_id = mo.id" +
                          " join childrenshealthorganization as doo on e.doo_id = doo.id" +
                          " join client as c on e.client_id = c.id" +
                          " join relaxinfo as r on e.relax_id = r.id" +
                          " where e.deleted = 0" +
                          "   and doo.deleted = 0" +
                          "   and c.deleted = 0" +
                          "   and a.deleted = 0" +
                          "   and mo.deleted = 0";

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
                                events.Add(new EventClass { id = int.Parse(reader["id"].ToString()), areaName = reader["AreaName"].ToString(), medOrgName = reader["name"].ToString(), eventDate = DateTime.Parse(reader["eventDate"].ToString()), DOOName = reader["DOOName"].ToString(), DOOAddress = reader["DOOAddress"].ToString(), clientFIO = reader["clientFIO"].ToString(), clientBirthDate = DateTime.Parse(reader["clientBirthDate"].ToString()), clientAge = reader["clientAge"].ToString(), clientAddress = reader["clientAddress"].ToString(), 
                                                            relaxName = reader["relaxName"].ToString(), TreatmentDate = DateTime.Parse(reader["TreatmentDate"].ToString()), HelpName = reader["HelpName"].ToString(), DiagName = reader["DiagName"].ToString(), DiagID = reader["DiagID"].ToString(), Speciality = reader["Speciality"].ToString(), Department = reader["Department"].ToString(), TransfertedDepart = reader["TransfertedDepart"].ToString(), TransfertedDate = DateTime.Parse(reader["TransfertedDate"].ToString()), HealthStatus = reader["HealthStatus"].ToString()
                                });
                            }
                        }
                    }

                }
            }

            if (events.Count > 0)
            {
                EventClass[] _events = new EventClass[events.Count];

                for (int i = 0; i < events.Count; i++)
                {
                    _events[i] = events[i];
                }

                return _events;
            }
            else return null;
        }*/

        public static DataTable LoadEvent()
        {
            DataTable events = new DataTable();

            string query = "select e.id," +
                                 " a.name as AreaName," +
                                 " mo.name as medOrgName," +
                                 " e.eventDate," +
                                 " doo.FullName as DOOName," +
                                 " doo.Address as DOOAddress," +
                                 " c.FIO as clientFIO," +
                                 " c.birthDate as clientBirthDate," +
                                 " CONCAT((YEAR(current_date())-YEAR(c.birthDate)) - if(DAYOFYEAR(current_date()) > DAYOFYEAR(c.birthDate),0,1),' лет '," +
                                 " (IF(MONTH(CURDATE()) - MONTH(c.birthDate) < 0, MONTH(CURDATE()) - MONTH(c.birthDate) + 12, MONTH(CURDATE()) - MONTH(c.birthDate))),' мес.') AS clientAge," +
                                 " c.Address as clientAddress," +
                                 " r.Name as relaxName," +
                                 " e.TreatmentDate," +
                                 " e.HelpName," +
                                 " e.DiagName," +
                                 " e.DiagID," +
                                 " e.Speciality," +
                                 " e.Department," +
                                 " e.TransfertedDepart," +
                                 " if (e.TransfertedDate = '0000-00-00', \"\", e.TransfertedDate) as TransfertedDate," +
                                 " e.HealthStatus" +
                          " from event as e" +
                          " join area as a on e.area_id = a.id" +
                          " join medorganisation as mo on e.medOrg_id = mo.id" +
                          " join childrenshealthorganization as doo on e.doo_id = doo.id" +
                          " join client as c on e.client_id = c.id" +
                          " join relaxinfo as r on e.relax_id = r.id" +
                          " where e.deleted = 0" +
                          "   and doo.deleted = 0" +
                          "   and c.deleted = 0" +
                          "   and a.deleted = 0" +
                          "   and mo.deleted = 0";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    events.Load(sqlCommand.ExecuteReader());
                }
            }
            
            return events;
        }
    }
}
