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
    public partial class MainForm : Form
    {
        DataTable eventsData = new DataTable();
        Classes.AreaClass[] areas = Classes.AreaClass.LoadListArea();
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            
            eventsData = Classes.EventClass.LoadEvent();
            dataGridView1.DataSource = eventsData;

            cbArea.DataSource = areas;
            cbArea.DisplayMember = "name";
            cbArea.ValueMember = "id";
            cbArea.SelectedIndex = -1;
        }

        private void AreaCB_CheckedChanged(object sender, EventArgs e)
        {
            if (AreaCB.Checked) cbArea.Enabled = true;
            else cbArea.Enabled = false;
        }

        private void medOrgCB_CheckedChanged(object sender, EventArgs e)
        {
            if (medOrgCB.Checked) cbMedOrg.Enabled = true;
            else cbMedOrg.Enabled = false;
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
            if (DOOCB.Checked) cbDOO.Enabled = true;
            else cbDOO.Enabled = false;
        }

        private void ClientFIOCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ClientFIOCB.Checked) ClientFIOTB.Enabled = true;
            else ClientFIOTB.Enabled = false;
        }

        private void AgeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (AgeCB.Checked)
            {
                label3.Enabled = true;
                ageTB_From.Enabled = true;
                label4.Enabled = true;
                ageTB_To.Enabled = true;
            }
            else
            {
                label3.Enabled = false;
                ageTB_From.Enabled = false;
                label4.Enabled = false;
                ageTB_To.Enabled = false;
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
            if (TransfertedCB.Checked) TransfertedTB.Enabled = true;
            else TransfertedTB.Enabled = false;
        }

        private void HealthStatusCB_CheckedChanged(object sender, EventArgs e)
        {
            if (HealthStatusCB.Checked) cbHealthStatus.Enabled = true;
            else cbHealthStatus.Enabled = false;
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex != -1) eventsData.DefaultView.RowFilter = string.Format($"Convert([areaName], System.String) = '{areas[cbArea.SelectedIndex].name}'");
            else eventsData.DefaultView.RowFilter = string.Format($"Convert([areaName], System.String) like '%'");
        }
    }
}
