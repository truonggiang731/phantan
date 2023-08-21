namespace TN_CSDLPT
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnLogin = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSubject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSVL = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonKhoa = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonGV = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDKThi = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonThi = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonKQThi = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRegister = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnExit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSinhVien = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribManagement = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.Mônhoc = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribThi = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.statusMaUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusHoTen = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusNhom = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(107, 111, 107, 111);
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barBtnLogin,
            this.barBtnSubject,
            this.barButtonItem4,
            this.barButtonSVL,
            this.barButtonKhoa,
            this.barButtonGV,
            this.barButtonDKThi,
            this.barButtonThi,
            this.barButtonKQThi,
            this.barBtnRegister,
            this.barBtnExit,
            this.barButtonSinhVien,
            this.barButtonItem3,
            this.barButtonItem5,
            this.barButtonItem6});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.ribbonControl1.MaxItemId = 19;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.OptionsMenuMinWidth = 1174;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribManagement,
            this.ribThi});
            this.ribbonControl1.Size = new System.Drawing.Size(884, 193);
            this.ribbonControl1.Click += new System.EventHandler(this.ribbonControl1_Click);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barBtnLogin
            // 
            this.barBtnLogin.Caption = "Đăng nhập";
            this.barBtnLogin.Id = 4;
            this.barBtnLogin.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnLogin.ImageOptions.Image")));
            this.barBtnLogin.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnLogin.ImageOptions.LargeImage")));
            this.barBtnLogin.Name = "barBtnLogin";
            // 
            // barBtnSubject
            // 
            this.barBtnSubject.Caption = "Môn học";
            this.barBtnSubject.Hint = "Quản lý môn học";
            this.barBtnSubject.Id = 5;
            this.barBtnSubject.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSubject.ImageOptions.Image")));
            this.barBtnSubject.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSubject.ImageOptions.LargeImage")));
            this.barBtnSubject.Name = "barBtnSubject";
            this.barBtnSubject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Đề thi";
            this.barButtonItem4.Id = 6;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonSVL
            // 
            this.barButtonSVL.Caption = "Lớp";
            this.barButtonSVL.Id = 7;
            this.barButtonSVL.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonSVL.ImageOptions.Image")));
            this.barButtonSVL.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonSVL.ImageOptions.LargeImage")));
            this.barButtonSVL.Name = "barButtonSVL";
            this.barButtonSVL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSVL_ItemClick);
            // 
            // barButtonKhoa
            // 
            this.barButtonKhoa.Caption = "Khoa";
            this.barButtonKhoa.Id = 8;
            this.barButtonKhoa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonKhoa.ImageOptions.Image")));
            this.barButtonKhoa.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonKhoa.ImageOptions.LargeImage")));
            this.barButtonKhoa.Name = "barButtonKhoa";
            this.barButtonKhoa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonKhoa_ItemClick);
            // 
            // barButtonGV
            // 
            this.barButtonGV.Caption = "Giáo viên";
            this.barButtonGV.Id = 9;
            this.barButtonGV.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonGV.ImageOptions.Image")));
            this.barButtonGV.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonGV.ImageOptions.LargeImage")));
            this.barButtonGV.Name = "barButtonGV";
            this.barButtonGV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonGV_ItemClick);
            // 
            // barButtonDKThi
            // 
            this.barButtonDKThi.Caption = "Đăng ký thi";
            this.barButtonDKThi.Id = 10;
            this.barButtonDKThi.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonDKThi.ImageOptions.Image")));
            this.barButtonDKThi.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonDKThi.ImageOptions.LargeImage")));
            this.barButtonDKThi.Name = "barButtonDKThi";
            this.barButtonDKThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDKThi_ItemClick);
            // 
            // barButtonThi
            // 
            this.barButtonThi.Caption = "Thi";
            this.barButtonThi.Id = 11;
            this.barButtonThi.ImageOptions.Image = global::TN_CSDLPT.Properties.Resources.time_500px;
            this.barButtonThi.Name = "barButtonThi";
            this.barButtonThi.VisibleInSearchMenu = false;
            this.barButtonThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonThi_ItemClick);
            // 
            // barButtonKQThi
            // 
            this.barButtonKQThi.Caption = "Kết quả thi";
            this.barButtonKQThi.Id = 12;
            this.barButtonKQThi.ImageOptions.Image = global::TN_CSDLPT.Properties.Resources.leaderboard_500px;
            this.barButtonKQThi.Name = "barButtonKQThi";
            this.barButtonKQThi.VisibleInSearchMenu = false;
            this.barButtonKQThi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonKQThi_ItemClick);
            // 
            // barBtnRegister
            // 
            this.barBtnRegister.Caption = "Đăng ký tài khoản";
            this.barBtnRegister.Id = 13;
            this.barBtnRegister.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRegister.ImageOptions.Image")));
            this.barBtnRegister.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRegister.ImageOptions.LargeImage")));
            this.barBtnRegister.Name = "barBtnRegister";
            // 
            // barBtnExit
            // 
            this.barBtnExit.Caption = "Đăng xuất";
            this.barBtnExit.Id = 14;
            this.barBtnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnExit.ImageOptions.Image")));
            this.barBtnExit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnExit.ImageOptions.LargeImage")));
            this.barBtnExit.Name = "barBtnExit";
            this.barBtnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonThoat_ItemClick);
            // 
            // barButtonSinhVien
            // 
            this.barButtonSinhVien.Caption = "Sinh viên";
            this.barButtonSinhVien.Id = 15;
            this.barButtonSinhVien.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonSinhVien.ImageOptions.Image")));
            this.barButtonSinhVien.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonSinhVien.ImageOptions.LargeImage")));
            this.barButtonSinhVien.Name = "barButtonSinhVien";
            this.barButtonSinhVien.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick_1);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Đổi mật khẩu";
            this.barButtonItem3.Id = 16;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Đăng ký";
            this.barButtonItem5.Id = 17;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup4});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Quản trị";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtnExit);
            this.ribbonPageGroup1.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.OneRow;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.barButtonItem5);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Visible = false;
            // 
            // ribManagement
            // 
            this.ribManagement.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.Mônhoc,
            this.ribbonPageGroup2});
            this.ribManagement.Name = "ribManagement";
            this.ribManagement.Text = "Quản lý";
            // 
            // Mônhoc
            // 
            this.Mônhoc.ItemLinks.Add(this.barBtnSubject);
            this.Mônhoc.ItemLinks.Add(this.barButtonItem4);
            this.Mônhoc.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.OneRow;
            this.Mônhoc.Name = "Mônhoc";
            this.Mônhoc.Text = "Môn học";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonSVL);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonKhoa);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonGV);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonSinhVien);
            this.ribbonPageGroup2.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.OneRow;
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Phòng ban";
            // 
            // ribThi
            // 
            this.ribThi.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3,
            this.ribbonPageGroup5});
            this.ribThi.Name = "ribThi";
            this.ribThi.Text = "Thi";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonDKThi);
            this.ribbonPageGroup3.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.OneRow;
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // statusMaUser
            // 
            this.statusMaUser.Name = "statusMaUser";
            this.statusMaUser.Size = new System.Drawing.Size(49, 20);
            this.statusMaUser.Text = "MaGV";
            // 
            // statusHoTen
            // 
            this.statusHoTen.Name = "statusHoTen";
            this.statusHoTen.Size = new System.Drawing.Size(57, 20);
            this.statusHoTen.Text = "HOTEN";
            // 
            // statusNhom
            // 
            this.statusNhom.Name = "statusNhom";
            this.statusNhom.Size = new System.Drawing.Size(55, 20);
            this.statusNhom.Text = "NHOM";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMaUser,
            this.statusHoTen,
            this.statusNhom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 417);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.barButtonItem6);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Thi thử";
            this.barButtonItem6.Id = 18;
            this.barButtonItem6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.Image")));
            this.barButtonItem6.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.LargeImage")));
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 443);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Ribbon = this.ribbonControl1;
            this.Text = "x";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarButtonItem barBtnLogin;
        private DevExpress.XtraBars.BarButtonItem barBtnSubject;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribManagement;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup Mônhoc;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribThi;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonSVL;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonKhoa;
        private DevExpress.XtraBars.BarButtonItem barButtonGV;
        private DevExpress.XtraBars.BarButtonItem barButtonDKThi;
        private DevExpress.XtraBars.BarButtonItem barButtonThi;
        private DevExpress.XtraBars.BarButtonItem barButtonKQThi;
        private DevExpress.XtraBars.BarButtonItem barBtnRegister;
        private DevExpress.XtraBars.BarButtonItem barBtnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel statusMaUser;
        public System.Windows.Forms.ToolStripStatusLabel statusHoTen;
        public System.Windows.Forms.ToolStripStatusLabel statusNhom;
        private DevExpress.XtraBars.BarButtonItem barButtonSinhVien;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
    }
}

