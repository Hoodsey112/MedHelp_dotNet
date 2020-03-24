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

        public HealthOrgForm()
        {
            InitializeComponent();
            LoadArea();
        }

        #region Area
        //Загрузка списка районов
        private void LoadArea()
        {
            areaClass = Classes.QueryClass.LoadListArea();
            cbArea.DataSource = areaClass;
            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
            cbArea.SelectedIndex = -1;
        }
        #endregion

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
