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

namespace TN_CSDLPT
{
    public partial class FromSV : Form
    {
        public FromSV()
        {
            InitializeComponent();
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
            /* Form ftm = this.IsExists(typeof(Dangnhap));
             if (ftm != null)
             {
                 ftm.Activate();
             }
             else
             {
                 Dangnhap f = new Dangnhap();
                 //f.MdiParent = this;
                 f.Show();
             }*/
        }

       public void HienThi()
        {
            toolStripStatusLabel1.Text = "Mã sinh viên: "+ Program.username;
            toolStripStatusLabel2.Text = "Họ tên: " + Program.mHoten;
            toolStripStatusLabel3.Text = "Nhóm: " + Program.mGroup;
        } 

        private void FromSV_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            HienThi();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormThi));
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showForm(typeof(FormXemKetQua));
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Bạn muốn đăng xuất chứ ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == (System.Windows.Forms.DialogResult.Yes))
            {
                Close();
            }
        }
    }
}
