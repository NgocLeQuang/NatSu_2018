using System;
using System.IO;
using System.Text;
namespace CLS_NatSu.MyClass
{
    internal class LogFile
    {
        public static string getcharacter(int n, string str)
        {
            string kq = "";
            for (int i = 1; i <= n; i++)
            {
                kq = kq.Insert(kq.Length, str);
            }
            return kq;
        }
        public static void Log(string logMessage, TextWriter w)
        {
            string milisecond = DateTime.Now.Millisecond.ToString();
            w.WriteLine("{0}",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")  + " "+ milisecond.Insert(0, getcharacter(3 - milisecond.Length, " ")) + " : " +logMessage);
            //w.WriteLine("-------------------------------");
        }

        public static void WriteLog(String fileName_username, String LogText)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(fileName_username, FileMode.Append);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, 512))
                {
                    Log(LogText, writer);
                    //Global.DAL.ExecuteLogFile("INSERT INTO [dbo].[tbl_LogFile]([Username],[Info],[DateLog]) VALUES('" + fileName_username + "','" + LogText + "',Getdate())");
                    //writer.Write(LogText);
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }
    }
}
