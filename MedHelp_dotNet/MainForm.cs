﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using NLog;

namespace MedHelp_dotNet
{
    public partial class MainForm : Form
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        bool firstStart = true;

        public XmlDocument xmlDoc;
        DataTable eventsData = new DataTable();
        Classes.AreaClass[] areas = Classes.AreaClass.LoadListArea();
        Classes.MOClass[] medOrgN;
        Classes.HealthOrgClass[] HealthOrg;
        int cntCheckRelax = 4;
        int cntCheckHelp = 3;
        string str = "'Организованный отдых (Самостоятельно)', 'Организованный отдых (По путевке Мать и дитя)', 'Неорганизованный отдых (Самостоятельно)', 'Неорганизованный отдых (С законным представителем)'";
        string strH = "1, 2, 3";
        string relaxCon= "2, 3, 5, 6";
        string relaxCondition= " and e.relax_id in (2, 3, 5, 6)";
        string areaCondition = "";
        string medOrgCondition = "";
        string eventDateCondition = "";
        string DOOCondition = "";
        string TreatmentDateCondition = "";
        string medHelpCondition = " and HelpName in (1, 2, 3)";
        string HealthStatusCondition = "";
        string ageCondition = "";
        string areaFilter = "[areaName] like '%'";
        string medOrgFilter = "[medOrgName] like '%'";
        string eventDateFilter = "[eventDate] > '01.01.1753'";
        string DOOFilter = "[DOOName] like '%'";
        string clientFilter = "[ClientFIO] like '%'";
        string ageFilter = "[age] >= 0";
        string relaxFilter = "[relaxName] in ('Организованный отдых (Самостоятельно)', 'Организованный отдых (По путевке Мать и дитя)', 'Неорганизованный отдых (Самостоятельно)', 'Неорганизованный отдых (С законным представителем)')";
        string TreatmentFilter = "[TreatmentDate] > '01.01.1753'";
        string helpFilter = "[HelpName] in (1, 2, 3)";
        string transfertedFilter = "[TransfertedDepart] like '%'";
        string healthFilter = "[HealthStatus] like '%'";


