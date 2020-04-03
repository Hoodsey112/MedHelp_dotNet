using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MedHelp_dotNet
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            DefaultSettings();
        }

        private void DefaultSettings()
        {
            ServerTB.Text = Properties.Settings.Default.server;
            LoginTB.Text = Properties.Settings.Default.login;
            PasswordTB.Text = Properties.Settings.Default.password;
            LoadDataBases();
            DataBaseCB.SelectedItem = Properties.Settings.Default.dataBase;
        }

        private void SaveDefaultSettings()
        {
            Properties.Settings.Default.server = ServerTB.Text;
            Properties.Settings.Default.login = LoginTB.Text;
            Properties.Settings.Default.password = PasswordTB.Text;
            Properties.Settings.Default.dataBase = DataBaseCB.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void LoadDataBases()
        {
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
