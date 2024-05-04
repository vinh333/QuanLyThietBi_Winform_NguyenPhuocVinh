﻿using DevExpress.XtraBars;
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
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ribbonControl1.SelectedPage = ribbonPage2;
        }
        void openform(Type typeForm)
        {
            foreach (var frm in MdiChildren)
            {
                if (frm.GetType() == typeForm)
                {
                    frm.Activate();
                    return;
                }
            }
            // Nếu không tìm thấy form đã mở, tạo một instance mới
            Form f = (Form)Activator.CreateInstance(typeForm);
            f.MdiParent = this;
            f.Show();
        }

        private void btnThietBi_ItemClick(object sender, ItemClickEventArgs e)
        {
            openform(typeof(FormDanhSachThietBi));

        }

        private void btnChucNang_ItemClick(object sender, ItemClickEventArgs e)
        {
            openform(typeof(FormChucNangThietBi));

        }

        private void btnLoaiThietBi_ItemClick(object sender, ItemClickEventArgs e)
        {
            openform(typeof(FormLoaiThietBi));

        }

        private void btnVitri_ItemClick(object sender, ItemClickEventArgs e)
        {
            openform(typeof(FormViTriThietBi));

        }

        private void btnLichSuSuDung_ItemClick(object sender, ItemClickEventArgs e)
        {
            openform(typeof(FormLichSuSuDung));
        }
    }
}