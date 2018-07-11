using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CLS_NatSu.MyUserControl
{
    public partial class UC_ShowDataInput : UserControl
    {
        public UC_ShowDataInput()
        {
            InitializeComponent();
        }

        private void UC_ShowDataInput_Load(object sender, EventArgs e)
        {

        }

        private void txt_Truong_01_Check_TextChanged(object sender, EventArgs e)
        {
            if (txt_Truong_01_Check.Text == "225")
                Tab_UserCheck.SelectedTabPage.Name = "tp_225_1";
            else
                Tab_UserCheck.SelectedTabPage.Name = "tp_2225_1";
        }

        private void txt_Truong_01_User1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Truong_01_User1.Text == "225")
                Tab_User1.SelectedTabPage.Name = "tp_225_2";
            else
                Tab_User1.SelectedTabPage.Name = "tp_2225_2";
        }

        private void txt_Truong_01_User2_TextChanged(object sender, EventArgs e)
        {
            if (txt_Truong_01_User2.Text == "225")
                Tab_User2.SelectedTabPage.Name = "tp_225_3";
            else
                Tab_User2.SelectedTabPage.Name = "tp_2225_3";
        }
    }
}
