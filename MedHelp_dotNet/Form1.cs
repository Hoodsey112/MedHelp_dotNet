using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MedHelp_dotNet
{
    public partial class AddEventForm : Form
    {
        public XmlDocument xmlDoc;
        Classes.AreaClass[] areaClass;
        Classes.MOClass[] moClass;
        Classes.HealthOrgClass[] healthClass;
        Classes.MKBClass[] MKB;
        public Classes.ClientClass client;
        bool firstStart = true;
        int RelaxInfo = 0;
        int HelpName = 0;
        
        public AddEventForm()
        {
            InitializeComponent();
            LoadHealthStatus();
            LoadArea();
            
            //----------------------------------
            MKB = Classes.MKBClass.LoadMKB();//|
            MKB10TB.DisplayMember = "DiagID";//|
            MKB10TB.ValueMember = "DiagName";//|
            MKB10TB.DataSource = MKB;        //|
            MKB10TB.SelectedIndex = -1;      //|
            //----------------------------------

            firstStart = false;
        }

        private void AddStatus_Click(object sender, EventArgs e)
        {
            Classes.HealthStatusClass.InsertHealthStatus(cbHealthStatus.Text);
        }

        private void RemoveStatus_Click(object sender, EventArgs e)
        {
            Classes.HealthStatusClass.RemoveHealthStatus(cbHealthStatus.SelectedIndex);
        }

        private void LoadHealthStatus()
        {
            cbHealthStatus.DataSource = null;
            cbHealthStatus.DataSource = Classes.HealthStatusClass.LoadHealthStatus();
        }

        #region Area
        //Загрузка списка районов
        private void LoadArea()
        {
            areaClass = Classes.AreaClass.LoadListArea();
            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
            cbArea.DataSource = areaClass;
            cbArea.SelectedIndex = -1;
        }

        //Добавление нового района в таблицу
        private void InsertArea()
        {
            Classes.AreaClass.InsertArea(cbArea.Text);
        }

        //Удаление выбранного района из таблицы
        private void RemoveAreaList()
        {
            Classes.AreaClass.RemoveArea(int.Parse(cbArea.SelectedValue.ToString()));
        }

        //Обработка кнопки добавить район
        private void AddArea_Click(object sender, EventArgs e)
        {
            InsertArea();
            LoadArea();
        }

        //Обработка кнопки удалить район
        private void RemoveArea_Click(object sender, EventArgs e)
        {
            RemoveAreaList();
            LoadArea();
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstStart)
            {
                if (cbArea.SelectedIndex != -1)
                {
                    LoadMOList();
                    LoadHealthOrgList();
                }
            }
        }
        #endregion

        #region MO
        //Получение списка МО согласно выбранному району
        private void LoadMOList()
        {
            moClass = Classes.MOClass.LoadMOList(int.Parse(cbArea.SelectedValue.ToString()));
            cbMO.ValueMember = "id";
            cbMO.DisplayMember = "name";
            cbMO.DataSource = moClass;
            cbMO.SelectedIndex = -1;
        }

        private void LoadHealthOrgList()
        {
            healthClass = Classes.HealthOrgClass.LoadHealthOrgList(int.Parse(cbArea.SelectedValue.ToString()));
            cbShortNameOrg.ValueMember = "id";
            cbShortNameOrg.DisplayMember = "ShortName";
            cbShortNameOrg.DataSource = healthClass;
            cbShortNameOrg.SelectedIndex = -1;
        }

        //Добавление новой МО в таблицу
        private void AddMO_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedItem != null)
            {
                Classes.MOClass.InsertNewMO(cbMO.Text, int.Parse(cbArea.SelectedValue.ToString()));
                LoadMOList();
            }
            else MessageBox.Show("Необходимо выбрать район, для добавления новой медицинской организации", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Удаление выбранного МО из таблицы
        private void RemoveMO_Click(object sender, EventArgs e)
        {
            Classes.MOClass.RemoveMO(int.Parse(cbMO.SelectedValue.ToString()), int.Parse(cbArea.SelectedValue.ToString()));
            LoadMOList();
        }
        #endregion

        private void AddOrg_Click(object sender, EventArgs e)
        {
            using (HealthOrgForm hoForm = new HealthOrgForm(false, 0, 0))
            {
                hoForm.ShowDialog();
            }
            LoadHealthOrgList();
        }

        private void EditOrg_Click(object sender, EventArgs e)
        {
            using (HealthOrgForm hoForm = new HealthOrgForm(true, int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbShortNameOrg.SelectedValue.ToString())))
            {
                hoForm.ShowDialog();
            }
            LoadHealthOrgList();
        }

        private void HelthOrgMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (cbShortNameOrg.SelectedItem != null) AddOrg.Enabled = false;
            else
            {
                AddOrg.Enabled = true;
                EditOrg.Enabled = false;
                RemoveOrg.Enabled = false;
            }
        }

        private void AddClient_Click(object sender, EventArgs e)
        {
            using (ClientForm cForm = new ClientForm(1))
            {
                client = new Classes.ClientClass();
                cForm.Owner = this;
                cForm.ShowDialog();
            }

            if (client != null)
            {
                ClientFIOTB.Text = client.FIO;
                ClientBirthDate.Value = client.birthDate;
                cbClientSex.SelectedItem = client.sex;
                ClientAddressTB.Text = client.Address;
            }
        }

        private void FoundClient_Click(object sender, EventArgs e)
        {
            using (ClientForm cForm = new ClientForm(2))
            {
                client = new Classes.ClientClass();
                cForm.Owner = this;
                cForm.ShowDialog();
            }

            if (client != null)
            {
                ClientFIOTB.Text = client.FIO;
                ClientBirthDate.Value = client.birthDate;
                cbClientSex.SelectedItem = client.sex;
                ClientAddressTB.Text = client.Address;
            }
        }

        private void ORGHimselfRelaxInfoRB_CheckedChanged(object sender, EventArgs e)
        {
            RelaxInfo = 2;
        }

        private void ORGMCRelaxInfoRB_CheckedChanged(object sender, EventArgs e)
        {
            RelaxInfo = 3;
        }

        private void NONORGHimselfRelaxInfoRB_CheckedChanged(object sender, EventArgs e)
        {
            RelaxInfo = 5;
        }

        private void NONORGWithParentRelaxInfoRB_CheckedChanged(object sender, EventArgs e)
        {
            RelaxInfo = 6;
        }

        private void AddBTN_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedItem != null)
            {
                if (cbMO.SelectedItem != null)
                {
                    if (cbShortNameOrg.SelectedItem != null)
                    {
                        if (client != null)
                        {
                            if (RelaxInfo != 0)
                            {
                                if (HelpName != 0)
                                {
                                    if (TransfertedCheck.Checked)
                                    {
                                        if (TransferTB.Text != "")
                                        {
                                            if (cbHealthStatus.SelectedItem != null)
                                            {
                                                Classes.EventClass.InsertEvent(int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, MKB[MKB10TB.SelectedIndex].DiagName, MKB[MKB10TB.SelectedIndex].DiagID, SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                                MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else MessageBox.Show("Вы не указали состояние ребенка по степени тяжести", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        else MessageBox.Show("Вы не указали информацию о направлении(переводе)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    else
                                    {
                                        if (cbHealthStatus.SelectedItem != null)
                                        {
                                            Classes.EventClass.InsertEvent(int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagName : DiagTB.Text, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagID : "", SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                            MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else MessageBox.Show("Вы не указали состояние ребенка по степени тяжести", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else MessageBox.Show("Вы не указали информацию об оказанной помощи", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else MessageBox.Show("Вы не указали информацию об отдыхе", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else MessageBox.Show("Вы не указали данные ребенка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else MessageBox.Show("Вы не выбрали ДОО", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else MessageBox.Show("Вы не выбрали МО", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else MessageBox.Show("Вы не выбрали район","Внимание",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void HelpRB_1_CheckedChanged(object sender, EventArgs e)
        {
            if(HelpRB_1.Checked)
            {
                DiagTB.Enabled = true;
                MKB10TB.Enabled = true;
                SpecialityTB.Enabled = false;
                DepartmentTB.Enabled = false;
                HelpName = 1;
            }
        }

        private void HelpRB_2_CheckedChanged(object sender, EventArgs e)
        {
            if (HelpRB_2.Checked)
            {
                DiagTB.Enabled = true;
                MKB10TB.Enabled = true;
                SpecialityTB.Enabled = true;
                DepartmentTB.Enabled = false;
                HelpName = 2;
            }
        }

        private void HelpRB_3_CheckedChanged(object sender, EventArgs e)
        {
            if (HelpRB_3.Checked)
            {
                DiagTB.Enabled = true;
                MKB10TB.Enabled = true;
                SpecialityTB.Enabled = false;
                DepartmentTB.Enabled = true;
                HelpName = 3;
            }
        }

        private void MKB10TB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MKB10TB.Enabled && DiagTB.Enabled)
            {
                if (MKB10TB.SelectedItem != null) DiagTB.Text = MKB10TB.SelectedValue.ToString();
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransfertedCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TransfertedCheck.Checked)
            {
                TransferTB.Enabled = true;
                TransferDate.Enabled = true;
            }
            else
            {
                TransferTB.Enabled = false;
                TransferDate.Enabled = false;
            }
        }
    }
}
