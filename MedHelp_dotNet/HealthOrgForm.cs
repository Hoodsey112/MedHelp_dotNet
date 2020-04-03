using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedHelp_dotNet
{
    public partial class HealthOrgForm : Form
    {
        Classes.AreaClass[] areaClass;
        Classes.HealthOrgClass health;

        bool EdIn = true; //"true" if you edit data, if you insert data then EdIn = "false"

        public HealthOrgForm(bool _EdIn, int _area_id, int _id)
        {
            InitializeComponent();
            LoadArea();
            EdIn = _EdIn;
            if (_EdIn)
            { 
                health = Classes.HealthOrgClass.LoadHealthOrgList(_id, _area_id);
                EditData();
            }
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
        #endregion

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditData()
        {
            cbArea.SelectedValue = health.area_id;
            FullNameTB.Text = health.FullName;
            ShortNameTB.Text = health.ShortName;
            AddressTB.Text = health.Address;
        }

        private void SaveData()
        {
            if (!EdIn) Classes.HealthOrgClass.InsertHealthOrg(FullNameTB.Text, ShortNameTB.Text, AddressTB.Text, int.Parse(cbArea.SelectedValue.ToString()));
            else Classes.HealthOrgClass.EditHealthOrg(health.id, health.area_id, FullNameTB.Text, ShortNameTB.Text, AddressTB.Text, int.Parse(cbArea.SelectedValue.ToString()));
            if (DialogResult.OK == MessageBox.Show("Данные успешно сохранены", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Information)) Close();
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
