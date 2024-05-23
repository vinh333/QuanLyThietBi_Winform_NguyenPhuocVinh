using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh.KhoVatTu
{
    public partial class FormChiTietNhapXuat : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool isAddingNew = false;

        public FormChiTietNhapXuat()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private void FormChiTietNhapXuat_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = @"SELECT chitietnhapxuat.MaChiTiet, 
                                 chitietnhapxuat.MaVatTu, 
                                 chitietnhapxuat.SoLuong, 
                                 chitietnhapxuat.NgayNhapXuat, 
                                 chitietnhapxuat.LoaiGiaoDich
                         FROM chitietnhapxuat";
                DataTable dataTable = mySQLConnector.Select(query);
                gridControl1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void _showHide(bool kt)
        {
            splitContainer1.Panel1Collapsed = kt;

            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnThoat.Enabled = kt;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            isAddingNew = true;
            ClearInputs();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            DataRow row = gridView1.GetDataRow(rowIndex);
            txt_MaChiTiet.Text = row["MaChiTiet"].ToString();
            txt_MaVatTu.Text = row["MaVatTu"].ToString();
            txt_SoLuong.Text = row["SoLuong"].ToString();
            dateEdit_NgayNhapXuat.DateTime = Convert.ToDateTime(row["NgayNhapXuat"]);
            SelectComboBoxItem(cbo_LoaiGiaoDich, row["LoaiGiaoDich"].ToString());

            isAddingNew = false;
            _showHide(false);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow row = gridView1.GetDataRow(rowIndex);
                int maChiTiet = Convert.ToInt32(row["MaChiTiet"]);
                string query = $"DELETE FROM chitietnhapxuat WHERE MaChiTiet = {maChiTiet}";
                mySQLConnector.ExecuteQuery(query);
                LoadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int maVatTu = Convert.ToInt32(txt_MaVatTu.Text.Trim());
                int soLuong = Convert.ToInt32(txt_SoLuong.Text.Trim());
                DateTime ngayNhapXuat = dateEdit_NgayNhapXuat.DateTime;
                string loaiGiaoDich = cbo_LoaiGiaoDich.SelectedItem.ToString();

                if (isAddingNew)
                {
                    string query = $"INSERT INTO chitietnhapxuat (MaVatTu, SoLuong, NgayNhapXuat, LoaiGiaoDich) VALUES ({maVatTu}, {soLuong}, '{ngayNhapXuat:yyyy-MM-dd}', '{loaiGiaoDich}')";
                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    int maChiTiet = Convert.ToInt32(txt_MaChiTiet.Text);
                    string query = $"UPDATE chitietnhapxuat SET MaVatTu = {maVatTu}, SoLuong = {soLuong}, NgayNhapXuat = '{ngayNhapXuat:yyyy-MM-dd}', LoaiGiaoDich = '{loaiGiaoDich}' WHERE MaChiTiet = {maChiTiet}";
                    mySQLConnector.ExecuteQuery(query);
                }

                LoadData();
                _showHide(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(true);
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            txt_MaChiTiet.Text = "";
            txt_MaVatTu.Text = "";
            txt_SoLuong.Text = "";
            cbo_LoaiGiaoDich.SelectedIndex = -1;
            dateEdit_NgayNhapXuat.EditValue = null;
        }

        private void SelectComboBoxItem(System.Windows.Forms.ComboBox comboBox, string id)
        {
            foreach (var item in comboBox.Items)
            {
                if (comboBox.GetItemText(item) == id)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }
    }
}
