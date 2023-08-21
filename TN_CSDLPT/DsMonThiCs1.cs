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
    public partial class DsMonThiCs1 : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();
        string NgayDau, NgaySau;
        public DsMonThiCs1(string Ngay_dau, string Ngay_Sau)
        {
            InitializeComponent();
            this.NgayDau = Ngay_dau;
            this.NgaySau = Ngay_Sau;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DsMonThiCs1_Load(object sender, EventArgs e)
        {
            if (Program.MACOSO == "CS1") label1.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM CƠ SỞ 1";
            if (Program.MACOSO == "CS2") label1.Text = "DANH SÁCH ĐĂNG KÝ THI TRẮC NGHIỆM CƠ SỞ 2";

            label3.Text = NgayDau; label5.Text = NgaySau;

            HienThiGrifView();



        }
        private void HienThiGrifView()
        {
            String strlenh = "select ROW_NUMBER() OVER(ORDER BY NGAYTHI) AS STT, MAGV, MAMH, MALOP, TRINHDO, NGAYTHI, LAN, SOCAUTHI, THOIGIAN" +" from GIAOVIEN_DANGKY where (NGAYTHI >= '" + NgayDau + "' and NGAYTHI <= '" + NgaySau + "')";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "Mã giáo viên";
            dataGridView1.Columns[2].HeaderText = "Mã môn học";
            dataGridView1.Columns[3].HeaderText = "Mã lớp";
            dataGridView1.Columns[4].HeaderText = "Trình độ";
            dataGridView1.Columns[5].HeaderText = "Ngày thi";
            dataGridView1.Columns[6].HeaderText = "Lần";
            dataGridView1.Columns[7].HeaderText = "Số câu";
            dataGridView1.Columns[8].HeaderText = "Thời gian";
            conn_publisher.Close();
        }
    }
}
