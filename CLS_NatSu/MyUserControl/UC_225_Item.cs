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
    public delegate void AllTextChange(object sender, EventArgs e);
    public partial class UC_225_Item : UserControl
    {
        public event AllTextChange Changed;
        public UC_225_Item()
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
            txt_Truong_07.Text = "";
            txt_Truong_08.Text = "";
            txt_Truong_09.Text = "";
            txt_Truong_10.Text = "";
            txt_Truong_11.Text = "";
            txt_Truong_12.Text = "";
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
            
            txt_Truong_02.ForeColor = Color.Black;
            txt_Truong_03.ForeColor = Color.Black;
            txt_Truong_04_1.ForeColor = Color.Black;
            txt_Truong_04_2.ForeColor = Color.Black;
            txt_Truong_04_3.ForeColor = Color.Black;
            txt_Truong_05_1.ForeColor = Color.Black;
            txt_Truong_05_2.ForeColor = Color.Black;
            txt_Truong_07.ForeColor = Color.Black;
            txt_Truong_08.ForeColor = Color.Black;
            txt_Truong_09.ForeColor = Color.Black;
            txt_Truong_10.ForeColor = Color.Black;
            txt_Truong_11.ForeColor = Color.Black;
            txt_Truong_12.ForeColor = Color.Black;
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

            txt_Truong_02.BackColor = Color.White;
            txt_Truong_03.BackColor = Color.White;
            txt_Truong_04_1.BackColor = Color.White;
            txt_Truong_04_2.BackColor = Color.White;
            txt_Truong_04_3.BackColor = Color.White;
            txt_Truong_05_1.BackColor = Color.White;
            txt_Truong_05_2.BackColor = Color.White;
            txt_Truong_07.BackColor = Color.White;
            txt_Truong_08.BackColor = Color.White;
            txt_Truong_09.BackColor = Color.White;
            txt_Truong_10.BackColor = Color.White;
            txt_Truong_11.BackColor = Color.White;
            txt_Truong_12.BackColor = Color.White;
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

            txt_Truong_02.Tag = "";
            txt_Truong_03.Tag = "";
            txt_Truong_04_1.Tag = "";
            txt_Truong_04_2.Tag = "";
            txt_Truong_04_3.Tag = "";
            txt_Truong_05_1.Tag = "";
            txt_Truong_05_2.Tag = "";
            txt_Truong_07.Tag = "";
            txt_Truong_08.Tag = "";
            txt_Truong_09.Tag = "";
            txt_Truong_10.Tag = "";
            txt_Truong_11.Tag = "";
            txt_Truong_12.Tag = "";
            txt_Truong_14.Tag = "";
            txt_Truong_15.Tag = "";
            txt_Truong_16.Tag = "";
            txt_Truong_17.Tag = "";
            txt_Truong_18.Tag = "";
            txt_Truong_19.Tag = "";
            txt_Truong_20.Tag = "";
            txt_Truong_21.Tag = "";
            txt_Truong_22.Tag = "";
            txt_Truong_23.Tag = "";
            txt_Truong_24.Tag = "";
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
                & string.IsNullOrEmpty(txt_Truong_07.Text)
                & string.IsNullOrEmpty(txt_Truong_08.Text)
                & string.IsNullOrEmpty(txt_Truong_09.Text)
                & string.IsNullOrEmpty(txt_Truong_10.Text)
                & string.IsNullOrEmpty(txt_Truong_11.Text)
                & string.IsNullOrEmpty(txt_Truong_12.Text)
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
                & string.IsNullOrEmpty(txt_Truong_24.Text))
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

        private void SetDataLookUpEdit()
        {
            category.Clear();
            category.Add(new Category() { DE = "" , JP = ""});
            category.Add(new Category() { DE = "1" , JP = "Số 70 or 70歳以上 or 70以上"});
            category.Add(new Category() { DE = "2" , JP = "二以上"});
            category.Add(new Category() { DE = "3" , JP = "月額変更予定"});
            category.Add(new Category() { DE = "4" , JP = "途中入社"});
            category.Add(new Category() { DE = "5" , JP = "病休 or 育休 or 休職 or育児休暇 or 病気休暇" });
            category.Add(new Category() { DE = "6" , JP = "短 or 短時間"});
            category.Add(new Category() { DE = "7" , JP = "パ or パート"});
            category.Add(new Category() { DE = "8" , JP = "年間平均"});
            category.Add(new Category() { DE = "9" , JP = "Thấy số 9"});
            category.Add(new Category() { DE = "*" , JP = "Nếu có hai giá trị chữ hoặc không phán đoán được (chữ khác)" });
        }
        public void SetFocusItem(object sender)
        {
            try
            {
                ((TextEdit)sender).Focus();
            }
            catch
            {
                try
                {
                    ((LookUpEdit)sender).Focus();
                }
                catch
                { }
            }
        }
        public void UC_225_Load(object sender, EventArgs e)
        {
            SetDataLookUpEdit();
            txt_Truong_19.Properties.DataSource = category;
            txt_Truong_19.Properties.DisplayMember = "DE";
            txt_Truong_19.Properties.ValueMember = "DE";

        }

        private void txt_Truong_02_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 6, (TextEdit)sender);
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
            }
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_04_1_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (!string.IsNullOrEmpty(txt_Truong_04_1.Text)& txt_Truong_04_1.Text.IndexOf("*")<0)
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
                    else if((txt_Truong_03.Text.IndexOf("*") >= 0 | string.IsNullOrEmpty(txt_Truong_03.Text)) & a >= 1 & a <= 64)
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_05_2_1_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 2, (TextEdit)sender);
            if (((TextEdit)sender).Text != "8" & ((TextEdit)sender).Text != "08" 
                & ((TextEdit)sender).Text != "9" & ((TextEdit)sender).Text != "09" & !string.IsNullOrEmpty(((TextEdit)sender).Text))
            {
                ((TextEdit)sender).BackColor = Color.Red;
                ((TextEdit)sender).ForeColor = Color.White;
            }
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
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
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
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

        private void txt_Truong_19_TextChanged(object sender, EventArgs e)
        {
            lb_Truong_19.Text= (from w in category where w.DE == txt_Truong_19.Text select w.JP).FirstOrDefault();
            if (Global.FlagCheckDeSo & ((LookUpEdit)sender).BackColor == Color.White & ((LookUpEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((LookUpEdit)sender).BackColor = Color.PaleVioletRed;
                ((LookUpEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_02_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                SendKeys.Send("+{Tab}");
            }
            else if(e.KeyCode == Keys.Right)
            {
                SendKeys.Send("{Tab}");
            }
            else if(e.KeyCode==Keys.Down &
                (((TextEdit)sender).Name == "txt_Truong_02"
                || ((TextEdit)sender).Name == "txt_Truong_03"
                || ((TextEdit)sender).Name == "txt_Truong_04_1"
                || ((TextEdit)sender).Name == "txt_Truong_04_2"
                || ((TextEdit)sender).Name == "txt_Truong_04_3"))
            {
                txt_Truong_08.Focus();
            }
            else if(e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_08")
            {
                txt_Truong_14.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_14")
            {
                txt_Truong_20.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_09")
            {
                txt_Truong_15.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_15")
            {
                txt_Truong_21.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_10")
            {
                txt_Truong_16.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_16")
            {
                txt_Truong_22.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_11")
            {
                txt_Truong_17.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_17")
            {
                txt_Truong_23.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_07")
            {
                txt_Truong_19.Focus();
            }
            else if (e.KeyCode == Keys.Down & ((TextEdit)sender).Name == "txt_Truong_12")
            {
                txt_Truong_18.Focus();
            }
            else if (e.KeyCode == Keys.Down &
               (((TextEdit)sender).Name == "txt_Truong_05_1"
               || ((TextEdit)sender).Name == "txt_Truong_05_2"))
            {
                txt_Truong_24.Focus();
            }
            else if (e.KeyCode == Keys.Down & (((TextEdit)sender).Name == "txt_Truong_18" | ((TextEdit)sender).Name == "txt_Truong_24"))
            {
                txt_Truong_23.Focus();
            }
            
            else if (e.KeyCode == Keys.Up & 
                (((TextEdit)sender).Name == "txt_Truong_08"
                | ((TextEdit)sender).Name == "txt_Truong_09"
                | ((TextEdit)sender).Name == "txt_Truong_10"
                | ((TextEdit)sender).Name == "txt_Truong_11"
                | ((TextEdit)sender).Name == "txt_Truong_12"
                | ((TextEdit)sender).Name == "txt_Truong_05_1"
                | ((TextEdit)sender).Name == "txt_Truong_05_2"))
            {
                txt_Truong_02.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_14")
            {
                txt_Truong_08.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_15")
            {
                txt_Truong_09.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_16")
            {
                txt_Truong_10.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_17")
            {
                txt_Truong_11.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_18")
            {
                txt_Truong_12.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_24")
            {
                txt_Truong_05_1.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_20")
            {
                txt_Truong_14.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_21")
            {
                txt_Truong_15.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_22")
            {
                txt_Truong_15.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_23")
            {
                txt_Truong_17.Focus();
            }
            else if (e.KeyCode == Keys.Up & ((TextEdit)sender).Name == "txt_Truong_07")
            {
                txt_Truong_04_3.Focus();
            }
            else if (e.Control & e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
            }
        }

        private void txt_Truong_19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SendKeys.Send("+{Tab}");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right | e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                txt_Truong_23.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txt_Truong_07.Focus();
                e.Handled = true;
            }
            else if (e.Control & e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
            }
        }

        private void txt_Truong_19_KeyPress(object sender, KeyPressEventArgs e)
        {
            string[] data19 = { "8", "42", "49", "50", "51", "52", "53", "54", "55", "56", "57","63" };
            if (!data19.Contains((int)e.KeyChar+""))
            {
                e.Handled = true;
            }
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
            if (((TextEdit)sender).Name == "txt_Truong_09" | ((TextEdit)sender).Name == "txt_Truong_10" || ((TextEdit)sender).Name == "txt_Truong_11")
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
                    //txt_Truong_11.Tag = "1";
                }
                else
                {
                    txt_Truong_11.BackColor = Color.White;
                    txt_Truong_11.ForeColor = Color.Black;
                    //txt_Truong_11.Tag = "0";
                }
            }
            else if (((TextEdit)sender).Name == "txt_Truong_15" | ((TextEdit)sender).Name == "txt_Truong_16" || ((TextEdit)sender).Name == "txt_Truong_17")
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
                    //txt_Truong_17.Tag = "1";
                }
                else
                {
                    txt_Truong_17.BackColor = Color.White;
                    txt_Truong_17.ForeColor = Color.Black;
                    //txt_Truong_17.Tag = "0";
                }
            }
            else if (((TextEdit)sender).Name == "txt_Truong_21" | ((TextEdit)sender).Name == "txt_Truong_22" || ((TextEdit)sender).Name == "txt_Truong_23")
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
                    //txt_Truong_23.Tag = "1";
                }
                else
                {
                    txt_Truong_23.BackColor = Color.White;
                    txt_Truong_23.ForeColor = Color.Black;
                    //txt_Truong_23.Tag = "0";
                }
            }
            else if (((TextEdit)sender).Name == "txt_Truong_12" | ((TextEdit)sender).Name == "txt_Truong_09" | ((TextEdit)sender).Name == "txt_Truong_10" 
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
                    //txt_Truong_12.Tag = "1";
                }
                else
                {
                    txt_Truong_12.BackColor = Color.White;
                    txt_Truong_12.ForeColor = Color.Black;
                    //txt_Truong_12.Tag = "0";
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
                    //txt_Truong_18.Tag = "1";
                }
                else
                {
                    txt_Truong_18.BackColor = Color.White;
                    txt_Truong_18.ForeColor = Color.Black;
                    //txt_Truong_18.Tag = "0";
                }
            }
            if (Global.FlagCheckDeSo & ((TextEdit)sender).BackColor == Color.White & ((TextEdit)sender).Tag + "" == "PaleVioletRed")
            {
                ((TextEdit)sender).BackColor = Color.PaleVioletRed;
                ((TextEdit)sender).ForeColor = Color.White;
            }
        }

        private void txt_Truong_09_1_KeyUp(object sender, KeyEventArgs e)
        {
            curency(((TextEdit)sender));
        }
        
        private void txt_Truong_09_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.Tab)
            {
                
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Add)
            {
                ((TextEdit)sender).Text = ((TextEdit)sender).Text + "000";
                ((TextEdit)sender).SelectionStart = ((TextEdit)sender).Text.Length;
            }
            else if(e.KeyCode==Keys.Divide)
            {
                if (((TextEdit)sender).Name == "txt_Truong_11" & (!string.IsNullOrEmpty(txt_Truong_09.Text)|!string.IsNullOrEmpty(txt_Truong_10.Text)))
                {
                    txt_Truong_11.Text = Tong_truong11+"";
                    txt_Truong_11.SelectAll();
                }
                else if (((TextEdit)sender).Name == "txt_Truong_17" & (!string.IsNullOrEmpty(txt_Truong_15.Text) | !string.IsNullOrEmpty(txt_Truong_16.Text)))
                {
                    txt_Truong_17.Text = Tong_truong17 + "";
                    txt_Truong_17.SelectAll();
                }
                else if (((TextEdit)sender).Name == "txt_Truong_23" & (!string.IsNullOrEmpty(txt_Truong_21.Text) | !string.IsNullOrEmpty(txt_Truong_22.Text)))
                {
                    txt_Truong_23.Text = Tong_truong23 + "";
                    txt_Truong_23.SelectAll();
                }
            }
            else
                txt_Truong_02_KeyDown(sender, e);
        }
    }
}
