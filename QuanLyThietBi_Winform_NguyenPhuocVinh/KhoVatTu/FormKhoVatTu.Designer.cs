namespace QuanLyThietBi_Winform_NguyenPhuocVinh.KhoVatTu
{
    partial class FormKhoVatTu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKhoVatTu));
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnThem = new DevExpress.XtraBars.BarButtonItem();
            this.btnSua = new DevExpress.XtraBars.BarButtonItem();
            this.btnXoa = new DevExpress.XtraBars.BarButtonItem();
            this.btnLuu = new DevExpress.XtraBars.BarButtonItem();
            this.btnHuy = new DevExpress.XtraBars.BarButtonItem();
            this.btnThoat = new DevExpress.XtraBars.BarButtonItem();
            this.btnNhapKho = new DevExpress.XtraBars.BarButtonItem();
            this.btnXuatKho = new DevExpress.XtraBars.BarButtonItem();
            this.btnIn = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txt_timkiem = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btn_TaoQr = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.label_ViTri = new System.Windows.Forms.Label();
            this.picHinhAnh = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_MaVatTu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_ViTri = new System.Windows.Forms.ComboBox();
            this.txt_TenVatTu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_SoLuong = new System.Windows.Forms.TextBox();
            this.label_Nguong = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_NguongToiThieu = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "In";
            this.barButtonItem2.Id = 6;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1228, 269);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gridView1.Appearance.FilterPanel.Options.UseFont = true;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsFind.FindNullPrompt = "Nhập dữ liệu muốn tìm...";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnThem,
            this.btnSua,
            this.btnLuu,
            this.btnXoa,
            this.btnHuy,
            this.btnThoat,
            this.btnIn,
            this.txt_timkiem,
            this.barButtonItem1,
            this.btn_TaoQr,
            this.barButtonItem3,
            this.btnNhapKho,
            this.btnXuatKho});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 16;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSua, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnXoa, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnHuy, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnThoat, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNhapKho, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnXuatKho, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnIn, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnThem
            // 
            this.btnThem.Caption = "Thêm";
            this.btnThem.Id = 0;
            this.btnThem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnThem.ImageOptions.SvgImage")));
            this.btnThem.Name = "btnThem";
            this.btnThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThem_ItemClick);
            // 
            // btnSua
            // 
            this.btnSua.Caption = "Sửa";
            this.btnSua.Id = 1;
            this.btnSua.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSua.ImageOptions.SvgImage")));
            this.btnSua.Name = "btnSua";
            this.btnSua.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSua_ItemClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Caption = "Xoá";
            this.btnXoa.Id = 3;
            this.btnXoa.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnXoa.ImageOptions.SvgImage")));
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXoa_ItemClick);
            // 
            // btnLuu
            // 
            this.btnLuu.Caption = "Lưu";
            this.btnLuu.Id = 2;
            this.btnLuu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLuu.ImageOptions.SvgImage")));
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLuu_ItemClick);
            // 
            // btnHuy
            // 
            this.btnHuy.Caption = "Huỷ";
            this.btnHuy.Id = 4;
            this.btnHuy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnHuy.ImageOptions.SvgImage")));
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHuy_ItemClick);
            // 
            // btnThoat
            // 
            this.btnThoat.Caption = "Thoát";
            this.btnThoat.Id = 5;
            this.btnThoat.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnThoat.ImageOptions.SvgImage")));
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThoat_ItemClick);
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Caption = "Nhập kho";
            this.btnNhapKho.Id = 14;
            this.btnNhapKho.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNhapKho.ImageOptions.SvgImage")));
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNhapKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNhapKho_ItemClick);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Caption = "Xuất kho";
            this.btnXuatKho.Id = 15;
            this.btnXuatKho.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnXuatKho.ImageOptions.SvgImage")));
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnXuatKho.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnXuatKho_ItemClick);
            // 
            // btnIn
            // 
            this.btnIn.Caption = "In";
            this.btnIn.Id = 6;
            this.btnIn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIn.ImageOptions.SvgImage")));
            this.btnIn.Name = "btnIn";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1228, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 708);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1228, 20);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 678);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1228, 30);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 678);
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Caption = "Tìm kiếm";
            this.txt_timkiem.Edit = this.repositoryItemTextEdit1;
            this.txt_timkiem.Id = 10;
            this.txt_timkiem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txt_timkiem.ImageOptions.SvgImage")));
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btn_TaoQr
            // 
            this.btn_TaoQr.Caption = "Tạo mã QR";
            this.btn_TaoQr.Id = 12;
            this.btn_TaoQr.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btn_TaoQr.ImageOptions.SvgImage")));
            this.btn_TaoQr.Name = "btn_TaoQr";
            this.btn_TaoQr.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 13;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // label_ViTri
            // 
            this.label_ViTri.AutoSize = true;
            this.label_ViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label_ViTri.Location = new System.Drawing.Point(25, 229);
            this.label_ViTri.Name = "label_ViTri";
            this.label_ViTri.Size = new System.Drawing.Size(50, 20);
            this.label_ViTri.TabIndex = 27;
            this.label_ViTri.Text = "Vị trí";
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHinhAnh.Image = global::QuanLyThietBi_Winform_NguyenPhuocVinh.Properties.Resources.nonimg;
            this.picHinhAnh.Location = new System.Drawing.Point(50, 34);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(297, 308);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHinhAnh.TabIndex = 27;
            this.picHinhAnh.TabStop = false;
            this.picHinhAnh.Click += new System.EventHandler(this.picHinhAnh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_MaVatTu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label_ViTri);
            this.groupBox1.Controls.Add(this.cbo_ViTri);
            this.groupBox1.Controls.Add(this.txt_TenVatTu);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_SoLuong);
            this.groupBox1.Controls.Add(this.label_Nguong);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_NguongToiThieu);
            this.groupBox1.Location = new System.Drawing.Point(390, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 308);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin vật tư";
            // 
            // txt_MaVatTu
            // 
            this.txt_MaVatTu.AutoSize = true;
            this.txt_MaVatTu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_MaVatTu.Location = new System.Drawing.Point(176, 47);
            this.txt_MaVatTu.Name = "txt_MaVatTu";
            this.txt_MaVatTu.Size = new System.Drawing.Size(59, 20);
            this.txt_MaVatTu.TabIndex = 30;
            this.txt_MaVatTu.Text = "02463";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "Mã vật tư";
            // 
            // cbo_ViTri
            // 
            this.cbo_ViTri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_ViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbo_ViTri.FormattingEnabled = true;
            this.cbo_ViTri.Location = new System.Drawing.Point(180, 228);
            this.cbo_ViTri.Name = "cbo_ViTri";
            this.cbo_ViTri.Size = new System.Drawing.Size(228, 28);
            this.cbo_ViTri.TabIndex = 28;
            // 
            // txt_TenVatTu
            // 
            this.txt_TenVatTu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_TenVatTu.Location = new System.Drawing.Point(180, 87);
            this.txt_TenVatTu.Name = "txt_TenVatTu";
            this.txt_TenVatTu.Size = new System.Drawing.Size(228, 27);
            this.txt_TenVatTu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(25, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên vật tư";
            // 
            // txt_SoLuong
            // 
            this.txt_SoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_SoLuong.Location = new System.Drawing.Point(180, 134);
            this.txt_SoLuong.Name = "txt_SoLuong";
            this.txt_SoLuong.Size = new System.Drawing.Size(228, 27);
            this.txt_SoLuong.TabIndex = 24;
            // 
            // label_Nguong
            // 
            this.label_Nguong.AutoSize = true;
            this.label_Nguong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label_Nguong.Location = new System.Drawing.Point(25, 183);
            this.label_Nguong.Name = "label_Nguong";
            this.label_Nguong.Size = new System.Drawing.Size(146, 20);
            this.label_Nguong.TabIndex = 6;
            this.label_Nguong.Text = "Ngưỡng tối thiểu";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(25, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "Số Lượng";
            // 
            // txt_NguongToiThieu
            // 
            this.txt_NguongToiThieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_NguongToiThieu.Location = new System.Drawing.Point(180, 181);
            this.txt_NguongToiThieu.Name = "txt_NguongToiThieu";
            this.txt_NguongToiThieu.Size = new System.Drawing.Size(228, 27);
            this.txt_NguongToiThieu.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.picHinhAnh);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1228, 678);
            this.splitContainer1.SplitterDistance = 405;
            this.splitContainer1.TabIndex = 8;
            // 
            // FormKhoVatTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 728);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormKhoVatTu";
            this.Text = "KhoVatTu";
            this.Load += new System.EventHandler(this.FormDanhSachVatTu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnThem;
        private DevExpress.XtraBars.BarButtonItem btnSua;
        private DevExpress.XtraBars.BarButtonItem btnXoa;
        private DevExpress.XtraBars.BarButtonItem btnLuu;
        private DevExpress.XtraBars.BarButtonItem btn_TaoQr;
        private DevExpress.XtraBars.BarButtonItem btnHuy;
        private DevExpress.XtraBars.BarButtonItem btnThoat;
        private DevExpress.XtraBars.BarButtonItem btnIn;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox picHinhAnh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_ViTri;
        private System.Windows.Forms.ComboBox cbo_ViTri;
        private System.Windows.Forms.TextBox txt_TenVatTu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_SoLuong;
        private System.Windows.Forms.Label label_Nguong;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_NguongToiThieu;
        private DevExpress.XtraBars.BarEditItem txt_timkiem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private System.Windows.Forms.Label txt_MaVatTu;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraBars.BarButtonItem btnNhapKho;
        private DevExpress.XtraBars.BarButtonItem btnXuatKho;
    }
}