using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MedHelp_dotNet
{
    public partial class MainForm : Form
    {
        bool firstStart = true;

        public XmlDocument xmlDoc;
        DataTable eventsData = new DataTable();
        Classes.AreaClass[] areas = Classes.AreaClass.LoadListArea();
        Classes.MOClass[] medOrgN;
        Classes.HealthOrgClass[] HealthOrg;
        int cntCheckRelax = 4;
        int cntCheckHelp = 3;
        string str = "'Организованный отдых (Самостоятельно)', 'Организованный отдых (По путевке Мать и дитя)', 'Неорганизованный отдых (Самостоятельно)', 'Неорганизованный отдых (С законным представителем)'";
        string strH = "'Первичная медико-санитарная помощь (поликлиника-педитр)', 'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)', 'Специализированная медицинская помощь (стационар)'";

        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            LoadEvent_Area();

            cbHealthStatus.DataSource = null;
            cbHealthStatus.DataSource = Classes.HealthStatusClass.LoadHealthStatus();

            firstStart = false;
            KeyPreview = true;

            Server.Text = $"Сервер: {Properties.Settings.Default.server}";
            DataBase.Text = $"База данных: {Properties.Settings.Default.dataBase}";
        }

        private void SettingsForm_Closed(object sender, FormClosedEventArgs e)
        {
            Server.Text = $"Сервер: {Properties.Settings.Default.server}"; //Отображение адреса сервера(внизу формы)
            DataBase.Text = $"База: {Properties.Settings.Default.dataBase}"; //Отображение БД(с которой работает пользователь)
        }

        private void LoadEvent_Area()
        {
            eventsData = Classes.EventClass.LoadEvent();
            dataGridView1.DataSource = eventsData;

            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
            cbArea.DataSource = areas;
            cbArea.SelectedIndex = -1;
        }

        private void AreaCB_CheckedChanged(object sender, EventArgs e)
        {
            if (AreaCB.Checked)
            {
                cbArea.Enabled = true;
                if (cbArea.SelectedItem != null) eventsData.DefaultView.RowFilter = string.Format($"Convert([areaName], System.String) = '{areas[cbArea.SelectedIndex].name}'");
            }
            else
            {
                cbArea.Enabled = false;
                eventsData.DefaultView.RowFilter = string.Format($"Convert([areaName], System.String) like '%'");
            }
        }

        private void medOrgCB_CheckedChanged(object sender, EventArgs e)
        {
            if (medOrgCB.Checked)
            {
                cbMedOrg.Enabled = true;
                if (cbMedOrg.SelectedItem != null) eventsData.DefaultView.RowFilter = string.Format($"Convert([medOrgName], System.String) = '{medOrgN[cbMedOrg.SelectedIndex].name}'");
            }
            else
            {
                cbMedOrg.Enabled = false;
                eventsData.DefaultView.RowFilter = string.Format($"Convert([medOrgName], System.String) like '%'");
            }
        }

        private void eventDateCB_CheckedChanged(object sender, EventArgs e)
        {
            if (eventDateCB.Checked)
            {
                eventDate_From.Enabled = true;
                eventDate_To.Enabled = true;
                label1.Enabled = true;
                label2.Enabled = true;
            }
            else
            {
                eventDate_From.Enabled = false;
                eventDate_To.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
            }
        }

        private void DOOCB_CheckedChanged(object sender, EventArgs e)
        {
            if (DOOCB.Checked)
            {
                cbDOO.Enabled = true;
                if (cbDOO.SelectedItem != null) eventsData.DefaultView.RowFilter = string.Format($"Convert([DOOName], System.String) = '{HealthOrg[cbDOO.SelectedIndex].FullName}'");
            }
            else
            {
                cbDOO.Enabled = false;
                eventsData.DefaultView.RowFilter = string.Format($"Convert([DOOName], System.String) like '%'");
            }
        }

        private void ClientFIOCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ClientFIOCB.Checked)
            {
                ClientFIOTB.Enabled = true;
                if (ClientFIOTB.Text.Length != 0) eventsData.DefaultView.RowFilter = string.Format($"Convert([ClientFIO], System.String) like '{ClientFIOTB.Text}%'");
            }
            else
            {
                ClientFIOTB.Enabled = false;
                eventsData.DefaultView.RowFilter = string.Format($"Convert([ClientFIO], System.String) like '%'");
            }
        }

        private void AgeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (AgeCB.Checked)
            {
                label3.Enabled = true;
                ageTB_From.Enabled = true;
                label4.Enabled = true;
                ageTB_To.Enabled = true;
                if (ageTB_From.Text.Length > 0 && ageTB_To.Text.Length > 0) eventsData.DefaultView.RowFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
                else eventsData.DefaultView.RowFilter = $"[age] >= 0";
            }
            else
            {
                label3.Enabled = false;
                ageTB_From.Enabled = false;
                label4.Enabled = false;
                ageTB_To.Enabled = false;
                eventsData.DefaultView.RowFilter = $"[age] >= 0";
            }
        }

        private void TreatmentCB_CheckedChanged(object sender, EventArgs e)
        {
            if (TreatmentCB.Checked)
            {
                label5.Enabled = true;
                TreatmentDateDP_From.Enabled = true;
                label6.Enabled = true;
                TreatmentDateDP_To.Enabled = true;
            }
            else
            {
                label5.Enabled = false;
                TreatmentDateDP_From.Enabled = false;
                label6.Enabled = false;
                TreatmentDateDP_To.Enabled = false;
            }
        }

        private void TransfertedCB_CheckedChanged(object sender, EventArgs e)
        {
            if (TransfertedCB.Checked)
            {
                TransfertedTB.Enabled = true;
                if (TransfertedTB.Text.Length > 0) eventsData.DefaultView.RowFilter = $"[TransfertedDepart] like '{TransfertedTB.Text}%'";
            }
            else
            {
                TransfertedTB.Enabled = false;
                eventsData.DefaultView.RowFilter = $"[TransfertedDepart] like '%'";
            }
        }

        private void HealthStatusCB_CheckedChanged(object sender, EventArgs e)
        {
            if (HealthStatusCB.Checked)
            {
                cbHealthStatus.Enabled = true;
                if (cbHealthStatus.SelectedItem != null) eventsData.DefaultView.RowFilter = $"[HealthStatus] = '{cbHealthStatus.SelectedItem.ToString()}'";
            }
            else
            {
                cbHealthStatus.Enabled = false;
                eventsData.DefaultView.RowFilter = $"[HealthStatus] like '%'";
            }
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (cbArea.SelectedItem != null)
                {
                    eventsData.DefaultView.RowFilter = $"[areaName] like '{areas[cbArea.SelectedIndex].name}'";//string.Format($"Convert([areaName], System.String) like '{areas[cbArea.SelectedIndex].name}'");
                }
                else eventsData.DefaultView.RowFilter = string.Format($"Convert([areaName], System.String) like '%'");
            }
        }

        private void cbMedOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (cbMedOrg.SelectedItem != null)
                {
                    eventsData.DefaultView.RowFilter = $"[medOrgName] like '{medOrgN[cbMedOrg.SelectedIndex].name}'";//string.Format($"Convert([medOrgName], System.String) = '{medOrgN[cbMedOrg.SelectedIndex].name}'");
                }
                else eventsData.DefaultView.RowFilter = $"[medOrgName] like '%'";
            }
        }

        private void cbDOO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (cbDOO.SelectedItem != null)
                {
                    eventsData.DefaultView.RowFilter = $"[DOOName] = '{HealthOrg[cbDOO.SelectedIndex].FullName}'";//string.Format($"Convert([DOOName], System.String) = '{HealthOrg[cbDOO.SelectedIndex].FullName}'");
                }
                else eventsData.DefaultView.RowFilter = $"[DOOName] like '%'";
            }
        }

        private void ClientFIOTB_TextChanged(object sender, EventArgs e)
        {
            if (ClientFIOCB.Checked) eventsData.DefaultView.RowFilter = $"[ClientFIO] like '{ClientFIOTB.Text}%'";//string.Format($"Convert([ClientFIO], System.String) like '{ClientFIOTB.Text}%'");
            else eventsData.DefaultView.RowFilter = $"[ClientFIO] like '%'";
        }

        private void cbMedOrg_DropDown(object sender, EventArgs e)
        {
            if (AreaCB.Checked) medOrgN = Classes.MOClass.LoadMOList(int.Parse(cbArea.SelectedValue.ToString()));
            else medOrgN = Classes.MOClass.LoadMOList();
            
            cbMedOrg.DataSource = medOrgN;
            cbMedOrg.DisplayMember = "name";
            cbMedOrg.ValueMember = "id";
        }

        private void cbDOO_DropDown(object sender, EventArgs e)
        {
            if (AreaCB.Checked) HealthOrg = Classes.HealthOrgClass.LoadHealthOrgList(int.Parse(cbArea.SelectedValue.ToString()));
            else HealthOrg = Classes.HealthOrgClass.LoadHealthOrgList();

            cbDOO.DataSource = HealthOrg;
            cbDOO.DisplayMember = "ShortName";
            cbDOO.ValueMember = "id";
        }

        private void eventDate_From_ValueChanged(object sender, EventArgs e)
        {
            if (eventDateCB.Checked) eventsData.DefaultView.RowFilter = $"[eventDate] >= '{eventDate_From.Value.ToString("dd.MM.yyyy")}' and [eventDate] <= '{eventDate_To.Value.ToString("dd.MM.yyyy")}'";
            else eventsData.DefaultView.RowFilter = string.Format($"[eventDate] like '%'");
        }

        private void eventDate_To_ValueChanged(object sender, EventArgs e)
        {
            if (eventDateCB.Checked) eventsData.DefaultView.RowFilter = $"[eventDate] >= '{eventDate_From.Value.ToString("dd.MM.yyyy")}' and [eventDate] <= '{eventDate_To.Value.ToString("dd.MM.yyyy")}'";
            else eventsData.DefaultView.RowFilter = string.Format($"[eventDate] like '%'");
        }

        private void TreatmentDateDP_From_ValueChanged(object sender, EventArgs e)
        {
            if (TreatmentCB.Checked) eventsData.DefaultView.RowFilter = $"[TreatmentDate] >= '{TreatmentDateDP_From.Value.ToString("dd.MM.yyyy")}' and [TreatmentDate] <= '{TreatmentDateDP_To.Value.ToString("dd.MM.yyyy")}'";
            else eventsData.DefaultView.RowFilter = $"[eventDate] like '%'";
        }

        private void TreatmentDateDP_To_ValueChanged(object sender, EventArgs e)
        {
            if (TreatmentCB.Checked) eventsData.DefaultView.RowFilter = $"[TreatmentDate] >= '{TreatmentDateDP_From.Value.ToString("dd.MM.yyyy")}' and [TreatmentDate] <= '{TreatmentDateDP_To.Value.ToString("dd.MM.yyyy")}'";
            else eventsData.DefaultView.RowFilter = $"[eventDate] like '%'";
        }

        private void TransfertedTB_TextChanged(object sender, EventArgs e)
        {
            if (TransfertedCB.Checked) eventsData.DefaultView.RowFilter = $"[TransfertedDepart] like '{TransfertedTB.Text}%'";
            else eventsData.DefaultView.RowFilter = $"[TransfertedDepart] like '%'";
        }

        private void cbHealthStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HealthStatusCB.Checked) eventsData.DefaultView.RowFilter = $"[HealthStatus] = '{cbHealthStatus.SelectedItem.ToString()}'";
        }

        private void RelaxInfo_Child2_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckRelaxInfo(RelaxInfo_Child2);
        }

        private void RelaxInfo_Child3_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckRelaxInfo(RelaxInfo_Child3);
        }

        private void RelaxInfo_Child5_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckRelaxInfo(RelaxInfo_Child5);
        }

        private void RelaxInfo_Child6_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckRelaxInfo(RelaxInfo_Child6);
        }

        private string CheckRelaxInfo(CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (cntCheckRelax == 0)
                {
                    switch(checkBox.Name)
                    {
                        case "RelaxInfo_Child2":
                            cntCheckRelax++;
                            str = "'Организованный отдых (Самостоятельно)'";
                            break;
                        case "RelaxInfo_Child3":
                            cntCheckRelax++;
                            str = "'Организованный отдых (По путевке Мать и дитя)'";
                            break;
                        case "RelaxInfo_Child5":
                            cntCheckRelax++;
                            str = "'Неорганизованный отдых (Самостоятельно)'";
                            break;
                        case "RelaxInfo_Child6":
                            cntCheckRelax++;
                            str = "'Неорганизованный отдых (С законным представителем)'";
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
                            break;
                        case "RelaxInfo_Child3":
                            cntCheckRelax++;
                            str += ", 'Организованный отдых (По путевке Мать и дитя)'";
                            break;
                        case "RelaxInfo_Child5":
                            cntCheckRelax++;
                            str += ", 'Неорганизованный отдых (Самостоятельно)'";
                            break;
                        case "RelaxInfo_Child6":
                            cntCheckRelax++;
                            str += ", 'Неорганизованный отдых (С законным представителем)'";
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
                                if (cntCheckRelax != 0) str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0), "'Организованный отдых (Самостоятельно)', ".Length);
                                else str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0), "'Организованный отдых (Самостоятельно)'".Length);
                            }
                            else str = str.Remove(str.IndexOf("'Организованный отдых (Самостоятельно)'", 0) - 2, ", 'Организованный отдых (Самостоятельно)'".Length);
                            break;
                        case "RelaxInfo_Child3":
                            cntCheckRelax--;
                            if (str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0) == 0)
                            {
                                if (cntCheckRelax != 0) str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0), "'Организованный отдых (По путевке Мать и дитя)', ".Length);
                                else str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0), "'Организованный отдых (По путевке Мать и дитя)'".Length);
                            }
                            else str = str.Remove(str.IndexOf("'Организованный отдых (По путевке Мать и дитя)'", 0) - 2, ", 'Организованный отдых (По путевке Мать и дитя)'".Length);
                            break;
                        case "RelaxInfo_Child5":
                            cntCheckRelax--;
                            if (str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0) == 0)
                            {
                                if (cntCheckRelax != 0) str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0), "'Неорганизованный отдых (Самостоятельно)', ".Length);
                                else str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0), "'Неорганизованный отдых (Самостоятельно)'".Length);
                            }
                            else str = str.Remove(str.IndexOf("'Неорганизованный отдых (Самостоятельно)'", 0) - 2, ", 'Неорганизованный отдых (Самостоятельно)'".Length);
                            break;
                        case "RelaxInfo_Child6":
                            cntCheckRelax--;
                            if (str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0) == 0)
                            {
                                if (cntCheckRelax != 0) str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0), "'Неорганизованный отдых (С законным представителем)', ".Length);
                                else str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0), "'Неорганизованный отдых (С законным представителем)'".Length);
                            }
                            else str = str.Remove(str.IndexOf("'Неорганизованный отдых (С законным представителем)'", 0) - 2, ", 'Неорганизованный отдых (С законным представителем)'".Length);
                            break;
                    }
                }
            }
            string condition = "";
            
            if (str != "") condition = $"[relaxName] in ({str}) ";
            else condition = $"[relaxName] is null";

            return condition;
        }

        private string CheckHelp(CheckBox checkBox)
        {
            if (checkBox.Checked)
            {
                if (cntCheckHelp == 0)
                {
                    switch (checkBox.Name)
                    {
                        case "PMSMP":
                            cntCheckHelp++;
                            strH = "'Первичная медико-санитарная помощь (поликлиника-педитр)'";
                            break;
                        case "PSMSP":
                            cntCheckHelp++;
                            strH = "'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'";
                            break;
                        case "SMP":
                            cntCheckHelp++;
                            strH = "'Специализированная медицинская помощь (стационар)'";
                            break;
                    }
                }
                else
                {
                    switch (checkBox.Name)
                    {
                        case "PMSMP":
                            cntCheckHelp++;
                            strH += ", 'Первичная медико-санитарная помощь (поликлиника-педитр)'";
                            break;
                        case "PSMSP":
                            cntCheckHelp++;
                            strH += ", 'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'";
                            break;
                        case "SMP":
                            cntCheckHelp++;
                            strH += ", 'Специализированная медицинская помощь (стационар)'";
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
                            if (strH.IndexOf("'Первичная медико-санитарная помощь (поликлиника-педитр)'", 0) == 0)
                            {
                                if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("'Первичная медико-санитарная помощь (поликлиника-педитр)'", 0), "'Первичная медико-санитарная помощь (поликлиника-педитр)', ".Length);
                                else strH = strH.Remove(strH.IndexOf("'Первичная медико-санитарная помощь (поликлиника-педитр)'", 0), "'Первичная медико-санитарная помощь (поликлиника-педитр)'".Length);
                            }
                            else strH = strH.Remove(strH.IndexOf("'Первичная медико-санитарная помощь (поликлиника-педитр)'", 0) - 2, ", 'Первичная медико-санитарная помощь (поликлиника-педитр)'".Length);
                            break;
                        case "PSMSP":
                            cntCheckHelp--;
                            if (strH.IndexOf("'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'", 0) == 0)
                            {
                                if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'", 0), "'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)', ".Length);
                                else strH = strH.Remove(strH.IndexOf("'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'", 0), "'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'".Length);
                            }
                            else strH = strH.Remove(strH.IndexOf("'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'", 0) - 2, ", 'Первичная специализированная медико-санитарная помощь (поликлиника-узкий специалист)'".Length);
                            break;
                        case "SMP":
                            cntCheckHelp--;
                            if (strH.IndexOf("'Специализированная медицинская помощь (стационар)'", 0) == 0)
                            {
                                if (cntCheckHelp != 0) strH = strH.Remove(strH.IndexOf("'Специализированная медицинская помощь (стационар)'", 0), "'Специализированная медицинская помощь (стационар)', ".Length);
                                else strH = strH.Remove(strH.IndexOf("'Специализированная медицинская помощь (стационар)'", 0), "'Специализированная медицинская помощь (стационар)'".Length);
                            }
                            else strH = strH.Remove(strH.IndexOf("'Специализированная медицинская помощь (стационар)'", 0) - 2, ", 'Специализированная медицинская помощь (стационар)'".Length);
                            break;
                    }
                }
            }
            string condition = "";

            if (strH != "") condition = $"[HelpName] in ({strH}) ";
            else condition = $"[HelpName] is null";

            return condition;
        }

        private void PMSMP_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckHelp(PMSMP);
        }

        private void PSMSP_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckHelp(PSMSP);
        }

        private void SMP_CheckedChanged(object sender, EventArgs e)
        {
            eventsData.DefaultView.RowFilter = CheckHelp(SMP);
        }

        private void ageTB_From_TextChanged(object sender, EventArgs e)
        {
            if (AgeCB.Checked) eventsData.DefaultView.RowFilter = eventsData.DefaultView.RowFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
            else eventsData.DefaultView.RowFilter = $"[age] >= 0";
        }

        private void ageTB_To_TextChanged(object sender, EventArgs e)
        {
            if (AgeCB.Checked) eventsData.DefaultView.RowFilter = $"[age] >= {ageTB_From.Text} and [age] <= {ageTB_To.Text}";
            else eventsData.DefaultView.RowFilter = $"[age] >= 0";
        }

        private void AddEventStrip_Click(object sender, EventArgs e)
        {
            using (AddEventForm addForm = new AddEventForm())
            {
                addForm.ShowDialog();
            }

            LoadEvent_Area();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.S | Keys.Control| Keys.Shift):
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Classes.ExcelClass.ExportExcelData(eventsData.DefaultView);
        }

        private void WordExportBTN_Click(object sender, EventArgs e)
        {
            Classes.WordClass.ExportWordData();
        }
    }
}
