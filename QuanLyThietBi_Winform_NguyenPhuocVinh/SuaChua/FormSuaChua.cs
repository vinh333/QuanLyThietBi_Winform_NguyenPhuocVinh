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
using QuanLyThietBi_Winform_NguyenPhuocVinh;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormSuaChua : DevExpress.XtraEditors.XtraForm
    {
        public FormSuaChua()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();

            gridView1.CustomDrawCell += GridView1_CustomDrawCell;

        }

        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;
        private string hexString = "";
        private bool checkTrangThai = false;
        // Biến để lưu tên của tệp biên bản
        private int MaLichSuSuaChua;

        private void FormSuaChua_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp

            LoadData();

        }





        private void LoadData()
        {
            try
            {

                string query = @"SELECT ThietBi.MaThietBi, ThietBi.TenThietBi,  ThietBi.TinhTrang, ThietBi.HinhAnh
         FROM ThietBi ";

                System.Data.DataTable dataTable = mySQLConnector.Select(query);


                // Gán DataSource cho GridControl
                gridControl1.DataSource = dataTable; 

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
                query = "SELECT HOTEN FROM kythuatvien"; // Chỉnh sửa query tương ứng
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


        private void btn_ThucHienSuaChua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    MessageBox.Show("Vui lòng chọn một thiết bị để thực hiện sửa chửa.");
                    return;
                }

                int maThietBi = Convert.ToInt32(selectedRow["MaThietBi"]);

                // Lấy bản ghi sửa chửa mới nhất cho thiết bị đã chọn
                string queryLatestMaintenance = $@"
            SELECT MaLichSuSuaChua, NgaySuaChua, MoTa, TienDo, HinhAnhSuaChua, BienBanSuaChua, TenBienBanSuaChua, MAKTV
                FROM lichsuSuaChua
                WHERE MaThietBi = {maThietBi} AND TienDo = 'Đang sửa chửa'
                ORDER BY NgaySuaChua DESC
                ";
                System.Data.DataTable latestMaintenanceRecord = mySQLConnector.Select(queryLatestMaintenance);

                if (latestMaintenanceRecord.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn đang thêm một sửa chửa mới.");
                    checkTrangThai = true;

                }
                else
                {
                    MessageBox.Show("Bạn đang cập nhật tiến độ sửa chửa.");
                    checkTrangThai = false;
                }

                // Hiển thị thông tin sửa chửa lên form


                try
                {
                    DataRow maintenanceRecord = latestMaintenanceRecord.Rows[0];
                    MaLichSuSuaChua = Convert.ToInt32(maintenanceRecord["MaLichSuSuaChua"]);
                    dtp_NgayThucHien.Value = (maintenanceRecord["NgaySuaChua"] != DBNull.Value) ? Convert.ToDateTime(maintenanceRecord["NgaySuaChua"]) : DateTime.Today;
                    txt_GhiChu.Text = (maintenanceRecord["MoTa"] != DBNull.Value) ? maintenanceRecord["MoTa"].ToString() : "";
                    cb_TienDo.Text = (maintenanceRecord["TienDo"] != DBNull.Value) ? maintenanceRecord["TienDo"].ToString() : "";
                    byte[] hinhAnh = maintenanceRecord["HinhAnhSuaChua"] as byte[];
                    if (hinhAnh != null && hinhAnh.Length > 0)
                    {
                        picHinhAnh.Image = Image.FromStream(new MemoryStream(hinhAnh));
                    }
                    txt_TenBaoCao.Text = (maintenanceRecord["TenBienBanSuaChua"] != DBNull.Value) ? maintenanceRecord["TenBienBanSuaChua"].ToString() : "";
                    int MaNv = Convert.ToInt32(maintenanceRecord["MAKTV"]);
                    SelectComboBoxItem(cbo_NguoiThucHien, GetTenById(MaNv.ToString(), "kythuatvien", "HOTEN", "MAKTV"));

                }
                catch
                {


                }
                txt_Ma.Text = selectedRow["MaThietBi"].ToString();
                txt_TenThietBi.Text = selectedRow["TenThietBi"].ToString();


 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện sửa chửa: " + ex.Message);
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
                string ngayThucHien = dtp_NgayThucHien.Value.ToString("yyyy-MM-dd"); // Định dạng ngày theo chuẩn yyyy-MM-dd
                string ghiChu = txt_GhiChu.Text.Trim();
                int maNguoiThucHien = GetIDByTen(cbo_NguoiThucHien.SelectedItem.ToString(), "kythuatvien", "HOTEN", "MAKTV");
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


                int maLichSuSuaChua = MaLichSuSuaChua;
                // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                if (tienDo == "Hoàn tất sửa chửa")
                {
                    // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                    string updateQuery = $"UPDATE thietbi SET TinhTrang = 'Đang hoạt động' WHERE MaThietBi = '{maThietBi}'";
                    mySQLConnector.ExecuteQuery(updateQuery);
                }
                else
                {
                    // Thực hiện câu lệnh UPDATE để cập nhật Trạng thái thiết bị
                    string updateQuery = $"UPDATE thietbi SET TinhTrang = 'Đang sửa chửa' WHERE MaThietBi = '{maThietBi}'";
                    mySQLConnector.ExecuteQuery(updateQuery);
                }

                



                if (checkTrangThai)
                {
                    // Thêm mới
                    string query = $"INSERT INTO lichsuSuaChua ( MaThietBi, NgaySuaChua, MoTa,  TienDo, HinhAnhSuaChua, TenBienBanSuaChua, BienBanSuaChua, MAKTV) VALUES " +
                                   $"( '{maThietBi}', '{ngayThucHien}', '{ghiChu}',  '{tienDo}', {hinhAnhHexString} , '{tenBaoCao}', '{hexString}', '{maNguoiThucHien}')";


                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    string query = $"UPDATE lichsuSuaChua SET MaThietBi = '{maThietBi}', NgaySuaChua = '{ngayThucHien}', MoTa = '{ghiChu}',  " +
                                   $"TienDo = '{tienDo}', HinhAnhSuaChua = {hinhAnhHexString}, TenBienBanSuaChua = '{tenBaoCao}', BienBanSuaChua = '{hexString}' , MAKTV = '{maNguoiThucHien}' " +
                                   $"WHERE MaLichSuSuaChua = '{maLichSuSuaChua}'";


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

        private void btn_LichSuSuaChua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                FormLichSuSuaChua frm = new FormLichSuSuaChua(mathietbi, tenthietbi, hinhAnh);

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
            dtp_NgayThucHien.Value = DateTime.Today;
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

            btn_ThucHienSuaChua.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnThoat.Enabled = kt;
            btnIn.Enabled = kt;

        }
        private void GridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != null && e.Column.FieldName == "TinhTrang")
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
        }


    }
}