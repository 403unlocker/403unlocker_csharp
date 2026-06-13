namespace _403Unlocker.Find_DNS
{
    partial class FindByIPv4Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindByIPv4Form));
            this.textBoxOctet4 = new System.Windows.Forms.TextBox();
            this.textBoxOctet3 = new System.Windows.Forms.TextBox();
            this.textBoxOctet2 = new System.Windows.Forms.TextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOctet1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxOctet4
            // 
            this.textBoxOctet4.BackColor = System.Drawing.Color.White;
            this.textBoxOctet4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet4.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet4.Location = new System.Drawing.Point(196, 12);
            this.textBoxOctet4.MaxLength = 3;
            this.textBoxOctet4.Name = "textBoxOctet4";
            this.textBoxOctet4.ShortcutsEnabled = false;
            this.textBoxOctet4.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet4.TabIndex = 47;
            this.textBoxOctet4.TextChanged += new System.EventHandler(this.textBoxOctet1_TextChanged);
            this.textBoxOctet4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet4.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // textBoxOctet3
            // 
            this.textBoxOctet3.BackColor = System.Drawing.Color.White;
            this.textBoxOctet3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet3.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet3.Location = new System.Drawing.Point(157, 12);
            this.textBoxOctet3.MaxLength = 3;
            this.textBoxOctet3.Name = "textBoxOctet3";
            this.textBoxOctet3.ShortcutsEnabled = false;
            this.textBoxOctet3.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet3.TabIndex = 46;
            this.textBoxOctet3.TextChanged += new System.EventHandler(this.textBoxOctet1_TextChanged);
            this.textBoxOctet3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet3.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // textBoxOctet2
            // 
            this.textBoxOctet2.BackColor = System.Drawing.Color.White;
            this.textBoxOctet2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet2.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet2.Location = new System.Drawing.Point(118, 12);
            this.textBoxOctet2.MaxLength = 3;
            this.textBoxOctet2.Name = "textBoxOctet2";
            this.textBoxOctet2.ShortcutsEnabled = false;
            this.textBoxOctet2.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet2.TabIndex = 45;
            this.textBoxOctet2.TextChanged += new System.EventHandler(this.textBoxOctet1_TextChanged);
            this.textBoxOctet2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet2.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.White;
            this.buttonClose.ForeColor = System.Drawing.Color.Black;
            this.buttonClose.Location = new System.Drawing.Point(181, 40);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 49;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.Color.White;
            this.buttonFind.Enabled = false;
            this.buttonFind.ForeColor = System.Drawing.Color.Black;
            this.buttonFind.Location = new System.Drawing.Point(100, 40);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 23);
            this.buttonFind.TabIndex = 48;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "DNS IPv4: ";
            // 
            // textBoxOctet1
            // 
            this.textBoxOctet1.BackColor = System.Drawing.Color.White;
            this.textBoxOctet1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOctet1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOctet1.ForeColor = System.Drawing.Color.Black;
            this.textBoxOctet1.Location = new System.Drawing.Point(79, 12);
            this.textBoxOctet1.MaxLength = 3;
            this.textBoxOctet1.Name = "textBoxOctet1";
            this.textBoxOctet1.ShortcutsEnabled = false;
            this.textBoxOctet1.Size = new System.Drawing.Size(30, 22);
            this.textBoxOctet1.TabIndex = 44;
            this.textBoxOctet1.TextChanged += new System.EventHandler(this.textBoxOctet1_TextChanged);
            this.textBoxOctet1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOctets_KeyPress);
            this.textBoxOctet1.Validated += new System.EventHandler(this.textBoxOctets_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(186, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 16);
            this.label5.TabIndex = 53;
            this.label5.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(147, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 16);
            this.label4.TabIndex = 52;
            this.label4.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(108, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 16);
            this.label3.TabIndex = 51;
            this.label3.Text = ".";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 45);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(82, 13);
            this.labelResult.TabIndex = 54;
            this.labelResult.Text = "Result: 00 of 00";
            // 
            // FindByIPv4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 75);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.textBoxOctet4);
            this.Controls.Add(this.textBoxOctet3);
            this.Controls.Add(this.textBoxOctet2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOctet1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FindByIPv4Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find by IPv4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOctet4;
        private System.Windows.Forms.TextBox textBoxOctet3;
        private System.Windows.Forms.TextBox textBoxOctet2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOctet1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelResult;
    }
}