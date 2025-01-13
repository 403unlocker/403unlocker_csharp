namespace _403unlocker
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dnsTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.defaultDnsButton = new System.Windows.Forms.Button();
            this.clearDnsButton = new System.Windows.Forms.Button();
            this.scrapDnsButton = new System.Windows.Forms.Button();
            this.timerLabel = new System.Windows.Forms.Label();
            this.publicDnsTimer = new System.Windows.Forms.Timer(this.components);
            this.customeDnsButton = new System.Windows.Forms.Button();
            this.dnsCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dnsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dnsTable
            // 
            this.dnsTable.AllowUserToAddRows = false;
            this.dnsTable.AllowUserToDeleteRows = false;
            this.dnsTable.AllowUserToResizeRows = false;
            this.dnsTable.BackgroundColor = System.Drawing.Color.Black;
            this.dnsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dnsTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dnsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dnsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dnsTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.dnsTable.EnableHeadersVisualStyles = false;
            this.dnsTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.dnsTable.Location = new System.Drawing.Point(16, 28);
            this.dnsTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dnsTable.MultiSelect = false;
            this.dnsTable.Name = "dnsTable";
            this.dnsTable.ReadOnly = true;
            this.dnsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dnsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dnsTable.RowHeadersVisible = false;
            this.dnsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dnsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dnsTable.Size = new System.Drawing.Size(242, 214);
            this.dnsTable.TabIndex = 3;
            this.dnsTable.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dnsTable_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "DNS Table:";
            // 
            // defaultDnsButton
            // 
            this.defaultDnsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.defaultDnsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.defaultDnsButton.ForeColor = System.Drawing.Color.Black;
            this.defaultDnsButton.Location = new System.Drawing.Point(97, 261);
            this.defaultDnsButton.Name = "defaultDnsButton";
            this.defaultDnsButton.Size = new System.Drawing.Size(79, 35);
            this.defaultDnsButton.TabIndex = 6;
            this.defaultDnsButton.Text = "Add Default DNS";
            this.defaultDnsButton.UseVisualStyleBackColor = false;
            this.defaultDnsButton.Click += new System.EventHandler(this.defaultDnsButton_Click);
            // 
            // clearDnsButton
            // 
            this.clearDnsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.clearDnsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearDnsButton.ForeColor = System.Drawing.Color.Black;
            this.clearDnsButton.Location = new System.Drawing.Point(12, 261);
            this.clearDnsButton.Name = "clearDnsButton";
            this.clearDnsButton.Size = new System.Drawing.Size(79, 35);
            this.clearDnsButton.TabIndex = 7;
            this.clearDnsButton.Text = "Clear Table";
            this.clearDnsButton.UseVisualStyleBackColor = false;
            this.clearDnsButton.Click += new System.EventHandler(this.clearDnsButton_Click);
            // 
            // scrapDnsButton
            // 
            this.scrapDnsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.scrapDnsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.scrapDnsButton.ForeColor = System.Drawing.Color.Black;
            this.scrapDnsButton.Location = new System.Drawing.Point(182, 261);
            this.scrapDnsButton.Name = "scrapDnsButton";
            this.scrapDnsButton.Size = new System.Drawing.Size(79, 35);
            this.scrapDnsButton.TabIndex = 8;
            this.scrapDnsButton.Text = "Add Public DNS";
            this.scrapDnsButton.UseVisualStyleBackColor = false;
            this.scrapDnsButton.Click += new System.EventHandler(this.scrapDnsButton_Click);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(179, 245);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timerLabel.Size = new System.Drawing.Size(76, 13);
            this.timerLabel.TabIndex = 9;
            this.timerLabel.Text = "Seconds Left: ";
            // 
            // publicDnsTimer
            // 
            this.publicDnsTimer.Interval = 1000;
            this.publicDnsTimer.Tick += new System.EventHandler(this.publicDnsTimer_Tick);
            // 
            // customeDnsButton
            // 
            this.customeDnsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.customeDnsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.customeDnsButton.ForeColor = System.Drawing.Color.Black;
            this.customeDnsButton.Location = new System.Drawing.Point(97, 302);
            this.customeDnsButton.Name = "customeDnsButton";
            this.customeDnsButton.Size = new System.Drawing.Size(79, 35);
            this.customeDnsButton.TabIndex = 10;
            this.customeDnsButton.Text = "Add Custome DNS";
            this.customeDnsButton.UseVisualStyleBackColor = false;
            // 
            // dnsCountLabel
            // 
            this.dnsCountLabel.AutoSize = true;
            this.dnsCountLabel.Location = new System.Drawing.Point(12, 245);
            this.dnsCountLabel.Name = "dnsCountLabel";
            this.dnsCountLabel.Size = new System.Drawing.Size(64, 13);
            this.dnsCountLabel.TabIndex = 11;
            this.dnsCountLabel.Text = "DNS Count:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(273, 344);
            this.Controls.Add(this.scrapDnsButton);
            this.Controls.Add(this.dnsCountLabel);
            this.Controls.Add(this.customeDnsButton);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.clearDnsButton);
            this.Controls.Add(this.defaultDnsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dnsTable);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dnsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button defaultDnsButton;
        private System.Windows.Forms.Button clearDnsButton;
        private System.Windows.Forms.Button scrapDnsButton;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Timer publicDnsTimer;
        private System.Windows.Forms.DataGridView dnsTable;
        private System.Windows.Forms.Button customeDnsButton;
        private System.Windows.Forms.Label dnsCountLabel;
    }
}

