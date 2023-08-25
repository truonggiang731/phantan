using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace TN_CSDLPT
{
    public partial class XrptXemKetQua : DevExpress.XtraReports.UI.XtraReport
    {
        public XrptXemKetQua()
        {
            
        }
        public XrptXemKetQua(String maNV, String MonHoc, int Lan)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = maNV;
            this.sqlDataSource1.Queries[1].Parameters[1].Value = MonHoc;
            this.sqlDataSource1.Queries[2].Parameters[2].Value = Lan;
            this.sqlDataSource1.Fill();

        }

    }
}
