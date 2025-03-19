namespace _403unlocker.Notification
{
    partial class MessageBoxProgress
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProgressBarStatus = new System.Windows.Forms.Label();
            this.labelTimeStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 28);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 15);
            this.progressBar1.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(120, 49);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelProgressBarStatus
            // 
            this.labelProgressBarStatus.AutoSize = true;
            this.labelProgressBarStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressBarStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.labelProgressBarStatus.Location = new System.Drawing.Point(9, 9);
            this.labelProgressBarStatus.Name = "labelProgressBarStatus";
            this.labelProgressBarStatus.Size = new System.Drawing.Size(94, 16);
            this.labelProgressBarStatus.TabIndex = 9;
            this.labelProgressBarStatus.Text = "99% Complete";
            // 
            // labelTimeStatus
            // 
            this.labelTimeStatus.AutoSize = true;
            this.labelTimeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(212)))), ((int)(((byte)(191)))));
            this.labelTimeStatus.Location = new System.Drawing.Point(155, 9);
            this.labelTimeStatus.Name = "labelTimeStatus";
            this.labelTimeStatus.Size = new System.Drawing.Size(147, 16);
            this.labelTimeStatus.TabIndex = 10;
            this.labelTimeStatus.Text = "Estimated Time:    99:99";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MessageBoxProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(310, 80);
            this.ControlBox = false;
            this.Controls.Add(this.labelTimeStatus);
            this.Controls.Add(this.labelProgressBarStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Processing...";
            this.Load += new System.EventHandler(this.WaitingThreadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        internal System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgressBarStatus;
        private System.Windows.Forms.Label labelTimeStatus;
        private System.Windows.Forms.Timer timer1;
    }
}