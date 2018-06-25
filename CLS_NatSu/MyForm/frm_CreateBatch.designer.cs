﻿namespace CLS_NatSu.MyForm
{
    partial class frm_CreateBatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CreateBatch));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txt_BatchName = new DevExpress.XtraEditors.TextEdit();
            this.txt_PathFolder = new DevExpress.XtraEditors.TextEdit();
            this.txt_Path = new DevExpress.XtraEditors.TextEdit();
            this.txt_UserCreate = new DevExpress.XtraEditors.TextEdit();
            this.txt_DateCreate = new DevExpress.XtraEditors.TextEdit();
            this.txt_ImagePath = new DevExpress.XtraEditors.TextEdit();
            this.btn_Browser = new DevExpress.XtraEditors.SimpleButton();
            this.btn_BrowserImage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_CreateBatch = new DevExpress.XtraEditors.SimpleButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lb_SoLuongHinh = new DevExpress.XtraEditors.LabelControl();
            this.lb_SobatchHoanThanh = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_SoImageDaHoanThanh = new System.Windows.Forms.Label();
            this.chk_ChiaUser = new System.Windows.Forms.CheckBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_BatchName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PathFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Path.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserCreate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_DateCreate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ImagePath.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(328, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(182, 27);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "TẠO BATCH MỚI";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(30, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 16);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Tên Batch (đơn):";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(30, 99);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(121, 16);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Folder Batch (nhiều):";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(30, 128);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 16);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "Đường dẫn:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(30, 158);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(88, 16);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "User tạo Batch:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(30, 189);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(90, 16);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "Ngày tạo Batch:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(30, 221);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(108, 16);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "Đường dẫn Image:";
            // 
            // txt_BatchName
            // 
            this.txt_BatchName.Location = new System.Drawing.Point(155, 65);
            this.txt_BatchName.Name = "txt_BatchName";
            this.txt_BatchName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BatchName.Properties.Appearance.Options.UseFont = true;
            this.txt_BatchName.Size = new System.Drawing.Size(460, 22);
            this.txt_BatchName.TabIndex = 2;
            this.txt_BatchName.EditValueChanged += new System.EventHandler(this.txt_BatchName_EditValueChanged);
            // 
            // txt_PathFolder
            // 
            this.txt_PathFolder.Location = new System.Drawing.Point(155, 96);
            this.txt_PathFolder.Name = "txt_PathFolder";
            this.txt_PathFolder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PathFolder.Properties.Appearance.Options.UseFont = true;
            this.txt_PathFolder.Size = new System.Drawing.Size(582, 22);
            this.txt_PathFolder.TabIndex = 3;
            this.txt_PathFolder.EditValueChanged += new System.EventHandler(this.txt_PathFolder_EditValueChanged);
            // 
            // txt_Path
            // 
            this.txt_Path.Location = new System.Drawing.Point(155, 125);
            this.txt_Path.Name = "txt_Path";
            this.txt_Path.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Path.Properties.Appearance.Options.UseFont = true;
            this.txt_Path.Size = new System.Drawing.Size(582, 22);
            this.txt_Path.TabIndex = 4;
            // 
            // txt_UserCreate
            // 
            this.txt_UserCreate.Location = new System.Drawing.Point(155, 155);
            this.txt_UserCreate.Name = "txt_UserCreate";
            this.txt_UserCreate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UserCreate.Properties.Appearance.Options.UseFont = true;
            this.txt_UserCreate.Properties.ReadOnly = true;
            this.txt_UserCreate.Size = new System.Drawing.Size(400, 22);
            this.txt_UserCreate.TabIndex = 5;
            // 
            // txt_DateCreate
            // 
            this.txt_DateCreate.Location = new System.Drawing.Point(155, 186);
            this.txt_DateCreate.Name = "txt_DateCreate";
            this.txt_DateCreate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DateCreate.Properties.Appearance.Options.UseFont = true;
            this.txt_DateCreate.Properties.ReadOnly = true;
            this.txt_DateCreate.Size = new System.Drawing.Size(400, 22);
            this.txt_DateCreate.TabIndex = 6;
            // 
            // txt_ImagePath
            // 
            this.txt_ImagePath.Location = new System.Drawing.Point(155, 218);
            this.txt_ImagePath.Name = "txt_ImagePath";
            this.txt_ImagePath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ImagePath.Properties.Appearance.Options.UseFont = true;
            this.txt_ImagePath.Properties.ReadOnly = true;
            this.txt_ImagePath.Size = new System.Drawing.Size(582, 22);
            this.txt_ImagePath.TabIndex = 7;
            // 
            // btn_Browser
            // 
            this.btn_Browser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browser.Appearance.Options.UseFont = true;
            this.btn_Browser.Location = new System.Drawing.Point(756, 94);
            this.btn_Browser.Name = "btn_Browser";
            this.btn_Browser.Size = new System.Drawing.Size(92, 23);
            this.btn_Browser.TabIndex = 10;
            this.btn_Browser.Text = "Browser...";
            this.btn_Browser.Click += new System.EventHandler(this.btn_Browser_Click);
            // 
            // btn_BrowserImage
            // 
            this.btn_BrowserImage.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BrowserImage.Appearance.Options.UseFont = true;
            this.btn_BrowserImage.Location = new System.Drawing.Point(756, 216);
            this.btn_BrowserImage.Name = "btn_BrowserImage";
            this.btn_BrowserImage.Size = new System.Drawing.Size(92, 23);
            this.btn_BrowserImage.TabIndex = 11;
            this.btn_BrowserImage.Text = "Chọn Image...";
            this.btn_BrowserImage.Click += new System.EventHandler(this.btn_BrowserImage_Click);
            // 
            // btn_CreateBatch
            // 
            this.btn_CreateBatch.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateBatch.Appearance.Options.UseFont = true;
            this.btn_CreateBatch.Location = new System.Drawing.Point(345, 284);
            this.btn_CreateBatch.Name = "btn_CreateBatch";
            this.btn_CreateBatch.Size = new System.Drawing.Size(164, 44);
            this.btn_CreateBatch.TabIndex = 12;
            this.btn_CreateBatch.Text = "Tạo Batch";
            this.btn_CreateBatch.Click += new System.EventHandler(this.btn_CreateBatch_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lb_SoLuongHinh
            // 
            this.lb_SoLuongHinh.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lb_SoLuongHinh.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lb_SoLuongHinh.Appearance.Options.UseFont = true;
            this.lb_SoLuongHinh.Appearance.Options.UseForeColor = true;
            this.lb_SoLuongHinh.Location = new System.Drawing.Point(158, 248);
            this.lb_SoLuongHinh.Name = "lb_SoLuongHinh";
            this.lb_SoLuongHinh.Size = new System.Drawing.Size(0, 19);
            this.lb_SoLuongHinh.TabIndex = 1;
            // 
            // lb_SobatchHoanThanh
            // 
            this.lb_SobatchHoanThanh.AutoSize = true;
            this.lb_SobatchHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SobatchHoanThanh.Location = new System.Drawing.Point(105, 306);
            this.lb_SobatchHoanThanh.Name = "lb_SobatchHoanThanh";
            this.lb_SobatchHoanThanh.Size = new System.Drawing.Size(0, 16);
            this.lb_SobatchHoanThanh.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 351);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 25);
            this.panel1.TabIndex = 14;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.White;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(855, 25);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Batch Số :";
            // 
            // lb_SoImageDaHoanThanh
            // 
            this.lb_SoImageDaHoanThanh.AutoSize = true;
            this.lb_SoImageDaHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SoImageDaHoanThanh.Location = new System.Drawing.Point(124, 306);
            this.lb_SoImageDaHoanThanh.Name = "lb_SoImageDaHoanThanh";
            this.lb_SoImageDaHoanThanh.Size = new System.Drawing.Size(0, 16);
            this.lb_SoImageDaHoanThanh.TabIndex = 1;
            // 
            // chk_ChiaUser
            // 
            this.chk_ChiaUser.AutoSize = true;
            this.chk_ChiaUser.Location = new System.Drawing.Point(622, 68);
            this.chk_ChiaUser.Name = "chk_ChiaUser";
            this.chk_ChiaUser.Size = new System.Drawing.Size(72, 17);
            this.chk_ChiaUser.TabIndex = 9;
            this.chk_ChiaUser.Text = "Chia User";
            this.chk_ChiaUser.UseVisualStyleBackColor = true;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(755, 130);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(31, 13);
            this.labelControl8.TabIndex = 52;
            this.labelControl8.Text = "Z:\\abc";
            // 
            // frm_CreateBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 376);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.chk_ChiaUser);
            this.Controls.Add(this.lb_SoImageDaHoanThanh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_SobatchHoanThanh);
            this.Controls.Add(this.lb_SoLuongHinh);
            this.Controls.Add(this.btn_CreateBatch);
            this.Controls.Add(this.btn_BrowserImage);
            this.Controls.Add(this.btn_Browser);
            this.Controls.Add(this.txt_ImagePath);
            this.Controls.Add(this.txt_DateCreate);
            this.Controls.Add(this.txt_UserCreate);
            this.Controls.Add(this.txt_Path);
            this.Controls.Add(this.txt_PathFolder);
            this.Controls.Add(this.txt_BatchName);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_CreateBatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ";";
            this.Load += new System.EventHandler(this.frm_CreateBatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_BatchName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PathFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Path.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserCreate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_DateCreate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ImagePath.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txt_BatchName;
        private DevExpress.XtraEditors.TextEdit txt_PathFolder;
        private DevExpress.XtraEditors.TextEdit txt_Path;
        private DevExpress.XtraEditors.TextEdit txt_UserCreate;
        private DevExpress.XtraEditors.TextEdit txt_DateCreate;
        private DevExpress.XtraEditors.TextEdit txt_ImagePath;
        private DevExpress.XtraEditors.SimpleButton btn_Browser;
        private DevExpress.XtraEditors.SimpleButton btn_BrowserImage;
        private DevExpress.XtraEditors.SimpleButton btn_CreateBatch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.LabelControl lb_SoLuongHinh;
        private System.Windows.Forms.Label lb_SobatchHoanThanh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_SoImageDaHoanThanh;
        private System.Windows.Forms.CheckBox chk_ChiaUser;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}