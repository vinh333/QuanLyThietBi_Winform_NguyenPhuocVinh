using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

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
            // Load data for cbo_BoPhan, cbo_ChucVu, cbo_DanToc, cbo_PhongBan, cbo_TonGiao, cbo_TrinhDo
            try
            {
                string query;
                // Load data for cbo_loaithietbi
                query = "SELECT TenThietBi FROM thietbi"; // Chỉnh sửa query tương ứng
                DataTable dtBoPhan = mySQLConnector.Select(query);
                foreach (DataRow row in dtBoPhan.Rows)
                {
                    cbo_ThietBi.Items.Add(row["TenThietBi"].ToString());
                }

                // Load data for cbo_ChucVu
                query = "SELECT HOTEN FROM nhanvien"; // Chỉnh sửa query tương ứng
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

                // Thêm một cột mới để lưu thông tin trạng thái cho mỗi hàng
                dataTable.Columns.Add("TrangThai", typeof(string));

                // Kiểm tra và thiết lập trạng thái cho từng hàng trong DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]);
                    if (ngayKetThuc < DateTime.Today)
                    {
                        // Thiết lập trạng thái "Đã hết hạn" cho các hàng có NgayKetThuc trước ngày hiện tại
                        row["TrangThai"] = "Đã hết hạn";
                    }
                    else if (ngayKetThuc < DateTime.Today.AddDays(1))
                    {
                        // Thiết lập trạng thái "Sắp hết hạn" cho các hàng có NgayKetThuc là ngày hôm sau
                        row["TrangThai"] = "Sắp hết hạn";
                    }
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
            btnIn.Enabled = kt;

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

                if (checkbutton)
                {
                    // Thêm mới
                    string query = $"INSERT INTO lichsusudung (MaThietBi, MANV, MucDichSuDung, NgayBatDau, NgayKetThuc) " +
                                   $"VALUES ({maThietBi}, {MANV}, '{mucDichSuDung}', '{ngayBatDau.ToString("yyyy-MM-dd")}', '{ngayKetThuc.ToString("yyyy-MM-dd")}')";

                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    DataRow row = gridView1.GetDataRow(rowIndex);
                    int maLichSuSuDung = Convert.ToInt32(row["MaLichSuSuDung"]);
                    string query = $"UPDATE lichsusudung SET MaThietBi = {maThietBi}, MANV = {MANV}, " +
                                   $"MucDichSuDung = '{mucDichSuDung}', NgayBatDau = '{ngayBatDau.ToString("yyyy-MM-dd")}', NgayKetThuc = '{ngayKetThuc.ToString("yyyy-MM-dd")}' " +
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

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void ClearInputs()
        {
            // Xóa các giá trị trong các control nhập liệu
            cbo_ThietBi.SelectedIndex = -1;
            cbo_NguoiDung.SelectedIndex = -1;
            txt_MucDich.Text = "";
            dtp_NgayBatDau.Value = DateTime.Now;
            dtp_NgayKetThuc.Value = DateTime.Now;
        }

        






        private int GetIDByTen(string ten, string tableName, string columnName, string idName)
        {
            int id = -1; // Giá trị mặc định nếu không tìm thấy ID

            try
            {
                string query = $"SELECT {idName} FROM {tableName} WHERE {columnName} = '{ten}'";
                object result = mySQLConnector.ExecuteScalar(query);
                if (result != null && result != DBNull.Value)
                {
                    id = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy ID từ bảng {tableName}: {ex.Message}");
            }

            return id;
        }

        private string GetTenById(string id, string tableName, string columnName, string idColumnName)
        {
            string ten = ""; // Giá trị mặc định nếu không tìm thấy tên

            try
            {
                string query = $"SELECT {columnName} FROM {tableName} WHERE {idColumnName} = {id}";
                object result = mySQLConnector.ExecuteScalar(query);
                if (result != null && result != DBNull.Value)
                {
                    ten = Convert.ToString(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy tên từ bảng {tableName}: {ex.Message}");
            }

            return ten;
        }
        // Hàm chọn item trong ComboBox theo ID

        private void SelectComboBoxItem(System.Windows.Forms.ComboBox comboBox, string id)
        {
            foreach (var item in comboBox.Items)
            {
                // Kiểm tra ID của mỗi item trong ComboBox
                // Nếu tìm thấy ID tương ứng, chọn item đó
                if (comboBox.GetItemText(item) == id)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

       
    }
}