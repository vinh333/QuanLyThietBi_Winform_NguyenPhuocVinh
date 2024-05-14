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

namespace QuanLyThietBi_Winform_NguyenPhuocVinh.Popup
{
    public partial class PopupSoLuongNhapXuat : DevExpress.XtraEditors.XtraForm
    {
        public int Quantity { get; private set; }

        public PopupSoLuongNhapXuat()
        {
            InitializeComponent();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Quantity = (int)numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}