using System;
using System.Data;
using System.Windows.Forms;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        DataTable ClientTable = new DataTable();
        int StatusForm = 0;
        int _StatForm = 0;
        public ClientForm(int _StatusForm, string FIO)
        {

            InitializeComponent();
            clientDGV.AutoGenerateColumns = false;
            LoadClient();
            if (FIO != "") ClientFIOTB.Text = FIO;
            StatusForm = _StatusForm;
            _StatForm = _StatusForm;
            CheckStatus();
        }

        private void CheckStatus()
        {
            try
            {
                switch (StatusForm)
                {
                    case 1:
                        ApplyBTN.Text = "Добавить";
                        break;
                    /*case 2:
                        ApplyBTN.Text = "Выбрать";
                        break;*/
                    case 3:
                        ApplyBTN.Text = "Сохранить";
                        break;
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClient()
        {
            try
            {
                ClientTable.Clear();
                clientDGV.DataSource = null;
                ClientTable = Classes.ClientClass.LoadClientData();
                clientDGV.DataSource = ClientTable;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearAll()
        {
            try
            {
                ClientFIOTB.Clear();
                birthDateDTP.Value = DateTime.Now;
                cbSex.SelectedIndex = -1;
                AddressTB.Clear();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (StatusForm)
                {
                    case 1:
                        Classes.ClientClass.InsertClientData(ClientFIOTB.Text, birthDateDTP.Value, AddressTB.Text, cbSex.SelectedIndex + 1);
                        ClearAll();
                        LoadClient();
                        break;
                    /*case 2:
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
                        break;*/
                    case 3:
                        Classes.ClientClass.EditClientData(ClientFIOTB.Text, birthDateDTP.Value, AddressTB.Text, cbSex.SelectedIndex + 1, int.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[0].Value.ToString()));
                        ClearAll();
                        LoadClient();
                        StatusForm = _StatForm;
                        CheckStatus();
                        break;
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

        private void EditClient_Click(object sender, EventArgs e)
        {
            try
            {
                ClientFIOTB.Text = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[1].Value.ToString();
                birthDateDTP.Value = DateTime.Parse(clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[2].Value.ToString());
                cbSex.SelectedItem = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[3].Value.ToString();
                AddressTB.Text = clientDGV.Rows[clientDGV.CurrentRow.Index].Cells[4].Value.ToString();

                StatusForm = 3;
                CheckStatus();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clientDGV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            try
            {
                /*AddEventForm main = this.Owner as AddEventForm;
                if (main != null)
                {
                    main.client = null;
                }*/

                Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FoundCHB_CheckedChanged(object sender, EventArgs e)
        {
            if (FoundCHB.Checked) ClientTable.DefaultView.RowFilter = $"[FIO] like '{ClientFIOTB.Text}%'";
            else ClientTable.DefaultView.RowFilter = $"[FIO] like '%'";
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void ClientFIOTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (FoundCHB.Checked) ClientTable.DefaultView.RowFilter = $"[FIO] like '{ClientFIOTB.Text}%'";
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