        public MainForm()
        {
            try
            {
                InitializeComponent();
                dataGridView1.AutoGenerateColumns = false;

                Cursor.Current = Cursors.WaitCursor;
                LoadEvent_Area();
                Cursor.Current = Cursors.Default;

                Cursor.Current = Cursors.WaitCursor;
                cbHealthStatus.DataSource = null;
                cbHealthStatus.DataSource = Classes.HealthStatusClass.LoadHealthStatus();
                Cursor.Current = Cursors.Default;

                firstStart = false;
                KeyPreview = true;

                Server.Text = $"Сервер: {Properties.Settings.Default.server}";
                DataBase.Text = $"База данных: {Properties.Settings.Default.dataBase}";
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SettingsForm_Closed(object sender, FormClosedEventArgs e)
        {
            Server.Text = $"Сервер: {Properties.Settings.Default.server}"; //Отображение адреса сервера(внизу формы)
            DataBase.Text = $"База: {Properties.Settings.Default.dataBase}"; //Отображение БД(с которой работает пользователь)
        }

        private void LoadEvent_Area()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                eventsData = Classes.EventClass.LoadEvent();
                dataGridView1.DataSource = eventsData;

                cbArea.DisplayMember = "name";
                cbArea.ValueMember = "id";
                cbArea.DataSource = areas;
                cbArea.SelectedIndex = -1;
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectFilter()
        {
            if (!firstStart)
            {
                eventsData.DefaultView.RowFilter = $"{areaFilter} and {medOrgFilter} and {DOOFilter} and {clientFilter} and {ageFilter} and {relaxFilter} and {helpFilter} and {transfertedFilter} and {healthFilter} and {eventDateFilter} and {TreatmentFilter}"; //
                TableCNTRows.Text = eventsData.DefaultView.Count.ToString();
            }
        }

        private void AreaCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (AreaCB.Checked)
                {
                    cbArea.Enabled = true;
                    if (cbArea.SelectedItem != null)
                    {
                        areaFilter = $"[areaName] = '{areas[cbArea.SelectedIndex].name}'";
                        areaCondition = $"and e.area_id = {areas[cbArea.SelectedIndex].id}";
                    }
                }
                else
                {
                    cbArea.Enabled = false;
                    areaFilter = $"[areaName] like '%'";
                    areaCondition = "";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void medOrgCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (medOrgCB.Checked)
                {
                    cbMedOrg.Enabled = true;
                    if (cbMedOrg.SelectedItem != null)
                    {
                        medOrgFilter = $"[medOrgName] = '{medOrgN[cbMedOrg.SelectedIndex].name}'";
                        medOrgCondition = $"and e.medOrg_id = {medOrgN[cbMedOrg.SelectedIndex].id}";
                    }
                }
                else
                {
                    cbMedOrg.Enabled = false;
                    medOrgFilter = $"[medOrgName] like '%'";
                    medOrgCondition = "";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eventDateCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (eventDateCB.Checked)
                {
                    eventDate_From.Enabled = true;
                    eventDate_To.Enabled = true;
                    label1.Enabled = true;
                    label2.Enabled = true;

                    eventDateFilter = $"[eventDate] >= '{eventDate_From.Value:dd.MM.yyyy}' and [eventDate] <= '{eventDate_To.Value:dd.MM.yyyy}'";
                    eventDateCondition = $" and e.eventDate between '{eventDate_From.Value:yyyy-MM-dd}' and '{eventDate_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    eventDate_From.Enabled = false;
                    eventDate_To.Enabled = false;
                    label1.Enabled = false;
                    label2.Enabled = false;

                    eventDateFilter = $"[eventDate] >= '{DateTime.MinValue:dd.MM.yyyy}'";
                    eventDateCondition = "";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DOOCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (DOOCB.Checked)
                {
                    cbDOO.Enabled = true;
                    if (cbDOO.SelectedItem != null)
                    {
                        DOOFilter = $"[DOOName] = '{HealthOrg[cbDOO.SelectedIndex].FullName}'";
                        DOOCondition = $" and e.doo_id = {HealthOrg[cbDOO.SelectedIndex].id}";
                    }
                }
                else
                {
                    cbDOO.Enabled = false;
                    DOOFilter = $"[DOOName] like '%'";
                    DOOCondition = "";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientFIOCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (ClientFIOCB.Checked)
                {
                    ClientFIOTB.Enabled = true;
                    if (ClientFIOTB.Text.Length != 0) clientFilter = $"[ClientFIO] like '{ClientFIOTB.Text}%'";
                }
                else
                {
                    ClientFIOTB.Enabled = false;
                    clientFilter = $"[ClientFIO] like '%'";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgeCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (AgeCB.Checked)
                {
                    label3.Enabled = true;
                    ageTB_From.Enabled = true;
                    label4.Enabled = true;
                    ageTB_To.Enabled = true;
                    if (ageTB_From.Text.Length > 0 && ageTB_To.Text.Length > 0)
                    {
                        ageFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
                        ageCondition = $" and age(c.birthDate, current_date()) BETWEEN {ageTB_From.Text} AND {ageTB_To.Text}";
                    }
                    else
                    {
                        ageFilter = $"[age] >= 0";
                        ageCondition = "";
                    }
                }
                else
                {
                    label3.Enabled = false;
                    ageTB_From.Enabled = false;
                    label4.Enabled = false;
                    ageTB_To.Enabled = false;
                    ageFilter = $"[age] >= 0";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreatmentCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (TreatmentCB.Checked)
                {
                    label5.Enabled = true;
                    TreatmentDateDP_From.Enabled = true;
                    label6.Enabled = true;
                    TreatmentDateDP_To.Enabled = true;

                    TreatmentFilter = $"[TreatmentDate] >= '{TreatmentDateDP_From.Value:dd.MM.yyyy}' and [TreatmentDate] <= '{TreatmentDateDP_To.Value:dd.MM.yyyy}'";
                    TreatmentDateCondition = $" and TreatmentDate between '{TreatmentDateDP_From.Value:yyyy-MM-dd}' and '{TreatmentDateDP_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    label5.Enabled = false;
                    TreatmentDateDP_From.Enabled = false;
                    label6.Enabled = false;
                    TreatmentDateDP_To.Enabled = false;

                    TreatmentFilter = $"[TreatmentDate] >= '{DateTime.MinValue:dd.MM.yyyy}'";
                    TreatmentDateCondition = $" and TreatmentDate between '{TreatmentDateDP_From.Value:yyyy-MM-dd}' and '{TreatmentDateDP_To.Value:yyyy-MM-dd}'";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransfertedCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (TransfertedCB.Checked)
                {
                    TransfertedTB.Enabled = true;
                    if (TransfertedTB.Text.Length > 0) transfertedFilter = $"[TransfertedDepart] like '{TransfertedTB.Text}%'";
                }
                else
                {
                    TransfertedTB.Enabled = false;
                    transfertedFilter = $"[TransfertedDepart] like '%'";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HealthStatusCB_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (HealthStatusCB.Checked)
                {
                    cbHealthStatus.Enabled = true;
                    if (cbHealthStatus.SelectedItem != null)
                    {
                        healthFilter = $"[HealthStatus] = '{cbHealthStatus.SelectedItem.ToString()}'";
                        HealthStatusCondition = $" and HealthStatus = '{cbHealthStatus.SelectedItem.ToString()}'";
                    }
                }
                else
                {
                    cbHealthStatus.Enabled = false;
                    healthFilter = $"[HealthStatus] like '%'";
                    HealthStatusCondition = "";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (cbArea.SelectedItem != null)
                {
                    areaFilter = $"[areaName] like '{areas[cbArea.SelectedIndex].name}'";
                    areaCondition = $" and e.area_id = {areas[cbArea.SelectedIndex].id}";
                }
                else
                {
                    areaFilter = $"[areaName] like '%'";
                    areaCondition = "";
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMedOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!firstStart)
                {
                    if (cbMedOrg.SelectedItem != null)
                    {
                        medOrgFilter = $"[medOrgName] like '{medOrgN[cbMedOrg.SelectedIndex].name}'";
                        medOrgCondition = $" and e.medOrg_id = {medOrgN[cbMedOrg.SelectedIndex].id}";
                    }
                    else
                    {
                        medOrgFilter = $"[medOrgName] like '%'";
                        medOrgCondition = $"";
                    }
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbDOO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!firstStart)
                {
                    if (cbDOO.SelectedItem != null)
                    {
                        DOOFilter = $"[DOOName] = '{HealthOrg[cbDOO.SelectedIndex].FullName}'";
                        DOOCondition = $" and e.doo_id = {HealthOrg[cbDOO.SelectedIndex].id}";
                    }
                    else
                    {
                        DOOFilter = $"[DOOName] like '%'";
                        DOOCondition = "";
                    }
                }
                SelectFilter();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientFIOTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ClientFIOCB.Checked) clientFilter = $"[ClientFIO] like '{ClientFIOTB.Text}%'";
                else clientFilter = $"[ClientFIO] like '%'";
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMedOrg_DropDown(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (AreaCB.Checked) medOrgN = Classes.MOClass.LoadMOList(int.Parse(cbArea.SelectedValue.ToString()));
                else medOrgN = Classes.MOClass.LoadMOList();

                cbMedOrg.DataSource = medOrgN;
                cbMedOrg.DisplayMember = "name";
                cbMedOrg.ValueMember = "id";
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbDOO_DropDown(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (AreaCB.Checked) HealthOrg = Classes.HealthOrgClass.LoadHealthOrgList(int.Parse(cbArea.SelectedValue.ToString()));
                else HealthOrg = Classes.HealthOrgClass.LoadHealthOrgList();

                cbDOO.DataSource = HealthOrg;
                cbDOO.DisplayMember = "ShortName";
                cbDOO.ValueMember = "id";
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eventDate_From_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (eventDateCB.Checked)
                {
                    eventDateFilter = $"[eventDate] >= '{eventDate_From.Value:dd.MM.yyyy}' and [eventDate] <= '{eventDate_To.Value:dd.MM.yyyy}'";
                    eventDateCondition = $" and e.eventDate between '{eventDate_From.Value:yyyy-MM-dd}' and '{eventDate_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    eventDateFilter = $"[eventDate] like '%'";
                    eventDateCondition = "";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eventDate_To_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (eventDateCB.Checked)
                {
                    eventDateFilter = $"[eventDate] >= '{eventDate_From.Value:dd.MM.yyyy}' and [eventDate] <= '{eventDate_To.Value:dd.MM.yyyy}'";
                    eventDateCondition = $" and e.eventDate between '{eventDate_From.Value:yyyy-MM-dd}' and '{eventDate_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    eventDateFilter = $"[eventDate] like '%'";
                    eventDateCondition = "";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreatmentDateDP_From_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TreatmentCB.Checked)
                {
                    TreatmentFilter = $"[TreatmentDate] >= '{TreatmentDateDP_From.Value:dd.MM.yyyy}' and [TreatmentDate] <= '{TreatmentDateDP_To.Value:dd.MM.yyyy}'";
                    TreatmentDateCondition = $" and TreatmentDate between '{TreatmentDateDP_From.Value:yyyy-MM-dd}' and '{TreatmentDateDP_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    TreatmentFilter = $"[TreatmentDate] like '%'";//$"[eventDate] like '%'";
                    TreatmentDateCondition = "";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreatmentDateDP_To_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TreatmentCB.Checked)
                {
                    eventsData.DefaultView.RowFilter = $"[TreatmentDate] >= '{TreatmentDateDP_From.Value:dd.MM.yyyy}' and [TreatmentDate] <= '{TreatmentDateDP_To.Value:dd.MM.yyyy}'";
                    TreatmentDateCondition = $" and TreatmentDate between '{TreatmentDateDP_From.Value:yyyy-MM-dd}' and '{TreatmentDateDP_To.Value:yyyy-MM-dd}'";
                }
                else
                {
                    eventsData.DefaultView.RowFilter = $"[eventDate] like '%'";
                    TreatmentDateCondition = "";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransfertedTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TransfertedCB.Checked) transfertedFilter = $"[TransfertedDepart] like '{TransfertedTB.Text}%'";
                else transfertedFilter = $"[TransfertedDepart] like '%'";
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbHealthStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (HealthStatusCB.Checked)
                {
                    healthFilter = $"[HealthStatus] = '{cbHealthStatus.SelectedItem.ToString()}'";
                    HealthStatusCondition = $" and HealthStatus = '{cbHealthStatus.SelectedItem.ToString()}'";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RelaxInfo_Child2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                relaxFilter = CheckRelaxInfo(RelaxInfo_Child2);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RelaxInfo_Child3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                relaxFilter = CheckRelaxInfo(RelaxInfo_Child3);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RelaxInfo_Child5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                relaxFilter = CheckRelaxInfo(RelaxInfo_Child5);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RelaxInfo_Child6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                relaxFilter = CheckRelaxInfo(RelaxInfo_Child6);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CheckRelaxInfo(CheckBox checkBox)
        {
            try
            {
                if (checkBox.Checked)
                {
                    if (cntCheckRelax == 0)
                    {
                        switch (checkBox.Name)
                        {
                            case "RelaxInfo_Child2":
                                cntCheckRelax++;
                                str = "'Организованный отдых (Самостоятельно)'";
                                relaxCon = "2";
                                break;
                            case "RelaxInfo_Child3":
                                cntCheckRelax++;
                                str = "'Организованный отдых (По путевке Мать и дитя)'";
                                relaxCon = "3";
                                break;
                            case "RelaxInfo_Child5":
                                cntCheckRelax++;
                                str = "'Неорганизованный отдых (Самостоятельно)'";
                                relaxCon = "5";
                                break;
                            case "RelaxInfo_Child6":
                                cntCheckRelax++;
                                str = "'Неорганизованный отдых (С законным представителем)'";
                                relaxCon = "6";
                                break;
                        }
                    }
                    else
                    {
                        switch (checkBox.Name)
                        {
                            case "RelaxInfo_Child2":
                                cntCheckRelax++;
                                str += ", 'Организованный отдых (Самостоятельно)'";
                                relaxCon += ", 2";
                                break;
                            case "RelaxInfo_Child3":
                                cntCheckRelax++;
                                str += ", 'Организованный отдых (По путевке Мать и дитя)'";
                                relaxCon += ", 3";
                                break;
                            case "RelaxInfo_Child5":
                                cntCheckRelax++;
                                str += ", 'Неорганизованный отдых (Самостоятельно)'";
                                relaxCon += ", 5";
                                break;
                            case "RelaxInfo_Child6":
                                cntCheckRelax++;
                                str += ", 'Неорганизованный отдых (С законным представителем)'";
                                relaxCon += ", 6";
                                break;
                        }
                    }
                }
                else
                {
                    if (cntCheckRelax > 0)
                    {
                        switch (checkBox.Name)
                        {
                            case "RelaxInfo_Child2":
                                cntCheckRelax--;
                                if (str.IndexOf("'Организованный отдых (Самостоятельно)'", 0) == 0)
                                {
                                    if (cntCheckRelax != 0)
                                    {
                                        str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0), "'Организованный отдых (Самостоятельно)', ".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("2", 0), 3);
                                    }
                                    else
                                    {
                                        str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0), "'Организованный отдых (Самостоятельно)'".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("2", 0), 1);
                                    }
                                }
                                else
                                {
                                    str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0) - 2, ", 'Организованный отдых (Самостоятельно)'".Length);
                                    relaxCon = relaxCon.Remove(relaxCon.IndexOf("2", 0) - 2, 3);
                                }
                                break;
                            case "RelaxInfo_Child3":
                                cntCheckRelax--;
                                if (str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0) == 0)
                                {
                                    if (cntCheckRelax != 0)
                                    {
                                        str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0), "'Организованный отдых (По путевке Мать и дитя)', ".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("3", 0), 3);
                                    }
                                    else
                                    {
                                        str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0), "'Организованный отдых (По путевке Мать и дитя)'".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("3", 0), 1);
                                    }
                                }
                                else
                                {
                                    str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0) - 2, ", 'Организованный отдых (По путевке Мать и дитя)'".Length);
                                    relaxCon = relaxCon.Remove(relaxCon.IndexOf("3", 0) - 2, 3);
                                }
                                break;
                            case "RelaxInfo_Child5":
                                cntCheckRelax--;
                                if (str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0) == 0)
                                {
                                    if (cntCheckRelax != 0)
                                    {
                                        str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0), "'Неорганизованный отдых (Самостоятельно)', ".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("5", 0), 3);
                                    }
                                    else
                                    {
                                        str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0), "'Неорганизованный отдых (Самостоятельно)'".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("5", 0), 1);
                                    }
                                }
                                else
                                {
                                    str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0) - 2, ", 'Неорганизованный отдых (Самостоятельно)'".Length);
                                    relaxCon = relaxCon.Remove(relaxCon.IndexOf("5", 0) - 2, 3);
                                }
                                break;
                            case "RelaxInfo_Child6":
                                cntCheckRelax--;
                                if (str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0) == 0)
                                {
                                    if (cntCheckRelax != 0)
                                    {
                                        str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0), "'Неорганизованный отдых (С законным представителем)', ".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("6", 0), 3);
                                    }
                                    else
                                    {
                                        str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0), "'Неорганизованный отдых (С законным представителем)'".Length);
                                        relaxCon = relaxCon.Remove(relaxCon.IndexOf("6", 0), 1);
                                    }
                                }
                                else
                                {
                                    str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0) - 2, ", 'Неорганизованный отдых (С законным представителем)'".Length);
                                    relaxCon = relaxCon.Remove(relaxCon.IndexOf("6", 0) - 2, 3);
                                }
                                break;
                        }
                    }
                }
                string condition = "";

                if (str != "")
                {
                    condition = $"[relaxName] in ({str}) ";
                    relaxCondition = $" and e.relax_id in ({relaxCon}) ";
                }
                else
                {
                    condition = $"[relaxName] is null";
                    relaxCondition = $" and e.relax_id = 0";
                }

                return condition;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string CheckHelp(CheckBox checkBox)
        {
            try
            {
                if (checkBox.Checked)
                {
                    if (cntCheckHelp == 0)
                    {
                        switch (checkBox.Name)
                        {
                            case "PMSMP":
                                cntCheckHelp++;
                                strH = "1";
                                break;
                            case "PSMSP":
                                cntCheckHelp++;
                                strH = "2";
                                break;
                            case "SMP":
                                cntCheckHelp++;
                                strH = "3";
                                break;
                        }
                    }
                    else
                    {
                        switch (checkBox.Name)
                        {
                            case "PMSMP":
                                cntCheckHelp++;
                                strH += ", 1";
                                break;
                            case "PSMSP":
                                cntCheckHelp++;
                                strH += ", 2";
                                break;
                            case "SMP":
                                cntCheckHelp++;
                                strH += ", 3";
                                break;
                        }
                    }
                }
                else
                {
                    if (cntCheckHelp > 0)
                    {
                        switch (checkBox.Name)
                        {
                            case "PMSMP":
                                cntCheckHelp--;
                                if (strH.IndexOf("1", 0) == 0)
                                {
                                    if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("1", 0), 3);
                                    else strH = strH.Remove(strH.IndexOf("1", 0), 1);
                                }
                                else
                                {
                                    strH = strH.Remove(strH.IndexOf("1", 0) - 2, 3);
                                }
                                break;
                            case "PSMSP":
                                cntCheckHelp--;
                                if (strH.IndexOf("2", 0) == 0)
                                {
                                    if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("2", 0), 3);
                                    else strH = strH.Remove(strH.IndexOf("2", 0), 1);
                                }
                                else strH = strH.Remove(strH.IndexOf("2", 0) - 2, 3);
                                break;
                            case "SMP":
                                cntCheckHelp--;
                                if (strH.IndexOf("3", 0) == 0)
                                {
                                    if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("3", 0), 3);
                                    else strH = strH.Remove(strH.IndexOf("3", 0), 1);
                                }
                                else strH = strH.Remove(strH.IndexOf("3", 0) - 2, 3);
                                break;
                        }
                    }
                }
                string condition = "";

                if (strH != "")
                {
                    condition = $"[HelpName] in ({strH}) ";
                    medHelpCondition = $" and HelpName in ({strH})";
                }
                else
                {
                    condition = $"[HelpName] = 0";
                    medHelpCondition = "";
                }

                return condition;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void PMSMP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                helpFilter = CheckHelp(PMSMP);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PSMSP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                helpFilter = CheckHelp(PSMSP);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SMP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                helpFilter = CheckHelp(SMP);
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ageTB_From_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AgeCB.Checked)
                {
                    ageFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
                    ageCondition = $" and age(c.birthDate, current_date()) BETWEEN {ageTB_From.Text} AND {ageTB_To.Text}";
                }
                else
                {
                    ageFilter = $"[age] >= 0";
                    ageCondition = "";
                }
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ageTB_To_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AgeCB.Checked)
                {
                    eventsData.DefaultView.RowFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
                    ageCondition = $" and age(c.birthDate, current_date()) BETWEEN {ageTB_From.Text} AND {ageTB_To.Text}";
                }
                else
                {
                    eventsData.DefaultView.RowFilter = $"[age] >= 0";
                    ageCondition = "";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEventStrip_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (AddEventForm addForm = new AddEventForm(0))
                {
                    addForm.ShowDialog();
                }

                LoadEvent_Area();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (e.KeyData)
                {
                    case (Keys.S | Keys.Control | Keys.Shift):
                        {
                            using (SettingsForm settingsForm = new SettingsForm())
                            {
                                settingsForm.FormClosed += new FormClosedEventHandler(SettingsForm_Closed);
                                settingsForm.ShowDialog();
                            }
                            break;
                        }
                    case (Keys.Q | Keys.Shift | Keys.Control):
                        {
                            this.Close();
                            break;
                        }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip settingsTip = new ToolTip();

                settingsTip.AutoPopDelay = 2000;
                settingsTip.InitialDelay = 1000;
                settingsTip.ReshowDelay = 500;
                settingsTip.ShowAlways = true;
                settingsTip.SetToolTip(statusStrip1, "Для открытия настроек необходимо нажать следующее сочетание клавишь: Ctrl+Shift+S");
                SelectFilter();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.ExcelClass.ExportExcelData(eventsData.DefaultView);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WordExportBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DateTime[] dateTimes = new DateTime[2];
                if (!TreatmentCB.Checked)
                {
                    dateTimes = Classes.EventClass.SetMinMaxDate();

                    DataTable wordExport = new DataTable();
                    DataTable textExport = new DataTable();
                    DataTable classExport = new DataTable();
                    DataTable chldReport = new DataTable();

                    wordExport = Classes.EventClass.LoadDataToReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    textExport = Classes.EventClass.ReportWord(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    classExport = Classes.EventClass.LoadDataToTableReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    chldReport = Classes.EventClass.LoadDataToCHLDReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, HealthStatusCondition);

                    Classes.WordClass.ExportWordData(wordExport, textExport, classExport, dateTimes[0], dateTimes[1], chldReport);
                }
                else
                {
                    DataTable wordExport = new DataTable();
                    DataTable textExport = new DataTable();
                    DataTable classExport = new DataTable();
                    DataTable chldReport = new DataTable();

                    wordExport = Classes.EventClass.LoadDataToReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    textExport = Classes.EventClass.ReportWord(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    classExport = Classes.EventClass.LoadDataToTableReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, relaxCondition, medHelpCondition, HealthStatusCondition);
                    chldReport = Classes.EventClass.LoadDataToCHLDReport(areaCondition, medOrgCondition, eventDateCondition, DOOCondition, ageCondition, TreatmentDateCondition, HealthStatusCondition);

                    Classes.WordClass.ExportWordData(wordExport, textExport, classExport, TreatmentDateDP_From.Value, TreatmentDateDP_To.Value, chldReport);
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DuplicateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (DuplicateForm dupForm = new DuplicateForm())
                {
                    dupForm.ShowDialog();
                }
                LoadEvent_Area();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteEvent_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.EventClass.RemoveEvent(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()));
                LoadEvent_Area();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditEventStrip_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (AddEventForm addForm = new AddEventForm(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                {
                    addForm.ShowDialog();
                }

                LoadEvent_Area();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
