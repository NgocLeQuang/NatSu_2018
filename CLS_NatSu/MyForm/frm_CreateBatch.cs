using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CLS_NatSu.MyData;
using System.Drawing;
using CLS_NatSu.MyClass;

namespace CLS_NatSu.MyForm
{
    public partial class frm_CreateBatch : DevExpress.XtraEditors.XtraForm
    {
        private string[] _lFileNames;
        private bool _multi;
        private int soluonghinh;

        public frm_CreateBatch()
        {
            InitializeComponent();
        }

        private void btn_Browser_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.FocusFolder_UpBatch = folderBrowserDialog1.SelectedPath;
                txt_PathFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btn_BrowserImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_BatchName.Text))
            {
                MessageBox.Show("Vui lòng điền tên batch", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Types Image|*.jpg;*.jpeg;*.png;*.tif;*.tiff";

            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _lFileNames = dlg.FileNames;
                txt_ImagePath.Text = Path.GetDirectoryName(dlg.FileName);
            }
            soluonghinh = 0;
            soluonghinh = dlg.FileNames.Length;
            lb_SoLuongHinh.Text = dlg.FileNames.Length + " files ";
        }
        string folderBatch = "";
        private void btn_CreateBatch_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Quá trình tạo batch đang diễn ra, Bạn hãy chờ quá trình tạo batch kết thúc mới tiếp tục tạo batch mới !");
                return;
            }
            lb_SobatchHoanThanh.Text = "";
            txt_DateCreate.Text = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            backgroundWorker1.RunWorkerAsync();
            //UpLoadMulti_3Folder();
        }

        private void txt_BatchName_EditValueChanged(object sender, EventArgs e)
        {
            folderBatch = "";
            if (!string.IsNullOrEmpty(txt_BatchName.Text))
            {
                _multi = false;
                txt_PathFolder.Enabled = false;
                btn_Browser.Enabled = false;
            }
            else
            {
                txt_PathFolder.Enabled = true;
                btn_Browser.Enabled = true;
            }
        }

        private void txt_PathFolder_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_PathFolder.Text))
                {
                    _multi = true;
                    folderBatch = Path.GetFileName(Path.GetDirectoryName(txt_PathFolder.Text+@"\"));
                    txt_BatchName.Enabled = false;
                    txt_ImagePath.Enabled = false;
                    btn_BrowserImage.Enabled = false;
                }
                else
                {
                    folderBatch = "";
                    txt_BatchName.Enabled = true;
                    txt_ImagePath.Enabled = true;
                    btn_BrowserImage.Enabled = true;
                }
            }
            catch
            {
                folderBatch = "";
            }
        }

        private bool flag_load = false;

        private void frm_CreateBatch_Load(object sender, EventArgs e)
        {
            flag_load = true;
            chk_ChiaUser.Checked = true;
            txt_UserCreate.Text = Global.StrUserName;
            txt_DateCreate.Text = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            txt_Path.Text = @"X:\N\13";
            flag_load = false;
            txt_BatchName.Focus();
        }

        public static string[] GetFilesFrom(string searchFolder, string[] filters, bool isRecursive)
        {
            List<string> filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, $"*.{filter}", searchOption));
            }
            return filesFound.ToArray();
        }

        string[] separators = { @"\", "/" };
        private void UpLoadSingle()
        {
            if (txt_Path.Text == @"X:\N\13")
            {
                MessageBox.Show("Bạn đang để đường dẫn path mặc định. Vui lòng kiểm tra lại.");
                return;
            }
            progressBar1.Step = 1;
            progressBar1.Value = 1;
            progressBar1.Maximum = _lFileNames.Length;
            progressBar1.Minimum = 0;
            ModifyProgressBarColor.SetState(progressBar1, 1);

            string sBatchID = (txt_BatchName.Text +txt_DateCreate.Text).Replace("/", "").Replace(@"\", "").Replace(@":", "").Replace(@"-", "");
            var batch = (from w in Global.Db.tbl_Batches.Where(w => w.BatchID == sBatchID) select w.BatchID).FirstOrDefault();
            if (!string.IsNullOrEmpty(txt_ImagePath.Text))
            {
                if (string.IsNullOrEmpty(batch))
                {
                    var fBatch = new tbl_Batch
                    {
                        BatchID = sBatchID,
                        BatchName = txt_BatchName.Text,
                        UserCreate = txt_UserCreate.Text,
                        DateCreate = DateTime.Now,
                        PathServer = "",
                        PathPicture = txt_Path.Text,
                        Location = txt_ImagePath.Text,
                        NumberImage = soluonghinh.ToString(),
                        ChiaUser = chk_ChiaUser.Checked ? true : false,
                        CongKhaiBatch = false,
                    };
                    Global.Db.tbl_Batches.InsertOnSubmit(fBatch);
                    Global.Db.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("Batch đã tồn tại vui lòng điền tên batch khác!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn hình ảnh!");
                return;
            }

            string temp = Global.StrPath + "\\" + sBatchID;
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            else
            {
                MessageBox.Show("Bị trùng tên batch!");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new[] { new DataColumn("ImageID", typeof(string)) });
            for (int i = 0; i < _lFileNames.Count(); i++)
            {
                FileInfo fi = new FileInfo(_lFileNames[i]);
                dt.Rows.Add(fi.Name);
            }
            string ConnectionString = Global.Db.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("Insert_Image", con);
            cmd.CommandTimeout = 10 * 60;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BatchID", sBatchID);
            cmd.Parameters.AddWithValue("@ListIdImage", dt);
            cmd.Parameters.AddWithValue("@ChiaUser", chk_ChiaUser.Checked ? 1 : 0);
            con.Open();
            cmd.ExecuteNonQuery();

            for (int i = 0; i < _lFileNames.Count(); i++)
            {
                File.Copy(_lFileNames[i], temp + @"\" + new FileInfo(_lFileNames[i]).Name);
                progressBar1.PerformStep();
                lb_SoImageDaHoanThanh.Text = (i + 1) + @"\" + _lFileNames.Count();
            }
            txt_DateCreate.Text = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            MessageBox.Show("Tạo batch mới thành công!");
            txt_BatchName.Text = "";
            txt_ImagePath.Text = "";
            txt_Path.Text = @"X:\N\13";
            lb_SoLuongHinh.Text = "";
        }
        string sBatchID = "", batch="";
        private void UpLoadMulti()
        {
            try
            {
                btn_Browser.Enabled = false;
                txt_PathFolder.Enabled = false;
                txt_Path.Enabled = false;
                List<string> lStrBath = new List<string>();
                lStrBath.AddRange(Directory.GetDirectories(txt_PathFolder.Text));
                int countBatchExists = 0;
                string listBatchExxists = "";
                for (int i = 0; i < lStrBath.Count; i++)
                {
                    sBatchID = (new DirectoryInfo(lStrBath[i]).Name + txt_DateCreate.Text).Replace("/", "").Replace(@"\", "").Replace(@":", "").Replace(@"-", "");
                    batch = (from w in Global.Db.tbl_Batches.Where(w => w.BatchID == sBatchID) select w.BatchID).FirstOrDefault();
                    if (!string.IsNullOrEmpty(batch))
                    {
                        countBatchExists += 1;
                        listBatchExxists += lStrBath[i] + "\r\n";
                    }
                }
                if (countBatchExists > 0)
                {
                    MessageBox.Show("Batch đã tồn tại :\r\n" + listBatchExxists);
                    btn_Browser.Enabled = true;
                    txt_PathFolder.Enabled = true;
                    txt_Path.Enabled = true;
                    return;
                }
                int n = 0;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new[] { new DataColumn("ImageID", typeof(string)) });
                foreach (string itemBatch in lStrBath)
                {
                    dt.Clear();
                    string batchName = "";
                    int m = 0;
                    batchName = (new DirectoryInfo(itemBatch).Name + txt_DateCreate.Text).Replace("/", "").Replace(@"\", "").Replace(@":", "").Replace(@"-", "");
                    n += 1;
                    lb_SobatchHoanThanh.Text = n + @" :";
                    var filters = new String[] { "jpg", "jpeg", "tif" };
                    string[] pathImageLocation = GetFilesFrom(itemBatch, filters, true);

                    var fBatch = new tbl_Batch
                    {
                        BatchID = batchName,
                        BatchName = new DirectoryInfo(itemBatch).Name,
                        UserCreate = txt_UserCreate.Text,
                        DateCreate = DateTime.Now,
                        PathServer = "",
                        PathPicture = txt_Path.Text,
                        Location = txt_PathFolder.Text,
                        NumberImage = pathImageLocation.Length + "",
                        ChiaUser = chk_ChiaUser.Checked ? true : false,
                        CongKhaiBatch = false,
                    };
                    Global.Db.tbl_Batches.InsertOnSubmit(fBatch);
                    Global.Db.SubmitChanges();

                    progressBar1.Step = 1;
                    progressBar1.Value = 1;
                    progressBar1.Maximum = pathImageLocation.Count();
                    progressBar1.Minimum = 0;
                    ModifyProgressBarColor.SetState(progressBar1, 1);

                    for (int i = 0; i < pathImageLocation.Count(); i++)
                    {
                        FileInfo fi = new FileInfo(pathImageLocation[i]);
                        dt.Rows.Add(fi.Name);
                    }
                    string ConnectionString = Global.Db.Connection.ConnectionString;
                    SqlConnection con = new SqlConnection(ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert_Image", con);
                    cmd.CommandTimeout = 10 * 60;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BatchID", batchName);
                    cmd.Parameters.AddWithValue("@ListIdImage", dt);
                    cmd.Parameters.AddWithValue("@ChiaUser", chk_ChiaUser.Checked ? 1 : 0);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    string temp = Global.StrPath + "\\" + batchName;
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    else
                    {
                        MessageBox.Show("Bị trùng tên batch!");
                        return;
                    }
                    for (int i = 0; i < pathImageLocation.Count(); i++)
                    {
                        File.Copy(pathImageLocation[i], temp + @"\" + new FileInfo(pathImageLocation[i]).Name);
                        lb_SoImageDaHoanThanh.Text = (i + 1) + @"\" + pathImageLocation.Count();
                        m += 1;
                        progressBar1.PerformStep();
                    }
                    lb_SoImageDaHoanThanh.Text = m + @"/" + pathImageLocation.Length;
                }
                MessageBox.Show(@"Tạo batch mới thành công!");
                txt_BatchName.Text = "";
                txt_ImagePath.Text = "";
                lb_SoLuongHinh.Text = "";
                txt_PathFolder.Text = "";
                txt_Path.Text = @"X:\N\13";
                //btn_CreateBatch.Enabled = true;
                btn_Browser.Enabled = true;
                txt_PathFolder.Enabled = true;
                txt_Path.Enabled = true;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void createFolder(string nameFolder)
        {
            string temp = Global.StrPath + "\\" + nameFolder;
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
        }
        private void UpLoadMulti_3Folder()
        {
            List<string> lStrBath1 = new List<string>();
            List<string> lStrBath2 = new List<string>();
            lStrBath1.AddRange(Directory.GetDirectories(txt_PathFolder.Text));
            int total = lStrBath1.Count;
            string pathserver = @"\" + new DirectoryInfo(txt_PathFolder.Text).Name;
            string pathexcel = @"X:\N\13";
            string s = new DirectoryInfo(txt_PathFolder.Text).Name;
            createFolder(s);
            int n = 0;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new[] { new DataColumn("ImageID", typeof(string))});
            foreach (string item1 in lStrBath1)
            {
                string pathexcel1 = pathexcel+ @"\" + new DirectoryInfo(item1).Name;
                lStrBath2.Clear();
                lStrBath2.AddRange(Directory.GetDirectories(item1));
                if (lStrBath2.Count > 0)
                {
                    string batchID = (new DirectoryInfo(new DirectoryInfo(item1).Name).Name + txt_DateCreate.Text).Replace("/", "").Replace(@"\", "").Replace(@":", "").Replace(@"-", "");
                    bool BatchNew = false;
                    foreach (string item2 in lStrBath2)
                    {
                        string pathexcel2 = pathexcel1 + @"\" + new DirectoryInfo(item2).Name;
                        dt.Clear();
                        int m = 0;
                        n += 1;
                        lb_SobatchHoanThanh.Text = n + @" :";
                        var filters = new String[] { "jpg", "jpeg", "tif" };
                        string[] pathImageLocation = GetFilesFrom(item2, filters, true);
                        if (!BatchNew)
                        {
                            BatchNew = true;
                            var fBatch = new tbl_Batch
                            {
                                BatchID = batchID,
                                BatchName = new DirectoryInfo(item1).Name,
                                UserCreate = txt_UserCreate.Text,
                                DateCreate = DateTime.Now,
                                PathServer = pathserver+ @"\",
                                PathPicture = pathexcel + @"\" + new DirectoryInfo(item1).Name + @"\" + new DirectoryInfo(item2).Name,
                                Location = txt_PathFolder.Text,
                                NumberImage = pathImageLocation.Length + "",
                                ChiaUser = chk_ChiaUser.Checked ? true : false,
                                CongKhaiBatch = false,
                            };
                            Global.Db.tbl_Batches.InsertOnSubmit(fBatch);
                            Global.Db.SubmitChanges();
                        }
                        progressBar1.Step = 1;
                        progressBar1.Value = 1;
                        progressBar1.Maximum = pathImageLocation.Count();
                        progressBar1.Minimum = 0;
                        ModifyProgressBarColor.SetState(progressBar1, 1);
                        for (int j = 0; j < pathImageLocation.Count(); j++)
                        {
                            FileInfo fi = new FileInfo(pathImageLocation[j]);
                            dt.Rows.Add(fi.Name);
                        }
                        string ConnectionString = Global.Db.Connection.ConnectionString;
                        SqlConnection con = new SqlConnection(ConnectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert_Image", con);
                        cmd.CommandTimeout = 10 * 60;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchID", batchID);
                        cmd.Parameters.AddWithValue("@ListIdImage", dt);
                        cmd.Parameters.AddWithValue("@ChiaUser", chk_ChiaUser.Checked ? 1 : 0);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        string temp = Global.StrPath + "\\" + pathserver + "\\" + batchID;
                        if (!Directory.Exists(temp))
                        {
                            Directory.CreateDirectory(temp);
                        }
                        else
                        {
                            MessageBox.Show("Bị trùng tên batch!");
                            return;
                        }
                        for (int j = 0; j < pathImageLocation.Count(); j++)
                        {
                            File.Copy(pathImageLocation[j], temp + @"\" + new FileInfo(pathImageLocation[j]).Name);
                            lb_SoImageDaHoanThanh.Text = (j + 1) + @"\" + pathImageLocation.Count();
                            m += 1;
                            progressBar1.PerformStep();
                        }
                        lb_SoImageDaHoanThanh.Text = m + @"/" + pathImageLocation.Length;
                    }
                }
            }
            MessageBox.Show(@"Tạo batch mới thành công!");
            txt_BatchName.Text = "";
            txt_ImagePath.Text = "";
            lb_SoLuongHinh.Text = "";
            txt_PathFolder.Text = "";
            txt_Path.Text = @"X:\N\13";
            btn_Browser.Enabled = true;
            txt_PathFolder.Enabled = true;
            txt_Path.Enabled = true;
        }

        private void UpLoadMulti_5Folder()
        {
            List<string> lStrBath1 = new List<string>();
            List<string> lStrBath2 = new List<string>();
            List<string> lStrBath3 = new List<string>();
            List<string> lStrBath4 = new List<string>();
            lStrBath1.AddRange(Directory.GetDirectories(txt_PathFolder.Text));
            int k = 0;
            int total = lStrBath1.Count;
            string pathserver = @"\"+new DirectoryInfo(txt_PathFolder.Text).Name;
            string pathexcel = @"X:";
            string s = new DirectoryInfo(txt_PathFolder.Text).Name;
            createFolder(s);
            int n = 0;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new[] { new DataColumn("ImageID", typeof(string)) });
            foreach (string item1 in lStrBath1)
            {
                string pathserver1 = pathserver + @"\" + new DirectoryInfo(item1).Name;
                string litem1 = s + @"\" + new DirectoryInfo(item1).Name;
                createFolder(litem1);
                lStrBath2.Clear();
                lStrBath2.AddRange(Directory.GetDirectories(item1));
                if (lStrBath2.Count > 0)
                {
                    foreach (string item2 in lStrBath2)
                    {
                        string litem2 = litem1 + @"\" + new DirectoryInfo(item2).Name;
                        createFolder(litem2);
                        lStrBath3.Clear();
                        lStrBath3.AddRange(Directory.GetDirectories(item2));
                        if (lStrBath3.Count > 0)
                        {
                            foreach (string item3 in lStrBath3)
                            {
                                string pathserver2 = pathserver1 + @"\" + new DirectoryInfo(item3).Name;
                                string litem3 = litem2 + @"\" + new DirectoryInfo(item3).Name;
                                createFolder(litem3);
                                lStrBath4.Clear();
                                lStrBath4.AddRange(Directory.GetDirectories(item3));
                                if (lStrBath4.Count > 0)
                                {
                                    for (int i = 0; i < lStrBath4.Count; i++)
                                    {
                                        string FolderNameTemp = new DirectoryInfo(lStrBath4[i]).Name;
                                        if (true/*FolderNameTemp.Substring(0, 7) == "2225000"*/)
                                        {
                                            string batchID  = (new DirectoryInfo(lStrBath4[i]).Name + txt_DateCreate.Text).Replace("/", "").Replace(@"\", "").Replace(@":", "").Replace(@"-", "");
                                            string pathserver3 = pathserver2 + @"\" + new DirectoryInfo(lStrBath4[i]).Name + @"\";
                                            string litem4 = litem3 + @"\" + batchID;
                                            dt.Clear();
                                            int m = 0;
                                            n += 1;
                                            lb_SobatchHoanThanh.Text = n + @" :";
                                            var filters = new String[] { "jpg", "jpeg", "tif" };
                                            string[] pathImageLocation = GetFilesFrom(lStrBath4[i], filters, true);

                                            var fBatch = new tbl_Batch
                                            {
                                                BatchID = batchID,
                                                BatchName = new DirectoryInfo(lStrBath4[i]).Name,
                                                UserCreate = txt_UserCreate.Text,
                                                DateCreate = DateTime.Now,
                                                PathServer= pathserver1 + @"\" + new DirectoryInfo(item2).Name + @"\" + new DirectoryInfo(item3).Name+@"\",
                                                PathPicture = pathexcel+pathserver3,
                                                Location = txt_PathFolder.Text,
                                                NumberImage = pathImageLocation.Length + "",
                                                ChiaUser = chk_ChiaUser.Checked ? true : false,
                                                CongKhaiBatch = false,
                                            };
                                            Global.Db.tbl_Batches.InsertOnSubmit(fBatch);
                                            Global.Db.SubmitChanges();

                                            progressBar1.Step = 1;
                                            progressBar1.Value = 1;
                                            progressBar1.Maximum = pathImageLocation.Count();
                                            progressBar1.Minimum = 0;
                                            ModifyProgressBarColor.SetState(progressBar1, 1);
                                            for (int j = 0;j < pathImageLocation.Count(); j++)
                                            {
                                                FileInfo fi = new FileInfo(pathImageLocation[j]);
                                                dt.Rows.Add(fi.Name);
                                            }
                                            string ConnectionString = Global.Db.Connection.ConnectionString;
                                            SqlConnection con = new SqlConnection(ConnectionString);
                                            con.Open();
                                            SqlCommand cmd = new SqlCommand("Insert_Image", con);
                                            cmd.CommandTimeout = 10 * 60;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@BatchID", batchID);
                                            cmd.Parameters.AddWithValue("@ListIdImage", dt);
                                            cmd.Parameters.AddWithValue("@ChiaUser", chk_ChiaUser.Checked ? 1 : 0);
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            string temp = Global.StrPath + "\\" + litem4;
                                            if (!Directory.Exists(temp))
                                            {
                                                Directory.CreateDirectory(temp);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Bị trùng tên batch!");
                                                return;
                                            }
                                            for (int j = 0; j < pathImageLocation.Count(); j++)
                                            {
                                                File.Copy(pathImageLocation[j], temp + @"\" + new FileInfo(pathImageLocation[j]).Name);
                                                lb_SoImageDaHoanThanh.Text = (j + 1) + @"\" + pathImageLocation.Count();
                                                m += 1;
                                                progressBar1.PerformStep();
                                            }
                                            lb_SoImageDaHoanThanh.Text = m + @"/" + pathImageLocation.Length;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                k++;
            }
            MessageBox.Show(@"Tạo batch mới thành công!");
            txt_BatchName.Text = "";
            txt_ImagePath.Text = "";
            lb_SoLuongHinh.Text = "";
            txt_PathFolder.Text = "";
            txt_Path.Text = @"X:\N\13";
            btn_Browser.Enabled = true;
            txt_PathFolder.Enabled = true;
            txt_Path.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_multi)
            {
                lb_SobatchHoanThanh.Text = "";
                lb_SoImageDaHoanThanh.Text = "";
                //label1.Visible = true;
                //lb_SobatchHoanThanh.Visible = true;
                //lb_SoImageDaHoanThanh.Visible = true;
                UpLoadMulti_3Folder();
            }
            else
            {
                lb_SobatchHoanThanh.Text = "1";
                lb_SoImageDaHoanThanh.Text = "";
                //label1.Visible = false;
                //lb_SobatchHoanThanh.Visible = false;
                //lb_SoImageDaHoanThanh.Visible = false;
                UpLoadSingle();
            }
        }
        
        private bool closePending;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Quá trình tạo batch đang diễn ra, Bạn hãy chờ quá trình tạo batch kết thúc!");
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (closePending) Close();
            closePending = false;
        }
        private bool flag = false;
        
    }
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}