namespace _403unlocker.Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.autoSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.getPingButton = new System.Windows.Forms.Button();
            this.networkComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 130);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.autoSelectionCheckBox);
            this.tabPage1.Controls.Add(this.getPingButton);
            this.tabPage1.Controls.Add(this.networkComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(341, 104);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Network Interface";
            // 
            // autoSelectionCheckBox
            // 
            this.autoSelectionCheckBox.AutoSize = true;
            this.autoSelectionCheckBox.Checked = true;
            this.autoSelectionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSelectionCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoSelectionCheckBox.Location = new System.Drawing.Point(11, 48);
            this.autoSelectionCheckBox.Name = "autoSelectionCheckBox";
            this.autoSelectionCheckBox.Size = new System.Drawing.Size(112, 20);
            this.autoSelectionCheckBox.TabIndex = 14;
            this.autoSelectionCheckBox.Text = "Auto Selection";
            this.autoSelectionCheckBox.UseVisualStyleBackColor = true;
            this.autoSelectionCheckBox.CheckedChanged += new System.EventHandler(this.autoSelectionCheckBox_CheckedChanged);
            // 
            // getPingButton
            // 
            this.getPingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.getPingButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.getPingButton.ForeColor = System.Drawing.Color.Black;
            this.getPingButton.Location = new System.Drawing.Point(131, 76);
            this.getPingButton.Name = "getPingButton";
            this.getPingButton.Size = new System.Drawing.Size(79, 23);
            this.getPingButton.TabIndex = 13;
            this.getPingButton.Text = "Apply";
            this.getPingButton.UseVisualStyleBackColor = false;
            this.getPingButton.Click += new System.EventHandler(this.getPingButton_Click);
            // 
            // networkComboBox
            // 
            this.networkComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.networkComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.networkComboBox.Enabled = false;
            this.networkComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.networkComboBox.FormattingEnabled = true;
            this.networkComboBox.Location = new System.Drawing.Point(200, 19);
            this.networkComboBox.Name = "networkComboBox";
            this.networkComboBox.Size = new System.Drawing.Size(121, 21);
            this.networkComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select your network interface: ";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(349, 130);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "403 Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getPingButton;
        private System.Windows.Forms.ComboBox networkComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox autoSelectionCheckBox;
    }
}