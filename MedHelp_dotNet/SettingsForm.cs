using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using NLog;

namespace MedHelp_dotNet
{
    public partial class SettingsForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SettingsForm()
        {
            InitializeComponent();
            DefaultSettings();
        }

        private void DefaultSettings()
        {
            try
            {
                ServerTB.Text = Properties.Settings.Default.server;
                LoginTB.Text = Properties.Settings.Default.login;
                PasswordTB.Text = Properties.Settings.Default.password;
                LoadDataBases();
                DataBaseCB.SelectedItem = Properties.Settings.Default.dataBase;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDefaultSettings()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Properties.Settings.Default.server = ServerTB.Text;
                Properties.Settings.Default.login = LoginTB.Text;
                Properties.Settings.Default.password = PasswordTB.Text;
                Properties.Settings.Default.dataBase = DataBaseCB.SelectedItem.ToString();
                Properties.Settings.Default.Save();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                logger.Error(ex, $"\r\n#---------#\r\n{ex.StackTrace}\r\n##---------##\r\n{ex.Message}\r\n###---------###\r\n{ex.Source}");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataBases()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string query = "Show databases";

                using (MySqlConnection sqlConnection = Classes.ConnectionClass.GetStringConnectionForComboBox(ServerTB.Text, LoginTB.Text, PasswordTB.Text))
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection))
                    {
                        if (sqlConnection.State == ConnectionState.Closed) sqlConnection.Open();

                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DataBaseCB.Items.Add(reader.GetString(0));
                                }
                            }
                        }
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

        private void DataBaseCB_DropDown(object sender, EventArgs e)
        {
            if (ServerTB.Text != "" && LoginTB.Text != "" && PasswordTB.Text !="") LoadDataBases();
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            SaveDefaultSettings();
        }
    }
}
