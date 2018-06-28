using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CLS_NatSu.MyClass;

namespace CLS_NatSu.MyForm
{
    public partial class Refresh_ImageNotInput : DevExpress.XtraEditors.XtraForm
    {
        int minute = 0;
        public Refresh_ImageNotInput()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetImageNotSubmit();
        }
        public void GetImageNotSubmit()
        {
            gridControl1.DataSource = (from w in Global.Db.GetImageNotSubmitDeInput(int.Parse(string.IsNullOrEmpty(txt_Minute.Text) ? "10" : txt_Minute.Text)) select new { w.BatchID, w.BatchName, w.IdImage, w.UserName, w.Start_Date, w.TimeRange }).ToList(); ;
           }
        private void Refresh_ImageNotInput_Load(object sender, EventArgs e)
        {
            txt_Minute.Text = "10";
            GetImageNotSubmit();
        }    

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        
        private void Refresh_ImageNotInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btn_Refresh_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tp_DeSo)
            {
                if(gridView1.GetSelectedRows().Count()<=0)
                {
                    MessageBox.Show("Bạn chưa chọn dòng. Hãy chọn dòng trước khi thực hiện.");
                    return;
                }
                foreach (var rowHandle in gridView1.GetSelectedRows())
                {
                    string BatchID = gridView1.GetRowCellValue(rowHandle, "BatchID").ToString();
                    string ImageName = gridView1.GetRowCellValue(rowHandle, "IdImage").ToString();
                    string UserName = gridView1.GetRowCellValue(rowHandle, "UserName").ToString();
                    Global.Db.RefreshImageNotInput(BatchID, ImageName, UserName,Global.StrIdProject);
                }
            }
            GetImageNotSubmit();
        }

        private void btn_Refresh_All_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tp_DeSo && gridView1.RowCount > 0)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn refresh tất cả.", "Cảnh bảo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string BatchID = gridView1.GetRowCellValue(i, "BatchID").ToString();
                    string ImageName = gridView1.GetRowCellValue(i, "IdImage").ToString();
                    string UserName = gridView1.GetRowCellValue(i, "UserName").ToString();
                    Global.Db.RefreshImageNotInput(BatchID, ImageName, UserName, Global.StrIdProject);
                }
            }
            GetImageNotSubmit();
        }

        private void txt_MinuteSo_TextChanged(object sender, EventArgs e)
        {
            GetImageNotSubmit();
        }
        
    }
}