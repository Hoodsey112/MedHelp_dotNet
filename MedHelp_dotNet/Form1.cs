using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using NLog;

namespace MedHelp_dotNet
{
    public partial class AddEventForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public XmlDocument xmlDoc;
        Classes.AreaClass[] areaClass;
        Classes.MOClass[] moClass;
        Classes.HealthOrgClass[] healthClass;
        Classes.MKBClass[] MKB;
        System.Data.DataTable editData;
        //List<Classes.MKBClass> MKB = new List<Classes.MKBClass>();

        public Classes.ClientClass client;
        bool firstStart = true;
        bool update = false;
        int RelaxInfo = 0;
        int HelpName = 0;
        int event_id = 0;

        public AddEventForm(int _event_id)
        {
            try
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
                if (_event_id != 0)
                {
                    update = true;
                    event_id = _event_id;
                    EditData(_event_id);
                    AddBTN.Text = "Сохранить";
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditData(int event_id)
        {
            editData = Classes.EventClass.EditEvent(event_id);
            client = Classes.ClientClass.LoadClientData(int.Parse(editData.Rows[0][5].ToString()));

            if (client.FIO != null)
            {
                ClientFIOTB.Text = client.FIO;
                ClientBirthDate.Value = client.birthDate;
                cbClientSex.SelectedItem = client.sex;
                ClientAddressTB.Text = client.Address;
            }

            cbArea.Text = editData.Rows[0][1].ToString();
            cbMO.Text = editData.Rows[0][2].ToString();
            EventDate.Value = DateTime.Parse(editData.Rows[0][3].ToString());
            cbShortNameOrg.Text = editData.Rows[0][4].ToString();
            switch(editData.Rows[0][6].ToString())
            {
                case "2":
                    ORGHimselfRelaxInfoRB.Checked = true;
                    break;
                case "3":
                    ORGMCRelaxInfoRB.Checked = true;
                    break;
                case "5":
                    NONORGHimselfRelaxInfoRB.Checked = true;
                    break;
                case "6":
                    NONORGWithParentRelaxInfoRB.Checked = true;
                    break;
            }
            TreatmentDate.Value = DateTime.Parse(editData.Rows[0][7].ToString());

            switch(editData.Rows[0][8].ToString())
            {
                case "1":
                    HelpRB_1.Checked = true;

                    MKB10TB.Text = editData.Rows[0][10].ToString();
                    DiagTB.Text = editData.Rows[0][9].ToString();
                    /*
                    DiagTB.Enabled = true;
                    MKB10TB.Enabled = true;
                    SpecialityTB.Enabled = false;
                    DepartmentTB.Enabled = false;
                     */
                    break;
                case "2":
                    HelpRB_2.Checked = true;

                    MKB10TB.Text = editData.Rows[0][10].ToString();
                    DiagTB.Text = editData.Rows[0][9].ToString();
                    SpecialityTB.Text = editData.Rows[0][11].ToString();
                    /*
                    DiagTB.Enabled = true;
                    MKB10TB.Enabled = true;
                    SpecialityTB.Enabled = true;
                    DepartmentTB.Enabled = false;
                    */
                    break;
                case "3":
                    HelpRB_3.Checked = true;

                    MKB10TB.Text = editData.Rows[0][10].ToString();
                    DiagTB.Text = editData.Rows[0][9].ToString();
                    DepartmentTB.Text = editData.Rows[0][12].ToString();
                    /*
                    DiagTB.Enabled = true;
                    MKB10TB.Enabled = true;
                    SpecialityTB.Enabled = false;
                    DepartmentTB.Enabled = true;
                     */
                    break;
            }

            cbHealthStatus.Text = editData.Rows[0][15].ToString();

            if (editData.Rows[0][13].ToString() != "")
            {
                TransfertedCheck.Checked = true;
                TransferTB.Text = editData.Rows[0][13].ToString();
                TransferDate.Value = DateTime.Parse(editData.Rows[0][14].ToString());
            }
        }

        private void AddStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.HealthStatusClass.InsertHealthStatus(cbHealthStatus.Text);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.HealthStatusClass.RemoveHealthStatus(cbHealthStatus.SelectedIndex);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHealthStatus()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbHealthStatus.DataSource = null;
                cbHealthStatus.DataSource = Classes.HealthStatusClass.LoadHealthStatus();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAll()
        {
            cbArea.SelectedIndex = -1;
            cbMO.SelectedIndex = -1;
            cbShortNameOrg.SelectedIndex = -1;
            EventDate.Value = DateTime.Now;
            ClientFIOTB.Clear();
            ClientAddressTB.Clear();
            ClientBirthDate.Value = DateTime.Now;
            cbClientSex.SelectedIndex = -1;

            foreach (RadioButton radioButton in groupBox4.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) radioButton.Checked = false;
            }

            TreatmentDate.Value = DateTime.Now;
            if (TransfertedCheck.Checked)
            {
                TransferTB.Clear();
                TransferDate.Value = DateTime.Now;
                TransfertedCheck.Checked = false;
            }

            foreach (RadioButton radioButton in groupBox8.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) radioButton.Checked = false;
            }

            DiagTB.Clear();
            MKB10TB.SelectedIndex = -1;
            SpecialityTB.Clear();
            DepartmentTB.Clear();
            cbHealthStatus.SelectedIndex = -1;

            MKB10TB.Enabled = false;
            DiagTB.Enabled = false;
            SpecialityTB.Enabled = false;
            DepartmentTB.Enabled = false;
        }

