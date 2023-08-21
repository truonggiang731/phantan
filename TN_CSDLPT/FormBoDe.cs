using DevExpress.Utils.Extensions;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TN_CSDLPT
{
    public partial class FormBoDe : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        //Đánh dấu đang là lưu khóa sửa hay thêm
        string flag = "";

        //Undo
         string textBox3_temp ="";
        string comboBox3_temp = "";
         string comboBox4_temp = "";
         string textBox5_temp = "";
        string textBox6_temp = "";
        string textBox7_temp = "";
        string textBox8_temp = "";
        string comboBox1_temp = "";
        string textBox4_temp = "";
        string comboBox2_temp = "";
        private void HienThiDuLieuGridView()
        {
            String strlenh = "select CAUHOI,MAMH,TRINHDO,NOIDUNG,A,B,C,D,DAP_AN,MAGV from BODE";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "Câu hỏi";
            dataGridView1.Columns[1].HeaderText = "MAMH";
            dataGridView1.Columns[2].HeaderText = "Trình độ";
            dataGridView1.Columns[3].HeaderText = "Nội dung";
            dataGridView1.Columns[4].HeaderText = "A";
            dataGridView1.Columns[5].HeaderText = "B";
            dataGridView1.Columns[6].HeaderText = "C";
            dataGridView1.Columns[7].HeaderText = "D";
            dataGridView1.Columns[8].HeaderText = "Đáp án";
            dataGridView1.Columns[9].HeaderText = "MAGV";
         
            conn_publisher.Close();
        }

        //LẤY DANH SACH MON HOC
        private void LayDSMONHOC()
        {
            DataTable dt = new DataTable();
            String strlenh = "select MAMH, TENMH from MONHOC";
            dt = Program.ExecSqlDataTable(strlenh);

            comboBox2.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox2.DisplayMember = "MAMH";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();
        }

        //LẤY DANH SACH GIANGVIEN
        private void LayDSGIANGVIEN()
        {
            DataTable dt = new DataTable();
            String strlenh = "select MAGV from GIAOVIEN";
            dt = Program.ExecSqlDataTable(strlenh);

            comboBox1.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox1.DisplayMember = "MAGV";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();
        }

        private void TRINHDO()
        {
            comboBox3.Items.Add("A");
            comboBox3.Items.Add("B");
            comboBox3.Items.Add("C");
           
        }

        private void DAPAN()
        {
           
            comboBox4.Items.Add("A");
            comboBox4.Items.Add("B");
            comboBox4.Items.Add("C");
            comboBox4.Items.Add("D");

        }

        public FormBoDe()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //Phân quyền cho mGroup
        private void NhomQuyen()
        {
            if (Program.mGroup == "TRUONG") menuStrip1.Enabled = false;
            if (Program.mGroup == "COSO") menuStrip1.Enabled = true;
            if (Program.mGroup == "GIANGVIEN") menuStrip1.Enabled = true;
        }
        private void FormBoDe_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            HienThiDuLieuGridView();
            LayDSMONHOC();
            LayDSGIANGVIEN();
            TRINHDO();
            DAPAN();
            HienThiNut();
            NhomQuyen();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Hiện thị nút
        public void HienThiNut()
        {
            lưuToolStripMenuItem.Enabled = false;
            thêmToolStripMenuItem.Enabled = xóaToolStripMenuItem.Enabled = sửaToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = thoátToolStripMenuItem.Enabled = true;

            //------------text
            comboBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = comboBox3.Enabled = comboBox4.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = false;
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
            textBox3.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox5.Text = ""; 
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = Program.username;
            textBox4.Text = "";
           

            //----------------text
            comboBox2.Enabled =  textBox3.Enabled = comboBox3.Enabled = comboBox4.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = true;
            comboBox1.Enabled = false;
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


            //------text
            comboBox3.Enabled = comboBox4.Enabled = textBox4.Enabled = textBox5.Enabled = textBox6.Enabled = textBox7.Enabled = textBox8.Enabled = true;
            comboBox2.Enabled = comboBox1.Enabled = textBox3.Enabled = false;

        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox3_temp;
           
            comboBox3.Text = comboBox3_temp;
            comboBox4.Text = comboBox4_temp;
            textBox5.Text = textBox5_temp;
            textBox6.Text = textBox6_temp;
            textBox7.Text = textBox7_temp;
            
            textBox4.Text = textBox4_temp;
            textBox8.Text = textBox8_temp;
            comboBox2.Text = comboBox2_temp;
            comboBox1.Text = comboBox1_temp;
        }

        //Refresh
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            HienThiDuLieuGridView();
            HienThiNut();

        }

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

        //Kiểm tra mã có trùng không
        private bool Check_Trung(String CAUHOI)
        {
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["CAUHOI"].ToString();
                if (maDBGridView.Trim() == CAUHOI.Trim())
                {
                    return true;
                }
            }
            return false;
        }
        //Lưu
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox3_temp = textBox3.Text.ToString().Trim();
            comboBox1_temp = comboBox1.Text.ToString().Trim();
            comboBox3_temp = comboBox3.Text.ToString().Trim();
            comboBox4_temp = comboBox4.Text.ToString().Trim();
            textBox5_temp = textBox5.Text.ToString().Trim();
            textBox6_temp = textBox6.Text.ToString().Trim();
            textBox7_temp = textBox7.Text.ToString().Trim();

            textBox4_temp = textBox4.Text.ToString().Trim();
            textBox8_temp = textBox8.Text.ToString().Trim();
            comboBox2_temp = comboBox2.Text.ToString().Trim();
            //Kiểm tra dữ liệu nhập vào:
            if (Check_NULL_CBX(comboBox2, "Mã môn học không được để trống!")) return;
            if (Check_NULL_CBX(comboBox1, "Mã giảng viên không được để trống!")) return;
            if (Check_NULL(textBox3, "Mã câu hỏi không được để trống!")) return;
            if (Check_NULL_CBX(comboBox3, "Trình độ không được để trống!")) return;
            if (Check_NULL_CBX(comboBox4, "Đáp án không được để trống!")) return;

            if (Check_NULL(textBox4, "Nội dung không được để trống!")) return;
            if (Check_NULL(textBox5, "Câu trả lời A không được để trống!")) return;
            if (Check_NULL(textBox6, "Câu trả lời B không được để trống!")) return;
            if (Check_NULL(textBox7, "Câu trả lời C không được để trống!")) return;
            if (Check_NULL(textBox8, "Câu trả lời D không được để trống!")) return;



            /*if (textBox1.Text.Trim().Length > 5 || 2 > textBox1.Text.Trim().Length)
            {
                MessageBox.Show("Mã môn học phải từ 2 đến 5 ký tự", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox1.Text.Contains(" "))
            {
                MessageBox.Show("Mã môn học không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            else if (Check_Trung(textBox3.Text.Trim()) && flag == "add")
            {
                MessageBox.Show("Mã câu hỏi đã tồn tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Xử lý
            //Thêm
            if (flag == "add")
            {
                String strLenh = "sp_AddBoDe";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@CauHoi", textBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaMh", comboBox2.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@TrinhDo", comboBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@NoiDung", textBox4.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@a", textBox5.Text.ToString().Trim()));

                sqlCommand.Parameters.Add(new SqlParameter("@b", textBox6.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@c", textBox7.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@d", textBox8.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@DapAn", comboBox4.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@MaGv", comboBox1.Text.ToString().Trim()));

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Thêm câu hỏi thành công!");
                HienThiDuLieuGridView();

            }

            //Xóa
            else if (flag == "delete")
            {
                if (comboBox1.Text.ToString().Trim() == Program.username)
                {
                    String strLenh = "sp_DeleteBoDe";
                    SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 600;

                    sqlCommand.Parameters.Add(new SqlParameter("@CauHoi", textBox3.Text.ToString().Trim()));

                    Program.ExecSQLCommand(sqlCommand, conn_publisher);
                    MessageBox.Show("Xóa câu hỏi thành công!");
                    HienThiDuLieuGridView();
                }
                else 
                {
                    MessageBox.Show("Giảng viên không có quyền xóa câu hỏi của giảng viên khác!"); return;
                }
            }

            //sửa
            else if (flag == "edit")
            {
                if (comboBox1.Text.ToString().Trim()==Program.username)
                {
                    String strLenh = "sp_EditBoDe";
                    SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 600;

                    sqlCommand.Parameters.Add(new SqlParameter("@CauHoi", textBox3.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@MaMh", comboBox2.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@TrinhDo", comboBox3.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@NoiDung", textBox4.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@a", textBox5.Text.ToString().Trim()));

                    sqlCommand.Parameters.Add(new SqlParameter("@b", textBox6.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@c", textBox7.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@d", textBox8.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@DapAn", comboBox4.Text.ToString().Trim()));
                    sqlCommand.Parameters.Add(new SqlParameter("@MaGv", comboBox1.Text.ToString().Trim()));

                    Program.ExecSQLCommand(sqlCommand, conn_publisher);
                    MessageBox.Show("Sửa câu hỏi thành công!");
                    HienThiDuLieuGridView();
                }
                else
                {
                    MessageBox.Show("Giảng viên không có quyền sửa câu hỏi của giảng viên khác!"); return;
                }
                
            }

        }

        //Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Tạo sự kiện Gridview
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox3.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                comboBox4.Text = dataGridView1.Rows[index].Cells[8].Value.ToString();
                textBox5.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.Rows[index].Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.Rows[index].Cells[7].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[index].Cells[9].Value.ToString();
                textBox4.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            }
        }

        //Kiểm tra giảng viên thuộc câu hỏi không
        private bool KiemTraGiangVienThuocBoDe()
        {
            DataTable kt = new DataTable();
            String strlenh = "select MAGV from BODE";
            kt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in kt.Rows)
            {
                String maDBGridView = row["MAGV"].ToString();
                if (maDBGridView.Trim() == Program.username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
