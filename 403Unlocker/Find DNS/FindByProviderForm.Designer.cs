namespace _403Unlocker.Find_DNS
{
    partial class FindByProviderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindByProviderForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProvider = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelResult = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.White;
            this.buttonClose.ForeColor = System.Drawing.Color.Black;
            this.buttonClose.Location = new System.Drawing.Point(383, 40);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 42;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.Color.White;
            this.buttonFind.ForeColor = System.Drawing.Color.Black;
            this.buttonFind.Location = new System.Drawing.Point(302, 40);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 23);
            this.buttonFind.TabIndex = 41;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Provider: ";
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.BackColor = System.Drawing.Color.White;
            this.textBoxProvider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxProvider.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProvider.ForeColor = System.Drawing.Color.Black;
            this.textBoxProvider.Location = new System.Drawing.Point(70, 12);
            this.textBoxProvider.MaxLength = 50;
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.Size = new System.Drawing.Size(388, 22);
            this.textBoxProvider.TabIndex = 40;
            this.textBoxProvider.TextChanged += new System.EventHandler(this.textBoxProvider_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 45);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(82, 13);
            this.labelResult.TabIndex = 44;
            this.labelResult.Text = "Result: 00 of 00";
            // 
            // FindByProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 75);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxProvider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FindByProviderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find by Provider";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProvider;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label labelResult;
    }
}