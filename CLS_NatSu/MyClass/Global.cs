using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using CLS_NatSu.Properties;
using CLS_NatSu.MyData;
using System.IO;

namespace CLS_NatSu.MyClass
{
    public class Global
    {
        public static string StrUserName = "";
        public static string StrPcName = "";
        public static string StrDomainName = "";
        public static string StrBatch = "";
        public static string StrBatchID = "";
        public static string StrRole = "";
        public static string Token = "";
        public static string StrIdProject = "NatSu_2018";
        public static string StrCheck = "";
        public static List<dataNote_> DataNote = new List<dataNote_>();
        public static bool FlagLoad = false;
        public static bool FlagLoadCheck = false;
        public static string Truong_18_1 = "";
        public static bool FlagCheckUpdate = true;
        public static bool FlagTong = true;

        public static string Version = "1.0.0";
        public static string Server = "DaNang";
        public static bool FlagChangeSave = true;
        public static string StrPath = @"\\192.168.165.10\NatSu$";
        public static string Webservice;
        public static string Webservice_Update;
        public static DataBPODataContext DbBpo;
        public static DataNatSuDataContext Db;
        public struct dataNote_
        {
            public string Truong { set; get; }
            public string Note { set; get; }
        }
        public struct DESO
        {
            public string txt_Truong01;
            public string txt_Truong02;
            public string txt_Truong03;
            public string txt_Truong04_1;
            public string txt_Truong04_2;
            public string txt_Truong05_1;
            public string txt_Truong05_2;
            public string txt_Truong06_1;
            public string txt_Truong06_2;
            public string txt_Truong07;
            public string txt_Truong08;
            public string txt_Truong09;
            public string txt_Truong10;
            public string txt_Truong11_1;
            public string txt_Truong11_2;
            public string txt_Truong12;
            public string txt_Truong13;
            public string txt_Truong14;
            public string txt_Truong15;
            public string txt_Truong16;
            public string txt_Truong17;
            public string txt_Truong18_1;
            public string txt_Truong18_2;
            public string txt_Truong18_3;

        }
        public static string GetToken(string strUserName)
        {
            Random rnd = new Random();
            return MyClass.HashMD5.GetMd5Hash(DateTime.Now + strUserName + rnd.Next(1111, 9999));
        }

        public static IPAddress GetServerIpAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            try
            {
                return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool CheckOutSource(string Role)
        {
            bool? OutSource = (from w in DbBpo.tbl_Versions where w.IDProject == StrIdProject select w.OutSource).FirstOrDefault();
            if (OutSource == false && Settings.Default.Server == "Khac" && (Role == "DESO"))
                return true;
            return false;
        }
        public static bool RunUpdateVersion()
        {
            var Version = (from w in DbBpo.tbl_Versions where w.IDProject == StrIdProject select  new { w.IDVersion, w.Is_Update}).FirstOrDefault();

            //MessageBox.Show(Version + "\r\n" + Version);
            if (Version.IDVersion + "" != Global.Version)
            {
                if (Version.Is_Update.Value)
                {
                    if (MessageBox.Show("Version bạn dùng đã cũ, vui lòng cập nhật phiên bản mới (Bắt buộc)!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start(Directory.GetCurrentDirectory() + @"\Update.exe");
                    }
                    return true;
                }
                else
                {
                    if (!FlagCheckUpdate)
                        return false;
                    if (MessageBox.Show("Version bạn dùng đã cũ, vui lòng cập nhật phiên bản mới!", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Process.Start(Directory.GetCurrentDirectory() + @"\Update.exe");
                        return true;
                    }
                    else
                    {
                        FlagCheckUpdate = false;
                        return false;
                    }
                }
            }
            else
                return false;
        }
    }
}
