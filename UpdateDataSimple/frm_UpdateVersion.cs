using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UpdateDataSimple
{
    public partial class frm_UpdateVersion : Form
    {
        public frm_UpdateVersion()
        {
            InitializeComponent();
        }
        
        bool Is_Run = false;
        bool flagLoad = false;
        private void frm_UpdateVersion_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Process[] pname = Process.GetProcessesByName("NatSu_BPO");
            //MessageBox.Show(pname.Length+"");
            if (pname.Length == 0)
            {
                string Link = "", FileZip = "";
                try
                {
                    //MessageBox.Show("0");
                    Is_Run = true;
                    DataDataContext db;
                    progressBar1.Step = 1;
                    progressBar1.Value = 5;
                    ModifyProgressBarColor.SetState(progressBar1, 1);
                    try
                    {
                        db = new DataDataContext("Data Source=192.168.165.10;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;User ID=NatSu_2018;Password=123@123a");
                        var temp = (from w in db.tbl_Versions where w.IDProject == "NatSu_2018" select w).ToList();
                        Link = temp[0].LinkUpdate;
                        FileZip = temp[0].FileName_Update;
                    }
                    catch
                    {
                        Link = "http://117.2.142.10:3602/Update/Natsu2018/";
                        FileZip = "UpdateNatSu.zip";
                    }
                    progressBar1.PerformStep();
                    WebClient webClient = new WebClient();
                    progressBar1.PerformStep();
                    webClient.DownloadFile(new Uri(Link + FileZip), Directory.GetCurrentDirectory() + @"\"+ FileZip);
                    progressBar1.PerformStep();
                    string FileName=Path.GetFileNameWithoutExtension(Directory.GetCurrentDirectory() + @"\" + FileZip);
                    if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + FileName))
                        Directory.Delete(Directory.GetCurrentDirectory() + @"\" + FileName, true);
                    progressBar1.PerformStep();
                    ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + @"\" + FileZip, Directory.GetCurrentDirectory());
                    progressBar1.PerformStep();
                    DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\" + FileName);
                    FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
                    progressBar1.Maximum = 5+files.Count();
                    foreach (FileInfo i in files)
                    {
                        File.Copy(i.FullName, Directory.GetCurrentDirectory() + @"\" + i.Name, true);
                        progressBar1.PerformStep();
                    }
                    if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + FileName))
                        Directory.Delete(Directory.GetCurrentDirectory() + @"\" + FileName, true);
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\" + FileZip))
                        File.Delete(Directory.GetCurrentDirectory() + @"\" + FileZip);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    string FileName = Path.GetFileNameWithoutExtension(Directory.GetCurrentDirectory() + @"\" + FileZip);
                    if (Directory.Exists(Directory.GetCurrentDirectory() + @"\" + FileName))
                        Directory.Delete(Directory.GetCurrentDirectory() + @"\" + FileName, true);
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\" + FileZip))
                        File.Delete(Directory.GetCurrentDirectory() + @"\" + FileZip);
                }
            }
            else
            {
                if (flagLoad)
                {
                    if(MessageBox.Show("Chương trình NatSu_BPO đang chạy. Hãy tắt chương trình trước khi update.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        timer1.Enabled = true;
                    }
                    flagLoad = true;
                }
                else
                {
                    flagLoad = true;
                    timer1.Enabled = true;
                }
            }
            if (Is_Run)
            {
                Process.Start(Directory.GetCurrentDirectory() + @"\NatSu_BPO.exe");
                Application.Exit();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            frm_UpdateVersion_Load(null, null);
        }
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
