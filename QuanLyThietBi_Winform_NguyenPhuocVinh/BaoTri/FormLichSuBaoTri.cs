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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormLichSuBaoTri : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;
        private string hexString = "";
        private int mathietbi;
        private string tenthietbi;
        private byte[] hinhAnhThietBi;
        // Biến để lưu tên của tệp biên bản
        private int MaLichSuBaoTri;
        public FormLichSuBaoTri()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }
        // Thêm một constructor chấp nhận một đối số để truyền giá trị makycong
        public FormLichSuBaoTri(int mathietbi, string tenthietbi, byte[] hinhAnh)
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();

            this.mathietbi = mathietbi;
            this.tenthietbi = tenthietbi;
            this.hinhAnhThietBi = hinhAnh;
          
        }

        private void FormBaoTri_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp
            gridView1.CustomDrawCell += GridView1_CustomDrawCell;

            LoadData();

        }

        private void LoadData()
        {
            try
            {
                // Cập nhật dữ iệu thiết bị vào panel 1 đc intent từ FormBaotri
                txt_MaThietBi_Panel1.Text = mathietbi.ToString();
                txt_TenThietBi_Panel1.Text = tenthietbi.ToString();
                if (hinhAnhThietBi != null && hinhAnhThietBi.Length > 0)
                {
                    picHinhAnhThietBi.Image = Image.FromStream(new MemoryStream(hinhAnhThietBi));
                }

                string query = $"SELECT * FROM Lichsubaotri where MaThietBi = {mathietbi} ";

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

        private void _showHide(bool kt)
        {
            splitContainer3.Panel2Collapsed = kt;
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
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
            int maLichSuBaoTri = Convert.ToInt32(row["MaLichSuBaoTri"]);
            int maThietBi = Convert.ToInt32(row["MaThietBi"]);
            DateTime ngayBaoTri = Convert.ToDateTime(row["NgayBaoTri"]);
            string moTa = row["MoTa"].ToString();
            string trangThai = row["TrangThai"].ToString();
            string tienDo = row["TienDo"].ToString();

            byte[] hinhAnhBaoTri = (byte[])row["HinhAnhBaoTri"]; // Đọc hình ảnh từ cột HinhAnhBaoTri
            string tenBienBanBaoTri = row["TenBienBanBaoTri"].ToString();
            byte[] bienBanBaoTri = (byte[])row["BienBanBaoTri"]; // Đọc biên bản bảo trì từ cột BienBanBaoTri
            int maNV = Convert.ToInt32(row["MAKTV"]);

            // Hiển thị thông tin lên các controls tương ứng
            txt_MaLichSuBaoTri.Text = maLichSuBaoTri.ToString();
            txt_MaThietBi.Text = maThietBi.ToString();
            txt_TenThietBi.Text = tenthietbi.ToString();
            cb_TrangThai.Text = trangThai;

            dtp_NgayBaoTri.Value = ngayBaoTri;
            txt_MoTa.Text = moTa;
            SelectComboBoxItem(cbo_NguoiThucHien, GetTenById(maNV.ToString(), "kythuatvien", "HOTEN", "MAKTV"));


            cb_TienDo.Text = tienDo.ToString();
            txt_TenBienBanBaoTri.Text = tenBienBanBaoTri;
            // Hiển thị hình ảnh lên pictureBox
            if (hinhAnhBaoTri != null)
            {
                picHinhAnhBaoTri.Image = Image.FromStream(new MemoryStream(hinhAnhBaoTri));
            }
            // Hiển thị biên bản bảo trì (nếu cần)
            // Ví dụ: hiển thị biên bản bảo trì lên một control RichTextBox
           // if (bienBanBaoTri != null)
           // {
              //  richTextBox_BienBanBaoTri.Text = Encoding.UTF8.GetString(bienBanBaoTri);
           // }
          
            // Hiển thị biên bản bảo trì (nếu cần)

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
                int MaLichSuBaoTri = Convert.ToInt32(row["MaLichSuBaoTri"]);
                string query = $"DELETE FROM lichsubaotri WHERE MaLichSuBaoTri = {MaLichSuBaoTri}";
                mySQLConnector.ExecuteQuery(query);
                LoadData(); // Hàm LoadData() dùng để load lại dữ liệu sau khi xóa
            }
        }

        private void btn_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowIndex = gridView1.FocusedRowHandle;

                string tenThietBi = txt_TenThietBi.Text.Trim();
                //fix chỗ này
                string maThietBi = txt_MaThietBi.Text.Trim();
                string trangThai = cb_TrangThai.Text.Trim();
                string ngayThucHien = dtp_NgayBaoTri.Value.ToString("yyyy-MM-dd"); // Định dạng ngày theo chuẩn yyyy-MM-dd
                string ghiChu = txt_MoTa.Text.Trim();
                int maNguoiThucHien = GetIDByTen(cbo_NguoiThucHien.SelectedItem.ToString(), "kythuatvien", "HOTEN", "MAKTV");
                string tienDo = cb_TienDo.Text.Trim();
                string tenBaoCao = txt_TenBienBanBaoTri.Text.Trim();

                // Lưu hình ảnh nếu có
                byte[] hinhAnhByteArray = null;
                if (picHinhAnhBaoTri.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picHinhAnhBaoTri.Image.Save(ms, picHinhAnhBaoTri.Image.RawFormat);
                        hinhAnhByteArray = ms.ToArray();
                    }
                }

                string hinhAnhHexString = ImageToHexString(picHinhAnhBaoTri.Image, picHinhAnhBaoTri.Image.RawFormat);


                int maLichSuBaoTri = int.Parse(txt_MaLichSuBaoTri.Text);

              





                if (checkbutton)
                {
                    // Thêm mới
                    string query = $"INSERT INTO lichsubaotri ( MaThietBi, NgayBaoTri, MoTa, TrangThai, TienDo, HinhAnhBaoTri, TenBienBanBaoTri, BienBanBaoTri, MAKTV) VALUES " +
                                   $"( '{maThietBi}', '{ngayThucHien}', '{ghiChu}', '{trangThai}', '{tienDo}', {hinhAnhHexString} , '{tenBaoCao}', '{hexString}', '{maNguoiThucHien}')";


                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    string query = $"UPDATE lichsubaotri SET MaThietBi = '{maThietBi}', NgayBaoTri = '{ngayThucHien}', MoTa = '{ghiChu}', TrangThai = '{trangThai}', " +
                                   $"TienDo = '{tienDo}', HinhAnhBaoTri = {hinhAnhHexString}, TenBienBanBaoTri = '{tenBaoCao}', BienBanBaoTri = '{hexString}' , MAKTV = '{maNguoiThucHien}' " +
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
                txt_TenBienBanBaoTri.Text = bienbanFileName;


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
            txt_MaThietBi.Text = "";
            cb_TrangThai.SelectedIndex = -1;
            dtp_NgayBaoTri.Value = DateTime.Today;
            txt_MoTa.Text = "";
            cb_TienDo.SelectedIndex = -1; // Chọn mục đầu tiên trong combobox
            cbo_NguoiThucHien.SelectedIndex = -1; // Chọn mục đầu tiên trong combobox
            txt_TenBienBanBaoTri.Text = "Chưa có tệp tin";
            picHinhAnhBaoTri.Image = Properties.Resources.nonimg;
        }
        private void GridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "TienDo")
            {
                string status = e.CellValue?.ToString();
                // Perform your logic for coloring based on the content of the "CanhBao" column
                // For example:
                if (status.Contains("Đang"))
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (status.Contains("Hoàn tất"))
                {
                    e.Appearance.BackColor = Color.Green;
                }
                
                else
                {
                    // Set default color
                    e.Appearance.BackColor = Color.White;
                }
            }
            if (e.Column.FieldName == "TrangThai")
            {
                string status = e.CellValue?.ToString();
                // Perform your logic for coloring based on the content of the "CanhBao" column
                // For example:
                if (status.Contains("Trễ"))
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else if (status.Contains("Còn"))
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else if (status.Contains("Đúng"))
                {
                    e.Appearance.BackColor = Color.Green;
                }
                else
                {
                    // Set default color
                    e.Appearance.BackColor = Color.White;
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
                picHinhAnhBaoTri.Image = Image.FromFile(openFile.FileName);
                picHinhAnhBaoTri.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearInputs();

            _showHide(true);
        }
    }
}