namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    partial class FormLogin
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnTogglePassword = new DevExpress.XtraEditors.LabelControl();
            this.txt_UserName = new DevExpress.XtraEditors.TextEdit();
            this.cbo_LoaiTaiKhoan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txt_PassWord = new DevExpress.XtraEditors.TextEdit();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_LoaiTaiKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PassWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(86)))), ((int)(((byte)(121)))));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(145, 464);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(110, 22);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Đăng nhập";
            this.labelControl3.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.Appearance.BackColor = System.Drawing.Color.White;
            this.btnTogglePassword.Appearance.Options.UseBackColor = true;
            this.btnTogglePassword.ImageOptions.Image = global::QuanLyThietBi_Winform_NguyenPhuocVinh.Properties.Resources.icons8_eye_30;
            this.btnTogglePassword.Location = new System.Drawing.Point(292, 183);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(30, 30);
            this.btnTogglePassword.TabIndex = 43;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(63, 183);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_UserName.Properties.Appearance.Options.UseFont = true;
            this.txt_UserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txt_UserName.Size = new System.Drawing.Size(225, 26);
            this.txt_UserName.TabIndex = 41;
            // 
            // cbo_LoaiTaiKhoan
            // 
            this.cbo_LoaiTaiKhoan.Location = new System.Drawing.Point(63, 357);
            this.cbo_LoaiTaiKhoan.Name = "cbo_LoaiTaiKhoan";
            this.cbo_LoaiTaiKhoan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_LoaiTaiKhoan.Properties.Appearance.Options.UseFont = true;
            this.cbo_LoaiTaiKhoan.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cbo_LoaiTaiKhoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_LoaiTaiKhoan.Properties.Items.AddRange(new object[] {
            "Quản trị viên",
            "Nhân viên",
            "Kỹ thuật viên"});
            this.cbo_LoaiTaiKhoan.Size = new System.Drawing.Size(225, 26);
            this.cbo_LoaiTaiKhoan.TabIndex = 40;
            // 
            // txt_PassWord
            // 
            this.txt_PassWord.Location = new System.Drawing.Point(63, 272);
            this.txt_PassWord.Name = "txt_PassWord";
            this.txt_PassWord.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_PassWord.Properties.Appearance.Options.UseFont = true;
            this.txt_PassWord.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txt_PassWord.Size = new System.Drawing.Size(225, 26);
            this.txt_PassWord.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::QuanLyThietBi_Winform_NguyenPhuocVinh.Properties.Resources.Borcelle99;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1091, 576);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 576);
            this.Controls.Add(this.btnTogglePassword);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.cbo_LoaiTaiKhoan);
            this.Controls.Add(this.txt_PassWord);
            this.Controls.Add(this.pictureBox2);
            this.Name = "FormLogin";
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.Form_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_LoaiTaiKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PassWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txt_PassWord;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_LoaiTaiKhoan;
        private DevExpress.XtraEditors.TextEdit txt_UserName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl btnTogglePassword;
    }
}