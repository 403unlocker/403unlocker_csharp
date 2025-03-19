namespace _403unlocker.ByPass_Url
{
    partial class GetUrlForm
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
            this.dnsCountLabel = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxUrl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dnsCountLabel
            // 
            this.dnsCountLabel.AutoSize = true;
            this.dnsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnsCountLabel.Location = new System.Drawing.Point(12, 15);
            this.dnsCountLabel.Name = "dnsCountLabel";
            this.dnsCountLabel.Size = new System.Drawing.Size(38, 15);
            this.dnsCountLabel.TabIndex = 18;
            this.dnsCountLabel.Text = "URL: ";
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOk.ForeColor = System.Drawing.Color.Black;
            this.buttonOk.Location = new System.Drawing.Point(82, 42);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(79, 23);
            this.buttonOk.TabIndex = 16;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(167, 42);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 23);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxUrl
            // 
            this.comboBoxUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.comboBoxUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxUrl.FormattingEnabled = true;
            this.comboBoxUrl.Location = new System.Drawing.Point(56, 12);
            this.comboBoxUrl.Name = "comboBoxUrl";
            this.comboBoxUrl.Size = new System.Drawing.Size(190, 24);
            this.comboBoxUrl.TabIndex = 20;
            // 
            // GetUrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(258, 77);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dnsCountLabel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxUrl);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetUrlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please enter URL...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetUrlForm_FormClosing);
            this.Load += new System.EventHandler(this.GetUrlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dnsCountLabel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.ComboBox comboBoxUrl;
    }
}