namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    partial class FormHienThiQR
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
            this.lblModel = new System.Windows.Forms.Label();
            this.lblSoSerial = new System.Windows.Forms.Label();
            this.pictureBoxQR = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).BeginInit();
            this.SuspendLayout();
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblModel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblModel.Location = new System.Drawing.Point(276, 18);
            this.lblModel.Name = "lblModel";
            this.lblModel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblModel.Size = new System.Drawing.Size(162, 16);
            this.lblModel.TabIndex = 1;
            this.lblModel.Text = "Điện thoại samsung S24";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoSerial
            // 
            this.lblSoSerial.AutoSize = true;
            this.lblSoSerial.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoSerial.Location = new System.Drawing.Point(166, 19);
            this.lblSoSerial.Name = "lblSoSerial";
            this.lblSoSerial.Size = new System.Drawing.Size(71, 16);
            this.lblSoSerial.TabIndex = 2;
            this.lblSoSerial.Text = "Sn234123";
            this.lblSoSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxQR
            // 
            this.pictureBoxQR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxQR.Image = global::QuanLyThietBi_Winform_NguyenPhuocVinh.Properties.Resources.qrcode_default;
            this.pictureBoxQR.Location = new System.Drawing.Point(127, 53);
            this.pictureBoxQR.Name = "pictureBoxQR";
            this.pictureBoxQR.Size = new System.Drawing.Size(340, 329);
            this.pictureBoxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxQR.TabIndex = 0;
            this.pictureBoxQR.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(256, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "|";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Lime;
            this.button1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.ForeColor = System.Drawing.Color.MintCream;
            this.button1.Location = new System.Drawing.Point(247, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "Downloads";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // FormHienThiQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 481);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSoSerial);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.pictureBoxQR);
            this.Name = "FormHienThiQR";
            this.Text = "FormHienThiQR";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxQR;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblSoSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}