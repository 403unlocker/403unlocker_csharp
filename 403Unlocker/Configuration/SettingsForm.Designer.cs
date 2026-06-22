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
            this.numericUpDownScraperHttpRequestTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPingTimeout = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPingPacketCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPingPacketSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTcpConnectTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownTlsHandshakeTimeout = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScraperHttpRequestTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingPacketCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingPacketSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTcpConnectTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTlsHandshakeTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(286, 293);
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
            this.label11.Location = new System.Drawing.Point(224, 136);
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
            this.label12.Location = new System.Drawing.Point(133, 84);
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
            this.label10.Location = new System.Drawing.Point(134, 58);
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
            this.numericUpDownDnsResolveTimeout.Location = new System.Drawing.Point(163, 134);
            this.numericUpDownDnsResolveTimeout.Maximum = new decimal(new int[] {
            10000,
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
            this.numericUpDownDnsResolveTimeout.Size = new System.Drawing.Size(55, 20);
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
            this.buttonCancel.Location = new System.Drawing.Point(269, 342);
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
            this.label8.Location = new System.Drawing.Point(12, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(207, 16);
            this.label8.TabIndex = 35;
            this.label8.Text = "Scraper HTTP Request Timeout: ";
            // 
            // numericUpDownScraperHttpRequestTimeout
            // 
            this.numericUpDownScraperHttpRequestTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownScraperHttpRequestTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownScraperHttpRequestTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownScraperHttpRequestTimeout.Location = new System.Drawing.Point(225, 290);
            this.numericUpDownScraperHttpRequestTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownScraperHttpRequestTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownScraperHttpRequestTimeout.Name = "numericUpDownScraperHttpRequestTimeout";
            this.numericUpDownScraperHttpRequestTimeout.ReadOnly = true;
            this.numericUpDownScraperHttpRequestTimeout.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownScraperHttpRequestTimeout.TabIndex = 34;
            this.numericUpDownScraperHttpRequestTimeout.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownScraperHttpRequestTimeout.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOK.ForeColor = System.Drawing.Color.Black;
            this.buttonOK.Location = new System.Drawing.Point(184, 342);
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
            // numericUpDownPingTimeout
            // 
            this.numericUpDownPingTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPingTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPingTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownPingTimeout.Location = new System.Drawing.Point(69, 82);
            this.numericUpDownPingTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPingTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPingTimeout.Name = "numericUpDownPingTimeout";
            this.numericUpDownPingTimeout.ReadOnly = true;
            this.numericUpDownPingTimeout.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownPingTimeout.TabIndex = 30;
            this.numericUpDownPingTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPingTimeout.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
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
            // numericUpDownPingPacketCount
            // 
            this.numericUpDownPingPacketCount.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPingPacketCount.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPingPacketCount.Location = new System.Drawing.Point(96, 30);
            this.numericUpDownPingPacketCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPingPacketCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPingPacketCount.Name = "numericUpDownPingPacketCount";
            this.numericUpDownPingPacketCount.Size = new System.Drawing.Size(31, 20);
            this.numericUpDownPingPacketCount.TabIndex = 29;
            this.numericUpDownPingPacketCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPingPacketCount.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // numericUpDownPingPacketSize
            // 
            this.numericUpDownPingPacketSize.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownPingPacketSize.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownPingPacketSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPingPacketSize.Location = new System.Drawing.Point(88, 56);
            this.numericUpDownPingPacketSize.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numericUpDownPingPacketSize.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPingPacketSize.Name = "numericUpDownPingPacketSize";
            this.numericUpDownPingPacketSize.ReadOnly = true;
            this.numericUpDownPingPacketSize.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownPingPacketSize.TabIndex = 28;
            this.numericUpDownPingPacketSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownPingPacketSize.ValueChanged += new System.EventHandler(this.numericUpDownPacketCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(208, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Miliseconds";
            // 
            // numericUpDownTcpConnectTimeout
            // 
            this.numericUpDownTcpConnectTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownTcpConnectTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownTcpConnectTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownTcpConnectTimeout.Location = new System.Drawing.Point(147, 212);
            this.numericUpDownTcpConnectTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTcpConnectTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTcpConnectTimeout.Name = "numericUpDownTcpConnectTimeout";
            this.numericUpDownTcpConnectTimeout.ReadOnly = true;
            this.numericUpDownTcpConnectTimeout.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownTcpConnectTimeout.TabIndex = 46;
            this.numericUpDownTcpConnectTimeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "TCP Connect Timeout:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(208, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "Miliseconds";
            // 
            // numericUpDownTlsHandshakeTimeout
            // 
            this.numericUpDownTlsHandshakeTimeout.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDownTlsHandshakeTimeout.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownTlsHandshakeTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownTlsHandshakeTimeout.Location = new System.Drawing.Point(147, 238);
            this.numericUpDownTlsHandshakeTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTlsHandshakeTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTlsHandshakeTimeout.Name = "numericUpDownTlsHandshakeTimeout";
            this.numericUpDownTlsHandshakeTimeout.ReadOnly = true;
            this.numericUpDownTlsHandshakeTimeout.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownTlsHandshakeTimeout.TabIndex = 49;
            this.numericUpDownTlsHandshakeTimeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(12, 240);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "TLS Handshake Timeout:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(12, 186);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 16);
            this.label15.TabIndex = 52;
            this.label15.Text = "Bypass:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 377);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDownTlsHandshakeTimeout);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTcpConnectTimeout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDownDnsResolveTimeout);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownScraperHttpRequestTimeout);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownPingTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownPingPacketCount);
            this.Controls.Add(this.numericUpDownPingPacketSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDnsResolveTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScraperHttpRequestTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingPacketCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingPacketSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTcpConnectTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTlsHandshakeTimeout)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownScraperHttpRequestTimeout;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownPingTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPingPacketCount;
        private System.Windows.Forms.NumericUpDown numericUpDownPingPacketSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTcpConnectTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownTlsHandshakeTimeout;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}