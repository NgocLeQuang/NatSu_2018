using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using CLS_NatSu.MyClass;
using System.Drawing;

namespace CLS_NatSu.MyForm
{
    public partial class frm_ExportExcel : DevExpress.XtraEditors.XtraForm
    {
        public frm_ExportExcel()
        {
            InitializeComponent();
        }
        Microsoft.Office.Interop.Excel.Application App = null;
        Microsoft.Office.Interop.Excel.Workbook book = null;
        Microsoft.Office.Interop.Excel.Worksheet wrksheet = null;
        int h = 2;
        private int soloisausua = 0;
        private int soloi = 0;
        string namefileExcel = "";

        public struct MyEntry
        {
            public string BatchID { get; set; }
            public string BatchName { get; set; }
        }
        bool FlagLoad = false;
        private void frm_ExportExcel_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            lb_BatchName.Text = "";
            FlagLoad = true;
            //dgv.PageVisible = true;
            //Batch.PageVisible = false;
            list_Batch.DataSource = (from w in Global.Db.GetBatchCongKhai() select new MyEntry { BatchID = w.BatchID, BatchName = w.BatchName }).ToList();
            list_Batch.DisplayMember = "BatchName";
            list_Batch.ValueMember = "BatchID";
            FlagLoad = false;
        }

