using DevExpress.XtraEditors;
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
    public partial class FormChucNangThietBi : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;
        private bool checkbutton = false;

        public FormChucNangThietBi()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private void FormChucNangThietBi_Load(object sender, EventArgs e)
        {
            _showHide(true);
            gridView1.OptionsBehavior.Editable = false; // Chặn chỉnh sửa trực tiếp

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM chucnangthietbi"; // Query for 'chucnangthietbi' table
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
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnThoat.Enabled = kt;
            btnIn.Enabled = kt;

            txtTenChucNang.Enabled = !kt;
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
            int maChucNang = Convert.ToInt32(row["MaChucNang"]);
            string tenChucNang = row["TenChucNang"].ToString();
            txtTenChucNang.Text = tenChucNang;
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
                int maChucNang = Convert.ToInt32(row["MaChucNang"]);
                string query = $"DELETE FROM chucnangthietbi WHERE MaChucNang = {maChucNang}";
                mySQLConnector.ExecuteQuery(query);
                LoadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int rowIndex = gridView1.FocusedRowHandle;
                if (checkbutton)
                {
                    // Thêm mới
                    string tenChucNang = txtTenChucNang.Text.Trim();
                    string query = $"INSERT INTO chucnangthietbi (TenChucNang) VALUES ('{tenChucNang}')";
                    mySQLConnector.ExecuteQuery(query);
                }
                else
                {
                    // Sửa
                    DataRow row = gridView1.GetDataRow(rowIndex);
                    int maChucNang = Convert.ToInt32(row["MaChucNang"]);
                    string tenChucNang = txtTenChucNang.Text.Trim();
                    string query = $"UPDATE chucnangthietbi SET TenChucNang = '{tenChucNang}' WHERE MaChucNang = {maChucNang}";
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
            txtTenChucNang.Text = "";
        }
    }
}
