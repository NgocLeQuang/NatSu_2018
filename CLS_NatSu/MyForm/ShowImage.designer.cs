﻿namespace CLS_NatSu.MyForm
{
    partial class ShowImage
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_Image = new System.Windows.Forms.Label();
            this.lb_Batch = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.uc_PictureBox1 = new CLS_NatSu.MyUserControl.uc_PictureBox();
            this.uC_ShowDataInput1 = new CLS_NatSu.MyUserControl.UC_ShowDataInput();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.AllowTouchScroll = true;
            this.panelControl2.AutoSize = true;
            this.panelControl2.Controls.Add(this.uC_ShowDataInput1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(670, 654);
            this.panelControl2.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panel1);
            this.panelControl3.Controls.Add(this.uc_PictureBox1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(760, 654);
            this.panelControl3.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_Image);
            this.panel1.Controls.Add(this.lb_Batch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 34);
            this.panel1.TabIndex = 3;
            // 
            // lb_Image
            // 
            this.lb_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Image.AutoSize = true;
            this.lb_Image.Location = new System.Drawing.Point(250, 10);
            this.lb_Image.Name = "lb_Image";
            this.lb_Image.Size = new System.Drawing.Size(37, 13);
            this.lb_Image.TabIndex = 1;
            this.lb_Image.Text = "Image";
            this.lb_Image.Click += new System.EventHandler(this.lb_Image_Click);
            // 
            // lb_Batch
            // 
            this.lb_Batch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_Batch.AutoSize = true;
            this.lb_Batch.Location = new System.Drawing.Point(20, 10);
            this.lb_Batch.Name = "lb_Batch";
            this.lb_Batch.Size = new System.Drawing.Size(34, 13);
            this.lb_Batch.TabIndex = 1;
            this.lb_Batch.Text = "Batch";
            this.lb_Batch.Click += new System.EventHandler(this.lb_Batch_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1435, 654);
            this.splitContainerControl1.SplitterPosition = 670;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitterPositionChanged += new System.EventHandler(this.splitContainerControl1_SplitterPositionChanged);
            // 
            // uc_PictureBox1
            // 
            this.uc_PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_PictureBox1.Location = new System.Drawing.Point(2, 2);
            this.uc_PictureBox1.Name = "uc_PictureBox1";
            this.uc_PictureBox1.Size = new System.Drawing.Size(756, 650);
            this.uc_PictureBox1.TabIndex = 2;
            // 
            // uC_ShowDataInput1
            // 
            this.uC_ShowDataInput1.AutoScroll = true;
            this.uC_ShowDataInput1.AutoSize = true;
            this.uC_ShowDataInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_ShowDataInput1.Location = new System.Drawing.Point(2, 2);
            this.uC_ShowDataInput1.Name = "uC_ShowDataInput1";
            this.uC_ShowDataInput1.Size = new System.Drawing.Size(666, 650);
            this.uC_ShowDataInput1.TabIndex = 0;
            // 
            // ShowImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 654);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ShowImage";
            this.Text = "Show data";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Label lb_Image;
        private System.Windows.Forms.Label lb_Batch;
        private System.Windows.Forms.Panel panel1;
        private MyUserControl.uc_PictureBox uc_PictureBox1;
        private MyUserControl.UC_ShowDataInput uC_ShowDataInput1;
    }
}