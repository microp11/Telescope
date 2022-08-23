namespace telescope
{
    partial class FormTelescope
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
            this.rtbListener = new System.Windows.Forms.RichTextBox();
            this.ServerIP = new System.Windows.Forms.TextBox();
            this.ServerPort = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelTrackingData = new System.Windows.Forms.Label();
            this.Chk360 = new System.Windows.Forms.RadioButton();
            this.Chk0 = new System.Windows.Forms.RadioButton();
            this.ChkN = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbListener
            // 
            this.rtbListener.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbListener.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbListener.Location = new System.Drawing.Point(13, 119);
            this.rtbListener.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtbListener.MaxLength = 2000000000;
            this.rtbListener.Name = "rtbListener";
            this.rtbListener.Size = new System.Drawing.Size(358, 353);
            this.rtbListener.TabIndex = 12;
            this.rtbListener.Text = "";
            // 
            // ServerIP
            // 
            this.ServerIP.Location = new System.Drawing.Point(16, 25);
            this.ServerIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(102, 23);
            this.ServerIP.TabIndex = 13;
            this.ServerIP.Text = "127.0.0.1";
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(126, 25);
            this.ServerPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ServerPort.Maximum = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.ServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(70, 23);
            this.ServerPort.TabIndex = 15;
            this.ServerPort.Value = new decimal(new int[] {
            10001,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 38);
            this.button1.TabIndex = 16;
            this.button1.Text = "Start";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(13, 9);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(52, 15);
            this.labelAddress.TabIndex = 17;
            this.labelAddress.Text = "Address:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(123, 9);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(32, 15);
            this.labelPort.TabIndex = 18;
            this.labelPort.Text = "Port:";
            // 
            // labelTrackingData
            // 
            this.labelTrackingData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTrackingData.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelTrackingData.Location = new System.Drawing.Point(13, 64);
            this.labelTrackingData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTrackingData.Name = "labelTrackingData";
            this.labelTrackingData.Size = new System.Drawing.Size(341, 41);
            this.labelTrackingData.TabIndex = 31;
            this.labelTrackingData.Text = "?";
            this.labelTrackingData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Chk360
            // 
            this.Chk360.AutoSize = true;
            this.Chk360.Location = new System.Drawing.Point(16, 478);
            this.Chk360.Name = "Chk360";
            this.Chk360.Size = new System.Drawing.Size(51, 19);
            this.Chk360.TabIndex = 32;
            this.Chk360.Text = "360+";
            this.Chk360.UseVisualStyleBackColor = true;
            // 
            // Chk0
            // 
            this.Chk0.AutoSize = true;
            this.Chk0.Location = new System.Drawing.Point(79, 478);
            this.Chk0.Name = "Chk0";
            this.Chk0.Size = new System.Drawing.Size(39, 19);
            this.Chk0.TabIndex = 33;
            this.Chk0.Text = "0+";
            this.Chk0.UseVisualStyleBackColor = true;
            // 
            // ChkN
            // 
            this.ChkN.AutoSize = true;
            this.ChkN.Checked = true;
            this.ChkN.Location = new System.Drawing.Point(141, 478);
            this.ChkN.Name = "ChkN";
            this.ChkN.Size = new System.Drawing.Size(127, 19);
            this.ChkN.TabIndex = 35;
            this.ChkN.TabStop = true;
            this.ChkN.Text = "AZ spillover neutral";
            this.ChkN.UseVisualStyleBackColor = true;
            // 
            // FormTelescope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 501);
            this.Controls.Add(this.ChkN);
            this.Controls.Add(this.Chk0);
            this.Controls.Add(this.Chk360);
            this.Controls.Add(this.ServerPort);
            this.Controls.Add(this.labelTrackingData);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.rtbListener);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(100, 282);
            this.Name = "FormTelescope";
            this.Text = "Stellarium-Orbitron, microp11, v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTelescope_FormClosing);
            this.Load += new System.EventHandler(this.FormTelescope_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbListener;
        private System.Windows.Forms.TextBox ServerIP;
        private System.Windows.Forms.NumericUpDown ServerPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelTrackingData;
        private System.Windows.Forms.RadioButton Chk360;
        private System.Windows.Forms.RadioButton Chk0;
        private System.Windows.Forms.RadioButton ChkN;
    }
}

