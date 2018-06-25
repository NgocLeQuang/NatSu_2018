using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Drawing;
using System.Windows.Forms;
using CLS_NatSu.MyClass;
using System.Linq;

namespace CLS_NatSu.MyForm
{
    public partial class frm_User : DevExpress.XtraEditors.XtraForm
    {
        public frm_User()
        {
            InitializeComponent();
        }

        public bool Cal(int width, GridView view)
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
        private void frm_User_Load(object sender, EventArgs e)
        {
            //dgv_listuser.DataSource = Global.DbBpo.GetListUser();
            dgv_listuser.DataSource = Global.DbBpo.GetListUser_PhanCong(Global.StrIdProject);
            cbb_idrole.DataSource = Global.DbBpo.GetListRole();
            cbb_idrole.DisplayMember = "RoleName";
            cbb_idrole.ValueMember = "RoleID";
            //dgv_ListUserInProject.DataSource = Global.DbBpo.GetListUserInProject(Global.StrIdProject);
        }
        private void btn_suauser_Click(object sender, EventArgs e)
        {
            string roleid, nhanvien;
            nhanvien = txt_FullName.Text;
            roleid = cbb_idrole.SelectedValue != null ? cbb_idrole.SelectedValue.ToString() : "";

            if (!string.IsNullOrEmpty(roleid) && !string.IsNullOrEmpty(nhanvien) && !string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(txt_password.Text))
            {
                DialogResult thongbao = MessageBox.Show(@"Bạn chắc chắn muốn sửa UserName '" + txt_username.Text + "'", @"Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (thongbao == DialogResult.Yes)
                {
                    Global.DbBpo.UpdateUsername_PhanCong(txt_username.Text, txt_password.Text,chk_LevelUser_Inexperience.Checked==true?true:false, roleid, nhanvien, "", Global.StrUserName, Global.GetServerIpAddress().ToString(), Environment.MachineName, Environment.UserDomainName + @"\" + Environment.UserName,Global.StrIdProject);
                    frm_User_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show(@"Nhập đầy đủ thông tin trước khi lưu !");
            }
        }
        
        private void btn_themuser_Click(object sender, EventArgs e)
        {

            string roleid, nhanvien;
            int r;

            nhanvien = txt_FullName.Text;
            roleid = cbb_idrole.SelectedValue != null ? cbb_idrole.SelectedValue.ToString() : "";

            if (!string.IsNullOrEmpty(roleid)&&!string.IsNullOrEmpty(nhanvien)&& !string.IsNullOrEmpty(txt_username.Text)&&!string.IsNullOrEmpty(txt_password.Text))
            {
                r = Global.DbBpo.InsertUsername_PhanCong(txt_username.Text, txt_password.Text, chk_LevelUser_Inexperience.Checked == true ? true : false, roleid,nhanvien, "", Global.StrUserName, Global.GetServerIpAddress().ToString(), Environment.MachineName, Environment.UserDomainName + @"\" + Environment.UserName,Global.StrIdProject);
                if (r == 0)
                {
                    MessageBox.Show("UserName đã tồn tại, Vui lòng nhập UserName khác !");
                }
                if (r == 1)
                {
                    MessageBox.Show("Đã thêm UserName '" + txt_username.Text + "' !");
                    frm_User_Load(sender, e);
                    txt_username.Text = "";
                    txt_FullName.Text = "";
                    txt_username.Focus();
                }
            }
            else
            {
                MessageBox.Show("Nhập đầy đủ thông tin trước khi lưu !");
            }
        }

        private void btn_delete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string username = gridView1.GetFocusedRowCellValue("Username") != null ? gridView1.GetFocusedRowCellValue("Username").ToString() : "";
            DialogResult thongbao = MessageBox.Show("Bạn chắc chắn muốn xóa UserName '" +username + "'", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thongbao == DialogResult.Yes)
            {
                if(!string.IsNullOrEmpty(username))
                {
                    Global.DbBpo.DeleteUsername(username);
                    frm_User_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Username rỗng, không thể xóa!");
                }
            }
        }   
     

        private void cbb_idrole_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 3)
                e.Handled = true;
        }

        private void gridView1_RowCellDefaultAlignment(object sender, DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventArgs e)
        {
            try
            {
                txt_username.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Username") != null ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Username").ToString() : "";
                cbb_idrole.SelectedValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IDRole") != null ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IDRole").ToString() : "";
                txt_FullName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FullName") != null ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FullName").ToString() : "";
                txt_password.Text = "";
                chk_LevelUser_Inexperience.Checked= Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LevelUser") != null ? gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LevelUser").ToString() : "");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            LoadSttGridView(e, gridView1);
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                string username = gridView1.GetFocusedRowCellValue("Username").ToString();
                if (e.Column.FieldName == "LevelUser")
                {
                    bool check = (bool)e.Value;
                    if (check)
                    {
                        Global.DbBpo.updateNotGoodUser_PhanCong(username, true, Global.StrUserName, Global.GetServerIpAddress().ToString(), Environment.MachineName, Environment.UserDomainName + @"\" + Environment.UserName,Global.StrIdProject);
                    }
                    else
                    {
                        Global.DbBpo.updateNotGoodUser_PhanCong(username, false, Global.StrUserName, Global.GetServerIpAddress().ToString(), Environment.MachineName, Environment.UserDomainName + @"\" + Environment.UserName, Global.StrIdProject);
                    }
                }
                else if(e.Column.FieldName == "TrangThai")
                {
                    bool check = (bool)e.Value;
                    if (check)
                    {
                        Global.DbBpo.UpdateUserInProject(Global.StrIdProject,username, false, Convert.ToBoolean(gridView1.GetFocusedRowCellValue("LevelUser").ToString()));
                    }
                    else
                    {
                        Global.DbBpo.UpdateUserInProject(Global.StrIdProject, username, true, Convert.ToBoolean(gridView1.GetFocusedRowCellValue("LevelUser").ToString()));
                        gridView1.SetFocusedRowCellValue("LevelUser", false);
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Lỗi : " + i);
            }
        }
        
        private void btn_AndToProkject_Click(object sender, EventArgs e)
        {

        }

        private void btn_DeleteOutProkject_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //LoadSttGridView(e, gridView2);
        }

        private void btn_RefeshData_Click(object sender, EventArgs e)
        {
            frm_User_Load(sender, e);
        }

        private void btn_Delete_Multiple_Click(object sender, EventArgs e)
        {
            int i = 0;
            string s = "";
            foreach (var rowHandle in gridView1.GetSelectedRows())
            {
                i += 1;
                string Username = gridView1.GetRowCellValue(rowHandle, "Username").ToString();
                s += Username + "\n";
            }
            if (i <= 0)
            {
                MessageBox.Show("Bạn chưa chọn User. Vui lòng chọn user trước khi xóa");
                return;
            }

            DialogResult dlr = (MessageBox.Show("Bạn đang thực hiện xóa " + i + " user sau:\n" + s + "\nYes = xóa những user này \nNo = không thực hiện xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2));
            if (dlr == DialogResult.Yes)
            {
                foreach (var rowHandle in gridView1.GetSelectedRows())
                {
                    string Username = gridView1.GetRowCellValue(rowHandle, "Username").ToString();
                    Global.DbBpo.DeleteUsername(Username);
                }
                frm_User_Load(sender, e);
            }
        }
    }
}