using DevExpress.XtraEditors;
using QuanLyThietBi_Winform_NguyenPhuocVinh.Popup;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh.KhoVatTu
{
    public partial class FormKhoVatTu : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;

        public FormKhoVatTu()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private void FormDanhSachVatTu_Load(object sender, EventArgs e)
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
                // Load data for cbo_ChucVu
                query = "SELECT TenViTri FROM vitridat"; // Chỉnh sửa query tương ứng
                DataTable dtChucVu = mySQLConnector.Select(query);
                foreach (DataRow row in dtChucVu.Rows)
                {
                    cbo_ViTri.Items.Add(row["TenViTri"].ToString());
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
                string query = @"SELECT khovattu.MaVatTu, 
                                 khovattu.TenVatTu,
                                 khovattu.HinhAnh,
                                 khovattu.SoLuong, 
                                 khovattu.NguongToiThieu, 
                                 vitridat.TenViTri
                         FROM khovattu
                         JOIN vitridat ON khovattu.MaViTri = vitridat.MaViTri";
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
            btn_XemChiTietNhapXuat.Enabled = kt;
            btnNhapKho.Enabled = kt;
            btnXuatKho.Enabled = kt;
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
            txt_MaVatTu.Text = row["MaVatTu"].ToString();
            txt_TenVatTu.Text = row["TenVatTu"].ToString();
            txt_SoLuong.Text = row["SoLuong"].ToString();
            txt_NguongToiThieu.Text = row["NguongToiThieu"].ToString();
            SelectComboBoxItem(cbo_ViTri, row["TenViTri"].ToString());

            byte[] hinhAnh = (byte[])row["HinhAnh"]; // Đọc hình ảnh từ cột HINHANH
            if (hinhAnh != null)
            {
                picHinhAnh.Image = Image.FromStream(new MemoryStream(hinhAnh));
            }
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
                int maVatTu = Convert.ToInt32(row["MaVatTu"]);
                string query = $"DELETE FROM khovattu WHERE MaVatTu = {maVatTu}";
                mySQLConnector.ExecuteQuery(query);
                LoadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string tenVatTu = txt_TenVatTu.Text.Trim();
                int soLuong = Convert.ToInt32(txt_SoLuong.Text.Trim());
                int nguongToiThieu = Convert.ToInt32(txt_NguongToiThieu.Text.Trim());
                int maViTri = GetIDByTen(cbo_ViTri.SelectedItem.ToString(), "ViTriDat", "TenViTri", "MaViTri");
                string hinhAnh = ImageToHexString(picHinhAnh.Image, picHinhAnh.Image.RawFormat);

                if (checkbutton)
                {
                    string query = $"INSERT INTO khovattu (TenVatTu, SoLuong, NguongToiThieu,HinhAnh, MaViTri) VALUES ('{tenVatTu}', {soLuong}, {nguongToiThieu},{hinhAnh}, {maViTri})";
                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    int maVatTu = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaVatTu"));
                    string query = $"UPDATE khovattu SET TenVatTu = '{tenVatTu}', SoLuong = {soLuong}, NguongToiThieu = {nguongToiThieu},HinhAnh = {hinhAnh}, MaViTri = {maViTri} WHERE MaVatTu = {maVatTu}";
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
            txt_TenVatTu.Text = "";
            txt_SoLuong.Text = "";
            txt_NguongToiThieu.Text = "";
            cbo_ViTri.SelectedIndex = -1;

            picHinhAnh.Image = Properties.Resources.nonimg;
            txt_MaVatTu.Text = "";
        }

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

        private void btnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để nhập kho.");
                return;
            }

            DataRow row = gridView1.GetDataRow(rowIndex);
            int maVatTu = Convert.ToInt32(row["MaVatTu"]);
            int soLuong = Convert.ToInt32(row["SoLuong"]);

            using (PopupSoLuongNhapXuat popupSoLuongNhapXuat = new PopupSoLuongNhapXuat())
            {
                if (popupSoLuongNhapXuat.ShowDialog() == DialogResult.OK)
                {
                    int soLuongNhap = popupSoLuongNhapXuat.Quantity;
                    soLuong += soLuongNhap; // Tăng số lượng theo giá trị nhập vào

                    string updateQuery = $"UPDATE khovattu SET SoLuong = {soLuong} WHERE MaVatTu = {maVatTu}";
                    mySQLConnector.ExecuteQuery(updateQuery);

                    // Chèn thông tin chi tiết nhập kho vào bảng chitietnhapxuat
                    string insertQuery = $"INSERT INTO chitietnhapxuat (MaVatTu, SoLuong, NgayNhapXuat, LoaiGiaoDich) VALUES ({maVatTu}, {soLuongNhap}, NOW(), 'Nhập')";
                    mySQLConnector.ExecuteQuery(insertQuery);

                    LoadData();
                }
            }
        }
        private void btnXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xuất kho.");
                return;
            }

            DataRow row = gridView1.GetDataRow(rowIndex);
            int maVatTu = Convert.ToInt32(row["MaVatTu"]);
            int soLuong = Convert.ToInt32(row["SoLuong"]);

            using (PopupSoLuongNhapXuat popupSoLuongNhapXuat = new PopupSoLuongNhapXuat())
            {
                if (popupSoLuongNhapXuat.ShowDialog() == DialogResult.OK)
                {
                    int soLuongXuat = popupSoLuongNhapXuat.Quantity;
                    if (soLuong >= soLuongXuat)
                    {
                        soLuong -= soLuongXuat; // Giảm số lượng theo giá trị nhập vào

                        string updateQuery = $"UPDATE khovattu SET SoLuong = {soLuong} WHERE MaVatTu = {maVatTu}";
                        mySQLConnector.ExecuteQuery(updateQuery);

                        // Chèn thông tin chi tiết xuất kho vào bảng chitietnhapxuat
                        string insertQuery = $"INSERT INTO chitietnhapxuat (MaVatTu, SoLuong, NgayNhapXuat, LoaiGiaoDich) VALUES ({maVatTu}, {soLuongXuat}, NOW(), 'Xuất')";
                        mySQLConnector.ExecuteQuery(insertQuery);

                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Số lượng vật tư không đủ để xuất kho.");
                    }
                }
            }
        }

        private void btnXemChiTietNhapXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormChiTietNhapXuat formChiTietNhapXuat = new FormChiTietNhapXuat();
            formChiTietNhapXuat.ShowDialog();
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
        public string ImageToHexString(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Lưu hình ảnh vào MemoryStream
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                // Chuyển đổi sang chuỗi hex
                string hexString = BitConverter.ToString(imageBytes).Replace("-", "");
                return "0x" + hexString.ToLower();
            }
        }
    }

}
