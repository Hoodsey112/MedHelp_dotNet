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
    public partial class DuplicateForm : Form
    {
        public DuplicateForm()
        {
            InitializeComponent();
        }

        private void DuplicateForm_Load(object sender, EventArgs e)
        {
            DuplicateDGV.DataSource = Classes.EventClass.SetDuplicate();
        }

        private void deleteEvent_Click(object sender, EventArgs e)
        {
            Classes.EventClass.RemoveEvent(int.Parse(DuplicateDGV.Rows[DuplicateDGV.CurrentRow.Index].Cells[0].Value.ToString()));
            DuplicateDGV.DataSource = null;
            DuplicateDGV.DataSource = Classes.EventClass.SetDuplicate();
        }
    }
}
