using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TN_CSDLPT
{
    public partial class FormLop : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Tạo biến tạm để lưu dữ liệu quay lại;
        string tempMaLop = "", tempTenLop = "", tempMaKhoa = "";
        public FormLop()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Quyền đối với nhà trường
        private void QuyenNhaTruong()
        {
            label6.Visible = comboBox2.Visible = button1.Visible = true;

            comboBox2.Items.Add("CS1"); comboBox2.Items.Add("CS2");
        }
        private void NhomQuyen()
        {
            if (Program.mGroup == "TRUONG") 
            {
                QuyenNhaTruong();
                menuStrip1.Enabled = false;
            } 
            if (Program.mGroup == "COSO") menuStrip1.Enabled = true;
            if (Program.mGroup == "GIANGVIEN") menuStrip1.Enabled = false;
        }
        private void FormLop_Load(object sender, EventArgs e)
        {
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            LayDSKHOA();
            HienThiNut();
            NhomQuyen();
        }

        //Hiện thị danh sách khoa
        private void HienThiDuLieuGridView()
        {
            String strlenh = "select MAKH,TENKH,MACS from KHOA";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "Mã khoa";
            dataGridView1.Columns[1].HeaderText = "Tên khoa";
            dataGridView1.Columns[2].HeaderText = "Mã cơ sở";
            conn_publisher.Close();
        }

        //Hiện thị danh sách lớp
        private void HienThiDuLieuGridView1()
        {
            String strlenh = "select MALOP, TENLOP, MAKH from LOP";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView2.DataSource = dt;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView2.Columns[0].HeaderText = "Mã lớp";
            dataGridView2.Columns[1].HeaderText = "Tên lớp";
            dataGridView2.Columns[2].HeaderText = "Mã khoa";
            conn_publisher.Close();
        }

        //lấy danh sách khoa
        private void LayDSKHOA()
        {
            DataTable dt = new DataTable();
            String strlenh = "select MAKH,TENKH,MACS from KHOA";
            dt = Program.ExecSqlDataTable(strlenh);

            comboBox1.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox1.DisplayMember = "MAKH";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();
        }

        //Tạo sự kiện gridview khoa
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        //Tạo sự kiện gridview lớp
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
            }
        }

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //--------------text
            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = false;
        }

        //Thêm
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "add";
            lưuToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
            thoátToolStripMenuItem.Enabled = true;
            textBox1.Text = ""; textBox2.Text = "" ; comboBox1.Text = "";

            //------------text
            textBox1.Enabled = textBox2.Enabled = comboBox1.Enabled = true;
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
            textBox1.Enabled = comboBox1.Enabled = false; textBox2.Enabled = true;
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = tempMaLop;
            textBox2.Text = tempTenLop;
            comboBox1.Text = tempMaKhoa;
        }

        //Refresh
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            HienThiNut();
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
        private bool Check_Trung(String MALOP)
        {
            String strlenh = "select MALOP from LINK0.TN_CSDLPT.dbo.LOP";
            dt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MALOP"].ToString();
                if (maDBGridView.Trim() == MALOP.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        //Xem danh sách theo từng cơ sở
        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.MACOSO == "CS1")
            {
                if (comboBox2.Text.ToString().Trim() == "CS1")
                {
                    String strlenh = "select MAKH, TENKH, MACS from KHOA";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã Khoa";
                    dataGridView1.Columns[1].HeaderText = "Tên Khoa";
                    dataGridView1.Columns[2].HeaderText = "Mã cơ sở";
                    conn_publisher.Close();

                    String str = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã lớp";
                    dataGridView2.Columns[1].HeaderText = "Tên lớp";
                    dataGridView2.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();
                }
                if (comboBox2.Text.ToString().Trim() == "CS2")
                {
                    String strlenh = "select MAKH, TENKH, MACS from LINK1.TN_CSDLPT.DBO.KHOA";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã khoa";
                    dataGridView1.Columns[1].HeaderText = "Tên tên";
                    dataGridView1.Columns[2].HeaderText = "Mã cơ sở";
                    conn_publisher.Close();

                    String str = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã lớp";
                    dataGridView2.Columns[1].HeaderText = "Tên lớp";
                    dataGridView2.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();
                }
            }

            if (Program.MACOSO == "CS2")
            {
                if (comboBox2.Text.ToString().Trim() == "CS1")
                {
                    String strlenh = "select MAKH, TENKH, MACS from LINK1.TN_CSDLPT.DBO.KHOA";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã khoa";
                    dataGridView1.Columns[1].HeaderText = "Tên tên";
                    dataGridView1.Columns[2].HeaderText = "Mã cơ sở";
                    conn_publisher.Close();

                    String str = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã lớp";
                    dataGridView2.Columns[1].HeaderText = "Tên lớp";
                    dataGridView2.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();
                }
                if (comboBox2.Text.ToString().Trim() == "CS2")
                {
                    String strlenh = "select MAKH, TENKH, MACS from KHOA";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã Khoa";
                    dataGridView1.Columns[1].HeaderText = "Tên Khoa";
                    dataGridView1.Columns[2].HeaderText = "Mã cơ sở";
                    conn_publisher.Close();

                    String str = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã lớp";
                    dataGridView2.Columns[1].HeaderText = "Tên lớp";
                    dataGridView2.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private Form IsExists(Type type)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == type) return f;
            }
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
                //f.MdiParent = this;
                f.Show();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            showForm(typeof(Formbangdiemlop));
        }

        //Lưu
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempMaLop = textBox1.Text.ToString().Trim();
            tempTenLop = textBox2.Text.ToString().Trim();
            tempMaKhoa = comboBox1.Text.ToString().Trim();
           
            //Kiểm tra dữ liệu nhập vào:
            if (Check_NULL(textBox1, "Mã lớp học không được để trống!")) return;
            if (Check_NULL(textBox2, "Tên lớp học không được để trống!")) return;
            if (Check_NULL_CBX(comboBox1, "Mã khoa không được để trống!")) return;
            if (textBox1.Text.Trim().Length > 16 || 2 > textBox1.Text.Trim().Length)
            {
                MessageBox.Show("Mã lớp phải từ 2 đến 15 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            else if (Check_Trung(textBox1.Text.Trim()) && flag == "add")
            {
                MessageBox.Show("Mã lớp học đã tồn tại ở các cơ sở", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Xử lý
            //Thêm
            if (flag == "add")
            {
                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Mã lớp học không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String strLenh = "SP_Addlop";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaLop", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TenLop", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", comboBox1.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Thêm lớp học thành công!");
                HienThiDuLieuGridView1();

            }

            //Xóa
            else if (flag == "delete")
            {
                String strLenh = "SP_Deletelop";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaLop", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@TENKHOA", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Xóa lớp học thành công!");
                HienThiDuLieuGridView1();
            }

            //sửa
            else if (flag == "edit")
            {
                String strLenh = "SP_Editlop";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaLop", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TenLop", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", comboBox1.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Sửa lớp học thành công!");
                HienThiDuLieuGridView1();
            }
        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
