using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace TN_CSDLPT
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        public void initializeLogin()
        {
           // barBtnExit.Enabled = false;

            // invisible các ribbon page khác khi chưa login
            //ribManagement.Visible = false;
           // ribThi.Visible = false;

        }

        //cài đặt hiển thị tab menu 
       public void showMenu()
        {

            statusMaUser.Text = "Giảng viên: " + Program.username;
            statusNhom.Text = "Nhóm: " + Program.mGroup;
            statusHoTen.Text = "Họ tên: " + Program.mHoten;


         /*  if(Program.logout)
            {
                barBtnLogin.Enabled = true;
            }*/

            // sau khi login - vô hiệu đăng ký, login button
           /* if(Program.mGroup != null)
            {
                barBtnLogin.Enabled = false;
                barBtnRegister.Enabled = false;
                barBtnExit.Enabled = true;
                ribManagement.Visible = true;
            }
*/

           /* // phân chức năng theo quyền
            if (Program.mGroup.Equals("Sinh viên"))
            {
               ribThi.Visible = true;
            }

            else if(Program.mGroup.Equals("GIAOVIEN"))
            {
                ribThi.Visible = true;
            }
            else if (Program.mGroup.Equals("TRUONG") || Program.mGroup.Equals("COSO"))
            {
                ribThi.Visible = false;

            }*/

            
        }

        //Phân quyền
        private void PhanQuyen()
        {
            if (Program.mGroup == "GIANGVIEN") ribbonPageGroup4.Visible = false;
            if (Program.mGroup == "TRUONG" || Program.mGroup == "COSO") ribbonPageGroup4.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.initializeLogin();
            //btnDangNhap_ItemClick(this, null);
            this.WindowState = FormWindowState.Maximized;
            showMenu();
            PhanQuyen();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }


        public void showForm(Type frmType)
        {
            Form frm = this.CheckExists(frmType);
            if (frm != null) frm.Activate();
            else
            {
                Form f = (Form)Activator.CreateInstance(frmType);

                //gán cha của formDangNhap là form hiện tại và show lên
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn đăng xuất chứ ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == (System.Windows.Forms.DialogResult.Yes))
            {
                Close();               
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        //Kiểm tra from đã tồn tại chưa
        private Form IsExists(Type type)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == type) return f;
            }
            return null;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormMonHoc));
            Form ftm = this.IsExists(typeof(FormMonHoc));
            if (ftm != null)
            {
                ftm.Activate();
            }
            else
            {
                FormMonHoc f = new FormMonHoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonKhoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormKhoa));

        }

        private void barButtonSVL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormLop));
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormSinhVien));
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormBoDe));
        }

        private void barButtonGV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormGiangVien));
        }

        private void barButtonDKThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormDangKyThi));
        }

        private void barButtonThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormThi));
        }

        private void barButtonKQThi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormXemKetQua));
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FromDangKy));
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormThi));
        }
    }
}
