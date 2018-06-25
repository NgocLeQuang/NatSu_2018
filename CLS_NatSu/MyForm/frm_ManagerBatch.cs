﻿using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using CLS_NatSu.MyClass;

namespace CLS_NatSu.MyForm
{
    public partial class frm_ManagerBatch : XtraForm
    {
        public frm_ManagerBatch()
        {
            InitializeComponent();
        }

        private void frm_ManagerBatch_Load(object sender, EventArgs e)
        {
            refresh();
        }

        public bool Cal(int width, DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.IndicatorWidth = view.IndicatorWidth < width ? width : view.IndicatorWidth;
            return true;
        }

        private void LoadSttGridView(RowIndicatorCustomDrawEventArgs e, GridView dgv)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            SizeF size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
            int width = Convert.ToInt32(size.Width) + 20;
            BeginInvoke(new MethodInvoker(delegate { Cal(width, dgv); }));
        }

        private void refresh()
        {
            gridControl1.DataSource = (from var in Global.Db.GetBatch() select var).ToList();
        }
        
        private void btn_Xoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string BatchID = gridView1.GetFocusedRowCellValue("BatchID").ToString();
            DialogResult dlr = (MessageBox.Show("Bạn đang thực hiện xóa batch: " + gridView1.GetFocusedRowCellValue("BatchName").ToString() + "\nYes = xóa batch này \nNo = không thực hiện xóa", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2));
            if (dlr == DialogResult.Yes)
            {
                try
                {
                    string temp = Global.StrPath + "\\" + BatchID;
                    Global.Db.XoaBatch(BatchID);
                    Directory.Delete(temp, true);
                    MessageBox.Show("Đã xóa batch thành công!");
                }
                catch (Exception i)
                {
                    MessageBox.Show("Xóa batch bị lỗi!\n"+i.Message);
                }
            }
            int rowHandleFocus = gridView1.LocateByValue("BatchID", BatchID);
            refresh();
            if (rowHandleFocus != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                gridView1.FocusedRowHandle = rowHandleFocus;
        }

        private void btn_TaoBatch_Click(object sender, EventArgs e)
        {
            frm_CreateBatch a = new frm_CreateBatch();
            a.ShowDialog();
            refresh();
        }

        private void btn_xoabatch_Click(object sender, EventArgs e)
        {
            int i = 0;
            string s = "";
            foreach (var rowHandle in gridView1.GetSelectedRows())
            {
                i += 1;
                string BatchID = gridView1.GetRowCellValue(rowHandle, "BatchName").ToString();
                s += BatchID + "\n";
            }
            if (i <= 0)
            {
                MessageBox.Show("Bạn chưa chọn batch. Vui lòng chọn batch trước khi xóa");
                return;
            }

            DialogResult dlr = (MessageBox.Show("Bạn đang thực hiện xóa " + i + " batch sau:\n" + s + "\nYes = xóa những batch này \nNo = không thực hiện xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2));
            if (dlr == DialogResult.Yes)
            {
                foreach (var rowHandle in gridView1.GetSelectedRows())
                {
                    string BatchID = gridView1.GetRowCellValue(rowHandle, "BatchID").ToString();
                    string temp = Global.StrPath + "\\" + BatchID;
                    Global.Db.XoaBatch(BatchID);
                    Directory.Delete(temp, true);
                }
            }
            refresh();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            LoadSttGridView(e, gridView1);
        }
        
        private void gridView1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                string BatchID = gridView1.GetFocusedRowCellValue("BatchID") + "";
                string fielname = e.Column.FieldName;
                if (fielname == "CongKhaiBatch")
                {
                    bool check = (bool)e.Value;
                    if (check)
                    {
                        Global.Db.UpdateCongKhaiBatch(BatchID, 1);
                    }
                    else
                    {
                        Global.Db.UpdateCongKhaiBatch(BatchID, 0);
                    }
                    int rowHandle = gridView1.LocateByValue("BatchID", BatchID);
                    refresh();
                    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        gridView1.FocusedRowHandle = rowHandle;
                }
                else if (fielname == "ChiaUser")
                {
                    var ktDeSo = (from w in Global.Db.tbl_MissImage_DeSos where w.BatchID == BatchID select w.IDImage).ToList();
                    if (ktDeSo.Count > 0)
                    {
                        MessageBox.Show("Batch này đã được nhập!");
                    }
                    else
                    {
                        bool check = (bool)e.Value;
                        if (check)
                        {
                            Global.Db.UpdateBatchChiaUser(BatchID, 1);
                        }
                        else
                        {
                            Global.Db.UpdateBatchChiaUser(BatchID, 0);
                        }
                    }
                    int rowHandle = gridView1.LocateByValue("BatchID", BatchID);
                    refresh();
                    if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        gridView1.FocusedRowHandle = rowHandle;
                }
            }
            catch(Exception i)
            {
                MessageBox.Show("Lỗi : " + i.Message);
            }
        }
    }
}