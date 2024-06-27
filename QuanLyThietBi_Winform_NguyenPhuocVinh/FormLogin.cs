using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormLogin : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool isPasswordVisible = false;

        public FormLogin()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();


        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            // Ẩn mật khẩu ngay khi form được tải lên
            txt_PassWord.Properties.PasswordChar = '*';
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txt_UserName.Text.Trim();
            string password = txt_PassWord.Text.Trim();
            string loaitaikhoan = cbo_LoaiTaiKhoan.Text;
            // Kiểm tra xem người dùng đã nhập tên người dùng và mật khẩu chưa
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                XtraMessageBox.Show("Vui lòng nhập tên người dùng và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thực hiện truy vấn để kiểm tra xem tên người dùng và mật khẩu có tồn tại trong cơ sở dữ liệu không
            string query = $"SELECT COUNT(*) FROM account WHERE TenDangNhap = '{username}' AND MatKhau = '{password}' AND VaiTro = '{loaitaikhoan}'";
            object result = mySQLConnector.ExecuteScalar(query);

            // Kiểm tra kết quả trả về
            if (result != null && Convert.ToInt32(result) > 0)
            {
                XtraMessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đóng form đăng nhập
                this.Hide();

                // Mở MainForm
                MainForm formMain = new MainForm();
                formMain.Closed += (s, args) => this.Close(); // Đóng form đăng nhập khi MainForm đóng
                formMain.Show();
            }
            else
            {
                XtraMessageBox.Show("Tên người dùng hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            // Chuyển đổi trạng thái hiển thị mật khẩu
            isPasswordVisible = !isPasswordVisible;

            // Cập nhật thuộc tính PasswordChar dựa trên trạng thái hiển thị mật khẩu
            txt_PassWord.Properties.PasswordChar = isPasswordVisible ? '\0' : '*';

            // Cập nhật văn bản của nút dựa trên trạng thái hiển thị mật khẩu
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}