namespace _403Unlocker.Configuration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownDnsResolveTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownHttpRequestTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPacketTimeout = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPacketCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPacketSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpRequestTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(233, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "Miliseconds";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(218, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Miliseconds";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(124, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Miliseconds";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(147, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "Bytes";
            // 
            // numericUpDownDnsResolveTimeout
            // 
            this.numericUpDownDnsResolveTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownDnsResolveTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownDnsResolveTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeout.Location = new System.Drawing.Point(159, 134);
            this.numericUpDownDnsResolveTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeout.Name = "numericUpDownDnsResolveTimeout";
            this.numericUpDownDnsResolveTimeout.ReadOnly = true;
            this.numericUpDownDnsResolveTimeout.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownDnsResolveTimeout.TabIndex = 36;
            this.numericUpDownDnsResolveTimeout.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownDnsResolveTimeout.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Window;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(216, 202);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 23);
            this.buttonCancel.TabIndex = 38;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 16);
            this.label8.TabIndex = 35;
            this.label8.Text = "HTTP Request Timeout: ";
            // 
            // numericUpDownHttpRequestTimeout
            // 
            this.numericUpDownHttpRequestTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownHttpRequestTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownHttpRequestTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeout.Location = new System.Drawing.Point(174, 160);
            this.numericUpDownHttpRequestTimeout.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeout.Name = "numericUpDownHttpRequestTimeout";
            this.numericUpDownHttpRequestTimeout.ReadOnly = true;
            this.numericUpDownHttpRequestTimeout.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownHttpRequestTimeout.TabIndex = 34;
            this.numericUpDownHttpRequestTimeout.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownHttpRequestTimeout.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.ForeColor = System.Drawing.Color.Black;
            this.buttonOK.Location = new System.Drawing.Point(131, 202);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(79, 23);
            this.buttonOK.TabIndex = 37;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "DNS Resolve Timeout:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Ping:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Packet Size: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Packet Count: ";
            // 
            // numericUpDownPacketTimeout
            // 
            this.numericUpDownPacketTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPacketTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPacketTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownPacketTimeout.Location = new System.Drawing.Point(69, 82);
            this.numericUpDownPacketTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPacketTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPacketTimeout.Name = "numericUpDownPacketTimeout";
            this.numericUpDownPacketTimeout.ReadOnly = true;
            this.numericUpDownPacketTimeout.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownPacketTimeout.TabIndex = 30;
            this.numericUpDownPacketTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPacketTimeout.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Timeout: ";
            // 
            // numericUpDownPacketCount
            // 
            this.numericUpDownPacketCount.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPacketCount.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPacketCount.Location = new System.Drawing.Point(96, 30);
            this.numericUpDownPacketCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPacketCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPacketCount.Name = "numericUpDownPacketCount";
            this.numericUpDownPacketCount.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownPacketCount.TabIndex = 29;
            this.numericUpDownPacketCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPacketCount.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // numericUpDownPacketSize
            // 
            this.numericUpDownPacketSize.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPacketSize.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPacketSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPacketSize.Location = new System.Drawing.Point(88, 56);
            this.numericUpDownPacketSize.Maximum = new decimal(new int[] {
            254,
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
            this.numericUpDownPacketSize.TabIndex = 28;
            this.numericUpDownPacketSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPacketSize.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 237);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDownDnsResolveTimeout);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownHttpRequestTimeout);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownPacketTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownPacketCount);
            this.Controls.Add(this.numericUpDownPacketSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHttpRequestTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownDnsResolveTimeout;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownHttpRequestTimeout;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketSize;
    }
}