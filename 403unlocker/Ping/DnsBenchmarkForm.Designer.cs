namespace _403unlocker.Ping
{
    partial class DnsBenchmarkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DnsBenchmarkForm));
            this.pcPingButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyDnsCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortButton = new System.Windows.Forms.Button();
            this.sitePingButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.about403ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddDns = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonDnsSet = new System.Windows.Forms.Button();
            this.comboBoxDnsSet = new System.Windows.Forms.ComboBox();
            this.buttonResetDns = new System.Windows.Forms.Button();
            this.labelDnsCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcPingButton
            // 
            this.pcPingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.pcPingButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.pcPingButton.ForeColor = System.Drawing.Color.Black;
            this.pcPingButton.Location = new System.Drawing.Point(12, 371);
            this.pcPingButton.Name = "pcPingButton";
            this.pcPingButton.Size = new System.Drawing.Size(79, 23);
            this.pcPingButton.TabIndex = 10;
            this.pcPingButton.Text = "Ping";
            this.pcPingButton.UseVisualStyleBackColor = false;
            this.pcPingButton.Click += new System.EventHandler(this.pcPingButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "DNS Table:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.dataGridView1.Location = new System.Drawing.Point(13, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(405, 317);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.contextMenuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyDnsCellToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            // 
            // copyDnsCellToolStripMenuItem
            // 
            this.copyDnsCellToolStripMenuItem.Name = "copyDnsCellToolStripMenuItem";
            this.copyDnsCellToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.copyDnsCellToolStripMenuItem.Text = "Copy DNS";
            this.copyDnsCellToolStripMenuItem.Click += new System.EventHandler(this.copyDnsCellToolStripMenuItem_Click);
            // 
            // sortButton
            // 
            this.sortButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.sortButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sortButton.ForeColor = System.Drawing.Color.Black;
            this.sortButton.Location = new System.Drawing.Point(182, 371);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(79, 23);
            this.sortButton.TabIndex = 11;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = false;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // sitePingButton
            // 
            this.sitePingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.sitePingButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sitePingButton.ForeColor = System.Drawing.Color.Black;
            this.sitePingButton.Location = new System.Drawing.Point(97, 371);
            this.sitePingButton.Name = "sitePingButton";
            this.sitePingButton.Size = new System.Drawing.Size(79, 23);
            this.sitePingButton.TabIndex = 13;
            this.sitePingButton.Text = "Bypass";
            this.sitePingButton.UseVisualStyleBackColor = false;
            this.sitePingButton.Click += new System.EventHandler(this.sitePingButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(431, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about403ToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // about403ToolStripMenuItem
            // 
            this.about403ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.about403ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codeSourceToolStripMenuItem,
            this.websiteToolStripMenuItem});
            this.about403ToolStripMenuItem.Name = "about403ToolStripMenuItem";
            this.about403ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.about403ToolStripMenuItem.Text = "About 403unlocker";
            // 
            // codeSourceToolStripMenuItem
            // 
            this.codeSourceToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.codeSourceToolStripMenuItem.Name = "codeSourceToolStripMenuItem";
            this.codeSourceToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.codeSourceToolStripMenuItem.Text = "Code Source";
            this.codeSourceToolStripMenuItem.Click += new System.EventHandler(this.codeSourceToolStripMenuItem_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.websiteToolStripMenuItem.Text = "Website";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // buttonAddDns
            // 
            this.buttonAddDns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonAddDns.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAddDns.ForeColor = System.Drawing.Color.Black;
            this.buttonAddDns.Location = new System.Drawing.Point(340, 400);
            this.buttonAddDns.Name = "buttonAddDns";
            this.buttonAddDns.Size = new System.Drawing.Size(79, 23);
            this.buttonAddDns.TabIndex = 17;
            this.buttonAddDns.Text = "Add DNS";
            this.buttonAddDns.UseVisualStyleBackColor = false;
            this.buttonAddDns.Click += new System.EventHandler(this.buttonAddDns_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JSON File|*.json";
            this.openFileDialog1.ShowReadOnly = true;
            this.openFileDialog1.Title = "Select JSON File";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JSON File|*.json";
            this.saveFileDialog1.Title = "Select where to save";
            // 
            // buttonDnsSet
            // 
            this.buttonDnsSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonDnsSet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDnsSet.ForeColor = System.Drawing.Color.Black;
            this.buttonDnsSet.Location = new System.Drawing.Point(12, 400);
            this.buttonDnsSet.Name = "buttonDnsSet";
            this.buttonDnsSet.Size = new System.Drawing.Size(99, 21);
            this.buttonDnsSet.TabIndex = 18;
            this.buttonDnsSet.Text = "Set as Primary";
            this.buttonDnsSet.UseVisualStyleBackColor = false;
            this.buttonDnsSet.Click += new System.EventHandler(this.buttonDnsSet_Click);
            // 
            // comboBoxDnsSet
            // 
            this.comboBoxDnsSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.comboBoxDnsSet.FormattingEnabled = true;
            this.comboBoxDnsSet.Items.AddRange(new object[] {
            "Set as Primary",
            "Set as Secondary"});
            this.comboBoxDnsSet.Location = new System.Drawing.Point(12, 400);
            this.comboBoxDnsSet.Name = "comboBoxDnsSet";
            this.comboBoxDnsSet.Size = new System.Drawing.Size(115, 21);
            this.comboBoxDnsSet.TabIndex = 19;
            this.comboBoxDnsSet.Text = "Set as Primary45511551";
            this.comboBoxDnsSet.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonResetDns
            // 
            this.buttonResetDns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonResetDns.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonResetDns.ForeColor = System.Drawing.Color.Black;
            this.buttonResetDns.Location = new System.Drawing.Point(133, 400);
            this.buttonResetDns.Name = "buttonResetDns";
            this.buttonResetDns.Size = new System.Drawing.Size(79, 23);
            this.buttonResetDns.TabIndex = 20;
            this.buttonResetDns.Text = "Reset";
            this.buttonResetDns.UseVisualStyleBackColor = false;
            this.buttonResetDns.Click += new System.EventHandler(this.buttonResetDns_Click);
            // 
            // labelDnsCount
            // 
            this.labelDnsCount.AutoSize = true;
            this.labelDnsCount.Location = new System.Drawing.Point(359, 368);
            this.labelDnsCount.Name = "labelDnsCount";
            this.labelDnsCount.Size = new System.Drawing.Size(59, 13);
            this.labelDnsCount.TabIndex = 21;
            this.labelDnsCount.Text = "Count: 999";
            // 
            // DnsBenchmarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(431, 435);
            this.Controls.Add(this.labelDnsCount);
            this.Controls.Add(this.buttonResetDns);
            this.Controls.Add(this.buttonDnsSet);
            this.Controls.Add(this.buttonAddDns);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.sitePingButton);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.pcPingButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxDnsSet);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DnsBenchmarkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "403unlocker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DnsPingForm_FormClosing);
            this.Load += new System.EventHandler(this.DnsPingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pcPingButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button sitePingButton;
        private System.Windows.Forms.ToolStripMenuItem copyDnsCellToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button buttonAddDns;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem about403ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem codeSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.Button buttonDnsSet;
        private System.Windows.Forms.ComboBox comboBoxDnsSet;
        private System.Windows.Forms.Button buttonResetDns;
        private System.Windows.Forms.Label labelDnsCount;
    }
}