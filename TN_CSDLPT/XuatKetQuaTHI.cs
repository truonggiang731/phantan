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
using System.Xml.Linq;

namespace TN_CSDLPT
{
    public partial class XuatKetQuaTHI : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();

        string monhoc, ngaythi, lanthi;
        public XuatKetQuaTHI(string name, string ngay_thi, string lan_thi)
        {
            InitializeComponent();
            this.monhoc = name;
            this.ngaythi = ngay_thi;
            this.lanthi = lan_thi;
        }

        //Hiện thị danh sách khoa
        private void HienThiDuLieuGridView()
        {
            String strlenh = "exec sp_LayChiTietDiem '"+Program.username+"', '"+lanthi+"', '"+ngaythi+"'";
            dt = Program.ExecSqlDataTable(strlenh);
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// phu het bang
            dataGridView1.Columns[0].HeaderText = "STT";
            dataGridView1.Columns[1].HeaderText = "CÂU HỎI";
            dataGridView1.Columns[2].HeaderText = "NỘI DUNG";
            dataGridView1.Columns[3].HeaderText = "A";
            dataGridView1.Columns[4].HeaderText = "B";
            dataGridView1.Columns[5].HeaderText = "C";
            dataGridView1.Columns[6].HeaderText = "D";
            dataGridView1.Columns[7].HeaderText = "ĐÁP ÁN";
            dataGridView1.Columns[8].HeaderText = "ĐÃ CHỌN";
            conn_publisher.Close();
        }

        private void XuatKetQuaTHI_Load(object sender, EventArgs e)
        {
            label8.Text = monhoc;
            label10.Text = lanthi;
            label7.Text = Program.mHoten;
            string label_9 = ngaythi;
            //-----------
            int spaceIndex = label_9.Trim().IndexOf(' ');
            string result = "";
            if (spaceIndex != -1)
            {
                result = label_9.Trim().Substring(0, spaceIndex);
                Console.WriteLine(result);
            }

            //string Ngay = "11/12/2023";

            int firstSlashIndex = result.IndexOf('/');
            int secondSlashIndex = result.IndexOf('/', firstSlashIndex + 1);

            string thang = result.Substring(0, firstSlashIndex);
            string ngay = result.Substring(firstSlashIndex + 1, secondSlashIndex - firstSlashIndex - 1);
            string nam = result.Substring(secondSlashIndex + 1);

            label9.Text = thang + "/" + ngay + "/" + nam;
            //------------

            String strlenh = "select MASV, MALOP, NGAYSINH from SINHVIEN where MASV = '"+Program.username+"'";
            DataTable dt = new DataTable();
            dt = Program.ExecSqlDataTable(strlenh);

            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MASV"].ToString();
                if (maDBGridView.Trim() == Program.username)
                {
                    label6.Text = row["MALOP"].ToString();
                    //label9.Text = row["NGAYSINH"].ToString();
                    break;
                }
            }

            HienThiDuLieuGridView();
        }
    }
}
