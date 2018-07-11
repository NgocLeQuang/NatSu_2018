namespace CLS_NatSu.MyForm
{
    partial class frm_ExportExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ExportExcel));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lb_BatchName = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lb_Complete = new System.Windows.Forms.Label();
            this.btn_Browse = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.Batch = new DevExpress.XtraTab.XtraTabPage();
            this.list_Batch = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.Batch.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(858, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(92, 25);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Xuất Excel";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lb_BatchName);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.txt_path);
            this.panelControl1.Controls.Add(this.progressBar1);
            this.panelControl1.Controls.Add(this.lb_Complete);
            this.panelControl1.Controls.Add(this.btn_Browse);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1105, 95);
            this.panelControl1.TabIndex = 4;
            // 
            // lb_BatchName
            // 
            this.lb_BatchName.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_BatchName.Appearance.Options.UseFont = true;
            this.lb_BatchName.Location = new System.Drawing.Point(33, 40);
            this.lb_BatchName.Name = "lb_BatchName";
            this.lb_BatchName.Size = new System.Drawing.Size(68, 16);
            this.lb_BatchName.TabIndex = 9;
            this.lb_BatchName.Text = "Batch Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Đường dẫn lưu file Excel:";
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(158, 13);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(591, 21);
            this.txt_path.TabIndex = 7;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(31, 61);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(919, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // lb_Complete
            // 
            this.lb_Complete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Complete.AutoSize = true;
            this.lb_Complete.Location = new System.Drawing.Point(962, 66);
            this.lb_Complete.Name = "lb_Complete";
            this.lb_Complete.Size = new System.Drawing.Size(0, 13);
            this.lb_Complete.TabIndex = 4;
            // 
            // btn_Browse
            // 
            this.btn_Browse.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Appearance.Options.UseFont = true;
            this.btn_Browse.Location = new System.Drawing.Point(760, 10);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(92, 25);
            this.btn_Browse.TabIndex = 2;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.xtraTabControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 95);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1105, 484);
            this.panelControl2.TabIndex = 5;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.Batch;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(1101, 480);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Batch});
            // 
            // Batch
            // 
            this.Batch.Controls.Add(this.list_Batch);
            this.Batch.Name = "Batch";
            this.Batch.Size = new System.Drawing.Size(1095, 452);
            this.Batch.Text = "Batch";
            // 
            // list_Batch
            // 
            this.list_Batch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_Batch.FormattingEnabled = true;
            this.list_Batch.Location = new System.Drawing.Point(0, 0);
            this.list_Batch.MultiColumn = true;
            this.list_Batch.Name = "list_Batch";
            this.list_Batch.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.list_Batch.Size = new System.Drawing.Size(1095, 452);
            this.list_Batch.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frm_ExportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 579);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ExportExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportExcel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_ExportExcel_FormClosing);
            this.Load += new System.EventHandler(this.frm_ExportExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.Batch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lb_Complete;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage Batch;
        private System.Windows.Forms.ListBox list_Batch;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_path;
        private DevExpress.XtraEditors.SimpleButton btn_Browse;
        private DevExpress.XtraEditors.LabelControl lb_BatchName;
    }
}