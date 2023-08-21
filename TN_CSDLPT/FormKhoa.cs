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
    public partial class FormKhoa : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Tạo biến tạm để lưu dữ liệu quay lại;
        string tempTextbox1 = "", tempTextbox2 = "", tempTextbox3 = Program.MACOSO;
        string tempTextbox4 = "", tempTextbox5 = "", tempTextbox6 = "";

        
        

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
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = Program.MACOSO;
            //textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";

            //------------text
            textBox1.Enabled = textBox2.Enabled = true;
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
            textBox1.Enabled = false; textBox2.Enabled = true;
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = tempTextbox1;
            textBox2.Text = tempTextbox2;

            textBox3.Text = tempTextbox3;
           // textBox4.Text = tempTextbox4;
           // textBox5.Text = tempTextbox5;
           // textBox6.Text = tempTextbox6;
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

        //Kiểm tra mã có trùng không
        private bool Check_Trung(String MAKHOA)
        {
            String strlenh = "select MAKH from LINK0.TN_CSDLPT.dbo.KHOA";
            dt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MAKH"].ToString();
                if (maDBGridView.Trim() == MAKHOA.Trim())
                {
                    return true;
                }
            }
            return false;
        }
        //Lưu
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempTextbox1 = textBox1.Text.ToString().Trim();
            tempTextbox2 = textBox2.Text.ToString().Trim();
            tempTextbox3 = textBox3.Text.ToString().Trim();
            //Kiểm tra dữ liệu nhập vào:
            if (Check_NULL(textBox1, "Mã khoa học không được để trống!")) return;
            if (Check_NULL(textBox2, "Tên khoa học không được để trống!")) return;
            //if (Check_NULL(textBox3, "Tên môn học không được để trống!")) return;
            if (textBox1.Text.Trim().Length > 9 || 2 > textBox1.Text.Trim().Length)
            {
                MessageBox.Show("Mã khoa phải từ 2 đến 9 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           /* else if (textBox1.Text.Contains(" "))
            {
                MessageBox.Show("Mã môn học không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            else if (Check_Trung(textBox1.Text.Trim()) && flag == "add")
            {
                MessageBox.Show("Mã khoa đã tồn tại ở các cơ sở", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Xử lý
            //Thêm
            if (flag == "add")
            {
                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Mã khoa không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String strLenh = "SP_AddKhoa";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TenKh", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaCs", textBox3.Text.ToString().Trim()));


                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Thêm khoa thành công!");
                HienThiDuLieuGridView();

            }

            //Xóa
            else if (flag == "delete")
            {
                String strLenh = "SP_DeleteKhoa";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@TENKHOA", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Xóa khoa thành công!");
                HienThiDuLieuGridView();
            }

            //sửa
            else if (flag == "edit")
            {
                String strLenh = "SP_EditKhoa";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaKh", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@MAMH", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TenKh", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaCs", textBox3.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Sửa môn học thành công!");
                HienThiDuLieuGridView();
            }
        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Tạo sự kiện khoa gridview
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        //Tạo sự kiện gridview lớp
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox4.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
                textBox5.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
                textBox6.Text = dataGridView2.Rows[index].Cells[2].Value.ToString();
            }
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

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //--------------text
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = false;
        }

        //Xem danh sách theo từng cơ sở
        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.MACOSO == "CS1")
            {
                if (comboBox1.Text.ToString().Trim() == "CS1")
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
                if (comboBox1.Text.ToString().Trim() == "CS2")
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
                if (comboBox1.Text.ToString().Trim() == "CS1")
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
                if (comboBox1.Text.ToString().Trim() == "CS2")
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

        public FormKhoa()
        {
            InitializeComponent();
        }

        //Phân quyền cho mGroup
        private void NhomQuyen()
        {
            if (Program.mGroup == "TRUONG") 
            {
                menuStrip1.Enabled = false;
                QuyenNhaTruong();
            } 
            if (Program.mGroup == "COSO") menuStrip1.Enabled = true;
            if (Program.mGroup == "GIANGVIEN") menuStrip1.Enabled = false;
        }

        private void FormKhoa_Load(object sender, EventArgs e)
        {
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            HienThiNut();
            NhomQuyen();

        }

        //Quyền đối với nhà trường
        private void QuyenNhaTruong()
        {
            label7.Visible = comboBox1.Visible = button1.Visible = true;

            comboBox1.Items.Add("CS1"); comboBox1.Items.Add("CS2");
        }
    }
}
