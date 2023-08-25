﻿using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;

namespace TN_CSDLPT
{
    internal static class Program
    {
       

        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static String connstr_Publisher = "Data source= DESKTOP-J73EN84\\MAYCHU; Initial Catalog = TN_CSDLPT; Integrated Security=True";

        public static SqlDataReader myReader; //đọc dữ liệu
        public static String servername = ""; //Biến lưu trữ sever name
        public static String username = ""; //Biến lưu trữ user name 
        public static String mlogin = ""; //Biến lưu trữ login người dùng truyền vào
        public static String password = ""; //Biến lưu trữ pass người dùng truyền vào

        public static String database = "TN_CSDLPT";
        public static String remotelogin = "hotroketnoi"; //Hỗ trợ kết nối giữa các trạm ví dụ tài khoản HoTroKetNoi
        public static String remotepassword = "123456";
        public static String mloginDN = ""; //login được lấy ra thông qua câu sp của mình
        public static String passwordDN = "";
        public static String mGroup = "";
        public static String mHoten = "";
        public static String MACOSO = "";
        public static String Lop = "";
        public static int mChinhanh = 0; // lưu trữ chi nhánh người đó thuộc về
        public static frmMain frmChinh;

        public static BindingSource bds_dspm = new BindingSource();  // giữ bdsPM khi đăng nhập
        public static SqlCommand sqlcmd = new SqlCommand();

       /* public static String Coin = "";
        public static float Tien ;
        public static String Id = "";*/
        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source="+Program.servername.ToString()+";Initial Catalog="+
                      Program.database + ";User ID=" +
                      Program.mlogin + ";password=" + Program.password;
                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                MessageBox.Show("Đăng nhập thành công!");
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }

        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            //sqlcmd.CommandTimeout = 600;
            if (Program.conn.State == System.Data.ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed)
            {
                Program.conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static int ExecuteReader(SqlCommand cmd)
        {
            int result;
            try
            {
                Program.KetNoi();
                cmd.Connection = Program.conn;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khi thuc thi lenh SQL: " + ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
            try
            {
                result = (int)cmd.Parameters["@VALUE"].Value;
            }
            catch { }

            return result;
        }

        public static void ExecSQLCommand(SqlCommand sqlCommand, SqlConnection conn)
        {
            if (Program.conn.State == System.Data.ConnectionState.Closed)
            {
                Program.conn.Open();
            }
            try
            {
                sqlCommand.ExecuteNonQuery();
                //Program.conn.Open();
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("Loi ket noi data ExecSQLCommand")) MessageBox.Show("loi");
                else MessageBox.Show(e.Message);
                Program.conn.Close();
            }

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*frmChinh = new frmMain();
            Application.Run(frmChinh);*/

            //Application.Run(new tiktok());

            Application.Run(new frmDangNhap());
            //Application.Run(new frmMain());
        }
    }
}
