using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CLS_NatSu.MyClass;
using CLS_NatSu.Properties;
using CLS_NatSu.MyData;
using DevExpress.LookAndFeel;

namespace CLS_NatSu.MyForm
{
    public partial class frmLogin : XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lb_version.Text = Global.Version;
            UserLookAndFeel.Default.SkinName = CLS_NatSu.Properties.Settings.Default.ApplicationSkinName;
            txt_password.Properties.UseSystemPasswordChar = true;
            txt_machine.ReadOnly = true;
            txt_userwindow.ReadOnly = true;
            txt_ipaddress.ReadOnly = true;
            txt_role.ReadOnly = true;
            txt_machine.Text = Environment.MachineName;
            txt_userwindow.Text = Environment.UserDomainName + @"\" + Environment.UserName;
            try
            {
                txt_ipaddress.Text = Global.GetServerIpAddress().ToString();
            }
            catch (Exception)
            {

                txt_ipaddress.Text = "127.0.0.1";
            }
            lb_ngay.Text = DateTime.Now.ToShortDateString();
            lb_gio.Text = DateTime.Now.ToShortTimeString();
            if (Settings.Default.Check)
            {
                txt_username.Text = Settings.Default.username;
                txt_password.Text = Settings.Default.password;
                chb_luu.Checked = true;
            }
            try
            {
                GetInfo();
                txt_username_TextChanged(sender, e);
            }
            catch (Exception i) { MessageBox.Show(i.Message + ""); }
        }

        bool GetInfo()
        {
            try
            {
                if (Global.DbBpo.KiemTraLogin(txt_username.Text, txt_password.Text) == 1)
                {
                    string role = "";
                    role = (from w in (Global.DbBpo.GetRoLeCheckLogin(txt_username.Text, ref role)) select w.Column1).FirstOrDefault();
                    txt_role.Text = role;
                }
                else
                {
                    txt_role.Text = "User & Password không đúng!";
                    return false;
                }
            }
            catch (Exception EX)
            {
                //MessageBox.Show(EX.Message + "");
                txt_role.Text = "User không tồn tại trong hệ thống!";
                return false;
            }
            return true;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (Global.RunUpdateVersion())
                Application.Exit();
            if (GetInfo())
            {
                var ktUser = (from w in Global.DbBpo.CheckLevelUser_PhanCong(Global.StrIdProject, txt_username.Text) select new { w.UserName, w.LevelUser }).FirstOrDefault();
                if (ktUser == null)
                {
                    MessageBox.Show("Bạn chưa có quyền tham gia dự án. Vui lòng liên hệ với Admin");
                    return;
                }
                if (Global.CheckOutSource(txt_role.Text) == true)
                {
                    MessageBox.Show("Hiện tại dự án chưa có nhu cầu về nguồn nhân lực bên ngoài");
                    return;
                }
                if (string.IsNullOrEmpty(cbb_batchname.Text))
                {
                    if (MessageBox.Show("Không có batch nào được chọn. Bạn vẫn muốn đăng nhập?", "Thông báo!", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (chb_luu.Checked)
                {
                    Settings.Default.username = txt_username.Text;
                    Settings.Default.password = txt_password.Text;
                    Settings.Default.Check = true;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.username = "";
                    Settings.Default.password = "";
                    Settings.Default.Check = false;
                    Settings.Default.Save();
                }
                Global.Token = Global.GetToken(txt_username.Text);
                Global.StrBatchName = cbb_batchname.Text;
                Global.StrBatchID = cbb_batchname.Text == "" ? "" : cbb_batchname.SelectedValue.ToString();
                Global.StrUserName = txt_username.Text;
                Global.StrPcName = txt_machine.Text;
                Global.StrDomainName = txt_userwindow.Text;
                Global.StrRole = txt_role.Text;
                Global.Version = lb_version.Text;
                bool has = Global.DbBpo.tbl_TokenLogins.Any(w => w.UserName == txt_username.Text && w.IDProject == Global.StrIdProject);
                if (has)
                {
                    var token = (from w in Global.DbBpo.tbl_TokenLogins where w.UserName == txt_username.Text && w.IDProject == Global.StrIdProject select w.Token).FirstOrDefault();
                    if (token == "")
                    {
                        Global.DbBpo.updateToken(txt_username.Text, Global.StrIdProject, Global.Token);
                        Global.DbBpo.InsertLoginTime_new(txt_username.Text, DateTime.Now, txt_userwindow.Text, txt_machine.Text, txt_ipaddress.Text, Global.Token, Global.StrIdProject);
                    }
                    else
                    {
                        if (MessageBox.Show(@"User đang đăng nhập vào máy khác. Bạn có muốn tiếp tục đăng nhập?", @"Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            Global.DbBpo.updateToken(txt_username.Text, Global.StrIdProject, Global.Token);
                            Global.DbBpo.InsertLoginTime_new(txt_username.Text, DateTime.Now, txt_userwindow.Text, txt_machine.Text, txt_ipaddress.Text, Global.Token, Global.StrIdProject);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    var token = new tbl_TokenLogin();
                    token.UserName = txt_username.Text;
                    token.IDProject = Global.StrIdProject;
                    token.Token = "";
                    token.DateLogin = DateTime.Now;
                    Global.DbBpo.tbl_TokenLogins.InsertOnSubmit(token);
                    Global.DbBpo.SubmitChanges();
                    Global.DbBpo.updateToken(txt_username.Text, Global.StrIdProject, Global.Token);
                    Global.DbBpo.InsertLoginTime_new(txt_username.Text, DateTime.Now, txt_userwindow.Text, txt_machine.Text, txt_ipaddress.Text, Global.Token, Global.StrIdProject);
                }
                DialogResult = DialogResult.OK;
            }
        } 

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        public void txt_username_TextChanged(object sender, EventArgs e)
        {
            cbb_batchname.DataSource = null;
            GetInfo();
            cbb_City_SelectedIndexChanged(null,null);            
        }

        private void txt_password_EditValueChanged(object sender, EventArgs e)
        {
            cbb_batchname.DataSource = null;
            GetInfo();
            cbb_City_SelectedIndexChanged(null, null);           
        }
        
        private void cbb_City_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ktUser = (from w in Global.DbBpo.tbl_PhanCongs where w.UserName == txt_username.Text & w.IDProject==Global.StrIdProject select w.LevelUser).FirstOrDefault();
            if (txt_role.Text == "DESO")
            {
                if (ktUser == false)
                {
                    cbb_batchname.DataSource = (from w in (Global.Db.GetBatNotFinishDeGood(txt_username.Text)) select new { w.BatchID, w.BatchName }).ToList();
                    cbb_batchname.DisplayMember = "BatchName";
                    cbb_batchname.ValueMember = "BatchID";
                }
                else if (ktUser == true)
                {
                    cbb_batchname.DataSource = (from w in (Global.Db.GetBatNotFinishDeNotGood(txt_username.Text)) select new { w.BatchID, w.BatchName }).ToList();
                    cbb_batchname.DisplayMember = "BatchName";
                    cbb_batchname.ValueMember = "BatchID";
                }
            }
            else if (txt_role.Text == "CHECKERDESO")
            {
                cbb_batchname.DataSource = (from w in (Global.Db.GetBatNotFinishChecker(txt_username.Text,"")) select new { w.BatchID, w.BatchName }).ToList();
                cbb_batchname.DisplayMember = "BatchName";
                cbb_batchname.ValueMember = "BatchID";
            }
            else if (txt_role.Text == "ADMIN")
            {
                cbb_batchname.DataSource = (from w in (Global.Db.GetBatchCongKhai()) select new { w.BatchID, w.BatchName }).ToList();
                cbb_batchname.DisplayMember = "BatchName";
                cbb_batchname.ValueMember = "BatchID";
            }
        }

        private void cbb_City_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                ((System.Windows.Forms.ComboBox)sender).Text = "";
        }

        private void cbb_City_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 3 && (int)e.KeyChar != 1)
                e.Handled = true;
        }
    }
}