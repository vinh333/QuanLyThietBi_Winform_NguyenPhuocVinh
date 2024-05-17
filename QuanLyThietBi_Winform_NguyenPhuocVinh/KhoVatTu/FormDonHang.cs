using System.Data;
using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh.KhoVatTu
{
    public partial class FormDonHang : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;

        public FormDonHang()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();

            // Đăng ký sự kiện SelectedIndexChanged cho ComboBox
            cbo_TrangThaiDonHang.SelectedIndexChanged += new EventHandler(cbo_TrangThaiDonHang_SelectedIndexChanged);

            // Ẩn các điều khiển liên quan khi khởi tạo form
            ToggleControls(false);
        }

        private void FormDonHang_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp

            LoadData();
            LoadComboBoxData(); // Load dữ liệu cho các ComboBox
        }

        private void LoadComboBoxData()
        {
            try
            {
                string query = "SELECT TenVatTu FROM khovattu"; // Chỉnh sửa query tương ứng
                DataTable dtVatTu = mySQLConnector.Select(query);
                foreach (DataRow row in dtVatTu.Rows)
                {
                    cbo_VatTu.Items.Add(row["TenVatTu"].ToString());
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
                string query = @"SELECT 
                                donhang.MaDonHang,
                                donhang.SoLuong,
                                donhang.NgayDatHang,
                                donhang.NgayGiaoHang,
                                donhang.TrangThaiDonHang,
                                donhang.TrangThaiNhapKho,
                                khovattu.TenVatTu
                            FROM 
                                donhang
                            JOIN 
                                khovattu 
                            ON 
                                donhang.MaVatTu = khovattu.MaVatTu;";
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
            txt_MaDonHang.Text = row["MaDonHang"].ToString();
            SelectComboBoxItem(cbo_VatTu, row["TenVatTu"].ToString());
            txt_SoLuong.Text = row["SoLuong"].ToString();
            dtp_NgayDatHang.Value = Convert.ToDateTime(row["NgayDatHang"]);
            dtp_NgayGiaoHang.Value = Convert.ToDateTime(row["NgayGiaoHang"]);

            SelectComboBoxItem(cbo_TrangThaiDonHang, row["TrangThaiDonHang"].ToString());
            SelectComboBoxItem(cbo_TrangThaiNhapKho, row["TrangThaiNhapKho"].ToString());
            // Kiểm tra giá trị của ComboBox
            if (cbo_TrangThaiDonHang.SelectedItem.ToString() == "Đã giao hàng")
            {
                // Hiển thị các điều khiển liên quan
                ToggleControls(true);
            }
            else
            {
                // Ẩn các điều khiển liên quan
                ToggleControls(false);
            }
            checkbutton = false;
            _showHide(false);
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string maDonHang = txt_MaDonHang.Text.Trim();
                int soLuong = Convert.ToInt32(txt_SoLuong.Text.Trim());
                DateTime ngayDatHang = dtp_NgayDatHang.Value;
                DateTime ngayGiaoHang = dtp_NgayGiaoHang.Value;
                string trangThaiDonHang = cbo_TrangThaiDonHang.SelectedItem.ToString();
                string trangThaiNhapKho = cbo_TrangThaiNhapKho.SelectedItem != null ? cbo_TrangThaiNhapKho.SelectedItem.ToString() : null;
                int maVatTu = GetIDByTen(cbo_VatTu.SelectedItem.ToString(), "khovattu", "TenVatTu", "MaVatTu");

                string query;
                if (checkbutton) // Thêm mới đơn hàng
                {
                    query = $"INSERT INTO donhang (MaDonHang, SoLuong, NgayDatHang, NgayGiaoHang, TrangThaiDonHang, TrangThaiNhapKho, MaVatTu) " +
                            $"VALUES ('{maDonHang}', {soLuong}, '{ngayDatHang:yyyy-MM-dd}', '{ngayGiaoHang:yyyy-MM-dd}', '{trangThaiDonHang}', '{trangThaiNhapKho}', {maVatTu})";
                }
                else // Cập nhật đơn hàng
                {
                    query = $"UPDATE donhang SET SoLuong = {soLuong}, NgayDatHang = '{ngayDatHang:yyyy-MM-dd}', NgayGiaoHang = '{ngayGiaoHang:yyyy-MM-dd}', " +
                            $"TrangThaiDonHang = '{trangThaiDonHang}', TrangThaiNhapKho = '{trangThaiNhapKho}', MaVatTu = {maVatTu} WHERE MaDonHang = '{maDonHang}'";
                }

                mySQLConnector.ExecuteQuery(query);
                LoadData();
                _showHide(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
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
                int maVatTu = Convert.ToInt32(row["MaVatTu"]);
                string query = $"DELETE FROM khovattu WHERE MaVatTu = {maVatTu}";
                mySQLConnector.ExecuteQuery(query);
                LoadData();
            }
        }
        private void btnNhapKho_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để thực hiện nhập kho.");
                return;
            }

            DataRow row = gridView1.GetDataRow(rowIndex);
            string tinhTrangNhapKho = row["TrangThaiNhapKho"].ToString();
            string trangThaiDonHang = row["TrangThaiDonHang"].ToString();

            if (tinhTrangNhapKho == "Chờ nhập kho" && trangThaiDonHang == "Đã giao hàng")
            {
                string tenVatTu = row["TenVatTu"].ToString();
                int maVatTu = GetIDByTen(tenVatTu, "khovattu", "TenVatTu", "MaVatTu");
                int soLuong = Convert.ToInt32(row["SoLuong"]);

                // Hiển thị hộp thoại xác nhận
                string message = $"Bạn có muốn Nhập {soLuong} {tenVatTu} vào kho không?";
                DialogResult dialogResult = MessageBox.Show(message, "Xác nhận nhập kho", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string updateQuery = $"UPDATE khovattu SET SoLuong = SoLuong + {soLuong} WHERE MaVatTu = {maVatTu}";
                        mySQLConnector.ExecuteQuery(updateQuery);

                        string updateDonHangQuery = $"UPDATE donhang SET TrangThaiNhapKho = 'Đã nhập kho' WHERE MaDonHang = '{row["MaDonHang"].ToString()}'";
                        mySQLConnector.ExecuteQuery(updateDonHangQuery);

                        LoadData();
                        MessageBox.Show("Nhập kho thành công.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thực hiện nhập kho: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Nhập kho đã bị hủy.");
                }
            }
            else
            {
                MessageBox.Show("Đơn hàng chưa đạt điều kiện để thực hiện nhập kho.");
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
            txt_MaDonHang.Text = "";
            cbo_VatTu.SelectedIndex = -1;
            cbo_TrangThaiDonHang.SelectedIndex = -1;
            cbo_TrangThaiNhapKho.SelectedIndex = -1;
            txt_SoLuong.Text = "";
            dtp_NgayDatHang.Value = DateTime.Now;
            dtp_NgayGiaoHang.Value = DateTime.Now;

            // Ẩn các điều khiển liên quan
            ToggleControls(false);
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

        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Picture file (.png, .jpg) | *.png; *.jpg";
            openFile.Title = "Chọn hình ảnh";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                picHinhAnh.Image = Image.FromFile(openFile.FileName);
                picHinhAnh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private int GetIDByTen(string ten, string tableName, string columnName, string idName)
        {
            int id = -1;

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
                MessageBox.Show($"Lỗi khi lấy ID từ {tableName}: {ex.Message}");
            }

            return id;
        }

        private void cbo_TrangThaiDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_TrangThaiDonHang.SelectedItem.ToString() == "Đã giao hàng")
            {
                ToggleControls(true);
            }
            else
            {
                ToggleControls(false);
            }
        }

        private void ToggleControls(bool show)
        {
            // Thiết lập thuộc tính Visible của các điều khiển
            cbo_TrangThaiNhapKho.Visible = show;
            dtp_NgayGiaoHang.Visible = show;
            label_TrangThaiNhapKho.Visible = show;
            label_NgayGiao.Visible = show;
        }
    }
}