        #region Area
        //Загрузка списка районов
        private void LoadArea()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                areaClass = Classes.AreaClass.LoadListArea();
                cbArea.DisplayMember = "name";
                cbArea.ValueMember = "id";
                cbArea.DataSource = areaClass;
                cbArea.SelectedIndex = -1;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Добавление нового района в таблицу
        private void InsertArea()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.AreaClass.InsertArea(cbArea.Text);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Удаление выбранного района из таблицы
        private void RemoveAreaList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.AreaClass.RemoveArea(int.Parse(cbArea.SelectedValue.ToString()));
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработка кнопки добавить район
        private void AddArea_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                InsertArea();
                LoadArea();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Обработка кнопки удалить район
        private void RemoveArea_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                RemoveAreaList();
                LoadArea();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region MO
        //Получение списка МО согласно выбранному району
        private void LoadMOList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                moClass = Classes.MOClass.LoadMOList(int.Parse(cbArea.SelectedValue.ToString()));
                cbMO.ValueMember = "id";
                cbMO.DisplayMember = "name";
                cbMO.DataSource = moClass;
                cbMO.SelectedIndex = -1;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHealthOrgList()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                healthClass = Classes.HealthOrgClass.LoadHealthOrgList(int.Parse(cbArea.SelectedValue.ToString()));
                cbShortNameOrg.ValueMember = "id";
                cbShortNameOrg.DisplayMember = "ShortName";
                cbShortNameOrg.DataSource = healthClass;
                cbShortNameOrg.SelectedIndex = -1;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Добавление новой МО в таблицу
        private void AddMO_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (cbArea.SelectedItem != null)
                {
                    Classes.MOClass.InsertNewMO(cbMO.Text, int.Parse(cbArea.SelectedValue.ToString()));
                    LoadMOList();
                }
                else MessageBox.Show("Необходимо выбрать район, для добавления новой медицинской организации", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Удаление выбранного МО из таблицы
        private void RemoveMO_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.MOClass.RemoveMO(int.Parse(cbMO.SelectedValue.ToString()), int.Parse(cbArea.SelectedValue.ToString()));
                LoadMOList();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void AddOrg_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (HealthOrgForm hoForm = new HealthOrgForm(false, 0, 0))
                {
                    hoForm.ShowDialog();
                }
                LoadHealthOrgList();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditOrg_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (HealthOrgForm hoForm = new HealthOrgForm(true, int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbShortNameOrg.SelectedValue.ToString())))
                {
                    hoForm.ShowDialog();
                }
                LoadHealthOrgList();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HelthOrgMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (cbShortNameOrg.SelectedItem != null) AddOrg.Enabled = false;
                else
                {
                    AddOrg.Enabled = true;
                    EditOrg.Enabled = false;
                    RemoveOrg.Enabled = false;
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

        private void AddClient_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (ClientForm cForm = new ClientForm(1, ClientFIOTB.Text))
                {
                    client = new Classes.ClientClass();
                    cForm.Owner = this;
                    cForm.ShowDialog();
                }

                if (client.FIO != null)
                {
                    ClientFIOTB.Text = client.FIO;
                    ClientBirthDate.Value = client.birthDate;
                    cbClientSex.SelectedItem = client.sex;
                    ClientAddressTB.Text = client.Address;
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

        private void FoundClient_Click(object sender, EventArgs e)
        {
            /*try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (ClientForm cForm = new ClientForm(2, ClientFIOTB.Text))
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
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
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
            try
            {
                Cursor.Current = Cursors.WaitCursor;
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
                                                    if (update != true) Classes.EventClass.InsertEvent(int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, DiagTB.Text /*MKB[MKB10TB.SelectedIndex].DiagName*/, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagID : "", SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                                    else Classes.EventClass.UpdateEvent(event_id, int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, DiagTB.Text /*MKB[MKB10TB.SelectedIndex].DiagName*/, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagID : "", SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                                    
                                                    MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    ClearAll();
                                                }
                                                else MessageBox.Show("Вы не указали состояние ребенка по степени тяжести", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            }
                                            else MessageBox.Show("Вы не указали информацию о направлении(переводе)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        else
                                        {
                                            if (cbHealthStatus.SelectedItem != null)
                                            {
                                                if (update != true) Classes.EventClass.InsertEvent(int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, DiagTB.Text /* MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagName : DiagTB.Text*/, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagID : "", SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                                else Classes.EventClass.UpdateEvent(event_id, int.Parse(cbArea.SelectedValue.ToString()), int.Parse(cbMO.SelectedValue.ToString()), EventDate.Value, int.Parse(cbShortNameOrg.SelectedValue.ToString()), client.id, RelaxInfo, TransferDate.Value, HelpName, DiagTB.Text /*MKB[MKB10TB.SelectedIndex].DiagName*/, MKB10TB.SelectedIndex != -1 ? MKB[MKB10TB.SelectedIndex].DiagID : "", SpecialityTB.Text, DepartmentTB.Text, TransfertedCheck.Checked == true ? TransferTB.Text : "", TransfertedCheck.Checked == true ? TransferDate.Value : DateTime.MinValue, cbHealthStatus.SelectedItem.ToString());
                                                MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                ClearAll();
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
                else MessageBox.Show("Вы не выбрали район", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HelpRB_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (HelpRB_1.Checked)
                {
                    DiagTB.Enabled = true;
                    MKB10TB.Enabled = true;
                    SpecialityTB.Enabled = false;
                    DepartmentTB.Enabled = false;
                    HelpName = 1;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HelpRB_2_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HelpRB_3_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MKB10TB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MKB10TB.Enabled && DiagTB.Enabled)
                {
                    if (MKB10TB.SelectedItem != null) DiagTB.Text = MKB10TB.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransfertedCheck_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
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
