using DevExpress.Utils.VisualEffects;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TN_CSDLPT
{
    public partial class FormThi : Form
    {
        String malop = "";
        int lan;
        String mamh = "";
        String td = "";
        int socau; int vitri = 1;
        int thoigian;
        float diem;
        Boolean sv = true; Boolean status = false;
        List<String> chon = new List<String>();

        List<String> da = new List<String>();


        //0000000000000000000000000000000000000000000000000000--ĐẦU-----00000000000000000000000000000000000000000000000000
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();
        public FormThi()
        {
            
            InitializeComponent();
        }

        private void FormThi_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            HienThiDuLieu();
            HienThiDuLieuGridView();
            HienThiNut();
        }

        private void HienThiDuLieu()
        {
            textBox3.Text = Program.mHoten;

            //lấy lớp theo mã sinh viên Pogram.username
            DataTable dt = new DataTable();
            String strlenh = "select MALOP from SINHVIEN where MASV='"+Program.username+"'";
            dt = Program.ExecSqlDataTable(strlenh);
            comboBox4.DataSource = dt;
            //comboBox2.ValueMember = "TENMH";
            comboBox4.DisplayMember = "MALOP";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();

            //Lấy tên lớp theo mã lớp
            DataTable dt1 = new DataTable();
            String strlenh1 = "select TENLOP from LOP where MALOP='" + comboBox4.Text + "'";
            dt1 = Program.ExecSqlDataTable(strlenh1);
            comboBox5.DataSource = dt1;
            //comboBox2.ValueMember = "TENMH";
            comboBox5.DisplayMember = "TENLOP";
            //comboBox1.SelectedIndex = 0;
            conn_publisher.Close();

        }

        //Hiện thị danh sách lớp
        private void HienThiDuLieuGridView()
        {
            if (Program.mGroup == "SINHVIEN")
            {
                String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY where MALOP = '" + comboBox4.Text.Trim() + "'";
                dt = Program.ExecSqlDataTable(strlenh);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                dataGridView1.Columns[0].HeaderText = "Mã giảng viên";
                dataGridView1.Columns[1].HeaderText = "Mã môn học";
                dataGridView1.Columns[2].HeaderText = "Mã lớp";
                dataGridView1.Columns[3].HeaderText = "Trình độ";
                dataGridView1.Columns[4].HeaderText = "Ngày thi";
                dataGridView1.Columns[5].HeaderText = "Lần";
                dataGridView1.Columns[6].HeaderText = "Số câu";
                dataGridView1.Columns[7].HeaderText = "Thời gian";
                conn_publisher.Close();
            }

            else if (Program.mGroup == "GIANGVIEN")
            {
                String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY where MAGV = '" +Program.username+"'";
                dt = Program.ExecSqlDataTable(strlenh);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                dataGridView1.Columns[0].HeaderText = "Mã giảng viên";
                dataGridView1.Columns[1].HeaderText = "Mã môn học";
                dataGridView1.Columns[2].HeaderText = "Mã lớp";
                dataGridView1.Columns[3].HeaderText = "Trình độ";
                dataGridView1.Columns[4].HeaderText = "Ngày thi";
                dataGridView1.Columns[5].HeaderText = "Lần";
                dataGridView1.Columns[6].HeaderText = "Số câu";
                dataGridView1.Columns[7].HeaderText = "Thời gian";
                conn_publisher.Close();
            }

            else if (Program.mGroup == "COSO")
            {
                String strlenh = "select MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN from GIAOVIEN_DANGKY";
                dt = Program.ExecSqlDataTable(strlenh);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
                dataGridView1.Columns[0].HeaderText = "Mã giảng viên";
                dataGridView1.Columns[1].HeaderText = "Mã môn học";
                dataGridView1.Columns[2].HeaderText = "Mã lớp";
                dataGridView1.Columns[3].HeaderText = "Trình độ";
                dataGridView1.Columns[4].HeaderText = "Ngày thi";
                dataGridView1.Columns[5].HeaderText = "Lần";
                dataGridView1.Columns[6].HeaderText = "Số câu";
                dataGridView1.Columns[7].HeaderText = "Thời gian";
                conn_publisher.Close();
            }

        }

        //Hiện thị nút ban đầu
        public void HienThiNut()
        {
                       //--------------text
            comboBox1.Enabled = comboBox2.Enabled = comboBox3.Enabled = comboBox4.Enabled = comboBox5.Enabled = textBox3.Enabled =false;
        }

        //Tạo sự kiện gridview
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                comboBox1.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();

                td= dataGridView1.Rows[index].Cells[3].Value.ToString();
                socau = int.Parse(dataGridView1.Rows[index].Cells[6].Value.ToString());
                thoigian = int.Parse(dataGridView1.Rows[index].Cells[7].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Chưa chọn môn thi!");
                return;

            }
           
            if (KiemTraMonTrung() == true)
            {
                MessageBox.Show("Môn học này đã thi");
            }
            else if (KiemTraMonTrung() == false)
            {
                panel3.Visible = panel4.Visible = true;
                dataGridView1.Enabled = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
                textBox1.Text = comboBox1.Text.Trim();

                //---load câu thi
                string sql = "exec sp_laycauhoi '" + comboBox4.Text.Trim() + "','" + comboBox1.Text.Trim() + "', '" + td + "','" + socau + "' ";
                DataTable dt = Program.ExecSqlDataTable(sql);
                bdsThi.DataSource = dt;
                for (int i = 0; i < socau; i++)
                {
                    chon.Add(" ");
                    da.Add(((DataRowView)bdsThi[i])["dap_an"].ToString());
                }
                timer1.Start();
                Hienthibandau();
                loadcauthi();

                Hienthidanhsachcauhoi();
            }


        }
        //0000000000000000000000000000000000000000000000000000--CUỐI-----00000000000000000000000000000000000000000000000000


        private Boolean loadInfoThi()
        {
            

            // lấy trình độ, số câu, thời gian
            return true;
        }

        private void loadInfo()
        {
           
            //Lấy mã lớp, tên lớp của sinh viên


        }

        private void loadcauthi()
        {
            labelCau.Text = "Câu " + (vitri).ToString();
            labelChon.Text = chon[vitri - 1];
            //labelda.Text = da[vitri - 1]; hiện thị đáp án
            labelND.Text = ((DataRowView)bdsThi[vitri - 1])["noidung"].ToString();
            radioButtonA.Text = ((DataRowView)bdsThi[vitri - 1])["a"].ToString();
            radioButtonB.Text = ((DataRowView)bdsThi[vitri - 1])["b"].ToString();
            radioButtonC.Text = ((DataRowView)bdsThi[vitri - 1])["c"].ToString();
            radioButtonD.Text = ((DataRowView)bdsThi[vitri - 1])["d"].ToString();
        }


        private void Hienthidanhsachcauhoi()
        {
            for (int i = 1; i <= socau; i++)
            {
                listBox1.Items.Add("CÂU "+i);
             
            }
    
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //Xét thời gian
        private int s = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            if (s == 0)
            {
                if (thoigian != 0)
                {
                    thoigian--;
                    s = 59;
                }
            }
            labelTimer.Text = thoigian.ToString() + " : " + s.ToString();
            if (thoigian == 0 && s == 0)
            {
                timer1.Stop(); status = false;
                MessageBox.Show("Đã hết thời gian!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tinhdiem();
                panel4.Visible = false;
                label9.Text = diem.ToString();
                labelTimer.Text = "HẾT GIỜ";
                button2.Text = "ĐÃ NỘP";
                
                if(Program.mGroup=="SINHVIEN") AddBangDiem();

                button2.BackColor = Color.Lime;
                button2.Enabled = false;
                //  if (Program.mGroup.Equals("SINHVIEN"))
                // {
                //  insertdiemsv();
                // }
                // button2.Enabled = false;
                //  button3.Enabled = true;

            }
        }

        private void bdsThi_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void xoacheck()
        {
            radioButtonA.Checked = false;
            radioButtonB.Checked = false;
            radioButtonC.Checked = false;
            radioButtonD.Checked = false;
        }
        private void check()
        {
            labelChon.Text = chon[vitri - 1];
            if (chon[vitri - 1].Equals("A")) radioButtonA.Checked = true;
            if (chon[vitri - 1].Equals("B")) radioButtonB.Checked = true;
            if (chon[vitri - 1].Equals("C")) radioButtonC.Checked = true;
            if (chon[vitri - 1].Equals("D")) radioButtonD.Checked = true;
            if (chon[vitri - 1].Equals(" ")) xoacheck();

        }
        //Nhấn câu hỏi tiếp theo
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            xoacheck();
            vitri++; check();
            loadcauthi();
            
        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {

        }

        //Quay lại câu trước
        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            xoacheck();
            vitri--; check();
            loadcauthi();
           
        }

        private void bindingNavigatorCountItem_Click(object sender, EventArgs e)
        {

        }

        //Đầu trang
        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            xoacheck();
            vitri = 1; check();
            loadcauthi();
        }

        //Cuối trang
        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            xoacheck();
            vitri = socau; check();
            loadcauthi();
        }

        //Hiện thị nút ban đầu
        private void Hienthibandau()
        {
            if (vitri == socau) bindingNavigatorMoveNextItem.Enabled = false;
            if (vitri >= 1 || vitri < socau) bindingNavigatorMoveNextItem.Enabled = true;

            if (vitri == 1) bindingNavigatorMovePreviousItem.Enabled = false;
            if (vitri > 1 || vitri <= socau) bindingNavigatorMovePreviousItem.Enabled = true;

            bindingNavigatorMoveFirstItem.Enabled = bindingNavigatorMoveLastItem.Enabled = true;
        }

        //Tính điểm
        private void tinhdiem()
        {

            int caudung = 0;
            for (int i = 0; i < socau; i++)
            {
                if (chon[i].Equals(da[i])) caudung++;
            }
            if (caudung == 0) diem = 0;
            else diem = (float)Math.Round((double)(10 * caudung) / socau, 2);
            MessageBox.Show("Số câu đúng: " + caudung + "/" + socau + "\nĐiểm: " + diem, "Kết Quả", MessageBoxButtons.OK);

        }

        //A
        private void radioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            chon[vitri - 1] = "A"; labelChon.Text = "A";
        }

        //B
        private void radioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            chon[vitri - 1] = "B"; labelChon.Text = "B";
        }

        //C
        private void radioButtonC_CheckedChanged(object sender, EventArgs e)
        {
            chon[vitri - 1] = "C"; labelChon.Text = "C";
        }

        //D
        private void radioButtonD_CheckedChanged(object sender, EventArgs e)
        {
            chon[vitri - 1] = "D"; labelChon.Text = "D";
        }

        //Nộp bài
        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.mGroup == "SINHVIEN")
            {
                tinhdiem();
                panel4.Visible = false;
                label9.Text = diem.ToString();
                labelTimer.Text = "HẾT GIỜ";
                button2.Text = "ĐÃ NỘP";
                button2.Enabled = false;
                button2.BackColor = Color.Lime;
                timer1.Stop();
                AddBangDiem();
            }

            else if(Program.mGroup == "GIANGVIEN" || Program.mGroup == "COSO")
            {
                tinhdiem();
                panel4.Visible = false;
                label9.Text = diem.ToString();
                labelTimer.Text = "HẾT GIỜ";
                button2.Text = "ĐÃ NỘP";
                button2.Enabled = false;
                button2.BackColor = Color.Lime;
                timer1.Stop();
            }
            
        }

        //Add bảng điểm sinh viên.
        private void AddBangDiem()
        {
            String strLenh = "sp_AddBangDiem";
            SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 600;

            

            SqlParameter parameter = new SqlParameter("@MaSv", SqlDbType.Char);
            parameter.Value = Program.username;
            sqlCommand.Parameters.Add(parameter);

            sqlCommand.Parameters.Add(new SqlParameter("@MaMh", comboBox1.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@Lan", comboBox2.Text.ToString().Trim()));
            sqlCommand.Parameters.Add(new SqlParameter("@NgayThi", comboBox3.Text.ToString().Trim()));
           
            SqlParameter parameter1 = new SqlParameter("@Diem", SqlDbType.Float);
            parameter1.Value = diem;
            sqlCommand.Parameters.Add(parameter1);

            Program.ExecSQLCommand(sqlCommand, conn_publisher);

            AddChiTietBangDiem();
            //MessageBox.Show("Thêm điểm thành công!");
        }

        private void AddChiTietBangDiem()
        {
            String sql = "select ID_Diem from BANGDIEM where MASV = '"+Program.username+"' and MAMH = '"+ comboBox1.Text.ToString().Trim()+"' and LAN = '"+comboBox2.Text.ToString().Trim()+"' and NGAYTHI = '"+ comboBox3.Text.ToString().Trim()+"'";
            Program.myReader = Program.ExecSqlDataReader(sql);
            if (Program.myReader == null) return;
            Program.myReader.Read();
            int madiem = Program.myReader.GetInt32(0);
            Program.myReader.Close();

            //List<String> chon = new List<String>();
            //List<String> da = new List<String>();
            for (int i = 0; i < socau; i++)
            {
                String str = "sp_AddCt_Diem";
                SqlCommand sqlCommand = new SqlCommand(str, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;
                //id điểm, mã câu hỏi, đáp án mình chọn

                SqlParameter parameter1 = new SqlParameter("@id_diem", SqlDbType.Int);
                parameter1.Value = madiem;
                sqlCommand.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter("@cauhoi", SqlDbType.Int);
                parameter2.Value = Int16.Parse(((DataRowView)bdsThi[i])["cauhoi"].ToString());
                sqlCommand.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter("@dapan", SqlDbType.Char);
                parameter3.Value = chon[i];
                sqlCommand.Parameters.Add(parameter3);

                Program.ExecSQLCommand(sqlCommand, conn_publisher);

            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = listBox1.SelectedIndex;
            xoacheck();
            vitri = index+1;
            check();
            loadcauthi();
        }



        //Kiểm tra môn này sinh viên đã thi chưa.
        bool KiemTraMonTrung()
        {
            int spaceIndex = comboBox3.Text.Trim().IndexOf(' ');
            string result = "";
            if (spaceIndex != -1)
            {
                result = comboBox3.Text.Trim().Substring(0, spaceIndex);
                Console.WriteLine(result);
            }

            //string Ngay = "11/12/2023";

            int firstSlashIndex = result.IndexOf('/');
            int secondSlashIndex = result.IndexOf('/', firstSlashIndex + 1);

            string thang = result.Substring(0, firstSlashIndex);
            string ngay = result.Substring(firstSlashIndex + 1, secondSlashIndex - firstSlashIndex - 1);
            string nam = result.Substring(secondSlashIndex + 1);


            // kiểm tra Mã môn học, lần, ngày thi.
            String strlenh = "exec SP_BangDiemSV '"+Program.username+"', '"+ comboBox1.Text.Trim()+"', '"+nam+"-"+thang+"-"+ngay+ "'";
            dt = Program.ExecSqlDataTable(strlenh);
            if(dt.Rows.Count ==0 ) 
            { 
                return false; 
            }
            else if(dt.Rows.Count>0) return true;
            return true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelTimer_Click(object sender, EventArgs e)
        {

        }
    }
}
