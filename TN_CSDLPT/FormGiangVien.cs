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

namespace TN_CSDLPT
{
    public partial class FormGiangVien : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Tạo biến tạm để lưu dữ liệu quay lại;
        string temptextbox1 = "", temptextbox2 = "", temptextbox3 = "", temptextbox4 = "", tempcombobox3 = "";

        //Thêm
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "add";
            lưuToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = false; undoToolStripMenuItem.Enabled = true;
            thoátToolStripMenuItem.Enabled = true;
            textBox1.Text = ""; textBox2.Text = ""; comboBox3.Text = "";
            textBox3.Text = ""; textBox4.Text = ""; 

            //------------text
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = comboBox3.Enabled = true;
        }

        //Xóa
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "delete";
            lưuToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = false;
            xóaToolStripMenuItem.Enabled = true;
            sửaToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = false;
            thoátToolStripMenuItem.Enabled = true;
        }

        //Sửa
        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "edit";
            lưuToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = false;
            xóaToolStripMenuItem.Enabled = false;
            sửaToolStripMenuItem.Enabled = true;
            undoToolStripMenuItem.Enabled = false;
            thoátToolStripMenuItem.Enabled = true;

            //---------------text
            textBox1.Enabled = false; textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true; comboBox3.Enabled = false;
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = temptextbox1;
            textBox2.Text = temptextbox2;
            textBox3.Text = temptextbox3;
            textBox4.Text = temptextbox4;
            comboBox3.Text = tempcombobox3;
        }

        //Refresh
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            HienThiDuLieuGridView();
            HienThiNut();
        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Kiểm tra text có rỗng không
        private bool Check_NULL(TextBox tb, string str)
        {
            if (tb.Text.Trim().Equals(""))
            {
                MessageBox.Show(str, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Focus();
                return true;
            }
            return false;

        }

        //Kiểm tra text có rỗng không
        private bool Check_NULL_CBX(ComboBox tb, string str)
        {
            if (tb.Text.Trim().Equals(""))
            {
                MessageBox.Show(str, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Focus();
                return true;
            }
            return false;

        }

        //Kiểm tra mã có trùng không
        private bool Check_Trung(String MAGV)
        {
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MAGV"].ToString();
                if (maDBGridView.Trim() == MAGV.Trim())
                {
                    return true;
                }
            }
            return false;
        }
        //Lưu
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temptextbox1 = textBox1.Text.ToString().Trim();
            temptextbox2 = textBox2.Text.ToString().Trim();
            temptextbox3 = textBox3.Text.ToString().Trim();
            temptextbox4 = textBox4.Text.ToString().Trim();
            tempcombobox3 = comboBox3.Text.ToString().Trim();
          
            //Kiểm tra dữ liệu nhập vào:
            if (Check_NULL(textBox1, "Mã giảng viên không được để trống!")) return;
            if (Check_NULL(textBox2, "Họ giảng viên không được để trống!")) return;
            if (Check_NULL(textBox3, "Địa chỉ giảng viên không được để trống!")) return;
            if (Check_NULL(textBox4, "Tên giảng viên không được để trống!")) return;
            if (Check_NULL_CBX(comboBox3, "Mã khoa không được để trống!")) return;

            if (textBox1.Text.Trim().Length > 9 || 2 > textBox1.Text.Trim().Length)
            {
                MessageBox.Show("Mã giảng viên phải từ 2 đến 8 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (Check_Trung(textBox1.Text.Trim()) && flag == "add")
            {
                MessageBox.Show("Mã giảng viên đã tồn tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Xử lý
            //Thêm
            if (flag == "add")
            {
                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Mã giảng viên không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String strLenh = "sp_AddGiangVien";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaGv", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ho", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ten", textBox4.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@DiaChi", textBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", comboBox3.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Thêm giảng viên thành công!");
                HienThiDuLieuGridView();

            }

            //Xóa
            else if (flag == "delete")
            {
                String strLenh = "sp_DeleteGiangVien";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaGv", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@TENKHOA", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Xóa giảng viên thành công!");
                HienThiDuLieuGridView();
            }

            //sửa
            else if (flag == "edit")
            {
                if (KiemTraGiangVienThuocCoSo(comboBox3.Text.ToString().Trim()) == true) 
                {
                    //Chỉ cho sửa những Giảng viên thuộc cơ sở.
                    String strLenh = "sp_EditGiangVien";
                    SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 600;

                    sqlCommand.Parameters.Add(new SqlParameter("@MaGv", textBox1.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@Ho", textBox2.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@Ten", textBox4.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@DiaChi", textBox3.Text.ToString().Trim()));

                    Program.ExecSQLCommand(sqlCommand, conn_publisher);
                    MessageBox.Show("Sửa giảng viên thành công!");
                    HienThiDuLieuGridView();
                }
                else if (KiemTraGiangVienThuocCoSo(comboBox3.Text.ToString().Trim()) == false)
                {
                    MessageBox.Show("Không có quyền sửa giảng viên thuộc cơ sở khác"); return;
                }
                
                
            }
        }

        //Hiện thị sự kiện gridview
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
            }
        }

        public FormGiangVien()
        {
            InitializeComponent();
        }

        //Phân quyền cho mGroup
        private void NhomQuyen()
        {
            if (Program.mGroup == "TRUONG") menuStrip1.Enabled = false;
            if (Program.mGroup == "COSO") menuStrip1.Enabled = true;
            if (Program.mGroup == "GIANGVIEN") menuStrip1.Enabled = false;
        }
        private void FormGiangVien_Load(object sender, EventArgs e)
        {
            HienThiDuLieuGridView();
            HienThiNut();
            LayDSKHOA();
            NhomQuyen();
        }

        //Hiện thị danh sách giang viên
        private void HienThiDuLieuGridView()
        {
            String strlenh = "select MAGV, HO, TEN, DIACHI, MAKH  from GIAOVIEN";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "Mã giảng viên";
            dataGridView1.Columns[1].HeaderText = "Họ";
            dataGridView1.Columns[2].HeaderText = "Tên";
            dataGridView1.Columns[3].HeaderText = "Địa chỉ";
            dataGridView1.Columns[4].HeaderText = "Mã khoa";
            conn_publisher.Close();
        }

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //--------------text
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = comboBox3.Enabled = false;
        }


        //lấy danh sách khoa
        private void LayDSKHOA()
        {
            DataTable dt = new DataTable();
            String strlenh = "select MAKH,TENKH,MACS from KHOA";
            dt = Program.ExecSqlDataTable(strlenh);

            comboBox3.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox3.DisplayMember = "MAKH";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();
        }

        //Kiểm tra giảng viên thuộc cơ sở
        private bool KiemTraGiangVienThuocCoSo(string MaKhoa)
        {
            DataTable kt = new DataTable();
            String strlenh = "select MAKH from KHOA";
            kt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in kt.Rows)
            {
                String maDBGridView = row["MAKH"].ToString();
                if (maDBGridView.Trim() == MaKhoa.Trim())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
