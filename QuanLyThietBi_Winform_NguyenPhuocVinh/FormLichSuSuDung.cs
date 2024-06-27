using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormLichSuSuDung : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;

        public FormLichSuSuDung()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private void FormDanhSachThietBi_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp

            LoadData();
            LoadComboBoxData(); // Load dữ liệu cho các ComboBox
        }

        private void LoadComboBoxData()
        {
            // Load data for cbo_ThietBi, cbo_NguoiDung
            try
            {
                string query;
                // Load data for cbo_loaithietbi
                query = "SELECT TenThietBi FROM thietbi";
                DataTable dtBoPhan = mySQLConnector.Select(query);
                foreach (DataRow row in dtBoPhan.Rows)
                {
                    cbo_ThietBi.Items.Add(row["TenThietBi"].ToString());
                }

                // Load data for cbo_ChucVu
                query = "SELECT HOTEN FROM nhanvien";
                DataTable dtChucVu = mySQLConnector.Select(query);
                foreach (DataRow row in dtChucVu.Rows)
                {
                    cbo_NguoiDung.Items.Add(row["HOTEN"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu ComboBox: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM lichsusudung";
                DataTable dataTable = mySQLConnector.Select(query);

                // Cập nhật trạng thái cho từng hàng và cập nhật vào cơ sở dữ liệu
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                    string trangThaiMoi;

                    if (row["TrangThai"].ToString() == "Đã trả")
                    {
                        trangThaiMoi = "Đã trả";
                    }
                    else if (ngayKetThuc < DateTime.Today)
                    {
                        trangThaiMoi = "Đã hết hạn";
                    }
                    else if (ngayKetThuc == DateTime.Today)
                    {
                        trangThaiMoi = "Sắp hết hạn";
                    }
                    else
                    {
                        trangThaiMoi = "Đang mượn";
                    }

                    row["TrangThai"] = trangThaiMoi;

                    // Cập nhật trạng thái vào cơ sở dữ liệu
                    string updateQuery = $"UPDATE lichsusudung SET TrangThai = '{trangThaiMoi}' WHERE MaLichSuSuDung = {row["MaLichSuSuDung"]}";
                    mySQLConnector.ExecuteQuery(updateQuery);
                }

                // Gán DataSource cho GridControl
                gridControl1.DataSource = dataTable;

                // Áp dụng định dạng điều kiện cho GridControl dựa trên cột TrangThai
                gridView1.FormatConditions.Clear();

                StyleFormatCondition conditionExpired = new StyleFormatCondition(FormatConditionEnum.Equal, gridView1.Columns["TrangThai"], null, "Đã hết hạn");
                conditionExpired.Appearance.BackColor = Color.Red;
                gridView1.FormatConditions.Add(conditionExpired);

                StyleFormatCondition conditionExpiring = new StyleFormatCondition(FormatConditionEnum.Equal, gridView1.Columns["TrangThai"], null, "Sắp hết hạn");
                conditionExpiring.Appearance.BackColor = Color.Yellow;
                gridView1.FormatConditions.Add(conditionExpiring);

                StyleFormatCondition conditionBorrowed = new StyleFormatCondition(FormatConditionEnum.Equal, gridView1.Columns["TrangThai"], null, "Đang mượn");
                conditionBorrowed.Appearance.BackColor = Color.LightGreen;
                gridView1.FormatConditions.Add(conditionBorrowed);
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
            btn_TraThietBi.Enabled = kt;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            checkbutton = true;
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
            int maLichSuSuDung = Convert.ToInt32(row["MaLichSuSuDung"]);
            int maThietBi = Convert.ToInt32(row["MaThietBi"]);
            int MANV = Convert.ToInt32(row["MANV"]);
            string mucDichSuDung = row["MucDichSuDung"].ToString();
            DateTime ngayBatDau = Convert.ToDateTime(row["NgayBatDau"]);
            DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);

            // Hiển thị thông tin lên các controls tương ứng
            SelectComboBoxItem(cbo_ThietBi, GetTenById(maThietBi.ToString(), "thietbi", "TenThietBi", "MaThietBi"));
            SelectComboBoxItem(cbo_NguoiDung, GetTenById(MANV.ToString(), "nhanvien", "HOTEN", "MANV"));
            txt_MucDich.Text = mucDichSuDung;
            dtp_NgayBatDau.Value = ngayBatDau;
            dtp_NgayKetThuc.Value = ngayKetThuc;

            checkbutton = false;
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
                int maLichSuSuDung = Convert.ToInt32(row["MaLichSuSuDung"]);
                string query = $"DELETE FROM lichsusudung WHERE MaLichSuSuDung = {maLichSuSuDung}";
                mySQLConnector.ExecuteQuery(query);
                LoadData(); // Hàm LoadData() dùng để load lại dữ liệu sau khi xóa
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowIndex = gridView1.FocusedRowHandle;
                int maThietBi = GetIDByTen(cbo_ThietBi.SelectedItem.ToString(), "ThietBi", "TenThietBi", "MaThietBi");
                int MANV = GetIDByTen(cbo_NguoiDung.SelectedItem.ToString(), "nhanvien", "HOTEN", "MANV");
                string mucDichSuDung = txt_MucDich.Text.Trim();
                DateTime ngayBatDau = dtp_NgayBatDau.Value;
                DateTime ngayKetThuc = dtp_NgayKetThuc.Value;

                string trangThai;
                if (ngayKetThuc < DateTime.Today)
                {
                    trangThai = "Đã hết hạn";
                }
                else if (ngayKetThuc == DateTime.Today)
                {
                    trangThai = "Sắp hết hạn";
                }
                else
                {
                    trangThai = "Đang mượn";
                }

                if (checkbutton)
                {
                    // Thêm mới
                    string query = $"INSERT INTO lichsusudung (MaThietBi, MANV, MucDichSuDung, NgayBatDau, NgayKetThuc, TrangThai) " +
                                   $"VALUES ({maThietBi}, {MANV}, '{mucDichSuDung}', '{ngayBatDau.ToString("yyyy-MM-dd")}', '{ngayKetThuc.ToString("yyyy-MM-dd")}', '{trangThai}')";

                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    DataRow row = gridView1.GetDataRow(rowIndex);
                    int maLichSuSuDung = Convert.ToInt32(row["MaLichSuSuDung"]);
                    string query = $"UPDATE lichsusudung SET MaThietBi = {maThietBi}, MANV = {MANV}, MucDichSuDung = '{mucDichSuDung}', " +
                                   $"NgayBatDau = '{ngayBatDau.ToString("yyyy-MM-dd")}', NgayKetThuc = '{ngayKetThuc.ToString("yyyy-MM-dd")}', TrangThai = '{trangThai}' " +
                                   $"WHERE MaLichSuSuDung = {maLichSuSuDung}";

                    mySQLConnector.ExecuteQuery(query);
                }

                LoadData(); // Load lại dữ liệu sau khi thêm mới hoặc sửa
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

        private void btn_TraThietBi_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để trả thiết bị.");
                return;
            }

            DataRow row = gridView1.GetDataRow(rowIndex);
            int maLichSuSuDung = Convert.ToInt32(row["MaLichSuSuDung"]);

            string query = $"UPDATE lichsusudung SET TrangThai = 'Đã trả' WHERE MaLichSuSuDung = {maLichSuSuDung}";
            mySQLConnector.ExecuteQuery(query);
            LoadData(); // Load lại dữ liệu sau khi trả thiết bị
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private int GetIDByTen(string ten, string tableName, string tenColumn, string idColumn)
        {
            string query = $"SELECT {idColumn} FROM {tableName} WHERE {tenColumn} = '{ten}'";
            DataTable dt = mySQLConnector.Select(query);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][idColumn]);
            }
            return -1;
        }

        private string GetTenById(string id, string tableName, string tenColumn, string idColumn)
        {
            string query = $"SELECT {tenColumn} FROM {tableName} WHERE {idColumn} = '{id}'";
            DataTable dt = mySQLConnector.Select(query);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][tenColumn].ToString();
            }
            return null;
        }

        private void SelectComboBoxItem(System.Windows.Forms.ComboBox comboBox, string item)
        {
            if (item == null)
                return;

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == item)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        private void ClearInputs()
        {
            cbo_ThietBi.SelectedIndex = -1;
            cbo_NguoiDung.SelectedIndex = -1;
            txt_MucDich.Clear();
            dtp_NgayBatDau.Value = DateTime.Today;
            dtp_NgayKetThuc.Value = DateTime.Today;
        }
    }
}
