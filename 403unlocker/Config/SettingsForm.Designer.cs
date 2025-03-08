namespace _403unlocker.Config
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numericUpDown3);
            this.tabPage2.Controls.Add(this.numericUpDown2);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(341, 104);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Buffer Size: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Packet Count: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "TimeOut in Mili Second: ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(90, 16);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65500,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(115, 39);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown2.TabIndex = 5;
            this.numericUpDown2.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(149, 70);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown3.TabIndex = 6;
            this.numericUpDown3.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(349, 130);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "403 Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getPingButton;
        private System.Windows.Forms.ComboBox networkComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox autoSelectionCheckBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
    }
}