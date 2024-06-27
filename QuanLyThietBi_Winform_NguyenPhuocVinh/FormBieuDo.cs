using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    public partial class FormBieuDo : DevExpress.XtraEditors.XtraForm
    {
        private MySQLConnector mySQLConnector;

        public FormBieuDo()
        {
            InitializeComponent();
            mySQLConnector = new MySQLConnector();
        }

        private void FormBieuDo_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                cbo_HinhThuc.SelectedIndex = 0;
                cbo_Loai.SelectedIndex = 0;
                cbo_Nam.SelectedIndex = 0;
                cbo_Thang.SelectedIndex = 0;

                // Ensure the charts are empty before adding new data
                chart_ThongKeSoLuongThietBi.Series.Clear();
                chart_ThongKeThietBiTheoPhanLoai.Series.Clear();
                chart_ThongKeSuaChua.Series.Clear();
                chart_ThongKeBaoTri.Series.Clear();
                chart_ThongKeNhapXuatVatTu.Series.Clear();
                chart_ThongKeTinhTrang.Series.Clear(); // Clear the new chart
                chart_BaoTriThietBi.Series.Clear(); // Clear the new chart

                // Create the various charts
                CreateBarChart_ThongKeSoLuongThietBi();
                CreatePieChart_ThongKeThietBiTheoPhanLoai();
                CreateBarChart_ThongKeSuaChua();
                CreateBarChart_ThongKeBaoTri();
                CreateBarChart_ThongKeNhapXuatVatTu();
                CreateBarChart_ThongKeTinhTrangThietBi(); // Create the new chart
                CreateBarChart_BaoTriThietBi(); // Create the new chart
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }



        private void CreateBarChart_ThongKeSoLuongThietBi()
        {
            string query = @"
                SELECT lt.TenLoaiThietBi, COUNT(tb.MaThietBi) AS SoLuong
                FROM thietbi tb
                JOIN loaithietbi lt ON tb.MaLoaiThietBi = lt.MaLoaiThietBi
                GROUP BY lt.TenLoaiThietBi;";

            DataTable dataTable = mySQLConnector.Select(query);

            Series barSeries = new Series("Số lượng thiết bị", ViewType.Bar);

            foreach (DataRow row in dataTable.Rows)
            {
                barSeries.Points.Add(new SeriesPoint(row["TenLoaiThietBi"], row["SoLuong"]));
            }

            chart_ThongKeSoLuongThietBi.Series.Add(barSeries);
            chart_ThongKeSoLuongThietBi.Refresh();
        }

        private void CreatePieChart_ThongKeThietBiTheoPhanLoai()
        {
            string query = @"
                SELECT lt.TenLoaiThietBi, COUNT(tb.MaThietBi) AS SoLuong
                FROM thietbi tb
                JOIN loaithietbi lt ON tb.MaLoaiThietBi = lt.MaLoaiThietBi
                GROUP BY lt.TenLoaiThietBi;";

            DataTable dataTable = mySQLConnector.Select(query);

            Series pieSeries = new Series("Số lượng thiết bị", ViewType.Pie);

            // Tính tổng số lượng thiết bị để tính phần trăm
            int totalDevices = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                totalDevices += Convert.ToInt32(row["SoLuong"]);
            }

            // Thêm dữ liệu vào biểu đồ Pie và tính phần trăm
            foreach (DataRow row in dataTable.Rows)
            {
                double percentage = Convert.ToDouble(row["SoLuong"]) * 100.0 / totalDevices;
                SeriesPoint point = new SeriesPoint(row["TenLoaiThietBi"], percentage);
                point.ToolTipHint = $"{row["TenLoaiThietBi"]}: {percentage:F2}%";
                pieSeries.Points.Add(point);
            }

            // Thiết lập LegendTextPattern để hiển thị tên loại thiết bị và phần trăm
            pieSeries.LegendTextPattern = "{A}: {V:F2}%";

            chart_ThongKeThietBiTheoPhanLoai.Series.Add(pieSeries);

            // Thiết lập tiêu đề cho biểu đồ
            AddChartTitle(chart_ThongKeThietBiTheoPhanLoai, "Thống Kê Thiết Bị Theo Phân Loại");

            // Làm mới biểu đồ
            chart_ThongKeThietBiTheoPhanLoai.Refresh();
        }

        private void CreateBarChart_ThongKeSuaChua()
        {
            string query = @"
        SELECT 
            TienDo, 
            COUNT(MaThietBi) AS SoLuong
        FROM 
            lichsusuachua
       
        GROUP BY 
            TienDo
        ORDER BY 
            TienDo;";

            DataTable dataTable = mySQLConnector.Select(query);

            Series series = new Series();

            foreach (DataRow row in dataTable.Rows)
            {
                SeriesPoint point = new SeriesPoint(row["TienDo"], row["SoLuong"]);

                // Đặt màu cho từng điểm dữ liệu
                switch (row["TienDo"].ToString())
                {
                    case "Hoàn tất sửa chửa":
                        point.Color = System.Drawing.Color.LightBlue; // Màu xanh lá cho hoàn tất sửa chữa
                        break;
                    case "Đang sửa chửa":
                        point.Color = System.Drawing.Color.IndianRed; // Màu cam cho đang sửa chữa
                        break;
                    // Thêm các trường hợp khác nếu cần
                    default:
                        point.Color = System.Drawing.Color.Gray; // Màu xám cho các trường hợp khác
                        break;
                }

                series.Points.Add(point);
            }

            chart_ThongKeSuaChua.Series.Add(series);

            // Thiết lập tiêu đề cho biểu đồ

            // Làm mới biểu đồ
            chart_ThongKeSuaChua.Refresh();
        }

        


        private void CreateBarChart_ThongKeBaoTri()
        {
            string query = @"
        SELECT 
            TienDo, 
            COUNT(MaThietBi) AS SoLuong
        FROM 
            lichsubaotri
        
        GROUP BY 
            TienDo
        ORDER BY 
            TienDo;";

            DataTable dataTable = mySQLConnector.Select(query);

            Series series = new Series();

            foreach (DataRow row in dataTable.Rows)
            {
                SeriesPoint point = new SeriesPoint(row["TienDo"], row["SoLuong"]);

                // Đặt màu cho từng điểm dữ liệu
                switch (row["TienDo"].ToString())
                {
                    case "Hoàn tất bảo trì":
                        point.Color = System.Drawing.Color.DarkSeaGreen; // Màu xanh lá cho hoàn tất bảo trì
                        break;
                    case "Đang bảo trì":
                        point.Color = System.Drawing.Color.DarkOrange; // Màu cam cho đang bảo trì
                        break;
                    // Thêm các trường hợp khác nếu cần
                    default:
                        point.Color = System.Drawing.Color.Gray; // Màu xám cho các trường hợp khác
                        break;
                }

                series.Points.Add(point);
            }

            chart_ThongKeBaoTri.Series.Add(series);

            // Thiết lập tiêu đề cho biểu đồ

            // Làm mới biểu đồ
            chart_ThongKeBaoTri.Refresh();
        }
        private void CreateBarChart_ThongKeNhapXuatVatTu()
        {
            string query = @"
        SELECT 
            v.TenVatTu, 
            SUM(CASE WHEN c.LoaiGiaoDich = 'Nhập' THEN c.SoLuong ELSE 0 END) AS SoLuongNhap,
            SUM(CASE WHEN c.LoaiGiaoDich = 'Xuất' THEN c.SoLuong ELSE 0 END) AS SoLuongXuat
        FROM 
            chitietnhapxuat c
        JOIN 
            khovattu v ON c.MaVatTu = v.MaVatTu
        GROUP BY 
            v.TenVatTu;";

            DataTable dataTable = mySQLConnector.Select(query);

            // Create series for "Nhập" and "Xuất"
            Series seriesNhap = new Series("Số lượng nhập", ViewType.Bar);
            Series seriesXuat = new Series("Số lượng xuất", ViewType.Bar);

            foreach (DataRow row in dataTable.Rows)
            {
                seriesNhap.Points.Add(new SeriesPoint(row["TenVatTu"], row["SoLuongNhap"]));
                seriesXuat.Points.Add(new SeriesPoint(row["TenVatTu"], row["SoLuongXuat"]));
            }

            chart_ThongKeNhapXuatVatTu.Series.Add(seriesNhap);
            chart_ThongKeNhapXuatVatTu.Series.Add(seriesXuat);

            // Setup the chart titles and legends
            chart_ThongKeNhapXuatVatTu.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Refresh the chart
            chart_ThongKeNhapXuatVatTu.Refresh();
        }
        private void CreateBarChart_ThongKeTinhTrangThietBi()
        {
            string query = @"
            SELECT 
                TinhTrang, 
                COUNT(MaThietBi) AS SoLuong
            FROM 
                thietbi
            GROUP BY 
                TinhTrang;";

            DataTable dataTable = mySQLConnector.Select(query);

            Series series = new Series();

            // Mảng màu cho các tình trạng thiết bị
            Color[] colors = new Color[] { Color.Green, Color.IndianRed, Color.Yellow, Color.Orange };

            foreach (DataRow row in dataTable.Rows)
            {
                // Lấy tên tình trạng và số lượng từ dữ liệu
                string tinhTrang = row["TinhTrang"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);

                // Tạo một điểm trong loạt dữ liệu của biểu đồ
                SeriesPoint point = new SeriesPoint(tinhTrang, soLuong);

                // Gán màu từ mảng màu cho cột tương ứng
                int colorIndex = Array.IndexOf(new string[] { "Đang hoạt động", "Hư hỏng", "Đang bảo trì", "Đang sửa chửa" }, tinhTrang);
                if (colorIndex >= 0)
                {
                    point.Color = colors[colorIndex];
                }

                // Thêm điểm vào loạt dữ liệu
                series.Points.Add(point);
            }

            // Thêm loạt dữ liệu vào biểu đồ
            chart_ThongKeTinhTrang.Series.Add(series);

            // Xoay biểu đồ để các cột nằm ngang
            if (chart_ThongKeTinhTrang.Diagram is XYDiagram diagram)
            {
                diagram.Rotated = true;
            }

            // Thiết lập tiêu đề và chú thích của biểu đồ
           // AddChartTitle(chart_ThongKeTinhTrang, "Thống Kê Tình Trạng Thiết Bị");
            chart_ThongKeTinhTrang.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Làm mới biểu đồ
            chart_ThongKeTinhTrang.Refresh();
        }

        private void CreateBarChart_BaoTriThietBi()
        {
            // Truy vấn dữ liệu từ cơ sở dữ liệu
            string query = @"
    SELECT 
        TenThietBi,
        NgayKetThucBaoTri
    FROM 
        thietbi;";

            DataTable dataTable = mySQLConnector.Select(query);

            int denHan = 0, sapDenHan = 0, treHan = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime ngayKetThucBaoTri = Convert.ToDateTime(row["NgayKetThucBaoTri"]);
                TimeSpan diff = ngayKetThucBaoTri - DateTime.Now;

                if (diff.Days < 0)
                {
                    treHan++;
                }
                else if (diff.Days <= 30)
                {
                    denHan++;
                }
                else
                {
                    sapDenHan++;
                }
            }

            Series series = new Series();

            // Thêm điểm dữ liệu và thiết lập màu sắc tương ứng
            SeriesPoint pointDenHan = new SeriesPoint("Đến Hạn", denHan);
            pointDenHan.Color = Color.Orange;
            series.Points.Add(pointDenHan);

            SeriesPoint pointSapDenHan = new SeriesPoint("Sắp Đến Hạn", sapDenHan);
            pointSapDenHan.Color = Color.Yellow;
            series.Points.Add(pointSapDenHan);

            SeriesPoint pointTreHan = new SeriesPoint("Trễ Hạn", treHan);
            pointTreHan.Color = Color.IndianRed;
            series.Points.Add(pointTreHan);

            chart_BaoTriThietBi.Series.Add(series);

            // Xoay biểu đồ để các cột nằm ngang
            if (chart_BaoTriThietBi.Diagram is XYDiagram diagram)
            {
                diagram.Rotated = true;
            }

            // Thiết lập tiêu đề và chú thích của biểu đồ
            //AddChartTitle(chart_BaoTriThietBi, "Thống Kê Bảo Trì Thiết Bị");
            chart_BaoTriThietBi.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Làm mới biểu đồ
            chart_BaoTriThietBi.Refresh();
        }

        // Hàm thêm tiêu đề vào biểu đồ
      



        private void AddChartTitle(ChartControl chart, string titleText)
        {
            ChartTitle chartTitle = new ChartTitle();
            chartTitle.Text = titleText;
            chart.Titles.Add(chartTitle);
        }

       
    }
}
