using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FormDanhSachThietBi : DevExpress.XtraEditors.XtraForm
    {
        public FormDanhSachThietBi()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
            gridView1.CustomDrawCell += GridView1_CustomDrawCell;

        }

        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;

        

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
                query = "SELECT TenLoaiThietBi FROM loaithietbi"; // Chỉnh sửa query tương ứng
                DataTable dtBoPhan = mySQLConnector.Select(query);
                foreach (DataRow row in dtBoPhan.Rows)
                {
                    cbo_LoaiThietBi.Items.Add(row["TenLoaiThietBi"].ToString());
                }

                // Load data for cbo_ChucVu
                query = "SELECT TenViTri FROM vitridat"; // Chỉnh sửa query tương ứng
                DataTable dtChucVu = mySQLConnector.Select(query);
                foreach (DataRow row in dtChucVu.Rows)
                {
                    cbo_ViTri.Items.Add(row["TenViTri"].ToString());
                }

                // Load data for cbo_DanToc
                query = "SELECT TenChucNang FROM chucnangthietbi"; // Chỉnh sửa query tương ứng
                DataTable dtDanToc = mySQLConnector.Select(query);
                foreach (DataRow row in dtDanToc.Rows)
                {
                    cbo_ChucNang.Items.Add(row["TenChucNang"].ToString());
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
                CapNhatSoNgayConLaiDenHanBaoTri();

                string query = @"SELECT ThietBi.MaThietBi, ThietBi.TenThietBi, ThietBi.Model, ThietBi.SoSerial, ThietBi.NhaSanXuat, ThietBi.NgayMua, ThietBi.GiaTri, ThietBi.TinhTrang, ThietBi.HinhAnh, ThietBi.TanSuatBaoTri, ThietBi.NgayBatDauBaoTri, ThietBi.NgayKetThucBaoTri, ThietBi.CanhBao, LoaiThietBi.TenLoaiThietBi, ChucNangThietBi.TenChucNang, ViTriDat.TenViTri 
                FROM ThietBi 
                JOIN LoaiThietBi ON ThietBi.MaLoaiThietBi = LoaiThietBi.MaLoaiThietBi 
                JOIN ChucNangThietBi ON ThietBi.MaChucNang = ChucNangThietBi.MaChucNang 
                JOIN ViTriDat ON ThietBi.MaViTri = ViTriDat.MaViTri";
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
            int maThietBi = Convert.ToInt32(row["MaThietBi"]);
            string tenThietBi = row["TenThietBi"].ToString();
            string model = row["Model"].ToString();
            string soSerial = row["SoSerial"].ToString();
            string nhaSanXuat = row["NhaSanXuat"].ToString();
            DateTime ngayMua = Convert.ToDateTime(row["NgayMua"]);
            decimal giaTri = Convert.ToDecimal(row["GiaTri"]);
            string tinhTrang = row["TinhTrang"].ToString();

            string tenLoaiThietBi = row["TenLoaiThietBi"].ToString(); // Lấy tên loại thiết bị
            string tenChucNang = row["TenChucNang"].ToString(); // Lấy tên chức năng thiết bị
            string tenViTri = row["TenViTri"].ToString(); // Lấy tên vị trí đặt
            string tansuatbaotri = row["TanSuatBaoTri"].ToString(); 


            byte[] hinhAnh = (byte[])row["HinhAnh"]; // Đọc hình ảnh từ cột HINHANH
               
            // Hiển thị thông tin lên các controls tương ứng
            txt_TenThietBi.Text = tenThietBi;
            txt_ModelThietBi.Text = model;
            txt_SerialThietBi.Text = soSerial;
            txt_NhaSanXuatThietBi.Text = nhaSanXuat;
            dtp_NgayMua.Value = ngayMua;
            txt_GiaTriThietBi.Text = giaTri.ToString();
            cbo_TinhTrang.Text = tinhTrang;

            // Chọn các ComboBox tương ứng với tên loại thiết bị, chức năng và vị trí đặt
            SelectComboBoxItem(cbo_LoaiThietBi, tenLoaiThietBi);
            SelectComboBoxItem(cbo_ChucNang, tenChucNang);
            SelectComboBoxItem(cbo_ViTri, tenViTri);
            SelectComboBoxItem(cb_TanSuatBaoTri, tansuatbaotri);

            // Hiển thị hình ảnh lên pictureBox
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
                int maThietBi = Convert.ToInt32(row["MaThietBi"]);
                string query = $"DELETE FROM ThietBi WHERE MaThietBi = {maThietBi}";
                mySQLConnector.ExecuteQuery(query);
                LoadData(); // Hàm LoadData() dùng để load lại dữ liệu sau khi xóa
            }
        }


        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowIndex = gridView1.FocusedRowHandle;

                string tenThietBi = txt_TenThietBi.Text.Trim();
                string model = txt_ModelThietBi.Text.Trim();
                string soSerial = txt_SerialThietBi.Text.Trim();
                string nhaSanXuat = txt_NhaSanXuatThietBi.Text.Trim();
                DateTime ngayMua = dtp_NgayMua.Value;

                decimal giaTri = Convert.ToDecimal(txt_GiaTriThietBi.Text.Trim());
                string tinhTrang = cbo_TinhTrang.SelectedItem.ToString();

                int idLoaiThietBi = GetIDByTen(cbo_LoaiThietBi.SelectedItem.ToString(), "LoaiThietBi", "TenLoaiThietBi", "MaLoaiThietBi");
                int idChucNang = GetIDByTen(cbo_ChucNang.SelectedItem.ToString(), "ChucNangThietBi", "TenChucNang", "MaChucNang");
                int idViTri = GetIDByTen(cbo_ViTri.SelectedItem.ToString(), "ViTriDat", "TenViTri", "MaViTri");

                string hinhAnh = ImageToHexString(picHinhAnh.Image, picHinhAnh.Image.RawFormat);



                /// Lấy tần suất bảo trì từ ComboBox
               
                string tansuatbaotri = cb_TanSuatBaoTri.SelectedItem.ToString();

                // Khởi tạo ngày bắt đầu bảo trì là ngày hôm nay
                DateTime ngayBatDauBaoTri = DateTime.Today;
                DateTime ngayKetThucBaoTri;

                // Xử lý tần suất bảo trì
                switch (tansuatbaotri)
                {
                    case "Hàng Ngày":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddDays(1);
                        break;
                    case "Hàng Tuần":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddDays(7);
                        break;
                    case "Hàng Tháng":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddMonths(1);
                        break;
                    case "Hàng Quý":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddMonths(3);
                        break;
                    case "6 Tháng":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddMonths(6);
                        break;
                    case "1 Năm":
                        ngayKetThucBaoTri = ngayBatDauBaoTri.AddYears(1);
                        break;
                    default:
                        // Xử lý trường hợp không xác định
                        // Ví dụ: Gán ngày kết thúc là ngày bắt đầu
                        ngayKetThucBaoTri = ngayBatDauBaoTri;
                        break;
                }

                //Cảnh báo bảo trì


                if (checkbutton)
                {
                    // Thêm mới
                    string query = $"INSERT INTO ThietBi (TenThietBi, Model, SoSerial, NhaSanXuat, NgayMua, GiaTri, TinhTrang, MaLoaiThietBi, MaChucNang, MaViTri, HinhAnh, TanSuatBaoTri, NgayBatDauBaoTri, NgayKetThucBaoTri) " +
                    $"VALUES ('{tenThietBi}', '{model}', '{soSerial}', '{nhaSanXuat}', '{ngayMua.ToString("yyyy-MM-dd")}', {giaTri}, '{tinhTrang}', " +
                    $"{idLoaiThietBi}, {idChucNang}, {idViTri},{hinhAnh}, '{tansuatbaotri}', '{ngayBatDauBaoTri.ToString("yyyy-MM-dd")}', '{ngayKetThucBaoTri.ToString("yyyy-MM-dd")}')";
                    mySQLConnector.ExecuteQuery(query);

                }
                else
                {
                    // Sửa
                    DataRow row = gridView1.GetDataRow(rowIndex);
                    int maThietBi = Convert.ToInt32(row["MaThietBi"]);
                    string query = $"UPDATE ThietBi SET TenThietBi = '{tenThietBi}', Model = '{model}', SoSerial = '{soSerial}', " +
                    $"NhaSanXuat = '{nhaSanXuat}', NgayMua = '{ngayMua.ToString("yyyy-MM-dd")}', GiaTri = {giaTri}, " +
                    $"TinhTrang = '{tinhTrang}', MaLoaiThietBi = {idLoaiThietBi}, MaChucNang = {idChucNang}, MaViTri = {idViTri}, " +
                    $"HinhAnh = {hinhAnh} ,TanSuatBaoTri='{tansuatbaotri}'" +
                    $"WHERE MaThietBi = {maThietBi}";
                    mySQLConnector.ExecuteQuery(query);
                }

                LoadData(); // Hàm LoadData() dùng để load lại dữ liệu sau khi thực hiện thêm mới hoặc sửa đổi
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
           
           // picHinhAnh.Image = Properties.Resources.No_Image_Available;

            txt_TenThietBi.Text = "";
            txt_ModelThietBi.Text = "";
            txt_SerialThietBi.Text = "";
            txt_ModelThietBi.Text = "";
            txt_NhaSanXuatThietBi.Text = "";
            txt_GiaTriThietBi.Text = "";

            picHinhAnh.Image = Properties.Resources.nonimg;


        }
        private void GridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != null)
            {
                if (e.Column.FieldName == "TinhTrang")
                {
                    string status = e.CellValue?.ToString();
                    switch (status)
                    {
                        case "Đang sửa chửa":
                            e.Appearance.BackColor = Color.Orange;
                            break;
                        case "Đang bảo trì":
                            e.Appearance.BackColor = Color.Yellow;
                            break;
                        case "Đang hoạt động":
                            e.Appearance.BackColor = Color.Green;
                            break;
                        default:
                            e.Appearance.BackColor = Color.White;
                            break;
                    }
                }
                else if (e.Column.FieldName == "CanhBao")
                {
                    string status = e.CellValue?.ToString();
                    // Perform your logic for coloring based on the content of the "CanhBao" column
                    // For example:
                    if (status.Contains("Hôm nay"))
                    {
                        e.Appearance.BackColor = Color.Orange;
                    }
                    else if (status.Contains("Còn"))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    else if (status.Contains("Quá hạn"))
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                    else
                    {
                        // Set default color
                        e.Appearance.BackColor = Color.White;
                    }
                }
            }
        }


        private void btn_TaoQr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex >= 0)
            {
                DataRow row = gridView1.GetDataRow(rowIndex);
                string model = row["TenThietBi"].ToString();
                string soSerial = row["SoSerial"].ToString();
                string thongTinThietBi = $"{model} - {soSerial}";

                Image qrCodeImage = GenerateQRCode(thongTinThietBi);
                if (qrCodeImage != null)
                {
                    FormHienThiQR formHienThiQR = new FormHienThiQR(qrCodeImage, model, soSerial);
                    formHienThiQR.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không thể tạo mã QR cho thiết bị này.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị từ danh sách.");
            }
        }

        // Hàm để tạo mã QR từ thông tin thiết bị và trả về hình ảnh mã QR
        private Image GenerateQRCode(string thongTinThietBi)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300,
                    Height = 300
                }
            };

            return writer.Write(thongTinThietBi);
        }
        // Hàm để chuyển đổi hình ảnh thành mảng byte
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void Canhbao()
        {
           
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

        private void CapNhatSoNgayConLaiDenHanBaoTri()
        {
            try
            {
                // Lấy dữ liệu từ bảng ThietBi
                string query = "SELECT MaThietBi, NgayKetThucBaoTri FROM ThietBi";
                DataTable dtThietBi = mySQLConnector.Select(query);

                // Duyệt qua từng dòng dữ liệu để tính toán và cập nhật số ngày còn lại đến hạn bảo trì
                foreach (DataRow row in dtThietBi.Rows)
                {
                    int maThietBi = Convert.ToInt32(row["MaThietBi"]);
                    DateTime ngayKetThucBaoTri = Convert.ToDateTime(row["NgayKetThucBaoTri"]);

                    // Tính toán số ngày còn lại đến hạn bảo trì
                    int soNgayConLai = (ngayKetThucBaoTri - DateTime.Today).Days;

                    // Cập nhật thông tin số ngày còn lại vào cột tương ứng trong bảng ThietBi
                    if (soNgayConLai > 0)
                    {
                        query = $"UPDATE ThietBi SET Canhbao = 'Còn {soNgayConLai} ngày' WHERE MaThietBi = {maThietBi}";
                    }
                    else if (soNgayConLai == 0)
                    {
                        query = $"UPDATE ThietBi SET Canhbao = 'Hôm nay là ngày bảo trì' WHERE MaThietBi = {maThietBi}";
                    }
                    else
                    {
                        // Nếu số ngày còn lại là số âm, đó có thể là trường hợp bảo trì đã quá hạn
                        int soNgayTre = Math.Abs(soNgayConLai); // Chuyển đổi số âm thành dương
                        query = $"UPDATE ThietBi SET Canhbao = 'Quá hạn {soNgayTre} ngày' WHERE MaThietBi = {maThietBi}";
                    }

                    mySQLConnector.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật số ngày còn lại đến hạn bảo trì: " + ex.Message);
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