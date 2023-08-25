using DevExpress.Utils;
using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TN_CSDLPT;

namespace TN_CSDLPT
{



    public partial class frmDangNhap : Form

    {

        public static SqlConnection conn_publisher = new SqlConnection();

        private Boolean isSinhVien = false;

        private void LayDSCOSO(String cmd)
        {
            DataTable dt = new DataTable();
            if (conn_publisher.State == ConnectionState.Closed) conn_publisher.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd, conn_publisher);
            da.Fill(dt);
            conn_publisher.Close();

            Program.bds_dspm.DataSource = dt;
            cmbCoSo.DataSource = Program.bds_dspm;
           // cmbCoSo.DisplayMember = "MaKhoa";
           // cmbCoSo.ValueMember = "TenKhoa";

            cmbCoSo.DisplayMember = "TenCS"; // hiện thị
            cmbCoSo.ValueMember = "TENSERVER";   // giá trị
        }

        public static int ketnoi_maychu()
        {
            if (conn_publisher != null && conn_publisher.State == ConnectionState.Open)
                conn_publisher.Close();
            try
            {
                conn_publisher.ConnectionString = Program.connstr_Publisher;
                conn_publisher.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu gốc. " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.Get_Subscribes' table. You can move, or remove it, as needed.
            this.WindowState = FormWindowState.Maximized;
            // this.get_SubscribesTableAdapter.Fill(this.dS.Get_Subscribes);
            // cmbCoSo.SelectedIndex = 1;
            //cmbCoSo.SelectedIndex = 0;
            this.txtPass.PasswordChar = '*';  // set aterisk for password input

            if (ketnoi_maychu() == 0) return;
            LayDSCOSO("select *from Get_Subscribes");
            cmbCoSo.SelectedIndex = 0;
            Program.servername = cmbCoSo.SelectedValue.ToString();
        }

        public frmDangNhap()
        {
            InitializeComponent();
        }

        //kIỂM TRA FROM CÓ TỒN TẠI KO
        private Form IsExists(Type type)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == type) return f;
            }
            return null;
        }

        private void labelCoSo_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }


        // lấy ra cơ sở được chọn lưu vào servername toàn cục ở program
        private void cmbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCoSo.SelectedValue != null)
            {
                Program.servername = cmbCoSo.SelectedValue.ToString();
                // gán để sử dụng cơ sở đã chọn sau khi login xong ở các chức năng khác
                // Program.mCoSo = cmbCoSo.SelectedIndex;
            }
        }

      /*  private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }*/

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            if (txtUsername.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                //MessageBox.Show("Tài khoản đăng nhập và mật khẩu không được để trống", "Báo lỗi đăng nhập", MessageBoxButtons.OK);

                XtraMessageBox.Show("Tài khoản đăng nhập và mật khẩu không được để trống ?", "Warning");
                txtUsername.Focus();
                return;
            }
            Program.mlogin = txtUsername.Text.Trim();
            Program.password = txtPass.Text.Trim();


            if (Program.KetNoi() == 0) return;

            Program.mChinhanh = cmbCoSo.SelectedIndex;
            Program.mloginDN = Program.mlogin;
            Program.passwordDN = Program.password;

            if (Program.servername == "DESKTOP-J73EN84\\COSO1") Program.MACOSO = "CS1";
            if (Program.servername == "DESKTOP-J73EN84\\COSO2") Program.MACOSO = "CS2";


            if (isSinhVien)
             {
                String strleng = "exec SP_LayThongTinSinhVien '" + Program.mloginDN + "'";
                Program.myReader = Program.ExecSqlDataReader(strleng);

                if (Program.myReader == null) return;
                Program.myReader.Read();

                Program.username = Program.myReader.GetString(0);
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login khong co quyen truy cap!");
                    return;
                }
                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);

                Program.myReader.Close();
                Program.conn.Close();


                //--------------mở from---------------------------------
                Form ftm = this.IsExists(typeof(FromSV));
                if (ftm != null)
                {
                    ftm.Activate();
                }
                else
                {
                    FromSV f = new FromSV();
                    //f.MdiParent = this;
                    f.Show();
                }
                //--------------mở from---------------------------------

            }
            else
             {

                String strleng = "exec SP_LayThongTinGiaoVien '" + Program.mloginDN + "'";
                Program.myReader = Program.ExecSqlDataReader(strleng);

                if (Program.myReader == null) return;
                Program.myReader.Read();

                Program.username = Program.myReader.GetString(0);
                if (Convert.IsDBNull(Program.username))
                {
                    MessageBox.Show("Login khong co quyen truy cap!");
                    return;
                }
                Program.mHoten = Program.myReader.GetString(1);
                Program.mGroup = Program.myReader.GetString(2);

                Program.myReader.Close();
                Program.conn.Close();


                //--------------mở from---------------------------------
                Form ftm = this.IsExists(typeof(frmMain));
                if (ftm != null)
                {
                    ftm.Activate();
                }
                else
                {
                    frmMain f = new frmMain();
                    //f.MdiParent = this;
                    f.Show();
                }
                //--------------mở from---------------------------------
            }
        }

        private void vDSPHANMANHBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ckbSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            if (!isSinhVien)
            {
                labelTaiKhoan.Text = "Sinh viên";
                isSinhVien = true;
            }
            else
            {
                labelTaiKhoan.Text = "Giáo viên";
                isSinhVien = false;
            }
        }

        
    }
}
