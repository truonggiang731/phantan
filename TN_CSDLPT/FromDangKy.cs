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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TN_CSDLPT
{
    public partial class FromDangKy : Form
    {
        SqlConnection conn_publisher = new SqlConnection();
        DataTable dt = new DataTable();
        string flag = "";
        public FromDangKy()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Phân quyền
        private void PhanQuyen()
        {
            if(Program.mGroup=="TRUONG")    radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = false;
            if (Program.mGroup == "COSO") 
            {
                radioButton1.Visible = false;
                radioButton2.Visible = radioButton3.Visible = radioButton4.Visible = true;
            } 

        }
        private void FromDangKy_Load(object sender, EventArgs e)
        {
            PhanQuyen();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            flag = "TRUONG";
            label2.Visible = label3.Visible = label3.Visible = label4.Visible = true;
            textBox1.Visible = textBox2.Visible = textBox3.Visible = true;
            button1.Visible = true;
            label2.Text = "Login name:";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            flag = "COSO";
            label2.Visible = label3.Visible = label3.Visible = label4.Visible = true;
            textBox1.Visible = textBox2.Visible = textBox3.Visible = true;
            button1.Visible = true;
            label2.Text = "Login name:";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            flag = "GIANGVIEN";
            label2.Visible = label3.Visible = label3.Visible = label4.Visible = true;
            textBox1.Visible = textBox2.Visible = textBox3.Visible = true;
            button1.Visible = true;
            label2.Text = "Login name:";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            flag = "SINHVIEN";
            label2.Visible = label3.Visible = label4.Visible = true;
            textBox1.Visible = textBox3.Visible = true;
            label3.Visible = textBox2.Visible = false;
            label2.Text = "Mã sinh viên:";
            button1.Visible = true;
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

        //Kiểm tra mã có trùng không
        private bool Check_Trung_GV(String MAGV)
        {
            String strlenh = "select MAGV from LINK0.TN_CSDLPT.dbo.GIAOVIEN";
            dt = Program.ExecSqlDataTable(strlenh);
            foreach (DataRow row in dt.Rows)
            {
                String maDBGridView = row["MAGV"].ToString();
                if (maDBGridView.Trim() == MAGV.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        //Kiểm tra mã có trùng không
        private bool Check_Trung_SV(String MASINHVIEN)
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

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (flag == "SINHVIEN")
            {
                if (Check_NULL(textBox1, "Mã sinh viên không được để trống!")) return;
                if (Check_NULL(textBox3, "Mật khẩu không được để trống!")) return;

                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Tài khoản không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (textBox3.Text.Contains(" "))
                {
                    MessageBox.Show("Mật khẩu không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Check_Trung_SV(textBox1.Text.ToString().Trim())==false)
                {
                    MessageBox.Show("Mã sinh viên không tồn tại!");
                    return;
                }

                String strLenh = "sp_LogIn";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@LoginName", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Password", textBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@UserName", textBox1.Text.ToString().Trim()));

                SqlParameter parameter1 = new SqlParameter("@Role", SqlDbType.Char);
                parameter1.Value = flag;
                sqlCommand.Parameters.Add(parameter1);

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Tạo longin thành công!");
            }
            else
            {
                if (Check_NULL(textBox1, "LoginName không được để trống!")) return;
                if (Check_NULL(textBox2, "Mã giáo viên không được để trống!")) return;
                if (Check_NULL(textBox3, "Mật khẩu không được để trống!")) return;

                if (textBox1.Text.Contains(" "))
                {
                    MessageBox.Show("Tài khoản không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox2.Text.Contains(" "))
                {
                    MessageBox.Show("Mã giảng viên không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox3.Text.Contains(" "))
                {
                    MessageBox.Show("Mật khẩu không được chứa khoảng trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Check_Trung_GV(textBox2.Text.ToString().Trim()) == false)
                {
                    MessageBox.Show("Mã giảng viên không tồn tại!");
                    return;
                }
                

                String strLenh = "sp_LogIn";
                SqlCommand sqlCommand = new SqlCommand(strLenh, Program.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 600;

                sqlCommand.Parameters.Add(new SqlParameter("@LoginName", textBox1.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@Password", textBox3.Text.ToString().Trim()));
                sqlCommand.Parameters.Add(new SqlParameter("@UserName", textBox2.Text.ToString().Trim()));

                SqlParameter parameter1 = new SqlParameter("@Role", SqlDbType.Char);
                parameter1.Value = flag;
                sqlCommand.Parameters.Add(parameter1);

                Program.ExecSQLCommand(sqlCommand, conn_publisher);
                MessageBox.Show("Tạo longin thành công!");
            }
           
        }
    }
}
