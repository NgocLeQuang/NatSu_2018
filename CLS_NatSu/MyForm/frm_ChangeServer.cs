using CLS_NatSu.Properties;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using CLS_NatSu.MyData;
using CLS_NatSu.MyClass;

namespace CLS_NatSu.MyForm
{
    public partial class frm_ChangeServer : DevExpress.XtraEditors.XtraForm
    {
        public frm_ChangeServer()
        {
            InitializeComponent();
        }

        private void frm_ChangeServer_Load(object sender, EventArgs e)
        {
            try
            {
                switch (Settings.Default.Server)
                {
                    case "DaNang":
                        rb_DaNang.Checked = true;
                        break;
                    case "Khac":
                        rb_Khac.Checked = true;
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn server!");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (rb_DaNang.Checked == false && rb_Khac.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn vị trí bạn làm việc.");
                return;
            }
            try
            {
                if (rb_DaNang.Checked)
                {
                    Settings.Default.Server = "DaNang";
                    Settings.Default.Save();
                    Global.Server = "DaNang";
                    Global.Webservice = "http://192.168.165.10:8888/NatSu/";
                    Global.Webservice_Update = "http://192.168.165.10:8888/Update/Natsu2018/";
                    Global.Db = new DataNatSuDataContext(@"Data Source=192.168.165.10\BPOSERVER;Initial Catalog=NatSu_2018;Persist Security Info=True;User ID=NatSu_2018;Password=123@123a");
                    Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                    Global.DbBpo = new DataBPODataContext(@"Data Source=192.168.165.10;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;User ID=NatSu_2018;Password=123@123a");
                }
                else if (rb_Khac.Checked)
                {
                    Settings.Default.Server = "Khac";
                    Settings.Default.Save();
                    Global.Server = "Khac";
                    Global.Webservice = "http://101.99.53.121:3602/NatSu/";
                    Global.Webservice_Update = "http://101.99.53.121:3602/Update/Natsu2018/";
                    
                    Global.Db = new DataNatSuDataContext(@"Data Source=http://101.99.53.121,3609;Initial Catalog=NatSu_2018;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                    Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                    Global.DbBpo = new DataBPODataContext(@"Data Source=http://101.99.53.121,3609;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                    try
                    {
                        Global.Db.Connection.Open();
                        //if(Global.Db.Connection.State!=System.Data.ConnectionState.Open)
                        //{
                        //    Global.Webservice = "http://118.69.176.36:3602/NatSu/";
                        //    Global.Webservice_Update = "http://118.69.176.36:3602/Update/Natsu2018/";
                        //    Global.Db = new DataNatSuDataContext(@"Data Source=118.69.176.36,3609;Initial Catalog=NatSu_2018;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //    Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                        //    Global.DbBpo = new DataBPODataContext(@"Data Source=118.69.176.36,3609;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //}
                        Global.Db.Connection.Close();
                    }
                    catch
                    {
                        //try
                        //{
                        //    Global.Webservice = "http://118.69.176.36:3602/NatSu/";
                        //    Global.Webservice_Update = "http://118.69.176.36:3602/Update/Natsu2018/";
                        //    Global.Db = new DataNatSuDataContext(@"Data Source=118.69.176.36,3609;Initial Catalog=NatSu_2018;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //    Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                        //    Global.DbBpo = new DataBPODataContext(@"Data Source=118.69.176.36,3609;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //    Global.Db.Connection.Open();
                        //    if (Global.Db.Connection.State != System.Data.ConnectionState.Open)
                        //    {
                        //        Global.Webservice = "http://117.2.142.10:3602/NatSu/";
                        //        Global.Webservice_Update = "http://117.2.142.10:3602/Update/Natsu2018/";
                        //        Global.Db = new DataNatSuDataContext(@"Data Source=117.2.142.10,3609;Initial Catalog=NatSu_2018;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //        Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                        //        Global.DbBpo = new DataBPODataContext(@"Data Source=117.2.142.10,3609;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //    }
                        //    Global.Db.Connection.Close();
                        //}
                        //catch
                        //{
                            Global.Webservice = "http://117.2.142.10:3602/NatSu/";
                            Global.Webservice_Update = "http://117.2.142.10:3602/Update/Natsu2018/";
                            Global.Db = new DataNatSuDataContext(@"Data Source=117.2.142.10,3609;Initial Catalog=NatSu_2018;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                            Global.Db.CommandTimeout = 15 * 60; // 5 Mins
                            Global.DbBpo = new DataBPODataContext(@"Data Source=117.2.142.10,3609;Initial Catalog=DatabaseDataEntryBPO;Persist Security Info=True;Network Library=DBMSSOCN;User ID=NatSu_2018;Password=123@123a");
                        //}
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show(i.Message + "");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void rb_DaNang_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_DaNang.Checked)
                rb_Khac.Checked = false;
            else
                rb_DaNang.Checked = false;

        }

        private void rb_Khac_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Khac.Checked)
                rb_DaNang.Checked = false;
            else
                rb_Khac.Checked = false;
        }
    }
}