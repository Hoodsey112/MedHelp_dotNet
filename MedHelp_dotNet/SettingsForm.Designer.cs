namespace MedHelp_dotNet
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ServerTB = new System.Windows.Forms.TextBox();
            this.LoginTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DataBaseCB = new System.Windows.Forms.ComboBox();
            this.ApplyBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сервер";
            // 
            // ServerTB
            // 
            this.ServerTB.Location = new System.Drawing.Point(62, 17);
            this.ServerTB.Name = "ServerTB";
            this.ServerTB.Size = new System.Drawing.Size(158, 20);
            this.ServerTB.TabIndex = 1;
            // 
            // LoginTB
            // 
            this.LoginTB.Location = new System.Drawing.Point(62, 43);
            this.LoginTB.Name = "LoginTB";
            this.LoginTB.Size = new System.Drawing.Size(158, 20);
            this.LoginTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Логин";
            // 
            // PasswordTB
            // 
            this.PasswordTB.Location = new System.Drawing.Point(62, 69);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = '*';
            this.PasswordTB.Size = new System.Drawing.Size(158, 20);
            this.PasswordTB.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Пароль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "БД";
            // 
            // DataBaseCB
            // 
            this.DataBaseCB.FormattingEnabled = true;
            this.DataBaseCB.Location = new System.Drawing.Point(62, 97);
            this.DataBaseCB.Name = "DataBaseCB";
            this.DataBaseCB.Size = new System.Drawing.Size(158, 21);
            this.DataBaseCB.TabIndex = 7;
            this.DataBaseCB.DropDown += new System.EventHandler(this.DataBaseCB_DropDown);
            // 
            // ApplyBTN
            // 
            this.ApplyBTN.Location = new System.Drawing.Point(30, 131);
            this.ApplyBTN.Name = "ApplyBTN";
            this.ApplyBTN.Size = new System.Drawing.Size(75, 23);
            this.ApplyBTN.TabIndex = 8;
            this.ApplyBTN.Text = "Сохранить";
            this.ApplyBTN.UseVisualStyleBackColor = true;
            this.ApplyBTN.Click += new System.EventHandler(this.ApplyBTN_Click);
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(145, 131);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 9;
            this.CancelBTN.Text = "Отмена";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 166);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.ApplyBTN);
            this.Controls.Add(this.DataBaseCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoginTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerTB);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(254, 193);
            this.MinimumSize = new System.Drawing.Size(254, 193);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerTB;
        private System.Windows.Forms.TextBox LoginTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DataBaseCB;
        private System.Windows.Forms.Button ApplyBTN;
        private System.Windows.Forms.Button CancelBTN;
    }
}