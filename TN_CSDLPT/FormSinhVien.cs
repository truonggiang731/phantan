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
    public partial class FormSinhVien : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Tạo biến tạm để lưu dữ liệu quay lại;
        string temptextbox1 = "", temptextbox2 = "", temptextbox3 = "", temptextbox4 = "", temptextbox5 = "";
        string tempcombobox1 = "";
        DateTime tempngaysinh;

        //Thêm
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "add";
            lưuToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            xÓaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = false; undoToolStripMenuItem.Enabled = true;
            thoátToolStripMenuItem.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            //textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";

            //------------text
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox5.Enabled = comboBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        //Xóa
        private void xÓaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "delete";
            lưuToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = false;
            xÓaToolStripMenuItem.Enabled = true;
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
            xÓaToolStripMenuItem.Enabled = false;
            sửaToolStripMenuItem.Enabled = true;
            undoToolStripMenuItem.Enabled = false;
            thoátToolStripMenuItem.Enabled = true;

            //------------text
            textBox1.Enabled = false; textBox2.Enabled = textBox3.Enabled = textBox5.Enabled = true; comboBox1.Enabled = false;
            dateTimePicker1.Enabled = true;
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = temptextbox1;
            textBox2.Text = temptextbox2;
            textBox3.Text = temptextbox3;
            //textBox4.Text = temptextbox4;
            textBox5.Text = temptextbox5;
            comboBox1.Text = tempcombobox1;
            dateTimePicker1.Value = tempngaysinh;
        }

        //refresh
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
        private bool Check_Trung(String MASINHVIEN)
        {
            String strlenh = "select MASV from LINK0.TN_CSDLPT.dbo.SINHVIEN";
            dt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MASV"].ToString();
                if (maDBGridView.Trim() == MASINHVIEN.Trim())
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
            temptextbox2 = textBox2.Text;
            temptextbox3 = textBox3.Text;
            //textBox4.Text = temptextbox4;
            temptextbox5 = textBox5.Text;
            tempcombobox1 = comboBox1.Text.ToString().Trim();
            tempngaysinh = dateTimePicker1.Value;
           
            //Kiểm tra dữ liệu nhập vào:
            if (Check_NULL(textBox1, "Mã sinh viên không được để trống!")) return;
            if (Check_NULL(textBox2, "Họ sinh viên không được để trống!")) return;
            if (Check_NULL(textBox5, "Tên sinh viên không được để trống!")) return; 
            if (Check_NULL(textBox3, "Địa chỉ không được để trống!")) return;
            if (Check_NULL_CBX(comboBox1, "Mã lớp không được để trống!")) return;

            if (textBox1.Text.Trim().Length > 9 || 2 > textBox1.Text.Trim().Length)
            {
                MessageBox.Show("Mã môn học phải từ 2 đến 9 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (Check_Trung(textBox1.Text.Trim()) && flag == "add")
            {
                MessageBox.Show("Mã sinh viên đã tồn tại ở các cơ sở", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Xử lý
            //Thêm
            if (flag == "add")
            {
                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Mã sinh viên không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String strLenh = "sp_AddSinhVien";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaSv", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ho", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ten", textBox5.Text.ToString().Trim()));

                    //sqlCommand.Parameters.Add(new SqlParameter("@NgaySinh", textBox2.Text.ToString().Trim()));
                //---------------Kiểm tra thời gian trình độ môn thi đăng ký đã có chưa
                DateTime userDateTime = dateTimePicker1.Value;
                string DayUser = userDateTime.ToString();
                int spaceIndex = DayUser.IndexOf(' ');
                string result1 = "";
                if (spaceIndex != -1)
                {
                    result1 = DayUser.Trim().Substring(0, spaceIndex);
                    Console.WriteLine(result1);
                }
                //string Ngay = "11/12/2023";

                int firstSlashIndex = result1.IndexOf('/');
                int secondSlashIndex = result1.IndexOf('/', firstSlashIndex + 1);

                string thang = result1.Substring(0, firstSlashIndex);
                string ngay = result1.Substring(firstSlashIndex + 1, secondSlashIndex - firstSlashIndex - 1);
                string nam = result1.Substring(secondSlashIndex + 1);

                string day = nam + "-" + thang + "-" + ngay;
                SqlParameter parameter1 = new SqlParameter("@NgaySinh", SqlDbType.Char);
                parameter1.Value = day;
                sqlCommand.Parameters.Add(parameter1);
                    //sqlCommand.Parameters.Add(new SqlParameter("@NgaySinh", textBox2.Text.ToString().Trim()));

                sqlCommand.Parameters.Add(new SqlParameter("@DiaChi", textBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaLop", comboBox1.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Thêm sinh viên thành công!");
                HienThiDuLieuGridView1();

            }

            //Xóa
            else if (flag == "delete")
            {
                String strLenh = "sp_DeleteSinhVien";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaSv", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@TENKHOA", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Xóa sinh viên thành công!");
                HienThiDuLieuGridView1();
            }

            //sửa
            else if (flag == "edit")
            {

                String strLenh = "sp_EditSinhVien";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MaSv", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ho", textBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Ten", textBox5.Text.ToString().Trim()));

                //sqlCommand.Parameters.Add(new SqlParameter("@NgaySinh", textBox2.Text.ToString().Trim()));
                //---------------Kiểm tra thời gian trình độ môn thi đăng ký đã có chưa
                DateTime userDateTime = dateTimePicker1.Value;
                string DayUser = userDateTime.ToString();
                int spaceIndex = DayUser.IndexOf(' ');
                string result1 = "";
                if (spaceIndex != -1)
                {
                    result1 = DayUser.Trim().Substring(0, spaceIndex);
                    Console.WriteLine(result1);
                }
                //string Ngay = "11/12/2023";

                int firstSlashIndex = result1.IndexOf('/');
                int secondSlashIndex = result1.IndexOf('/', firstSlashIndex + 1);

                string thang = result1.Substring(0, firstSlashIndex);
                string ngay = result1.Substring(firstSlashIndex + 1, secondSlashIndex - firstSlashIndex - 1);
                string nam = result1.Substring(secondSlashIndex + 1);

                string day = nam + "-" + thang + "-" + ngay;
                SqlParameter parameter1 = new SqlParameter("@NgaySinh", SqlDbType.Char);
                parameter1.Value = day;
                sqlCommand.Parameters.Add(parameter1);
                //sqlCommand.Parameters.Add(new SqlParameter("@NgaySinh", textBox2.Text.ToString().Trim()));

                sqlCommand.Parameters.Add(new SqlParameter("@DiaChi", textBox3.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Sửa địa bàn thành công!");
                HienThiDuLieuGridView1();
            }
        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FormSinhVien()
        {
            InitializeComponent();
        }

        //Gridview lớp
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                comboBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                //textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            }
        }

        //Hiện thị sự kiện gridview sinh viên
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
                textBox5.Text = dataGridView2.Rows[index].Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView2.Rows[index].Cells[3].Value.ToString();
                textBox3.Text = dataGridView2.Rows[index].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView2.Rows[index].Cells[5].Value.ToString();
                //textBox4.Text = dataGridView2.Rows[index].Cells[5].Value.ToString();
            }
        }

        //Quyền nhà trường
        private void QuyenNhaTruong()
        {
            label7.Visible = comboBox2.Visible = button1.Visible = true;

            comboBox2.Items.Add("CS1"); comboBox2.Items.Add("CS2");
        }
        //Phân quyền cho mGroup
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

        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            NhomQuyen();
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            HienThiNut();
            LayDSLOP();

        }

        //Xem dữ liệu từng cơ sở:
        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.MACOSO == "CS1")
            {
                if (comboBox2.Text.ToString().Trim() == "CS1")
                {
                    String strlenh = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String str = "select MASV, HO, TEN, NGAYSINH, DIACHI, MALOP from SINHVIEN";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã sinh viên";
                    dataGridView2.Columns[1].HeaderText = "Họ";
                    dataGridView2.Columns[2].HeaderText = "Tên";
                    dataGridView2.Columns[3].HeaderText = "Ngày sinh";
                    dataGridView2.Columns[4].HeaderText = "Địa chỉ";
                    dataGridView2.Columns[5].HeaderText = "Mã lớp";
                    conn_publisher.Close();
                }
                if (comboBox2.Text.ToString().Trim() == "CS2")
                {
                    String strlenh = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String str = "select MASV, HO, TEN, NGAYSINH, DIACHI, MALOP from LINK1.TN_CSDLPT.DBO.SINHVIEN";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã sinh viên";
                    dataGridView2.Columns[1].HeaderText = "Họ";
                    dataGridView2.Columns[2].HeaderText = "Tên";
                    dataGridView2.Columns[3].HeaderText = "Ngày sinh";
                    dataGridView2.Columns[4].HeaderText = "Địa chỉ";
                    dataGridView2.Columns[5].HeaderText = "Mã lớp";
                    conn_publisher.Close();
                }
            }

            if (Program.MACOSO == "CS2")
            {
                if (comboBox2.Text.ToString().Trim() == "CS1")
                {
                    String strlenh = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String str = "select MASV, HO, TEN, NGAYSINH, DIACHI, MALOP from LINK1.TN_CSDLPT.DBO.SINHVIEN";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã sinh viên";
                    dataGridView2.Columns[1].HeaderText = "Họ";
                    dataGridView2.Columns[2].HeaderText = "Tên";
                    dataGridView2.Columns[3].HeaderText = "Ngày sinh";
                    dataGridView2.Columns[4].HeaderText = "Địa chỉ";
                    dataGridView2.Columns[5].HeaderText = "Mã lớp";
                    conn_publisher.Close();
                }
                if (comboBox2.Text.ToString().Trim() == "CS2")
                {
                    String strlenh = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String str = "select MASV, HO, TEN, NGAYSINH, DIACHI, MALOP from SINHVIEN";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView2.DataSource = dt;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView2.Columns[0].HeaderText = "Mã sinh viên";
                    dataGridView2.Columns[1].HeaderText = "Họ";
                    dataGridView2.Columns[2].HeaderText = "Tên";
                    dataGridView2.Columns[3].HeaderText = "Ngày sinh";
                    dataGridView2.Columns[4].HeaderText = "Địa chỉ";
                    dataGridView2.Columns[5].HeaderText = "Mã lớp";
                    conn_publisher.Close();
                }
            }
        }

        //Hiện thị danh sách lớp
        private void HienThiDuLieuGridView()
        {
            String strlenh = "select MALOP, TENLOP, MAKH from LOP";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "Mã lớp";
            dataGridView1.Columns[1].HeaderText = "Tên lớp";
            dataGridView1.Columns[2].HeaderText = "Mã khoa";
            conn_publisher.Close();
        }

        //Hiện thị danh sách sinh viên
        private void HienThiDuLieuGridView1()
        {
            String strlenh = "select MASV, HO, TEN, NGAYSINH, DIACHI, MALOP from SINHVIEN";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView2.DataSource = dt;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView2.Columns[0].HeaderText = "Mã sinh viên";
            dataGridView2.Columns[1].HeaderText = "Họ";
            dataGridView2.Columns[2].HeaderText = "Tên";
            dataGridView2.Columns[3].HeaderText = "Ngày sinh";
            dataGridView2.Columns[4].HeaderText = "Địa chỉ";
            dataGridView2.Columns[5].HeaderText = "Mã lớp";
            conn_publisher.Close();
        }

        //Lấy danh sách lớp
        private void LayDSLOP()
        {
            int index1 = dataGridView1.CurrentCell.RowIndex;
            DataTable dt1 = (DataTable)dataGridView1.DataSource;
            foreach (DataRow row in dt1.Rows)
            {
                String maKhoaGridView = row["MALOP"].ToString();
                comboBox1.Items.Add(maKhoaGridView);
            }
        }

    
     

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xÓaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //--------------text
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled  = textBox5.Enabled = comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";
        }

    }
}
