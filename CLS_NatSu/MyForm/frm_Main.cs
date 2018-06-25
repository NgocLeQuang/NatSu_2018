using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using CLS_NatSu.Properties;
using CLS_NatSu.MyClass;
using System.Collections.Generic;
using CLS_NatSu.MyData;
using System.Drawing;

namespace CLS_NatSu.MyForm
{
    public partial class frm_Main : XtraForm
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        int ChiaUser = -1;
        int LevelUser = -1;
        private string Folder = "";
        bool FlagLoad = false;
        private void btn_Logout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btn_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void btn_QuanLyBatch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frm_ManagerBatch().ShowDialog();
        }
        private void frm_Main_Load(object sender, EventArgs e)
        {
            try
            {
                Text = "NatSu 2018 _ " + Global.Version;
                Global.FlagLoad = true;
                ChiaUser = -1;
                LevelUser = -1;
                lb_IdImage.Text = "";
                Global.FlagChangeSave = false;
                Global.FlagLoadCheck = false;
                splitMain.SplitterPosition = 620;
                Tab_Main.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                menu_QuanLy.Enabled = false;
                btn_Check_DeSo.Enabled = false;
                btn_Submit.Enabled = false;
                btn_Submit_Logout.Enabled = false;
                Folder = "";
                lb_fBatchName.Text = Global.StrBatch;
                lb_UserName.Text = Global.StrUserName;
                Global.FlagLoad = false;
                var checkDisableUser = (from w in Global.DbBpo.tbl_Users where w.Username == Global.StrUserName select w.IsDelete).FirstOrDefault();
                //Global.DataNote = (from w in Global.Db.tbl_Notes select new Global.dataNote_ {Truong = w.Truong, Note = w.Note }).ToList();
                if (checkDisableUser)
                {
                    MessageBox.Show("Tài khoản này đã vô hiệu hóa. Vui lòng liên hệ với Admin");
                    DialogResult = DialogResult.Yes;
                }
                if (Global.StrRole.ToUpper() == "DESO")
                {
                    var ktBatch = (from w in Global.Db.CheckBatchChiaUser(Global.StrBatchID) select w.ChiaUser).FirstOrDefault();
                    if (ktBatch == true)
                    {
                        ChiaUser = 1;
                    }
                    else if (ktBatch == false)
                    {
                        ChiaUser = 0;
                    }
                    else
                    {
                        ChiaUser = -1;
                    }
                    var ktUser = (from w in Global.DbBpo.CheckLevelUser_PhanCong(Global.StrIdProject, Global.StrUserName) select new { w.UserName, w.LevelUser }).FirstOrDefault();
                    if (ktUser == null)
                    {
                        MessageBox.Show("Bạn chưa có quyền tham gia dự án. Vui lòng liên hệ với Admin");
                        return;
                    }
                    if (ktUser.LevelUser == true)
                        LevelUser = 0;
                    else if (ktUser.LevelUser == false)
                        LevelUser = 1;
                    else
                        LevelUser = -1;
                    lb_TongPhieu.Text = (from w in Global.Db.tbl_Batches where w.BatchID == Global.StrBatchID select w.NumberImage).FirstOrDefault();
                    setValue();
                    if (Global.StrRole.ToUpper() == "DESO")
                    {
                        UC_225_1.UC_225_Load(null, null);
                        UC_225_1.uC_225_Item1.Focus += UC_Focus;
                        UC_225_1.uC_225_Item2.Focus += UC_Focus;
                        UC_225_1.uC_225_Item3.Focus += UC_Focus;
                        UC_225_1.uC_225_Item4.Focus += UC_Focus;
                        UC_225_1.uC_225_Item5.Focus += UC_Focus;
                        UC_2225_1.UC_2225_Load(null, null);
                        UC_2225_1.uC_2225_Item1.Focus += UC_Focus;
                        UC_2225_1.uC_2225_Item2.Focus += UC_Focus;
                        UC_2225_1.uC_2225_Item3.Focus += UC_Focus;
                        UC_2225_1.uC_2225_Item4.Focus += UC_Focus;
                        UC_2225_1.uC_2225_Item5.Focus += UC_Focus;
                        txt_Truong_01.LostFocus += Txt_Truong_01_LostFocus;
                    }
                    menu_QuanLy.Enabled = false;
                    btn_Check_DeSo.Enabled = false;
                    btn_Submit.Enabled = true;
                }
                else if (Global.StrRole.ToUpper() == "ADMIN")
                {
                    menu_QuanLy.Enabled = true;
                    btn_Check_DeSo.Enabled = true;
                    btn_Submit.Enabled = false;
                    btn_Submit_Logout.Enabled = false;
                    FlagLoad = true;
                    bool? OutSource = (from w in Global.DbBpo.tbl_Versions where w.IDProject == Global.StrIdProject select w.OutSource).FirstOrDefault();
                    if (OutSource == true)
                        ckOutSource.EditValue = true;
                    else
                        ckOutSource.EditValue = false;
                    FlagLoad = false;
                }
                else if (Global.StrRole.ToUpper() == "CHECKERDESO")
                {
                    btn_Check_DeSo.Enabled = true;
                    menu_QuanLy.Enabled = false;
                    btn_Check_DeSo.Enabled = true;
                    btn_Submit.Enabled = false;
                    btn_Submit_Logout.Enabled = false;
                }
                RefreshUC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối internet của bạn bị gián đoạn, Vui lòng kiểm tra lại!");
                DialogResult = DialogResult.Yes;
            }
        }
        bool flagLostFocus = true;
        private void Txt_Truong_01_LostFocus(object sender, EventArgs e)
        {
            if (!flagLostFocus)
                return;
            if (Tab_Main.SelectedTabPage.Name == "tp_225")
            {
                UC_225_1.uC_225_Item1.txt_Truong_02.Focus();
            }
            else if (Tab_Main.SelectedTabPage.Name == "tp_2225")
            {
                UC_2225_1.uC_2225_Item1.txt_Truong_02.Focus();
            }
        }

        private void UC_Focus(string Truong, string Tag)
        {
            txt_Note.Text = Tag;
        }

        private void setValue()
        {
            var a = (from w in Global.Db.GetSoLuongPhieu(Global.StrBatchID, Global.StrUserName, LevelUser + "", ChiaUser + "") select new { w.SoPhieuCon, w.SoPhieuNhap }).FirstOrDefault();
            lb_SoPhieuCon.Text = a.SoPhieuCon + "";
            lb_SoPhieuNhap.Text = a.SoPhieuNhap + "";
        }
        private void RefreshUC()
        {
            flagLostFocus = false;
            txt_Truong_01.Text = "225";
            txt_Truong_01.ForeColor = Color.Black;
            txt_Truong_01.BackColor = Color.White;
            UC_225_1.ResetData();
            UC_2225_1.ResetData();
            txt_Truong_01.Focus();
            flagLostFocus = true;
        }
        public struct ImageImformation
        {
            public string ImageName { set; get; }
        }
        private ImageImformation getFilename = new ImageImformation();

        public void SetFieldLocation_IsNull()
        {
            //Settings.Default.BatchID = Global.StrBatchID;
            //Settings.Default.ImageID = lb_IdImage.Text;
            //Settings.Default.UserInput = Global.StrUserName;
            //Settings.Default.Truong01 = "";
            //Settings.Default.Truong02 = "";
            //Settings.Default.Truong03 = "";
            //Settings.Default.Truong04_1 = "";
            //Settings.Default.Truong05_1 = "";
            //Settings.Default.Truong06_1 = "";
            //Settings.Default.Truong07 = "";
            //Settings.Default.Truong08 = "";
            //Settings.Default.Truong09 = "";
            //Settings.Default.Truong10 = "";
            //Settings.Default.Truong11_1 = "";
            //Settings.Default.Truong12 = "";
            //Settings.Default.Truong13 = "";
            //Settings.Default.Truong14 = "";
            //Settings.Default.Truong15 = "";
            //Settings.Default.Truong16 = "";
            //Settings.Default.Truong17 = "";
            //Settings.Default.Truong18_1 = Global.Truong_18_1;
            //Settings.Default.Truong18_2 = "";
            //Settings.Default.Truong18_3 = "";
            //Settings.Default.Save();
        }
        public void SetFieldLocation_IsValue()
        {
            //uC_Minami1.txt_Truong01.Text = Settings.Default.Truong01;
            //uC_Minami1.txt_Truong02.Text = Settings.Default.Truong02;
            //uC_Minami1.txt_Truong03.Text = Settings.Default.Truong03;
            //uC_Minami1.txt_Truong04_1.Text = Settings.Default.Truong04_1;
            //uC_Minami1.txt_Truong04_2.Text = Settings.Default.Truong04_2;
            //uC_Minami1.txt_Truong05_1.Text = Settings.Default.Truong05_1;
            //uC_Minami1.txt_Truong05_2.Text = Settings.Default.Truong05_2;
            //uC_Minami1.txt_Truong06_1.Text = Settings.Default.Truong06_1;
            //uC_Minami1.txt_Truong06_2.Text = Settings.Default.Truong06_2;
            //uC_Minami1.txt_Truong07.Text = Settings.Default.Truong07;
            //uC_Minami1.txt_Truong08.Text = Settings.Default.Truong08;
            //uC_Minami1.txt_Truong09.Text = Settings.Default.Truong09;
            //uC_Minami1.txt_Truong10.Text = Settings.Default.Truong10;
            //uC_Minami1.txt_Truong11_1.Text = Settings.Default.Truong11_1;
            //uC_Minami1.txt_Truong11_2.Text = Settings.Default.Truong11_2;
            //uC_Minami1.txt_Truong12.Text = Settings.Default.Truong12;
            //uC_Minami1.txt_Truong13.Text = Settings.Default.Truong13;
            //uC_Minami1.txt_Truong14.Text = Settings.Default.Truong14;
            //uC_Minami1.txt_Truong15.Text = Settings.Default.Truong15;
            //uC_Minami1.txt_Truong16.Text = Settings.Default.Truong16;
            //uC_Minami1.txt_Truong17.Text = Settings.Default.Truong17;
            //uC_Minami1.txt_Truong18_1.Text = Settings.Default.Truong18_1;
            //uC_Minami1.txt_Truong18_2.Text = Settings.Default.Truong18_2;
            //uC_Minami1.txt_Truong18_3.Text = Settings.Default.Truong18_3;
        }
        private string GetImageNew()
        {
            lb_IdImage.Text = "";
            getFilename.ImageName = "";
            Global.FlagChangeSave = true;
            if (Global.StrRole == "DESO")
            {
                if (FlagLoadMissImage)
                {
                    var temp = (from w in Global.Db.GetImage_MissImage_Deso(Global.StrBatchID, Global.StrUserName) select new { w.IDImage }).ToList();
                    if (temp.Count > 0)
                    {
                        getFilename.ImageName = temp[0]?.IDImage;
                    }
                }
                if (string.IsNullOrEmpty(getFilename.ImageName))
                {
                    if (ChiaUser == 1)  //Batch có chia User nhập
                    {
                        if (LevelUser == 1) //User Level Good
                        {
                            try
                            {
                                var temp1 = (from w in Global.Db.GetImage_Group_Good_DeSo(Global.StrBatchID, lb_UserName.Text) select new { w.IDImage }).ToList();
                                if (temp1.Count > 0)
                                {
                                    getFilename.ImageName = temp1[0]?.IDImage;
                                }
                                if (string.IsNullOrEmpty(getFilename.ImageName))
                                {
                                    return "NULL";
                                }
                            }
                            catch (Exception)
                            {
                                return "NULL";
                            }
                        }
                        else if (LevelUser == 0) //User Level Not Good
                        {
                            try
                            {
                                var temp1 = (from w in Global.Db.GetImage_Group_Notgood_DeSo(Global.StrBatchID, lb_UserName.Text) select new { w.IDImage }).ToList();
                                if (temp1.Count > 0)
                                {
                                    getFilename.ImageName = temp1[0]?.IDImage;
                                }
                                if (string.IsNullOrEmpty(getFilename.ImageName))
                                {
                                    return "NULL";
                                }
                            }
                            catch (Exception)
                            {
                                return "NULL";
                            }
                        }
                    }
                    else if (ChiaUser == 0)  //Batch không chia user
                    {
                        if (string.IsNullOrEmpty(getFilename.ImageName))
                        {
                            try
                            {
                                var temp1 = (from w in Global.Db.LayHinhMoi_DeSo(Global.StrBatchID, lb_UserName.Text) select new { w.IDImage }).ToList();
                                if (temp1.Count > 0)
                                {
                                    getFilename.ImageName = temp1[0]?.IDImage;
                                }
                                if (string.IsNullOrEmpty(getFilename.ImageName))
                                {
                                    return "NULL";
                                }
                            }
                            catch (Exception)
                            {
                                return "NULL";
                            }
                        }
                    }
                }
                lb_IdImage.Text = getFilename.ImageName;

                uc_PictureBox1.imageBox1.Image = null;
                UC_225_1.ResetData();
                if (uc_PictureBox1.LoadImage(Global.Webservice + Global.StrBatchID + "/" + getFilename.ImageName, getFilename.ImageName, Settings.Default.ZoomImage) == "Error")
                {
                    uc_PictureBox1.imageBox1.Image = Resources.svn_deleted;
                    return "Error";
                }
                ////LogFile.WriteLog(Global.StrUserName + ".txt", "Bắt đầu load data đã nhập DE.");
                //if (Settings.Default.BatchID == Global.StrBatchID & Settings.Default.ImageID == lb_IdImage.Text & Settings.Default.UserInput.ToUpper() == Global.StrUserName.ToUpper())
                //{
                //    SetFieldLocation_IsValue();
                //}
                //else
                //{
                //    SetFieldLocation_IsNull();
                //}
                ////LogFile.WriteLog(Global.StrUserName + ".txt", "Kết thúc load data đã nhập DE.");
                ////LogFile.WriteLog(Global.StrUserName + ".txt", "Bắt đầu load data compare màu.");

            }
            return "ok";
        }
        
        private string token = "", Image_temp="";
        bool FlagLoadMissImage = false;
        public bool CheckLoaiPhieu()
        {
            string[] listLoaiPhieu = { "225", "2225"};
            bool a = false;
            if (!listLoaiPhieu.Contains(txt_Truong_01.Text))
            {
                a = true;
            }
            else
            {
                a = false;
            }
            return a;
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            token = "";
            Image_temp = "";
            if (Global.RunUpdateVersion())
                Application.Exit();
            token = (from w in Global.DbBpo.tbl_TokenLogins where w.UserName == Global.StrUserName && w.IDProject == Global.StrIdProject select w.Token).FirstOrDefault();
            if (token != Global.Token)
            {
                MessageBox.Show(@"User logged on to another PC, please login again!");
                DialogResult = DialogResult.Yes;
            }
            if (btn_Submit.Text == "Start")
            {
                FlagLoadMissImage = true;
                if (string.IsNullOrEmpty(Global.StrBatchID))
                {
                    MessageBox.Show("Vui lòng đăng nhập lại và chọn Batch!");
                    return;
                }
                RefreshUC();
                Image_temp = GetImageNew();
                if (Image_temp == "NULL")
                {
                    MessageBox.Show(@"Hoàn thành batch '" + lb_fBatchName.Text + "'");
                    Global.StrBatch = "";
                    Global.StrBatchID = "";
                    Folder = "";
                    if (LevelUser == 0)
                    {
                        var listResult = Global.Db.GetBatNotFinishDeNotGood(Global.StrUserName).ToList();
                        if (listResult.Count > 0)
                        {
                            if (MessageBox.Show(@"Batch tiếp theo: " + listResult[0].BatchName + "\nBạn muốn làm tiếp ??", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (Global.CheckOutSource(Global.StrRole) == true)
                                {
                                    MessageBox.Show("Hiện tại dự án chưa có nhu cầu về nguồn nhân lực bên ngoài");
                                    btn_Logout_ItemClick(null, null);
                                }
                                Global.StrBatchID = listResult[0].BatchID;
                                Global.StrBatch = listResult[0].BatchName;
                                var ktBatch = (from w in Global.Db.CheckBatchChiaUser(listResult[0].BatchID) select w.ChiaUser).FirstOrDefault();
                                if (ktBatch == true)
                                {
                                    ChiaUser = 1;
                                }
                                else if (ktBatch == false)
                                {
                                    ChiaUser = 0;
                                }
                                else
                                {
                                    ChiaUser = -1;
                                }
                                lb_fBatchName.Text = Global.StrBatch;
                                lb_IdImage.Text = "";
                                lb_TongPhieu.Text = (from w in Global.Db.tbl_Images where w.BatchID == Global.StrBatchID select w.IDImage).Count().ToString();
                                setValue();
                                btn_Submit.Text = @"Start";
                                btn_Submit_Click(null, null);
                            }
                            else
                            {
                                btn_Logout_ItemClick(null, null);
                            }
                        }
                        else
                        {
                            btn_Logout_ItemClick(null, null);
                        }
                    }
                    else
                    {
                        var listResult = Global.Db.GetBatNotFinishDeGood(Global.StrUserName).ToList();
                        if (listResult.Count > 0)
                        {
                            if (MessageBox.Show(@"Batch tiếp theo: " + listResult[0].BatchName + "\nBạn muốn làm tiếp ??", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (Global.CheckOutSource(Global.StrRole) == true)
                                {
                                    MessageBox.Show("Hiện tại dự án chưa có nhu cầu về nguồn nhân lực bên ngoài");
                                    btn_Logout_ItemClick(null, null);
                                }
                                Global.StrBatch = listResult[0].BatchName;
                                Global.StrBatchID = listResult[0].BatchID;
                                var ktBatch = (from w in Global.Db.CheckBatchChiaUser(listResult[0].BatchID) select w.ChiaUser).FirstOrDefault();
                                if (ktBatch == true)
                                {
                                    ChiaUser = 1;
                                }
                                else if (ktBatch == false)
                                {
                                    ChiaUser = 0;
                                }
                                else
                                {
                                    ChiaUser = -1;
                                }
                                lb_fBatchName.Text = Global.StrBatch;
                                lb_TongPhieu.Text = (from w in Global.Db.tbl_Images where w.BatchID == Global.StrBatchID select w.IDImage).Count().ToString();
                                setValue();
                                btn_Submit.Text = @"Start";
                                btn_Submit_Click(null, null);
                            }
                            else
                            {
                                btn_Logout_ItemClick(null, null);
                            }
                        }
                        else
                        {
                            btn_Logout_ItemClick(null, null);
                        }
                    }
                }
                else if (Image_temp == "Error")
                {
                    MessageBox.Show("Không thể load hình!");
                    btn_Logout_ItemClick(null, null);
                }
                setValue();
                btn_Submit.Text = "Submit";
                btn_Submit_Logout.Enabled = true;
            }
            else
            {
                FlagLoadMissImage = false;

                if (string.IsNullOrEmpty(txt_Truong_01.Text))
                {
                    MessageBox.Show("Bạn đang để trống trường 1. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (txt_Truong_01.Text == "225")
                {
                    if (CheckLoaiPhieu())
                    {
                        if (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    if (UC_225_1.IsEmpty())
                    {
                        if (MessageBox.Show(@"Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", @"Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    UC_225_1.Save225(Global.StrBatchID, lb_IdImage.Text, txt_Truong_01.Text);
                }
                else
                {
                    if (CheckLoaiPhieu())
                    {
                        if (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    if (UC_2225_1.IsEmpty())
                    {
                        if (MessageBox.Show(@"Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", @"Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    UC_2225_1.Save2225(Global.StrBatchID, lb_IdImage.Text, txt_Truong_01.Text);
                }
                RefreshUC();
                setValue();
                Image_temp = GetImageNew();
                if (Image_temp == "NULL")
                {
                    MessageBox.Show(@"Hoàn thành batch '" + lb_fBatchName.Text + "'");
                    Global.StrBatch = "";
                    Global.StrBatchID = "";
                    Folder = "";
                    if (LevelUser == 0)
                    {
                        var listResult = Global.Db.GetBatNotFinishDeNotGood(Global.StrUserName).ToList();
                        if (listResult.Count > 0)
                        {
                            if (MessageBox.Show(@"Batch tiếp theo: " + listResult[0].BatchName + "\nBạn muốn làm tiếp ??", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (Global.CheckOutSource(Global.StrRole) == true)
                                {
                                    MessageBox.Show("Hiện tại dự án chưa có nhu cầu về nguồn nhân lực bên ngoài");
                                    btn_Logout_ItemClick(null, null);
                                }
                                Global.StrBatch = listResult[0].BatchName;
                                Global.StrBatchID = listResult[0].BatchID;
                                var ktBatch = (from w in Global.Db.CheckBatchChiaUser(listResult[0].BatchID) select w.ChiaUser).FirstOrDefault();
                                if (ktBatch == true)
                                {
                                    ChiaUser = 1;
                                }
                                else if (ktBatch == false)
                                {
                                    ChiaUser = 0;
                                }
                                else
                                {
                                    ChiaUser = -1;
                                }
                                lb_fBatchName.Text = Global.StrBatch;
                                lb_IdImage.Text = "";
                                lb_TongPhieu.Text = (from w in Global.Db.tbl_Images where w.BatchID == Global.StrBatchID select w.IDImage).Count().ToString();
                                setValue();
                                btn_Submit.Text = @"Start";
                                btn_Submit_Click(null, null);
                            }
                            else
                            {
                                btn_Logout_ItemClick(null, null);
                            }
                        }
                        else
                        {
                            btn_Logout_ItemClick(null, null);
                        }
                    }
                    else
                    {
                        var listResult = Global.Db.GetBatNotFinishDeGood(Global.StrUserName).ToList();
                        if (listResult.Count > 0)
                        {
                            if (MessageBox.Show(@"Batch tiếp theo: " + listResult[0].BatchName + "\nBạn muốn làm tiếp ??", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                if (Global.CheckOutSource(Global.StrRole) == true)
                                {
                                    MessageBox.Show("Hiện tại dự án chưa có nhu cầu về nguồn nhân lực bên ngoài");
                                    btn_Logout_ItemClick(null, null);
                                }
                                Global.StrBatch = listResult[0].BatchName;
                                Global.StrBatchID = listResult[0].BatchID;
                                var ktBatch = (from w in Global.Db.CheckBatchChiaUser(listResult[0].BatchID) select w.ChiaUser).FirstOrDefault();
                                if (ktBatch == true)
                                {
                                    ChiaUser = 1;
                                }
                                else if (ktBatch == false)
                                {
                                    ChiaUser = 0;
                                }
                                else
                                {
                                    ChiaUser = -1;
                                }
                                lb_fBatchName.Text = Global.StrBatch;
                                lb_TongPhieu.Text = (from w in Global.Db.tbl_Images where w.BatchID == Global.StrBatchID select w.IDImage).Count().ToString();
                                setValue();
                                btn_Submit.Text = @"Start";
                                btn_Submit_Click(null, null);
                            }
                            else
                            {
                                btn_Logout_ItemClick(null, null);
                            }
                        }
                        else
                        {
                            btn_Logout_ItemClick(null, null);
                        }
                    }
                }
                else if (Image_temp == "Error")
                {
                    MessageBox.Show("Không thể load hình!");
                    btn_Logout_ItemClick(null, null);
                }
            }
        }

        private void btn_Submit_Logout_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lb_IdImage.Text))
                return;
            if (Global.RunUpdateVersion())
                Application.Exit();
            token = (from w in Global.DbBpo.tbl_TokenLogins where w.UserName == Global.StrUserName && w.IDProject == Global.StrIdProject select w.Token).FirstOrDefault();
            if (token != Global.Token)
            {
                MessageBox.Show(@"User logged on to another PC, please login again!");
                DialogResult = DialogResult.Yes;
            }
            if (Global.StrRole == "DESO")
            {
                if (string.IsNullOrEmpty(txt_Truong_01.Text))
                {
                    MessageBox.Show("Bạn đang để trống trường 1. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (txt_Truong_01.Text == "225")
                {
                    if (CheckLoaiPhieu())
                    {
                        if (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    if (UC_225_1.IsEmpty())
                    {
                        if (MessageBox.Show(@"Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", @"Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    UC_225_1.Save225(Global.StrBatchID, lb_IdImage.Text, txt_Truong_01.Text);
                }
                else
                {
                    if (CheckLoaiPhieu())
                    {
                        if (MessageBox.Show("Bạn đang chọn loại phiếu khác. Bạn muốn gửi phiếu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    if (UC_2225_1.IsEmpty())
                    {
                        if (MessageBox.Show(@"Bạn đang để trống phiếu. Bạn muốn gửi phiếu không ?", @"Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                    UC_2225_1.Save2225(Global.StrBatchID, lb_IdImage.Text, txt_Truong_01.Text);
                }
            }
            RefreshUC();
            DialogResult = DialogResult.Yes;
        }
        private void frm_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter && btn_Submit.Enabled == true)
            {
                btn_Submit_Click(null, null);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.PageUp)
            {
                uc_PictureBox1.btn_Xoaytrai_Click(null, null);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.PageDown)
            {
                uc_PictureBox1.btn_xoayphai_Click(null, null);
            }
        }
        
        private void btn_ExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           new frm_ExportExcel().ShowDialog();
        }

        private void btn_TienDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //new FrmTienDo().ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //new FrmFeedback().ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           //new frm_NangSuat().ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frm_User().ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           new frm_ChangePassword().ShowDialog();
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Global.DbBpo.UpdateTimeLastRequest(Global.Token);
                Global.DbBpo.UpdateTimeLogout(Global.Token);
                Global.DbBpo.ResetToken(Global.StrUserName, Global.StrIdProject, Global.Token);
            }
            catch { /**/}
            Settings.Default.ApplicationSkinName = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();
        }

        private void splitMain_SplitterPositionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(splitMain.SplitterPosition + "");
            Settings.Default.PositionSplitMain = splitMain.SplitterPosition;
            Settings.Default.Save();
        }

        private void lb_IdImage_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_IdImage.Text);
            XtraMessageBox.Show("Copy image name Success!");
        }

        private void lb_fBatchName_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_fBatchName.Text);
            XtraMessageBox.Show("Copy batch name Success!");
        }
        
        private void btn_RefreshImageNotInput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //new Refresh_ImageNotInput().ShowDialog();
        }

        private void btn_Check_DeSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Global.FlagChangeSave = false;
            Global.StrCheck = "CHECKDESO";
            frm_Checker fCheck = new frm_Checker();
            fCheck.TypeCheck = "CHECK DESO";
            fCheck.ShowDialog();
        }
        
        private void txt_Truong_01_TextChanged(object sender, EventArgs e)
        {
            if(txt_Truong_01.Text=="225")
            {
                Tab_Main.SelectedTabPage = tp_225;
            }
            else
            {
                Tab_Main.SelectedTabPage = tp_2225;
            }
        }

        private void Tab_Main_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (Tab_Main.SelectedTabPage.Name == "tp_225")
            {
                splitMain.SplitterPosition = 625;
            }
            else if (Tab_Main.SelectedTabPage.Name == "tp_2225")
            {
                splitMain.SplitterPosition = 670;
            }
        }

        private void ckOutSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FlagLoad == true)
                    return;
                int a = Global.DbBpo.UpdateOutSourceProject(Global.StrIdProject, Convert.ToBoolean(ckOutSource.EditValue+""));
                if (a == 0)
                {
                    MessageBox.Show("Thay đổi thành công");
                }
                else if (a == -1)
                {
                    MessageBox.Show("Thay đổi không thành công");
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi: " + i.Message); ;
            }
        }
    }
}