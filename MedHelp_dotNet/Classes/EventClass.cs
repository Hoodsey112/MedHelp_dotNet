﻿using System;
using System.CodeDom;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet.Classes
{
    public class EventClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
        public int HelpName { get; set; }
        public string DiagName { get; set; }
        public string DiagID { get; set; }
        public string Speciality { get; set; }
        public string Department { get; set; }
        public string TransfertedDepart { get; set; }
        public string TransfertedDate { get; set; }
        public string HealthStatus { get; set; }

        public static void InsertEvent(int area_id, int medOrg_id, DateTime eventDate, int doo_id, int client_id, int relax_id, DateTime TreatmentDate, int HelpName, string DiagName, string DiagID, string Speciality, string Department, string TransfertedDepart, DateTime TransfertedDate, string HealthStatus, int classCase_id)
        {
            try
            {
                string query = "insert into event (area_id, medOrg_id, eventDate, doo_id, client_id, relax_id, TreatmentDate, HelpName, DiagName, DiagID, Speciality, Department, TransfertedDepart, TransfertedDate, HealthStatus, classCase_id)" +
                             $" value ({area_id}, {medOrg_id}, '{eventDate:yyyy-MM-dd}', {doo_id}, {client_id}, {relax_id}, '{TreatmentDate:yyyy-MM-dd}', '{HelpName}', '{DiagName}', '{DiagID}', '{Speciality}', '{Department}', '{TransfertedDepart}', '{(TransfertedDate.ToString("yyyy-MM-dd") == DateTime.MinValue.ToString("yyyy-MM-dd") ? "0000-00-00" : TransfertedDate.ToString("yyyy-MM-dd"))}', '{HealthStatus}', {classCase_id})";

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

        public static void UpdateEvent(int event_id, int area_id, int medOrg_id, DateTime eventDate, int doo_id, int client_id, int relax_id, DateTime TreatmentDate, int HelpName, string DiagName, string DiagID, string Speciality, string Department, string TransfertedDepart, DateTime TransfertedDate, string HealthStatus, int classCase_id)
        {
            try
            { 
                string query = $"update event set area_id = {area_id}, medOrg_id = {medOrg_id}, eventDate = '{eventDate:yyyy-MM-dd}', doo_id = {doo_id}, client_id = {client_id}, relax_id = {relax_id}, TreatmentDate = '{TreatmentDate:yyyy-MM-dd}', HelpName = '{HelpName}', DiagName = '{DiagName}', DiagID = '{DiagID}', Speciality = '{Speciality}', Department = '{Department}', TransfertedDepart = '{TransfertedDepart}', TransfertedDate = '{TransfertedDate:yyyy-MM-dd}', HealthStatus = '{HealthStatus}', classCase_id = {classCase_id} where id = {event_id} and deleted = 0";

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

        public static DataTable EditEvent(int event_id)
        {
            try
            {
                DataTable editData = new DataTable();

                string query = "select e.id," +
                                     " a.name as AreaName," +
                                     " mo.name as medOrgName," +
                                     " e.eventDate," +
                                     " doo.ShortName as DOOName," +
                                     " e.client_id," +
                                     " e.relax_id," +
                                     " e.TreatmentDate," + //7
                                     " e.HelpName," +
                                     " e.DiagName," +
                                     " e.DiagID," +
                                     " e.Speciality," + //11
                                     " e.Department," +
                                     " e.TransfertedDepart," + //13
                                     " if (e.TransfertedDate = '0000-00-00', CURRENT_DATE(), e.TransfertedDate) as TransfertedDate," +
                                     " e.HealthStatus," + //15
                                     " cc.name as ClassCaseName" + //16
                              " from event as e" +
                              " join area as a on e.area_id = a.id" +
                              " join medorganisation as mo on e.medOrg_id = mo.id" +
                              " join childrenshealthorganization as doo on e.doo_id = doo.id" +
                              " left join classCase as cc on e.classCase_id = cc.id" +
                              " where e.deleted = 0" +
                             $"   and e.id = {event_id}";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        editData.Load(sqlCommand.ExecuteReader());
                    }
                }

                return editData;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable LoadEvent()
        {
            try
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
                                     " hc.FullName," +
                                     " e.DiagName," +
                                     " e.DiagID," +
                                     " e.Speciality," +
                                     " e.Department," +
                                     " e.TransfertedDepart," +
                                     " if (e.TransfertedDate = '0000-00-00', \"\", e.TransfertedDate) as TransfertedDate," +
                                     " cc.name as ClassCaseName," +
                                     " e.HealthStatus" +
                              " from event as e" +
                              " join area as a on e.area_id = a.id" +
                              " join medorganisation as mo on e.medOrg_id = mo.id" +
                              " join childrenshealthorganization as doo on e.doo_id = doo.id" +
                              " join client as c on e.client_id = c.id" +
                              " join relaxinfo as r on e.relax_id = r.id" +
                              " join relaxinfo as r1 on r.parent_id = r1.id" +
                              " join HelpCare as hc on e.HelpName = hc.id" +
                              " left join classCase as cc on cc.id = e.classCase_id" +
                              " where e.deleted = 0" +
                              "   and doo.deleted = 0" +
                              "   and c.deleted = 0" +
                              "   and a.deleted = 0" +
                              "   and mo.deleted = 0" +
                              " order by e.id asc";

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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable LoadDataToReport(string areaCondition, string medOrgCondition, string eventDateCondition, string DOOCondition, string ageCondition, string TreatmentDateCondition, string relaxCondition, string medHelpCondition, string HealthStatusCondition)
        {
            try
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
                                   relaxCondition +
                                   medHelpCondition +
                                   HealthStatusCondition +
                                   " GROUP BY 1) _all" +
                              " LEFT JOIN (select z0.area_id," +
                                                " sum(z0.samost) samost," +
                                                " sum(z0.MotherAndChild) MotherAndChild" +
                                         " from(" +
                                                " SELECT e.area_id," +
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
                                                " GROUP BY e.area_id, ri.name) z0" +
                                         " group by 1) orgRelax ON orgRelax.area_id = _all.area_id" +
                              " LEFT JOIN(select z2.area_id," +
                                               " sum(z2.samost) samost," +
                                               " sum(z2._WithParent) _WithParent" +
                                        " from (" +
                                                " SELECT e.area_id," +
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
                                                " GROUP BY e.area_id, ri.name) z2" +
                                        " group by 1) norgRelax ON norgRelax.area_id = _all.area_id" +
                              " LEFT JOIN(SELECT z1.area_id," +
                                               " SUM(z1.SMP) SMP," +
                                               " SUM(z1.PMSP) PMSP," +
                                               " SUM(z1.PSMSP) PSMSP" +
                                        " FROM(" +
                                            " SELECT e.area_id," +
                                                    " CASE WHEN HelpName = 3 THEN COUNT(e.id) ELSE 0 END SMP," +
                                                    " CASE WHEN HelpName = 1 THEN COUNT(e.id) ELSE 0 END PMSP," +
                                                    " CASE WHEN HelpName = 2 THEN COUNT(e.id) ELSE 0 END PSMSP" +
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
                                            " GROUP BY e.area_id, HelpName) z1" +
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable LoadDataToTableReport(string areaCondition, string medOrgCondition, string eventDateCondition, string DOOCondition, string ageCondition, string TreatmentDateCondition, string relaxCondition, string medHelpCondition, string HealthStatusCondition)
        {
            try
            {
                DataTable events = new DataTable();

                string query = "SELECT a.name," +
                                     " travm," +
                                     " utopl," +
                                     " infekc," +
                                     " hirurg," +
                                     " somat," +
                                     " neiro," +
                                     " nevro," +
                                     " lor," +
                                     " otrav," +
                                     " alko," +
                                     " narko," +
                                     " other" +
                              " FROM(" +
                                    " SELECT area_id," +
                                            " SUM(CASE WHEN classCase_id = 1 then 1 ELSE 0 END) travm," +
                                            " SUM(CASE WHEN classCase_id = 2 then 1 ELSE 0 END) utopl," +
                                            " SUM(CASE WHEN classCase_id = 3 then 1 ELSE 0 END) infekc," +
                                            " SUM(CASE WHEN classCase_id = 4 then 1 ELSE 0 END) hirurg," +
                                            " SUM(CASE WHEN classCase_id = 5 then 1 ELSE 0 END) somat," +
                                            " SUM(CASE WHEN classCase_id = 6 then 1 ELSE 0 END) neiro," +
                                            " SUM(CASE WHEN classCase_id = 7 then 1 ELSE 0 END) nevro," +
                                            " SUM(CASE WHEN classCase_id = 8 then 1 ELSE 0 END) lor," +
                                            " SUM(CASE WHEN classCase_id = 9 then 1 ELSE 0 END) otrav," +
                                            " SUM(CASE WHEN classCase_id = 10 then 1 ELSE 0 END) alko," +
                                            " SUM(CASE WHEN classCase_id = 11 then 1 ELSE 0 END) narko," +
                                            " SUM(CASE WHEN classCase_id = 12 then 1 ELSE 0 END) other" +
                                    " FROM event e" +
                                    " WHERE e.deleted = 0" +
                                        " AND classCase_id IS NOT null" +
                                            areaCondition +
                                            medOrgCondition +
                                            eventDateCondition +
                                            DOOCondition +
                                            ageCondition +
                                            TreatmentDateCondition +
                                            relaxCondition +
                                            medHelpCondition +
                                            HealthStatusCondition +
                                    " GROUP BY area_id) z1" +
                              " JOIN area a ON z1.area_id = a.id";

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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable LoadDataToCHLDReport(string areaCondition, string medOrgCondition, string eventDateCondition, string DOOCondition, string ageCondition, string TreatmentDateCondition, string HealthStatusCondition)
        {
            try
            {
                DataTable events = new DataTable();

                string query = "SELECT CASE WHEN relax_id IN (2,3) THEN 'Организованный отдых' ELSE 'Неорганизованный отдых' END relax," +
                                     " COUNT(id) cnt" +
                              " FROM event e" +
                              " WHERE e.deleted = 0" +
                                " AND e.helpname = 3" +
                                  areaCondition +
                                  medOrgCondition +
                                  eventDateCondition +
                                  DOOCondition +
                                  ageCondition +
                                  TreatmentDateCondition +
                                  HealthStatusCondition +
                              " GROUP BY relax_id";

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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable ReportWord(string areaCondition, string medOrgCondition, string eventDateCondition, string DOOCondition, string ageCondition, string TreatmentDateCondition, string relaxCondition, string medHelpCondition, string HealthStatusCondition)
        {
            try
            {
                DataTable reportWord = new DataTable();

                string query = "SELECT a.name," +
                                     " _area.AllEvent," +
                                     " ri.name," +
                                     " _relax.relaxCNT," +
                                     " doo.FullName," +
                                     " _doo.dooCNT," +
                                     " hc.FullName," +
                                     " _help.helpCNT," +
                                     " _diag.DiagName," +
                                     " _diag.diagCNT" +
                              " FROM(SELECT e.area_id," +
                                          " COUNT(e.id) AllEvent" +
                                   " FROM event e" +
                                   " join client c on e.client_id = c.id" +
                                   " WHERE e.deleted = 0" +
                                     " AND c.deleted = 0" +
                                     areaCondition +
                                     medOrgCondition +
                                     eventDateCondition +
                                     DOOCondition +
                                     ageCondition +
                                     TreatmentDateCondition +
                                     relaxCondition +
                                     medHelpCondition +
                                     HealthStatusCondition +
                                   " GROUP BY 1) _area" +
                              " LEFT JOIN(SELECT e.area_id," +
                                               " ri.parent_id," +
                                               " COUNT(e.id) relaxCNT" +
                                        " FROM event e" +
                                        " JOIN client c ON e.client_id = c.id" +
                                        " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                        " WHERE e.deleted = 0" +
                                          " AND c.deleted = 0" +
                                          areaCondition +
                                          medOrgCondition +
                                          eventDateCondition +
                                          DOOCondition +
                                          ageCondition +
                                          TreatmentDateCondition +
                                          relaxCondition +
                                          medHelpCondition +
                                          HealthStatusCondition +
                                        " GROUP BY 1,2) _relax ON _area.area_id = _relax.area_id" +
                              " LEFT JOIN(SELECT e.area_id," +
                                               " ri.parent_id," +
                                               " e.doo_id," +
                                               " COUNT(e.id) dooCNT" +
                                        " FROM event e" +
                                        " JOIN client c ON e.client_id = c.id" +
                                        " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                        " WHERE e.deleted = 0" +
                                          " AND c.deleted = 0" +
                                          areaCondition +
                                          medOrgCondition +
                                          eventDateCondition +
                                          DOOCondition +
                                          ageCondition +
                                          TreatmentDateCondition +
                                          relaxCondition +
                                          medHelpCondition +
                                          HealthStatusCondition +
                                        " GROUP BY 1,2,3)_doo ON _relax.area_id = _doo.area_id AND _relax.parent_id = _doo.parent_id" +
                              " LEFT JOIN(SELECT e.area_id," +
                                               " ri.parent_id," +
                                               " e.doo_id," +
                                               " e.HelpName," +
                                               " COUNT(e.id) helpCNT" +
                                        " FROM event e" +
                                        " JOIN client c ON e.client_id = c.id" +
                                        " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                        " WHERE e.deleted = 0" +
                                          " AND c.deleted = 0" +
                                          areaCondition +
                                          medOrgCondition +
                                          eventDateCondition +
                                          DOOCondition +
                                          ageCondition +
                                          TreatmentDateCondition +
                                          relaxCondition +
                                          medHelpCondition +
                                          HealthStatusCondition +
                                        " GROUP BY 1,2,3,4) _help ON _doo.area_id = _help.area_id AND _doo.parent_id = _help.parent_id AND _doo.doo_id = _help.doo_id" +
                              " LEFT JOIN(SELECT e.area_id," +
                                               " ri.parent_id," +
                                               " e.doo_id," +
                                               " e.HelpName," +
                                               " e.DiagName," +
                                               " COUNT(e.id) diagCNT" +
                                        " FROM event e" +
                                        " JOIN client c ON e.client_id = c.id" +
                                        " JOIN relaxinfo ri ON e.relax_id = ri.id" +
                                        " WHERE e.deleted = 0" +
                                          " AND c.deleted = 0" +
                                          areaCondition +
                                          medOrgCondition +
                                          eventDateCondition +
                                          DOOCondition +
                                          ageCondition +
                                          TreatmentDateCondition +
                                          relaxCondition +
                                          medHelpCondition +
                                          HealthStatusCondition +
                                        " GROUP BY 1,2,3,4,5) _diag ON _diag.area_id = _help.area_id AND _diag.parent_id = _help.parent_id AND _diag.doo_id = _help.doo_id AND _help.HelpName = _diag.HelpName" +
                              " JOIN area a ON _area.area_id = a.id" +
                              " JOIN relaxinfo ri ON _relax.parent_id = ri.id" +
                              " JOIN childrenshealthorganization doo ON _doo.doo_id = doo.id" +
                              " JOIN HelpCare hc ON _help.HelpName = hc.id";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        reportWord.Load(sqlCommand.ExecuteReader());
                    }
                }

                return reportWord;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DateTime[] SetMinMaxDate()
        {
            try
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
                                while (reader.Read())
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static DataTable SetDuplicate()
        {
            try
            {
                DataTable DataTable = new DataTable();

                string query = "SELECT e.area_id," +
                                     " e.doo_id," +
                                     " e.relax_id," +
                                     " e.client_id," +
                                     " COUNT(e.id) cntEvent" +
                              " FROM event e" +
                              " JOIN client c ON e.client_id = c.id" +
                              " WHERE e.deleted = 0" +
                              " GROUP BY 1,2,3,4" +
                              " HAVING cntEvent > 1";

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        DataTable.Load(sqlCommand.ExecuteReader());
                    }
                }

                string area = " and e.area_id in (";
                string doo = " and e.doo_id in (";
                string relax = " and e.relax_id in (";
                string client = " and e.client_id in (";

                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    if (i != DataTable.Rows.Count-1)
                    {
                        area += $"{DataTable.Rows[i][0].ToString()}, ";
                        doo += $"{DataTable.Rows[i][1].ToString()}, ";
                        relax += $"{DataTable.Rows[i][2].ToString()}, ";
                        client += $"{DataTable.Rows[i][3].ToString()}, ";
                    }
                    else
                    {
                        area += $"{DataTable.Rows[i][0].ToString()})";
                        doo += $"{DataTable.Rows[i][1].ToString()})";
                        relax += $"{DataTable.Rows[i][2].ToString()})";
                        client += $"{DataTable.Rows[i][3].ToString()})";
                    }
                }

                query = "select e.id," +
                              " a.name as AreaName," +
                              " mo.name as medOrgName," +
                              " e.eventDate," +
                              " doo.FullName as DOOName," +
                              " doo.Address as DOOAddress," +
                              " c.FIO as clientFIO," +
                              " c.birthDate as clientBirthDate," +
                              " CONCAT((YEAR(current_date()) - YEAR(c.birthDate)) - if (DAYOFYEAR(current_date()) > DAYOFYEAR(c.birthDate),0,1),' лет ', (IF(MONTH(CURDATE()) - MONTH(c.birthDate) < 0, MONTH(CURDATE()) - MONTH(c.birthDate) + 12, MONTH(CURDATE()) - MONTH(c.birthDate))),' мес.') AS clientAge," +
                              " c.Address as clientAddress," +
                              " CONCAT(r1.Name, ' (', r.Name, ')') as relaxName," +
                              " e.TreatmentDate," +
                              " e.HelpName," +
                              " hc.FullName," +
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
                       " join HelpCare as hc on e.HelpName = hc.id" +
                       " where e.deleted = 0" +
                         " and doo.deleted = 0" +
                         " and c.deleted = 0" +
                         " and a.deleted = 0" +
                         " and mo.deleted = 0" +
                         area +
                         doo +
                         relax +
                         client;

                DataTable = new DataTable();

                using (MySqlConnection sqlConnection = ConnectionClass.GetStringConnection())
                {
                    sqlConnection.Open();

                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        DataTable.Load(sqlCommand.ExecuteReader());
                    }
                }

                return DataTable;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static void RemoveEvent(int id)
        {
            try
            {
                string query = $"update event set deleted = 1 where id = {id}";

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
    }
}
