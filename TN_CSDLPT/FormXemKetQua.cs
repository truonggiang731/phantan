using DevExpress.XtraReports.UI;
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
    public partial class FormXemKetQua : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();
        public FormXemKetQua()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            textBox1.Text = Program.username;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //Hiện thị danh sách lớp
        private void HienThiDuLieuGridView()
        {
            String strlenh = "select MASV, MAMH, LAN, NGAYTHI, DIEM from DBO.BANGDIEM where MASV = '"+Program.username+"'";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "Mã sinh viên";
            dataGridView1.Columns[1].HeaderText = "Mã môn học";
            dataGridView1.Columns[2].HeaderText = "Lần";
            dataGridView1.Columns[3].HeaderText = "Ngày thi";
            dataGridView1.Columns[4].HeaderText = "Điểm";
           
            conn_publisher.Close();
        }
        private void FormXemKetQua_Load(object sender, EventArgs e)
        {
            HienThiDuLieuGridView();
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false;
            textBox1.Text = Program.username;
        }

        //Tạo sự kiện lên các ô
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox2.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        //Xem kết quả
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Bạn chưa có chọn môn học!"); return;
            }
            string MON_HOC = textBox2.Text.ToString().Trim();
            string NGAY_THI = textBox3.Text.ToString().Trim();
            string LAN_THI = textBox4.Text.ToString().Trim();
            //XuatKetQuaTHI f = new XuatKetQuaTHI(MON_HOC, NGAY_THI, LAN_THI);
            //f.MdiParent = this;
            //f.Show();
            XrptXemKetQua rpt = new XrptXemKetQua(Program.username, MON_HOC, LAN_THI);
            rpt.xrLanthi.Text = LAN_THI;
            rpt.xrHoten.Text = Program.mHoten;
            rpt.xrMonthi.Text = MON_HOC;
            rpt.xrNgaythi.Text = NGAY_THI;
            rpt.xrLop.Text = Program.Lop;

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
