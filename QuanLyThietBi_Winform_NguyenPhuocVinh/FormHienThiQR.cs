using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormHienThiQR : DevExpress.XtraEditors.XtraForm
    {
        public FormHienThiQR()
        {
            InitializeComponent();
        }
        public FormHienThiQR(Image qrCodeImage, string model, string soSerial)
        {
            InitializeComponent();
            pictureBoxQR.Image = qrCodeImage;
            UpdateDeviceInfo(model, soSerial);
        }

        public void UpdateDeviceInfo(string model, string soSerial)
        {
            lblModel.Text = model;
            lblSoSerial.Text = soSerial;
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FileName = "QRCode.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxQR.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Mã QR đã được lưu thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu mã QR: " + ex.Message);
                }
            }
        }

    }
}