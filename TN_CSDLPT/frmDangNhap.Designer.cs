namespace TN_CSDLPT
{
    partial class frmDangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbCoSo = new System.Windows.Forms.ComboBox();
            this.getSubscribesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dS = new TN_CSDLPT.DS();
            this.getSubscribesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.labelCoSo = new DevExpress.XtraEditors.LabelControl();
            this.labelTaiKhoan = new DevExpress.XtraEditors.LabelControl();
            this.labelMatMa = new DevExpress.XtraEditors.LabelControl();
            this.btnDangNhap = new DevExpress.XtraEditors.SimpleButton();
            this.ckbSinhVien = new System.Windows.Forms.CheckBox();
            this.get_SubscribesTableAdapter = new TN_CSDLPT.DSTableAdapters.Get_SubscribesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.getSubscribesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getSubscribesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCoSo
            // 
            this.cmbCoSo.DataSource = this.getSubscribesBindingSource1;
            this.cmbCoSo.DisplayMember = "TENCS";
            this.cmbCoSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoSo.FormattingEnabled = true;
            this.cmbCoSo.Location = new System.Drawing.Point(225, 115);
            this.cmbCoSo.Name = "cmbCoSo";
            this.cmbCoSo.Size = new System.Drawing.Size(233, 24);
            this.cmbCoSo.TabIndex = 0;
            this.cmbCoSo.ValueMember = "TENSERVER";
            this.cmbCoSo.SelectedIndexChanged += new System.EventHandler(this.cmbCoSo_SelectedIndexChanged);
            // 
            // getSubscribesBindingSource1
            // 
            this.getSubscribesBindingSource1.DataMember = "Get_Subscribes";
            this.getSubscribesBindingSource1.DataSource = this.dS;
            // 
            // dS
            // 
            this.dS.DataSetName = "DS";
            this.dS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getSubscribesBindingSource
            // 
            this.getSubscribesBindingSource.DataMember = "Get_Subscribes";
            this.getSubscribesBindingSource.DataSource = this.dS;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(225, 167);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(233, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(225, 224);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(233, 22);
            this.txtPass.TabIndex = 2;
            // 
            // labelCoSo
            // 
            this.labelCoSo.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCoSo.Appearance.Options.UseFont = true;
            this.labelCoSo.Location = new System.Drawing.Point(120, 118);
            this.labelCoSo.Name = "labelCoSo";
            this.labelCoSo.Size = new System.Drawing.Size(46, 21);
            this.labelCoSo.TabIndex = 3;
            this.labelCoSo.Text = "Cơ sở";
            this.labelCoSo.Click += new System.EventHandler(this.labelCoSo_Click);
            // 
            // labelTaiKhoan
            // 
            this.labelTaiKhoan.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTaiKhoan.Appearance.Options.UseFont = true;
            this.labelTaiKhoan.Location = new System.Drawing.Point(120, 168);
            this.labelTaiKhoan.Name = "labelTaiKhoan";
            this.labelTaiKhoan.Size = new System.Drawing.Size(86, 21);
            this.labelTaiKhoan.TabIndex = 4;
            this.labelTaiKhoan.Text = "Giảng Viên:";
            this.labelTaiKhoan.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelMatMa
            // 
            this.labelMatMa.Appearance.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMatMa.Appearance.Options.UseFont = true;
            this.labelMatMa.Location = new System.Drawing.Point(120, 225);
            this.labelMatMa.Name = "labelMatMa";
            this.labelMatMa.Size = new System.Drawing.Size(56, 21);
            this.labelMatMa.TabIndex = 5;
            this.labelMatMa.Text = "Mật mã";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Location = new System.Drawing.Point(198, 300);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(94, 29);
            this.btnDangNhap.TabIndex = 6;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // ckbSinhVien
            // 
            this.ckbSinhVien.AutoSize = true;
            this.ckbSinhVien.Location = new System.Drawing.Point(488, 168);
            this.ckbSinhVien.Name = "ckbSinhVien";
            this.ckbSinhVien.Size = new System.Drawing.Size(83, 20);
            this.ckbSinhVien.TabIndex = 8;
            this.ckbSinhVien.Text = "Sinh viên";
            this.ckbSinhVien.UseVisualStyleBackColor = true;
            this.ckbSinhVien.CheckedChanged += new System.EventHandler(this.ckbSinhVien_CheckedChanged);
            // 
            // get_SubscribesTableAdapter
            // 
            this.get_SubscribesTableAdapter.ClearBeforeFill = true;
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ckbSinhVien);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.labelMatMa);
            this.Controls.Add(this.labelTaiKhoan);
            this.Controls.Add(this.labelCoSo);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.cmbCoSo);
            this.Name = "frmDangNhap";
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.getSubscribesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getSubscribesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCoSo;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPass;
        private DevExpress.XtraEditors.LabelControl labelCoSo;
        private DevExpress.XtraEditors.LabelControl labelTaiKhoan;
        private DevExpress.XtraEditors.LabelControl labelMatMa;
        private DevExpress.XtraEditors.SimpleButton btnDangNhap;
        private System.Windows.Forms.CheckBox ckbSinhVien;
        private DS dS;
        private System.Windows.Forms.BindingSource getSubscribesBindingSource;
        private DSTableAdapters.Get_SubscribesTableAdapter get_SubscribesTableAdapter;
        private System.Windows.Forms.BindingSource getSubscribesBindingSource1;
    }
}