        int n = 1;
        string getcharacter(int n, string str)
        {
            string kq = "";
            for (int i = 1; i <= n; i++)
            {
                kq = kq.Insert(kq.Length, str);
            }
            return kq;
        }
        public string ThemOTruocThemSpaceKhiCoDauHoi(string input, int iByte, string str)
        {
            if (string.IsNullOrEmpty(input))
                return input.Insert(0, getcharacter(iByte - input.Length, " "));
            if (input.IndexOf("?") >= 0)
            {
                return input.Insert(0, getcharacter(iByte - input.Length, " "));
            }
            else if (input.Length >= iByte)
                return input.Substring(0, iByte);
            return input.Insert(0, getcharacter(iByte - input.Length, str));
        }
        protected virtual bool IsFileinUse(FileInfo file)
        {
            FileStream stream = null;
            if (!File.Exists(file.FullName))
                return false;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
        private DataTable _kq = new DataTable();
        string ListBatchDaXuat = "",
            ListFileinUse="";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ListBatchDaXuat = "";
            ListFileinUse = "";
            namefileExcel = "";
            try
            {
                if (backgroundWorker1.IsBusy)
                {
                    MessageBox.Show("Quá trình xuất file đang diễn ra, Bạn hãy chờ hoàn thành mới tiếp tục !");
                    return;
                }
                if (!Directory.Exists(txt_path.Text))
                {
                    MessageBox.Show("Thư mục lưu file convert không tồn tại. Vui lòng chọn thư mục lưu.");
                    return;
                }
                if (list_Batch.SelectedItems.Count <= 0 & list_Batch.Items.Count > 0)
                {
                    MessageBox.Show("Vui lòng chọn batch.");
                    return;
                }
                if (list_Batch.Items.Count <= 0)
                {
                    MessageBox.Show("Không có Batch.");
                    return;
                }
                string ListBatchNotFinish = "", ListUser = "";
                foreach (var item in list_Batch.SelectedItems)
                {
                    var CountImageNotComplete = (from w in Global.Db.CheckInputComplete(((MyEntry)item).BatchID + "") select w.IdImage).ToList();
                    var check = (from w in Global.Db.tbl_MissImage_DeSos where w.BatchID == ((MyEntry)item).BatchID + "" && w.Submit == false select w.IDImage).Count();
                    if (CountImageNotComplete.Count > 0)
                    {
                        ListBatchNotFinish += ((MyEntry)item).BatchName + "\r\n";
                    }
                    if (check > 0)
                    {
                        var list_user = (from w in Global.Db.tbl_MissImage_DeSos where w.BatchID == ((MyEntry)item).BatchID + "" && w.Submit == false select w.UserName).ToList();
                        string sss = "";
                        foreach (var item_temp in list_user)
                        {
                            sss += item_temp + "\r\n";
                        }
                        if (list_user.Count > 0)
                        {
                            ListUser += sss;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ListBatchNotFinish))
                {
                    MessageBox.Show("Batch: " + ListBatchNotFinish + "Chưa nhập xong DeSo!");
                    return;
                }
                if (!string.IsNullOrEmpty(ListUser))
                {
                    MessageBox.Show("\r\nNhững user lấy hình về nhưng chưa nhập deso: \r\n" + ListUser);
                    return;
                }
                ListBatchNotFinish = "";
                ListUser = "";
                foreach (var item in list_Batch.SelectedItems)
                {
                    var soloi = (from w in Global.Db.GetNumberErrorUnfinish_Excel(((MyEntry)item).BatchID + "") select w.IDImage).Count();
                    var ListCheckNotComplete = (from w in Global.Db.tbl_MissCheck_DeSos where w.BatchID == ((MyEntry)item).BatchID + "" && w.Submit == false select new { w.IDImage, w.UserName }).ToList();
                    if (ListCheckNotComplete.Count > 0 || soloi > 0)
                    {
                        ListBatchNotFinish += ("\r\n" + ((MyEntry)item).BatchName);
                        MessageBox.Show("Batch: " + ((MyEntry)item).BatchName + "Chưa check xong DeSo!");
                        string sss = "";
                        foreach (var item_temp in ListCheckNotComplete)
                        {
                            sss += item_temp.UserName + "\r\n";
                        }
                        if (ListCheckNotComplete.Count() > 0)
                        {
                            ListUser += sss;
                        }
                    }
                    if (File.Exists(txt_path.Text + @"\" + ((MyEntry)item).BatchName + ".xlsx"))
                    {
                        ListBatchDaXuat += ("\r\n" + ((MyEntry)item).BatchName);
                    }
                    if (IsFileinUse(new FileInfo(txt_path.Text + @"\" + ((MyEntry)item).BatchName + ".xlsx")))
                    {
                        ListFileinUse += ("\r\n" + ((MyEntry)item).BatchName);
                    }
                }
                if (!string.IsNullOrEmpty(ListBatchNotFinish) || !string.IsNullOrEmpty(ListUser))
                {
                    MessageBox.Show("Batch: " + ListBatchNotFinish + "Chưa check xong DeSo!");
                    if (!string.IsNullOrEmpty(ListUser))
                    {
                        MessageBox.Show("\r\nNhững user lấy hình về nhưng chưa check deso: \r\n" + ListUser);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(ListBatchDaXuat))
                {
                    if (MessageBox.Show("\r\nBatch đã xuất:" + ListBatchDaXuat + "\r\nBạn muốn xuất lại không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                    {
                        ListBatchDaXuat = "";
                    }
                    else
                        ListFileinUse = "";
                }
                if (!string.IsNullOrEmpty(ListFileinUse))
                {
                    MessageBox.Show("\r\nFile đang sử dụng:" + ListFileinUse + "\r\nVui lòng đóng file");
                        return;
                }
                backgroundWorker1.RunWorkerAsync();
                //backgroundWorker1_DoWork(null, null);
            }
            catch { }
        }
        private string QuaKyTuXuatDauChamHoi(string input, int iByte, string str)
        {
            if (input.Length > iByte)
                return "?";
            return input;
        }
        /// <summary>
        /// Áp dụng cho trường ngày
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iByte"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string ThemKyTuPhiaTruoc(string input, int iByte, string str)
        {
            if (string.IsNullOrEmpty(input))
                return "";
            if(input.Length<iByte)
            {
                return input.Insert(0, getcharacter(iByte - input.Length, str));
            }
            //if (input== "*")
            //{
            //    return input.Insert(0, getcharacter(iByte - input.Length, str));
            //}
            return input;
        }
        private string ThemKyTuPhiaTruoc_8_14_20(string input, string str)
        {
            if (input == "*")
            {
                return input.Insert(0, str);
            }
            if (string.IsNullOrEmpty(input))
                return "";
            if (input.Length < 2)
            {
                return input.Insert(0, str);
            }
            int Index = input.IndexOf(".");
            if(Index > 0)
            {
                string s = input.Substring(0, Index);
                if (s.Length < 2)
                {
                    return input.Insert(0, str);
                }
            }
            return input;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] separators = { "\r\n" };
            string[] ProjectBranch_Split = (ListBatchDaXuat).Split(separators, StringSplitOptions.None);

            txt_path.Enabled = false;
            btn_Browse.Enabled = false;
            list_Batch.Enabled = false;
            foreach (var item in list_Batch.SelectedItems)
            {
                if (ListBatchDaXuat.IndexOf(((MyEntry)item).BatchName + "") >= 0)
                {
                    continue;
                }
                h = 2;
                namefileExcel = "";
                namefileExcel += ((MyEntry)item).BatchName;
                lb_BatchName.Text = ((MyEntry)item).BatchName;
                _kq = new DataTable();
                string ConnectionString = Global.Db.Connection.ConnectionString;
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("ExportExcel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BatchID", ((MyEntry)item).BatchID);
                con.Open();
                _kq.Load(cmd.ExecuteReader());
                int RowCount = _kq.Rows.Count;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                progressBar1.Maximum = RowCount / 3;
                progressBar1.Minimum = 0;
                ModifyProgressBarColor.SetState(progressBar1, 1);

                if (_kq.Rows.Count <= 0)
                {
                    continue;
                }
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx");
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + namefileExcel + ".xlsx"), Properties.Resources.ExportExcel);
                }
                else
                {
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/" + namefileExcel + ".xlsx"), Properties.Resources.ExportExcel);
                }
                App = new Microsoft.Office.Interop.Excel.Application();
                book = App.Workbooks.Open(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx", 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                wrksheet = (Microsoft.Office.Interop.Excel.Worksheet)book.ActiveSheet;
                //------------

                string truong1 = "";
                string truong2 = "";
                string truong3 = "";
                string truong4_1 = "";
                string truong4_2 = "";
                string truong4_3 = "";
                string truong5_1 = "";
                string truong5_2 = "";
                string truong6 = "";
                string truong7 = "";
                string truong8 = "";
                string truong9 = "";
                string truong10 = "";
                string truong11 = "";
                string truong12 = "";
                string truong13 = "";
                string truong14 = "";
                string truong15 = "";
                string truong16 = "";
                string truong17 = "";
                string truong18 = "";
                string truong19 = "";
                string truong20 = "";
                string truong21 = "";
                string truong22 = "";
                string truong23 = "";
                string truong24 = "";
                string truong25 = "";
                string truong26 = "";
                string FlagZ = "";


                string truong1UserNhap = "";
                string truong2UserNhap = "";
                string truong3UserNhap = "";
                string truong4_1UserNhap = "";
                string truong4_2UserNhap = "";
                string truong4_3UserNhap = "";
                string truong5_1UserNhap = "";
                string truong5_2UserNhap = "";
                string truong6UserNhap = "";
                string truong7UserNhap = "";
                string truong8UserNhap = "";
                string truong9UserNhap = "";
                string truong10UserNhap = "";
                string truong11UserNhap = "";
                string truong12UserNhap = "";
                string truong13UserNhap = "";
                string truong14UserNhap = "";
                string truong15UserNhap = "";
                string truong16UserNhap = "";
                string truong17UserNhap = "";
                string truong18UserNhap = "";
                string truong19UserNhap = "";
                string truong20UserNhap = "";
                string truong21UserNhap = "";
                string truong22UserNhap = "";
                string truong23UserNhap = "";
                string truong24UserNhap = "";
                string truong25UserNhap = "";
                string truong26UserNhap = "";
                string FlagZUserNhap = "";


                var totalRow = RowCount / 3;
                var totalRowError = (from w in Global.Db.tbl_DeSos where w.BatchID == ((MyEntry)item).BatchID && w.Error == true group w by new { w.BatchID,w.IDImage,w.IDPhieu} into g  select g.Key.IDImage).Count();
                var soDongLoiChoPhep = 0.03 * totalRow;
                int tileCanSua = (int)Math.Round(totalRowError / soDongLoiChoPhep, 0);
                int demUser1 = 1, demUser2 = 1;
                //MessageBox.Show("Tổng dòng:"+ totalImage+
                //                "\r\nTổng dòng lỗi:"+totalImageError+
                //                "\r\nTỉ lệ cần sửa:"+tileCanSua+
                //                "\r\nSố dòng cho phép lỗi"+soDongLoiChoPhep);
                if (totalRowError <= soDongLoiChoPhep)
                {
                    for (int i = 0; i < RowCount; i += 3)
                    {
                        wrksheet.Cells[h, 1] = _kq.Rows[i][0] + ""; //tên image
                        //Dữ liệu đúng
                        wrksheet.Cells[h, 2] = truong1 = _kq.Rows[i][4] + "";//Truong01
                        wrksheet.Cells[h, 3] = truong2 = _kq.Rows[i][5] + "";//Truong02
                        wrksheet.Cells[h, 4] = truong3 = _kq.Rows[i][6] + "";//Truong03
                        wrksheet.Cells[h, 5] = truong4_1 = ThemKyTuPhiaTruoc(_kq.Rows[i][7] + "", 2, "0");//Truong04_1
                        wrksheet.Cells[h, 6] = truong4_2 = ThemKyTuPhiaTruoc(_kq.Rows[i][8] + "", 2, "0");//Truong04_2
                        wrksheet.Cells[h, 7] = truong4_3 = ThemKyTuPhiaTruoc(_kq.Rows[i][9] + "", 2, "0");//Truong04_3
                        wrksheet.Cells[h, 8] = truong5_1 = ThemKyTuPhiaTruoc(_kq.Rows[i][10] + "", 2, "0");//Truong05_1
                        wrksheet.Cells[h, 9] = truong5_2 = ThemKyTuPhiaTruoc(_kq.Rows[i][11] + "", 2, "0");//Truong05_2
                        wrksheet.Cells[h, 10] = truong6 = ThemKyTuPhiaTruoc(_kq.Rows[i][12] + "", 2, "0");//Truong06
                        wrksheet.Cells[h, 11] = truong7 = _kq.Rows[i][13] + "";//Truong07
                        wrksheet.Cells[h, 12] = truong8 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][14] + "", "0");//Truong08
                        wrksheet.Cells[h, 13] = truong9 = _kq.Rows[i][15] + "";//Truong09
                        wrksheet.Cells[h, 14] = truong10 = _kq.Rows[i][16] + "";//Truong10
                        wrksheet.Cells[h, 15] = truong11 = _kq.Rows[i][17] + "";//Truong11
                        wrksheet.Cells[h, 16] = truong12 = _kq.Rows[i][18] + "";//Truong12
                        wrksheet.Cells[h, 17] = truong13 = _kq.Rows[i][19] + "";//Truong13
                        wrksheet.Cells[h, 18] = truong14 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][20] + "", "0");//Truong14
                        wrksheet.Cells[h, 19] = truong15 = _kq.Rows[i][21] + "";//Truong15
                        wrksheet.Cells[h, 20] = truong16 = _kq.Rows[i][22] + "";//Truong16
                        wrksheet.Cells[h, 21] = truong17 = _kq.Rows[i][23] + "";//Truong17
                        wrksheet.Cells[h, 22] = truong18 = _kq.Rows[i][24] + "";//Truong18
                        wrksheet.Cells[h, 23] = truong19 = _kq.Rows[i][25] + "";//Truong19
                        wrksheet.Cells[h, 24] = truong20 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][26] + "", "0");//Truong20
                        wrksheet.Cells[h, 25] = truong21 = _kq.Rows[i][27] + "";//Truong21
                        wrksheet.Cells[h, 26] = truong22 = _kq.Rows[i][28] + "";//Truong22
                        wrksheet.Cells[h, 27] = truong23 = _kq.Rows[i][29] + "";//Truong23
                        wrksheet.Cells[h, 28] = truong24 = _kq.Rows[i][30] + "";//Truong24
                        if (string.IsNullOrEmpty(_kq.Rows[i][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i][32] + ""))
                        {
                            wrksheet.Cells[h, 29] = truong25 = ThemKyTuPhiaTruoc(_kq.Rows[i][32] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 30] = truong26 = "";//Truong26
                        }
                        else
                        {
                            wrksheet.Cells[h, 29] = truong25 = ThemKyTuPhiaTruoc(_kq.Rows[i][31] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 30] = truong26 = ThemKyTuPhiaTruoc(_kq.Rows[i][32] + "", 2, "0");//Truong26
                        }
                        wrksheet.Cells[h, 31] = _kq.Rows[i][33] + ""; //FlagZ
                        wrksheet.Cells[h, 32] = _kq.Rows[i][2] + ""; //IDPhieu

                        //Dữ liệu User 1
                        wrksheet.Cells[h, 33] = "U:" + _kq.Rows[i + 1][3] + "";//UserName
                        wrksheet.Cells[h, 34] = truong1UserNhap = _kq.Rows[i + 1][4] + "";//Truong01
                        if (truong1 != truong1UserNhap)
                        {
                            wrksheet.Cells[h, 34].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 35] = truong2UserNhap = _kq.Rows[i + 1][5] + "";//Truong02
                        if (truong2 != truong2UserNhap)
                        {
                            wrksheet.Cells[h, 35].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 36] = truong3UserNhap = _kq.Rows[i + 1][6] + "";//Truong03
                        if (truong3 != truong3UserNhap)
                        {
                            wrksheet.Cells[h, 36].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 37] = truong4_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][7] + "", 2, "0");//Truong04_1
                        if (truong4_1 != truong4_1UserNhap)
                        {
                            wrksheet.Cells[h, 37].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 38] = truong4_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][8] + "", 2, "0");//Truong04_2
                        if (truong4_2 != truong4_2UserNhap)
                        {
                            wrksheet.Cells[h, 38].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 39] = truong4_3UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][9] + "", 2, "0");//Truong04_3
                        if (truong4_3 != truong4_3UserNhap)
                        {
                            wrksheet.Cells[h, 39].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 40] = truong5_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][10] + "", 2, "0");//Truong05_1
                        if (truong5_1 != truong5_1UserNhap)
                        {
                            wrksheet.Cells[h, 40].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 41] = truong5_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][11] + "", 2, "0");//Truong05_2
                        if (truong5_2 != truong5_2UserNhap)
                        {
                            wrksheet.Cells[h, 41].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 42] = truong6UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][12] + "", 2, "0");//Truong06
                        if (truong6 != truong6UserNhap)
                        {
                            wrksheet.Cells[h, 42].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 43] = truong7UserNhap = _kq.Rows[i + 1][13] + "";//Truong07
                        if (truong7 != truong7UserNhap)
                        {
                            wrksheet.Cells[h, 43].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 44] = truong8UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][14] + "", "0");//Truong08
                        if (truong8 != truong8UserNhap)
                        {
                            wrksheet.Cells[h, 44].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 45] = truong9UserNhap = _kq.Rows[i + 1][15] + "";//Truong09
                        if (truong9 != truong9UserNhap)
                        {
                            wrksheet.Cells[h, 45].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 46] = truong10UserNhap = _kq.Rows[i + 1][16] + "";//Truong10
                        if (truong10 != truong10UserNhap)
                        {
                            wrksheet.Cells[h, 46].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 47] = truong11UserNhap = _kq.Rows[i + 1][17] + "";//Truong11
                        if (truong11 != truong11UserNhap)
                        {
                            wrksheet.Cells[h, 47].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 48] = truong12UserNhap = _kq.Rows[i + 1][18] + "";//Truong12
                        if (truong12 != truong12UserNhap)
                        {
                            wrksheet.Cells[h, 48].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 49] = truong13UserNhap = _kq.Rows[i + 1][19] + "";//Truong13
                        if (truong13 != truong13UserNhap)
                        {
                            wrksheet.Cells[h, 49].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 50] = truong14UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][20] + "", "0");//Truong14
                        if (truong14 != truong14UserNhap)
                        {
                            wrksheet.Cells[h, 50].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 51] = truong15UserNhap = _kq.Rows[i + 1][21] + "";//Truong15
                        if (truong15 != truong15UserNhap)
                        {
                            wrksheet.Cells[h, 51].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 52] = truong16UserNhap = _kq.Rows[i + 1][22] + "";//Truong16
                        if (truong16 != truong16UserNhap)
                        {
                            wrksheet.Cells[h, 52].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 53] = truong17UserNhap = _kq.Rows[i + 1][23] + "";//Truong17
                        if (truong17 != truong17UserNhap)
                        {
                            wrksheet.Cells[h, 53].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 54] = truong18UserNhap = _kq.Rows[i + 1][24] + "";//Truong18
                        if (truong18 != truong18UserNhap)
                        {
                            wrksheet.Cells[h, 54].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 55] = truong19UserNhap = _kq.Rows[i + 1][25] + "";//Truong19
                        if (truong19 != truong19UserNhap)
                        {
                            wrksheet.Cells[h, 55].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 56] = truong20UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][26] + "", "0");//Truong20
                        if (truong20 != truong20UserNhap)
                        {
                            wrksheet.Cells[h, 56].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 57] = truong21UserNhap = _kq.Rows[i + 1][27] + "";//Truong21
                        if (truong21 != truong21UserNhap)
                        {
                            wrksheet.Cells[h, 57].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 58] = truong22UserNhap = _kq.Rows[i + 1][28] + "";//Truong22
                        if (truong22 != truong22UserNhap)
                        {
                            wrksheet.Cells[h, 58].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 59] = truong23UserNhap = _kq.Rows[i + 1][29] + "";//Truong23
                        if (truong23 != truong23UserNhap)
                        {
                            wrksheet.Cells[h, 59].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 60] = truong24UserNhap = _kq.Rows[i + 1][30] + "";//Truong24
                        if (truong24 != truong24UserNhap)
                        {
                            wrksheet.Cells[h, 60].Interior.Color = Color.Red;
                        }
                        if (string.IsNullOrEmpty(_kq.Rows[i + 1][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i + 1][32] + ""))
                        {
                            wrksheet.Cells[h, 61] = truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][32] + "", 2, "0");//Truong25
                            if (truong25 != truong25UserNhap)
                            {
                                wrksheet.Cells[h, 61].Interior.Color = Color.Red;
                            }

                            wrksheet.Cells[h, 62] = "";                      //Truong26
                            if (truong26 != "")
                            {
                                wrksheet.Cells[h, 62].Interior.Color = Color.Red;
                            }
                        }
                        else
                        {
                            wrksheet.Cells[h, 61] = truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][31] + "", 2, "0");//Truong25
                            if (truong25 != truong25UserNhap)
                            {
                                wrksheet.Cells[h, 61].Interior.Color = Color.Red;
                            }
                            wrksheet.Cells[h, 62] = truong26UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][32] + "", 2, "0");//Truong26
                            if (truong26 != truong26UserNhap)
                            {
                                wrksheet.Cells[h, 62].Interior.Color = Color.Red;
                            }
                        }
                        wrksheet.Cells[h, 63] = FlagZUserNhap = _kq.Rows[i + 1][33] + ""; //FlagZ
                        if (FlagZ != FlagZUserNhap)
                        {
                            wrksheet.Cells[h, 63].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 64] = _kq.Rows[i + 1][2] + ""; //IDPhieu

                        //Dữ liệu User 2

                        wrksheet.Cells[h, 65] = "V:" + _kq.Rows[i + 2][3] + "";//UserName
                        wrksheet.Cells[h, 66] = truong1UserNhap = _kq.Rows[i + 2][4] + "";//Truong01
                        if (truong1 != truong1UserNhap)
                        {
                            wrksheet.Cells[h, 66].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 67] = truong2UserNhap = _kq.Rows[i + 2][5] + "";//Truong02
                        if (truong2 != truong2UserNhap)
                        {
                            wrksheet.Cells[h, 67].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 68] = truong3UserNhap = _kq.Rows[i + 2][6] + "";//Truong03
                        if (truong3 != truong3UserNhap)
                        {
                            wrksheet.Cells[h, 68].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 69] = truong4_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][7] + "", 2, "0");//Truong04_1
                        if (truong4_1 != truong4_1UserNhap)
                        {
                            wrksheet.Cells[h, 69].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 70] = truong4_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][8] + "", 2, "0");//Truong04_2
                        if (truong4_2 != truong4_2UserNhap)
                        {
                            wrksheet.Cells[h, 70].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 71] = truong4_3UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][9] + "", 2, "0");//Truong04_3
                        if (truong4_3 != truong4_3UserNhap)
                        {
                            wrksheet.Cells[h, 71].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 72] = truong5_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][10] + "", 2, "0");//Truong05_1
                        if (truong5_1 != truong5_1UserNhap)
                        {
                            wrksheet.Cells[h, 72].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 73] = truong5_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][11] + "", 2, "0");//Truong05_2
                        if (truong5_2 != truong5_2UserNhap)
                        {
                            wrksheet.Cells[h, 73].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 74] = truong6UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][12] + "", 2, "0");//Truong06
                        if (truong6 != truong6UserNhap)
                        {
                            wrksheet.Cells[h, 74].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 75] = truong7UserNhap = _kq.Rows[i + 2][13] + "";//Truong07
                        if (truong7 != truong7UserNhap)
                        {
                            wrksheet.Cells[h, 75].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 76] = truong8UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][14] + "", "0");//Truong08
                        if (truong8 != truong8UserNhap)
                        {
                            wrksheet.Cells[h, 76].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 77] = truong9UserNhap = _kq.Rows[i + 2][15] + "";//Truong09
                        if (truong9 != truong9UserNhap)
                        {
                            wrksheet.Cells[h, 77].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 78] = truong19UserNhap = _kq.Rows[i + 2][16] + "";//Truong10
                        if (truong10 != truong19UserNhap)
                        {
                            wrksheet.Cells[h, 78].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 79] = truong11UserNhap = _kq.Rows[i + 2][17] + "";//Truong11
                        if (truong11 != truong11UserNhap)
                        {
                            wrksheet.Cells[h, 79].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 80] = truong12UserNhap = _kq.Rows[i + 2][18] + "";//Truong12
                        if (truong12 != truong12UserNhap)
                        {
                            wrksheet.Cells[h, 80].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 81] = truong13UserNhap = _kq.Rows[i + 2][19] + "";//Truong13
                        if (truong13 != truong13UserNhap)
                        {
                            wrksheet.Cells[h, 81].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 82] = truong14UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][20] + "", "0");//Truong14
                        if (truong14 != truong14UserNhap)
                        {
                            wrksheet.Cells[h, 82].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 83] = truong15UserNhap = _kq.Rows[i + 2][21] + "";//Truong15
                        if (truong15 != truong15UserNhap)
                        {
                            wrksheet.Cells[h, 83].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 84] = truong16UserNhap = _kq.Rows[i + 2][22] + "";//Truong16
                        if (truong16 != truong16UserNhap)
                        {
                            wrksheet.Cells[h, 84].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 85] = truong17UserNhap = _kq.Rows[i + 2][23] + "";//Truong17
                        if (truong17 != truong17UserNhap)
                        {
                            wrksheet.Cells[h, 85].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 86] = truong18UserNhap = _kq.Rows[i + 2][24] + "";//Truong18
                        if (truong18 != truong18UserNhap)
                        {
                            wrksheet.Cells[h, 86].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 87] = truong19UserNhap = _kq.Rows[i + 2][25] + "";//Truong19
                        if (truong19 != truong19UserNhap)
                        {
                            wrksheet.Cells[h, 87].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 88] = truong20UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][26] + "", "0");//Truong20
                        if (truong20 != truong20UserNhap)
                        {
                            wrksheet.Cells[h, 88].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 89] = truong21UserNhap = _kq.Rows[i + 2][27] + "";//Truong21
                        if (truong21 != truong21UserNhap)
                        {
                            wrksheet.Cells[h, 89].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 90] = truong22UserNhap = _kq.Rows[i + 2][28] + "";//Truong22
                        if (truong22 != truong22UserNhap)
                        {
                            wrksheet.Cells[h, 90].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 91] = truong23UserNhap = _kq.Rows[i + 2][29] + "";//Truong23
                        if (truong23 != truong23UserNhap)
                        {
                            wrksheet.Cells[h, 91].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 92] = truong24UserNhap = _kq.Rows[i + 2][30] + "";//Truong24
                        if (truong24 != truong24UserNhap)
                        {
                            wrksheet.Cells[h, 92].Interior.Color = Color.Red;
                        }

                        if (string.IsNullOrEmpty(_kq.Rows[i + 2][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i + 2][32] + ""))
                        {
                            wrksheet.Cells[h, 93] = truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][32] + "", 2, "0");//Truong25
                            if (truong25 != truong25UserNhap)
                            {
                                wrksheet.Cells[h, 93].Interior.Color = Color.Red;
                            }

                            wrksheet.Cells[h, 94] = "";                      //Truong26
                            if (truong26 != "")
                            {
                                wrksheet.Cells[h, 94].Interior.Color = Color.Red;
                            }
                        }
                        else
                        {
                            wrksheet.Cells[h, 93] = truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][31] + "", 2, "0");//Truong25
                            if (truong25 != truong25UserNhap)
                            {
                                wrksheet.Cells[h, 93].Interior.Color = Color.Red;
                            }
                            wrksheet.Cells[h, 94] = truong26UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][32] + "", 2, "0");//Truong26
                            if (truong26 != truong26UserNhap)
                            {
                                wrksheet.Cells[h, 94].Interior.Color = Color.Red;
                            }
                        }

                        wrksheet.Cells[h, 95] = FlagZUserNhap = _kq.Rows[i + 2][33] + ""; //FlagZ
                        if (FlagZ != FlagZUserNhap)
                        {
                            wrksheet.Cells[h, 34].Interior.Color = Color.Red;
                        }
                        wrksheet.Cells[h, 96] = _kq.Rows[i + 2][2] + ""; //IDPhieu

                        lb_Complete.Text = (h - 1) + "/" + RowCount / 3 + " (" + (int)(((double)(h - 1) / (double)(RowCount / 3)) * 100) + "%) ";
                        progressBar1.PerformStep();
                        h++;
                    }
                }
                else
                {
                    for (int i = 0; i < RowCount; i += 3)
                    {
                        wrksheet.Cells[h, 1] = _kq.Rows[i][0] + ""; //tên image
                        //Dữ liệu đúng
                        wrksheet.Cells[h, 2] = truong1 = _kq.Rows[i][4] + "";//Truong01
                        wrksheet.Cells[h, 3] = truong2 = _kq.Rows[i][5] + "";//Truong02
                        wrksheet.Cells[h, 4] = truong3 = _kq.Rows[i][6] + "";//Truong03
                        wrksheet.Cells[h, 5] = truong4_1 = ThemKyTuPhiaTruoc(_kq.Rows[i][7] + "", 2, "0");//Truong04_1
                        wrksheet.Cells[h, 6] = truong4_2 = ThemKyTuPhiaTruoc(_kq.Rows[i][8] + "", 2, "0");//Truong04_2
                        wrksheet.Cells[h, 7] = truong4_3 = ThemKyTuPhiaTruoc(_kq.Rows[i][9] + "", 2, "0");//Truong04_3
                        wrksheet.Cells[h, 8] = truong5_1 = ThemKyTuPhiaTruoc(_kq.Rows[i][10] + "", 2, "0");//Truong05_1
                        wrksheet.Cells[h, 9] = truong5_2 = ThemKyTuPhiaTruoc(_kq.Rows[i][11] + "", 2, "0");//Truong05_2
                        wrksheet.Cells[h, 10] = truong6 = ThemKyTuPhiaTruoc(_kq.Rows[i][12] + "", 2, "0");//Truong06
                        wrksheet.Cells[h, 11] = truong7 = _kq.Rows[i][13] + "";//Truong07
                        wrksheet.Cells[h, 12] = truong8 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][14] + "", "0");//Truong08
                        wrksheet.Cells[h, 13] = truong9 = _kq.Rows[i][15] + "";//Truong09
                        wrksheet.Cells[h, 14] = truong10 = _kq.Rows[i][16] + "";//Truong10
                        wrksheet.Cells[h, 15] = truong11 = _kq.Rows[i][17] + "";//Truong11
                        wrksheet.Cells[h, 16] = truong12 = _kq.Rows[i][18] + "";//Truong12
                        wrksheet.Cells[h, 17] = truong13 = _kq.Rows[i][19] + "";//Truong13
                        wrksheet.Cells[h, 18] = truong14 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][20] + "", "0");//Truong14
                        wrksheet.Cells[h, 19] = truong15 = _kq.Rows[i][21] + "";//Truong15
                        wrksheet.Cells[h, 20] = truong16 = _kq.Rows[i][22] + "";//Truong16
                        wrksheet.Cells[h, 21] = truong17 = _kq.Rows[i][23] + "";//Truong17
                        wrksheet.Cells[h, 22] = truong18 = _kq.Rows[i][24] + "";//Truong18
                        wrksheet.Cells[h, 23] = truong19 = _kq.Rows[i][25] + "";//Truong19
                        wrksheet.Cells[h, 24] = truong20 = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i][26] + "", "0");//Truong20
                        wrksheet.Cells[h, 25] = truong21 = _kq.Rows[i][27] + "";//Truong21
                        wrksheet.Cells[h, 26] = truong22 = _kq.Rows[i][28] + "";//Truong22
                        wrksheet.Cells[h, 27] = truong23 = _kq.Rows[i][29] + "";//Truong23
                        wrksheet.Cells[h, 28] = truong24 = _kq.Rows[i][30] + "";//Truong24
                        if (string.IsNullOrEmpty(_kq.Rows[i][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i][32] + ""))
                        {
                            wrksheet.Cells[h, 29] = truong25 = ThemKyTuPhiaTruoc(_kq.Rows[i][32] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 30] = truong26 = "";//Truong26
                        }
                        else
                        {
                            wrksheet.Cells[h, 29] = truong25 = ThemKyTuPhiaTruoc(_kq.Rows[i][31] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 30] = truong26 = ThemKyTuPhiaTruoc(_kq.Rows[i][32] + "", 2, "0");//Truong26
                        }
                        wrksheet.Cells[h, 31] = _kq.Rows[i][33] + ""; //FlagZ
                        wrksheet.Cells[h, 32] = _kq.Rows[i][2] + ""; //IDPhieu

                        //Dữ liệu User 1
                        truong1UserNhap = _kq.Rows[i + 1][4] + "";//Truong01
                        truong2UserNhap = _kq.Rows[i + 1][5] + "";//Truong02
                        truong3UserNhap = _kq.Rows[i + 1][6] + "";//Truong03
                        truong4_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][7] + "", 2, "0");//Truong04_1
                        truong4_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][8] + "", 2, "0");//Truong04_2
                        truong4_3UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][9] + "", 2, "0");//Truong04_3
                        truong5_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][10] + "", 2, "0");//Truong05_1
                        truong5_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][11] + "", 2, "0");//Truong05_2
                        truong6UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][12] + "", 2, "0");//Truong06
                        truong7UserNhap = _kq.Rows[i + 1][13] + "";//Truong07
                        truong8UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][14] + "", "0");//Truong08
                        truong9UserNhap = _kq.Rows[i + 1][15] + "";//Truong09
                        truong10UserNhap = _kq.Rows[i + 1][16] + "";//Truong10
                        truong11UserNhap = _kq.Rows[i + 1][17] + "";//Truong11
                        truong12UserNhap = _kq.Rows[i + 1][18] + "";//Truong12
                        truong13UserNhap = _kq.Rows[i + 1][19] + "";//Truong13
                        truong14UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][20] + "", "0");//Truong14
                        truong15UserNhap = _kq.Rows[i + 1][21] + "";//Truong15
                        truong16UserNhap = _kq.Rows[i + 1][22] + "";//Truong16
                        truong17UserNhap = _kq.Rows[i + 1][23] + "";//Truong17
                        truong18UserNhap = _kq.Rows[i + 1][24] + "";//Truong18
                        truong19UserNhap = _kq.Rows[i + 1][25] + "";//Truong19
                        truong20UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 1][26] + "", "0");//Truong20
                        truong21UserNhap = _kq.Rows[i + 1][27] + "";//Truong21
                        truong22UserNhap = _kq.Rows[i + 1][28] + "";//Truong22
                        truong23UserNhap = _kq.Rows[i + 1][29] + "";//Truong23
                        truong24UserNhap = _kq.Rows[i + 1][30] + "";//Truong24
                        if (string.IsNullOrEmpty(_kq.Rows[i + 1][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i + 1][32] + ""))
                        {
                            truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][32] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 62] = "";                      //Truong26
                        }
                        else
                        {
                            truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][31] + "", 2, "0");//Truong25
                            truong26UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 1][32] + "", 2, "0");//Truong26
                        }
                        FlagZUserNhap = _kq.Rows[i + 1][33] + ""; //FlagZ

                        wrksheet.Cells[h, 33] = "U:" + _kq.Rows[i + 1][3] + "";//UserName
                        if (truong1 != truong1UserNhap
                           || truong2 != truong2UserNhap
                           || truong3 != truong3UserNhap
                           || truong4_1 != truong4_1UserNhap
                           || truong4_2 != truong4_2UserNhap
                           || truong4_3 != truong4_3UserNhap
                           || truong5_1 != truong5_1UserNhap
                           || truong5_2 != truong5_2UserNhap
                           || truong6 != truong6UserNhap
                           || truong7 != truong7UserNhap
                           || truong8 != truong8UserNhap
                           || truong9 != truong9UserNhap
                           || truong10 != truong10UserNhap
                           || truong12 != truong11UserNhap
                           || truong13 != truong12UserNhap
                           || truong14 != truong13UserNhap
                           || truong15 != truong14UserNhap
                           || truong16 != truong15UserNhap
                           || truong17 != truong16UserNhap
                           || truong18 != truong17UserNhap
                           || truong19 != truong18UserNhap
                           || truong20 != truong20UserNhap
                           || truong21 != truong21UserNhap
                           || truong22 != truong22UserNhap
                           || truong23 != truong23UserNhap
                           || truong24 != truong24UserNhap
                           || truong25 != truong25UserNhap
                           || truong26 != truong26UserNhap)
                        {
                            soloi += 1;
                            if (demUser1 == tileCanSua)
                            {
                                wrksheet.Cells[h, 34] = truong1UserNhap;//Truong01
                                if (truong1 != truong1UserNhap)
                                {
                                    wrksheet.Cells[h, 34].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 35] = truong2UserNhap;//Truong02
                                if (truong2 != truong2UserNhap)
                                {
                                    wrksheet.Cells[h, 35].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 36] = truong3UserNhap;//Truong03
                                if (truong3 != truong3UserNhap)
                                {
                                    wrksheet.Cells[h, 36].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 37] = truong4_1UserNhap;//Truong04_1
                                if (truong4_1 != truong4_1UserNhap)
                                {
                                    wrksheet.Cells[h, 37].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 38] = truong4_2UserNhap;//Truong04_2
                                if (truong4_2 != truong4_2UserNhap)
                                {
                                    wrksheet.Cells[h, 38].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 39] = truong4_3UserNhap;//Truong04_3
                                if (truong4_3 != truong4_3UserNhap)
                                {
                                    wrksheet.Cells[h, 39].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 40] = truong5_1UserNhap;//Truong05_1
                                if (truong5_1 != truong5_1UserNhap)
                                {
                                    wrksheet.Cells[h, 40].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 41] = truong5_2UserNhap;//Truong05_2
                                if (truong5_2 != truong5_2UserNhap)
                                {
                                    wrksheet.Cells[h, 41].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 42] = truong6UserNhap;//Truong06
                                if (truong6 != truong6UserNhap)
                                {
                                    wrksheet.Cells[h, 42].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 43] = truong7UserNhap;//Truong07
                                if (truong7 != truong7UserNhap)
                                {
                                    wrksheet.Cells[h, 43].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 44] = truong8UserNhap;//Truong08
                                if (truong8 != truong8UserNhap)
                                {
                                    wrksheet.Cells[h, 44].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 45] = truong9UserNhap;//Truong09
                                if (truong9 != truong9UserNhap)
                                {
                                    wrksheet.Cells[h, 45].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 46] = truong10UserNhap;//Truong10
                                if (truong10 != truong10UserNhap)
                                {
                                    wrksheet.Cells[h, 46].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 47] = truong11UserNhap;//Truong11
                                if (truong11 != truong11UserNhap)
                                {
                                    wrksheet.Cells[h, 47].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 48] = truong12UserNhap;//Truong12
                                if (truong12 != truong12UserNhap)
                                {
                                    wrksheet.Cells[h, 48].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 49] = truong13UserNhap;//Truong13
                                if (truong13 != truong13UserNhap)
                                {
                                    wrksheet.Cells[h, 49].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 50] = truong14UserNhap;//Truong14
                                if (truong14 != truong14UserNhap)
                                {
                                    wrksheet.Cells[h, 50].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 51] = truong15UserNhap;//Truong15
                                if (truong15 != truong15UserNhap)
                                {
                                    wrksheet.Cells[h, 51].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 52] = truong16UserNhap;//Truong16
                                if (truong16 != truong16UserNhap)
                                {
                                    wrksheet.Cells[h, 52].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 53] = truong17UserNhap;//Truong17
                                if (truong17 != truong17UserNhap)
                                {
                                    wrksheet.Cells[h, 53].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 54] = truong18UserNhap;//Truong18
                                if (truong18 != truong18UserNhap)
                                {
                                    wrksheet.Cells[h, 54].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 55] = truong19UserNhap;//Truong19
                                if (truong19 != truong19UserNhap)
                                {
                                    wrksheet.Cells[h, 55].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 56] = truong20UserNhap;//Truong20
                                if (truong20 != truong20UserNhap)
                                {
                                    wrksheet.Cells[h, 56].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 57] = truong21UserNhap;//Truong21
                                if (truong21 != truong21UserNhap)
                                {
                                    wrksheet.Cells[h, 57].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 58] = truong22UserNhap;//Truong22
                                if (truong22 != truong22UserNhap)
                                {
                                    wrksheet.Cells[h, 58].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 59] = truong23UserNhap;//Truong23
                                if (truong23 != truong23UserNhap)
                                {
                                    wrksheet.Cells[h, 59].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 60] = truong24UserNhap;//Truong24
                                if (truong24 != truong24UserNhap)
                                {
                                    wrksheet.Cells[h, 60].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 61] = truong25UserNhap;//Truong25
                                if (truong25 != truong25UserNhap)
                                {
                                    wrksheet.Cells[h, 61].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 62] = truong26UserNhap;//Truong26
                                if (truong26 != truong26UserNhap)
                                {
                                    wrksheet.Cells[h, 62].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 63] = FlagZUserNhap; //FlagZ
                                if (FlagZ != FlagZUserNhap)
                                {
                                    wrksheet.Cells[h, 63].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 64] = _kq.Rows[i + 1][2] + ""; //IDPhieu
                                demUser1 = 1;
                                soloisausua += 1;
                            }
                            else
                            {
                                wrksheet.Cells[h, 34] = truong1;//Truong01
                                wrksheet.Cells[h, 35] = truong2;//Truong02
                                wrksheet.Cells[h, 36] = truong3;//Truong03
                                wrksheet.Cells[h, 37] = truong4_1;//Truong04_1
                                wrksheet.Cells[h, 38] = truong4_2;//Truong04_2
                                wrksheet.Cells[h, 39] = truong4_3;//Truong04_3
                                wrksheet.Cells[h, 40] = truong5_1;//Truong05_1
                                wrksheet.Cells[h, 41] = truong5_2;//Truong05_2
                                wrksheet.Cells[h, 42] = truong6;//Truong06
                                wrksheet.Cells[h, 43] = truong7;//Truong07
                                wrksheet.Cells[h, 44] = truong8;//Truong08
                                wrksheet.Cells[h, 45] = truong9;//Truong09
                                wrksheet.Cells[h, 46] = truong10;//Truong10
                                wrksheet.Cells[h, 47] = truong11;//Truong11
                                wrksheet.Cells[h, 48] = truong12;//Truong12
                                wrksheet.Cells[h, 49] = truong13;//Truong13
                                wrksheet.Cells[h, 50] = truong14;//Truong14
                                wrksheet.Cells[h, 51] = truong15;//Truong15
                                wrksheet.Cells[h, 52] = truong16;//Truong16
                                wrksheet.Cells[h, 53] = truong17;//Truong17
                                wrksheet.Cells[h, 54] = truong18;//Truong18
                                wrksheet.Cells[h, 55] = truong19;//Truong19
                                wrksheet.Cells[h, 56] = truong20;//Truong20
                                wrksheet.Cells[h, 57] = truong21;//Truong21
                                wrksheet.Cells[h, 58] = truong22;//Truong22
                                wrksheet.Cells[h, 59] = truong23;//Truong23
                                wrksheet.Cells[h, 60] = truong24;//Truong24
                                wrksheet.Cells[h, 61] = truong25;//Truong25
                                wrksheet.Cells[h, 62] = truong26;//Truong26
                                wrksheet.Cells[h, 63] = FlagZ; //FlagZ
                                wrksheet.Cells[h, 64] = _kq.Rows[i + 1][2] + ""; //IDPhieu
                                demUser1 += 1;
                            }
                        }
                        else
                        {
                            wrksheet.Cells[h, 34] = truong1;//Truong01
                            wrksheet.Cells[h, 35] = truong2;//Truong02
                            wrksheet.Cells[h, 36] = truong3;//Truong03
                            wrksheet.Cells[h, 37] = truong4_1;//Truong04_1
                            wrksheet.Cells[h, 38] = truong4_2;//Truong04_2
                            wrksheet.Cells[h, 39] = truong4_3;//Truong04_3
                            wrksheet.Cells[h, 40] = truong5_1;//Truong05_1
                            wrksheet.Cells[h, 41] = truong5_2;//Truong05_2
                            wrksheet.Cells[h, 42] = truong6;//Truong06
                            wrksheet.Cells[h, 43] = truong7;//Truong07
                            wrksheet.Cells[h, 44] = truong8;//Truong08
                            wrksheet.Cells[h, 45] = truong9;//Truong09
                            wrksheet.Cells[h, 46] = truong10;//Truong10
                            wrksheet.Cells[h, 47] = truong11;//Truong11
                            wrksheet.Cells[h, 48] = truong12;//Truong12
                            wrksheet.Cells[h, 49] = truong13;//Truong13
                            wrksheet.Cells[h, 50] = truong14;//Truong14
                            wrksheet.Cells[h, 51] = truong15;//Truong15
                            wrksheet.Cells[h, 52] = truong16;//Truong16
                            wrksheet.Cells[h, 53] = truong17;//Truong17
                            wrksheet.Cells[h, 54] = truong18;//Truong18
                            wrksheet.Cells[h, 55] = truong19;//Truong19
                            wrksheet.Cells[h, 56] = truong20;//Truong20
                            wrksheet.Cells[h, 57] = truong21;//Truong21
                            wrksheet.Cells[h, 58] = truong22;//Truong22
                            wrksheet.Cells[h, 59] = truong23;//Truong23
                            wrksheet.Cells[h, 60] = truong24;//Truong24
                            wrksheet.Cells[h, 61] = truong25;//Truong25
                            wrksheet.Cells[h, 62] = truong26;//Truong26
                            wrksheet.Cells[h, 63] = FlagZ; //FlagZ
                            wrksheet.Cells[h, 64] = _kq.Rows[i + 1][2] + ""; //IDPhieu
                        }


                        //Dữ liệu User 2
                        truong1UserNhap = _kq.Rows[i + 2][4] + "";//Truong01
                        truong2UserNhap = _kq.Rows[i + 2][5] + "";//Truong02
                        truong3UserNhap = _kq.Rows[i + 2][6] + "";//Truong03
                        truong4_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][7] + "", 2, "0");//Truong04_1
                        truong4_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][8] + "", 2, "0");//Truong04_2
                        truong4_3UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][9] + "", 2, "0");//Truong04_3
                        truong5_1UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][10] + "", 2, "0");//Truong05_1
                        truong5_2UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][11] + "", 2, "0");//Truong05_2
                        truong6UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][12] + "", 2, "0");//Truong06
                        truong7UserNhap = _kq.Rows[i + 2][13] + "";//Truong07
                        truong8UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][14] + "", "0");//Truong08
                        truong9UserNhap = _kq.Rows[i + 2][15] + "";//Truong09
                        truong10UserNhap = _kq.Rows[i + 2][16] + "";//Truong10
                        truong11UserNhap = _kq.Rows[i + 2][17] + "";//Truong11
                        truong12UserNhap = _kq.Rows[i + 2][18] + "";//Truong12
                        truong13UserNhap = _kq.Rows[i + 2][19] + "";//Truong13
                        truong14UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][20] + "", "0");//Truong14
                        truong15UserNhap = _kq.Rows[i + 2][21] + "";//Truong15
                        truong16UserNhap = _kq.Rows[i + 2][22] + "";//Truong16
                        truong17UserNhap = _kq.Rows[i + 2][23] + "";//Truong17
                        truong18UserNhap = _kq.Rows[i + 2][24] + "";//Truong18
                        truong19UserNhap = _kq.Rows[i + 2][25] + "";//Truong19
                        truong20UserNhap = ThemKyTuPhiaTruoc_8_14_20(_kq.Rows[i + 2][26] + "", "0");//Truong20
                        truong21UserNhap = _kq.Rows[i + 2][27] + "";//Truong21
                        truong22UserNhap = _kq.Rows[i + 2][28] + "";//Truong22
                        truong23UserNhap = _kq.Rows[i + 2][29] + "";//Truong23
                        truong24UserNhap = _kq.Rows[i + 2][30] + "";//Truong24
                        if (string.IsNullOrEmpty(_kq.Rows[i + 2][31] + "") && !string.IsNullOrEmpty(_kq.Rows[i + 2][32] + ""))
                        {
                            truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][32] + "", 2, "0");//Truong25
                            wrksheet.Cells[h, 62] = "";                      //Truong26
                        }
                        else
                        {
                            truong25UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][31] + "", 2, "0");//Truong25
                            truong26UserNhap = ThemKyTuPhiaTruoc(_kq.Rows[i + 2][32] + "", 2, "0");//Truong26
                        }
                        FlagZUserNhap = _kq.Rows[i + 2][33] + ""; //FlagZ

                        wrksheet.Cells[h, 65] = "V:" + _kq.Rows[i + 2][3] + "";//UserName
                        if (truong1 != truong1UserNhap
                           || truong2 != truong2UserNhap
                           || truong3 != truong3UserNhap
                           || truong4_1 != truong4_1UserNhap
                           || truong4_2 != truong4_2UserNhap
                           || truong4_3 != truong4_3UserNhap
                           || truong5_1 != truong5_1UserNhap
                           || truong5_2 != truong5_2UserNhap
                           || truong6 != truong6UserNhap
                           || truong7 != truong7UserNhap
                           || truong8 != truong8UserNhap
                           || truong9 != truong9UserNhap
                           || truong10 != truong10UserNhap
                           || truong12 != truong11UserNhap
                           || truong13 != truong12UserNhap
                           || truong14 != truong13UserNhap
                           || truong15 != truong14UserNhap
                           || truong16 != truong15UserNhap
                           || truong17 != truong16UserNhap
                           || truong18 != truong17UserNhap
                           || truong19 != truong18UserNhap
                           || truong20 != truong20UserNhap
                           || truong21 != truong21UserNhap
                           || truong22 != truong22UserNhap
                           || truong23 != truong23UserNhap
                           || truong24 != truong24UserNhap
                           || truong25 != truong25UserNhap
                           || truong26 != truong26UserNhap)
                        {
                            soloi += 1;
                            if (demUser2 == tileCanSua)
                            {
                                wrksheet.Cells[h, 66] = truong1UserNhap;//Truong01
                                if (truong1 != truong1UserNhap)
                                {
                                    wrksheet.Cells[h, 66].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 67] = truong2UserNhap;//Truong02
                                if (truong2 != truong2UserNhap)
                                {
                                    wrksheet.Cells[h, 67].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 68] = truong3UserNhap;//Truong03
                                if (truong3 != truong3UserNhap)
                                {
                                    wrksheet.Cells[h, 68].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 69] = truong4_1UserNhap;//Truong04_1
                                if (truong4_1 != truong4_1UserNhap)
                                {
                                    wrksheet.Cells[h, 69].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 70] = truong4_2UserNhap;//Truong04_2
                                if (truong4_2 != truong4_2UserNhap)
                                {
                                    wrksheet.Cells[h, 70].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 71] = truong4_3UserNhap;//Truong04_3
                                if (truong4_3 != truong4_3UserNhap)
                                {
                                    wrksheet.Cells[h, 71].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 72] = truong5_1UserNhap;//Truong05_1
                                if (truong5_1 != truong5_1UserNhap)
                                {
                                    wrksheet.Cells[h, 72].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 73] = truong5_2UserNhap;//Truong05_2
                                if (truong5_2 != truong5_2UserNhap)
                                {
                                    wrksheet.Cells[h, 73].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 74] = truong6UserNhap;//Truong06
                                if (truong6 != truong6UserNhap)
                                {
                                    wrksheet.Cells[h, 74].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 75] = truong7UserNhap;//Truong07
                                if (truong7 != truong7UserNhap)
                                {
                                    wrksheet.Cells[h, 75].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 76] = truong8UserNhap;//Truong08
                                if (truong8 != truong8UserNhap)
                                {
                                    wrksheet.Cells[h, 76].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 77] = truong9UserNhap;//Truong09
                                if (truong9 != truong9UserNhap)
                                {
                                    wrksheet.Cells[h, 77].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 78] = truong19UserNhap;//Truong10
                                if (truong10 != truong19UserNhap)
                                {
                                    wrksheet.Cells[h, 78].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 79] = truong11UserNhap;//Truong11
                                if (truong11 != truong11UserNhap)
                                {
                                    wrksheet.Cells[h, 79].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 80] = truong12UserNhap;//Truong12
                                if (truong12 != truong12UserNhap)
                                {
                                    wrksheet.Cells[h, 80].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 81] = truong13UserNhap;//Truong13
                                if (truong13 != truong13UserNhap)
                                {
                                    wrksheet.Cells[h, 81].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 82] = truong14UserNhap;//Truong14
                                if (truong14 != truong14UserNhap)
                                {
                                    wrksheet.Cells[h, 82].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 83] = truong15UserNhap;//Truong15
                                if (truong15 != truong15UserNhap)
                                {
                                    wrksheet.Cells[h, 83].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 84] = truong16UserNhap;//Truong16
                                if (truong16 != truong16UserNhap)
                                {
                                    wrksheet.Cells[h, 84].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 85] = truong17UserNhap;//Truong17
                                if (truong17 != truong17UserNhap)
                                {
                                    wrksheet.Cells[h, 85].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 86] = truong18UserNhap;//Truong18
                                if (truong18 != truong18UserNhap)
                                {
                                    wrksheet.Cells[h, 86].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 87] = truong19UserNhap;//Truong19
                                if (truong19 != truong19UserNhap)
                                {
                                    wrksheet.Cells[h, 87].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 88] = truong20UserNhap;//Truong20
                                if (truong20 != truong20UserNhap)
                                {
                                    wrksheet.Cells[h, 88].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 89] = truong21UserNhap;//Truong21
                                if (truong21 != truong21UserNhap)
                                {
                                    wrksheet.Cells[h, 89].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 90] = truong22UserNhap;//Truong22
                                if (truong22 != truong22UserNhap)
                                {
                                    wrksheet.Cells[h, 90].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 91] = truong23UserNhap;//Truong23
                                if (truong23 != truong23UserNhap)
                                {
                                    wrksheet.Cells[h, 91].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 92] = truong24UserNhap;//Truong24
                                if (truong24 != truong24UserNhap)
                                {
                                    wrksheet.Cells[h, 92].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 93] = truong25UserNhap;//Truong25
                                if (truong25 != truong25UserNhap)
                                {
                                    wrksheet.Cells[h, 93].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 94] = truong26UserNhap;//Truong26
                                if (truong26 != truong26UserNhap)
                                {
                                    wrksheet.Cells[h, 94].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 95] = FlagZUserNhap; //FlagZ
                                if (FlagZ != FlagZUserNhap)
                                {
                                    wrksheet.Cells[h, 34].Interior.Color = Color.Red;
                                }
                                wrksheet.Cells[h, 96] = _kq.Rows[i + 2][2] + ""; //IDPhieu
                                demUser2 = 1;
                                soloisausua += 1;
                            }
                            else
                            {
                                wrksheet.Cells[h, 66] = truong1;//Truong01
                                wrksheet.Cells[h, 67] = truong2;//Truong02
                                wrksheet.Cells[h, 68] = truong3;//Truong03
                                wrksheet.Cells[h, 69] = truong4_1;//Truong04_1
                                wrksheet.Cells[h, 70] = truong4_2;//Truong04_2
                                wrksheet.Cells[h, 71] = truong4_3;//Truong04_3
                                wrksheet.Cells[h, 72] = truong5_1;//Truong05_1
                                wrksheet.Cells[h, 73] = truong5_2;//Truong05_2
                                wrksheet.Cells[h, 74] = truong6;//Truong06
                                wrksheet.Cells[h, 75] = truong7;//Truong07
                                wrksheet.Cells[h, 76] = truong8;//Truong08
                                wrksheet.Cells[h, 77] = truong9;//Truong09
                                wrksheet.Cells[h, 78] = truong19;//Truong10
                                wrksheet.Cells[h, 79] = truong11;//Truong11
                                wrksheet.Cells[h, 80] = truong12;//Truong12
                                wrksheet.Cells[h, 81] = truong13;//Truong13
                                wrksheet.Cells[h, 82] = truong14;//Truong14
                                wrksheet.Cells[h, 83] = truong15;//Truong15
                                wrksheet.Cells[h, 84] = truong16;//Truong16
                                wrksheet.Cells[h, 85] = truong17;//Truong17
                                wrksheet.Cells[h, 86] = truong18;//Truong18
                                wrksheet.Cells[h, 87] = truong19;//Truong19
                                wrksheet.Cells[h, 88] = truong20;//Truong20
                                wrksheet.Cells[h, 89] = truong21;//Truong21
                                wrksheet.Cells[h, 90] = truong22;//Truong22
                                wrksheet.Cells[h, 91] = truong23;//Truong23
                                wrksheet.Cells[h, 92] = truong24;//Truong24
                                wrksheet.Cells[h, 93] = truong25;//Truong25
                                wrksheet.Cells[h, 94] = truong26;//Truong26
                                wrksheet.Cells[h, 95] = FlagZ; //FlagZ
                                wrksheet.Cells[h, 96] = _kq.Rows[i + 2][2] + ""; //IDPhieu
                                demUser2 += 1;
                            }
                        }
                        else
                        {
                            wrksheet.Cells[h, 66] = truong1;//Truong01
                            wrksheet.Cells[h, 67] = truong2;//Truong02
                            wrksheet.Cells[h, 68] = truong3;//Truong03
                            wrksheet.Cells[h, 69] = truong4_1;//Truong04_1
                            wrksheet.Cells[h, 70] = truong4_2;//Truong04_2
                            wrksheet.Cells[h, 71] = truong4_3;//Truong04_3
                            wrksheet.Cells[h, 72] = truong5_1;//Truong05_1
                            wrksheet.Cells[h, 73] = truong5_2;//Truong05_2
                            wrksheet.Cells[h, 74] = truong6;//Truong06
                            wrksheet.Cells[h, 75] = truong7;//Truong07
                            wrksheet.Cells[h, 76] = truong8;//Truong08
                            wrksheet.Cells[h, 77] = truong9;//Truong09
                            wrksheet.Cells[h, 78] = truong19;//Truong10
                            wrksheet.Cells[h, 79] = truong11;//Truong11
                            wrksheet.Cells[h, 80] = truong12;//Truong12
                            wrksheet.Cells[h, 81] = truong13;//Truong13
                            wrksheet.Cells[h, 82] = truong14;//Truong14
                            wrksheet.Cells[h, 83] = truong15;//Truong15
                            wrksheet.Cells[h, 84] = truong16;//Truong16
                            wrksheet.Cells[h, 85] = truong17;//Truong17
                            wrksheet.Cells[h, 86] = truong18;//Truong18
                            wrksheet.Cells[h, 87] = truong19;//Truong19
                            wrksheet.Cells[h, 88] = truong20;//Truong20
                            wrksheet.Cells[h, 89] = truong21;//Truong21
                            wrksheet.Cells[h, 90] = truong22;//Truong22
                            wrksheet.Cells[h, 91] = truong23;//Truong23
                            wrksheet.Cells[h, 92] = truong24;//Truong24
                            wrksheet.Cells[h, 93] = truong25;//Truong25
                            wrksheet.Cells[h, 94] = truong26;//Truong26
                            wrksheet.Cells[h, 95] = FlagZ; //FlagZ
                            wrksheet.Cells[h, 96] = _kq.Rows[i + 2][2] + ""; //IDPhieu
                        }
                        lb_Complete.Text = (h - 1) + "/" + RowCount / 3 + " (" + (int)(((double)(h - 1) / (double)(RowCount/3))*100) + "%) ";
                        progressBar1.PerformStep();
                        h++;
                    }
                }
                Microsoft.Office.Interop.Excel.Range rowHead = wrksheet.get_Range("A2", "CR" + (h - 1));
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

                //-----------
                try
                {
                    book.SaveCopyAs(txt_path.Text+@"\"+ namefileExcel+".xlsx");
                    book.Saved = true;
                    Process.Start(txt_path.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (book != null)
                        book.Close(false);
                    if (App != null)
                        App.Quit();
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx");
                    }
                }
            }
            btn_Browse.Enabled = true;
            list_Batch.Enabled = true;
            txt_path.Enabled = true;
        }
        
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //try
            //{
            //    string savePath = "";
            //    saveFileDialog1.Title = "Save Excel Files";
            //    saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            //    saveFileDialog1.FileName = namefileExcel;
            //    saveFileDialog1.RestoreDirectory = true;
            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        book.SaveCopyAs(saveFileDialog1.FileName);
            //        book.Saved = true;
            //        savePath = Path.GetDirectoryName(saveFileDialog1.FileName);
            //    }
            //    else
            //    {
            //        MessageBox.Show(@"Error exporting excel!");
            //        return;
            //    }
            //    Process.Start(savePath);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (book != null)
            //        book.Close(false);
            //    if (App != null)
            //        App.Quit();
            //    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx"))
            //    {
            //        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + namefileExcel + ".xlsx");
            //    }
            //}
        }

        private void cbb_Batch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 3)
                e.Handled = true;
        }
        
        private void cbb_Batch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                ((ComboBox)sender).Text = "";
        }
        FolderBrowserDialog fbd = new FolderBrowserDialog();

        private void frm_ExportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                if(MessageBox.Show("Quá trình xuất file đang diễn ra, Bạn chắc chắn muốn dừng ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                {
                    return;
                }
                e.Cancel = true;
            }
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
                txt_path.Text = fbd.SelectedPath.ToString();
        }
    }
}