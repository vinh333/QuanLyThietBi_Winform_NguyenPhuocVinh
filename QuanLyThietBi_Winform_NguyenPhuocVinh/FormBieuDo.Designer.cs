namespace QuanLyThietBi_Winform_NguyenPhuocVinh
{
    partial class FormBieuDo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.SeriesTitle seriesTitle2 = new DevExpress.XtraCharts.SeriesTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ChartTitle chartTitle5 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle6 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ChartTitle chartTitle7 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.ChartTitle chartTitle8 = new DevExpress.XtraCharts.ChartTitle();
            this.chart_ThongKeThietBiTheoPhanLoai = new DevExpress.XtraCharts.ChartControl();
            this.chart_ThongKeSoLuongThietBi = new DevExpress.XtraCharts.ChartControl();
            this.chart_ThongKeSuaChua = new DevExpress.XtraCharts.ChartControl();
            this.chart_ThongKeNhapXuatVatTu = new DevExpress.XtraCharts.ChartControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxEdit4 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.chart_ThongKeBaoTri = new DevExpress.XtraCharts.ChartControl();
            this.chart_ThongKeTinhTrang = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeThietBiTheoPhanLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeSoLuongThietBi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeSuaChua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeNhapXuatVatTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeBaoTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeTinhTrang)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_ThongKeThietBiTheoPhanLoai
            // 
            this.chart_ThongKeThietBiTheoPhanLoai.Location = new System.Drawing.Point(42, 145);
            this.chart_ThongKeThietBiTheoPhanLoai.Name = "chart_ThongKeThietBiTheoPhanLoai";
            series6.Name = "Series 1";
            seriesTitle2.Text = "BIỂU ĐỒ THỐNG KÊ THIẾT BỊ THEO LOẠI";
            pieSeriesView2.Titles.AddRange(new DevExpress.XtraCharts.SeriesTitle[] {
            seriesTitle2});
            series6.View = pieSeriesView2;
            this.chart_ThongKeThietBiTheoPhanLoai.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series6};
            this.chart_ThongKeThietBiTheoPhanLoai.Size = new System.Drawing.Size(805, 416);
            this.chart_ThongKeThietBiTheoPhanLoai.TabIndex = 0;
            // 
            // chart_ThongKeSoLuongThietBi
            // 
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            this.chart_ThongKeSoLuongThietBi.Diagram = xyDiagram4;
            this.chart_ThongKeSoLuongThietBi.Location = new System.Drawing.Point(867, 145);
            this.chart_ThongKeSoLuongThietBi.Name = "chart_ThongKeSoLuongThietBi";
            series7.Name = "Series 1";
            this.chart_ThongKeSoLuongThietBi.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7};
            this.chart_ThongKeSoLuongThietBi.Size = new System.Drawing.Size(796, 416);
            this.chart_ThongKeSoLuongThietBi.TabIndex = 1;
            chartTitle5.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 12F);
            chartTitle5.Text = "SỐ LƯỢNG THIẾT BỊ";
            this.chart_ThongKeSoLuongThietBi.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle5});
            // 
            // chart_ThongKeSuaChua
            // 
            xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
            this.chart_ThongKeSuaChua.Diagram = xyDiagram5;
            this.chart_ThongKeSuaChua.Location = new System.Drawing.Point(38, 583);
            this.chart_ThongKeSuaChua.Name = "chart_ThongKeSuaChua";
            series8.Name = "Series 1";
            series8.View = lineSeriesView2;
            this.chart_ThongKeSuaChua.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series8};
            this.chart_ThongKeSuaChua.Size = new System.Drawing.Size(481, 314);
            this.chart_ThongKeSuaChua.TabIndex = 2;
            chartTitle6.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 12F);
            chartTitle6.Text = "THỐNG KÊ SỬA CHỬA THIẾT BỊ";
            this.chart_ThongKeSuaChua.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle6});
            // 
            // chart_ThongKeNhapXuatVatTu
            // 
            xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
            this.chart_ThongKeNhapXuatVatTu.Diagram = xyDiagram6;
            this.chart_ThongKeNhapXuatVatTu.Location = new System.Drawing.Point(1082, 583);
            this.chart_ThongKeNhapXuatVatTu.Name = "chart_ThongKeNhapXuatVatTu";
            series9.Name = "Series 1";
            series10.Name = "Series 2";
            this.chart_ThongKeNhapXuatVatTu.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9,
        series10};
            this.chart_ThongKeNhapXuatVatTu.Size = new System.Drawing.Size(537, 351);
            this.chart_ThongKeNhapXuatVatTu.TabIndex = 4;
            chartTitle7.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 12F);
            chartTitle7.Text = "Thống Kê Nhập/Xuất Vật Tư";
            this.chart_ThongKeNhapXuatVatTu.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle7});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(55, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 34);
            this.label1.TabIndex = 5;
            this.label1.Text = "TỔNG QUAN THIẾT BỊ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(529, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Hình thức";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(505, 81);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.comboBoxEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.comboBoxEdit1.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(118, 24);
            this.comboBoxEdit1.TabIndex = 17;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(651, 81);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.comboBoxEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.comboBoxEdit2.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Size = new System.Drawing.Size(118, 24);
            this.comboBoxEdit2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(681, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Tên loại";
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.Location = new System.Drawing.Point(789, 81);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.comboBoxEdit3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.comboBoxEdit3.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Size = new System.Drawing.Size(118, 24);
            this.comboBoxEdit3.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(823, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Tháng";
            // 
            // comboBoxEdit4
            // 
            this.comboBoxEdit4.Location = new System.Drawing.Point(926, 81);
            this.comboBoxEdit4.Name = "comboBoxEdit4";
            this.comboBoxEdit4.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.comboBoxEdit4.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBoxEdit4.Properties.Appearance.Options.UseBackColor = true;
            this.comboBoxEdit4.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.comboBoxEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit4.Size = new System.Drawing.Size(118, 24);
            this.comboBoxEdit4.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(966, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Năm";
            // 
            // chart_ThongKeBaoTri
            // 
            this.chart_ThongKeBaoTri.Location = new System.Drawing.Point(540, 582);
            this.chart_ThongKeBaoTri.Name = "chart_ThongKeBaoTri";
            this.chart_ThongKeBaoTri.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chart_ThongKeBaoTri.Size = new System.Drawing.Size(517, 314);
            this.chart_ThongKeBaoTri.TabIndex = 3;
            chartTitle8.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 12F);
            chartTitle8.Text = "THỐNG KÊ BẢO TRÌ THIẾT BỊ";
            this.chart_ThongKeBaoTri.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle8});
            // 
            // chart_ThongKeTinhTrang
            // 
            this.chart_ThongKeTinhTrang.Location = new System.Drawing.Point(573, 902);
            this.chart_ThongKeTinhTrang.Name = "chart_ThongKeTinhTrang";
            this.chart_ThongKeTinhTrang.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chart_ThongKeTinhTrang.Size = new System.Drawing.Size(471, 200);
            this.chart_ThongKeTinhTrang.TabIndex = 24;
            // 
            // FormBieuDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1675, 1062);
            this.Controls.Add(this.chart_ThongKeTinhTrang);
            this.Controls.Add(this.comboBoxEdit4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxEdit3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxEdit2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart_ThongKeNhapXuatVatTu);
            this.Controls.Add(this.chart_ThongKeBaoTri);
            this.Controls.Add(this.chart_ThongKeSuaChua);
            this.Controls.Add(this.chart_ThongKeSoLuongThietBi);
            this.Controls.Add(this.chart_ThongKeThietBiTheoPhanLoai);
            this.Name = "FormBieuDo";
            this.Text = "FormBieuDo";
            this.Load += new System.EventHandler(this.FormBieuDo_Load);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeThietBiTheoPhanLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeSoLuongThietBi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeSuaChua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeNhapXuatVatTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeBaoTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_ThongKeTinhTrang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chart_ThongKeThietBiTheoPhanLoai;
        private DevExpress.XtraCharts.ChartControl chart_ThongKeSoLuongThietBi;
        private DevExpress.XtraCharts.ChartControl chart_ThongKeSuaChua;
        private DevExpress.XtraCharts.ChartControl chart_ThongKeNhapXuatVatTu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit4;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraCharts.ChartControl chart_ThongKeBaoTri;
        private DevExpress.XtraCharts.ChartControl chart_ThongKeTinhTrang;
    }
}