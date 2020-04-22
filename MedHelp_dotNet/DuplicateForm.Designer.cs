namespace MedHelp_dotNet
{
    partial class DuplicateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DuplicateDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AreaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medOrgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOOName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOOAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.relaxName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TreatmentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HelpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiagID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speciality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransfertedDepart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransfertedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HealthStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEvent = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DuplicateDGV)).BeginInit();
            this.EventMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DuplicateDGV
            // 
            this.DuplicateDGV.AllowUserToAddRows = false;
            this.DuplicateDGV.AllowUserToDeleteRows = false;
            this.DuplicateDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DuplicateDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DuplicateDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DuplicateDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DuplicateDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DuplicateDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.AreaName,
            this.medOrgName,
            this.eventDate,
            this.DOOName,
            this.DOOAddress,
            this.clientFIO,
            this.clientBirthDate,
            this.clientAge,
            this.clientAddress,
            this.relaxName,
            this.TreatmentDate,
            this.HelpName,
            this.FullName,
            this.DiagName,
            this.DiagID,
            this.Speciality,
            this.Department,
            this.TransfertedDepart,
            this.TransfertedDate,
            this.HealthStatus});
            this.DuplicateDGV.ContextMenuStrip = this.EventMenuStrip;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DuplicateDGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.DuplicateDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DuplicateDGV.Location = new System.Drawing.Point(0, 0);
            this.DuplicateDGV.Name = "DuplicateDGV";
            this.DuplicateDGV.ReadOnly = true;
            this.DuplicateDGV.RowHeadersVisible = false;
            this.DuplicateDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DuplicateDGV.Size = new System.Drawing.Size(800, 450);
            this.DuplicateDGV.TabIndex = 0;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // AreaName
            // 
            this.AreaName.DataPropertyName = "AreaName";
            this.AreaName.HeaderText = "Район";
            this.AreaName.Name = "AreaName";
            this.AreaName.ReadOnly = true;
            // 
            // medOrgName
            // 
            this.medOrgName.DataPropertyName = "medOrgName";
            this.medOrgName.HeaderText = "Медицинская организация";
            this.medOrgName.Name = "medOrgName";
            this.medOrgName.ReadOnly = true;
            // 
            // eventDate
            // 
            this.eventDate.DataPropertyName = "eventDate";
            this.eventDate.HeaderText = "Дата подачи информации";
            this.eventDate.Name = "eventDate";
            this.eventDate.ReadOnly = true;
            // 
            // DOOName
            // 
            this.DOOName.DataPropertyName = "DOOName";
            this.DOOName.HeaderText = "Название ДОО";
            this.DOOName.Name = "DOOName";
            this.DOOName.ReadOnly = true;
            // 
            // DOOAddress
            // 
            this.DOOAddress.DataPropertyName = "DOOAddress";
            this.DOOAddress.HeaderText = "Адрес ДОО";
            this.DOOAddress.Name = "DOOAddress";
            this.DOOAddress.ReadOnly = true;
            // 
            // clientFIO
            // 
            this.clientFIO.DataPropertyName = "clientFIO";
            this.clientFIO.HeaderText = "ФИО ребенка";
            this.clientFIO.Name = "clientFIO";
            this.clientFIO.ReadOnly = true;
            // 
            // clientBirthDate
            // 
            this.clientBirthDate.DataPropertyName = "clientBirthDate";
            this.clientBirthDate.HeaderText = "Дата рождения";
            this.clientBirthDate.Name = "clientBirthDate";
            this.clientBirthDate.ReadOnly = true;
            // 
            // clientAge
            // 
            this.clientAge.DataPropertyName = "clientAge";
            this.clientAge.HeaderText = "Возраст";
            this.clientAge.Name = "clientAge";
            this.clientAge.ReadOnly = true;
            // 
            // clientAddress
            // 
            this.clientAddress.DataPropertyName = "clientAddress";
            this.clientAddress.HeaderText = "Адрес постоянного проживания";
            this.clientAddress.Name = "clientAddress";
            this.clientAddress.ReadOnly = true;
            // 
            // relaxName
            // 
            this.relaxName.DataPropertyName = "relaxName";
            this.relaxName.HeaderText = "Тип отдыха";
            this.relaxName.Name = "relaxName";
            this.relaxName.ReadOnly = true;
            // 
            // TreatmentDate
            // 
            this.TreatmentDate.DataPropertyName = "TreatmentDate";
            this.TreatmentDate.HeaderText = "Дата обращения";
            this.TreatmentDate.Name = "TreatmentDate";
            this.TreatmentDate.ReadOnly = true;
            // 
            // HelpName
            // 
            this.HelpName.DataPropertyName = "HelpName";
            this.HelpName.HeaderText = "HelpName";
            this.HelpName.Name = "HelpName";
            this.HelpName.ReadOnly = true;
            this.HelpName.Visible = false;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Вид оказанной помощи";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // DiagName
            // 
            this.DiagName.DataPropertyName = "DiagName";
            this.DiagName.HeaderText = "Диагноз";
            this.DiagName.Name = "DiagName";
            this.DiagName.ReadOnly = true;
            // 
            // DiagID
            // 
            this.DiagID.DataPropertyName = "DiagID";
            this.DiagID.HeaderText = "МКБ10";
            this.DiagID.Name = "DiagID";
            this.DiagID.ReadOnly = true;
            // 
            // Speciality
            // 
            this.Speciality.DataPropertyName = "Speciality";
            this.Speciality.HeaderText = "Врач-специалист";
            this.Speciality.Name = "Speciality";
            this.Speciality.ReadOnly = true;
            // 
            // Department
            // 
            this.Department.DataPropertyName = "Department";
            this.Department.HeaderText = "Отделение";
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            // 
            // TransfertedDepart
            // 
            this.TransfertedDepart.DataPropertyName = "TransfertedDepart";
            this.TransfertedDepart.HeaderText = "Направлен(переведен)";
            this.TransfertedDepart.Name = "TransfertedDepart";
            this.TransfertedDepart.ReadOnly = true;
            // 
            // TransfertedDate
            // 
            this.TransfertedDate.DataPropertyName = "TransfertedDate";
            this.TransfertedDate.HeaderText = "Дата перевода";
            this.TransfertedDate.Name = "TransfertedDate";
            this.TransfertedDate.ReadOnly = true;
            // 
            // HealthStatus
            // 
            this.HealthStatus.DataPropertyName = "HealthStatus";
            this.HealthStatus.HeaderText = "Состояние ребенка по степени тяжести";
            this.HealthStatus.Name = "HealthStatus";
            this.HealthStatus.ReadOnly = true;
            // 
            // EventMenuStrip
            // 
            this.EventMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEvent});
            this.EventMenuStrip.Name = "EventMenuStrip";
            this.EventMenuStrip.Size = new System.Drawing.Size(156, 26);
            // 
            // deleteEvent
            // 
            this.deleteEvent.Name = "deleteEvent";
            this.deleteEvent.Size = new System.Drawing.Size(155, 22);
            this.deleteEvent.Text = "Удалить запись";
            this.deleteEvent.Click += new System.EventHandler(this.deleteEvent_Click);
            // 
            // DuplicateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DuplicateDGV);
            this.Name = "DuplicateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дубли";
            this.Load += new System.EventHandler(this.DuplicateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DuplicateDGV)).EndInit();
            this.EventMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DuplicateDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn AreaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn medOrgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOOName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOOAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn relaxName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TreatmentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn HelpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speciality;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransfertedDepart;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransfertedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn HealthStatus;
        private System.Windows.Forms.ContextMenuStrip EventMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteEvent;
    }
}