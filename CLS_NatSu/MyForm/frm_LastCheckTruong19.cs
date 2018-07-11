using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using CLS_NatSu.MyClass;
using CLS_NatSu.Properties;
using CLS_NatSu.MyData;

namespace CLS_NatSu.MyForm
{
    public partial class frm_LastCheckTruong19 : DevExpress.XtraEditors.XtraForm
    {
        public frm_LastCheckTruong19()
        {
            InitializeComponent();
        }
        string Folder = "";
        private void frm_LastCheckTruong19_Load(object sender, EventArgs e)
        {
            FlagLoad = true;
            Folder = "";
            SetDataLookUpEdit();
            txt_Truong_19_1.Properties.DataSource = category;
            txt_Truong_19_1.Properties.DisplayMember = "DE";
            txt_Truong_19_1.Properties.ValueMember = "DE";
            txt_Truong_19_2.Properties.DataSource = category;
            txt_Truong_19_2.Properties.DisplayMember = "DE";
            txt_Truong_19_2.Properties.ValueMember = "DE";
            txt_Truong_19_3.Properties.DataSource = category;
            txt_Truong_19_3.Properties.DisplayMember = "DE";
            txt_Truong_19_3.Properties.ValueMember = "DE";
            txt_Truong_19_4.Properties.DataSource = category;
            txt_Truong_19_4.Properties.DisplayMember = "DE";
            txt_Truong_19_4.Properties.ValueMember = "DE";
            txt_Truong_19_5.Properties.DataSource = category;
            txt_Truong_19_5.Properties.DisplayMember = "DE";
            txt_Truong_19_5.Properties.ValueMember = "DE";
            tp_UC_Check.ShowTabHeader = DefaultBoolean.False;
            this.btn_Luu_DeSo1.Visible = false;
            var BatchTemp = (from w in Global.Db.GetBatchLastcheckTruong19(Global.StrUserName) select new { w.BatchID, w.BatchName }).ToList();

            if (BatchTemp.Count() > 0)
            {
                cbb_Batch_Check.DataSource = BatchTemp;
                cbb_Batch_Check.DisplayMember = "BatchName";
                cbb_Batch_Check.ValueMember = "BatchID";
                cbb_Batch_Check.SelectedValue = Global.StrBatchID;
                if (string.IsNullOrEmpty(cbb_Batch_Check.Text))
                    cbb_Batch_Check.SelectedIndex = 0;
                Folder = (from w in Global.Db.GetFolder(cbb_Batch_Check.SelectedValue + "") select w.PathServer).FirstOrDefault();
            }
            ResetData();
            cbb_Batch_Check.Focus();
            FlagLoad = false;
        }
        private void ResetData()
        {
            this.lb_Image.Text = "";
            this.txt_Truong_19_1.Text = "";
            this.txt_Truong_19_2.Text = "";
            this.txt_Truong_19_3.Text = "";
            this.txt_Truong_19_4.Text = "";
            this.txt_Truong_19_5.Text = "";
            this.uc_PictureBox1.imageBox1.Image = null;
            var str = (from w in Global.Db.GetSoLoiLastCheck(cbb_Batch_Check.SelectedValue + "", Global.StrUserName) select w.CountImage).FirstOrDefault();
            this.lb_Loi.Text = str + " Lỗi";
        }
        private string imageTemp_check = "";
        private void LockControl(bool kt)
        {
            if (kt)
            {
                btn_Luu_DeSo1.Visible = false;
            }
            else
            {
                btn_Luu_DeSo1.Visible = true;
            }
        }
        private string GetImage()
        {
            LockControl(true);
            lb_Image.Text = "";
            imageTemp_check = "";
            imageTemp_check = (from w in Global.Db.GetImageLastCheck(cbb_Batch_Check.SelectedValue + "", Global.StrUserName) select w.Column1).FirstOrDefault();
            if (string.IsNullOrEmpty(imageTemp_check))
            {
                return "NULL";
            }
            lb_Image.Text = imageTemp_check;
            uc_PictureBox1.imageBox1.Image = null;
            if (uc_PictureBox1.LoadImage(Global.Webservice + Folder + cbb_Batch_Check.SelectedValue + "" + "/" + imageTemp_check, imageTemp_check, Settings.Default.ZoomImage) == "Error")
            {
                uc_PictureBox1.imageBox1.Image = Resources.svn_deleted;
                return "Error";
            }
            return "ok";
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbb_Batch_Check.Text))
                return;
            if (Global.RunUpdateVersion())
                Application.Exit();
            if (Global.StrRole.ToUpper() == "CheckerDEJP".ToUpper()|| Global.StrRole.ToUpper() == "ADMIN".ToUpper())
            {
                //var nhap = (from w in Global.Db.tbl_Images join b in Global.Db.tbl_Batches on w.BatchID equals b.BatchID where w.BatchID == cbb_Batch_Check.SelectedValue + "" && w.ReadImageDeSo < 2 select w.IDImage).Count();
                //if (nhap > 0)
                //{
                //    MessageBox.Show("Chưa nhập xong DeSo!");
                //    return;
                //}
                Folder = (from w in Global.Db.GetFolder(cbb_Batch_Check.SelectedValue + "") select w.PathServer).FirstOrDefault();
                string temp = GetImage();
                if (temp == "NULL")
                {
                    uc_PictureBox1.imageBox1.Image = null;
                    MessageBox.Show(@"Batch '" + cbb_Batch_Check.Text + "' đã hoàn thành");
                    return;
                }
                if (temp == "Error")
                {
                    MessageBox.Show("Lỗi load hình");
                    return;
                }
                Load_DeSo(cbb_Batch_Check.SelectedValue + "", lb_Image.Text);
                //
                btn_Luu_DeSo1.Visible = true;
                btn_Start.Visible = false;
            }
        }
        private void Load_DeSo(string fbatchname, string idimage)
        {
            List<GetDataLastCheckResult> data = (from w in Global.Db.GetDataLastCheck(fbatchname, idimage) select w).ToList();
            txt_Truong_19_1.Text = data[0].Truong_19;
            txt_Truong_19_2.Text = data[1].Truong_19;
            txt_Truong_19_3.Text = data[2].Truong_19;
            txt_Truong_19_4.Text = data[3].Truong_19;
            txt_Truong_19_5.Text = data[4].Truong_19;
            txt_Truong_19_1.Focus();
            this.timer1.Enabled = true;
        }

        private List<Category> category = new List<Category>();

        public class Category
        {
            public string DE { get; set; }
            public string JP { get; set; }
        }
        private void SetDataLookUpEdit()
        {
            category.Clear();
            category.Add(new Category() { DE = "", JP = "" });
            category.Add(new Category() { DE = "1", JP = "Số 70 or 70歳以上 or 70以上" });
            category.Add(new Category() { DE = "2", JP = "二以上" });
            category.Add(new Category() { DE = "3", JP = "月額変更予定" });
            category.Add(new Category() { DE = "4", JP = "途中入社" });
            category.Add(new Category() { DE = "5", JP = "病休 or 育休 or 休職 or育児休暇 or 病気休暇" });
            category.Add(new Category() { DE = "6", JP = "短 or 短時間" });
            category.Add(new Category() { DE = "7", JP = "パ or パート" });
            category.Add(new Category() { DE = "8", JP = "年間平均" });
            category.Add(new Category() { DE = "9", JP = "Thấy số 9" });
            category.Add(new Category() { DE = "*", JP = "Nếu có hai giá trị chữ hoặc không phán đoán được (chữ khác)" });
        }
        private void txt_Truong_19_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] data19 = { "8", "42", "49", "50", "51", "52", "53", "54", "55", "56", "57", "63" };
            if (!data19.Contains((int)e.KeyChar + ""))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            LockControl(false);
        }

        bool FlagLoad = false;
        public void LoadBatchMoi()
        {
            if (MessageBox.Show("You want to do the next batch?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                base.Close();
            }
            else
            {
                Folder = "";
                this.btn_Luu_DeSo1.Visible = false;
                this.ResetData();
                this.cbb_Batch_Check.Text = "";
                this.cbb_Batch_Check.DataSource = null;
                this.cbb_Batch_Check.DataSource = (from w in Global.Db.GetBatchLastcheckTruong19(Global.StrUserName) select new { w.BatchID, w.BatchName }).ToList();
                this.cbb_Batch_Check.DisplayMember = "BatchName";
                this.cbb_Batch_Check.ValueMember = "BatchID";
                this.btn_Start_Click(null, null);
            }
        }

        private void btn_Luu_DeSo1_Click(object sender, EventArgs e)
        {
            Global.Db.Insert_LastCheckTruong19(cbb_Batch_Check.SelectedValue + "", lb_Image.Text, Global.StrUserName, txt_Truong_19_1.Text, txt_Truong_19_2.Text, txt_Truong_19_3.Text, txt_Truong_19_4.Text, txt_Truong_19_5.Text);
            ResetData();
            if (Global.RunUpdateVersion())
                Application.Exit();
            string temp = GetImage();
            if (temp == "NULL")
            {
                uc_PictureBox1.imageBox1.Image = null;
                MessageBox.Show(@"Batch '" + cbb_Batch_Check.Text + "' đã hoàn thành");
                LoadBatchMoi();
                return;
            }
            if (temp == "Error")
            {
                MessageBox.Show("Lỗi load hình");
                return;
            }
            Load_DeSo(cbb_Batch_Check.SelectedValue + "", lb_Image.Text);
        }

        private void cbb_Batch_Check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                cbb_Batch_Check.Text = "";
        }

        private void cbb_Batch_Check_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 3)
            {
                e.Handled = true;
            }
        }

        private void cbb_Batch_Check_TextChanged(object sender, EventArgs e)
        {
            if (FlagLoad)
                return;
            this.btn_Luu_DeSo1.Visible = false;
            lb_Image.Text = "";
            ResetData();
            btn_Start.Visible = true;
        }
    }
}