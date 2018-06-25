using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CLS_NatSu.Properties;
using System.Collections.Generic;
using CLS_NatSu.MyData;
using CLS_NatSu.MyClass;
using System.Text.RegularExpressions;
using System.Text;
using DevExpress.XtraTab;
using DevExpress.Utils;

namespace CLS_NatSu.MyForm
{
    public partial class frm_Checker : XtraForm
    {
        public frm_Checker()
        {
            InitializeComponent();
        }
        private string fbatchRefresh = "";
        private bool fLagRefresh = false;
        public string TypeCheck = "";
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbb_Batch_Check.Text))
                return;
            if (Global.RunUpdateVersion())
                Application.Exit();
            if (Global.StrCheck == "CHECKDESO")
            {
                var nhap = (from w in Global.Db.tbl_Images join b in Global.Db.tbl_Batches on w.BatchID equals b.BatchID where w.BatchID == cbb_Batch_Check.SelectedValue+"" && w.ReadImageDeSo < 2 select w.IDImage).Count();
                var check = (from w in Global.Db.tbl_MissImage_DeSos join b in Global.Db.tbl_Batches on w.BatchID equals b.BatchID where w.BatchID == cbb_Batch_Check.SelectedValue + "" && w.Submit == false select w.IDImage).Count();
                if (nhap > 0)
                {
                    MessageBox.Show("Chưa nhập xong DeSo!");
                    return;
                }
                if (check > 0)
                {
                    var list_user = (from w in Global.Db.tbl_MissImage_DeSos join b in Global.Db.tbl_Batches on w.BatchID equals b.BatchID where w.BatchID == cbb_Batch_Check.SelectedValue + "" && w.Submit == false select w.UserName).ToList();
                    string sss = "";
                    foreach (var item in list_user)
                    {
                        sss += item + "\r\n";
                    }

                    if (list_user.Count > 0)
                    {
                        MessageBox.Show("Những user lấy hình về nhưng không nhập: \r\n" + sss);
                        return;
                    }
                }
            }
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
            btn_Luu_DeSo2.Visible = true;
            btn_Start.Visible = false;
        }

        private void ResetData()
        {
            this.lb_Image.Text = "";
            this.txt_Truong_01_User1.Text = "";
            this.txt_Truong_01_User2.Text = "";
            this.lb_User1.Text = "";
            this.lb_User2.Text = "";
            if (Global.StrCheck == "CHECKDESO")
            {
                this.UC_225_1.ResetData();
                this.UC_225_2.ResetData();
                this.UC_2225_1.ResetData();
                this.UC_2225_2.ResetData();
            }
            this.uc_PictureBox1.imageBox1.Image = null;
            string str = (from w in Global.Db.GetSoLoiCheck(string.IsNullOrEmpty(cbb_Batch_Check.Text ?? "") ? "" : (cbb_Batch_Check.SelectedValue)+"", Global.StrUserName, this.TypeCheck) select w.CountImage).FirstOrDefault<string>();
            this.lb_Loi.Text = str + " Lỗi";
        }


        public void LoadBatchMoi()
        {
            if (MessageBox.Show("You want to do the next batch?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                base.Close();
            }
            else
            {
                this.VisibleButtonSave();
                this.ResetData();
                this.cbb_Batch_Check.Text = "";
                this.cbb_Batch_Check.DataSource = null;
                this.cbb_Batch_Check.DataSource = (from w in Global.Db.GetBatNotFinishChecker(Global.StrUserName, this.TypeCheck) select new {BatchID = w.BatchID,BatchName = w.BatchName}).ToList();
                this.cbb_Batch_Check.DisplayMember = "BatchName";
                this.cbb_Batch_Check.ValueMember = "BatchID";
                this.ResetData();
                this.btn_Start_Click(null, null);
            }
        }

        private void VisibleButtonSave()
        {
            this.btn_Luu_DeSo1.Visible = false;
            this.btn_Luu_DeSo2.Visible = false;
        }

        private void LockControl(bool kt)
        {
            if (kt)
            {
                btn_Luu_DeSo1.Visible = false;
                btn_Luu_DeSo2.Visible = false;
            }
            else
            {
                btn_Luu_DeSo1.Visible = true;
                btn_Luu_DeSo2.Visible = true;
            }
        }
        private string imageTemp_check = "";
        private string GetImage()
        {
            LockControl(true);
            lb_Image.Text = "";
            imageTemp_check = "";
            imageTemp_check = (from w in Global.Db.GetImageCheck(cbb_Batch_Check.SelectedValue+"", Global.StrUserName,TypeCheck) select w.Column1).FirstOrDefault();
            if (string.IsNullOrEmpty(imageTemp_check))
            {
                return "NULL";
            }
            lb_Image.Text = imageTemp_check;
            uc_PictureBox1.imageBox1.Image = null;
            if (uc_PictureBox1.LoadImage(Global.Webservice + cbb_Batch_Check.SelectedValue + "" + "/" + imageTemp_check, imageTemp_check, Settings.Default.ZoomImage) == "Error")
            {
                uc_PictureBox1.imageBox1.Image = Resources.svn_deleted;
                return "Error";
            }
            return "ok";
        }
        string FormatCurency(string curency)// định dạng 1,234
        {
            string str = curency.ToString().Replace(",", "");
            if (str.Length < 1)
                return "";
            try
            {
                Convert.ToDouble(str);
            }
            catch
            {
                return curency;
            }
            if (str[0] + "" == "-")
            {
                str = str.Replace("-", "");
                string pattern = @"(?<a>\d*)(?<b>\d{3})*";
                Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
                StringBuilder sb = new StringBuilder();
                foreach (Capture i in m.Groups["b"].Captures)
                {
                    sb.Insert(0, "," + i.Value);
                }
                sb.Insert(0, m.Groups["a"].Value);
                return "-"+sb.ToString().Trim(',');
            }
            else
            {
                string pattern = @"(?<a>\d*)(?<b>\d{3})*";
                Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
                StringBuilder sb = new StringBuilder();
                foreach (Capture i in m.Groups["b"].Captures)
                {
                    sb.Insert(0, "," + i.Value);
                }
                sb.Insert(0, m.Groups["a"].Value);
                return sb.ToString().Trim(',');
            }
        }
        private List<tbl_DeSo> DataUser1 = new List<tbl_DeSo>();
        private List<tbl_DeSo> DataUser2 = new List<tbl_DeSo>();
        private void Load_DeSo(string fbatchname, string idimage)
        {
            //lb_User1.ForeColor = Color.Black;
            //lb_User2.ForeColor = Color.Black;
            if (Global.StrCheck == "CHECKDESO")
            {
                List<tbl_DeSo> data = (from w in Global.Db.tbl_DeSos where w.BatchID == fbatchname && w.IDImage == idimage && w.Phase == 1 select w).ToList();
                var result = (from w in Global.DbBpo.tbl_PhanCongs where w.UserName == data[0].UserName & w.IDProject==Global.StrIdProject select w.LevelUser).FirstOrDefault();
                if (!result)
                {
                    this.DataUser1.Add(data[0]);
                    this.DataUser1.Add(data[1]);
                    this.DataUser1.Add(data[2]);
                    this.DataUser1.Add(data[3]);
                    this.DataUser1.Add(data[4]);
                    this.DataUser2.Add(data[5]);
                    this.DataUser2.Add(data[6]);
                    this.DataUser2.Add(data[7]);
                    this.DataUser2.Add(data[8]);
                    this.DataUser2.Add(data[9]);
                    this.lb_User1.Text = data[0].UserName;
                    this.lb_User2.Text = data[5].UserName;
                    this.txt_Truong_01_User1.Text = data[0].Truong_01;

                    if (data[0].Truong_01 == "225")
                    {
                        this.UC_225_1.uC_225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                        this.UC_225_1.uC_225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                        this.UC_225_1.uC_225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                        this.UC_225_1.uC_225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                        this.UC_225_1.uC_225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                        this.UC_225_1.uC_225_Item1.txt_Truong_09.Text = FormatCurency(data[0].Truong_09);
                        this.UC_225_1.uC_225_Item1.txt_Truong_10.Text = FormatCurency(data[0].Truong_10);
                        this.UC_225_1.uC_225_Item1.txt_Truong_11.Text = FormatCurency(data[0].Truong_11);
                        this.UC_225_1.uC_225_Item1.txt_Truong_12.Text = FormatCurency(data[0].Truong_12);
                        this.UC_225_1.uC_225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                        this.UC_225_1.uC_225_Item1.txt_Truong_15.Text = FormatCurency(data[0].Truong_15);
                        this.UC_225_1.uC_225_Item1.txt_Truong_16.Text = FormatCurency(data[0].Truong_16);
                        this.UC_225_1.uC_225_Item1.txt_Truong_17.Text = FormatCurency(data[0].Truong_17);
                        this.UC_225_1.uC_225_Item1.txt_Truong_18.Text = FormatCurency(data[0].Truong_18);
                        this.UC_225_1.uC_225_Item1.txt_Truong_19.EditValue = data[0].Truong_19;
                        this.UC_225_1.uC_225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                        this.UC_225_1.uC_225_Item1.txt_Truong_21.Text = FormatCurency(data[0].Truong_21);
                        this.UC_225_1.uC_225_Item1.txt_Truong_22.Text = FormatCurency(data[0].Truong_22);
                        this.UC_225_1.uC_225_Item1.txt_Truong_23.Text = FormatCurency(data[0].Truong_23);
                        this.UC_225_1.uC_225_Item1.txt_Truong_24.Text = FormatCurency(data[0].Truong_24);
                        this.UC_225_1.uC_225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                        this.UC_225_1.uC_225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                        this.UC_225_1.uC_225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                        this.UC_225_1.uC_225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                        this.UC_225_1.uC_225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                        this.UC_225_1.uC_225_Item2.txt_Truong_09.Text = FormatCurency(data[1].Truong_09);
                        this.UC_225_1.uC_225_Item2.txt_Truong_10.Text = FormatCurency(data[1].Truong_10);
                        this.UC_225_1.uC_225_Item2.txt_Truong_11.Text = FormatCurency(data[1].Truong_11);
                        this.UC_225_1.uC_225_Item2.txt_Truong_12.Text = FormatCurency(data[1].Truong_12);
                        this.UC_225_1.uC_225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                        this.UC_225_1.uC_225_Item2.txt_Truong_15.Text = FormatCurency(data[1].Truong_15);
                        this.UC_225_1.uC_225_Item2.txt_Truong_16.Text = FormatCurency(data[1].Truong_16);
                        this.UC_225_1.uC_225_Item2.txt_Truong_17.Text = FormatCurency(data[1].Truong_17);
                        this.UC_225_1.uC_225_Item2.txt_Truong_18.Text = FormatCurency(data[1].Truong_18);
                        this.UC_225_1.uC_225_Item2.txt_Truong_19.EditValue = data[1].Truong_19;
                        this.UC_225_1.uC_225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                        this.UC_225_1.uC_225_Item2.txt_Truong_21.Text = FormatCurency(data[1].Truong_21);
                        this.UC_225_1.uC_225_Item2.txt_Truong_22.Text = FormatCurency(data[1].Truong_22);
                        this.UC_225_1.uC_225_Item2.txt_Truong_23.Text = FormatCurency(data[1].Truong_23);
                        this.UC_225_1.uC_225_Item2.txt_Truong_24.Text = FormatCurency(data[1].Truong_24);
                        this.UC_225_1.uC_225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                        this.UC_225_1.uC_225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                        this.UC_225_1.uC_225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                        this.UC_225_1.uC_225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                        this.UC_225_1.uC_225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                        this.UC_225_1.uC_225_Item3.txt_Truong_09.Text = FormatCurency(data[2].Truong_09);
                        this.UC_225_1.uC_225_Item3.txt_Truong_10.Text = FormatCurency(data[2].Truong_10);
                        this.UC_225_1.uC_225_Item3.txt_Truong_11.Text = FormatCurency(data[2].Truong_11);
                        this.UC_225_1.uC_225_Item3.txt_Truong_12.Text = FormatCurency(data[2].Truong_12);
                        this.UC_225_1.uC_225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                        this.UC_225_1.uC_225_Item3.txt_Truong_15.Text = FormatCurency(data[2].Truong_15);
                        this.UC_225_1.uC_225_Item3.txt_Truong_16.Text = FormatCurency(data[2].Truong_16);
                        this.UC_225_1.uC_225_Item3.txt_Truong_17.Text = FormatCurency(data[2].Truong_17);
                        this.UC_225_1.uC_225_Item3.txt_Truong_18.Text = FormatCurency(data[2].Truong_18);
                        this.UC_225_1.uC_225_Item3.txt_Truong_19.EditValue = data[2].Truong_19;
                        this.UC_225_1.uC_225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                        this.UC_225_1.uC_225_Item3.txt_Truong_21.Text = FormatCurency(data[2].Truong_21);
                        this.UC_225_1.uC_225_Item3.txt_Truong_22.Text = FormatCurency(data[2].Truong_22);
                        this.UC_225_1.uC_225_Item3.txt_Truong_23.Text = FormatCurency(data[2].Truong_23);
                        this.UC_225_1.uC_225_Item3.txt_Truong_24.Text = FormatCurency(data[2].Truong_24);
                        this.UC_225_1.uC_225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                        this.UC_225_1.uC_225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                        this.UC_225_1.uC_225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                        this.UC_225_1.uC_225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                        this.UC_225_1.uC_225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                        this.UC_225_1.uC_225_Item4.txt_Truong_09.Text = FormatCurency(data[3].Truong_09);
                        this.UC_225_1.uC_225_Item4.txt_Truong_10.Text = FormatCurency(data[3].Truong_10);
                        this.UC_225_1.uC_225_Item4.txt_Truong_11.Text = FormatCurency(data[3].Truong_11);
                        this.UC_225_1.uC_225_Item4.txt_Truong_12.Text = FormatCurency(data[3].Truong_12);
                        this.UC_225_1.uC_225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                        this.UC_225_1.uC_225_Item4.txt_Truong_15.Text = FormatCurency(data[3].Truong_15);
                        this.UC_225_1.uC_225_Item4.txt_Truong_16.Text = FormatCurency(data[3].Truong_16);
                        this.UC_225_1.uC_225_Item4.txt_Truong_17.Text = FormatCurency(data[3].Truong_17);
                        this.UC_225_1.uC_225_Item4.txt_Truong_18.Text = FormatCurency(data[3].Truong_18);
                        this.UC_225_1.uC_225_Item4.txt_Truong_19.EditValue = data[3].Truong_19;
                        this.UC_225_1.uC_225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                        this.UC_225_1.uC_225_Item4.txt_Truong_21.Text = FormatCurency(data[3].Truong_21);
                        this.UC_225_1.uC_225_Item4.txt_Truong_22.Text = FormatCurency(data[3].Truong_22);
                        this.UC_225_1.uC_225_Item4.txt_Truong_23.Text = FormatCurency(data[3].Truong_23);
                        this.UC_225_1.uC_225_Item4.txt_Truong_24.Text = FormatCurency(data[3].Truong_24);
                        this.UC_225_1.uC_225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                        this.UC_225_1.uC_225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                        this.UC_225_1.uC_225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                        this.UC_225_1.uC_225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                        this.UC_225_1.uC_225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                        this.UC_225_1.uC_225_Item5.txt_Truong_09.Text = FormatCurency(data[4].Truong_09);
                        this.UC_225_1.uC_225_Item5.txt_Truong_10.Text = FormatCurency(data[4].Truong_10);
                        this.UC_225_1.uC_225_Item5.txt_Truong_11.Text = FormatCurency(data[4].Truong_11);
                        this.UC_225_1.uC_225_Item5.txt_Truong_12.Text = FormatCurency(data[4].Truong_12);
                        this.UC_225_1.uC_225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                        this.UC_225_1.uC_225_Item5.txt_Truong_15.Text = FormatCurency(data[4].Truong_15);
                        this.UC_225_1.uC_225_Item5.txt_Truong_16.Text = FormatCurency(data[4].Truong_16);
                        this.UC_225_1.uC_225_Item5.txt_Truong_17.Text = FormatCurency(data[4].Truong_17);
                        this.UC_225_1.uC_225_Item5.txt_Truong_18.Text = FormatCurency(data[4].Truong_18);
                        this.UC_225_1.uC_225_Item5.txt_Truong_19.EditValue = data[4].Truong_19;
                        this.UC_225_1.uC_225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                        this.UC_225_1.uC_225_Item5.txt_Truong_21.Text = FormatCurency(data[4].Truong_21);
                        this.UC_225_1.uC_225_Item5.txt_Truong_22.Text = FormatCurency(data[4].Truong_22);
                        this.UC_225_1.uC_225_Item5.txt_Truong_23.Text = FormatCurency(data[4].Truong_23);
                        this.UC_225_1.uC_225_Item5.txt_Truong_24.Text = FormatCurency(data[4].Truong_24);
                        this.UC_225_1.txt_Truong_Flag.Text = data[3].Truong_Flag;
                    }
                    else
                    {
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_06.Text = data[0].Truong_06;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_07.Text = FormatCurency(data[0].Truong_07);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_09.Text = FormatCurency(data[0].Truong_09);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_10.Text = FormatCurency(data[0].Truong_10);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_11.Text = FormatCurency(data[0].Truong_11);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_12.Text = FormatCurency(data[0].Truong_12);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_13.Text = data[0].Truong_13;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_15.Text = FormatCurency(data[0].Truong_15);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_16.Text = FormatCurency(data[0].Truong_16);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_17.Text = FormatCurency(data[0].Truong_17);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_18.Text = FormatCurency(data[0].Truong_18);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_19.EditValue = data[0].Truong_19;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_21.Text = FormatCurency(data[0].Truong_21);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_22.Text = FormatCurency(data[0].Truong_22);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_23.Text = FormatCurency(data[0].Truong_23);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_24.Text = FormatCurency(data[0].Truong_24);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_25.Text = data[0].Truong_25;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_26.Text = data[0].Truong_26;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_06.Text = data[1].Truong_06;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_07.Text = FormatCurency(data[1].Truong_07);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_09.Text = FormatCurency(data[1].Truong_09);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_10.Text = FormatCurency(data[1].Truong_10);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_11.Text = FormatCurency(data[1].Truong_11);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_12.Text = FormatCurency(data[1].Truong_12);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_13.Text = data[1].Truong_13;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_15.Text = FormatCurency(data[1].Truong_15);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_16.Text = FormatCurency(data[1].Truong_16);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_17.Text = FormatCurency(data[1].Truong_17);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_18.Text = FormatCurency(data[1].Truong_18);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_19.EditValue = data[1].Truong_19;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_21.Text = FormatCurency(data[1].Truong_21);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_22.Text = FormatCurency(data[1].Truong_22);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_23.Text = FormatCurency(data[1].Truong_23);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_24.Text = FormatCurency(data[1].Truong_24);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_25.Text = data[1].Truong_25;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_26.Text = data[1].Truong_26;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_06.Text = data[2].Truong_06;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_07.Text = FormatCurency(data[2].Truong_07);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_09.Text = FormatCurency(data[2].Truong_09);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_10.Text = FormatCurency(data[2].Truong_10);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_11.Text = FormatCurency(data[2].Truong_11);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_12.Text = FormatCurency(data[2].Truong_12);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_13.Text = data[2].Truong_13;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_15.Text = FormatCurency(data[2].Truong_15);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_16.Text = FormatCurency(data[2].Truong_16);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_17.Text = FormatCurency(data[2].Truong_17);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_18.Text = FormatCurency(data[2].Truong_18);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_19.EditValue = data[2].Truong_19;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_21.Text = FormatCurency(data[2].Truong_21);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_22.Text = FormatCurency(data[2].Truong_22);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_23.Text = FormatCurency(data[2].Truong_23);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_24.Text = FormatCurency(data[2].Truong_24);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_25.Text = data[2].Truong_25;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_26.Text = data[2].Truong_26;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_06.Text = data[3].Truong_06;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_07.Text = FormatCurency(data[3].Truong_07);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_09.Text = FormatCurency(data[3].Truong_09);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_10.Text = FormatCurency(data[3].Truong_10);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_11.Text = FormatCurency(data[3].Truong_11);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_12.Text = FormatCurency(data[3].Truong_12);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_13.Text = data[3].Truong_13;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_15.Text = FormatCurency(data[3].Truong_15);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_16.Text = FormatCurency(data[3].Truong_16);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_17.Text = FormatCurency(data[3].Truong_17);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_18.Text = FormatCurency(data[3].Truong_18);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_19.EditValue = data[3].Truong_19;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_21.Text = FormatCurency(data[3].Truong_21);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_22.Text = FormatCurency(data[3].Truong_22);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_23.Text = FormatCurency(data[3].Truong_23);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_24.Text = FormatCurency(data[3].Truong_24);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_25.Text = data[3].Truong_25;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_26.Text = data[3].Truong_26;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_06.Text = data[4].Truong_06;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_07.Text = FormatCurency(data[4].Truong_07);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_09.Text = FormatCurency(data[4].Truong_09);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_10.Text = FormatCurency(data[4].Truong_10);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_11.Text = FormatCurency(data[4].Truong_11);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_12.Text = FormatCurency(data[4].Truong_12);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_13.Text = data[4].Truong_13;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_15.Text = FormatCurency(data[4].Truong_15);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_16.Text = FormatCurency(data[4].Truong_16);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_17.Text = FormatCurency(data[4].Truong_17);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_18.Text = FormatCurency(data[4].Truong_18);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_19.EditValue = data[4].Truong_19;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_21.Text = FormatCurency(data[4].Truong_21);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_22.Text = FormatCurency(data[4].Truong_22);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_23.Text = FormatCurency(data[4].Truong_23);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_24.Text = FormatCurency(data[4].Truong_24);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_25.Text = data[4].Truong_25;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_26.Text = data[4].Truong_26;
                        this.UC_2225_1.txt_Truong_Flag.Text = data[3].Truong_Flag;
                    }
                    this.txt_Truong_01_User2.Text = data[5].Truong_01;
                    if (data[5].Truong_01 == "225")
                    {
                        this.UC_225_2.uC_225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                        this.UC_225_2.uC_225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                        this.UC_225_2.uC_225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                        this.UC_225_2.uC_225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                        this.UC_225_2.uC_225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                        this.UC_225_2.uC_225_Item1.txt_Truong_09.Text = FormatCurency(data[5].Truong_09);
                        this.UC_225_2.uC_225_Item1.txt_Truong_10.Text = FormatCurency(data[5].Truong_10);
                        this.UC_225_2.uC_225_Item1.txt_Truong_11.Text = FormatCurency(data[5].Truong_11);
                        this.UC_225_2.uC_225_Item1.txt_Truong_12.Text = FormatCurency(data[5].Truong_12);
                        this.UC_225_2.uC_225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                        this.UC_225_2.uC_225_Item1.txt_Truong_15.Text = FormatCurency(data[5].Truong_15);
                        this.UC_225_2.uC_225_Item1.txt_Truong_16.Text = FormatCurency(data[5].Truong_16);
                        this.UC_225_2.uC_225_Item1.txt_Truong_17.Text = FormatCurency(data[5].Truong_17);
                        this.UC_225_2.uC_225_Item1.txt_Truong_18.Text = FormatCurency(data[5].Truong_18);
                        this.UC_225_2.uC_225_Item1.txt_Truong_19.EditValue = data[5].Truong_19;
                        this.UC_225_2.uC_225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                        this.UC_225_2.uC_225_Item1.txt_Truong_21.Text = FormatCurency(data[5].Truong_21);
                        this.UC_225_2.uC_225_Item1.txt_Truong_22.Text = FormatCurency(data[5].Truong_22);
                        this.UC_225_2.uC_225_Item1.txt_Truong_23.Text = FormatCurency(data[5].Truong_23);
                        this.UC_225_2.uC_225_Item1.txt_Truong_24.Text = FormatCurency(data[5].Truong_24);
                        this.UC_225_2.uC_225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                        this.UC_225_2.uC_225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                        this.UC_225_2.uC_225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                        this.UC_225_2.uC_225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                        this.UC_225_2.uC_225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                        this.UC_225_2.uC_225_Item2.txt_Truong_09.Text = FormatCurency(data[6].Truong_09);
                        this.UC_225_2.uC_225_Item2.txt_Truong_10.Text = FormatCurency(data[6].Truong_10);
                        this.UC_225_2.uC_225_Item2.txt_Truong_11.Text = FormatCurency(data[6].Truong_11);
                        this.UC_225_2.uC_225_Item2.txt_Truong_12.Text = FormatCurency(data[6].Truong_12);
                        this.UC_225_2.uC_225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                        this.UC_225_2.uC_225_Item2.txt_Truong_15.Text = FormatCurency(data[6].Truong_15);
                        this.UC_225_2.uC_225_Item2.txt_Truong_16.Text = FormatCurency(data[6].Truong_16);
                        this.UC_225_2.uC_225_Item2.txt_Truong_17.Text = FormatCurency(data[6].Truong_17);
                        this.UC_225_2.uC_225_Item2.txt_Truong_18.Text = FormatCurency(data[6].Truong_18);
                        this.UC_225_2.uC_225_Item2.txt_Truong_19.EditValue = data[6].Truong_19;
                        this.UC_225_2.uC_225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                        this.UC_225_2.uC_225_Item2.txt_Truong_21.Text = FormatCurency(data[6].Truong_21);
                        this.UC_225_2.uC_225_Item2.txt_Truong_22.Text = FormatCurency(data[6].Truong_22);
                        this.UC_225_2.uC_225_Item2.txt_Truong_23.Text = FormatCurency(data[6].Truong_23);
                        this.UC_225_2.uC_225_Item2.txt_Truong_24.Text = FormatCurency(data[6].Truong_24);
                        this.UC_225_2.uC_225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                        this.UC_225_2.uC_225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                        this.UC_225_2.uC_225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                        this.UC_225_2.uC_225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                        this.UC_225_2.uC_225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                        this.UC_225_2.uC_225_Item3.txt_Truong_09.Text = FormatCurency(data[7].Truong_09);
                        this.UC_225_2.uC_225_Item3.txt_Truong_10.Text = FormatCurency(data[7].Truong_10);
                        this.UC_225_2.uC_225_Item3.txt_Truong_11.Text = FormatCurency(data[7].Truong_11);
                        this.UC_225_2.uC_225_Item3.txt_Truong_12.Text = FormatCurency(data[7].Truong_12);
                        this.UC_225_2.uC_225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                        this.UC_225_2.uC_225_Item3.txt_Truong_15.Text = FormatCurency(data[7].Truong_15);
                        this.UC_225_2.uC_225_Item3.txt_Truong_16.Text = FormatCurency(data[7].Truong_16);
                        this.UC_225_2.uC_225_Item3.txt_Truong_17.Text = FormatCurency(data[7].Truong_17);
                        this.UC_225_2.uC_225_Item3.txt_Truong_18.Text = FormatCurency(data[7].Truong_18);
                        this.UC_225_2.uC_225_Item3.txt_Truong_19.EditValue = data[7].Truong_19;
                        this.UC_225_2.uC_225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                        this.UC_225_2.uC_225_Item3.txt_Truong_21.Text = FormatCurency(data[7].Truong_21);
                        this.UC_225_2.uC_225_Item3.txt_Truong_22.Text = FormatCurency(data[7].Truong_22);
                        this.UC_225_2.uC_225_Item3.txt_Truong_23.Text = FormatCurency(data[7].Truong_23);
                        this.UC_225_2.uC_225_Item3.txt_Truong_24.Text = FormatCurency(data[7].Truong_24);
                        this.UC_225_2.uC_225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                        this.UC_225_2.uC_225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                        this.UC_225_2.uC_225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                        this.UC_225_2.uC_225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                        this.UC_225_2.uC_225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                        this.UC_225_2.uC_225_Item4.txt_Truong_09.Text = FormatCurency(data[8].Truong_09);
                        this.UC_225_2.uC_225_Item4.txt_Truong_10.Text = FormatCurency(data[8].Truong_10);
                        this.UC_225_2.uC_225_Item4.txt_Truong_11.Text = FormatCurency(data[8].Truong_11);
                        this.UC_225_2.uC_225_Item4.txt_Truong_12.Text = FormatCurency(data[8].Truong_12);
                        this.UC_225_2.uC_225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                        this.UC_225_2.uC_225_Item4.txt_Truong_15.Text = FormatCurency(data[8].Truong_15);
                        this.UC_225_2.uC_225_Item4.txt_Truong_16.Text = FormatCurency(data[8].Truong_16);
                        this.UC_225_2.uC_225_Item4.txt_Truong_17.Text = FormatCurency(data[8].Truong_17);
                        this.UC_225_2.uC_225_Item4.txt_Truong_18.Text = FormatCurency(data[8].Truong_18);
                        this.UC_225_2.uC_225_Item4.txt_Truong_19.EditValue = data[8].Truong_19;
                        this.UC_225_2.uC_225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                        this.UC_225_2.uC_225_Item4.txt_Truong_21.Text = FormatCurency(data[8].Truong_21);
                        this.UC_225_2.uC_225_Item4.txt_Truong_22.Text = FormatCurency(data[8].Truong_22);
                        this.UC_225_2.uC_225_Item4.txt_Truong_23.Text = FormatCurency(data[8].Truong_23);
                        this.UC_225_2.uC_225_Item4.txt_Truong_24.Text = FormatCurency(data[8].Truong_24);
                        this.UC_225_2.uC_225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                        this.UC_225_2.uC_225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                        this.UC_225_2.uC_225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                        this.UC_225_2.uC_225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                        this.UC_225_2.uC_225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                        this.UC_225_2.uC_225_Item5.txt_Truong_09.Text = FormatCurency(data[9].Truong_09);
                        this.UC_225_2.uC_225_Item5.txt_Truong_10.Text = FormatCurency(data[9].Truong_10);
                        this.UC_225_2.uC_225_Item5.txt_Truong_11.Text = FormatCurency(data[9].Truong_11);
                        this.UC_225_2.uC_225_Item5.txt_Truong_12.Text = FormatCurency(data[9].Truong_12);
                        this.UC_225_2.uC_225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                        this.UC_225_2.uC_225_Item5.txt_Truong_15.Text = FormatCurency(data[9].Truong_15);
                        this.UC_225_2.uC_225_Item5.txt_Truong_16.Text = FormatCurency(data[9].Truong_16);
                        this.UC_225_2.uC_225_Item5.txt_Truong_17.Text = FormatCurency(data[9].Truong_17);
                        this.UC_225_2.uC_225_Item5.txt_Truong_18.Text = FormatCurency(data[9].Truong_18);
                        this.UC_225_2.uC_225_Item5.txt_Truong_19.EditValue = data[9].Truong_19;
                        this.UC_225_2.uC_225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                        this.UC_225_2.uC_225_Item5.txt_Truong_21.Text = FormatCurency(data[9].Truong_21);
                        this.UC_225_2.uC_225_Item5.txt_Truong_22.Text = FormatCurency(data[9].Truong_22);
                        this.UC_225_2.uC_225_Item5.txt_Truong_23.Text = FormatCurency(data[9].Truong_23);
                        this.UC_225_2.uC_225_Item5.txt_Truong_24.Text = FormatCurency(data[9].Truong_24);
                        this.UC_225_2.txt_Truong_Flag.Text = data[8].Truong_Flag;
                    }
                    else
                    {
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_06.Text = data[5].Truong_06;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_07.Text = FormatCurency(data[5].Truong_07);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_09.Text = FormatCurency(data[5].Truong_09);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_10.Text = FormatCurency(data[5].Truong_10);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_11.Text = FormatCurency(data[5].Truong_11);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_12.Text = FormatCurency(data[5].Truong_12);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_13.Text = data[5].Truong_13;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_15.Text = FormatCurency(data[5].Truong_15);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_16.Text = FormatCurency(data[5].Truong_16);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_17.Text = FormatCurency(data[5].Truong_17);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_18.Text = FormatCurency(data[5].Truong_18);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_19.EditValue = data[5].Truong_19;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_21.Text = FormatCurency(data[5].Truong_21);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_22.Text = FormatCurency(data[5].Truong_22);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_23.Text = FormatCurency(data[5].Truong_23);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_24.Text = FormatCurency(data[5].Truong_24);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_25.Text = data[5].Truong_25;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_26.Text = data[5].Truong_26;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_06.Text = data[6].Truong_06;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_07.Text = FormatCurency(data[6].Truong_07);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_09.Text = FormatCurency(data[6].Truong_09);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_10.Text = FormatCurency(data[6].Truong_10);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_11.Text = FormatCurency(data[6].Truong_11);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_12.Text = FormatCurency(data[6].Truong_12);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_13.Text = data[6].Truong_13;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_15.Text = FormatCurency(data[6].Truong_15);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_16.Text = FormatCurency(data[6].Truong_16);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_17.Text = FormatCurency(data[6].Truong_17);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_18.Text = FormatCurency(data[6].Truong_18);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_19.EditValue = data[6].Truong_19;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_21.Text = FormatCurency(data[6].Truong_21);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_22.Text = FormatCurency(data[6].Truong_22);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_23.Text = FormatCurency(data[6].Truong_23);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_24.Text = FormatCurency(data[6].Truong_24);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_25.Text = data[6].Truong_25;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_26.Text = data[6].Truong_26;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_06.Text = data[7].Truong_06;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_07.Text = FormatCurency(data[7].Truong_07);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_09.Text = FormatCurency(data[7].Truong_09);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_10.Text = FormatCurency(data[7].Truong_10);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_11.Text = FormatCurency(data[7].Truong_11);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_12.Text = FormatCurency(data[7].Truong_12);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_13.Text = data[7].Truong_13;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_15.Text = FormatCurency(data[7].Truong_15);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_16.Text = FormatCurency(data[7].Truong_16);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_17.Text = FormatCurency(data[7].Truong_17);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_18.Text = FormatCurency(data[7].Truong_18);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_19.EditValue = data[7].Truong_19;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_21.Text = FormatCurency(data[7].Truong_21);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_22.Text = FormatCurency(data[7].Truong_22);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_23.Text = FormatCurency(data[7].Truong_23);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_24.Text = FormatCurency(data[7].Truong_24);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_25.Text = data[7].Truong_25;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_26.Text = data[7].Truong_26;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_06.Text = data[8].Truong_06;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_07.Text = FormatCurency(data[8].Truong_07);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_09.Text = FormatCurency(data[8].Truong_09);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_10.Text = FormatCurency(data[8].Truong_10);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_11.Text = FormatCurency(data[8].Truong_11);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_12.Text = FormatCurency(data[8].Truong_12);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_13.Text = data[8].Truong_13;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_15.Text = FormatCurency(data[8].Truong_15);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_16.Text = FormatCurency(data[8].Truong_16);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_17.Text = FormatCurency(data[8].Truong_17);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_18.Text = FormatCurency(data[8].Truong_18);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_19.EditValue = data[8].Truong_19;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_21.Text = FormatCurency(data[8].Truong_21);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_22.Text = FormatCurency(data[8].Truong_22);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_23.Text = FormatCurency(data[8].Truong_23);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_24.Text = FormatCurency(data[8].Truong_24);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_25.Text = data[8].Truong_25;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_26.Text = data[8].Truong_26;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_06.Text = data[9].Truong_06;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_07.Text = FormatCurency(data[9].Truong_07);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_09.Text = FormatCurency(data[9].Truong_09);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_10.Text = FormatCurency(data[9].Truong_10);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_11.Text = FormatCurency(data[9].Truong_11);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_12.Text = FormatCurency(data[9].Truong_12);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_13.Text = data[9].Truong_13;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_15.Text = FormatCurency(data[9].Truong_15);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_16.Text = FormatCurency(data[9].Truong_16);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_17.Text = FormatCurency(data[9].Truong_17);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_18.Text = FormatCurency(data[9].Truong_18);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_19.EditValue = data[9].Truong_19;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_21.Text = FormatCurency(data[9].Truong_21);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_22.Text = FormatCurency(data[9].Truong_22);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_23.Text = FormatCurency(data[9].Truong_23);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_24.Text = FormatCurency(data[9].Truong_24);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_25.Text = data[9].Truong_25;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_26.Text = data[9].Truong_26;
                        this.UC_2225_2.txt_Truong_Flag.Text = data[8].Truong_Flag;
                    }
                    //lb_User1.Text = data[0].UserName;
                    //lb_User2.Text = data[1].UserName;
                    ////if (data[0].True.Value)
                    ////    lb_User1.ForeColor = Color.Red;
                    ////if (data[1].True.Value)
                    ////    lb_User2.ForeColor = Color.Red;
                }
                else if (result)
                {
                    this.DataUser1.Add(data[5]);
                    this.DataUser1.Add(data[6]);
                    this.DataUser1.Add(data[7]);
                    this.DataUser1.Add(data[8]);
                    this.DataUser1.Add(data[9]);
                    this.DataUser2.Add(data[0]);
                    this.DataUser2.Add(data[1]);
                    this.DataUser2.Add(data[2]);
                    this.DataUser2.Add(data[3]);
                    this.DataUser2.Add(data[4]);
                    this.lb_User1.Text = data[5].UserName;
                    this.lb_User2.Text = data[0].UserName;
                    this.txt_Truong_01_User1.Text = data[5].Truong_01;
                    if (data[5].Truong_01 == "225")
                    {
                        this.UC_225_1.uC_225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                        this.UC_225_1.uC_225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                        this.UC_225_1.uC_225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                        this.UC_225_1.uC_225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                        this.UC_225_1.uC_225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                        this.UC_225_1.uC_225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                        this.UC_225_1.uC_225_Item1.txt_Truong_09.Text = FormatCurency(data[5].Truong_09);
                        this.UC_225_1.uC_225_Item1.txt_Truong_10.Text = FormatCurency(data[5].Truong_10);
                        this.UC_225_1.uC_225_Item1.txt_Truong_11.Text = FormatCurency(data[5].Truong_11);
                        this.UC_225_1.uC_225_Item1.txt_Truong_12.Text = FormatCurency(data[5].Truong_12);
                        this.UC_225_1.uC_225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                        this.UC_225_1.uC_225_Item1.txt_Truong_15.Text = FormatCurency(data[5].Truong_15);
                        this.UC_225_1.uC_225_Item1.txt_Truong_16.Text = FormatCurency(data[5].Truong_16);
                        this.UC_225_1.uC_225_Item1.txt_Truong_17.Text = FormatCurency(data[5].Truong_17);
                        this.UC_225_1.uC_225_Item1.txt_Truong_18.Text = FormatCurency(data[5].Truong_18);
                        this.UC_225_1.uC_225_Item1.txt_Truong_19.EditValue = data[5].Truong_19;
                        this.UC_225_1.uC_225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                        this.UC_225_1.uC_225_Item1.txt_Truong_21.Text = FormatCurency(data[5].Truong_21);
                        this.UC_225_1.uC_225_Item1.txt_Truong_22.Text = FormatCurency(data[5].Truong_22);
                        this.UC_225_1.uC_225_Item1.txt_Truong_23.Text = FormatCurency(data[5].Truong_23);
                        this.UC_225_1.uC_225_Item1.txt_Truong_24.Text = FormatCurency(data[5].Truong_24);
                        this.UC_225_1.uC_225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                        this.UC_225_1.uC_225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                        this.UC_225_1.uC_225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                        this.UC_225_1.uC_225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                        this.UC_225_1.uC_225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                        this.UC_225_1.uC_225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                        this.UC_225_1.uC_225_Item2.txt_Truong_09.Text = FormatCurency(data[6].Truong_09);
                        this.UC_225_1.uC_225_Item2.txt_Truong_10.Text = FormatCurency(data[6].Truong_10);
                        this.UC_225_1.uC_225_Item2.txt_Truong_11.Text = FormatCurency(data[6].Truong_11);
                        this.UC_225_1.uC_225_Item2.txt_Truong_12.Text = FormatCurency(data[6].Truong_12);
                        this.UC_225_1.uC_225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                        this.UC_225_1.uC_225_Item2.txt_Truong_15.Text = FormatCurency(data[6].Truong_15);
                        this.UC_225_1.uC_225_Item2.txt_Truong_16.Text = FormatCurency(data[6].Truong_16);
                        this.UC_225_1.uC_225_Item2.txt_Truong_17.Text = FormatCurency(data[6].Truong_17);
                        this.UC_225_1.uC_225_Item2.txt_Truong_18.Text = FormatCurency(data[6].Truong_18);
                        this.UC_225_1.uC_225_Item2.txt_Truong_19.EditValue = data[6].Truong_19;
                        this.UC_225_1.uC_225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                        this.UC_225_1.uC_225_Item2.txt_Truong_21.Text = FormatCurency(data[6].Truong_21);
                        this.UC_225_1.uC_225_Item2.txt_Truong_22.Text = FormatCurency(data[6].Truong_22);
                        this.UC_225_1.uC_225_Item2.txt_Truong_23.Text = FormatCurency(data[6].Truong_23);
                        this.UC_225_1.uC_225_Item2.txt_Truong_24.Text = FormatCurency(data[6].Truong_24);
                        this.UC_225_1.uC_225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                        this.UC_225_1.uC_225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                        this.UC_225_1.uC_225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                        this.UC_225_1.uC_225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                        this.UC_225_1.uC_225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                        this.UC_225_1.uC_225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                        this.UC_225_1.uC_225_Item3.txt_Truong_09.Text = FormatCurency(data[7].Truong_09);
                        this.UC_225_1.uC_225_Item3.txt_Truong_10.Text = FormatCurency(data[7].Truong_10);
                        this.UC_225_1.uC_225_Item3.txt_Truong_11.Text = FormatCurency(data[7].Truong_11);
                        this.UC_225_1.uC_225_Item3.txt_Truong_12.Text = FormatCurency(data[7].Truong_12);
                        this.UC_225_1.uC_225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                        this.UC_225_1.uC_225_Item3.txt_Truong_15.Text = FormatCurency(data[7].Truong_15);
                        this.UC_225_1.uC_225_Item3.txt_Truong_16.Text = FormatCurency(data[7].Truong_16);
                        this.UC_225_1.uC_225_Item3.txt_Truong_17.Text = FormatCurency(data[7].Truong_17);
                        this.UC_225_1.uC_225_Item3.txt_Truong_18.Text = FormatCurency(data[7].Truong_18);
                        this.UC_225_1.uC_225_Item3.txt_Truong_19.EditValue = data[7].Truong_19;
                        this.UC_225_1.uC_225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                        this.UC_225_1.uC_225_Item3.txt_Truong_21.Text = FormatCurency(data[7].Truong_21);
                        this.UC_225_1.uC_225_Item3.txt_Truong_22.Text = FormatCurency(data[7].Truong_22);
                        this.UC_225_1.uC_225_Item3.txt_Truong_23.Text = FormatCurency(data[7].Truong_23);
                        this.UC_225_1.uC_225_Item3.txt_Truong_24.Text = FormatCurency(data[7].Truong_24);
                        this.UC_225_1.uC_225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                        this.UC_225_1.uC_225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                        this.UC_225_1.uC_225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                        this.UC_225_1.uC_225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                        this.UC_225_1.uC_225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                        this.UC_225_1.uC_225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                        this.UC_225_1.uC_225_Item4.txt_Truong_09.Text = FormatCurency(data[8].Truong_09);
                        this.UC_225_1.uC_225_Item4.txt_Truong_10.Text = FormatCurency(data[8].Truong_10);
                        this.UC_225_1.uC_225_Item4.txt_Truong_11.Text = FormatCurency(data[8].Truong_11);
                        this.UC_225_1.uC_225_Item4.txt_Truong_12.Text = FormatCurency(data[8].Truong_12);
                        this.UC_225_1.uC_225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                        this.UC_225_1.uC_225_Item4.txt_Truong_15.Text = FormatCurency(data[8].Truong_15);
                        this.UC_225_1.uC_225_Item4.txt_Truong_16.Text = FormatCurency(data[8].Truong_16);
                        this.UC_225_1.uC_225_Item4.txt_Truong_17.Text = FormatCurency(data[8].Truong_17);
                        this.UC_225_1.uC_225_Item4.txt_Truong_18.Text = FormatCurency(data[8].Truong_18);
                        this.UC_225_1.uC_225_Item4.txt_Truong_19.EditValue = data[8].Truong_19;
                        this.UC_225_1.uC_225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                        this.UC_225_1.uC_225_Item4.txt_Truong_21.Text = FormatCurency(data[8].Truong_21);
                        this.UC_225_1.uC_225_Item4.txt_Truong_22.Text = FormatCurency(data[8].Truong_22);
                        this.UC_225_1.uC_225_Item4.txt_Truong_23.Text = FormatCurency(data[8].Truong_23);
                        this.UC_225_1.uC_225_Item4.txt_Truong_24.Text = FormatCurency(data[8].Truong_24);
                        this.UC_225_1.uC_225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                        this.UC_225_1.uC_225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                        this.UC_225_1.uC_225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                        this.UC_225_1.uC_225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                        this.UC_225_1.uC_225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                        this.UC_225_1.uC_225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                        this.UC_225_1.uC_225_Item5.txt_Truong_09.Text = FormatCurency(data[9].Truong_09);
                        this.UC_225_1.uC_225_Item5.txt_Truong_10.Text = FormatCurency(data[9].Truong_10);
                        this.UC_225_1.uC_225_Item5.txt_Truong_11.Text = FormatCurency(data[9].Truong_11);
                        this.UC_225_1.uC_225_Item5.txt_Truong_12.Text = FormatCurency(data[9].Truong_12);
                        this.UC_225_1.uC_225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                        this.UC_225_1.uC_225_Item5.txt_Truong_15.Text = FormatCurency(data[9].Truong_15);
                        this.UC_225_1.uC_225_Item5.txt_Truong_16.Text = FormatCurency(data[9].Truong_16);
                        this.UC_225_1.uC_225_Item5.txt_Truong_17.Text = FormatCurency(data[9].Truong_17);
                        this.UC_225_1.uC_225_Item5.txt_Truong_18.Text = FormatCurency(data[9].Truong_18);
                        this.UC_225_1.uC_225_Item5.txt_Truong_19.EditValue = data[9].Truong_19;
                        this.UC_225_1.uC_225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                        this.UC_225_1.uC_225_Item5.txt_Truong_21.Text = FormatCurency(data[9].Truong_21);
                        this.UC_225_1.uC_225_Item5.txt_Truong_22.Text = FormatCurency(data[9].Truong_22);
                        this.UC_225_1.uC_225_Item5.txt_Truong_23.Text = FormatCurency(data[9].Truong_23);
                        this.UC_225_1.uC_225_Item5.txt_Truong_24.Text = FormatCurency(data[9].Truong_24);
                        this.UC_225_1.txt_Truong_Flag.Text = data[8].Truong_Flag;
                    }
                    else
                    {
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_06.Text = data[5].Truong_06;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_07.Text = FormatCurency(data[5].Truong_07);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_09.Text = FormatCurency(data[5].Truong_09);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_10.Text = FormatCurency(data[5].Truong_10);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_11.Text = FormatCurency(data[5].Truong_11);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_12.Text = FormatCurency(data[5].Truong_12);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_13.Text = data[5].Truong_13;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_15.Text = FormatCurency(data[5].Truong_15);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_16.Text = FormatCurency(data[5].Truong_16);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_17.Text = FormatCurency(data[5].Truong_17);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_18.Text = FormatCurency(data[5].Truong_18);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_19.EditValue = data[5].Truong_19;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_21.Text = FormatCurency(data[5].Truong_21);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_22.Text = FormatCurency(data[5].Truong_22);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_23.Text = FormatCurency(data[5].Truong_23);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_24.Text = FormatCurency(data[5].Truong_24);
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_25.Text = data[5].Truong_25;
                        this.UC_2225_1.uC_2225_Item1.txt_Truong_26.Text = data[5].Truong_26;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_06.Text = data[6].Truong_06;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_07.Text = FormatCurency(data[6].Truong_07);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_09.Text = FormatCurency(data[6].Truong_09);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_10.Text = FormatCurency(data[6].Truong_10);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_11.Text = FormatCurency(data[6].Truong_11);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_12.Text = FormatCurency(data[6].Truong_12);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_13.Text = data[6].Truong_13;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_15.Text = FormatCurency(data[6].Truong_15);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_16.Text = FormatCurency(data[6].Truong_16);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_17.Text = FormatCurency(data[6].Truong_17);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_18.Text = FormatCurency(data[6].Truong_18);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_19.EditValue = data[6].Truong_19;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_21.Text = FormatCurency(data[6].Truong_21);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_22.Text = FormatCurency(data[6].Truong_22);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_23.Text = FormatCurency(data[6].Truong_23);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_24.Text = FormatCurency(data[6].Truong_24);
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_25.Text = data[6].Truong_25;
                        this.UC_2225_1.uC_2225_Item2.txt_Truong_26.Text = data[6].Truong_26;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_06.Text = data[7].Truong_06;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_07.Text = FormatCurency(data[7].Truong_07);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_09.Text = FormatCurency(data[7].Truong_09);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_10.Text = FormatCurency(data[7].Truong_10);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_11.Text = FormatCurency(data[7].Truong_11);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_12.Text = FormatCurency(data[7].Truong_12);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_13.Text = data[7].Truong_13;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_15.Text = FormatCurency(data[7].Truong_15);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_16.Text = FormatCurency(data[7].Truong_16);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_17.Text = FormatCurency(data[7].Truong_17);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_18.Text = FormatCurency(data[7].Truong_18);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_19.EditValue = data[7].Truong_19;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_21.Text = FormatCurency(data[7].Truong_21);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_22.Text = FormatCurency(data[7].Truong_22);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_23.Text = FormatCurency(data[7].Truong_23);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_24.Text = FormatCurency(data[7].Truong_24);
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_25.Text = data[7].Truong_25;
                        this.UC_2225_1.uC_2225_Item3.txt_Truong_26.Text = data[7].Truong_26;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_06.Text = data[8].Truong_06;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_07.Text = FormatCurency(data[8].Truong_07);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_09.Text = FormatCurency(data[8].Truong_09);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_10.Text = FormatCurency(data[8].Truong_10);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_11.Text = FormatCurency(data[8].Truong_11);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_12.Text = FormatCurency(data[8].Truong_12);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_13.Text = data[8].Truong_13;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_15.Text = FormatCurency(data[8].Truong_15);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_16.Text = FormatCurency(data[8].Truong_16);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_17.Text = FormatCurency(data[8].Truong_17);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_18.Text = FormatCurency(data[8].Truong_18);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_19.EditValue = data[8].Truong_19;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_21.Text = FormatCurency(data[8].Truong_21);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_22.Text = FormatCurency(data[8].Truong_22);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_23.Text = FormatCurency(data[8].Truong_23);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_24.Text = FormatCurency(data[8].Truong_24);
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_25.Text = data[8].Truong_25;
                        this.UC_2225_1.uC_2225_Item4.txt_Truong_26.Text = data[8].Truong_26;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_06.Text = data[9].Truong_06;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_07.Text = FormatCurency(data[9].Truong_07);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_09.Text = FormatCurency(data[9].Truong_09);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_10.Text = FormatCurency(data[9].Truong_10);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_11.Text = FormatCurency(data[9].Truong_11);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_12.Text = FormatCurency(data[9].Truong_12);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_13.Text = data[9].Truong_13;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_15.Text = FormatCurency(data[9].Truong_15);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_16.Text = FormatCurency(data[9].Truong_16);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_17.Text = FormatCurency(data[9].Truong_17);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_18.Text = FormatCurency(data[9].Truong_18);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_19.EditValue = data[9].Truong_19;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_21.Text = FormatCurency(data[9].Truong_21);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_22.Text = FormatCurency(data[9].Truong_22);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_23.Text = FormatCurency(data[9].Truong_23);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_24.Text = FormatCurency(data[9].Truong_24);
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_25.Text = data[9].Truong_25;
                        this.UC_2225_1.uC_2225_Item5.txt_Truong_26.Text = data[9].Truong_26;
                        this.UC_2225_1.txt_Truong_Flag.Text = data[8].Truong_Flag;
                    }
                    this.txt_Truong_01_User2.Text = data[0].Truong_01;
                    if (data[0].Truong_01 == "225")
                    {
                        this.UC_225_2.uC_225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                        this.UC_225_2.uC_225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                        this.UC_225_2.uC_225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                        this.UC_225_2.uC_225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                        this.UC_225_2.uC_225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                        this.UC_225_2.uC_225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                        this.UC_225_2.uC_225_Item1.txt_Truong_09.Text = FormatCurency(data[0].Truong_09);
                        this.UC_225_2.uC_225_Item1.txt_Truong_10.Text = FormatCurency(data[0].Truong_10);
                        this.UC_225_2.uC_225_Item1.txt_Truong_11.Text = FormatCurency(data[0].Truong_11);
                        this.UC_225_2.uC_225_Item1.txt_Truong_12.Text = FormatCurency(data[0].Truong_12);
                        this.UC_225_2.uC_225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                        this.UC_225_2.uC_225_Item1.txt_Truong_15.Text = FormatCurency(data[0].Truong_15);
                        this.UC_225_2.uC_225_Item1.txt_Truong_16.Text = FormatCurency(data[0].Truong_16);
                        this.UC_225_2.uC_225_Item1.txt_Truong_17.Text = FormatCurency(data[0].Truong_17);
                        this.UC_225_2.uC_225_Item1.txt_Truong_18.Text = FormatCurency(data[0].Truong_18);
                        this.UC_225_2.uC_225_Item1.txt_Truong_19.EditValue = data[0].Truong_19;
                        this.UC_225_2.uC_225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                        this.UC_225_2.uC_225_Item1.txt_Truong_21.Text = FormatCurency(data[0].Truong_21);
                        this.UC_225_2.uC_225_Item1.txt_Truong_22.Text = FormatCurency(data[0].Truong_22);
                        this.UC_225_2.uC_225_Item1.txt_Truong_23.Text = FormatCurency(data[0].Truong_23);
                        this.UC_225_2.uC_225_Item1.txt_Truong_24.Text = FormatCurency(data[0].Truong_24);
                        this.UC_225_2.uC_225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                        this.UC_225_2.uC_225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                        this.UC_225_2.uC_225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                        this.UC_225_2.uC_225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                        this.UC_225_2.uC_225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                        this.UC_225_2.uC_225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                        this.UC_225_2.uC_225_Item2.txt_Truong_09.Text = FormatCurency(data[1].Truong_09);
                        this.UC_225_2.uC_225_Item2.txt_Truong_10.Text = FormatCurency(data[1].Truong_10);
                        this.UC_225_2.uC_225_Item2.txt_Truong_11.Text = FormatCurency(data[1].Truong_11);
                        this.UC_225_2.uC_225_Item2.txt_Truong_12.Text = FormatCurency(data[1].Truong_12);
                        this.UC_225_2.uC_225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                        this.UC_225_2.uC_225_Item2.txt_Truong_15.Text = FormatCurency(data[1].Truong_15);
                        this.UC_225_2.uC_225_Item2.txt_Truong_16.Text = FormatCurency(data[1].Truong_16);
                        this.UC_225_2.uC_225_Item2.txt_Truong_17.Text = FormatCurency(data[1].Truong_17);
                        this.UC_225_2.uC_225_Item2.txt_Truong_18.Text = FormatCurency(data[1].Truong_18);
                        this.UC_225_2.uC_225_Item2.txt_Truong_19.EditValue = data[1].Truong_19;
                        this.UC_225_2.uC_225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                        this.UC_225_2.uC_225_Item2.txt_Truong_21.Text = FormatCurency(data[1].Truong_21);
                        this.UC_225_2.uC_225_Item2.txt_Truong_22.Text = FormatCurency(data[1].Truong_22);
                        this.UC_225_2.uC_225_Item2.txt_Truong_23.Text = FormatCurency(data[1].Truong_23);
                        this.UC_225_2.uC_225_Item2.txt_Truong_24.Text = FormatCurency(data[1].Truong_24);
                        this.UC_225_2.uC_225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                        this.UC_225_2.uC_225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                        this.UC_225_2.uC_225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                        this.UC_225_2.uC_225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                        this.UC_225_2.uC_225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                        this.UC_225_2.uC_225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                        this.UC_225_2.uC_225_Item3.txt_Truong_09.Text = FormatCurency(data[2].Truong_09);
                        this.UC_225_2.uC_225_Item3.txt_Truong_10.Text = FormatCurency(data[2].Truong_10);
                        this.UC_225_2.uC_225_Item3.txt_Truong_11.Text = FormatCurency(data[2].Truong_11);
                        this.UC_225_2.uC_225_Item3.txt_Truong_12.Text = FormatCurency(data[2].Truong_12);
                        this.UC_225_2.uC_225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                        this.UC_225_2.uC_225_Item3.txt_Truong_15.Text = FormatCurency(data[2].Truong_15);
                        this.UC_225_2.uC_225_Item3.txt_Truong_16.Text = FormatCurency(data[2].Truong_16);
                        this.UC_225_2.uC_225_Item3.txt_Truong_17.Text = FormatCurency(data[2].Truong_17);
                        this.UC_225_2.uC_225_Item3.txt_Truong_18.Text = FormatCurency(data[2].Truong_18);
                        this.UC_225_2.uC_225_Item3.txt_Truong_19.EditValue = data[2].Truong_19;
                        this.UC_225_2.uC_225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                        this.UC_225_2.uC_225_Item3.txt_Truong_21.Text = FormatCurency(data[2].Truong_21);
                        this.UC_225_2.uC_225_Item3.txt_Truong_22.Text = FormatCurency(data[2].Truong_22);
                        this.UC_225_2.uC_225_Item3.txt_Truong_23.Text = FormatCurency(data[2].Truong_23);
                        this.UC_225_2.uC_225_Item3.txt_Truong_24.Text = FormatCurency(data[2].Truong_24);
                        this.UC_225_2.uC_225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                        this.UC_225_2.uC_225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                        this.UC_225_2.uC_225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                        this.UC_225_2.uC_225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                        this.UC_225_2.uC_225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                        this.UC_225_2.uC_225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                        this.UC_225_2.uC_225_Item4.txt_Truong_09.Text = FormatCurency(data[3].Truong_09);
                        this.UC_225_2.uC_225_Item4.txt_Truong_10.Text = FormatCurency(data[3].Truong_10);
                        this.UC_225_2.uC_225_Item4.txt_Truong_11.Text = FormatCurency(data[3].Truong_11);
                        this.UC_225_2.uC_225_Item4.txt_Truong_12.Text = FormatCurency(data[3].Truong_12);
                        this.UC_225_2.uC_225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                        this.UC_225_2.uC_225_Item4.txt_Truong_15.Text = FormatCurency(data[3].Truong_15);
                        this.UC_225_2.uC_225_Item4.txt_Truong_16.Text = FormatCurency(data[3].Truong_16);
                        this.UC_225_2.uC_225_Item4.txt_Truong_17.Text = FormatCurency(data[3].Truong_17);
                        this.UC_225_2.uC_225_Item4.txt_Truong_18.Text = FormatCurency(data[3].Truong_18);
                        this.UC_225_2.uC_225_Item4.txt_Truong_19.EditValue = data[3].Truong_19;
                        this.UC_225_2.uC_225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                        this.UC_225_2.uC_225_Item4.txt_Truong_21.Text = FormatCurency(data[3].Truong_21);
                        this.UC_225_2.uC_225_Item4.txt_Truong_22.Text = FormatCurency(data[3].Truong_22);
                        this.UC_225_2.uC_225_Item4.txt_Truong_23.Text = FormatCurency(data[3].Truong_23);
                        this.UC_225_2.uC_225_Item4.txt_Truong_24.Text = FormatCurency(data[3].Truong_24);
                        this.UC_225_2.uC_225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                        this.UC_225_2.uC_225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                        this.UC_225_2.uC_225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                        this.UC_225_2.uC_225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                        this.UC_225_2.uC_225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                        this.UC_225_2.uC_225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                        this.UC_225_2.uC_225_Item5.txt_Truong_09.Text = FormatCurency(data[4].Truong_09);
                        this.UC_225_2.uC_225_Item5.txt_Truong_10.Text = FormatCurency(data[4].Truong_10);
                        this.UC_225_2.uC_225_Item5.txt_Truong_11.Text = FormatCurency(data[4].Truong_11);
                        this.UC_225_2.uC_225_Item5.txt_Truong_12.Text = FormatCurency(data[4].Truong_12);
                        this.UC_225_2.uC_225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                        this.UC_225_2.uC_225_Item5.txt_Truong_15.Text = FormatCurency(data[4].Truong_15);
                        this.UC_225_2.uC_225_Item5.txt_Truong_16.Text = FormatCurency(data[4].Truong_16);
                        this.UC_225_2.uC_225_Item5.txt_Truong_17.Text = FormatCurency(data[4].Truong_17);
                        this.UC_225_2.uC_225_Item5.txt_Truong_18.Text = FormatCurency(data[4].Truong_18);
                        this.UC_225_2.uC_225_Item5.txt_Truong_19.EditValue = data[4].Truong_19;
                        this.UC_225_2.uC_225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                        this.UC_225_2.uC_225_Item5.txt_Truong_21.Text = FormatCurency(data[4].Truong_21);
                        this.UC_225_2.uC_225_Item5.txt_Truong_22.Text = FormatCurency(data[4].Truong_22);
                        this.UC_225_2.uC_225_Item5.txt_Truong_23.Text = FormatCurency(data[4].Truong_23);
                        this.UC_225_2.uC_225_Item5.txt_Truong_24.Text = FormatCurency(data[4].Truong_24);
                        this.UC_225_2.txt_Truong_Flag.Text = data[3].Truong_Flag;
                    }
                    else
                    {
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_06.Text = data[0].Truong_06;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_07.Text = FormatCurency(data[0].Truong_07);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_09.Text = FormatCurency(data[0].Truong_09);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_10.Text = FormatCurency(data[0].Truong_10);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_11.Text = FormatCurency(data[0].Truong_11);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_12.Text = FormatCurency(data[0].Truong_12);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_13.Text = data[0].Truong_13;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_15.Text = FormatCurency(data[0].Truong_15);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_16.Text = FormatCurency(data[0].Truong_16);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_17.Text = FormatCurency(data[0].Truong_17);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_18.Text = FormatCurency(data[0].Truong_18);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_19.EditValue = data[0].Truong_19;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_21.Text = FormatCurency(data[0].Truong_21);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_22.Text = FormatCurency(data[0].Truong_22);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_23.Text = FormatCurency(data[0].Truong_23);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_24.Text = FormatCurency(data[0].Truong_24);
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_25.Text = data[0].Truong_25;
                        this.UC_2225_2.uC_2225_Item1.txt_Truong_26.Text = data[0].Truong_26;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_06.Text = data[1].Truong_06;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_07.Text = FormatCurency(data[1].Truong_07);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_09.Text = FormatCurency(data[1].Truong_09);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_10.Text = FormatCurency(data[1].Truong_10);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_11.Text = FormatCurency(data[1].Truong_11);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_12.Text = FormatCurency(data[1].Truong_12);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_13.Text = data[1].Truong_13;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_15.Text = FormatCurency(data[1].Truong_15);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_16.Text = FormatCurency(data[1].Truong_16);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_17.Text = FormatCurency(data[1].Truong_17);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_18.Text = FormatCurency(data[1].Truong_18);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_19.EditValue = data[1].Truong_19;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_21.Text = FormatCurency(data[1].Truong_21);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_22.Text = FormatCurency(data[1].Truong_22);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_23.Text = FormatCurency(data[1].Truong_23);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_24.Text = FormatCurency(data[1].Truong_24);
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_25.Text = data[1].Truong_25;
                        this.UC_2225_2.uC_2225_Item2.txt_Truong_26.Text = data[1].Truong_26;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_06.Text = data[2].Truong_06;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_07.Text = FormatCurency(data[2].Truong_07);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_09.Text = FormatCurency(data[2].Truong_09);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_10.Text = FormatCurency(data[2].Truong_10);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_11.Text = FormatCurency(data[2].Truong_11);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_12.Text = FormatCurency(data[2].Truong_12);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_13.Text = data[2].Truong_13;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_15.Text = FormatCurency(data[2].Truong_15);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_16.Text = FormatCurency(data[2].Truong_16);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_17.Text = FormatCurency(data[2].Truong_17);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_18.Text = FormatCurency(data[2].Truong_18);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_19.EditValue = data[2].Truong_19;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_21.Text = FormatCurency(data[2].Truong_21);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_22.Text = FormatCurency(data[2].Truong_22);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_23.Text = FormatCurency(data[2].Truong_23);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_24.Text = FormatCurency(data[2].Truong_24);
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_25.Text = data[2].Truong_25;
                        this.UC_2225_2.uC_2225_Item3.txt_Truong_26.Text = data[2].Truong_26;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_06.Text = data[3].Truong_06;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_07.Text = FormatCurency(data[3].Truong_07);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_09.Text = FormatCurency(data[3].Truong_09);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_10.Text = FormatCurency(data[3].Truong_10);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_11.Text = FormatCurency(data[3].Truong_11);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_12.Text = FormatCurency(data[3].Truong_12);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_13.Text = data[3].Truong_13;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_15.Text = FormatCurency(data[3].Truong_15);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_16.Text = FormatCurency(data[3].Truong_16);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_17.Text = FormatCurency(data[3].Truong_17);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_18.Text = FormatCurency(data[3].Truong_18);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_19.EditValue = data[3].Truong_19;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_21.Text = FormatCurency(data[3].Truong_21);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_22.Text = FormatCurency(data[3].Truong_22);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_23.Text = FormatCurency(data[3].Truong_23);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_24.Text = FormatCurency(data[3].Truong_24);
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_25.Text = data[3].Truong_25;
                        this.UC_2225_2.uC_2225_Item4.txt_Truong_26.Text = data[3].Truong_26;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_06.Text = data[4].Truong_06;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_07.Text = FormatCurency(data[4].Truong_07);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_09.Text = FormatCurency(data[4].Truong_09);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_10.Text = FormatCurency(data[4].Truong_10);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_11.Text = FormatCurency(data[4].Truong_11);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_12.Text = FormatCurency(data[4].Truong_12);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_13.Text = data[4].Truong_13;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_15.Text = FormatCurency(data[4].Truong_15);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_16.Text = FormatCurency(data[4].Truong_16);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_17.Text = FormatCurency(data[4].Truong_17);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_18.Text = FormatCurency(data[4].Truong_18);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_19.EditValue = data[4].Truong_19;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_21.Text = FormatCurency(data[4].Truong_21);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_22.Text = FormatCurency(data[4].Truong_22);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_23.Text = FormatCurency(data[4].Truong_23);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_24.Text = FormatCurency(data[4].Truong_24);
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_25.Text = data[4].Truong_25;
                        this.UC_2225_2.uC_2225_Item5.txt_Truong_26.Text = data[4].Truong_26;
                        this.UC_2225_2.txt_Truong_Flag.Text = data[3].Truong_Flag;
                    }
                    ////if (data[1].True.Value)
                    ////    lb_User1.ForeColor = Color.Red;
                    ////if (data[0].True.Value)
                    ////    lb_User2.ForeColor = Color.Red;
                }
                
                this.Compare_ComBoBox(this.txt_Truong_01_User1, this.txt_Truong_01_User2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_02, this.UC_225_2.uC_225_Item1.txt_Truong_02);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_03, this.UC_225_2.uC_225_Item1.txt_Truong_03);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_04_1, this.UC_225_2.uC_225_Item1.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_04_2, this.UC_225_2.uC_225_Item1.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_04_3, this.UC_225_2.uC_225_Item1.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_05_1, this.UC_225_2.uC_225_Item1.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_05_2, this.UC_225_2.uC_225_Item1.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_08, this.UC_225_2.uC_225_Item1.txt_Truong_08);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_09, this.UC_225_2.uC_225_Item1.txt_Truong_09);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_10, this.UC_225_2.uC_225_Item1.txt_Truong_10);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_11, this.UC_225_2.uC_225_Item1.txt_Truong_11);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_12, this.UC_225_2.uC_225_Item1.txt_Truong_12);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_14, this.UC_225_2.uC_225_Item1.txt_Truong_14);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_15, this.UC_225_2.uC_225_Item1.txt_Truong_15);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_16, this.UC_225_2.uC_225_Item1.txt_Truong_16);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_17, this.UC_225_2.uC_225_Item1.txt_Truong_17);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_18, this.UC_225_2.uC_225_Item1.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_225_1.uC_225_Item1.txt_Truong_19, this.UC_225_2.uC_225_Item1.txt_Truong_19);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_20, this.UC_225_2.uC_225_Item1.txt_Truong_20);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_21, this.UC_225_2.uC_225_Item1.txt_Truong_21);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_22, this.UC_225_2.uC_225_Item1.txt_Truong_22);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_23, this.UC_225_2.uC_225_Item1.txt_Truong_23);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item1.txt_Truong_24, this.UC_225_2.uC_225_Item1.txt_Truong_24);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_02, this.UC_225_2.uC_225_Item2.txt_Truong_02);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_03, this.UC_225_2.uC_225_Item2.txt_Truong_03);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_04_1, this.UC_225_2.uC_225_Item2.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_04_2, this.UC_225_2.uC_225_Item2.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_04_3, this.UC_225_2.uC_225_Item2.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_05_1, this.UC_225_2.uC_225_Item2.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_05_2, this.UC_225_2.uC_225_Item2.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_08, this.UC_225_2.uC_225_Item2.txt_Truong_08);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_09, this.UC_225_2.uC_225_Item2.txt_Truong_09);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_10, this.UC_225_2.uC_225_Item2.txt_Truong_10);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_11, this.UC_225_2.uC_225_Item2.txt_Truong_11);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_12, this.UC_225_2.uC_225_Item2.txt_Truong_12);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_14, this.UC_225_2.uC_225_Item2.txt_Truong_14);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_15, this.UC_225_2.uC_225_Item2.txt_Truong_15);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_16, this.UC_225_2.uC_225_Item2.txt_Truong_16);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_17, this.UC_225_2.uC_225_Item2.txt_Truong_17);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_18, this.UC_225_2.uC_225_Item2.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_225_1.uC_225_Item2.txt_Truong_19, this.UC_225_2.uC_225_Item2.txt_Truong_19);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_20, this.UC_225_2.uC_225_Item2.txt_Truong_20);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_21, this.UC_225_2.uC_225_Item2.txt_Truong_21);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_22, this.UC_225_2.uC_225_Item2.txt_Truong_22);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_23, this.UC_225_2.uC_225_Item2.txt_Truong_23);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item2.txt_Truong_24, this.UC_225_2.uC_225_Item2.txt_Truong_24);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_02, this.UC_225_2.uC_225_Item3.txt_Truong_02);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_03, this.UC_225_2.uC_225_Item3.txt_Truong_03);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_04_1, this.UC_225_2.uC_225_Item3.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_04_2, this.UC_225_2.uC_225_Item3.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_04_3, this.UC_225_2.uC_225_Item3.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_05_1, this.UC_225_2.uC_225_Item3.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_05_2, this.UC_225_2.uC_225_Item3.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_08, this.UC_225_2.uC_225_Item3.txt_Truong_08);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_09, this.UC_225_2.uC_225_Item3.txt_Truong_09);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_10, this.UC_225_2.uC_225_Item3.txt_Truong_10);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_11, this.UC_225_2.uC_225_Item3.txt_Truong_11);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_12, this.UC_225_2.uC_225_Item3.txt_Truong_12);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_14, this.UC_225_2.uC_225_Item3.txt_Truong_14);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_15, this.UC_225_2.uC_225_Item3.txt_Truong_15);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_16, this.UC_225_2.uC_225_Item3.txt_Truong_16);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_17, this.UC_225_2.uC_225_Item3.txt_Truong_17);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_18, this.UC_225_2.uC_225_Item3.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_225_1.uC_225_Item3.txt_Truong_19, this.UC_225_2.uC_225_Item3.txt_Truong_19);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_20, this.UC_225_2.uC_225_Item3.txt_Truong_20);
                Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_21, this.UC_225_2.uC_225_Item3.txt_Truong_21);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_22, this.UC_225_2.uC_225_Item3.txt_Truong_22);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_23, this.UC_225_2.uC_225_Item3.txt_Truong_23);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item3.txt_Truong_24, this.UC_225_2.uC_225_Item3.txt_Truong_24);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_02, this.UC_225_2.uC_225_Item4.txt_Truong_02);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_03, this.UC_225_2.uC_225_Item4.txt_Truong_03);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_04_1, this.UC_225_2.uC_225_Item4.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_04_2, this.UC_225_2.uC_225_Item4.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_04_3, this.UC_225_2.uC_225_Item4.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_05_1, this.UC_225_2.uC_225_Item4.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_05_2, this.UC_225_2.uC_225_Item4.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_08, this.UC_225_2.uC_225_Item4.txt_Truong_08);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_09, this.UC_225_2.uC_225_Item4.txt_Truong_09);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_10, this.UC_225_2.uC_225_Item4.txt_Truong_10);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_11, this.UC_225_2.uC_225_Item4.txt_Truong_11);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_12, this.UC_225_2.uC_225_Item4.txt_Truong_12);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_14, this.UC_225_2.uC_225_Item4.txt_Truong_14);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_15, this.UC_225_2.uC_225_Item4.txt_Truong_15);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_16, this.UC_225_2.uC_225_Item4.txt_Truong_16);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_17, this.UC_225_2.uC_225_Item4.txt_Truong_17);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_18, this.UC_225_2.uC_225_Item4.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_225_1.uC_225_Item4.txt_Truong_19, this.UC_225_2.uC_225_Item4.txt_Truong_19);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_20, this.UC_225_2.uC_225_Item4.txt_Truong_20);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_21, this.UC_225_2.uC_225_Item4.txt_Truong_21);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_22, this.UC_225_2.uC_225_Item4.txt_Truong_22);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_23, this.UC_225_2.uC_225_Item4.txt_Truong_23);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item4.txt_Truong_24, this.UC_225_2.uC_225_Item4.txt_Truong_24);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_02, this.UC_225_2.uC_225_Item5.txt_Truong_02);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_03, this.UC_225_2.uC_225_Item5.txt_Truong_03);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_04_1, this.UC_225_2.uC_225_Item5.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_04_2, this.UC_225_2.uC_225_Item5.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_04_3, this.UC_225_2.uC_225_Item5.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_05_1, this.UC_225_2.uC_225_Item5.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_05_2, this.UC_225_2.uC_225_Item5.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_08, this.UC_225_2.uC_225_Item5.txt_Truong_08);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_09, this.UC_225_2.uC_225_Item5.txt_Truong_09);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_10, this.UC_225_2.uC_225_Item5.txt_Truong_10);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_11, this.UC_225_2.uC_225_Item5.txt_Truong_11);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_12, this.UC_225_2.uC_225_Item5.txt_Truong_12);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_14, this.UC_225_2.uC_225_Item5.txt_Truong_14);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_15, this.UC_225_2.uC_225_Item5.txt_Truong_15);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_16, this.UC_225_2.uC_225_Item5.txt_Truong_16);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_17, this.UC_225_2.uC_225_Item5.txt_Truong_17);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_18, this.UC_225_2.uC_225_Item5.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_225_1.uC_225_Item5.txt_Truong_19, this.UC_225_2.uC_225_Item5.txt_Truong_19);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_20, this.UC_225_2.uC_225_Item5.txt_Truong_20);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_21, this.UC_225_2.uC_225_Item5.txt_Truong_21);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_22, this.UC_225_2.uC_225_Item5.txt_Truong_22);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_23, this.UC_225_2.uC_225_Item5.txt_Truong_23);
                this.Compare_TextBox(this.UC_225_1.uC_225_Item5.txt_Truong_24, this.UC_225_2.uC_225_Item5.txt_Truong_24);
                this.Compare_TextBox(this.UC_225_1.txt_Truong_Flag, this.UC_225_2.txt_Truong_Flag);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_02, this.UC_2225_2.uC_2225_Item1.txt_Truong_02);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_03, this.UC_2225_2.uC_2225_Item1.txt_Truong_03);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_04_1, this.UC_2225_2.uC_2225_Item1.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_04_2, this.UC_2225_2.uC_2225_Item1.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_04_3, this.UC_2225_2.uC_2225_Item1.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_05_1, this.UC_2225_2.uC_2225_Item1.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_05_2, this.UC_2225_2.uC_2225_Item1.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_06, this.UC_2225_2.uC_2225_Item1.txt_Truong_06);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_07, this.UC_2225_2.uC_2225_Item1.txt_Truong_07);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_08, this.UC_2225_2.uC_2225_Item1.txt_Truong_08);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_09, this.UC_2225_2.uC_2225_Item1.txt_Truong_09);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_10, this.UC_2225_2.uC_2225_Item1.txt_Truong_10);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_11, this.UC_2225_2.uC_2225_Item1.txt_Truong_11);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_12, this.UC_2225_2.uC_2225_Item1.txt_Truong_12);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_13, this.UC_2225_2.uC_2225_Item1.txt_Truong_13);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_14, this.UC_2225_2.uC_2225_Item1.txt_Truong_14);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_15, this.UC_2225_2.uC_2225_Item1.txt_Truong_15);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_16, this.UC_2225_2.uC_2225_Item1.txt_Truong_16);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_17, this.UC_2225_2.uC_2225_Item1.txt_Truong_17);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_18, this.UC_2225_2.uC_2225_Item1.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_2225_1.uC_2225_Item1.txt_Truong_19, this.UC_2225_2.uC_2225_Item1.txt_Truong_19);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_20, this.UC_2225_2.uC_2225_Item1.txt_Truong_20);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_21, this.UC_2225_2.uC_2225_Item1.txt_Truong_21);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_22, this.UC_2225_2.uC_2225_Item1.txt_Truong_22);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_23, this.UC_2225_2.uC_2225_Item1.txt_Truong_23);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_24, this.UC_2225_2.uC_2225_Item1.txt_Truong_24);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_25, this.UC_2225_2.uC_2225_Item1.txt_Truong_25);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item1.txt_Truong_26, this.UC_2225_2.uC_2225_Item1.txt_Truong_26);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_02, this.UC_2225_2.uC_2225_Item2.txt_Truong_02);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_03, this.UC_2225_2.uC_2225_Item2.txt_Truong_03);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_04_1, this.UC_2225_2.uC_2225_Item2.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_04_2, this.UC_2225_2.uC_2225_Item2.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_04_3, this.UC_2225_2.uC_2225_Item2.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_05_1, this.UC_2225_2.uC_2225_Item2.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_05_2, this.UC_2225_2.uC_2225_Item2.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_06, this.UC_2225_2.uC_2225_Item2.txt_Truong_06);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_07, this.UC_2225_2.uC_2225_Item2.txt_Truong_07);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_08, this.UC_2225_2.uC_2225_Item2.txt_Truong_08);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_09, this.UC_2225_2.uC_2225_Item2.txt_Truong_09);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_10, this.UC_2225_2.uC_2225_Item2.txt_Truong_10);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_11, this.UC_2225_2.uC_2225_Item2.txt_Truong_11);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_12, this.UC_2225_2.uC_2225_Item2.txt_Truong_12);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_13, this.UC_2225_2.uC_2225_Item2.txt_Truong_13);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_14, this.UC_2225_2.uC_2225_Item2.txt_Truong_14);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_15, this.UC_2225_2.uC_2225_Item2.txt_Truong_15);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_16, this.UC_2225_2.uC_2225_Item2.txt_Truong_16);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_17, this.UC_2225_2.uC_2225_Item2.txt_Truong_17);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_18, this.UC_2225_2.uC_2225_Item2.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_2225_1.uC_2225_Item2.txt_Truong_19, this.UC_2225_2.uC_2225_Item2.txt_Truong_19);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_20, this.UC_2225_2.uC_2225_Item2.txt_Truong_20);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_21, this.UC_2225_2.uC_2225_Item2.txt_Truong_21);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_22, this.UC_2225_2.uC_2225_Item2.txt_Truong_22);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_23, this.UC_2225_2.uC_2225_Item2.txt_Truong_23);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_24, this.UC_2225_2.uC_2225_Item2.txt_Truong_24);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_25, this.UC_2225_2.uC_2225_Item2.txt_Truong_25);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item2.txt_Truong_26, this.UC_2225_2.uC_2225_Item2.txt_Truong_26);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_02, this.UC_2225_2.uC_2225_Item3.txt_Truong_02);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_03, this.UC_2225_2.uC_2225_Item3.txt_Truong_03);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_04_1, this.UC_2225_2.uC_2225_Item3.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_04_2, this.UC_2225_2.uC_2225_Item3.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_04_3, this.UC_2225_2.uC_2225_Item3.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_05_1, this.UC_2225_2.uC_2225_Item3.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_05_2, this.UC_2225_2.uC_2225_Item3.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_06, this.UC_2225_2.uC_2225_Item3.txt_Truong_06);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_07, this.UC_2225_2.uC_2225_Item3.txt_Truong_07);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_08, this.UC_2225_2.uC_2225_Item3.txt_Truong_08);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_09, this.UC_2225_2.uC_2225_Item3.txt_Truong_09);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_10, this.UC_2225_2.uC_2225_Item3.txt_Truong_10);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_11, this.UC_2225_2.uC_2225_Item3.txt_Truong_11);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_12, this.UC_2225_2.uC_2225_Item3.txt_Truong_12);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_13, this.UC_2225_2.uC_2225_Item3.txt_Truong_13);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_14, this.UC_2225_2.uC_2225_Item3.txt_Truong_14);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_15, this.UC_2225_2.uC_2225_Item3.txt_Truong_15);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_16, this.UC_2225_2.uC_2225_Item3.txt_Truong_16);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_17, this.UC_2225_2.uC_2225_Item3.txt_Truong_17);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_18, this.UC_2225_2.uC_2225_Item3.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_2225_1.uC_2225_Item3.txt_Truong_19, this.UC_2225_2.uC_2225_Item3.txt_Truong_19);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_20, this.UC_2225_2.uC_2225_Item3.txt_Truong_20);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_21, this.UC_2225_2.uC_2225_Item3.txt_Truong_21);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_22, this.UC_2225_2.uC_2225_Item3.txt_Truong_22);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_23, this.UC_2225_2.uC_2225_Item3.txt_Truong_23);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_24, this.UC_2225_2.uC_2225_Item3.txt_Truong_24);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_25, this.UC_2225_2.uC_2225_Item3.txt_Truong_25);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item3.txt_Truong_26, this.UC_2225_2.uC_2225_Item3.txt_Truong_26);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_02, this.UC_2225_2.uC_2225_Item4.txt_Truong_02);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_03, this.UC_2225_2.uC_2225_Item4.txt_Truong_03);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_04_1, this.UC_2225_2.uC_2225_Item4.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_04_2, this.UC_2225_2.uC_2225_Item4.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_04_3, this.UC_2225_2.uC_2225_Item4.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_05_1, this.UC_2225_2.uC_2225_Item4.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_05_2, this.UC_2225_2.uC_2225_Item4.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_06, this.UC_2225_2.uC_2225_Item4.txt_Truong_06);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_07, this.UC_2225_2.uC_2225_Item4.txt_Truong_07);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_08, this.UC_2225_2.uC_2225_Item4.txt_Truong_08);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_09, this.UC_2225_2.uC_2225_Item4.txt_Truong_09);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_10, this.UC_2225_2.uC_2225_Item4.txt_Truong_10);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_11, this.UC_2225_2.uC_2225_Item4.txt_Truong_11);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_12, this.UC_2225_2.uC_2225_Item4.txt_Truong_12);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_13, this.UC_2225_2.uC_2225_Item4.txt_Truong_13);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_14, this.UC_2225_2.uC_2225_Item4.txt_Truong_14);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_15, this.UC_2225_2.uC_2225_Item4.txt_Truong_15);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_16, this.UC_2225_2.uC_2225_Item4.txt_Truong_16);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_17, this.UC_2225_2.uC_2225_Item4.txt_Truong_17);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_18, this.UC_2225_2.uC_2225_Item4.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_2225_1.uC_2225_Item4.txt_Truong_19, this.UC_2225_2.uC_2225_Item4.txt_Truong_19);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_20, this.UC_2225_2.uC_2225_Item4.txt_Truong_20);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_21, this.UC_2225_2.uC_2225_Item4.txt_Truong_21);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_22, this.UC_2225_2.uC_2225_Item4.txt_Truong_22);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_23, this.UC_2225_2.uC_2225_Item4.txt_Truong_23);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_24, this.UC_2225_2.uC_2225_Item4.txt_Truong_24);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_25, this.UC_2225_2.uC_2225_Item4.txt_Truong_25);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item4.txt_Truong_26, this.UC_2225_2.uC_2225_Item4.txt_Truong_26);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_02, this.UC_2225_2.uC_2225_Item5.txt_Truong_02);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_03, this.UC_2225_2.uC_2225_Item5.txt_Truong_03);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_04_1, this.UC_2225_2.uC_2225_Item5.txt_Truong_04_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_04_2, this.UC_2225_2.uC_2225_Item5.txt_Truong_04_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_04_3, this.UC_2225_2.uC_2225_Item5.txt_Truong_04_3);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_05_1, this.UC_2225_2.uC_2225_Item5.txt_Truong_05_1);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_05_2, this.UC_2225_2.uC_2225_Item5.txt_Truong_05_2);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_06, this.UC_2225_2.uC_2225_Item5.txt_Truong_06);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_07, this.UC_2225_2.uC_2225_Item5.txt_Truong_07);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_08, this.UC_2225_2.uC_2225_Item5.txt_Truong_08);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_09, this.UC_2225_2.uC_2225_Item5.txt_Truong_09);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_10, this.UC_2225_2.uC_2225_Item5.txt_Truong_10);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_11, this.UC_2225_2.uC_2225_Item5.txt_Truong_11);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_12, this.UC_2225_2.uC_2225_Item5.txt_Truong_12);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_13, this.UC_2225_2.uC_2225_Item5.txt_Truong_13);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_14, this.UC_2225_2.uC_2225_Item5.txt_Truong_14);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_15, this.UC_2225_2.uC_2225_Item5.txt_Truong_15);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_16, this.UC_2225_2.uC_2225_Item5.txt_Truong_16);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_17, this.UC_2225_2.uC_2225_Item5.txt_Truong_17);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_18, this.UC_2225_2.uC_2225_Item5.txt_Truong_18);
                this.Compare_LookUpEdit(this.UC_2225_1.uC_2225_Item5.txt_Truong_19, this.UC_2225_2.uC_2225_Item5.txt_Truong_19);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_20, this.UC_2225_2.uC_2225_Item5.txt_Truong_20);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_21, this.UC_2225_2.uC_2225_Item5.txt_Truong_21);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_22, this.UC_2225_2.uC_2225_Item5.txt_Truong_22);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_23, this.UC_2225_2.uC_2225_Item5.txt_Truong_23);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_24, this.UC_2225_2.uC_2225_Item5.txt_Truong_24);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_25, this.UC_2225_2.uC_2225_Item5.txt_Truong_25);
                this.Compare_TextBox(this.UC_2225_1.uC_2225_Item5.txt_Truong_26, this.UC_2225_2.uC_2225_Item5.txt_Truong_26);
                this.Compare_TextBox(this.UC_2225_1.txt_Truong_Flag, this.UC_2225_2.txt_Truong_Flag);
            }
            if (this.txt_Truong_01_User1.Text == "225")
            {
                this.UC_225_1.uC_225_Item1.txt_Truong_02.Focus();
            }
            else
            {
                this.UC_2225_1.uC_2225_Item1.txt_Truong_02.Focus();
            }
            this.timer1.Enabled = true;
        }
        bool FlagLoad = false;
        private void frm_Checker_Load(object sender, EventArgs e)
        {
            Global.FlagLoad = true;
            FlagLoad = true;
            this.Text = TypeCheck;
            this.splitCheck.SplitterPosition = 640;
            this.Tab_User1.ShowTabHeader = DefaultBoolean.False;
            this.Tab_User2.ShowTabHeader = DefaultBoolean.False;
            VisibleButtonSave();
            var BatchTemp = (from w in Global.Db.GetBatNotFinishChecker(Global.StrUserName, TypeCheck) select new { w.BatchID, w.BatchName }).ToList();

            if (BatchTemp.Count()>0)
            {
                cbb_Batch_Check.DataSource = BatchTemp;
                cbb_Batch_Check.DisplayMember = "BatchName";
                cbb_Batch_Check.ValueMember = "BatchID";
                cbb_Batch_Check.SelectedValue = Global.StrBatchID;
                if (string.IsNullOrEmpty(cbb_Batch_Check.Text))
                    cbb_Batch_Check.SelectedIndex = 0;
            }
            
            Global.FlagLoad = false;

            if (Global.StrCheck == "CHECKDESO")
            {
                this.UC_225_1.UC_225_Load(null, null);
                this.UC_225_2.UC_225_Load(null, null);
                this.UC_2225_1.UC_2225_Load(null, null);
                this.UC_2225_2.UC_2225_Load(null, null);
                this.UC_225_1.Changed  += UC_DESO1_Changed;
                this.UC_2225_1.Changed   += UC_DESO1_Changed;
                this.UC_225_2.Changed += Uc_DeSo2_Changed;
                this.UC_2225_2.Changed += Uc_DeSo2_Changed;
            }
            ResetData();
            FlagLoad = false;
            cbb_Batch_Check.Focus();
        }
        
        private void UC_DESO1_Changed(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(lb_Image.Text))
            //    return;
            //btn_Luu_DeSo1.Visible = true;
            //btn_Luu_DeSo2.Visible = false;
        }

        private void Uc_DeSo2_Changed(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(lb_Image.Text))
            //    return;
            //btn_Luu_DeSo1.Visible = false;
            //btn_Luu_DeSo2.Visible = true;
        }
        
        public bool CheckLoaiPhieu(string txt_Truong_01)
        {
            string[] source = new string[] { "225", "2225" };
            return !source.Contains<string>(txt_Truong_01);
        }
        private void btn_Luu_DeSo1_Click(object sender, EventArgs e)
        {
            if (Global.StrCheck == "CHECKDESO")
            {
                if (string.IsNullOrEmpty(this.txt_Truong_01_User1.Text))
                {
                    MessageBox.Show("Bạn đang để trống trường 1. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (this.CheckLoaiPhieu(this.txt_Truong_01_User1.Text) && (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No))
                {
                    return;
                }
                bool flag_Phieu1_User1Sai = false;
                bool flag_Phieu2_User1Sai = false;
                bool flag_Phieu3_User1Sai = false;
                bool flag_Phieu4_User1Sai = false;
                bool flag_Phieu5_User1Sai = false;
                bool flag_Phieu1_User2Sai = false;
                bool flag_Phieu2_User2Sai = false;
                bool flag_Phieu3_User2Sai = false;
                bool flag_Phieu4_User2Sai = false;
                bool flag_Phieu5_User2Sai = false;
                if (this.txt_Truong_01_User1.Text == "225")
                {
                    if (this.UC_225_1.IsEmpty() && (MessageBox.Show("Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
                    {
                        return;
                    }
                    //--Check user 1
                    //Check phiếu 1
                    if (DataUser1[0].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[0].Truong_02 != UC_225_1.uC_225_Item1.txt_Truong_02.Text 
                        || DataUser1[0].Truong_03 != UC_225_1.uC_225_Item1.txt_Truong_03.Text 
                        || DataUser1[0].Truong_04_1 != UC_225_1.uC_225_Item1.txt_Truong_04_1.Text 
                        || DataUser1[0].Truong_04_2 != UC_225_1.uC_225_Item1.txt_Truong_04_2.Text 
                        || DataUser1[0].Truong_04_3 != UC_225_1.uC_225_Item1.txt_Truong_04_3.Text 
                        || DataUser1[0].Truong_05_1 != UC_225_1.uC_225_Item1.txt_Truong_05_1.Text 
                        || DataUser1[0].Truong_05_2 != UC_225_1.uC_225_Item1.txt_Truong_05_2.Text 
                        || DataUser1[0].Truong_08 != UC_225_1.uC_225_Item1.txt_Truong_08.Text 
                        || DataUser1[0].Truong_09 != UC_225_1.uC_225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[0].Truong_10 != UC_225_1.uC_225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[0].Truong_11 != UC_225_1.uC_225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[0].Truong_12 != UC_225_1.uC_225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[0].Truong_14 != UC_225_1.uC_225_Item1.txt_Truong_14.Text 
                        || DataUser1[0].Truong_15 != UC_225_1.uC_225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[0].Truong_16 != UC_225_1.uC_225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[0].Truong_17 != UC_225_1.uC_225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[0].Truong_18 != UC_225_1.uC_225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[0].Truong_19 != UC_225_1.uC_225_Item1.txt_Truong_19.Text 
                        || DataUser1[0].Truong_20 != UC_225_1.uC_225_Item1.txt_Truong_20.Text 
                        || DataUser1[0].Truong_21 != UC_225_1.uC_225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[0].Truong_22 != UC_225_1.uC_225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[0].Truong_23 != UC_225_1.uC_225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[0].Truong_24 != UC_225_1.uC_225_Item1.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu1_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User1Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser1[1].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[1].Truong_02 != UC_225_1.uC_225_Item2.txt_Truong_02.Text 
                        || DataUser1[1].Truong_03 != UC_225_1.uC_225_Item2.txt_Truong_03.Text 
                        || DataUser1[1].Truong_04_1 != UC_225_1.uC_225_Item2.txt_Truong_04_1.Text 
                        || DataUser1[1].Truong_04_2 != UC_225_1.uC_225_Item2.txt_Truong_04_2.Text 
                        || DataUser1[1].Truong_04_3 != UC_225_1.uC_225_Item2.txt_Truong_04_3.Text 
                        || DataUser1[1].Truong_05_1 != UC_225_1.uC_225_Item2.txt_Truong_05_1.Text 
                        || DataUser1[1].Truong_05_2 != UC_225_1.uC_225_Item2.txt_Truong_05_2.Text 
                        || DataUser1[1].Truong_08 != UC_225_1.uC_225_Item2.txt_Truong_08.Text 
                        || DataUser1[1].Truong_09 != UC_225_1.uC_225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[1].Truong_10 != UC_225_1.uC_225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[1].Truong_11 != UC_225_1.uC_225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[1].Truong_12 != UC_225_1.uC_225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[1].Truong_14 != UC_225_1.uC_225_Item2.txt_Truong_14.Text 
                        || DataUser1[1].Truong_15 != UC_225_1.uC_225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[1].Truong_16 != UC_225_1.uC_225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[1].Truong_17 != UC_225_1.uC_225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[1].Truong_18 != UC_225_1.uC_225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[1].Truong_19 != UC_225_1.uC_225_Item2.txt_Truong_19.Text 
                        || DataUser1[1].Truong_20 != UC_225_1.uC_225_Item2.txt_Truong_20.Text 
                        || DataUser1[1].Truong_21 != UC_225_1.uC_225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[1].Truong_22 != UC_225_1.uC_225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[1].Truong_23 != UC_225_1.uC_225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[1].Truong_24 != UC_225_1.uC_225_Item2.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu2_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User1Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser1[2].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[2].Truong_02 != UC_225_1.uC_225_Item3.txt_Truong_02.Text 
                        || DataUser1[2].Truong_03 != UC_225_1.uC_225_Item3.txt_Truong_03.Text 
                        || DataUser1[2].Truong_04_1 != UC_225_1.uC_225_Item3.txt_Truong_04_1.Text 
                        || DataUser1[2].Truong_04_2 != UC_225_1.uC_225_Item3.txt_Truong_04_2.Text 
                        || DataUser1[2].Truong_04_3 != UC_225_1.uC_225_Item3.txt_Truong_04_3.Text 
                        || DataUser1[2].Truong_05_1 != UC_225_1.uC_225_Item3.txt_Truong_05_1.Text 
                        || DataUser1[2].Truong_05_2 != UC_225_1.uC_225_Item3.txt_Truong_05_2.Text 
                        || DataUser1[2].Truong_08 != UC_225_1.uC_225_Item3.txt_Truong_08.Text 
                        || DataUser1[2].Truong_09 != UC_225_1.uC_225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[2].Truong_10 != UC_225_1.uC_225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[2].Truong_11 != UC_225_1.uC_225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[2].Truong_12 != UC_225_1.uC_225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[2].Truong_14 != UC_225_1.uC_225_Item3.txt_Truong_14.Text 
                        || DataUser1[2].Truong_15 != UC_225_1.uC_225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[2].Truong_16 != UC_225_1.uC_225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[2].Truong_17 != UC_225_1.uC_225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[2].Truong_18 != UC_225_1.uC_225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[2].Truong_19 != UC_225_1.uC_225_Item3.txt_Truong_19.Text 
                        || DataUser1[2].Truong_20 != UC_225_1.uC_225_Item3.txt_Truong_20.Text 
                        || DataUser1[2].Truong_21 != UC_225_1.uC_225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[2].Truong_22 != UC_225_1.uC_225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[2].Truong_23 != UC_225_1.uC_225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[2].Truong_24 != UC_225_1.uC_225_Item3.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu3_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User1Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser1[3].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[3].Truong_02 != UC_225_1.uC_225_Item4.txt_Truong_02.Text 
                        || DataUser1[3].Truong_03 != UC_225_1.uC_225_Item4.txt_Truong_03.Text 
                        || DataUser1[3].Truong_04_1 != UC_225_1.uC_225_Item4.txt_Truong_04_1.Text 
                        || DataUser1[3].Truong_04_2 != UC_225_1.uC_225_Item4.txt_Truong_04_2.Text 
                        || DataUser1[3].Truong_04_3 != UC_225_1.uC_225_Item4.txt_Truong_04_3.Text 
                        || DataUser1[3].Truong_05_1 != UC_225_1.uC_225_Item4.txt_Truong_05_1.Text 
                        || DataUser1[3].Truong_05_2 != UC_225_1.uC_225_Item4.txt_Truong_05_2.Text 
                        || DataUser1[3].Truong_08 != UC_225_1.uC_225_Item4.txt_Truong_08.Text 
                        || DataUser1[3].Truong_09 != UC_225_1.uC_225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[3].Truong_10 != UC_225_1.uC_225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[3].Truong_11 != UC_225_1.uC_225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[3].Truong_12 != UC_225_1.uC_225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[3].Truong_14 != UC_225_1.uC_225_Item4.txt_Truong_14.Text 
                        || DataUser1[3].Truong_15 != UC_225_1.uC_225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[3].Truong_16 != UC_225_1.uC_225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[3].Truong_17 != UC_225_1.uC_225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[3].Truong_18 != UC_225_1.uC_225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[3].Truong_19 != UC_225_1.uC_225_Item4.txt_Truong_19.Text 
                        || DataUser1[3].Truong_20 != UC_225_1.uC_225_Item4.txt_Truong_20.Text 
                        || DataUser1[3].Truong_21 != UC_225_1.uC_225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[3].Truong_22 != UC_225_1.uC_225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[3].Truong_23 != UC_225_1.uC_225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[3].Truong_24 != UC_225_1.uC_225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[3].Truong_Flag != UC_225_1.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User1Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser1[4].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[4].Truong_02 != UC_225_1.uC_225_Item5.txt_Truong_02.Text 
                        || DataUser1[4].Truong_03 != UC_225_1.uC_225_Item5.txt_Truong_03.Text 
                        || DataUser1[4].Truong_04_1 != UC_225_1.uC_225_Item5.txt_Truong_04_1.Text 
                        || DataUser1[4].Truong_04_2 != UC_225_1.uC_225_Item5.txt_Truong_04_2.Text 
                        || DataUser1[4].Truong_04_3 != UC_225_1.uC_225_Item5.txt_Truong_04_3.Text 
                        || DataUser1[4].Truong_05_1 != UC_225_1.uC_225_Item5.txt_Truong_05_1.Text 
                        || DataUser1[4].Truong_05_2 != UC_225_1.uC_225_Item5.txt_Truong_05_2.Text 
                        || DataUser1[4].Truong_08 != UC_225_1.uC_225_Item5.txt_Truong_08.Text 
                        || DataUser1[4].Truong_09 != UC_225_1.uC_225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[4].Truong_10 != UC_225_1.uC_225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[4].Truong_11 != UC_225_1.uC_225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[4].Truong_12 != UC_225_1.uC_225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[4].Truong_14 != UC_225_1.uC_225_Item5.txt_Truong_14.Text 
                        || DataUser1[4].Truong_15 != UC_225_1.uC_225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[4].Truong_16 != UC_225_1.uC_225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[4].Truong_17 != UC_225_1.uC_225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[4].Truong_18 != UC_225_1.uC_225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[4].Truong_19 != UC_225_1.uC_225_Item5.txt_Truong_19.Text 
                        || DataUser1[4].Truong_20 != UC_225_1.uC_225_Item5.txt_Truong_20.Text 
                        || DataUser1[4].Truong_21 != UC_225_1.uC_225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[4].Truong_22 != UC_225_1.uC_225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[4].Truong_23 != UC_225_1.uC_225_Item5.txt_Truong_23.Text.Replace(",", "")
                        || DataUser1[4].Truong_24 != UC_225_1.uC_225_Item5.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu5_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User1Sai = false;
                    }
                    //--Check user 2
                    //Check phiếu 1
                    if (DataUser2[0].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[0].Truong_02 != UC_225_1.uC_225_Item1.txt_Truong_02.Text 
                        || DataUser2[0].Truong_03 != UC_225_1.uC_225_Item1.txt_Truong_03.Text 
                        || DataUser2[0].Truong_04_1 != UC_225_1.uC_225_Item1.txt_Truong_04_1.Text 
                        || DataUser2[0].Truong_04_2 != UC_225_1.uC_225_Item1.txt_Truong_04_2.Text 
                        || DataUser2[0].Truong_04_3 != UC_225_1.uC_225_Item1.txt_Truong_04_3.Text 
                        || DataUser2[0].Truong_05_1 != UC_225_1.uC_225_Item1.txt_Truong_05_1.Text 
                        || DataUser2[0].Truong_05_2 != UC_225_1.uC_225_Item1.txt_Truong_05_2.Text 
                        || DataUser2[0].Truong_08 != UC_225_1.uC_225_Item1.txt_Truong_08.Text 
                        || DataUser2[0].Truong_09 != UC_225_1.uC_225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[0].Truong_10 != UC_225_1.uC_225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[0].Truong_11 != UC_225_1.uC_225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[0].Truong_12 != UC_225_1.uC_225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[0].Truong_14 != UC_225_1.uC_225_Item1.txt_Truong_14.Text 
                        || DataUser2[0].Truong_15 != UC_225_1.uC_225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[0].Truong_16 != UC_225_1.uC_225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[0].Truong_17 != UC_225_1.uC_225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[0].Truong_18 != UC_225_1.uC_225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[0].Truong_19 != UC_225_1.uC_225_Item1.txt_Truong_19.Text 
                        || DataUser2[0].Truong_20 != UC_225_1.uC_225_Item1.txt_Truong_20.Text 
                        || DataUser2[0].Truong_21 != UC_225_1.uC_225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[0].Truong_22 != UC_225_1.uC_225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[0].Truong_23 != UC_225_1.uC_225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[0].Truong_24 != UC_225_1.uC_225_Item1.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu1_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User2Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser2[1].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[1].Truong_02 != UC_225_1.uC_225_Item2.txt_Truong_02.Text 
                        || DataUser2[1].Truong_03 != UC_225_1.uC_225_Item2.txt_Truong_03.Text 
                        || DataUser2[1].Truong_04_1 != UC_225_1.uC_225_Item2.txt_Truong_04_1.Text 
                        || DataUser2[1].Truong_04_2 != UC_225_1.uC_225_Item2.txt_Truong_04_2.Text 
                        || DataUser2[1].Truong_04_3 != UC_225_1.uC_225_Item2.txt_Truong_04_3.Text 
                        || DataUser2[1].Truong_05_1 != UC_225_1.uC_225_Item2.txt_Truong_05_1.Text 
                        || DataUser2[1].Truong_05_2 != UC_225_1.uC_225_Item2.txt_Truong_05_2.Text 
                        || DataUser2[1].Truong_08 != UC_225_1.uC_225_Item2.txt_Truong_08.Text 
                        || DataUser2[1].Truong_09 != UC_225_1.uC_225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[1].Truong_10 != UC_225_1.uC_225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[1].Truong_11 != UC_225_1.uC_225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[1].Truong_12 != UC_225_1.uC_225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[1].Truong_14 != UC_225_1.uC_225_Item2.txt_Truong_14.Text 
                        || DataUser2[1].Truong_15 != UC_225_1.uC_225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[1].Truong_16 != UC_225_1.uC_225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[1].Truong_17 != UC_225_1.uC_225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[1].Truong_18 != UC_225_1.uC_225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[1].Truong_19 != UC_225_1.uC_225_Item2.txt_Truong_19.Text 
                        || DataUser2[1].Truong_20 != UC_225_1.uC_225_Item2.txt_Truong_20.Text 
                        || DataUser2[1].Truong_21 != UC_225_1.uC_225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[1].Truong_22 != UC_225_1.uC_225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[1].Truong_23 != UC_225_1.uC_225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[1].Truong_24 != UC_225_1.uC_225_Item2.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu2_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User2Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser2[2].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[2].Truong_02 != UC_225_1.uC_225_Item3.txt_Truong_02.Text 
                        || DataUser2[2].Truong_03 != UC_225_1.uC_225_Item3.txt_Truong_03.Text 
                        || DataUser2[2].Truong_04_1 != UC_225_1.uC_225_Item3.txt_Truong_04_1.Text 
                        || DataUser2[2].Truong_04_2 != UC_225_1.uC_225_Item3.txt_Truong_04_2.Text 
                        || DataUser2[2].Truong_04_3 != UC_225_1.uC_225_Item3.txt_Truong_04_3.Text 
                        || DataUser2[2].Truong_05_1 != UC_225_1.uC_225_Item3.txt_Truong_05_1.Text 
                        || DataUser2[2].Truong_05_2 != UC_225_1.uC_225_Item3.txt_Truong_05_2.Text 
                        || DataUser2[2].Truong_08 != UC_225_1.uC_225_Item3.txt_Truong_08.Text 
                        || DataUser2[2].Truong_09 != UC_225_1.uC_225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[2].Truong_10 != UC_225_1.uC_225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[2].Truong_11 != UC_225_1.uC_225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[2].Truong_12 != UC_225_1.uC_225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[2].Truong_14 != UC_225_1.uC_225_Item3.txt_Truong_14.Text 
                        || DataUser2[2].Truong_15 != UC_225_1.uC_225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[2].Truong_16 != UC_225_1.uC_225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[2].Truong_17 != UC_225_1.uC_225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[2].Truong_18 != UC_225_1.uC_225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[2].Truong_19 != UC_225_1.uC_225_Item3.txt_Truong_19.Text 
                        || DataUser2[2].Truong_20 != UC_225_1.uC_225_Item3.txt_Truong_20.Text 
                        || DataUser2[2].Truong_21 != UC_225_1.uC_225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[2].Truong_22 != UC_225_1.uC_225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[2].Truong_23 != UC_225_1.uC_225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[2].Truong_24 != UC_225_1.uC_225_Item3.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu3_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User2Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser2[3].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[3].Truong_02 != UC_225_1.uC_225_Item4.txt_Truong_02.Text 
                        || DataUser2[3].Truong_03 != UC_225_1.uC_225_Item4.txt_Truong_03.Text 
                        || DataUser2[3].Truong_04_1 != UC_225_1.uC_225_Item4.txt_Truong_04_1.Text 
                        || DataUser2[3].Truong_04_2 != UC_225_1.uC_225_Item4.txt_Truong_04_2.Text 
                        || DataUser2[3].Truong_04_3 != UC_225_1.uC_225_Item4.txt_Truong_04_3.Text 
                        || DataUser2[3].Truong_05_1 != UC_225_1.uC_225_Item4.txt_Truong_05_1.Text 
                        || DataUser2[3].Truong_05_2 != UC_225_1.uC_225_Item4.txt_Truong_05_2.Text 
                        || DataUser2[3].Truong_08 != UC_225_1.uC_225_Item4.txt_Truong_08.Text 
                        || DataUser2[3].Truong_09 != UC_225_1.uC_225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[3].Truong_10 != UC_225_1.uC_225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[3].Truong_11 != UC_225_1.uC_225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[3].Truong_12 != UC_225_1.uC_225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[3].Truong_14 != UC_225_1.uC_225_Item4.txt_Truong_14.Text 
                        || DataUser2[3].Truong_15 != UC_225_1.uC_225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[3].Truong_16 != UC_225_1.uC_225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[3].Truong_17 != UC_225_1.uC_225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[3].Truong_18 != UC_225_1.uC_225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[3].Truong_19 != UC_225_1.uC_225_Item4.txt_Truong_19.Text 
                        || DataUser2[3].Truong_20 != UC_225_1.uC_225_Item4.txt_Truong_20.Text 
                        || DataUser2[3].Truong_21 != UC_225_1.uC_225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[3].Truong_22 != UC_225_1.uC_225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[3].Truong_23 != UC_225_1.uC_225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[3].Truong_24 != UC_225_1.uC_225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[3].Truong_Flag != UC_225_1.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User2Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser2[4].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[4].Truong_02 != UC_225_1.uC_225_Item5.txt_Truong_02.Text 
                        || DataUser2[4].Truong_03 != UC_225_1.uC_225_Item5.txt_Truong_03.Text 
                        || DataUser2[4].Truong_04_1 != UC_225_1.uC_225_Item5.txt_Truong_04_1.Text 
                        || DataUser2[4].Truong_04_2 != UC_225_1.uC_225_Item5.txt_Truong_04_2.Text 
                        || DataUser2[4].Truong_04_3 != UC_225_1.uC_225_Item5.txt_Truong_04_3.Text 
                        || DataUser2[4].Truong_05_1 != UC_225_1.uC_225_Item5.txt_Truong_05_1.Text 
                        || DataUser2[4].Truong_05_2 != UC_225_1.uC_225_Item5.txt_Truong_05_2.Text 
                        || DataUser2[4].Truong_08 != UC_225_1.uC_225_Item5.txt_Truong_08.Text 
                        || DataUser2[4].Truong_09 != UC_225_1.uC_225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[4].Truong_10 != UC_225_1.uC_225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[4].Truong_11 != UC_225_1.uC_225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[4].Truong_12 != UC_225_1.uC_225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[4].Truong_14 != UC_225_1.uC_225_Item5.txt_Truong_14.Text 
                        || DataUser2[4].Truong_15 != UC_225_1.uC_225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[4].Truong_16 != UC_225_1.uC_225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[4].Truong_17 != UC_225_1.uC_225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[4].Truong_18 != UC_225_1.uC_225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[4].Truong_19 != UC_225_1.uC_225_Item5.txt_Truong_19.Text 
                        || DataUser2[4].Truong_20 != UC_225_1.uC_225_Item5.txt_Truong_20.Text 
                        || DataUser2[4].Truong_21 != UC_225_1.uC_225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[4].Truong_22 != UC_225_1.uC_225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[4].Truong_23 != UC_225_1.uC_225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[4].Truong_24 != UC_225_1.uC_225_Item5.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu5_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User2Sai = false;
                    }
                    if (this.fLagRefresh)
                    {
                        this.UC_225_1.Save225_Check(this.fbatchRefresh, this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                    else
                    {
                        this.UC_225_1.Save225_Check(this.cbb_Batch_Check.SelectedValue+"", this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                }
                else
                {
                    if (this.UC_2225_1.IsEmpty() && (MessageBox.Show("Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
                    {
                        return;
                    }
                    //--Check User 1
                    //Check phiếu 1
                    if (DataUser1[0].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[0].Truong_02 != UC_2225_1.uC_2225_Item1.txt_Truong_02.Text 
                        || DataUser1[0].Truong_03 != UC_2225_1.uC_2225_Item1.txt_Truong_03.Text 
                        || DataUser1[0].Truong_04_1 != UC_2225_1.uC_2225_Item1.txt_Truong_04_1.Text 
                        || DataUser1[0].Truong_04_2 != UC_2225_1.uC_2225_Item1.txt_Truong_04_2.Text 
                        || DataUser1[0].Truong_04_3 != UC_2225_1.uC_2225_Item1.txt_Truong_04_3.Text 
                        || DataUser1[0].Truong_05_1 != UC_2225_1.uC_2225_Item1.txt_Truong_05_1.Text 
                        || DataUser1[0].Truong_05_2 != UC_2225_1.uC_2225_Item1.txt_Truong_05_2.Text 
                        || DataUser1[0].Truong_06 != UC_2225_1.uC_2225_Item1.txt_Truong_06.Text 
                        || DataUser1[0].Truong_07 != UC_2225_1.uC_2225_Item1.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[0].Truong_08 != UC_2225_1.uC_2225_Item1.txt_Truong_08.Text 
                        || DataUser1[0].Truong_09 != UC_2225_1.uC_2225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[0].Truong_10 != UC_2225_1.uC_2225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[0].Truong_11 != UC_2225_1.uC_2225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[0].Truong_12 != UC_2225_1.uC_2225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[0].Truong_13 != UC_2225_1.uC_2225_Item1.txt_Truong_13.Text
                        || DataUser1[0].Truong_14 != UC_2225_1.uC_2225_Item1.txt_Truong_14.Text 
                        || DataUser1[0].Truong_15 != UC_2225_1.uC_2225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[0].Truong_16 != UC_2225_1.uC_2225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[0].Truong_17 != UC_2225_1.uC_2225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[0].Truong_18 != UC_2225_1.uC_2225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[0].Truong_19 != UC_2225_1.uC_2225_Item1.txt_Truong_19.Text 
                        || DataUser1[0].Truong_20 != UC_2225_1.uC_2225_Item1.txt_Truong_20.Text 
                        || DataUser1[0].Truong_21 != UC_2225_1.uC_2225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[0].Truong_22 != UC_2225_1.uC_2225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[0].Truong_23 != UC_2225_1.uC_2225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[0].Truong_24 != UC_2225_1.uC_2225_Item1.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser1[0].Truong_25 != UC_2225_1.uC_2225_Item1.txt_Truong_25.Text 
                        || DataUser1[0].Truong_26 != UC_2225_1.uC_2225_Item1.txt_Truong_26.Text)
                    {
                        flag_Phieu1_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User1Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser1[1].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[1].Truong_02 != UC_2225_1.uC_2225_Item2.txt_Truong_02.Text 
                        || DataUser1[1].Truong_03 != UC_2225_1.uC_2225_Item2.txt_Truong_03.Text 
                        || DataUser1[1].Truong_04_1 != UC_2225_1.uC_2225_Item2.txt_Truong_04_1.Text 
                        || DataUser1[1].Truong_04_2 != UC_2225_1.uC_2225_Item2.txt_Truong_04_2.Text 
                        || DataUser1[1].Truong_04_3 != UC_2225_1.uC_2225_Item2.txt_Truong_04_3.Text 
                        || DataUser1[1].Truong_05_1 != UC_2225_1.uC_2225_Item2.txt_Truong_05_1.Text 
                        || DataUser1[1].Truong_05_2 != UC_2225_1.uC_2225_Item2.txt_Truong_05_2.Text 
                        || DataUser1[1].Truong_06 != UC_2225_1.uC_2225_Item2.txt_Truong_06.Text 
                        || DataUser1[1].Truong_07 != UC_2225_1.uC_2225_Item2.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[1].Truong_08 != UC_2225_1.uC_2225_Item2.txt_Truong_08.Text 
                        || DataUser1[1].Truong_09 != UC_2225_1.uC_2225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[1].Truong_10 != UC_2225_1.uC_2225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[1].Truong_11 != UC_2225_1.uC_2225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[1].Truong_12 != UC_2225_1.uC_2225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[1].Truong_13 != UC_2225_1.uC_2225_Item2.txt_Truong_13.Text 
                        || DataUser1[1].Truong_14 != UC_2225_1.uC_2225_Item2.txt_Truong_14.Text 
                        || DataUser1[1].Truong_15 != UC_2225_1.uC_2225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[1].Truong_16 != UC_2225_1.uC_2225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[1].Truong_17 != UC_2225_1.uC_2225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[1].Truong_18 != UC_2225_1.uC_2225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[1].Truong_19 != UC_2225_1.uC_2225_Item2.txt_Truong_19.Text 
                        || DataUser1[1].Truong_20 != UC_2225_1.uC_2225_Item2.txt_Truong_20.Text 
                        || DataUser1[1].Truong_21 != UC_2225_1.uC_2225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[1].Truong_22 != UC_2225_1.uC_2225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[1].Truong_23 != UC_2225_1.uC_2225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[1].Truong_24 != UC_2225_1.uC_2225_Item2.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser1[1].Truong_25 != UC_2225_1.uC_2225_Item2.txt_Truong_25.Text 
                        || DataUser1[1].Truong_26 != UC_2225_1.uC_2225_Item2.txt_Truong_26.Text)
                    {
                        flag_Phieu2_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User1Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser1[2].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[2].Truong_02 != UC_2225_1.uC_2225_Item3.txt_Truong_02.Text 
                        || DataUser1[2].Truong_03 != UC_2225_1.uC_2225_Item3.txt_Truong_03.Text 
                        || DataUser1[2].Truong_04_1 != UC_2225_1.uC_2225_Item3.txt_Truong_04_1.Text 
                        || DataUser1[2].Truong_04_2 != UC_2225_1.uC_2225_Item3.txt_Truong_04_2.Text 
                        || DataUser1[2].Truong_04_3 != UC_2225_1.uC_2225_Item3.txt_Truong_04_3.Text 
                        || DataUser1[2].Truong_05_1 != UC_2225_1.uC_2225_Item3.txt_Truong_05_1.Text 
                        || DataUser1[2].Truong_05_2 != UC_2225_1.uC_2225_Item3.txt_Truong_05_2.Text 
                        || DataUser1[2].Truong_06 != UC_2225_1.uC_2225_Item3.txt_Truong_06.Text 
                        || DataUser1[2].Truong_07 != UC_2225_1.uC_2225_Item3.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[2].Truong_08 != UC_2225_1.uC_2225_Item3.txt_Truong_08.Text 
                        || DataUser1[2].Truong_09 != UC_2225_1.uC_2225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[2].Truong_10 != UC_2225_1.uC_2225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[2].Truong_11 != UC_2225_1.uC_2225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[2].Truong_12 != UC_2225_1.uC_2225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[2].Truong_13 != UC_2225_1.uC_2225_Item3.txt_Truong_13.Text 
                        || DataUser1[2].Truong_14 != UC_2225_1.uC_2225_Item3.txt_Truong_14.Text 
                        || DataUser1[2].Truong_15 != UC_2225_1.uC_2225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[2].Truong_16 != UC_2225_1.uC_2225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[2].Truong_17 != UC_2225_1.uC_2225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[2].Truong_18 != UC_2225_1.uC_2225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[2].Truong_19 != UC_2225_1.uC_2225_Item3.txt_Truong_19.Text 
                        || DataUser1[2].Truong_20 != UC_2225_1.uC_2225_Item3.txt_Truong_20.Text 
                        || DataUser1[2].Truong_21 != UC_2225_1.uC_2225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[2].Truong_22 != UC_2225_1.uC_2225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[2].Truong_23 != UC_2225_1.uC_2225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[2].Truong_24 != UC_2225_1.uC_2225_Item3.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser1[2].Truong_25 != UC_2225_1.uC_2225_Item3.txt_Truong_25.Text 
                        || DataUser1[2].Truong_26 != UC_2225_1.uC_2225_Item3.txt_Truong_26.Text)
                    {
                        flag_Phieu3_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User1Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser1[3].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[3].Truong_02 != UC_2225_1.uC_2225_Item4.txt_Truong_02.Text 
                        || DataUser1[3].Truong_03 != UC_2225_1.uC_2225_Item4.txt_Truong_03.Text 
                        || DataUser1[3].Truong_04_1 != UC_2225_1.uC_2225_Item4.txt_Truong_04_1.Text 
                        || DataUser1[3].Truong_04_2 != UC_2225_1.uC_2225_Item4.txt_Truong_04_2.Text 
                        || DataUser1[3].Truong_04_3 != UC_2225_1.uC_2225_Item4.txt_Truong_04_3.Text 
                        || DataUser1[3].Truong_05_1 != UC_2225_1.uC_2225_Item4.txt_Truong_05_1.Text 
                        || DataUser1[3].Truong_05_2 != UC_2225_1.uC_2225_Item4.txt_Truong_05_2.Text 
                        || DataUser1[3].Truong_06 != UC_2225_1.uC_2225_Item4.txt_Truong_06.Text 
                        || DataUser1[3].Truong_07 != UC_2225_1.uC_2225_Item4.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[3].Truong_08 != UC_2225_1.uC_2225_Item4.txt_Truong_08.Text 
                        || DataUser1[3].Truong_09 != UC_2225_1.uC_2225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[3].Truong_10 != UC_2225_1.uC_2225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[3].Truong_11 != UC_2225_1.uC_2225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[3].Truong_12 != UC_2225_1.uC_2225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[3].Truong_13 != UC_2225_1.uC_2225_Item4.txt_Truong_13.Text 
                        || DataUser1[3].Truong_14 != UC_2225_1.uC_2225_Item4.txt_Truong_14.Text 
                        || DataUser1[3].Truong_15 != UC_2225_1.uC_2225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[3].Truong_16 != UC_2225_1.uC_2225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[3].Truong_17 != UC_2225_1.uC_2225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[3].Truong_18 != UC_2225_1.uC_2225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[3].Truong_19 != UC_2225_1.uC_2225_Item4.txt_Truong_19.Text 
                        || DataUser1[3].Truong_20 != UC_2225_1.uC_2225_Item4.txt_Truong_20.Text 
                        || DataUser1[3].Truong_21 != UC_2225_1.uC_2225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[3].Truong_22 != UC_2225_1.uC_2225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[3].Truong_23 != UC_2225_1.uC_2225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[3].Truong_24 != UC_2225_1.uC_2225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[3].Truong_25 != UC_2225_1.uC_2225_Item4.txt_Truong_25.Text
                        || DataUser1[3].Truong_26 != UC_2225_1.uC_2225_Item4.txt_Truong_26.Text
                        || DataUser1[3].Truong_Flag != UC_2225_1.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User1Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser1[4].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser1[4].Truong_02 != UC_2225_1.uC_2225_Item5.txt_Truong_02.Text 
                        || DataUser1[4].Truong_03 != UC_2225_1.uC_2225_Item5.txt_Truong_03.Text 
                        || DataUser1[4].Truong_04_1 != UC_2225_1.uC_2225_Item5.txt_Truong_04_1.Text 
                        || DataUser1[4].Truong_04_2 != UC_2225_1.uC_2225_Item5.txt_Truong_04_2.Text 
                        || DataUser1[4].Truong_04_3 != UC_2225_1.uC_2225_Item5.txt_Truong_04_3.Text 
                        || DataUser1[4].Truong_05_1 != UC_2225_1.uC_2225_Item5.txt_Truong_05_1.Text 
                        || DataUser1[4].Truong_05_2 != UC_2225_1.uC_2225_Item5.txt_Truong_05_2.Text 
                        || DataUser1[4].Truong_06 != UC_2225_1.uC_2225_Item5.txt_Truong_06.Text 
                        || DataUser1[4].Truong_07 != UC_2225_1.uC_2225_Item5.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[4].Truong_08 != UC_2225_1.uC_2225_Item5.txt_Truong_08.Text 
                        || DataUser1[4].Truong_09 != UC_2225_1.uC_2225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[4].Truong_10 != UC_2225_1.uC_2225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[4].Truong_11 != UC_2225_1.uC_2225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[4].Truong_12 != UC_2225_1.uC_2225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[4].Truong_13 != UC_2225_1.uC_2225_Item5.txt_Truong_13.Text 
                        || DataUser1[4].Truong_14 != UC_2225_1.uC_2225_Item5.txt_Truong_14.Text 
                        || DataUser1[4].Truong_15 != UC_2225_1.uC_2225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[4].Truong_16 != UC_2225_1.uC_2225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[4].Truong_17 != UC_2225_1.uC_2225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[4].Truong_18 != UC_2225_1.uC_2225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[4].Truong_19 != UC_2225_1.uC_2225_Item5.txt_Truong_19.Text 
                        || DataUser1[4].Truong_20 != UC_2225_1.uC_2225_Item5.txt_Truong_20.Text 
                        || DataUser1[4].Truong_21 != UC_2225_1.uC_2225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[4].Truong_22 != UC_2225_1.uC_2225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[4].Truong_23 != UC_2225_1.uC_2225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[4].Truong_24 != UC_2225_1.uC_2225_Item5.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser1[4].Truong_25 != UC_2225_1.uC_2225_Item5.txt_Truong_25.Text 
                        || DataUser1[4].Truong_26 != UC_2225_1.uC_2225_Item5.txt_Truong_26.Text)
                    {
                        flag_Phieu5_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User1Sai = false;
                    }

                    
                    //--Check User 2
                    //Check phiếu 1
                    if (DataUser2[0].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[0].Truong_02 != UC_2225_1.uC_2225_Item1.txt_Truong_02.Text 
                        || DataUser2[0].Truong_03 != UC_2225_1.uC_2225_Item1.txt_Truong_03.Text 
                        || DataUser2[0].Truong_04_1 != UC_2225_1.uC_2225_Item1.txt_Truong_04_1.Text 
                        || DataUser2[0].Truong_04_2 != UC_2225_1.uC_2225_Item1.txt_Truong_04_2.Text 
                        || DataUser2[0].Truong_04_3 != UC_2225_1.uC_2225_Item1.txt_Truong_04_3.Text 
                        || DataUser2[0].Truong_05_1 != UC_2225_1.uC_2225_Item1.txt_Truong_05_1.Text 
                        || DataUser2[0].Truong_05_2 != UC_2225_1.uC_2225_Item1.txt_Truong_05_2.Text 
                        || DataUser2[0].Truong_06 != UC_2225_1.uC_2225_Item1.txt_Truong_06.Text 
                        || DataUser2[0].Truong_07 != UC_2225_1.uC_2225_Item1.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[0].Truong_08 != UC_2225_1.uC_2225_Item1.txt_Truong_08.Text 
                        || DataUser2[0].Truong_09 != UC_2225_1.uC_2225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[0].Truong_10 != UC_2225_1.uC_2225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[0].Truong_11 != UC_2225_1.uC_2225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[0].Truong_12 != UC_2225_1.uC_2225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[0].Truong_13 != UC_2225_1.uC_2225_Item1.txt_Truong_13.Text 
                        || DataUser2[0].Truong_14 != UC_2225_1.uC_2225_Item1.txt_Truong_14.Text 
                        || DataUser2[0].Truong_15 != UC_2225_1.uC_2225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[0].Truong_16 != UC_2225_1.uC_2225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[0].Truong_17 != UC_2225_1.uC_2225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[0].Truong_18 != UC_2225_1.uC_2225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[0].Truong_19 != UC_2225_1.uC_2225_Item1.txt_Truong_19.Text 
                        || DataUser2[0].Truong_20 != UC_2225_1.uC_2225_Item1.txt_Truong_20.Text 
                        || DataUser2[0].Truong_21 != UC_2225_1.uC_2225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[0].Truong_22 != UC_2225_1.uC_2225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[0].Truong_23 != UC_2225_1.uC_2225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[0].Truong_24 != UC_2225_1.uC_2225_Item1.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[0].Truong_25 != UC_2225_1.uC_2225_Item1.txt_Truong_25.Text
                        || DataUser2[0].Truong_26 != UC_2225_1.uC_2225_Item1.txt_Truong_26.Text)
                    {
                        flag_Phieu1_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User2Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser2[1].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[1].Truong_02 != UC_2225_1.uC_2225_Item2.txt_Truong_02.Text 
                        || DataUser2[1].Truong_03 != UC_2225_1.uC_2225_Item2.txt_Truong_03.Text 
                        || DataUser2[1].Truong_04_1 != UC_2225_1.uC_2225_Item2.txt_Truong_04_1.Text 
                        || DataUser2[1].Truong_04_2 != UC_2225_1.uC_2225_Item2.txt_Truong_04_2.Text 
                        || DataUser2[1].Truong_04_3 != UC_2225_1.uC_2225_Item2.txt_Truong_04_3.Text 
                        || DataUser2[1].Truong_05_1 != UC_2225_1.uC_2225_Item2.txt_Truong_05_1.Text 
                        || DataUser2[1].Truong_05_2 != UC_2225_1.uC_2225_Item2.txt_Truong_05_2.Text 
                        || DataUser2[1].Truong_06 != UC_2225_1.uC_2225_Item2.txt_Truong_06.Text  
                        || DataUser2[1].Truong_07 != UC_2225_1.uC_2225_Item2.txt_Truong_07.Text.Replace(",", "")  
                        || DataUser2[1].Truong_08 != UC_2225_1.uC_2225_Item2.txt_Truong_08.Text 
                        || DataUser2[1].Truong_09 != UC_2225_1.uC_2225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[1].Truong_10 != UC_2225_1.uC_2225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[1].Truong_11 != UC_2225_1.uC_2225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[1].Truong_12 != UC_2225_1.uC_2225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[1].Truong_13 != UC_2225_1.uC_2225_Item2.txt_Truong_13.Text 
                        || DataUser2[1].Truong_14 != UC_2225_1.uC_2225_Item2.txt_Truong_14.Text 
                        || DataUser2[1].Truong_15 != UC_2225_1.uC_2225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[1].Truong_16 != UC_2225_1.uC_2225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[1].Truong_17 != UC_2225_1.uC_2225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[1].Truong_18 != UC_2225_1.uC_2225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[1].Truong_19 != UC_2225_1.uC_2225_Item2.txt_Truong_19.Text 
                        || DataUser2[1].Truong_20 != UC_2225_1.uC_2225_Item2.txt_Truong_20.Text 
                        || DataUser2[1].Truong_21 != UC_2225_1.uC_2225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[1].Truong_22 != UC_2225_1.uC_2225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[1].Truong_23 != UC_2225_1.uC_2225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[1].Truong_24 != UC_2225_1.uC_2225_Item2.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser2[1].Truong_25 != UC_2225_1.uC_2225_Item2.txt_Truong_25.Text 
                        || DataUser2[1].Truong_26 != UC_2225_1.uC_2225_Item2.txt_Truong_26.Text)
                    {
                        flag_Phieu2_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User2Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser2[2].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[2].Truong_02 != UC_2225_1.uC_2225_Item3.txt_Truong_02.Text 
                        || DataUser2[2].Truong_03 != UC_2225_1.uC_2225_Item3.txt_Truong_03.Text 
                        || DataUser2[2].Truong_04_1 != UC_2225_1.uC_2225_Item3.txt_Truong_04_1.Text 
                        || DataUser2[2].Truong_04_2 != UC_2225_1.uC_2225_Item3.txt_Truong_04_2.Text 
                        || DataUser2[2].Truong_04_3 != UC_2225_1.uC_2225_Item3.txt_Truong_04_3.Text 
                        || DataUser2[2].Truong_05_1 != UC_2225_1.uC_2225_Item3.txt_Truong_05_1.Text 
                        || DataUser2[2].Truong_05_2 != UC_2225_1.uC_2225_Item3.txt_Truong_05_2.Text 
                        || DataUser2[2].Truong_06 != UC_2225_1.uC_2225_Item3.txt_Truong_06.Text 
                        || DataUser2[2].Truong_07 != UC_2225_1.uC_2225_Item3.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[2].Truong_08 != UC_2225_1.uC_2225_Item3.txt_Truong_08.Text 
                        || DataUser2[2].Truong_09 != UC_2225_1.uC_2225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[2].Truong_10 != UC_2225_1.uC_2225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[2].Truong_11 != UC_2225_1.uC_2225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[2].Truong_12 != UC_2225_1.uC_2225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[2].Truong_13 != UC_2225_1.uC_2225_Item3.txt_Truong_13.Text 
                        || DataUser2[2].Truong_14 != UC_2225_1.uC_2225_Item3.txt_Truong_14.Text 
                        || DataUser2[2].Truong_15 != UC_2225_1.uC_2225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[2].Truong_16 != UC_2225_1.uC_2225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[2].Truong_17 != UC_2225_1.uC_2225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[2].Truong_18 != UC_2225_1.uC_2225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[2].Truong_19 != UC_2225_1.uC_2225_Item3.txt_Truong_19.Text 
                        || DataUser2[2].Truong_20 != UC_2225_1.uC_2225_Item3.txt_Truong_20.Text 
                        || DataUser2[2].Truong_21 != UC_2225_1.uC_2225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[2].Truong_22 != UC_2225_1.uC_2225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[2].Truong_23 != UC_2225_1.uC_2225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[2].Truong_24 != UC_2225_1.uC_2225_Item3.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser2[2].Truong_25 != UC_2225_1.uC_2225_Item3.txt_Truong_25.Text 
                        || DataUser2[2].Truong_26 != UC_2225_1.uC_2225_Item3.txt_Truong_26.Text)
                    {
                        flag_Phieu3_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User2Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser2[3].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[3].Truong_02 != UC_2225_1.uC_2225_Item4.txt_Truong_02.Text 
                        || DataUser2[3].Truong_03 != UC_2225_1.uC_2225_Item4.txt_Truong_03.Text 
                        || DataUser2[3].Truong_04_1 != UC_2225_1.uC_2225_Item4.txt_Truong_04_1.Text 
                        || DataUser2[3].Truong_04_2 != UC_2225_1.uC_2225_Item4.txt_Truong_04_2.Text 
                        || DataUser2[3].Truong_04_3 != UC_2225_1.uC_2225_Item4.txt_Truong_04_3.Text 
                        || DataUser2[3].Truong_05_1 != UC_2225_1.uC_2225_Item4.txt_Truong_05_1.Text 
                        || DataUser2[3].Truong_05_2 != UC_2225_1.uC_2225_Item4.txt_Truong_05_2.Text 
                        || DataUser2[3].Truong_06 != UC_2225_1.uC_2225_Item4.txt_Truong_06.Text 
                        || DataUser2[3].Truong_07 != UC_2225_1.uC_2225_Item4.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[3].Truong_08 != UC_2225_1.uC_2225_Item4.txt_Truong_08.Text 
                        || DataUser2[3].Truong_09 != UC_2225_1.uC_2225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[3].Truong_10 != UC_2225_1.uC_2225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[3].Truong_11 != UC_2225_1.uC_2225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[3].Truong_12 != UC_2225_1.uC_2225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[3].Truong_13 != UC_2225_1.uC_2225_Item4.txt_Truong_13.Text 
                        || DataUser2[3].Truong_14 != UC_2225_1.uC_2225_Item4.txt_Truong_14.Text 
                        || DataUser2[3].Truong_15 != UC_2225_1.uC_2225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[3].Truong_16 != UC_2225_1.uC_2225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[3].Truong_17 != UC_2225_1.uC_2225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[3].Truong_18 != UC_2225_1.uC_2225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[3].Truong_19 != UC_2225_1.uC_2225_Item4.txt_Truong_19.Text 
                        || DataUser2[3].Truong_20 != UC_2225_1.uC_2225_Item4.txt_Truong_20.Text 
                        || DataUser2[3].Truong_21 != UC_2225_1.uC_2225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[3].Truong_22 != UC_2225_1.uC_2225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[3].Truong_23 != UC_2225_1.uC_2225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[3].Truong_24 != UC_2225_1.uC_2225_Item4.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser2[3].Truong_25 != UC_2225_1.uC_2225_Item4.txt_Truong_25.Text 
                        || DataUser2[3].Truong_26!= UC_2225_1.uC_2225_Item4.txt_Truong_26.Text
                        || DataUser2[3].Truong_Flag != UC_2225_1.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User2Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser2[4].Truong_01 != txt_Truong_01_User1.Text 
                        || DataUser2[4].Truong_02 != UC_2225_1.uC_2225_Item5.txt_Truong_02.Text 
                        || DataUser2[4].Truong_03 != UC_2225_1.uC_2225_Item5.txt_Truong_03.Text 
                        || DataUser2[4].Truong_04_1 != UC_2225_1.uC_2225_Item5.txt_Truong_04_1.Text 
                        || DataUser2[4].Truong_04_2 != UC_2225_1.uC_2225_Item5.txt_Truong_04_2.Text 
                        || DataUser2[4].Truong_04_3 != UC_2225_1.uC_2225_Item5.txt_Truong_04_3.Text 
                        || DataUser2[4].Truong_05_1 != UC_2225_1.uC_2225_Item5.txt_Truong_05_1.Text 
                        || DataUser2[4].Truong_05_2 != UC_2225_1.uC_2225_Item5.txt_Truong_05_2.Text 
                        || DataUser2[4].Truong_06 != UC_2225_1.uC_2225_Item5.txt_Truong_06.Text 
                        || DataUser2[4].Truong_07 != UC_2225_1.uC_2225_Item5.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[4].Truong_08 != UC_2225_1.uC_2225_Item5.txt_Truong_08.Text 
                        || DataUser2[4].Truong_09 != UC_2225_1.uC_2225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[4].Truong_10 != UC_2225_1.uC_2225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[4].Truong_11 != UC_2225_1.uC_2225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[4].Truong_12 != UC_2225_1.uC_2225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[4].Truong_13 != UC_2225_1.uC_2225_Item5.txt_Truong_13.Text 
                        || DataUser2[4].Truong_14 != UC_2225_1.uC_2225_Item5.txt_Truong_14.Text 
                        || DataUser2[4].Truong_15 != UC_2225_1.uC_2225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[4].Truong_16 != UC_2225_1.uC_2225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[4].Truong_17 != UC_2225_1.uC_2225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[4].Truong_18 != UC_2225_1.uC_2225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[4].Truong_19 != UC_2225_1.uC_2225_Item5.txt_Truong_19.Text 
                        || DataUser2[4].Truong_20 != UC_2225_1.uC_2225_Item5.txt_Truong_20.Text 
                        || DataUser2[4].Truong_21 != UC_2225_1.uC_2225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[4].Truong_22 != UC_2225_1.uC_2225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[4].Truong_23 != UC_2225_1.uC_2225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[4].Truong_24 != UC_2225_1.uC_2225_Item5.txt_Truong_24.Text.Replace(",", "") 
                        || DataUser2[4].Truong_25 != UC_2225_1.uC_2225_Item5.txt_Truong_25.Text 
                        || DataUser2[4].Truong_26 != UC_2225_1.uC_2225_Item5.txt_Truong_26.Text)
                    {
                        flag_Phieu5_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User2Sai = false;
                    }
                    if (this.fLagRefresh)
                    {
                        this.UC_2225_1.Save2225_Check(this.fbatchRefresh, this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                    else
                    {
                        this.UC_2225_1.Save2225_Check(this.cbb_Batch_Check.SelectedValue+"", this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                }
            }
            fLagRefresh = false;
            fbatchRefresh = "";
            ResetData();
            if (Global.RunUpdateVersion())
                Application.Exit(); ;
            string temp = GetImage();
            if (temp == "NULL")
            {
                uc_PictureBox1.imageBox1.Image = null;
                MessageBox.Show(@"Batch '" + cbb_Batch_Check.Text + "' đã hoàn thành");
                LoadBatchMoi();
                return;
            }
            else if (temp == "Error")
            {
                MessageBox.Show("Lỗi load hình");
                VisibleButtonSave();
                return;
            }
            else
            {
                Load_DeSo(cbb_Batch_Check.SelectedValue + "", lb_Image.Text);
                VisibleButtonSave();
            }
        }

        private void btn_Luu_DeSo2_Click(object sender, EventArgs e)
        {
            if (Global.StrCheck == "CHECKDESO")
            {
                if (string.IsNullOrEmpty(this.txt_Truong_01_User2.Text))
                {
                    MessageBox.Show("Bạn đang để trống trường 1. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (this.CheckLoaiPhieu(this.txt_Truong_01_User2.Text) && (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No))
                {
                    return;
                }
                bool flag_Phieu1_User1Sai = false;
                bool flag_Phieu2_User1Sai = false;
                bool flag_Phieu3_User1Sai = false;
                bool flag_Phieu4_User1Sai = false;
                bool flag_Phieu5_User1Sai = false;
                bool flag_Phieu1_User2Sai = false;
                bool flag_Phieu2_User2Sai = false;
                bool flag_Phieu3_User2Sai = false;
                bool flag_Phieu4_User2Sai = false;
                bool flag_Phieu5_User2Sai = false;
                if (this.txt_Truong_01_User2.Text == "225")
                {
                    if (this.UC_225_2.IsEmpty() && (MessageBox.Show("Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
                    {
                        return;
                    }
                    //--Check user 1
                    //Check phiếu 1
                    if (DataUser1[0].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[0].Truong_02 != UC_225_2.uC_225_Item1.txt_Truong_02.Text 
                        || DataUser1[0].Truong_03 != UC_225_2.uC_225_Item1.txt_Truong_03.Text 
                        || DataUser1[0].Truong_04_1 != UC_225_2.uC_225_Item1.txt_Truong_04_1.Text 
                        || DataUser1[0].Truong_04_2 != UC_225_2.uC_225_Item1.txt_Truong_04_2.Text 
                        || DataUser1[0].Truong_04_3 != UC_225_2.uC_225_Item1.txt_Truong_04_3.Text 
                        || DataUser1[0].Truong_05_1 != UC_225_2.uC_225_Item1.txt_Truong_05_1.Text 
                        || DataUser1[0].Truong_05_2 != UC_225_2.uC_225_Item1.txt_Truong_05_2.Text 
                        || DataUser1[0].Truong_08 != UC_225_2.uC_225_Item1.txt_Truong_08.Text 
                        || DataUser1[0].Truong_09 != UC_225_2.uC_225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[0].Truong_10 != UC_225_2.uC_225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[0].Truong_11 != UC_225_2.uC_225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[0].Truong_12 != UC_225_2.uC_225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[0].Truong_14 != UC_225_2.uC_225_Item1.txt_Truong_14.Text 
                        || DataUser1[0].Truong_15 != UC_225_2.uC_225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[0].Truong_16 != UC_225_2.uC_225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[0].Truong_17 != UC_225_2.uC_225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[0].Truong_18 != UC_225_2.uC_225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[0].Truong_19 != UC_225_2.uC_225_Item1.txt_Truong_19.Text 
                        || DataUser1[0].Truong_20 != UC_225_2.uC_225_Item1.txt_Truong_20.Text 
                        || DataUser1[0].Truong_21 != UC_225_2.uC_225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[0].Truong_22 != UC_225_2.uC_225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[0].Truong_23 != UC_225_2.uC_225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[0].Truong_24 != UC_225_2.uC_225_Item1.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu1_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User1Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser1[1].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[1].Truong_02 != UC_225_2.uC_225_Item2.txt_Truong_02.Text 
                        || DataUser1[1].Truong_03 != UC_225_2.uC_225_Item2.txt_Truong_03.Text 
                        || DataUser1[1].Truong_04_1 != UC_225_2.uC_225_Item2.txt_Truong_04_1.Text 
                        || DataUser1[1].Truong_04_2 != UC_225_2.uC_225_Item2.txt_Truong_04_2.Text 
                        || DataUser1[1].Truong_04_3 != UC_225_2.uC_225_Item2.txt_Truong_04_3.Text 
                        || DataUser1[1].Truong_05_1 != UC_225_2.uC_225_Item2.txt_Truong_05_1.Text 
                        || DataUser1[1].Truong_05_2 != UC_225_2.uC_225_Item2.txt_Truong_05_2.Text 
                        || DataUser1[1].Truong_08 != UC_225_2.uC_225_Item2.txt_Truong_08.Text 
                        || DataUser1[1].Truong_09 != UC_225_2.uC_225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[1].Truong_10 != UC_225_2.uC_225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[1].Truong_11 != UC_225_2.uC_225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[1].Truong_12 != UC_225_2.uC_225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[1].Truong_14 != UC_225_2.uC_225_Item2.txt_Truong_14.Text 
                        || DataUser1[1].Truong_15 != UC_225_2.uC_225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[1].Truong_16 != UC_225_2.uC_225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[1].Truong_17 != UC_225_2.uC_225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[1].Truong_18 != UC_225_2.uC_225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[1].Truong_19 != UC_225_2.uC_225_Item2.txt_Truong_19.Text 
                        || DataUser1[1].Truong_20 != UC_225_2.uC_225_Item2.txt_Truong_20.Text 
                        || DataUser1[1].Truong_21 != UC_225_2.uC_225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[1].Truong_22 != UC_225_2.uC_225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[1].Truong_23 != UC_225_2.uC_225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[1].Truong_24 != UC_225_2.uC_225_Item2.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu2_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User1Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser1[2].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[2].Truong_02 != UC_225_2.uC_225_Item3.txt_Truong_02.Text 
                        || DataUser1[2].Truong_03 != UC_225_2.uC_225_Item3.txt_Truong_03.Text 
                        || DataUser1[2].Truong_04_1 != UC_225_2.uC_225_Item3.txt_Truong_04_1.Text 
                        || DataUser1[2].Truong_04_2 != UC_225_2.uC_225_Item3.txt_Truong_04_2.Text 
                        || DataUser1[2].Truong_04_3 != UC_225_2.uC_225_Item3.txt_Truong_04_3.Text 
                        || DataUser1[2].Truong_05_1 != UC_225_2.uC_225_Item3.txt_Truong_05_1.Text 
                        || DataUser1[2].Truong_05_2 != UC_225_2.uC_225_Item3.txt_Truong_05_2.Text 
                        || DataUser1[2].Truong_08 != UC_225_2.uC_225_Item3.txt_Truong_08.Text 
                        || DataUser1[2].Truong_09 != UC_225_2.uC_225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[2].Truong_10 != UC_225_2.uC_225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[2].Truong_11 != UC_225_2.uC_225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[2].Truong_12 != UC_225_2.uC_225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[2].Truong_14 != UC_225_2.uC_225_Item3.txt_Truong_14.Text 
                        || DataUser1[2].Truong_15 != UC_225_2.uC_225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[2].Truong_16 != UC_225_2.uC_225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[2].Truong_17 != UC_225_2.uC_225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[2].Truong_18 != UC_225_2.uC_225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[2].Truong_19 != UC_225_2.uC_225_Item3.txt_Truong_19.Text 
                        || DataUser1[2].Truong_20 != UC_225_2.uC_225_Item3.txt_Truong_20.Text 
                        || DataUser1[2].Truong_21 != UC_225_2.uC_225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[2].Truong_22 != UC_225_2.uC_225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[2].Truong_23 != UC_225_2.uC_225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[2].Truong_24 != UC_225_2.uC_225_Item3.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu3_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User1Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser1[3].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[3].Truong_02 != UC_225_2.uC_225_Item4.txt_Truong_02.Text 
                        || DataUser1[3].Truong_03 != UC_225_2.uC_225_Item4.txt_Truong_03.Text 
                        || DataUser1[3].Truong_04_1 != UC_225_2.uC_225_Item4.txt_Truong_04_1.Text 
                        || DataUser1[3].Truong_04_2 != UC_225_2.uC_225_Item4.txt_Truong_04_2.Text 
                        || DataUser1[3].Truong_04_3 != UC_225_2.uC_225_Item4.txt_Truong_04_3.Text 
                        || DataUser1[3].Truong_05_1 != UC_225_2.uC_225_Item4.txt_Truong_05_1.Text 
                        || DataUser1[3].Truong_05_2 != UC_225_2.uC_225_Item4.txt_Truong_05_2.Text 
                        || DataUser1[3].Truong_08 != UC_225_2.uC_225_Item4.txt_Truong_08.Text 
                        || DataUser1[3].Truong_09 != UC_225_2.uC_225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[3].Truong_10 != UC_225_2.uC_225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[3].Truong_11 != UC_225_2.uC_225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[3].Truong_12 != UC_225_2.uC_225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[3].Truong_14 != UC_225_2.uC_225_Item4.txt_Truong_14.Text 
                        || DataUser1[3].Truong_15 != UC_225_2.uC_225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[3].Truong_16 != UC_225_2.uC_225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[3].Truong_17 != UC_225_2.uC_225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[3].Truong_18 != UC_225_2.uC_225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[3].Truong_19 != UC_225_2.uC_225_Item4.txt_Truong_19.Text 
                        || DataUser1[3].Truong_20 != UC_225_2.uC_225_Item4.txt_Truong_20.Text 
                        || DataUser1[3].Truong_21 != UC_225_2.uC_225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[3].Truong_22 != UC_225_2.uC_225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[3].Truong_23 != UC_225_2.uC_225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[3].Truong_24 != UC_225_2.uC_225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[3].Truong_Flag != UC_225_2.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User1Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser1[4].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[4].Truong_02 != UC_225_2.uC_225_Item5.txt_Truong_02.Text 
                        || DataUser1[4].Truong_03 != UC_225_2.uC_225_Item5.txt_Truong_03.Text 
                        || DataUser1[4].Truong_04_1 != UC_225_2.uC_225_Item5.txt_Truong_04_1.Text 
                        || DataUser1[4].Truong_04_2 != UC_225_2.uC_225_Item5.txt_Truong_04_2.Text 
                        || DataUser1[4].Truong_04_3 != UC_225_2.uC_225_Item5.txt_Truong_04_3.Text 
                        || DataUser1[4].Truong_05_1 != UC_225_2.uC_225_Item5.txt_Truong_05_1.Text 
                        || DataUser1[4].Truong_05_2 != UC_225_2.uC_225_Item5.txt_Truong_05_2.Text 
                        || DataUser1[4].Truong_08 != UC_225_2.uC_225_Item5.txt_Truong_08.Text 
                        || DataUser1[4].Truong_09 != UC_225_2.uC_225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[4].Truong_10 != UC_225_2.uC_225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[4].Truong_11 != UC_225_2.uC_225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[4].Truong_12 != UC_225_2.uC_225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[4].Truong_14 != UC_225_2.uC_225_Item5.txt_Truong_14.Text 
                        || DataUser1[4].Truong_15 != UC_225_2.uC_225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[4].Truong_16 != UC_225_2.uC_225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[4].Truong_17 != UC_225_2.uC_225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[4].Truong_18 != UC_225_2.uC_225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[4].Truong_19 != UC_225_2.uC_225_Item5.txt_Truong_19.Text 
                        || DataUser1[4].Truong_20 != UC_225_2.uC_225_Item5.txt_Truong_20.Text 
                        || DataUser1[4].Truong_21 != UC_225_2.uC_225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[4].Truong_22 != UC_225_2.uC_225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[4].Truong_23 != UC_225_2.uC_225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[4].Truong_24 != UC_225_2.uC_225_Item5.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu5_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User1Sai = false;
                    }
                    //--Check user 2
                    //Check phiếu 1
                    if (DataUser2[0].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[0].Truong_02 != UC_225_2.uC_225_Item1.txt_Truong_02.Text 
                        || DataUser2[0].Truong_03 != UC_225_2.uC_225_Item1.txt_Truong_03.Text 
                        || DataUser2[0].Truong_04_1 != UC_225_2.uC_225_Item1.txt_Truong_04_1.Text 
                        || DataUser2[0].Truong_04_2 != UC_225_2.uC_225_Item1.txt_Truong_04_2.Text 
                        || DataUser2[0].Truong_04_3 != UC_225_2.uC_225_Item1.txt_Truong_04_3.Text 
                        || DataUser2[0].Truong_05_1 != UC_225_2.uC_225_Item1.txt_Truong_05_1.Text 
                        || DataUser2[0].Truong_05_2 != UC_225_2.uC_225_Item1.txt_Truong_05_2.Text 
                        || DataUser2[0].Truong_08 != UC_225_2.uC_225_Item1.txt_Truong_08.Text 
                        || DataUser2[0].Truong_09 != UC_225_2.uC_225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[0].Truong_10 != UC_225_2.uC_225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[0].Truong_11 != UC_225_2.uC_225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[0].Truong_12 != UC_225_2.uC_225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[0].Truong_14 != UC_225_2.uC_225_Item1.txt_Truong_14.Text 
                        || DataUser2[0].Truong_15 != UC_225_2.uC_225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[0].Truong_16 != UC_225_2.uC_225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[0].Truong_17 != UC_225_2.uC_225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[0].Truong_18 != UC_225_2.uC_225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[0].Truong_19 != UC_225_2.uC_225_Item1.txt_Truong_19.Text 
                        || DataUser2[0].Truong_20 != UC_225_2.uC_225_Item1.txt_Truong_20.Text 
                        || DataUser2[0].Truong_21 != UC_225_2.uC_225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[0].Truong_22 != UC_225_2.uC_225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[0].Truong_23 != UC_225_2.uC_225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[0].Truong_24 != UC_225_2.uC_225_Item1.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu1_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User2Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser2[1].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[1].Truong_02 != UC_225_2.uC_225_Item2.txt_Truong_02.Text 
                        || DataUser2[1].Truong_03 != UC_225_2.uC_225_Item2.txt_Truong_03.Text 
                        || DataUser2[1].Truong_04_1 != UC_225_2.uC_225_Item2.txt_Truong_04_1.Text 
                        || DataUser2[1].Truong_04_2 != UC_225_2.uC_225_Item2.txt_Truong_04_2.Text 
                        || DataUser2[1].Truong_04_3 != UC_225_2.uC_225_Item2.txt_Truong_04_3.Text 
                        || DataUser2[1].Truong_05_1 != UC_225_2.uC_225_Item2.txt_Truong_05_1.Text 
                        || DataUser2[1].Truong_05_2 != UC_225_2.uC_225_Item2.txt_Truong_05_2.Text 
                        || DataUser2[1].Truong_08 != UC_225_2.uC_225_Item2.txt_Truong_08.Text 
                        || DataUser2[1].Truong_09 != UC_225_2.uC_225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[1].Truong_10 != UC_225_2.uC_225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[1].Truong_11 != UC_225_2.uC_225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[1].Truong_12 != UC_225_2.uC_225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[1].Truong_14 != UC_225_2.uC_225_Item2.txt_Truong_14.Text 
                        || DataUser2[1].Truong_15 != UC_225_2.uC_225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[1].Truong_16 != UC_225_2.uC_225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[1].Truong_17 != UC_225_2.uC_225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[1].Truong_18 != UC_225_2.uC_225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[1].Truong_19 != UC_225_2.uC_225_Item2.txt_Truong_19.Text 
                        || DataUser2[1].Truong_20 != UC_225_2.uC_225_Item2.txt_Truong_20.Text 
                        || DataUser2[1].Truong_21 != UC_225_2.uC_225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[1].Truong_22 != UC_225_2.uC_225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[1].Truong_23 != UC_225_2.uC_225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[1].Truong_24 != UC_225_2.uC_225_Item2.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu2_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User2Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser2[2].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[2].Truong_02 != UC_225_2.uC_225_Item3.txt_Truong_02.Text 
                        || DataUser2[2].Truong_03 != UC_225_2.uC_225_Item3.txt_Truong_03.Text 
                        || DataUser2[2].Truong_04_1 != UC_225_2.uC_225_Item3.txt_Truong_04_1.Text 
                        || DataUser2[2].Truong_04_2 != UC_225_2.uC_225_Item3.txt_Truong_04_2.Text 
                        || DataUser2[2].Truong_04_3 != UC_225_2.uC_225_Item3.txt_Truong_04_3.Text 
                        || DataUser2[2].Truong_05_1 != UC_225_2.uC_225_Item3.txt_Truong_05_1.Text 
                        || DataUser2[2].Truong_05_2 != UC_225_2.uC_225_Item3.txt_Truong_05_2.Text 
                        || DataUser2[2].Truong_08 != UC_225_2.uC_225_Item3.txt_Truong_08.Text 
                        || DataUser2[2].Truong_09 != UC_225_2.uC_225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[2].Truong_10 != UC_225_2.uC_225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[2].Truong_11 != UC_225_2.uC_225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[2].Truong_12 != UC_225_2.uC_225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[2].Truong_14 != UC_225_2.uC_225_Item3.txt_Truong_14.Text 
                        || DataUser2[2].Truong_15 != UC_225_2.uC_225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[2].Truong_16 != UC_225_2.uC_225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[2].Truong_17 != UC_225_2.uC_225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[2].Truong_18 != UC_225_2.uC_225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[2].Truong_19 != UC_225_2.uC_225_Item3.txt_Truong_19.Text 
                        || DataUser2[2].Truong_20 != UC_225_2.uC_225_Item3.txt_Truong_20.Text 
                        || DataUser2[2].Truong_21 != UC_225_2.uC_225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[2].Truong_22 != UC_225_2.uC_225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[2].Truong_23 != UC_225_2.uC_225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[2].Truong_24 != UC_225_2.uC_225_Item3.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu3_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User2Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser2[3].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[3].Truong_02 != UC_225_2.uC_225_Item4.txt_Truong_02.Text 
                        || DataUser2[3].Truong_03 != UC_225_2.uC_225_Item4.txt_Truong_03.Text 
                        || DataUser2[3].Truong_04_1 != UC_225_2.uC_225_Item4.txt_Truong_04_1.Text 
                        || DataUser2[3].Truong_04_2 != UC_225_2.uC_225_Item4.txt_Truong_04_2.Text 
                        || DataUser2[3].Truong_04_3 != UC_225_2.uC_225_Item4.txt_Truong_04_3.Text 
                        || DataUser2[3].Truong_05_1 != UC_225_2.uC_225_Item4.txt_Truong_05_1.Text 
                        || DataUser2[3].Truong_05_2 != UC_225_2.uC_225_Item4.txt_Truong_05_2.Text 
                        || DataUser2[3].Truong_08 != UC_225_2.uC_225_Item4.txt_Truong_08.Text 
                        || DataUser2[3].Truong_09 != UC_225_2.uC_225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[3].Truong_10 != UC_225_2.uC_225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[3].Truong_11 != UC_225_2.uC_225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[3].Truong_12 != UC_225_2.uC_225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[3].Truong_14 != UC_225_2.uC_225_Item4.txt_Truong_14.Text 
                        || DataUser2[3].Truong_15 != UC_225_2.uC_225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[3].Truong_16 != UC_225_2.uC_225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[3].Truong_17 != UC_225_2.uC_225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[3].Truong_18 != UC_225_2.uC_225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[3].Truong_19 != UC_225_2.uC_225_Item4.txt_Truong_19.Text 
                        || DataUser2[3].Truong_20 != UC_225_2.uC_225_Item4.txt_Truong_20.Text 
                        || DataUser2[3].Truong_21 != UC_225_2.uC_225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[3].Truong_22 != UC_225_2.uC_225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[3].Truong_23 != UC_225_2.uC_225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[3].Truong_24 != UC_225_2.uC_225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[3].Truong_Flag != UC_225_2.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User2Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser2[4].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[4].Truong_02 != UC_225_2.uC_225_Item5.txt_Truong_02.Text 
                        || DataUser2[4].Truong_03 != UC_225_2.uC_225_Item5.txt_Truong_03.Text 
                        || DataUser2[4].Truong_04_1 != UC_225_2.uC_225_Item5.txt_Truong_04_1.Text 
                        || DataUser2[4].Truong_04_2 != UC_225_2.uC_225_Item5.txt_Truong_04_2.Text 
                        || DataUser2[4].Truong_04_3 != UC_225_2.uC_225_Item5.txt_Truong_04_3.Text 
                        || DataUser2[4].Truong_05_1 != UC_225_2.uC_225_Item5.txt_Truong_05_1.Text 
                        || DataUser2[4].Truong_05_2 != UC_225_2.uC_225_Item5.txt_Truong_05_2.Text 
                        || DataUser2[4].Truong_08 != UC_225_2.uC_225_Item5.txt_Truong_08.Text 
                        || DataUser2[4].Truong_09 != UC_225_2.uC_225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[4].Truong_10 != UC_225_2.uC_225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[4].Truong_11 != UC_225_2.uC_225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[4].Truong_12 != UC_225_2.uC_225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[4].Truong_14 != UC_225_2.uC_225_Item5.txt_Truong_14.Text 
                        || DataUser2[4].Truong_15 != UC_225_2.uC_225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[4].Truong_16 != UC_225_2.uC_225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[4].Truong_17 != UC_225_2.uC_225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[4].Truong_18 != UC_225_2.uC_225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[4].Truong_19 != UC_225_2.uC_225_Item5.txt_Truong_19.Text 
                        || DataUser2[4].Truong_20 != UC_225_2.uC_225_Item5.txt_Truong_20.Text 
                        || DataUser2[4].Truong_21 != UC_225_2.uC_225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[4].Truong_22 != UC_225_2.uC_225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[4].Truong_23 != UC_225_2.uC_225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[4].Truong_24 != UC_225_2.uC_225_Item5.txt_Truong_24.Text.Replace(",", ""))
                    {
                        flag_Phieu5_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User2Sai = false;
                    }
                    if (this.fLagRefresh)
                    {
                        this.UC_225_2.Save225_Check(this.fbatchRefresh, this.lb_Image.Text, this.txt_Truong_01_User2.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                    else
                    {
                        this.UC_225_2.Save225_Check(this.cbb_Batch_Check.SelectedValue+"", this.lb_Image.Text, this.txt_Truong_01_User2.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                }
                else
                {
                    if (this.UC_2225_2.IsEmpty() && (MessageBox.Show("Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
                    {
                        return;
                    }
                    //--Check User 1
                    //Check phiếu 1
                    if (DataUser1[0].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[0].Truong_02 != UC_2225_2.uC_2225_Item1.txt_Truong_02.Text 
                        || DataUser1[0].Truong_03 != UC_2225_2.uC_2225_Item1.txt_Truong_03.Text 
                        || DataUser1[0].Truong_04_1 != UC_2225_2.uC_2225_Item1.txt_Truong_04_1.Text 
                        || DataUser1[0].Truong_04_2 != UC_2225_2.uC_2225_Item1.txt_Truong_04_2.Text 
                        || DataUser1[0].Truong_04_3 != UC_2225_2.uC_2225_Item1.txt_Truong_04_3.Text 
                        || DataUser1[0].Truong_05_1 != UC_2225_2.uC_2225_Item1.txt_Truong_05_1.Text 
                        || DataUser1[0].Truong_05_2 != UC_2225_2.uC_2225_Item1.txt_Truong_05_2.Text 
                        || DataUser1[0].Truong_06 != UC_2225_2.uC_2225_Item1.txt_Truong_06.Text
                        || DataUser1[0].Truong_07 != UC_2225_2.uC_2225_Item1.txt_Truong_07.Text.Replace(",", "")
                        || DataUser1[0].Truong_08 != UC_2225_2.uC_2225_Item1.txt_Truong_08.Text
                        || DataUser1[0].Truong_09 != UC_2225_2.uC_2225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[0].Truong_10 != UC_2225_2.uC_2225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[0].Truong_11 != UC_2225_2.uC_2225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[0].Truong_12 != UC_2225_2.uC_2225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[0].Truong_13 != UC_2225_2.uC_2225_Item1.txt_Truong_13.Text 
                        || DataUser1[0].Truong_14 != UC_2225_2.uC_2225_Item1.txt_Truong_14.Text 
                        || DataUser1[0].Truong_15 != UC_2225_2.uC_2225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[0].Truong_16 != UC_2225_2.uC_2225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[0].Truong_17 != UC_2225_2.uC_2225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[0].Truong_18 != UC_2225_2.uC_2225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[0].Truong_19 != UC_2225_2.uC_2225_Item1.txt_Truong_19.Text 
                        || DataUser1[0].Truong_20 != UC_2225_2.uC_2225_Item1.txt_Truong_20.Text 
                        || DataUser1[0].Truong_21 != UC_2225_2.uC_2225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[0].Truong_22 != UC_2225_2.uC_2225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[0].Truong_23 != UC_2225_2.uC_2225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[0].Truong_24 != UC_2225_2.uC_2225_Item1.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[0].Truong_25 != UC_2225_2.uC_2225_Item1.txt_Truong_25.Text 
                        || DataUser1[0].Truong_26 != UC_2225_2.uC_2225_Item1.txt_Truong_26.Text )
                    {
                        flag_Phieu1_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User1Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser1[1].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[1].Truong_02 != UC_2225_2.uC_2225_Item2.txt_Truong_02.Text 
                        || DataUser1[1].Truong_03 != UC_2225_2.uC_2225_Item2.txt_Truong_03.Text 
                        || DataUser1[1].Truong_04_1 != UC_2225_2.uC_2225_Item2.txt_Truong_04_1.Text 
                        || DataUser1[1].Truong_04_2 != UC_2225_2.uC_2225_Item2.txt_Truong_04_2.Text 
                        || DataUser1[1].Truong_04_3 != UC_2225_2.uC_2225_Item2.txt_Truong_04_3.Text 
                        || DataUser1[1].Truong_05_1 != UC_2225_2.uC_2225_Item2.txt_Truong_05_1.Text 
                        || DataUser1[1].Truong_05_2 != UC_2225_2.uC_2225_Item2.txt_Truong_05_2.Text 
                        || DataUser1[1].Truong_06 != UC_2225_2.uC_2225_Item2.txt_Truong_06.Text 
                        || DataUser1[1].Truong_07 != UC_2225_2.uC_2225_Item2.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[1].Truong_08 != UC_2225_2.uC_2225_Item2.txt_Truong_08.Text 
                        || DataUser1[1].Truong_09 != UC_2225_2.uC_2225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[1].Truong_10 != UC_2225_2.uC_2225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[1].Truong_11 != UC_2225_2.uC_2225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[1].Truong_12 != UC_2225_2.uC_2225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[1].Truong_13 != UC_2225_2.uC_2225_Item2.txt_Truong_13.Text 
                        || DataUser1[1].Truong_14 != UC_2225_2.uC_2225_Item2.txt_Truong_14.Text 
                        || DataUser1[1].Truong_15 != UC_2225_2.uC_2225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[1].Truong_16 != UC_2225_2.uC_2225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[1].Truong_17 != UC_2225_2.uC_2225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[1].Truong_18 != UC_2225_2.uC_2225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[1].Truong_19 != UC_2225_2.uC_2225_Item2.txt_Truong_19.Text 
                        || DataUser1[1].Truong_20 != UC_2225_2.uC_2225_Item2.txt_Truong_20.Text 
                        || DataUser1[1].Truong_21 != UC_2225_2.uC_2225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[1].Truong_22 != UC_2225_2.uC_2225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[1].Truong_23 != UC_2225_2.uC_2225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[1].Truong_24 != UC_2225_2.uC_2225_Item2.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[1].Truong_25 != UC_2225_2.uC_2225_Item2.txt_Truong_25.Text 
                        || DataUser1[1].Truong_26 != UC_2225_2.uC_2225_Item2.txt_Truong_26.Text )
                    {
                        flag_Phieu2_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User1Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser1[2].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[2].Truong_02 != UC_2225_2.uC_2225_Item3.txt_Truong_02.Text 
                        || DataUser1[2].Truong_03 != UC_2225_2.uC_2225_Item3.txt_Truong_03.Text 
                        || DataUser1[2].Truong_04_1 != UC_2225_2.uC_2225_Item3.txt_Truong_04_1.Text 
                        || DataUser1[2].Truong_04_2 != UC_2225_2.uC_2225_Item3.txt_Truong_04_2.Text 
                        || DataUser1[2].Truong_04_3 != UC_2225_2.uC_2225_Item3.txt_Truong_04_3.Text 
                        || DataUser1[2].Truong_05_1 != UC_2225_2.uC_2225_Item3.txt_Truong_05_1.Text 
                        || DataUser1[2].Truong_05_2 != UC_2225_2.uC_2225_Item3.txt_Truong_05_2.Text 
                        || DataUser1[2].Truong_06 != UC_2225_2.uC_2225_Item3.txt_Truong_06.Text 
                        || DataUser1[2].Truong_07 != UC_2225_2.uC_2225_Item3.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[2].Truong_08 != UC_2225_2.uC_2225_Item3.txt_Truong_08.Text 
                        || DataUser1[2].Truong_09 != UC_2225_2.uC_2225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[2].Truong_10 != UC_2225_2.uC_2225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[2].Truong_11 != UC_2225_2.uC_2225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[2].Truong_12 != UC_2225_2.uC_2225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[2].Truong_13 != UC_2225_2.uC_2225_Item3.txt_Truong_13.Text 
                        || DataUser1[2].Truong_14 != UC_2225_2.uC_2225_Item3.txt_Truong_14.Text 
                        || DataUser1[2].Truong_15 != UC_2225_2.uC_2225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[2].Truong_16 != UC_2225_2.uC_2225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[2].Truong_17 != UC_2225_2.uC_2225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[2].Truong_18 != UC_2225_2.uC_2225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[2].Truong_19 != UC_2225_2.uC_2225_Item3.txt_Truong_19.Text 
                        || DataUser1[2].Truong_20 != UC_2225_2.uC_2225_Item3.txt_Truong_20.Text 
                        || DataUser1[2].Truong_21 != UC_2225_2.uC_2225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[2].Truong_22 != UC_2225_2.uC_2225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[2].Truong_23 != UC_2225_2.uC_2225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[2].Truong_24 != UC_2225_2.uC_2225_Item3.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[2].Truong_25 != UC_2225_2.uC_2225_Item3.txt_Truong_25.Text 
                        || DataUser1[2].Truong_26 != UC_2225_2.uC_2225_Item3.txt_Truong_26.Text)
                    {
                        flag_Phieu3_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User1Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser1[3].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[3].Truong_02 != UC_2225_2.uC_2225_Item4.txt_Truong_02.Text 
                        || DataUser1[3].Truong_03 != UC_2225_2.uC_2225_Item4.txt_Truong_03.Text 
                        || DataUser1[3].Truong_04_1 != UC_2225_2.uC_2225_Item4.txt_Truong_04_1.Text 
                        || DataUser1[3].Truong_04_2 != UC_2225_2.uC_2225_Item4.txt_Truong_04_2.Text 
                        || DataUser1[3].Truong_04_3 != UC_2225_2.uC_2225_Item4.txt_Truong_04_3.Text 
                        || DataUser1[3].Truong_05_1 != UC_2225_2.uC_2225_Item4.txt_Truong_05_1.Text 
                        || DataUser1[3].Truong_05_2 != UC_2225_2.uC_2225_Item4.txt_Truong_05_2.Text 
                        || DataUser1[3].Truong_06 != UC_2225_2.uC_2225_Item4.txt_Truong_06.Text 
                        || DataUser1[3].Truong_07 != UC_2225_2.uC_2225_Item4.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[3].Truong_08 != UC_2225_2.uC_2225_Item4.txt_Truong_08.Text 
                        || DataUser1[3].Truong_09 != UC_2225_2.uC_2225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[3].Truong_10 != UC_2225_2.uC_2225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[3].Truong_11 != UC_2225_2.uC_2225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[3].Truong_12 != UC_2225_2.uC_2225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[3].Truong_13 != UC_2225_2.uC_2225_Item4.txt_Truong_13.Text 
                        || DataUser1[3].Truong_14 != UC_2225_2.uC_2225_Item4.txt_Truong_14.Text 
                        || DataUser1[3].Truong_15 != UC_2225_2.uC_2225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[3].Truong_16 != UC_2225_2.uC_2225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[3].Truong_17 != UC_2225_2.uC_2225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[3].Truong_18 != UC_2225_2.uC_2225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[3].Truong_19 != UC_2225_2.uC_2225_Item4.txt_Truong_19.Text 
                        || DataUser1[3].Truong_20 != UC_2225_2.uC_2225_Item4.txt_Truong_20.Text 
                        || DataUser1[3].Truong_21 != UC_2225_2.uC_2225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[3].Truong_22 != UC_2225_2.uC_2225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[3].Truong_23 != UC_2225_2.uC_2225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[3].Truong_24 != UC_2225_2.uC_2225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[3].Truong_25 != UC_2225_2.uC_2225_Item4.txt_Truong_25.Text 
                        || DataUser1[3].Truong_26 != UC_2225_2.uC_2225_Item4.txt_Truong_26.Text
                        || DataUser1[3].Truong_Flag != UC_2225_2.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User1Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser1[4].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser1[4].Truong_02 != UC_2225_2.uC_2225_Item5.txt_Truong_02.Text 
                        || DataUser1[4].Truong_03 != UC_2225_2.uC_2225_Item5.txt_Truong_03.Text 
                        || DataUser1[4].Truong_04_1 != UC_2225_2.uC_2225_Item5.txt_Truong_04_1.Text 
                        || DataUser1[4].Truong_04_2 != UC_2225_2.uC_2225_Item5.txt_Truong_04_2.Text 
                        || DataUser1[4].Truong_04_3 != UC_2225_2.uC_2225_Item5.txt_Truong_04_3.Text 
                        || DataUser1[4].Truong_05_1 != UC_2225_2.uC_2225_Item5.txt_Truong_05_1.Text 
                        || DataUser1[4].Truong_05_2 != UC_2225_2.uC_2225_Item5.txt_Truong_05_2.Text 
                        || DataUser1[4].Truong_06 != UC_2225_2.uC_2225_Item5.txt_Truong_06.Text 
                        || DataUser1[4].Truong_07 != UC_2225_2.uC_2225_Item5.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser1[4].Truong_08 != UC_2225_2.uC_2225_Item5.txt_Truong_08.Text 
                        || DataUser1[4].Truong_09 != UC_2225_2.uC_2225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser1[4].Truong_10 != UC_2225_2.uC_2225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser1[4].Truong_11 != UC_2225_2.uC_2225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser1[4].Truong_12 != UC_2225_2.uC_2225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser1[4].Truong_13 != UC_2225_2.uC_2225_Item5.txt_Truong_13.Text 
                        || DataUser1[4].Truong_14 != UC_2225_2.uC_2225_Item5.txt_Truong_14.Text 
                        || DataUser1[4].Truong_15 != UC_2225_2.uC_2225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser1[4].Truong_16 != UC_2225_2.uC_2225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser1[4].Truong_17 != UC_2225_2.uC_2225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser1[4].Truong_18 != UC_2225_2.uC_2225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser1[4].Truong_19 != UC_2225_2.uC_2225_Item5.txt_Truong_19.Text 
                        || DataUser1[4].Truong_20 != UC_2225_2.uC_2225_Item5.txt_Truong_20.Text 
                        || DataUser1[4].Truong_21 != UC_2225_2.uC_2225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser1[4].Truong_22 != UC_2225_2.uC_2225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser1[4].Truong_23 != UC_2225_2.uC_2225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser1[4].Truong_24 != UC_2225_2.uC_2225_Item5.txt_Truong_24.Text.Replace(",", "")
                        || DataUser1[4].Truong_25 != UC_2225_2.uC_2225_Item5.txt_Truong_25.Text 
                        || DataUser1[4].Truong_26 != UC_2225_2.uC_2225_Item5.txt_Truong_26.Text)
                    {
                        flag_Phieu5_User1Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User1Sai = false;
                    }

                    
                    //--Check User 2
                    //Check phiếu 1
                    if (DataUser2[0].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[0].Truong_02 != UC_2225_2.uC_2225_Item1.txt_Truong_02.Text 
                        || DataUser2[0].Truong_03 != UC_2225_2.uC_2225_Item1.txt_Truong_03.Text 
                        || DataUser2[0].Truong_04_1 != UC_2225_2.uC_2225_Item1.txt_Truong_04_1.Text 
                        || DataUser2[0].Truong_04_2 != UC_2225_2.uC_2225_Item1.txt_Truong_04_2.Text 
                        || DataUser2[0].Truong_04_3 != UC_2225_2.uC_2225_Item1.txt_Truong_04_3.Text 
                        || DataUser2[0].Truong_05_1 != UC_2225_2.uC_2225_Item1.txt_Truong_05_1.Text 
                        || DataUser2[0].Truong_05_2 != UC_2225_2.uC_2225_Item1.txt_Truong_05_2.Text 
                        || DataUser2[0].Truong_06 != UC_2225_2.uC_2225_Item1.txt_Truong_06.Text 
                        || DataUser2[0].Truong_07 != UC_2225_2.uC_2225_Item1.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[0].Truong_08 != UC_2225_2.uC_2225_Item1.txt_Truong_08.Text 
                        || DataUser2[0].Truong_09 != UC_2225_2.uC_2225_Item1.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[0].Truong_10 != UC_2225_2.uC_2225_Item1.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[0].Truong_11 != UC_2225_2.uC_2225_Item1.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[0].Truong_12 != UC_2225_2.uC_2225_Item1.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[0].Truong_13 != UC_2225_2.uC_2225_Item1.txt_Truong_13.Text 
                        || DataUser2[0].Truong_14 != UC_2225_2.uC_2225_Item1.txt_Truong_14.Text 
                        || DataUser2[0].Truong_15 != UC_2225_2.uC_2225_Item1.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[0].Truong_16 != UC_2225_2.uC_2225_Item1.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[0].Truong_17 != UC_2225_2.uC_2225_Item1.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[0].Truong_18 != UC_2225_2.uC_2225_Item1.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[0].Truong_19 != UC_2225_2.uC_2225_Item1.txt_Truong_19.Text 
                        || DataUser2[0].Truong_20 != UC_2225_2.uC_2225_Item1.txt_Truong_20.Text 
                        || DataUser2[0].Truong_21 != UC_2225_2.uC_2225_Item1.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[0].Truong_22 != UC_2225_2.uC_2225_Item1.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[0].Truong_23 != UC_2225_2.uC_2225_Item1.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[0].Truong_24 != UC_2225_2.uC_2225_Item1.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[0].Truong_25 != UC_2225_2.uC_2225_Item1.txt_Truong_25.Text 
                        || DataUser2[0].Truong_26 != UC_2225_2.uC_2225_Item1.txt_Truong_26.Text )
                    {
                        flag_Phieu1_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu1_User2Sai = false;
                    }
                    //Check phiếu 2
                    if (DataUser2[1].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[1].Truong_02 != UC_2225_2.uC_2225_Item2.txt_Truong_02.Text 
                        || DataUser2[1].Truong_03 != UC_2225_2.uC_2225_Item2.txt_Truong_03.Text 
                        || DataUser2[1].Truong_04_1 != UC_2225_2.uC_2225_Item2.txt_Truong_04_1.Text 
                        || DataUser2[1].Truong_04_2 != UC_2225_2.uC_2225_Item2.txt_Truong_04_2.Text 
                        || DataUser2[1].Truong_04_3 != UC_2225_2.uC_2225_Item2.txt_Truong_04_3.Text 
                        || DataUser2[1].Truong_05_1 != UC_2225_2.uC_2225_Item2.txt_Truong_05_1.Text 
                        || DataUser2[1].Truong_05_2 != UC_2225_2.uC_2225_Item2.txt_Truong_05_2.Text 
                        || DataUser2[1].Truong_06 != UC_2225_2.uC_2225_Item2.txt_Truong_06.Text 
                        || DataUser2[1].Truong_07 != UC_2225_2.uC_2225_Item2.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[1].Truong_08 != UC_2225_2.uC_2225_Item2.txt_Truong_08.Text 
                        || DataUser2[1].Truong_09 != UC_2225_2.uC_2225_Item2.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[1].Truong_10 != UC_2225_2.uC_2225_Item2.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[1].Truong_11 != UC_2225_2.uC_2225_Item2.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[1].Truong_12 != UC_2225_2.uC_2225_Item2.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[1].Truong_13 != UC_2225_2.uC_2225_Item2.txt_Truong_13.Text 
                        || DataUser2[1].Truong_14 != UC_2225_2.uC_2225_Item2.txt_Truong_14.Text 
                        || DataUser2[1].Truong_15 != UC_2225_2.uC_2225_Item2.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[1].Truong_16 != UC_2225_2.uC_2225_Item2.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[1].Truong_17 != UC_2225_2.uC_2225_Item2.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[1].Truong_18 != UC_2225_2.uC_2225_Item2.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[1].Truong_19 != UC_2225_2.uC_2225_Item2.txt_Truong_19.Text 
                        || DataUser2[1].Truong_20 != UC_2225_2.uC_2225_Item2.txt_Truong_20.Text 
                        || DataUser2[1].Truong_21 != UC_2225_2.uC_2225_Item2.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[1].Truong_22 != UC_2225_2.uC_2225_Item2.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[1].Truong_23 != UC_2225_2.uC_2225_Item2.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[1].Truong_24 != UC_2225_2.uC_2225_Item2.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[1].Truong_25 != UC_2225_2.uC_2225_Item2.txt_Truong_25.Text 
                        || DataUser2[1].Truong_26 != UC_2225_2.uC_2225_Item2.txt_Truong_26.Text )
                    {
                        flag_Phieu2_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu2_User2Sai = false;
                    }
                    //Check phiếu 3
                    if (DataUser2[2].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[2].Truong_02 != UC_2225_2.uC_2225_Item3.txt_Truong_02.Text 
                        || DataUser2[2].Truong_03 != UC_2225_2.uC_2225_Item3.txt_Truong_03.Text 
                        || DataUser2[2].Truong_04_1 != UC_2225_2.uC_2225_Item3.txt_Truong_04_1.Text 
                        || DataUser2[2].Truong_04_2 != UC_2225_2.uC_2225_Item3.txt_Truong_04_2.Text 
                        || DataUser2[2].Truong_04_3 != UC_2225_2.uC_2225_Item3.txt_Truong_04_3.Text 
                        || DataUser2[2].Truong_05_1 != UC_2225_2.uC_2225_Item3.txt_Truong_05_1.Text 
                        || DataUser2[2].Truong_05_2 != UC_2225_2.uC_2225_Item3.txt_Truong_05_2.Text 
                        || DataUser2[2].Truong_06 != UC_2225_2.uC_2225_Item3.txt_Truong_06.Text 
                        || DataUser2[2].Truong_07 != UC_2225_2.uC_2225_Item3.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[2].Truong_08 != UC_2225_2.uC_2225_Item3.txt_Truong_08.Text 
                        || DataUser2[2].Truong_09 != UC_2225_2.uC_2225_Item3.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[2].Truong_10 != UC_2225_2.uC_2225_Item3.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[2].Truong_11 != UC_2225_2.uC_2225_Item3.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[2].Truong_12 != UC_2225_2.uC_2225_Item3.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[2].Truong_13 != UC_2225_2.uC_2225_Item3.txt_Truong_13.Text 
                        || DataUser2[2].Truong_14 != UC_2225_2.uC_2225_Item3.txt_Truong_14.Text 
                        || DataUser2[2].Truong_15 != UC_2225_2.uC_2225_Item3.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[2].Truong_16 != UC_2225_2.uC_2225_Item3.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[2].Truong_17 != UC_2225_2.uC_2225_Item3.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[2].Truong_18 != UC_2225_2.uC_2225_Item3.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[2].Truong_19 != UC_2225_2.uC_2225_Item3.txt_Truong_19.Text 
                        || DataUser2[2].Truong_20 != UC_2225_2.uC_2225_Item3.txt_Truong_20.Text 
                        || DataUser2[2].Truong_21 != UC_2225_2.uC_2225_Item3.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[2].Truong_22 != UC_2225_2.uC_2225_Item3.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[2].Truong_23 != UC_2225_2.uC_2225_Item3.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[2].Truong_24 != UC_2225_2.uC_2225_Item3.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[2].Truong_25 != UC_2225_2.uC_2225_Item3.txt_Truong_25.Text 
                        || DataUser2[2].Truong_26 != UC_2225_2.uC_2225_Item3.txt_Truong_26.Text )
                    {
                        flag_Phieu3_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu3_User2Sai = false;
                    }
                    //Check phiếu 4
                    if (DataUser2[3].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[3].Truong_02 != UC_2225_2.uC_2225_Item4.txt_Truong_02.Text 
                        || DataUser2[3].Truong_03 != UC_2225_2.uC_2225_Item4.txt_Truong_03.Text 
                        || DataUser2[3].Truong_04_1 != UC_2225_2.uC_2225_Item4.txt_Truong_04_1.Text 
                        || DataUser2[3].Truong_04_2 != UC_2225_2.uC_2225_Item4.txt_Truong_04_2.Text 
                        || DataUser2[3].Truong_04_3 != UC_2225_2.uC_2225_Item4.txt_Truong_04_3.Text 
                        || DataUser2[3].Truong_05_1 != UC_2225_2.uC_2225_Item4.txt_Truong_05_1.Text 
                        || DataUser2[3].Truong_05_2 != UC_2225_2.uC_2225_Item4.txt_Truong_05_2.Text 
                        || DataUser2[3].Truong_06 != UC_2225_2.uC_2225_Item4.txt_Truong_06.Text 
                        || DataUser2[3].Truong_07 != UC_2225_2.uC_2225_Item4.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[3].Truong_08 != UC_2225_2.uC_2225_Item4.txt_Truong_08.Text 
                        || DataUser2[3].Truong_09 != UC_2225_2.uC_2225_Item4.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[3].Truong_10 != UC_2225_2.uC_2225_Item4.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[3].Truong_11 != UC_2225_2.uC_2225_Item4.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[3].Truong_12 != UC_2225_2.uC_2225_Item4.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[3].Truong_13 != UC_2225_2.uC_2225_Item4.txt_Truong_13.Text 
                        || DataUser2[3].Truong_14 != UC_2225_2.uC_2225_Item4.txt_Truong_14.Text 
                        || DataUser2[3].Truong_15 != UC_2225_2.uC_2225_Item4.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[3].Truong_16 != UC_2225_2.uC_2225_Item4.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[3].Truong_17 != UC_2225_2.uC_2225_Item4.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[3].Truong_18 != UC_2225_2.uC_2225_Item4.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[3].Truong_19 != UC_2225_2.uC_2225_Item4.txt_Truong_19.Text 
                        || DataUser2[3].Truong_20 != UC_2225_2.uC_2225_Item4.txt_Truong_20.Text 
                        || DataUser2[3].Truong_21 != UC_2225_2.uC_2225_Item4.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[3].Truong_22 != UC_2225_2.uC_2225_Item4.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[3].Truong_23 != UC_2225_2.uC_2225_Item4.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[3].Truong_24 != UC_2225_2.uC_2225_Item4.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[3].Truong_25 != UC_2225_2.uC_2225_Item4.txt_Truong_25.Text 
                        || DataUser2[3].Truong_26 != UC_2225_2.uC_2225_Item4.txt_Truong_26.Text
                        || DataUser2[3].Truong_Flag != UC_2225_2.txt_Truong_Flag.Text)
                    {
                        flag_Phieu4_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu4_User2Sai = false;
                    }
                    //Check phiếu 5
                    if (DataUser2[4].Truong_01 != txt_Truong_01_User2.Text 
                        || DataUser2[4].Truong_02 != UC_2225_2.uC_2225_Item5.txt_Truong_02.Text 
                        || DataUser2[4].Truong_03 != UC_2225_2.uC_2225_Item5.txt_Truong_03.Text 
                        || DataUser2[4].Truong_04_1 != UC_2225_2.uC_2225_Item5.txt_Truong_04_1.Text 
                        || DataUser2[4].Truong_04_2 != UC_2225_2.uC_2225_Item5.txt_Truong_04_2.Text 
                        || DataUser2[4].Truong_04_3 != UC_2225_2.uC_2225_Item5.txt_Truong_04_3.Text 
                        || DataUser2[4].Truong_05_1 != UC_2225_2.uC_2225_Item5.txt_Truong_05_1.Text 
                        || DataUser2[4].Truong_05_2 != UC_2225_2.uC_2225_Item5.txt_Truong_05_2.Text 
                        || DataUser2[4].Truong_06 != UC_2225_2.uC_2225_Item5.txt_Truong_06.Text 
                        || DataUser2[4].Truong_07 != UC_2225_2.uC_2225_Item5.txt_Truong_07.Text.Replace(",", "") 
                        || DataUser2[4].Truong_08 != UC_2225_2.uC_2225_Item5.txt_Truong_08.Text 
                        || DataUser2[4].Truong_09 != UC_2225_2.uC_2225_Item5.txt_Truong_09.Text.Replace(",", "") 
                        || DataUser2[4].Truong_10 != UC_2225_2.uC_2225_Item5.txt_Truong_10.Text.Replace(",", "") 
                        || DataUser2[4].Truong_11 != UC_2225_2.uC_2225_Item5.txt_Truong_11.Text.Replace(",", "") 
                        || DataUser2[4].Truong_12 != UC_2225_2.uC_2225_Item5.txt_Truong_12.Text.Replace(",", "") 
                        || DataUser2[4].Truong_13 != UC_2225_2.uC_2225_Item5.txt_Truong_13.Text 
                        || DataUser2[4].Truong_14 != UC_2225_2.uC_2225_Item5.txt_Truong_14.Text 
                        || DataUser2[4].Truong_15 != UC_2225_2.uC_2225_Item5.txt_Truong_15.Text.Replace(",", "") 
                        || DataUser2[4].Truong_16 != UC_2225_2.uC_2225_Item5.txt_Truong_16.Text.Replace(",", "") 
                        || DataUser2[4].Truong_17 != UC_2225_2.uC_2225_Item5.txt_Truong_17.Text.Replace(",", "") 
                        || DataUser2[4].Truong_18 != UC_2225_2.uC_2225_Item5.txt_Truong_18.Text.Replace(",", "") 
                        || DataUser2[4].Truong_19 != UC_2225_2.uC_2225_Item5.txt_Truong_19.Text 
                        || DataUser2[4].Truong_20 != UC_2225_2.uC_2225_Item5.txt_Truong_20.Text 
                        || DataUser2[4].Truong_21 != UC_2225_2.uC_2225_Item5.txt_Truong_21.Text.Replace(",", "") 
                        || DataUser2[4].Truong_22 != UC_2225_2.uC_2225_Item5.txt_Truong_22.Text.Replace(",", "") 
                        || DataUser2[4].Truong_23 != UC_2225_2.uC_2225_Item5.txt_Truong_23.Text.Replace(",", "") 
                        || DataUser2[4].Truong_24 != UC_2225_2.uC_2225_Item5.txt_Truong_24.Text.Replace(",", "")
                        || DataUser2[4].Truong_25 != UC_2225_2.uC_2225_Item5.txt_Truong_25.Text 
                        || DataUser2[4].Truong_26 != UC_2225_2.uC_2225_Item5.txt_Truong_26.Text )
                    {
                        flag_Phieu5_User2Sai = true;
                    }
                    else
                    {
                        flag_Phieu5_User2Sai = false;
                    }
                    if (this.fLagRefresh)
                    {
                        this.UC_2225_2.Save2225_Check(this.fbatchRefresh, this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                    else
                    {
                        this.UC_2225_2.Save2225_Check(this.cbb_Batch_Check.SelectedValue+"", this.lb_Image.Text, this.txt_Truong_01_User1.Text,
                                                    this.lb_User1.Text, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                                                    this.lb_User2.Text, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai);
                    }
                }
            }
            fLagRefresh = false;
            fbatchRefresh = "";
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
                VisibleButtonSave();
                return;
            }
            Load_DeSo(cbb_Batch_Check.SelectedValue + "", lb_Image.Text);
            VisibleButtonSave();
        }
        
        private void Compare_ComBoBox(System.Windows.Forms.ComboBox t1, System.Windows.Forms.ComboBox t2)
        {
            if (t1.Text != t2.Text)
            {
                t1.BackColor = Color.PaleVioletRed;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.PaleVioletRed;
                t2.ForeColor = Color.Black;
            }
            else
            {
                t1.BackColor = Color.White;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.White;
                t2.ForeColor = Color.Black;
            }
        }
        private void Compare_Textbox_Word(RichTextBox t1, RichTextBox t2)
        {
            int n = 0;
            string s1 = t1.Text;
            string s2 = t2.Text;
            int check = s1.CompareTo(s2);
            if (check != 0)
            {
                if (s1.Length > s2.Length)
                {
                    n = s2.Length;
                    t1.SelectionStart = n;
                    t1.SelectionLength = s1.Length - s2.Length;
                    t1.SelectionColor = Color.Red;
                }
                else
                {
                    n = s1.Length;
                    t2.SelectionStart = n;
                    t2.SelectionLength = s2.Length - s1.Length;
                    t2.SelectionColor = Color.Red;
                }
                for (int i = 0; i < n; i++)
                {
                    if (s1[i] != s2[i])
                    {
                        t1.SelectionStart = i;
                        t1.SelectionLength = 1;
                        t1.SelectionColor = Color.Red;
                        
                        t2.SelectionStart = i;
                        t2.SelectionLength = 1;
                        t2.SelectionColor = Color.Red;
                    }
                }
                t1.BackColor = Color.AntiqueWhite;
                t2.BackColor = Color.AntiqueWhite;
            }
        }
        private void Compare_LookUpEdit(LookUpEdit t1, LookUpEdit t2)
        {
            if (t1.Text != t2.Text)
            {
                t1.BackColor = Color.PaleVioletRed;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.PaleVioletRed;
                t2.ForeColor = Color.Black;
            }
            else
            {
                t1.BackColor = Color.White;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.White;
                t2.ForeColor = Color.Black;
            }
        }
        private void Compare_TextBox(TextEdit t1, TextEdit t2)
        {
            if (t1.Text != t2.Text)
            {
                t1.BackColor = Color.PaleVioletRed;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.PaleVioletRed;
                t2.ForeColor = Color.Black;
            }
            else
            {
                t1.BackColor = Color.White;
                t1.ForeColor = Color.Black;
                t2.BackColor = Color.White;
                t2.ForeColor = Color.Black;
            }
        }
        public void CompareRichTextBox(RichTextBox t1, RichTextBox t2)
        {
            int n = 0;
            string s = t1.Text;
            string s1 = t2.Text;
            if (s.Length > s1.Length)
            {
                n = s1.Length;
                t1.SelectionStart = n;
                t1.SelectionLength = s.Length - s1.Length;
                t1.SelectionColor = Color.Red;
            }
            else
            {
                n = s.Length;
                t2.SelectionStart = n;
                t2.SelectionLength = s1.Length - s.Length;
                t2.SelectionColor = Color.Red;
            }

            for (int i = 0; i < n; i++)
            {
                if (s[i] != s1[i])
                {
                    t1.SelectionStart = i;
                    t1.SelectionLength = 1;
                    t1.SelectionColor = Color.Red;

                    t2.SelectionStart = i;
                    t2.SelectionLength = 1;
                    t2.SelectionColor = Color.Red;
                }
            }
        }

        private void lb_fBatchName_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(cbb_Batch_Check.Text);
            XtraMessageBox.Show("Copy batch name Success!");
        }

        private void lb_Image_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_Image.Text);
            XtraMessageBox.Show("Copy image name Success!");
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            LockControl(false);
        }

        private void cbb_Batch_Check_TextChanged(object sender, EventArgs e)
        {
            if (FlagLoad)
                return;
            VisibleButtonSave();
            lb_Image.Text = "";
            //Global.StrBatchID = cbb_Batch_Check.SelectedValue+"";
            ResetData();
            btn_Start.Visible = true;
        }

        private void btn_ShowImageCheck_Click(object sender, EventArgs e)
        {
            //frm_ShowCheckedImage a = new frm_ShowCheckedImage();
            //a.BatchID = cbb_Batch_Check.SelectedValue + "";
            //a.TypeCheck = TypeCheck;
            //a.ShowDialog();
        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(splitCheck.SplitterPosition+"");
            //Settings.Default.PositionSplitCheck = splitCheck.SplitterPosition;
            //Settings.Default.Save();
        }
        private void btn_CheckLai_Click(object sender, EventArgs e)
        {
            var temp = (from w in Global.Db.GetImage_RefreshCheck(Global.StrUserName, TypeCheck) select new { w.BatchID, w.IDImage }).FirstOrDefault();
            if (temp == null)
            {
                MessageBox.Show("Bạn chưa check, vui lòng check hình trước khi check lại");
                return;
            }
            lb_Image.Text = "";
            fbatchRefresh = "";
            if (Global.RunUpdateVersion())
                Application.Exit();
            ResetData();
            lb_Image.Text = temp.IDImage;
            fbatchRefresh = temp.BatchID;
            uc_PictureBox1.LoadImage(Global.Webservice + fbatchRefresh + "/" + lb_Image.Text, lb_Image.Text, Settings.Default.ZoomImage);
            Load_DeSo(fbatchRefresh, lb_Image.Text);
            VisibleButtonSave();
            fLagRefresh = true;
            btn_Start.Visible = false;
        }
        
        private void cbb_Batch_Check_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 3)
            {
                e.Handled = true;
            }
        }

        private void cbb_Batch_Check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                cbb_Batch_Check.Text = "";
        }
        
        private void Tab_User1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if ((this.Tab_User1.SelectedTabPage.Name == "tp_2225_User1") || (this.Tab_User2.SelectedTabPage.Name == "tp_2225_User2"))
            {
                this.splitCheck.SplitterPosition = 680;
            }
            else
            {
                this.splitCheck.SplitterPosition = 640;
            }
        }
        
        private void Tab_User2_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if ((this.Tab_User1.SelectedTabPage.Name == "tp_2225_User1") || (this.Tab_User2.SelectedTabPage.Name == "tp_2225_User2"))
            {
                this.splitCheck.SplitterPosition = 680;
            }
            else
            {
                this.splitCheck.SplitterPosition = 640;
            }
        }
        
        private void uC_225_1_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.Tab_User2.SelectedTabPage.Name == "tp_225_User2")
            {
                if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
                {
                    this.UC_225_2.HorizontalScroll.Value = e.NewValue;
                }
                else if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
                {
                    this.UC_225_2.VerticalScroll.Value = e.NewValue;
                }
            }
        }
        
        private void uC_2225_1_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.Tab_User2.SelectedTabPage.Name == "tp_2225_User2")
            {
                if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
                {
                    this.UC_2225_2.HorizontalScroll.Value = e.NewValue;
                }
                else if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
                {
                    this.UC_2225_2.VerticalScroll.Value = e.NewValue;
                }
            }
        }
        
        private void uC_225_2_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.Tab_User1.SelectedTabPage.Name == "tp_225_User1")
            {
                if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
                {
                    this.UC_225_1.HorizontalScroll.Value = e.NewValue;
                }
                else if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
                {
                    this.UC_225_1.VerticalScroll.Value = e.NewValue;
                }
            }
        }
        
        private void uC_2225_2_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.Tab_User1.SelectedTabPage.Name == "tp_2225_User1")
            {
                if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
                {
                    this.UC_2225_1.HorizontalScroll.Value = e.NewValue;
                }
                else if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
                {
                    this.UC_2225_1.VerticalScroll.Value = e.NewValue;
                }
            }
        }
        
        private void txt_Truong_01_User1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txt_Truong_01_User1.Text == "225")
            {
                this.Tab_User1.SelectedTabPage = this.tp_225_User1;
            }
            else
            {
                this.Tab_User1.SelectedTabPage = this.tp_2225_User1;
            }
        }

        private void txt_Truong_01_User2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txt_Truong_01_User2.Text == "225")
            {
                this.Tab_User2.SelectedTabPage = this.tp_225_User2;
            }
            else
            {
                this.Tab_User2.SelectedTabPage = this.tp_2225_User2;
            }
        }
    }
}