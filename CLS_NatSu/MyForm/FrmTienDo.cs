using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using CLS_NatSu.MyClass;
using Series = DevExpress.XtraCharts.Series;

namespace CLS_NatSu.MyForm
{
    public partial class FrmTienDo : XtraForm
    {
        public FrmTienDo()
        {
            InitializeComponent();
        }

        bool FlagLoad = false;
        private void frm_TienDo_Load(object sender, EventArgs e)
        {
            FlagLoad = true;
            cbb_Batch.DataSource = null;
            cbb_Batch.Text = "";
            var fBatchName = (from w in Global.Db.tbl_Batches where w.CongKhaiBatch == true orderby w.DateCreate descending select new { w.BatchID, w.BatchName }).ToList();
            cbb_Batch.DataSource = fBatchName;
            cbb_Batch.DisplayMember = "BatchName";
            cbb_Batch.ValueMember = "BatchID";
            FlagLoad = false;
            cbb_Batch_SelectedIndexChanged(null, null);
        }
        private void ThongKe()
        {
            try
            {
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                
                if (rb_All_DESO.Checked)
                {
                    lb_soHinhUserGood.Text = (from w in Global.Db.tbl_Images join b in Global.Db.tbl_Batches on w.BatchID equals (b.BatchID) where b.CongKhaiBatch == true & w.FlagReadDeSo_Good == 0 select w).Count().ToString();
                    lb_soHinhUserNotGood.Text = (from w in Global.Db.tbl_Images where w.FlagReadDeSo_NotGood == 0 & w.BatchID == cbb_Batch.SelectedValue + "" select w).Count().ToString();
                    lb_TongSoHinh.Text = (from w in Global.Db.tbl_Images join b in Global.Db.tbl_Batches on w.BatchID equals (b.BatchID) where b.CongKhaiBatch == true select w).Count().ToString();
                    chartControl1.DataSource = Global.Db.ThongKeTienDoAll();
                }
                else if (rb_deso.Checked)
                {
                    lb_soHinhUserGood.Text = (from w in Global.Db.tbl_Images where w.FlagReadDeSo_Good == 0 & w.BatchID == cbb_Batch.SelectedValue + "" select w).Count().ToString();
                    lb_soHinhUserNotGood.Text = (from w in Global.Db.tbl_Images where w.FlagReadDeSo_NotGood == 0 & w.BatchID == cbb_Batch.SelectedValue + "" select w).Count().ToString();
                    lb_TongSoHinh.Text = (from w in Global.Db.tbl_Images join b in Global.Db.tbl_Batches on w.BatchID equals (b.BatchID) where w.BatchID == cbb_Batch.SelectedValue.ToString() select w.IDImage).Count() + "";
                    chartControl1.DataSource = Global.Db.ThongKeTienDo(cbb_Batch.SelectedValue + "");
                }
                Series series1 = new Series("Series1", ViewType.Pie);
                series1.ArgumentScaleType = ScaleType.Qualitative;
                series1.ArgumentDataMember = "name";
                series1.ValueScaleType = ScaleType.Numerical;
                series1.ValueDataMembers.AddRange("soluong");
                chartControl1.Series.Add(series1);
                ((PiePointOptions)series1.Label.PointOptions).PointView = PointView.ArgumentAndValues;
                chartControl1.PaletteName = "Palette 1";
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void btn_ChiTiet_Click(object sender, EventArgs e)
        {
            frm_ChiTietTienDo frm = new frm_ChiTietTienDo();
            frm.Loai = rb_deso.Checked|| rb_All_DESO.Checked ? "DESO" : "";
            frm.lb_fBatchName.Text = rb_All_DESO.Checked ? "AllDESO" : cbb_Batch.Text;
            frm.BatchID = rb_All_DESO.Checked ? "AllDESO" : cbb_Batch.SelectedValue.ToString();
            frm.ShowDialog();
        }
        
        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            string argument = e.SeriesPoint.Argument;
            var pointValue = e.SeriesPoint.Values[0];
            if (argument == "Hình chưa nhập")
            {
                e.LabelText = "Hình chưa nhập: " + pointValue + " hình";
            }
            else if (argument == "Hình đang nhập")
            {
                e.LabelText = "Hình đang nhập: " + pointValue + " hình";
            }
            else if (argument == "Hình chờ check")
            {
                e.LabelText = "Hình chờ check: " + pointValue + " hình";
            }
            else if (argument == "Hình đang check")
            {
                e.LabelText = "Hình đang check: " + pointValue + " hình";
            }
            else if (argument == "Hình hoàn thành")
            {
                e.LabelText = "Hình hoàn thành: " + pointValue + " hình";
            }
        }

        private void time_CheckHinhChuaNhap_Tick(object sender, EventArgs e)
        {
            if (cbb_Batch.Text == "")
                return;
            ThongKe();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_deso.Checked)
                ThongKe();
        }
        
        private void cbb_Batch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 3)
                e.Handled = true;
        }

        private void cbb_Batch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                ((System.Windows.Forms.ComboBox)sender).Text = "";
        }

        private void cbb_Batch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FlagLoad)
                return;
            ThongKe();
        }

        private void rb_All_CheckedChanged(object sender, EventArgs e)
        {
            cbb_Batch.Enabled = !rb_All_DESO.Checked;
            if (rb_All_DESO.Checked)
                ThongKe();
        }
    }
}