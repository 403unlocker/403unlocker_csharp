namespace _403Unlocker.Network_Interface_Configuration
{
    partial class NetworkInterfaceConfigurationForm
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
            this.buttonSetAsPrimary = new System.Windows.Forms.Button();
            this.buttonSetAsSecondary = new System.Windows.Forms.Button();
            this.buttonResetDNS = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDns = new System.Windows.Forms.Label();
            this.labelSelectedDns = new System.Windows.Forms.Label();
            this.checkBoxAutoSelect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonSetAsPrimary
            // 
            this.buttonSetAsPrimary.Location = new System.Drawing.Point(12, 148);
            this.buttonSetAsPrimary.Name = "buttonSetAsPrimary";
            this.buttonSetAsPrimary.Size = new System.Drawing.Size(100, 23);
            this.buttonSetAsPrimary.TabIndex = 0;
            this.buttonSetAsPrimary.Text = "Set as Primary";
            this.buttonSetAsPrimary.UseVisualStyleBackColor = true;
            this.buttonSetAsPrimary.Click += new System.EventHandler(this.buttonSetAsPrimary_Click);
            // 
            // buttonSetAsSecondary
            // 
            this.buttonSetAsSecondary.Location = new System.Drawing.Point(118, 148);
            this.buttonSetAsSecondary.Name = "buttonSetAsSecondary";
            this.buttonSetAsSecondary.Size = new System.Drawing.Size(100, 23);
            this.buttonSetAsSecondary.TabIndex = 1;
            this.buttonSetAsSecondary.Text = "Set as Secondary";
            this.buttonSetAsSecondary.UseVisualStyleBackColor = true;
            this.buttonSetAsSecondary.Click += new System.EventHandler(this.buttonSetAsSecondary_Click);
            // 
            // buttonResetDNS
            // 
            this.buttonResetDNS.Location = new System.Drawing.Point(224, 148);
            this.buttonResetDNS.Name = "buttonResetDNS";
            this.buttonResetDNS.Size = new System.Drawing.Size(75, 23);
            this.buttonResetDNS.TabIndex = 2;
            this.buttonResetDNS.Text = "Reset DNS";
            this.buttonResetDNS.UseVisualStyleBackColor = true;
            this.buttonResetDNS.Click += new System.EventHandler(this.buttonResetDNS_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Network Interface Names:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(224, 177);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current DNS Servers:";
            // 
            // labelDns
            // 
            this.labelDns.AutoSize = true;
            this.labelDns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDns.Location = new System.Drawing.Point(12, 113);
            this.labelDns.Name = "labelDns";
            this.labelDns.Size = new System.Drawing.Size(204, 32);
            this.labelDns.TabIndex = 11;
            this.labelDns.Text = "Primary DNS: 000.000.000.000\r\nSecondary DNS: 000.000.000.000";
            this.labelDns.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDns.TextChanged += new System.EventHandler(this.labelDns_TextChanged);
            // 
            // labelSelectedDns
            // 
            this.labelSelectedDns.AutoSize = true;
            this.labelSelectedDns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedDns.Location = new System.Drawing.Point(12, 52);
            this.labelSelectedDns.Name = "labelSelectedDns";
            this.labelSelectedDns.Size = new System.Drawing.Size(192, 16);
            this.labelSelectedDns.TabIndex = 12;
            this.labelSelectedDns.Text = "Selected DNS: 000.000.000.000";
            this.labelSelectedDns.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(139, 29);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(81, 17);
            this.checkBoxAutoSelect.TabIndex = 13;
            this.checkBoxAutoSelect.Text = "Auto Select";
            this.checkBoxAutoSelect.UseVisualStyleBackColor = true;
            this.checkBoxAutoSelect.CheckedChanged += new System.EventHandler(this.checkBoxAutoSelect_CheckedChanged);
            // 
            // NetworkInterfaceConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 212);
            this.Controls.Add(this.checkBoxAutoSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonResetDNS);
            this.Controls.Add(this.buttonSetAsSecondary);
            this.Controls.Add(this.buttonSetAsPrimary);
            this.Controls.Add(this.labelDns);
            this.Controls.Add(this.labelSelectedDns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetworkInterfaceConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Interface Config";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NetworkInterfaceConfigurationForm_FormClosed);
            this.Load += new System.EventHandler(this.NetworkInterfaceConfigurationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSetAsPrimary;
        private System.Windows.Forms.Button buttonSetAsSecondary;
        private System.Windows.Forms.Button buttonResetDNS;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDns;
        private System.Windows.Forms.Label labelSelectedDns;
        private System.Windows.Forms.CheckBox checkBoxAutoSelect;
    }
}