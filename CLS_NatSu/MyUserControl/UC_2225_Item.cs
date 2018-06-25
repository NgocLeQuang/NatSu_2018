using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CLS_NatSu.MyClass;
using System.Text.RegularExpressions;

namespace CLS_NatSu.MyUserControl
{
    public partial class UC_2225_Item : UserControl
    {
        public event AllTextChange Changed;
        public event Focus_Text Focus;
        public UC_2225_Item()
        {
            InitializeComponent();
        }
        private List<Category> category = new List<Category>();

        public class Category
        {
            public string DE { get; set; }
            public string JP { get; set; }
        }
        public void ResetData()
        {
            txt_Truong_02.Text = "";
            txt_Truong_03.Text = "";
            txt_Truong_04_1.Text = "";
            txt_Truong_04_2.Text = "";
            txt_Truong_04_3.Text = "";
            txt_Truong_05_1.Text = "";
            txt_Truong_05_2.Text = "";
            txt_Truong_06.Text = "";
            txt_Truong_07.Text = "";
            txt_Truong_08.Text = "";
            txt_Truong_09.Text = "";
            txt_Truong_10.Text = "";
            txt_Truong_11.Text = "";
            txt_Truong_12.Text = "";
            txt_Truong_13.Text = "";
            txt_Truong_14.Text = "";
            txt_Truong_15.Text = "";
            txt_Truong_16.Text = "";
            txt_Truong_17.Text = "";
            txt_Truong_18.Text = "";
            txt_Truong_19.ItemIndex = 0;
            txt_Truong_20.Text = "";
            txt_Truong_21.Text = "";
            txt_Truong_22.Text = "";
            txt_Truong_23.Text = "";
            txt_Truong_24.Text = "";
            txt_Truong_25.Text = "";
            txt_Truong_26.Text = "";

            txt_Truong_02.ForeColor = Color.Black;
            txt_Truong_03.ForeColor = Color.Black;
            txt_Truong_04_1.ForeColor = Color.Black;
            txt_Truong_04_2.ForeColor = Color.Black;
            txt_Truong_04_3.ForeColor = Color.Black;
            txt_Truong_05_1.ForeColor = Color.Black;
            txt_Truong_05_2.ForeColor = Color.Black;
            txt_Truong_06.ForeColor = Color.Black;
            txt_Truong_07.ForeColor = Color.Black;
            txt_Truong_08.ForeColor = Color.Black;
            txt_Truong_09.ForeColor = Color.Black;
            txt_Truong_10.ForeColor = Color.Black;
            txt_Truong_11.ForeColor = Color.Black;
            txt_Truong_12.ForeColor = Color.Black;
            txt_Truong_13.ForeColor = Color.Black;
            txt_Truong_14.ForeColor = Color.Black;
            txt_Truong_15.ForeColor = Color.Black;
            txt_Truong_16.ForeColor = Color.Black;
            txt_Truong_17.ForeColor = Color.Black;
            txt_Truong_18.ForeColor = Color.Black;
            txt_Truong_19.ForeColor = Color.Black;
            txt_Truong_20.ForeColor = Color.Black;
            txt_Truong_21.ForeColor = Color.Black;
            txt_Truong_22.ForeColor = Color.Black;
            txt_Truong_23.ForeColor = Color.Black;
            txt_Truong_24.ForeColor = Color.Black;
            txt_Truong_25.ForeColor = Color.Black;
            txt_Truong_26.ForeColor = Color.Black;

            txt_Truong_02.BackColor = Color.White;
            txt_Truong_03.BackColor = Color.White;
            txt_Truong_04_1.BackColor = Color.White;
            txt_Truong_04_2.BackColor = Color.White;
            txt_Truong_04_3.BackColor = Color.White;
            txt_Truong_05_1.BackColor = Color.White;
            txt_Truong_05_2.BackColor = Color.White;
            txt_Truong_06.BackColor = Color.White;
            txt_Truong_07.BackColor = Color.White;
            txt_Truong_08.BackColor = Color.White;
            txt_Truong_09.BackColor = Color.White;
            txt_Truong_10.BackColor = Color.White;
            txt_Truong_11.BackColor = Color.White;
            txt_Truong_12.BackColor = Color.White;
            txt_Truong_13.BackColor = Color.White;
            txt_Truong_14.BackColor = Color.White;
            txt_Truong_15.BackColor = Color.White;
            txt_Truong_16.BackColor = Color.White;
            txt_Truong_17.BackColor = Color.White;
            txt_Truong_18.BackColor = Color.White;
            txt_Truong_19.BackColor = Color.White;
            txt_Truong_20.BackColor = Color.White;
            txt_Truong_21.BackColor = Color.White;
            txt_Truong_22.BackColor = Color.White;
            txt_Truong_23.BackColor = Color.White;
            txt_Truong_24.BackColor = Color.White;
            txt_Truong_25.BackColor = Color.White;
            txt_Truong_26.BackColor = Color.White;

            txt_Truong_02.Tag = "0";
            txt_Truong_03.Tag = "0";
            txt_Truong_04_1.Tag = "0";
            txt_Truong_04_2.Tag = "0";
            txt_Truong_04_3.Tag = "0";
            txt_Truong_05_1.Tag = "0";
            txt_Truong_05_2.Tag = "0";
            txt_Truong_06.Tag = "0";
            txt_Truong_07.Tag = "0";
            txt_Truong_08.Tag = "0";
            txt_Truong_09.Tag = "0";
            txt_Truong_10.Tag = "0";
            txt_Truong_11.Tag = "0";
            txt_Truong_12.Tag = "0";
            txt_Truong_13.Tag = "0";
            txt_Truong_14.Tag = "0";
            txt_Truong_15.Tag = "0";
            txt_Truong_16.Tag = "0";
            txt_Truong_17.Tag = "0";
            txt_Truong_18.Tag = "0";
            txt_Truong_19.Tag = "0";
            txt_Truong_20.Tag = "0";
            txt_Truong_21.Tag = "0";
            txt_Truong_22.Tag = "0";
            txt_Truong_23.Tag = "0";
            txt_Truong_24.Tag = "0";
            txt_Truong_25.Tag = "0";
            txt_Truong_26.Tag = "0";
        }
        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(txt_Truong_02.Text)
                & string.IsNullOrEmpty(txt_Truong_03.Text)
                & string.IsNullOrEmpty(txt_Truong_04_1.Text)
                & string.IsNullOrEmpty(txt_Truong_04_2.Text)
                & string.IsNullOrEmpty(txt_Truong_04_3.Text)
                & string.IsNullOrEmpty(txt_Truong_05_1.Text)
                & string.IsNullOrEmpty(txt_Truong_05_2.Text)
                & string.IsNullOrEmpty(txt_Truong_06.Text)
                & string.IsNullOrEmpty(txt_Truong_07.Text)
                & string.IsNullOrEmpty(txt_Truong_08.Text)
                & string.IsNullOrEmpty(txt_Truong_09.Text)
                & string.IsNullOrEmpty(txt_Truong_10.Text)
                & string.IsNullOrEmpty(txt_Truong_11.Text)
                & string.IsNullOrEmpty(txt_Truong_12.Text)
                & string.IsNullOrEmpty(txt_Truong_13.Text)
                & string.IsNullOrEmpty(txt_Truong_14.Text)
                & string.IsNullOrEmpty(txt_Truong_15.Text)
                & string.IsNullOrEmpty(txt_Truong_16.Text)
                & string.IsNullOrEmpty(txt_Truong_17.Text)
                & string.IsNullOrEmpty(txt_Truong_18.Text)
                & string.IsNullOrEmpty(txt_Truong_19.Text)
                & string.IsNullOrEmpty(txt_Truong_20.Text)
                & string.IsNullOrEmpty(txt_Truong_21.Text)
                & string.IsNullOrEmpty(txt_Truong_22.Text)
                & string.IsNullOrEmpty(txt_Truong_23.Text)
                & string.IsNullOrEmpty(txt_Truong_24.Text)
                & string.IsNullOrEmpty(txt_Truong_25.Text)
                & string.IsNullOrEmpty(txt_Truong_26.Text))
                return true;
            return false;
        }

        private void DoiMau(int soByteBe, int soBytelon, TextEdit textBox)
        {
            if (textBox.Text.IndexOf('*') < 0 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (textBox.Text.Length >= soByteBe && textBox.Text.Length <= soBytelon)
                {
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.Black;
                }
                else
                {
                    textBox.BackColor = Color.Red;
                    textBox.ForeColor = Color.White;
                }
            }
            else
            {
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
            }
        }

        private void Txt_GotFocus(object sender, EventArgs e)
        {
            //try
            //{
            //    Focus(((TextEdit)sender).Name, ((TextEdit)sender).Tag + "");
            //    ((TextEdit)sender).SelectAll();
            //}
            //catch
            //{
            //    try
            //    {
            //        Focus(((LookUpEdit)sender).Name, ((LookUpEdit)sender).Tag + "");
            //        ((LookUpEdit)sender).SelectAll();
            //    }
            //    catch
            //    {

            //    }
            //}
        }
        private void SetDataLookUpEdit()
        {
            category.Clear();
            category.Add(new Category() { DE = "" , JP = ""});
            category.Add(new Category() { DE = "1" , JP = "Số 70 or 70歳以上 or 70以上"});
            category.Add(new Category() { DE = "2" , JP = "二以上"});
            category.Add(new Category() { DE = "3" , JP = "月額変更予定"});
            category.Add(new Category() { DE = "4" , JP = "途中入社"});
            category.Add(new Category() { DE = "5" , JP = "病休 or 育休 or 休職"});
            category.Add(new Category() { DE = "6" , JP = "短 or 短時間"});
            category.Add(new Category() { DE = "7" , JP = "パ or パート"});
            category.Add(new Category() { DE = "8" , JP = "年間平均"});
            category.Add(new Category() { DE = "9" , JP = "Thấy số 9 hoặc hai giá trị chữ hoặc không phán đoán được(chữ khác)"});
            category.Add(new Category() { DE = "*" , JP = "*"});
        }
        public void UC_2225_Load(object sender, EventArgs e)
        {
            SetDataLookUpEdit();
            txt_Truong_19.Properties.DataSource = category;
            txt_Truong_19.Properties.DisplayMember = "DE";
            txt_Truong_19.Properties.ValueMember = "DE";
            if (Global.FlagLoad)
                return;
            //txt_Truong_02.Tag = (from w in Global.DataNote where w.Truong == "1" select w.Note).FirstOrDefault();
            txt_Truong_02.GotFocus += Txt_GotFocus;
            txt_Truong_03.GotFocus += Txt_GotFocus;
            txt_Truong_04_1.GotFocus += Txt_GotFocus;
            txt_Truong_04_2.GotFocus += Txt_GotFocus;
            txt_Truong_04_3.GotFocus += Txt_GotFocus;
            txt_Truong_05_1.GotFocus += Txt_GotFocus;
            txt_Truong_05_2.GotFocus += Txt_GotFocus;
            txt_Truong_06.GotFocus += Txt_GotFocus;
            txt_Truong_07.GotFocus += Txt_GotFocus;
            txt_Truong_08.GotFocus += Txt_GotFocus;
            txt_Truong_09.GotFocus += Txt_GotFocus;
            txt_Truong_10.GotFocus += Txt_GotFocus;
            txt_Truong_11.GotFocus += Txt_GotFocus;
            txt_Truong_12.GotFocus += Txt_GotFocus;
            txt_Truong_13.GotFocus += Txt_GotFocus;
            txt_Truong_14.GotFocus += Txt_GotFocus;
            txt_Truong_15.GotFocus += Txt_GotFocus;
            txt_Truong_16.GotFocus += Txt_GotFocus;
            txt_Truong_17.GotFocus += Txt_GotFocus;
            txt_Truong_18.GotFocus += Txt_GotFocus;
            txt_Truong_19.GotFocus += Txt_GotFocus;
            txt_Truong_20.GotFocus += Txt_GotFocus;
            txt_Truong_21.GotFocus += Txt_GotFocus;
            txt_Truong_22.GotFocus += Txt_GotFocus;
            txt_Truong_23.GotFocus += Txt_GotFocus;
            txt_Truong_24.GotFocus += Txt_GotFocus;
            txt_Truong_25.GotFocus += Txt_GotFocus;
            txt_Truong_26.GotFocus += Txt_GotFocus;
        }

        private void txt_Truong_02_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 6, (TextEdit)sender);
        }

        private void txt_Truong_03_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            if (((TextEdit)sender).Text.IndexOf('*') < 0 && !string.IsNullOrEmpty(((TextEdit)sender).Text))
            {
                if (((TextEdit)sender).Text.Length >= 0 && ((TextEdit)sender).Text.Length <= 1 && ((TextEdit)sender).Text!="1" && ((TextEdit)sender).Text != "3")
                {
                    ((TextEdit)sender).BackColor = Color.White;
                    ((TextEdit)sender).ForeColor = Color.Black;
                }
                else
                {
                    ((TextEdit)sender).BackColor = Color.Red;
                    ((TextEdit)sender).ForeColor = Color.White;
                }
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
            txt_Truong_04_1_1_EditValueChanged(txt_Truong_04_1, null);
            txt_Truong_04_2_1_EditValueChanged(txt_Truong_04_2, null);
            txt_Truong_04_3_1_EditValueChanged(txt_Truong_04_3, null);
        }

        private void txt_Truong_04_1_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (!string.IsNullOrEmpty(txt_Truong_04_1.Text) & txt_Truong_04_1.Text.IndexOf("*") < 0)
            {
                double a = 0;
                try
                {
                    a = Convert.ToDouble(txt_Truong_04_1.Text);
                    if ((txt_Truong_03.Text == "5" & a >= 1 & a <= 64)
                        | (txt_Truong_03.Text == "7" & a >= 1 & a <= 30)
                        | (txt_Truong_03.Text == "1" & a >= 1 & a <= 45)
                        | (txt_Truong_03.Text == "3" & a >= 1 & a <= 15))
                    {
                        ((TextEdit)sender).BackColor = Color.White;
                        ((TextEdit)sender).ForeColor = Color.Black;
                    }
                    else if ((txt_Truong_03.Text.IndexOf("*") >= 0 | string.IsNullOrEmpty(txt_Truong_03.Text)) & a >= 1 & a <= 64)
                    {
                        ((TextEdit)sender).BackColor = Color.White;
                        ((TextEdit)sender).ForeColor = Color.Black;
                    }
                    else
                    {
                        ((TextEdit)sender).BackColor = Color.Red;
                        ((TextEdit)sender).ForeColor = Color.White;
                    }
                }
                catch
                {
                    DoiMau(0, 2, (TextEdit)sender);
                }
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
        }

        private void txt_Truong_04_2_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (!string.IsNullOrEmpty(((TextEdit)sender).Text) & ((TextEdit)sender).Text.IndexOf("*") < 0)
            {
                double a = 0;
                try
                {
                    a = Convert.ToDouble(((TextEdit)sender).Text);
                    if (a >= 1 & a <= 12)
                    {
                        ((TextEdit)sender).BackColor = Color.White;
                        ((TextEdit)sender).ForeColor = Color.Black;
                    }
                    else
                    {
                        ((TextEdit)sender).BackColor = Color.Red;
                        ((TextEdit)sender).ForeColor = Color.White;
                    }
                }
                catch
                {
                    DoiMau(0, 2, (TextEdit)sender);
                }
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
        }

        private void txt_Truong_04_3_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (!string.IsNullOrEmpty(((TextEdit)sender).Text) & ((TextEdit)sender).Text.IndexOf("*") < 0)
            {
                double a = 0;
                try
                {
                    a = Convert.ToDouble(((TextEdit)sender).Text);
                    if (a >= 1 & a <= 31)
                    {
                        ((TextEdit)sender).BackColor = Color.White;
                        ((TextEdit)sender).ForeColor = Color.Black;
                    }
                    else
                    {
                        ((TextEdit)sender).BackColor = Color.Red;
                        ((TextEdit)sender).ForeColor = Color.White;
                    }
                }
                catch
                {
                    DoiMau(0, 2, (TextEdit)sender);
                }
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
        }

        private void txt_Truong_05_1_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if(((TextEdit)sender).Text!="30" & !string.IsNullOrEmpty(((TextEdit)sender).Text))
            {
                ((TextEdit)sender).BackColor = Color.Red;
                ((TextEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_05_2_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (((TextEdit)sender).Text != "08" & ((TextEdit)sender).Text != "09" & !string.IsNullOrEmpty(((TextEdit)sender).Text))
            {
                ((TextEdit)sender).BackColor = Color.Red;
                ((TextEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_08_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            if (((TextEdit)sender).Text.IndexOf('*') < 0 && !string.IsNullOrEmpty(((TextEdit)sender).Text))
            {
                if (((TextEdit)sender).Text.Length >= 0 && ((TextEdit)sender).Text.Length <= 4)
                {
                    try
                    {
                        double a = Convert.ToDouble(((TextEdit)sender).Text);
                        if (a >= 0 & a <= 31)
                        {
                            ((TextEdit)sender).BackColor = Color.White;
                            ((TextEdit)sender).ForeColor = Color.Black;
                        }
                        else
                        {
                            ((TextEdit)sender).BackColor = Color.Red;
                            ((TextEdit)sender).ForeColor = Color.White;
                        }
                    }
                    catch
                    {
                        ((TextEdit)sender).BackColor = Color.Red;
                        ((TextEdit)sender).ForeColor = Color.White;
                    }
                }
                else
                {
                    ((TextEdit)sender).BackColor = Color.Red;
                    ((TextEdit)sender).ForeColor = Color.White;
                }
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
        }
        private void DoiMauTruongTien(int soByteBe, int soBytelon, TextEdit textBox)
        {
            if (textBox.Text.IndexOf('?') < 0 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (textBox.Text.Substring(0, 1) == "-")
                {
                    if (textBox.Text.Length >= soByteBe && textBox.Text.Length <= soBytelon + 1)
                    {
                        textBox.ForeColor = Color.Black;
                        textBox.BackColor = Color.White;
                    }
                    else
                    {
                        textBox.ForeColor = Color.White;
                        textBox.BackColor = Color.Red;
                    }
                }
                else
                {
                    if (textBox.Text.Length >= soByteBe && textBox.Text.Length <= soBytelon)
                    {
                        textBox.BackColor = Color.White;
                        textBox.ForeColor = Color.Black;
                    }
                    else
                    {
                        textBox.BackColor = Color.Red;
                        textBox.ForeColor = Color.White;
                    }
                }
            }
            else
            {
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
            }
        }
        string FormatCurency(string curency)// định dạng 1,234
        {
            string str = curency.ToString();
            string pattern = @"(?<a>\d*)(?<b>\d{3})*";
            Match m = Regex.Match(str, pattern, RegexOptions.RightToLeft);
            StringBuilder sb = new StringBuilder();
            foreach (Capture i in m.Groups["b"].Captures)
            {
                sb.Insert(0, "," + i.Value);
            }
            sb.Insert(0, m.Groups["a"].Value);
            return sb.ToString().Trim(',');
        }
        private void curency(TextEdit text)
        {
            if (text.Text.IndexOf('*') >= 0)
            {
                text.Text = text.Text.Replace(",", "");
            }
            else if (!string.IsNullOrEmpty(text.Text))
            {
                if (text.Text[0] + "" == "-")
                {
                    string str = text.Text.Replace("-", "").Replace(",", "");
                    int start = text.Text.Length - text.SelectionStart;
                    text.Text = "-" + FormatCurency(str);
                    text.SelectionStart = -start + text.Text.Length;
                }
                else
                {
                    string str = text.Text.Replace(",", "");
                    int start = text.Text.Length - text.SelectionStart;
                    text.Text = FormatCurency(str);
                    text.SelectionStart = -start + text.Text.Length;
                }
            }
        }
        double Tong_truong11 = 0,
            Tong_truong12 = 0,
            Tong_truong17 = 0,
            Tong_truong18 = 0,
            Tong_truong23 = 0;
        private double Total_Truong11(TextEdit txt1, TextEdit txt2)
        {
            double x1 = 0, x2 = 0,T=0;
            if (Global.FlagTong)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txt1.Text))
                        x1 = double.Parse(txt1.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt2.Text))
                        x2 = double.Parse(txt2.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    T= x1 + x2;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return T;
        }
        
        private double Total_Truong12(TextEdit txt1, TextEdit txt2, TextEdit txt3, TextEdit txt4, TextEdit txt5, TextEdit txt6)
        {
            double x1 = 0, x2 = 0, x3 = 0, x4 = 0, x5 = 0, x6 = 0,T=0;
            if (Global.FlagTong)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txt1.Text))
                        x1 = double.Parse(txt1.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt2.Text))
                        x2 = double.Parse(txt2.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt3.Text))
                        x3 = double.Parse(txt3.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt4.Text))
                        x4 = double.Parse(txt4.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt5.Text))
                        x5 = double.Parse(txt5.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    if (!string.IsNullOrEmpty(txt6.Text))
                        x6 = double.Parse(txt6.Text.Replace(",", ""));
                }
                catch (Exception)
                {
                    // ignored
                }
                try
                {
                    T = x1 + x2 + x3 + x4 + x5 + x6;
                    Tong_truong18 = T / 3;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return T;
        }

        private void txt_Truong_06_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            try
            {
                if (!string.IsNullOrEmpty(((TextEdit)sender).Text) & ((TextEdit)sender).Text.IndexOf("*") <0)
                {
                    if (int.Parse(((TextEdit)sender).Text) <= 0 | int.Parse(((TextEdit)sender).Text) > 12)
                    {
                        ((TextEdit)sender).BackColor = Color.Red;
                        ((TextEdit)sender).ForeColor = Color.White;
                    }
                    else
                    {
                        ((TextEdit)sender).BackColor = Color.White;
                        ((TextEdit)sender).ForeColor = Color.Black;
                    }
                }
                else
                {
                    ((TextEdit)sender).BackColor = Color.White;
                    ((TextEdit)sender).ForeColor = Color.Black;
                }
            }
            catch
            {

            }
        }

        private void txt_Truong_13_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            if (!string.IsNullOrEmpty(((TextEdit)sender).Text) & ((TextEdit)sender).Text.IndexOf("*") < 0 & ((TextEdit)sender).Text.Length != 10 & ((TextEdit)sender).Text.Length != 12)
            {
                ((TextEdit)sender).BackColor = Color.Red;
                ((TextEdit)sender).ForeColor = Color.White;
            }
            else
            {
                ((TextEdit)sender).BackColor = Color.White;
                ((TextEdit)sender).ForeColor = Color.Black;
            }
        }

        private void txt_Truong_19_TextChanged(object sender, EventArgs e)
        {
            lb_Truong_19.Text = (from w in category where w.DE == txt_Truong_19.Text select w.JP).FirstOrDefault();
        }

        private void txt_Truong_08_Paint(object sender, PaintEventArgs e)
        {
            RectangleF rec = e.Graphics.ClipBounds;
            rec.Inflate(-1, -1);
            e.Graphics.DrawRectangle(Pens.Red, rec.Left, rec.Top, rec.Width, rec.Height);
        }

        private void txt_Truong_09_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMauTruongTien(0, 10, ((TextEdit)sender));
            if (((TextEdit)sender).Name == "txt_Truong_09" | ((TextEdit)sender).Name == "txt_Truong_10" | ((TextEdit)sender).Name == "txt_Truong_11")
            {
                Tong_truong11= Total_Truong11(txt_Truong_09, txt_Truong_10);

                double truong11 = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_Truong_11.Text))
                        truong11 = double.Parse(txt_Truong_11.Text.Replace(",", ""));
                }
                catch (Exception ex)
                {

                }
                if (Tong_truong11 != truong11 && !string.IsNullOrEmpty(txt_Truong_11.Text))
                {
                    txt_Truong_11.BackColor = Color.Red;
                    txt_Truong_11.ForeColor = Color.White;
                    txt_Truong_11.Tag = "1";
                }
                else
                {
                    txt_Truong_11.BackColor = Color.White;
                    txt_Truong_11.ForeColor = Color.Black;
                    txt_Truong_11.Tag = "0";
                }
            }
            if (((TextEdit)sender).Name == "txt_Truong_15" | ((TextEdit)sender).Name == "txt_Truong_16" | ((TextEdit)sender).Name == "txt_Truong_17")
            {
                Tong_truong17= Total_Truong11(txt_Truong_15, txt_Truong_16);
                double truong17 = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_Truong_17.Text))
                        truong17 = double.Parse(txt_Truong_17.Text.Replace(",", ""));
                }
                catch (Exception ex)
                {

                }
                if (Tong_truong17 != truong17 && !string.IsNullOrEmpty(txt_Truong_17.Text))
                {
                    txt_Truong_17.BackColor = Color.Red;
                    txt_Truong_17.ForeColor = Color.White;
                    txt_Truong_17.Tag = "1";
                }
                else
                {
                    txt_Truong_17.BackColor = Color.White;
                    txt_Truong_17.ForeColor = Color.Black;
                    txt_Truong_17.Tag = "0";
                }
            }
            if (((TextEdit)sender).Name == "txt_Truong_21" | ((TextEdit)sender).Name == "txt_Truong_22" | ((TextEdit)sender).Name == "txt_Truong_23")
            {
                Tong_truong23= Total_Truong11(txt_Truong_21, txt_Truong_22);

                double truong23 = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_Truong_23.Text))
                        truong23 = double.Parse(txt_Truong_23.Text.Replace(",", ""));
                }
                catch (Exception ex)
                {

                }
                if (Tong_truong23 != truong23 && !string.IsNullOrEmpty(txt_Truong_23.Text))
                {
                    txt_Truong_23.BackColor = Color.Red;
                    txt_Truong_23.ForeColor = Color.White;
                    txt_Truong_23.Tag = "1";
                }
                else
                {
                    txt_Truong_23.BackColor = Color.White;
                    txt_Truong_23.ForeColor = Color.Black;
                    txt_Truong_23.Tag = "0";
                }
            }
            if (((TextEdit)sender).Name == "txt_Truong_12" | ((TextEdit)sender).Name == "txt_Truong_09" | ((TextEdit)sender).Name == "txt_Truong_10" 
               |((TextEdit)sender).Name == "txt_Truong_15" | ((TextEdit)sender).Name == "txt_Truong_16" | ((TextEdit)sender).Name == "txt_Truong_21"
               |((TextEdit)sender).Name == "txt_Truong_22" | ((TextEdit)sender).Name == "txt_Truong_18")
            {
                Tong_truong12= Total_Truong12(txt_Truong_09, txt_Truong_10, txt_Truong_15, txt_Truong_16, txt_Truong_21, txt_Truong_22);

                double truong12 = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_Truong_12.Text))
                        truong12 = double.Parse(txt_Truong_12.Text.Replace(",", ""));
                }
                catch (Exception ex)
                {

                }
                if (Tong_truong12 != truong12 && !string.IsNullOrEmpty(txt_Truong_12.Text))
                {
                    txt_Truong_12.BackColor = Color.Red;
                    txt_Truong_12.ForeColor = Color.White;
                    txt_Truong_12.Tag = "1";
                }
                else
                {
                    txt_Truong_12.BackColor = Color.White;
                    txt_Truong_12.ForeColor = Color.Black;
                    txt_Truong_12.Tag = "0";
                }
                double truong18 = 0;
                try
                {
                    if (!string.IsNullOrEmpty(txt_Truong_18.Text))
                        truong18 = double.Parse(txt_Truong_18.Text.Replace(",", ""));
                }
                catch (Exception ex)
                {

                }
                if (Tong_truong18 != truong18 && !string.IsNullOrEmpty(txt_Truong_18.Text))
                {
                    txt_Truong_18.BackColor = Color.Red;
                    txt_Truong_18.ForeColor = Color.White;
                    txt_Truong_18.Tag = "1";
                }
                else
                {
                    txt_Truong_18.BackColor = Color.White;
                    txt_Truong_18.ForeColor = Color.Black;
                    txt_Truong_18.Tag = "0";
                }
            }
        }

        private void txt_Truong_09_1_KeyUp(object sender, KeyEventArgs e)
        {
            curency(((TextEdit)sender));
        }
        
        private void txt_Truong_09_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                ((TextEdit)sender).Text = ((TextEdit)sender).Text + "000";
                ((TextEdit)sender).SelectionStart = ((TextEdit)sender).Text.Length;
            }
        }
    }
}
