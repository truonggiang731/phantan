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

namespace TN_CSDLPT
{
    public partial class Formbangdiemlop : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();
        string malop = "";
        string mamonhoc = "";
        public Formbangdiemlop()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Formbangdiemlop_Load(object sender, EventArgs e)
        {
            HienThiDuLieuGridView();
            HienThiDuLieuGridView1();
        }

        public void HienThiDuLieuGridView()
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

        public void HienThiDuLieuGridView1()
        {
            String strlenh = "select MAMH, TENMH from MONHOC";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView2.DataSource = dt;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView2.Columns[0].HeaderText = "Mã môn học";
            dataGridView2.Columns[1].HeaderText = "Tên môn học";
            conn_publisher.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Mã lớp không được để trống!");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Mã môn học không được để trống!");
                return;
            }
            if (numericUpDown1.Text == "0")
            {
                MessageBox.Show("Lần thi không được để 0!");
                return;
            }
            XrptBangDiemMonHoc rpt = new XrptBangDiemMonHoc(textBox1.Text.ToString().Trim(), textBox2.Text.ToString().Trim(), numericUpDown1.Text.ToString().Trim());
            rpt.xrLop.Text = textBox1.Text.ToString().Trim();
            rpt.xrMonHoc.Text = textBox2.Text.ToString().Trim();
            rpt.xrLan.Text = numericUpDown1.Text.ToString().Trim();

            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }

        // sự kiện 1
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            }

        }


        //sưj kiện môn học
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView2.DataSource;
            if (dt.Rows.Count > 0)
            {
                textBox2.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
            }
        }
    }
}
