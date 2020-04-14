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
                                 " age(c.birthDate, current_date()) as age," +
                                 " c.Address as clientAddress," +
                                 " CONCAT(r1.Name, ' (', r.Name, ')') as relaxName," +
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
                          " join relaxinfo as r1 on r.parent_id = r1.id" +
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

        public static DataTable LoadDataToReport(string areaCondition, string medOrgCondition, string eventDateCondition, string DOOCondition, string ageCondition, string TreatmentDateCondition, string relaxCondition, string medHelpCondition, string HealthStatusCondition)//int area_id, int medOrg_id
        {
            DataTable events = new DataTable();

            string query = "SELECT a.name," +
                                 " IF(_all.AllEvent IS NULL, 0, _all.AllEvent) AllEvent," +
                                 " IF(orgRelax.samost IS NULL, 0, orgRelax.samost) orgSamost," +
                                 " IF(orgRelax.MotherAndChild IS NULL, 0, orgRelax.MotherAndChild) MotherAndChild," +
                                 " IF(norgRelax.samost IS NULL, 0, norgRelax.samost) noorgSamost," +
                                 " IF(norgRelax._WithParent IS NULL, 0, norgRelax._WithParent) WithParent," +
                                 " IF(medHelp.PMSP IS NULL, 0, medHelp.PMSP) PMSP," +
                                 " IF(medHelp.PSMSP IS NULL, 0, medHelp.PSMSP) PSMSP," +
                                 " IF(medHelp.SMP IS NULL, 0, medHelp.SMP) SMP," +
                                 " IF(reanim.ambulance IS NULL, 0, reanim.ambulance) ambulance" +
                          " FROM(" +
                               " SELECT e.area_id," +
                                      " COUNT(e.id) AllEvent" +
                               " FROM event e" +
                               " join client c on e.client_id = c.id" +
                               " WHERE e.deleted = 0" +
                               areaCondition +
                               medOrgCondition +
                               eventDateCondition +
                               DOOCondition +
                               ageCondition +
                               TreatmentDateCondition +
                               relaxCondition+
                               medHelpCondition +
                               HealthStatusCondition +
                               " GROUP BY 1) _all" +
                          " LEFT JOIN (SELECT e.area_id," +
                                            " CASE WHEN ri.name = 'Самостоятельно' THEN COUNT(e.id) ELSE 0 end samost," +
                                            " CASE WHEN ri.name = 'По путевке Мать и дитя' THEN COUNT(e.id) ELSE 0 end MotherAndChild" +
                                     " FROM event e" +
                                     " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                     " JOIN relaxinfo ri1 ON ri.parent_id = ri1.id" +
                                     " join client c on e.client_id = c.id" +
                                     " WHERE e.deleted = 0" +
                                       " AND ri1.id = 1" +
                                       areaCondition +
                                       medOrgCondition +
                                       eventDateCondition +
                                       DOOCondition +
                                       ageCondition +
                                       TreatmentDateCondition +
                                       relaxCondition +
                                       medHelpCondition +
                                       HealthStatusCondition +
                                     " GROUP BY 1) orgRelax ON orgRelax.area_id = _all.area_id" +
                          " LEFT JOIN(SELECT e.area_id," +
                                           " CASE WHEN ri.name = 'Самостоятельно' THEN COUNT(e.id) ELSE 0 end samost," +
                                           " CASE WHEN ri.name = 'С законным представителем' THEN COUNT(e.id) ELSE 0 end _WithParent" +
                                    " FROM event e" +
                                    " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                    " JOIN relaxinfo ri1 ON ri.parent_id = ri1.id" +
                                    " join client c on e.client_id = c.id" +
                                    " WHERE e.deleted = 0" +
                                      " AND ri1.id = 4" +
                                      areaCondition +
                                      medOrgCondition +
                                      eventDateCondition +
                                      DOOCondition +
                                      ageCondition +
                                      TreatmentDateCondition +
                                      relaxCondition +
                                      medHelpCondition +
                                      HealthStatusCondition +
                                    " GROUP BY 1) norgRelax ON norgRelax.area_id = _all.area_id" +
                          " LEFT JOIN(SELECT e.area_id," +
                                           " CASE WHEN HelpName = 'Специализированная медицинская помощь (стационар)' THEN COUNT(e.id) ELSE 0 END SMP," +
                                           " CASE WHEN HelpName = 'Первичная медико-санитарная помощь (поликлиника-педиатр)' THEN COUNT(e.id) ELSE 0 END PMSP," +
                                           " CASE WHEN HelpName = 'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)' THEN COUNT(e.id) ELSE 0 END PSMSP" +
                                    " FROM event e" +
                                    " join client c on e.client_id = c.id" +
                                    " WHERE e.deleted = 0" +
                                       areaCondition +
                                       medOrgCondition +
                                       eventDateCondition +
                                       DOOCondition +
                                       ageCondition +
                                       TreatmentDateCondition +
                                       relaxCondition +
                                       medHelpCondition +
                                       HealthStatusCondition +
                                    " GROUP BY 1) medHelp ON medHelp.area_id = _all.area_id" +
                          " LEFT JOIN(SELECT e.area_id," +
                                           " CASE WHEN (TransfertedDepart IS NOT NULL AND TransfertedDepart<> '') THEN COUNT(e.id) ELSE 0 END ambulance" +
                                    " FROM event e" +
                                    " join client c on e.client_id = c.id" +
                                    " WHERE e.deleted = 0" +
                                      " AND (TransfertedDepart IS NOT NULL AND TransfertedDepart <> '')" +
                                      areaCondition +
                                      medOrgCondition +
                                      eventDateCondition +
                                      DOOCondition +
                                      ageCondition +
                                      TreatmentDateCondition +
                                      relaxCondition +
                                      medHelpCondition +
                                      HealthStatusCondition +
                                    " GROUP BY 1) reanim ON _all.area_id = reanim.area_id" +
                          " JOIN area a ON _all.area_id = a.id";

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

        public static DateTime[] SetMinMaxDate()
        {
            DateTime[] dateTimes = new DateTime[2];
            string query = "SELECT MIN(TreatmentDate) minDate, MAX(TreatmentDate) maxDate FROM event e";

            using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
            {
                sqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                {
                    using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    dateTimes[i] = reader.GetDateTime(i);
                                }
                            }
                        }
                    }
                }
            }

            return dateTimes;
        }
    }
}
