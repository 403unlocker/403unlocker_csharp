namespace _403Unlocker.Add_DNS
{
    partial class CustomDnsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDnsForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOctet1 = new System.Windows.Forms.TextBox();
            this.textBoxProvider = new System.Windows.Forms.TextBox();
            this.textBoxOctet2 = new System.Windows.Forms.TextBox();
            this.textBoxOctet3 = new System.Windows.Forms.TextBox();
            this.textBoxOctet4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.White;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(386, 68);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.White;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.Location = new System.Drawing.Point(305, 68);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "DNS IPv4: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Provider: ";
            // 
            // textBoxOctet1
            // 
            this.textBoxOctet1.BackColor = System.Drawing.Color.White;
            this.textBoxOctet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxOctet1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet1.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet1.Location = new System.Drawing.Point(79, 12);
            this.textBoxOctet1.MaxLength = 3;
            this.textBoxOctet1.Name = "textBoxOctet1";
            this.textBoxOctet1.ShortcutsEnabled = false;
            this.textBoxOctet1.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet1.TabIndex = 1;
            this.textBoxOctet1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet1.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.BackColor = System.Drawing.Color.White;
            this.textBoxProvider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxProvider.ContextMenuStrip = this.contextMenuStrip2;
            this.textBoxProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProvider.ForeColor = System.Drawing.Color.Black;
            this.textBoxProvider.Location = new System.Drawing.Point(79, 40);
            this.textBoxProvider.MaxLength = 50;
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.Size = new System.Drawing.Size(382, 22);
            this.textBoxProvider.TabIndex = 5;
            // 
            // textBoxOctet2
            // 
            this.textBoxOctet2.BackColor = System.Drawing.Color.White;
            this.textBoxOctet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet2.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxOctet2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet2.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet2.Location = new System.Drawing.Point(118, 12);
            this.textBoxOctet2.MaxLength = 3;
            this.textBoxOctet2.Name = "textBoxOctet2";
            this.textBoxOctet2.ShortcutsEnabled = false;
            this.textBoxOctet2.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet2.TabIndex = 2;
            this.textBoxOctet2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet2.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // textBoxOctet3
            // 
            this.textBoxOctet3.BackColor = System.Drawing.Color.White;
            this.textBoxOctet3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet3.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxOctet3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet3.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet3.Location = new System.Drawing.Point(157, 12);
            this.textBoxOctet3.MaxLength = 3;
            this.textBoxOctet3.Name = "textBoxOctet3";
            this.textBoxOctet3.ShortcutsEnabled = false;
            this.textBoxOctet3.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet3.TabIndex = 3;
            this.textBoxOctet3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet3.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // textBoxOctet4
            // 
            this.textBoxOctet4.BackColor = System.Drawing.Color.White;
            this.textBoxOctet4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet4.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxOctet4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet4.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet4.Location = new System.Drawing.Point(196, 12);
            this.textBoxOctet4.MaxLength = 3;
            this.textBoxOctet4.Name = "textBoxOctet4";
            this.textBoxOctet4.ShortcutsEnabled = false;
            this.textBoxOctet4.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet4.TabIndex = 4;
            this.textBoxOctet4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet4.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(108, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(147, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(186, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = ".";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem1.Text = "Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(103, 26);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click_1);
            // 
            // CustomDnsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 103);
            this.Controls.Add(this.textBoxOctet4);
            this.Controls.Add(this.textBoxOctet3);
            this.Controls.Add(this.textBoxOctet2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOctet1);
            this.Controls.Add(this.textBoxProvider);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomDnsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom DNS";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOctet1;
        private System.Windows.Forms.TextBox textBoxProvider;
        private System.Windows.Forms.TextBox textBoxOctet2;
        private System.Windows.Forms.TextBox textBoxOctet3;
        private System.Windows.Forms.TextBox textBoxOctet4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
    }
}