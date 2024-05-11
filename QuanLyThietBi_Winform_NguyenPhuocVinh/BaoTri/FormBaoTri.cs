using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Office.Interop.Word;
using DevExpress.Utils.DirectXPaint;
using Mysqlx.Crud;
namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormBaoTri : DevExpress.XtraEditors.XtraForm
    {
        public FormBaoTri()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;
        private string  hexString = "" ;
        private bool checkTrangThai = false ;
        // Biến để lưu tên của tệp biên bản
        private int MaLichSuBaoTri;

        private void FormBaoTri_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp

            LoadData();
           
        }

        

        

        private void LoadData()
        {
            try
            {
                CapNhatSoNgayConLaiDenHanBaoTri();

                string query = @"SELECT ThietBi.MaThietBi, ThietBi.TenThietBi,  ThietBi.TinhTrang, ThietBi.HinhAnh, ThietBi.TanSuatBaoTri, ThietBi.NgayBatDauBaoTri, ThietBi.NgayKetThucBaoTri, ThietBi.CanhBao
         FROM ThietBi ";

                System.Data.DataTable dataTable = mySQLConnector.Select(query);

                // Thêm một cột mới để lưu thông tin trạng thái cho mỗi hàng
                dataTable.Columns.Add("TrangThai", typeof(string));
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime ngayKetThuc = Convert.ToDateTime(row["NgayKetThucBaoTri"]);
                    if (ngayKetThuc < DateTime.Today)
                    {
                        // Thiết lập trạng thái "Đã hết hạn" cho các hàng có NgayKetThuc trước ngày hiện tại
                        row["TrangThai"] = "Cần bảo trì";
                    }
                    else if (ngayKetThuc < DateTime.Today.AddDays(1))
                    {
                        // Thiết lập trạng thái "Sắp hết hạn" cho các hàng có NgayKetThuc là ngày hôm sau
                        row["TrangThai"] = "Sắp đến hạn bảo trì";
                    }
                    else
                    {
                        row["TrangThai"] = "Chưa đến hạn bảo trì";

                    }
                }

                // Gán DataSource cho GridControl
                gridControl1.DataSource = dataTable;

                // Áp dụng định dạng điều kiện cho GridControl dựa trên cột TrangThai
                gridView1.FormatConditions.Clear();
                StyleFormatCondition conditionExpired = new StyleFormatCondition(FormatConditionEnum.Equal, gridView1.Columns["TrangThai"], null, "Cần bảo trì");
                conditionExpired.Appearance.BackColor = Color.Red;
                gridView1.FormatConditions.Add(conditionExpired);

                StyleFormatCondition conditionExpiring = new StyleFormatCondition(FormatConditionEnum.Equal, gridView1.Columns["TrangThai"], null, "Sắp đến hạn bảo trì");
                conditionExpiring.Appearance.BackColor = Color.Yellow;
                gridView1.FormatConditions.Add(conditionExpiring);


                LoadComboBoxData();
                ClearInputs();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void LoadComboBoxData()
        {
            // Load data for cbo_BoPhan, cbo_ChucVu, cbo_DanToc, cbo_PhongBan, cbo_TonGiao, cbo_TrinhDo
            try
            {
                string query;
                
                // Load data for cbo_ChucVu
                query = "SELECT HOTEN FROM nhanvien"; // Chỉnh sửa query tương ứng
                System.Data.DataTable dtChucVu = mySQLConnector.Select(query);
                foreach (DataRow row in dtChucVu.Rows)
                {
                    cbo_NguoiThucHien.Items.Add(row["HOTEN"].ToString());
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu ComboBox: " + ex.Message);
            }
        }


        private void btn_ThucHienBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // Ẩn hiện dữ liệu
                _showHide(false);

                // Lấy thông tin của hàng được chọn trong gridControl1
                int rowIndex = gridView1.FocusedRowHandle;
                DataRow selectedRow = gridView1.GetDataRow(rowIndex);

                if (selectedRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một thiết bị để thực hiện bảo trì.");
                    return;
                }

                int maThietBi = Convert.ToInt32(selectedRow["MaThietBi"]);

                // Lấy bản ghi bảo trì mới nhất cho thiết bị đã chọn
                string queryLatestMaintenance = $@"
            SELECT MaLichSuBaoTri, NgayBaoTri, MoTa, TrangThai, TienDo, HinhAnhBaoTri, BienBanBaoTri, TenBienBanBaoTri, MANV
                FROM lichsubaotri
                WHERE MaThietBi = {maThietBi} AND TienDo = 'Đang bảo trì'
                ORDER BY NgayBaoTri DESC
                ";
                System.Data.DataTable latestMaintenanceRecord = mySQLConnector.Select(queryLatestMaintenance);

                if (latestMaintenanceRecord.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn đang thêm một bảo trì mới.");
                    checkTrangThai = true;

                }
                else
                {
                    MessageBox.Show("Bạn đang cập nhật tiến độ bảo trì.");
                    checkTrangThai = false;
                }

                // Hiển thị thông tin bảo trì lên form

                
                try
                {
                    DataRow maintenanceRecord = latestMaintenanceRecord.Rows[0];
                    MaLichSuBaoTri = Convert.ToInt32(maintenanceRecord["MaLichSuBaoTri"]);
                    dtp_NgayThucHien.Value = (maintenanceRecord["NgayBaoTri"] != DBNull.Value) ? Convert.ToDateTime(maintenanceRecord["NgayBaoTri"]) : DateTime.Today;
                    txt_GhiChu.Text = (maintenanceRecord["MoTa"] != DBNull.Value) ? maintenanceRecord["MoTa"].ToString() : "";
                    cb_TienDo.Text = (maintenanceRecord["TienDo"] != DBNull.Value) ? maintenanceRecord["TienDo"].ToString() : "";
                    byte[] hinhAnh = maintenanceRecord["HinhAnhBaoTri"] as byte[];
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        picHinhAnh.Image = Image.FromStream(new MemoryStream(hinhAnh));
                    }
                    txt_TenBaoCao.Text = (maintenanceRecord["TenBienBanBaoTri"] != DBNull.Value) ? maintenanceRecord["TenBienBanBaoTri"].ToString() : "";
                    int MaNv = Convert.ToInt32(maintenanceRecord["MANV"]);
                    SelectComboBoxItem(cbo_NguoiThucHien, GetTenById(MaNv.ToString(), "nhanvien", "HOTEN", "MANV"));

                }
                catch 
                {

                   
                }
                txt_Ma.Text = selectedRow["MaThietBi"].ToString();
                txt_TenThietBi.Text = selectedRow["TenThietBi"].ToString();

             
                // Xử lý trạng thái bảo trì
                txt_ThoiHanBaoTri.Text = Convert.ToDateTime(selectedRow["NgayKetThucBaoTri"]).ToString("yyyy-MM-dd");

                DateTime ngayKetThuc = Convert.ToDateTime(selectedRow["NgayKetThucBaoTri"]);
                if (ngayKetThuc < DateTime.Today)
                {
                    txt_TrangThai.Text = "Trễ hạn bảo trì";
                }
                else if (ngayKetThuc < DateTime.Today.AddDays(1))
                {
                    txt_TrangThai.Text = "Đúng hạn bảo trì";
                }
                else
                {
                    txt_TrangThai.Text = "Còn hạn bảo trì";
                }

                // Hiển thị thông tin bảo trì

                try
                {
                    txt_ThoiHanBaoTri.Text = (selectedRow["NgayKetThucBaoTri"] != DBNull.Value) ? selectedRow["NgayKetThucBaoTri"].ToString() : "";
                }
                catch { }

                

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện bảo trì: " + ex.Message);
            }
        }




        private void btn_BienBan_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Chỉ cho phép người dùng chọn các tệp PDF hoặc DOC
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;


                // Check the file extension to determine the conversion method
                if (System.IO.Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    hexString = PdfToHexString(filePath);
                }
                else if (System.IO.Path.GetExtension(filePath).Equals(".doc", StringComparison.OrdinalIgnoreCase) ||
                         System.IO.Path.GetExtension(filePath).Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    hexString = DocToHexString(filePath);
                }
                else
                {
                    MessageBox.Show("Unsupported file format. Please select a PDF or Word document.");
                    return;
                }

                // Lấy tên tệp
                string bienbanFileName = System.IO.Path.GetFileName(filePath);
                txt_TenBaoCao.Text = bienbanFileName;

                
            }
        }


        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowIndex = gridView1.FocusedRowHandle;

                string tenThietBi = txt_TenThietBi.Text.Trim();
                string maThietBi = txt_Ma.Text.Trim();
                string trangThai = txt_TrangThai.Text.Trim();
                string ngayThucHien = dtp_NgayThucHien.Value.ToString("yyyy-MM-dd"); // Định dạng ngày theo chuẩn yyyy-MM-dd
                string thoiHanBaoTri = txt_ThoiHanBaoTri.Text.Trim();
                string ghiChu = txt_GhiChu.Text.Trim();
                int maNguoiThucHien = GetIDByTen(cbo_NguoiThucHien.SelectedItem.ToString(), "nhanvien", "HOTEN", "MANV");
                string tienDo = cb_TienDo.Text.Trim();
                string tenBaoCao = txt_TenBaoCao.Text.Trim();

                // Lưu hình ảnh nếu có
                byte[] hinhAnhByteArray = null;
                if (picHinhAnh.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picHinhAnh.Image.Save(ms, picHinhAnh.Image.RawFormat);
                        hinhAnhByteArray = ms.ToArray();
                    }
                }

                string hinhAnhHexString = ImageToHexString(picHinhAnh.Image, picHinhAnh.Image.RawFormat);


                int  maLichSuBaoTri = MaLichSuBaoTri;
                // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                if (tienDo == "Hoàn tất bảo trì")
                {
                    // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                    string updateQuery = $"UPDATE thietbi SET TinhTrang = 'Đang hoạt động' WHERE MaThietBi = '{maThietBi}'";
                    mySQLConnector.ExecuteQuery(updateQuery);
                }
                else
                {
                    // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                    string updateQuery = $"UPDATE thietbi SET TinhTrang = 'Đang bảo trì' WHERE MaThietBi = '{maThietBi}'";
                    mySQLConnector.ExecuteQuery(updateQuery);
                }

                if (tienDo == "Hoàn tất bảo trì")
                {
                    // Lấy ngày hôm nay
                    DateTime ngayBatDauBaoTri = DateTime.Today;
                    DateTime ngayKetThucBaoTri;
                    string tansuatbaotri_query = $"SELECT TanSuatBaoTri FROM `thietbi` where MaThietBi = '{maThietBi}'";
                    string tansuatbaotri = mySQLConnector.ExecuteScalar(tansuatbaotri_query).ToString();


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
                    // Thực hiện câu lệnh UPDATE để cập nhật NgayBatDauBaoTri
                    string updateQuery = $"UPDATE thietbi SET NgayBatDauBaoTri = '{ngayBatDauBaoTri.ToString("yyyy-MM-dd")}',NgayKetThucBaoTri = '{ngayKetThucBaoTri.ToString("yyyy-MM-dd")} ' WHERE MaThietBi = '{maThietBi}'";
                    mySQLConnector.ExecuteQuery(updateQuery);

                    // Gọi hàm để cập nhật số ngày còn lại đến hạn bảo trì
                    CapNhatSoNgayConLaiDenHanBaoTri();
                }



                if (checkTrangThai)
                {
                    // Thêm mới
                    string query = $"INSERT INTO lichsubaotri ( MaThietBi, NgayBaoTri, MoTa, TrangThai, TienDo, HinhAnhBaoTri, TenBienBanBaoTri, BienBanBaoTri, MANV) VALUES " +
                                   $"( '{maThietBi}', '{ngayThucHien}', '{ghiChu}', '{trangThai}', '{tienDo}', {hinhAnhHexString} , '{tenBaoCao}', '{hexString}', '{maNguoiThucHien}')";

                    
                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    string query = $"UPDATE lichsubaotri SET MaThietBi = '{maThietBi}', NgayBaoTri = '{ngayThucHien}', MoTa = '{ghiChu}', TrangThai = '{trangThai}', " +
                                   $"TienDo = '{tienDo}', HinhAnhBaoTri = {hinhAnhHexString}, TenBienBanBaoTri = '{tenBaoCao}', BienBanBaoTri = '{hexString}' , MANV = '{maNguoiThucHien}' " +
                                   $"WHERE MaLichSuBaoTri = '{maLichSuBaoTri}'";

                   
                    mySQLConnector.ExecuteQuery(query);
                }

                LoadData(); // Hàm LoadData() dùng để load lại dữ liệu sau khi thực hiện thêm mới hoặc sửa đổi
                ClearInputs();
                _showHide(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
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
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearInputs();

            _showHide(true);
        }

        private void btn_LichSuBaoTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Lấy dòng đang chọn từ gridView1
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex >= 0)
            {
                DataRow row = gridView1.GetDataRow(rowIndex);
                int mathietbi = Convert.ToInt32(row["MaThietBi"]);
                string tenthietbi = row["TenThietBi"].ToString();
                byte[] hinhAnh = row["HinhAnh"] as byte[];
                // Khởi tạo form FormBangCongChiTiet và truyền giá trị makycong qua constructor
                FormLichSuBaoTri frm = new FormLichSuBaoTri(mathietbi,tenthietbi,hinhAnh);

                // Hiển thị form
                frm.ShowDialog();
            }
            else
            {
                // Hiển thị thông báo nếu không có dòng nào được chọn
                MessageBox.Show("Vui lòng chọn một dòng để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        public string PdfToHexString(string filePath)
        {
            StringBuilder hexString = new StringBuilder();

            using (PdfReader reader = new PdfReader(filePath))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(reader, page);
                    hexString.Append(Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text))));
                }
            }

            return hexString.ToString();
        }

        public string DocToHexString(string filePath)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document doc = wordApp.Documents.Open(filePath);
            string text = doc.Content.Text;
            doc.Close();
            wordApp.Quit();

            return BitConverter.ToString(Encoding.Default.GetBytes(text)).Replace("-", string.Empty);
        }
        private void ClearInputs()
        {
            txt_TenThietBi.Text = "";
            txt_Ma.Text = "";
            txt_TrangThai.Text = "";
            dtp_NgayThucHien.Value = DateTime.Today;
            txt_ThoiHanBaoTri.Text = "";
            txt_GhiChu.Text = "";
            cb_TienDo.SelectedIndex = -1; // Chọn mục đầu tiên trong combobox
            cbo_NguoiThucHien.SelectedIndex = -1; // Chọn mục đầu tiên trong combobox
            txt_TenBaoCao.Text = "Chưa có tệp tin";
            picHinhAnh.Image = Properties.Resources.nonimg;
        }
        private void _showHide(bool kt)
        {
            splitContainer1.Panel1Collapsed = kt;

            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btn_ThucHienBaoTri.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnThoat.Enabled = kt;
            btnIn.Enabled = kt;

        }

        private void CapNhatSoNgayConLaiDenHanBaoTri()
        {
            try
            {
                // Lấy dữ liệu từ bảng ThietBi
                string query = "SELECT MaThietBi, NgayKetThucBaoTri FROM ThietBi";
                System.Data.DataTable dtThietBi = mySQLConnector.Select(query);

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

    }
}