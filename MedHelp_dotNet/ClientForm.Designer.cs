namespace MedHelp_dotNet
{
    partial class ClientForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddressTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clientDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditClient = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveClient = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSex = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.birthDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientFIOTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplyBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientDGV)).BeginInit();
            this.ClientMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.AddressTB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.clientDGV);
            this.groupBox1.Controls.Add(this.cbSex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.birthDateDTP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ClientFIOTB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 448);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // AddressTB
            // 
            this.AddressTB.Location = new System.Drawing.Point(103, 109);
            this.AddressTB.Multiline = true;
            this.AddressTB.Name = "AddressTB";
            this.AddressTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AddressTB.Size = new System.Drawing.Size(426, 62);
            this.AddressTB.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 45);
            this.label4.TabIndex = 7;
            this.label4.Text = "Адрес постоянного проживания";
            // 
            // clientDGV
            // 
            this.clientDGV.AllowUserToAddRows = false;
            this.clientDGV.AllowUserToDeleteRows = false;
            this.clientDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.clientDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.clientDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clientDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.clientDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.FIO,
            this.birthDate,
            this.sex,
            this.Address});
            this.clientDGV.ContextMenuStrip = this.ClientMenuStrip;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.clientDGV.DefaultCellStyle = dataGridViewCellStyle6;
            this.clientDGV.Location = new System.Drawing.Point(6, 177);
            this.clientDGV.MultiSelect = false;
            this.clientDGV.Name = "clientDGV";
            this.clientDGV.RowHeadersVisible = false;
            this.clientDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientDGV.Size = new System.Drawing.Size(547, 263);
            this.clientDGV.TabIndex = 6;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // FIO
            // 
            this.FIO.DataPropertyName = "FIO";
            this.FIO.HeaderText = "ФИО ребенка";
            this.FIO.Name = "FIO";
            this.FIO.ReadOnly = true;
            // 
            // birthDate
            // 
            this.birthDate.DataPropertyName = "birthDate";
            this.birthDate.HeaderText = "Дата рождения";
            this.birthDate.Name = "birthDate";
            this.birthDate.ReadOnly = true;
            // 
            // sex
            // 
            this.sex.DataPropertyName = "sex";
            this.sex.HeaderText = "Пол";
            this.sex.Name = "sex";
            this.sex.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Адрес постоянного проживания";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // ClientMenuStrip
            // 
            this.ClientMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditClient,
            this.RemoveClient});
            this.ClientMenuStrip.Name = "ClientMenuStrip";
            this.ClientMenuStrip.Size = new System.Drawing.Size(154, 48);
            // 
            // EditClient
            // 
            this.EditClient.Name = "EditClient";
            this.EditClient.Size = new System.Drawing.Size(153, 22);
            this.EditClient.Text = "Редактировать";
            this.EditClient.Click += new System.EventHandler(this.EditClient_Click);
            // 
            // RemoveClient
            // 
            this.RemoveClient.Name = "RemoveClient";
            this.RemoveClient.Size = new System.Drawing.Size(153, 22);
            this.RemoveClient.Text = "Удалить";
            // 
            // cbSex
            // 
            this.cbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSex.FormattingEnabled = true;
            this.cbSex.Items.AddRange(new object[] {
            "Мужской",
            "Женский"});
            this.cbSex.Location = new System.Drawing.Point(103, 82);
            this.cbSex.Name = "cbSex";
            this.cbSex.Size = new System.Drawing.Size(73, 21);
            this.cbSex.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Пол";
            // 
            // birthDateDTP
            // 
            this.birthDateDTP.Location = new System.Drawing.Point(103, 51);
            this.birthDateDTP.Name = "birthDateDTP";
            this.birthDateDTP.Size = new System.Drawing.Size(124, 20);
            this.birthDateDTP.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Дата рождения";
            // 
            // ClientFIOTB
            // 
            this.ClientFIOTB.Location = new System.Drawing.Point(103, 23);
            this.ClientFIOTB.Name = "ClientFIOTB";
            this.ClientFIOTB.Size = new System.Drawing.Size(426, 20);
            this.ClientFIOTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО ребенка";
            // 
            // ApplyBTN
            // 
            this.ApplyBTN.Location = new System.Drawing.Point(169, 469);
            this.ApplyBTN.Name = "ApplyBTN";
            this.ApplyBTN.Size = new System.Drawing.Size(75, 23);
            this.ApplyBTN.TabIndex = 5;
            this.ApplyBTN.Text = "button1";
            this.ApplyBTN.UseVisualStyleBackColor = true;
            this.ApplyBTN.Click += new System.EventHandler(this.ApplyBTN_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(336, 469);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 6;
            this.CancelBTN.Text = "Отмена";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 500);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.ApplyBTN);
            this.Controls.Add(this.groupBox1);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Данные о детях";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientDGV)).EndInit();
            this.ClientMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbSex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker birthDateDTP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ClientFIOTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ApplyBTN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.DataGridView clientDGV;
        private System.Windows.Forms.TextBox AddressTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip ClientMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem EditClient;
        private System.Windows.Forms.ToolStripMenuItem RemoveClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
    }
}