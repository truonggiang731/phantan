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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TN_CSDLPT
{
    public partial class FormDangKyThi : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Undo
        string comboBox2_temp ="";
        string textBox1_temp="";
        string textBox2_temp="";
        string comboBox3_temp="";
        string dateTimePicker1_temp="";
        string numericUpDown1_temp="";
        string numericUpDown2_temp="";
        string numericUpDown3_temp="";

        //Kiểm tra text có rỗng không
        private bool Check_NULL(System.Windows.Forms.TextBox tb, string str)
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
        private bool Check_NULL_CBX(System.Windows.Forms.ComboBox tb, string str)
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
        private bool Check_NULL_NUD(System.Windows.Forms.NumericUpDown tb, string str)
        {
            if (tb.Text.Trim().Equals(""))
            {
                MessageBox.Show(str, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Focus();
                return true;
            }
            return false;

        }

        public FormDangKyThi()
        {
            InitializeComponent();
        }

        //Quyền đối với nhà trường
        private void QuyenNhaTruong()
        {
            label15.Visible = comboBox4.Visible = button1.Visible = true;

            comboBox4.Items.Add("CS1"); comboBox4.Items.Add("CS2");

            button3.Visible = true;
            button2.Visible = false;
            //button4.Visible = true;
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
            if (Program.mGroup == "GIANGVIEN") menuStrip1.Enabled = true;
            
        }
        private void FormDangKyThi_Load(object sender, EventArgs e)
        {
            LayDSGV();
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            HienThiDuLieuGridView2();
            TrinhDO();
            HienThiNut();
            NhomQuyen();
            label16.Visible = label1.Visible = true;

        }

       /* //lấy danh sách khoa
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
        }*/

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

        //Hiện thị danh sách môn học
        private void HienThiDuLieuGridView1()
        {
            String strlenh = "select MAMH, TENMH from MONHOC";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView2.DataSource = dt;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView2.Columns[0].HeaderText = "Mã môn học";
            dataGridView2.Columns[1].HeaderText = "Tên môn học";
            conn_publisher.Close();
        }

        //Hiện thị danh sách giảng viên đăng ký
        private void HienThiDuLieuGridView2()
        {
            String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView3.DataSource = dt;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
            dataGridView3.Columns[1].HeaderText = "Mã môn học";
            dataGridView3.Columns[2].HeaderText = "Mã lớp";
            dataGridView3.Columns[3].HeaderText = "Trình độ";
            dataGridView3.Columns[4].HeaderText = "Ngày thi";
            dataGridView3.Columns[5].HeaderText = "Lần";
            dataGridView3.Columns[6].HeaderText = "Số câu";
            dataGridView3.Columns[7].HeaderText = "Thời gian";
            conn_publisher.Close();
        }

        //Sự kiện bảng đăng ký thi
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView3.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView3.DataSource;
            if (dt.Rows.Count > 0)
            {
                comboBox2.Text = dataGridView3.Rows[index].Cells[0].Value.ToString();
                textBox1.Text = dataGridView3.Rows[index].Cells[1].Value.ToString();
                textBox2.Text = dataGridView3.Rows[index].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView3.Rows[index].Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView3.Rows[index].Cells[4].Value.ToString();
                numericUpDown1.Text = dataGridView3.Rows[index].Cells[5].Value.ToString();
                numericUpDown2.Text = dataGridView3.Rows[index].Cells[6].Value.ToString();
                numericUpDown3.Text = dataGridView3.Rows[index].Cells[7].Value.ToString();
            }
        }

        //Sự kiện bảng danh sách lớp
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox2.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
               
            }
        }

        //Sự kiện bảng danh sách môn học
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();

            }
        }

        private void TrinhDO()
        {
            comboBox3.Items.Add("A");
            comboBox3.Items.Add("B");
            comboBox3.Items.Add("C");
        }

        //lấy danh sách giảng viên
        private void LayDSGV()
        {
            DataTable dt = new DataTable();
            String strlenh = "select MAGV from GIAOVIEN";
            dt = Program.ExecSqlDataTable(strlenh);

            comboBox2.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox2.DisplayMember = "MAGV";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();
        }

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //--------------text
            comboBox2.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
        }

        //Thêm
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = "add";
            lưuToolStripMenuItem.Enabled = true;
            thêmToolStripMenuItem.Enabled = true;
            refreshToolStripMenuItem.Enabled = true;
            xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = false;
            thoátToolStripMenuItem.Enabled = true;

            comboBox2.Text = Program.username;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox3.Text = "";
            dateTimePicker1.Text = "";
            numericUpDown1.Text = "";
            numericUpDown2.Text = "";
            numericUpDown3.Text = "";

            //------------text
            //--------------text
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
            dateTimePicker1.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
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

            //--------------text
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
            dateTimePicker1.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            dataGridView1.Enabled = false; dataGridView2.Enabled = false;
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

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox2.Text = comboBox2_temp;
            textBox1.Text = textBox1_temp;
            textBox2.Text = textBox2_temp;
            comboBox3.Text = comboBox3_temp;
            dateTimePicker1.Text = dateTimePicker1_temp;
            numericUpDown1.Text = numericUpDown1_temp;
            numericUpDown2.Text = numericUpDown2_temp;
            numericUpDown3.Text = numericUpDown3_temp;
        }

        //Refresh
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            dataGridView3.Refresh();
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
            HienThiDuLieuGridView2();
            HienThiNut();
        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Lưu
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Check_NULL(textBox2, "Mã lớp không được để trống") == true) return;
            if (Check_NULL(textBox1, "Mã môn học không được để trống") == true) return;
            if (Check_NULL_CBX(comboBox3, "Trình độ không được để trống") == true) return;
            if (Check_NULL_NUD(numericUpDown1, "Lần thi không được để trống") == true) return;
            if (Check_NULL_NUD(numericUpDown2, "Số câu thi không được để trống") == true) return;
            if (Check_NULL_NUD(numericUpDown3, "Thời gian thi không được để trống") == true) return;
            if (Check_NULL_CBX(comboBox2, "Mã giảng viên không được để trống") == true) return;
           
            //Kiểm tra ngày đăng ký phải lớn hơn ngày hiện tại
            
            // Lấy ngày tháng hiện tại
            DateTime currentDate = DateTime.Now;

            // Lấy giá trị ngày tháng từ dateTimePicker
            DateTime userDate = dateTimePicker1.Value;

            // So sánh ngày tháng
            int result = DateTime.Compare(userDate, currentDate);

            if (result > 0)
            {
                // Ngày tháng người dùng nhập vào lớn hơn ngày hiện tại
                // Thực hiện các hành động tương ứng
            }
            else if (result == 0)
            {
                // Ngày tháng người dùng nhập vào bằng ngày hiện tại
                // Thực hiện các hành động tương ứng
            }
            else { MessageBox.Show("Ngày đăng ký thi phải sau ngày bạn đăng ký hoặc bằng ngày bạn đăng ký"); return; }

            //Kiểm tra số câu có đủ không: numericUpDown2.Text và trình độ: comboBox3.Text


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

            String strlenh = "SP_KiemTraMonDaDangKyThi '" + textBox1.Text.ToString().Trim() + "', '" + textBox2.Text.ToString().Trim() + "', '" + comboBox3.Text.ToString().Trim() + "' , '" + day + "', '" + numericUpDown1.Text.ToString().Trim() + "'";
            dt = Program.ExecSqlDataTable(strlenh);
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show("Trùng môn học đã đăng ký trước đó");
                return;
            }

            //Kiểm tra số câu thi trong bộ đề có đủ điều kiện không 
            String KiemTraDeThi = "SP_KiemTraSoCauThi '"+ comboBox3.Text.ToString().Trim()+"', '"+ numericUpDown2.Text.ToString().Trim()+"', '"+ textBox1.Text.ToString().Trim()+"'";
            dt = Program.ExecSqlDataTable(KiemTraDeThi);

            if (dt.Columns.Count == 1)
            {
                MessageBox.Show("Số câu thi không đủ để đáp ứng đề thi, vui lòng thêm số câu thi");
                return;
            }


            //Xử lý
            //Thêm
            if (flag == "add")
            {

                Add_KyThi(day);
                HienThiDuLieuGridView2();

            }

            //Xóa
            else if (flag == "delete")
            {
                /*String strLenh = "sp_DeleteDiaBan";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MADIABAN", textBox1.Text.ToString().Trim()));
                //sqlCommand.Parameters.Add(new SqlParameter("@TENKHOA", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Xóa địa bàn thành công!");
                HienThiDuLieu();*/
            }

            //sửa
            else if (flag == "edit")
            {
               /* String strLenh = "sp_UpdateDiaBan";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@MADIABAN", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TENDIABAN", textBox2.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Sửa địa bàn thành công!");
                HienThiDuLieu();*/
            }
        }

        private void Add_KyThi(string DAY)
        {
            String strLenh = "sp_AddDangKyThi";
            SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 600;

            sqlCommand.Parameters.Add(new SqlParameter("@MaGv", comboBox2.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@MaMh", textBox1.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@MaLop", textBox2.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@TrinhDo", comboBox3.Text.ToString().Trim()));
            //-----Ngày thi
           /* sqlCommand.Parameters.Add(new SqlParameter("@NgayThi", SqlDbType.Char);*/
            SqlParameter parameter1 = new SqlParameter("@NgayThi", SqlDbType.Char);
            parameter1.Value = DAY;
            sqlCommand.Parameters.Add(parameter1);
            //-----Ngày thi
            sqlCommand.Parameters.Add(new SqlParameter("@Lan", numericUpDown1.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@SoCau", numericUpDown2.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@ThoiGian", numericUpDown3.Text.ToString().Trim()));
            
            Program.ExecSQLCommand(sqlCommand, conn_publisher);
            MessageBox.Show("Đăng ký thi thành công!");
        }

        //Xem dữ liệu theo từng cơ sở
        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.MACOSO == "CS1")
            {
                if (comboBox4.Text.ToString().Trim() == "CS1")
                {
                   
                    String str = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView3.DataSource = dt;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
                    dataGridView3.Columns[1].HeaderText = "Mã môn học";
                    dataGridView3.Columns[2].HeaderText = "Mã lớp";
                    dataGridView3.Columns[3].HeaderText = "Trình độ";
                    dataGridView3.Columns[4].HeaderText = "Ngày thi";
                    dataGridView3.Columns[5].HeaderText = "Lần";
                    dataGridView3.Columns[6].HeaderText = "Số câu";
                    dataGridView3.Columns[7].HeaderText = "Thời gian";
                    conn_publisher.Close();
                }
                if (comboBox4.Text.ToString().Trim() == "CS2")
                {
                   
                    String str = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from LINK1.TN_CSDLPT.DBO.GIAOVIEN_DANGKY";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView3.DataSource = dt;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
                    dataGridView3.Columns[1].HeaderText = "Mã môn học";
                    dataGridView3.Columns[2].HeaderText = "Mã lớp";
                    dataGridView3.Columns[3].HeaderText = "Trình độ";
                    dataGridView3.Columns[4].HeaderText = "Ngày thi";
                    dataGridView3.Columns[5].HeaderText = "Lần";
                    dataGridView3.Columns[6].HeaderText = "Số câu";
                    dataGridView3.Columns[7].HeaderText = "Thời gian";
                    conn_publisher.Close();
                }
            }

            if (Program.MACOSO == "CS2")
            {
                if (comboBox4.Text.ToString().Trim() == "CS1")
                {
                    
                    String str = "select MALOP, TENLOP, MAKH from LINK1.TN_CSDLPT.DBO.LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from LINK1.TN_CSDLPT.DBO.GIAOVIEN_DANGKY";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView3.DataSource = dt;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
                    dataGridView3.Columns[1].HeaderText = "Mã môn học";
                    dataGridView3.Columns[2].HeaderText = "Mã lớp";
                    dataGridView3.Columns[3].HeaderText = "Trình độ";
                    dataGridView3.Columns[4].HeaderText = "Ngày thi";
                    dataGridView3.Columns[5].HeaderText = "Lần";
                    dataGridView3.Columns[6].HeaderText = "Số câu";
                    dataGridView3.Columns[7].HeaderText = "Thời gian";
                    conn_publisher.Close();
                }
                if (comboBox4.Text.ToString().Trim() == "CS2")
                {
                    
                    String str = "select MALOP, TENLOP, MAKH from LOP";
                    dt = Program.ExecSqlDataTable(str);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView1.Columns[0].HeaderText = "Mã lớp";
                    dataGridView1.Columns[1].HeaderText = "Tên lớp";
                    dataGridView1.Columns[2].HeaderText = "Mã khoa";
                    conn_publisher.Close();

                    String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY";
                    dt = Program.ExecSqlDataTable(strlenh);
                    dataGridView3.DataSource = dt;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                    dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
                    dataGridView3.Columns[1].HeaderText = "Mã môn học";
                    dataGridView3.Columns[2].HeaderText = "Mã lớp";
                    dataGridView3.Columns[3].HeaderText = "Trình độ";
                    dataGridView3.Columns[4].HeaderText = "Ngày thi";
                    dataGridView3.Columns[5].HeaderText = "Lần";
                    dataGridView3.Columns[6].HeaderText = "Số câu";
                    dataGridView3.Columns[7].HeaderText = "Thời gian";
                    conn_publisher.Close();
                }
            }
        }

        //Lấy cả 2 cơ sở:
        private void button3_Click(object sender, EventArgs e)
        {

            DateTime dau = dateTimePicker2.Value;
            DateTime sau = dateTimePicker3.Value;

            string NgayDau = dau.ToString("yyyy-MM-dd");
            string NgaySau = sau.ToString("yyyy-MM-dd"); 

            String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from LINK0.TN_CSDLPT.DBO.GIAOVIEN_DANGKY where NGAYTHI >= '"+ NgayDau + "' and NGAYTHI <= '"+ NgaySau + "'";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView3.DataSource = dt;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView3.Columns[0].HeaderText = "Mã giáo viên";
            dataGridView3.Columns[1].HeaderText = "Mã môn học";
            dataGridView3.Columns[2].HeaderText = "Mã lớp";
            dataGridView3.Columns[3].HeaderText = "Trình độ";
            dataGridView3.Columns[4].HeaderText = "Ngày thi";
            dataGridView3.Columns[5].HeaderText = "Lần";
            dataGridView3.Columns[6].HeaderText = "Số câu";
            dataGridView3.Columns[7].HeaderText = "Thời gian";
            conn_publisher.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dau = dateTimePicker2.Value;
            DateTime sau = dateTimePicker3.Value;

            string NgayDau = dau.ToString("yyyy-MM-dd");
            string NgaySau = sau.ToString("yyyy-MM-dd");

            DsMonThiCs1 f = new DsMonThiCs1(NgayDau, NgaySau);
            //f.MdiParent = this;
            f.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
