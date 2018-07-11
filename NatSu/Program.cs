using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using CLS_NatSu.MyForm;

namespace NatSu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            //Application.Run(new Form1());
            if (new frm_ChangeServer().ShowDialog() != DialogResult.OK)
                return;
            bool temp = false;
            do
            {
                temp = false;
                if (CLS_NatSu.MyClass.Global.RunUpdateVersion())
                    temp = false;
                else
                {
                    frmLogin frLogin = new frmLogin();
                    if (frLogin.ShowDialog() == DialogResult.OK)
                    {
                        frm_Main frMain = new frm_Main();
                        if (frMain.ShowDialog() == DialogResult.Yes)
                        {
                            frMain.Close();
                            temp = true;
                        }
                    }
                }
            }
            while (temp);
        }
    }
}
