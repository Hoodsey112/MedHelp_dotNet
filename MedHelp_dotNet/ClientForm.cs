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
    /*
     * Форма принимает параметр (StatusForm)
     * Если StatusForm = 1 - Форма открыта для добавления данных ребенка в таблицу
     * Если StatusForm = 2 - Форма открыта для выбора данных ребенка из таблицы
     * Если StatusForm = 3 - Форма открыта для выбора данных ребенка из таблицы
     */

    public partial class ClientForm : Form
    {
        DataTable ClientTable = new DataTable();
        int StatusForm = 0;
        int _StatForm = 0;
        public ClientForm(int _StatusForm)
        {
            InitializeComponent();
            clientDGV.AutoGenerateColumns = false;
            LoadClient();
            StatusForm = _StatusForm;
            _StatForm = _StatusForm;
            CheckStatus();
        }

        private void CheckStatus()
        {
            switch (StatusForm)
            {
                case 1:
                    ApplyBTN.Text = "Добавить";
                    break;
                case 2:
                    ApplyBTN.Text = "Выбрать";
                    break;
                case 3:
                    ApplyBTN.Text = "Сохранить";
                    break;
            }
        }

        private void LoadClient()
        {
            ClientTable.Clear();
            clientDGV.DataSource = null;
            ClientTable = Classes.ClientClass.LoadClientData();
            clientDGV.DataSource = ClientTable;
        }
        private void ClearAll()
        {
            ClientFIOTB.Clear();
            birthDateDTP.Value = DateTime.Now;
            cbSex.SelectedIndex = -1;
            AddressTB.Clear();
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            switch(StatusForm)
            {
                case 1:
                    Classes.ClientClass.InsertClientData(ClientFIOTB.Text, birthDateDTP.Value, AddressTB.Text, cbSex.SelectedIndex+1);
                    ClearAll();
                    LoadClient();
                    break;
                case 2:
                    AddEventForm main = this.Owner as AddEventForm;
                    if (main != null)
                    {
                        main.client.id = int.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[0].Value.ToString());
                        main.client.FIO = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[1].Value.ToString();
                        main.client.birthDate = DateTime.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[2].Value.ToString());
                        main.client.sex = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[3].Value.ToString();
                        main.client.Address = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[4].Value.ToString();
                    }
                    Close();
                    break;
                case 3:
                    Classes.ClientClass.EditClientData(ClientFIOTB.Text, birthDateDTP.Value, AddressTB.Text, cbSex.SelectedIndex + 1, int.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[0].Value.ToString()));
                    ClearAll();
                    LoadClient();
                    StatusForm = _StatForm;
                    CheckStatus();
                    break;
            }
        }

        private void EditClient_Click(object sender, EventArgs e)
        {
            ClientFIOTB.Text = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[1].Value.ToString();
            birthDateDTP.Value = DateTime.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[2].Value.ToString());
            cbSex.SelectedItem = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[3].Value.ToString();
            AddressTB.Text = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[4].Value.ToString();
            
            StatusForm = 3;
            CheckStatus();
        }
    }
}
