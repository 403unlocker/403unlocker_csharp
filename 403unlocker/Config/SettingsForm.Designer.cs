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
            this.checkBoxAutoSelection = new System.Windows.Forms.CheckBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.comboBoxNetworkInterfaces = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPingTimeOut = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPacketCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPacketSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownHttpRequestTimeOut = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownDnsResolveTimeOut = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpRequestTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeOut)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxAutoSelection
            // 
            this.checkBoxAutoSelection.AutoSize = true;
            this.checkBoxAutoSelection.Checked = true;
            this.checkBoxAutoSelection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAutoSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.checkBoxAutoSelection.Location = new System.Drawing.Point(12, 57);
            this.checkBoxAutoSelection.Name = "checkBoxAutoSelection";
            this.checkBoxAutoSelection.Size = new System.Drawing.Size(112, 20);
            this.checkBoxAutoSelection.TabIndex = 14;
            this.checkBoxAutoSelection.Text = "Auto Selection";
            this.checkBoxAutoSelection.UseVisualStyleBackColor = true;
            this.checkBoxAutoSelection.CheckedChanged += new System.EventHandler(this.checkBoxAutoSelection_CheckedChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonApply.ForeColor = System.Drawing.Color.Black;
            this.buttonApply.Location = new System.Drawing.Point(173, 303);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(79, 23);
            this.buttonApply.TabIndex = 13;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // comboBoxNetworkInterfaces
            // 
            this.comboBoxNetworkInterfaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.comboBoxNetworkInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNetworkInterfaces.Enabled = false;
            this.comboBoxNetworkInterfaces.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxNetworkInterfaces.FormattingEnabled = true;
            this.comboBoxNetworkInterfaces.Location = new System.Drawing.Point(201, 30);
            this.comboBoxNetworkInterfaces.Name = "comboBoxNetworkInterfaces";
            this.comboBoxNetworkInterfaces.Size = new System.Drawing.Size(121, 21);
            this.comboBoxNetworkInterfaces.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select your network interface: ";
            // 
            // numericUpDownPingTimeOut
            // 
            this.numericUpDownPingTimeOut.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownPingTimeOut.Location = new System.Drawing.Point(82, 179);
            this.numericUpDownPingTimeOut.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPingTimeOut.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPingTimeOut.Name = "numericUpDownPingTimeOut";
            this.numericUpDownPingTimeOut.ReadOnly = true;
            this.numericUpDownPingTimeOut.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownPingTimeOut.TabIndex = 6;
            this.numericUpDownPingTimeOut.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // numericUpDownPacketCount
            // 
            this.numericUpDownPacketCount.Location = new System.Drawing.Point(110, 128);
            this.numericUpDownPacketCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPacketCount.Name = "numericUpDownPacketCount";
            this.numericUpDownPacketCount.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownPacketCount.TabIndex = 5;
            this.numericUpDownPacketCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numericUpDownPacketSize
            // 
            this.numericUpDownPacketSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPacketSize.Location = new System.Drawing.Point(102, 153);
            this.numericUpDownPacketSize.Maximum = new decimal(new int[] {
            20480,
            0,
            0,
            0});
            this.numericUpDownPacketSize.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPacketSize.Name = "numericUpDownPacketSize";
            this.numericUpDownPacketSize.ReadOnly = true;
            this.numericUpDownPacketSize.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownPacketSize.TabIndex = 4;
            this.numericUpDownPacketSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "TimeOut: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Packet Count: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label2.Location = new System.Drawing.Point(12, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Packet Size: ";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(258, 303);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label5.Location = new System.Drawing.Point(12, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ping:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label6.Location = new System.Drawing.Point(12, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Bypass && NS Look Up:";
            // 
            // numericUpDownHttpRequestTimeOut
            // 
            this.numericUpDownHttpRequestTimeOut.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeOut.Location = new System.Drawing.Point(176, 268);
            this.numericUpDownHttpRequestTimeOut.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeOut.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeOut.Name = "numericUpDownHttpRequestTimeOut";
            this.numericUpDownHttpRequestTimeOut.ReadOnly = true;
            this.numericUpDownHttpRequestTimeOut.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownHttpRequestTimeOut.TabIndex = 10;
            this.numericUpDownHttpRequestTimeOut.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label7.Location = new System.Drawing.Point(12, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "DNS Resolve TimeOut:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label8.Location = new System.Drawing.Point(12, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "HTTP Request TimeOut: ";
            // 
            // numericUpDownDnsResolveTimeOut
            // 
            this.numericUpDownDnsResolveTimeOut.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeOut.Location = new System.Drawing.Point(165, 242);
            this.numericUpDownDnsResolveTimeOut.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeOut.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeOut.Name = "numericUpDownDnsResolveTimeOut";
            this.numericUpDownDnsResolveTimeOut.ReadOnly = true;
            this.numericUpDownDnsResolveTimeOut.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownDnsResolveTimeOut.TabIndex = 12;
            this.numericUpDownDnsResolveTimeOut.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Network Adaptor:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label10.Location = new System.Drawing.Point(161, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "Bytes";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label12.Location = new System.Drawing.Point(137, 179);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Mili Seconds";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label11.Location = new System.Drawing.Point(224, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "Mili Seconds";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label13.Location = new System.Drawing.Point(235, 268);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 16);
            this.label13.TabIndex = 20;
            this.label13.Text = "Mili Seconds";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(349, 338);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkBoxAutoSelection);
            this.Controls.Add(this.numericUpDownDnsResolveTimeOut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxNetworkInterfaces);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownHttpRequestTimeOut);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownPingTimeOut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownPacketCount);
            this.Controls.Add(this.numericUpDownPacketSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "403 Setting";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpRequestTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ComboBox comboBoxNetworkInterfaces;
        private System.Windows.Forms.CheckBox checkBoxAutoSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPingTimeOut;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownHttpRequestTimeOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownDnsResolveTimeOut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
    }
}