using System;
using System.Windows.Forms;
using NLog;

namespace MedHelp_dotNet
{
    public partial class HealthOrgForm : Form
    {
        Classes.AreaClass[] areaClass;
        Classes.HealthOrgClass health;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool EdIn = true; //"true" if you edit data, if you insert data then EdIn = "false"

        public HealthOrgForm(bool _EdIn, int _area_id, int _id)
        {
            try
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
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditData()
        {
            try
            {
                cbArea.SelectedValue = health.area_id;
                FullNameTB.Text = health.FullName;
                ShortNameTB.Text = health.ShortName;
                AddressTB.Text = health.Address;
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!EdIn) Classes.HealthOrgClass.InsertHealthOrg(FullNameTB.Text, ShortNameTB.Text, AddressTB.Text, int.Parse(cbArea.SelectedValue.ToString()));
                else Classes.HealthOrgClass.EditHealthOrg(health.id, health.area_id, FullNameTB.Text, ShortNameTB.Text, AddressTB.Text, int.Parse(cbArea.SelectedValue.ToString()));
                if (DialogResult.OK == MessageBox.Show("Данные успешно сохранены", "Сохранение записи", MessageBoxButtons.OK, MessageBoxIcon.Information)) Close();
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch(Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
