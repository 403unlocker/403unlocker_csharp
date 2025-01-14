namespace _403unlocker
{
    partial class CustomeDNSForm
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
            this.providerTextBox = new System.Windows.Forms.TextBox();
            this.primaryDnsTextBox = new System.Windows.Forms.TextBox();
            this.secondaryDnsTextBox = new System.Windows.Forms.TextBox();
            this.providerLabel = new System.Windows.Forms.Label();
            this.primaryDnsLabel = new System.Windows.Forms.Label();
            this.secondaryDnsLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // providerTextBox
            // 
            this.providerTextBox.BackColor = System.Drawing.Color.Red;
            this.providerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.providerTextBox.ForeColor = System.Drawing.Color.Black;
            this.providerTextBox.Location = new System.Drawing.Point(119, 12);
            this.providerTextBox.Name = "providerTextBox";
            this.providerTextBox.Size = new System.Drawing.Size(100, 20);
            this.providerTextBox.TabIndex = 0;
            this.providerTextBox.Validated += new System.EventHandler(this.providerTextBox_Validated);
            // 
            // primaryDnsTextBox
            // 
            this.primaryDnsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.primaryDnsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.primaryDnsTextBox.ForeColor = System.Drawing.Color.Black;
            this.primaryDnsTextBox.Location = new System.Drawing.Point(119, 38);
            this.primaryDnsTextBox.Name = "primaryDnsTextBox";
            this.primaryDnsTextBox.Size = new System.Drawing.Size(100, 20);
            this.primaryDnsTextBox.TabIndex = 1;
            this.primaryDnsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.primaryDnsTextBox_KeyPress);
            this.primaryDnsTextBox.Validated += new System.EventHandler(this.primaryDnsTextBox_Validated);
            // 
            // secondaryDnsTextBox
            // 
            this.secondaryDnsTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.secondaryDnsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.secondaryDnsTextBox.ForeColor = System.Drawing.Color.Black;
            this.secondaryDnsTextBox.Location = new System.Drawing.Point(119, 64);
            this.secondaryDnsTextBox.Name = "secondaryDnsTextBox";
            this.secondaryDnsTextBox.Size = new System.Drawing.Size(100, 20);
            this.secondaryDnsTextBox.TabIndex = 2;
            this.secondaryDnsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.secondaryDnsTextBox_KeyPress);
            this.secondaryDnsTextBox.Validated += new System.EventHandler(this.secondaryDnsTextBox_Validated);
            // 
            // providerLabel
            // 
            this.providerLabel.AutoSize = true;
            this.providerLabel.Location = new System.Drawing.Point(12, 9);
            this.providerLabel.Name = "providerLabel";
            this.providerLabel.Size = new System.Drawing.Size(49, 13);
            this.providerLabel.TabIndex = 3;
            this.providerLabel.Text = "Provider:";
            // 
            // primaryDnsLabel
            // 
            this.primaryDnsLabel.AutoSize = true;
            this.primaryDnsLabel.Location = new System.Drawing.Point(12, 37);
            this.primaryDnsLabel.Name = "primaryDnsLabel";
            this.primaryDnsLabel.Size = new System.Drawing.Size(70, 13);
            this.primaryDnsLabel.TabIndex = 4;
            this.primaryDnsLabel.Text = "Primary DNS:";
            // 
            // secondaryDnsLabel
            // 
            this.secondaryDnsLabel.AutoSize = true;
            this.secondaryDnsLabel.Location = new System.Drawing.Point(12, 65);
            this.secondaryDnsLabel.Name = "secondaryDnsLabel";
            this.secondaryDnsLabel.Size = new System.Drawing.Size(87, 13);
            this.secondaryDnsLabel.TabIndex = 5;
            this.secondaryDnsLabel.Text = "Secondary DNS:";
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addButton.ForeColor = System.Drawing.Color.Black;
            this.addButton.Location = new System.Drawing.Point(32, 90);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 6;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(144, 90);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CustomeDNSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(242, 111);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.secondaryDnsLabel);
            this.Controls.Add(this.primaryDnsLabel);
            this.Controls.Add(this.providerLabel);
            this.Controls.Add(this.secondaryDnsTextBox);
            this.Controls.Add(this.primaryDnsTextBox);
            this.Controls.Add(this.providerTextBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomeDNSForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add DNS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label providerLabel;
        private System.Windows.Forms.Label primaryDnsLabel;
        private System.Windows.Forms.Label secondaryDnsLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        internal System.Windows.Forms.TextBox providerTextBox;
        internal System.Windows.Forms.TextBox primaryDnsTextBox;
        internal System.Windows.Forms.TextBox secondaryDnsTextBox;
    }
}