using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CLS_NatSu.MyClass;
using CLS_NatSu.Properties;
using CLS_NatSu.MyData;

namespace CLS_NatSu.MyForm
{
    public partial class ShowImage : DevExpress.XtraEditors.XtraForm
    {
        public ShowImage()
        {
            InitializeComponent();
        }
        public string BatchName = "";
        public string BatchID = "";
        public string IdImage = "";

        private void SoSanhDoiMau(TextEdit txt1, TextEdit txt2, TextEdit txt3)
        {
            if ((txt1.Text != txt2.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Red;
                if (txt3.Text == txt1.Text)
                {
                    txt3.ForeColor = Color.White;
                    txt3.BackColor = Color.Green;
                }
            }
            if ((txt1.Text != txt3.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt3.ForeColor = Color.White;
                txt3.BackColor = Color.Red;
                if (txt1.Text == txt2.Text)
                {
                    txt2.ForeColor = Color.White;
                    txt2.BackColor = Color.Green;
                }
            }
        }

        private void SoSanhDoiMau_Nocheck(TextEdit txt1, TextEdit txt2)
        {
            if ((txt1.Text != txt2.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Red;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Red;
            }
            else if(!string.IsNullOrEmpty(txt1.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Green;
            }
        }
        private void SoSanhDoiMau_cbb(System.Windows.Forms.ComboBox txt1, System.Windows.Forms.ComboBox txt2, System.Windows.Forms.ComboBox txt3)
        {
            if ((txt1.Text != txt2.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Red;
                if (txt3.Text == txt1.Text)
                {
                    txt3.ForeColor = Color.White;
                    txt3.BackColor = Color.Green;
                }
            }
            if ((txt1.Text != txt3.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt3.ForeColor = Color.White;
                txt3.BackColor = Color.Red;
                if (txt1.Text == txt2.Text)
                {
                    txt2.ForeColor = Color.White;
                    txt2.BackColor = Color.Green;
                }
            }
        }

        private void SoSanhDoiMau_Nocheck_cbb(System.Windows.Forms.ComboBox txt1, System.Windows.Forms.ComboBox txt2)
        {
            if ((txt1.Text != txt2.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Red;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Red;
            }
            else if (!string.IsNullOrEmpty(txt1.Text))
            {
                txt1.ForeColor = Color.White;
                txt1.BackColor = Color.Green;
                txt2.ForeColor = Color.White;
                txt2.BackColor = Color.Green;
            }
        }

        private void ShowImage_Load(object sender, EventArgs e)
        {
            uC_ShowDataInput1.UC_225_1.UC_225_Load(null, null);
            uC_ShowDataInput1.UC_2225_1.UC_2225_Load(null, null);
            uC_ShowDataInput1.UC_225_2.UC_225_Load(null, null);
            uC_ShowDataInput1.UC_2225_2.UC_2225_Load(null, null);
            uC_ShowDataInput1.UC_225_3.UC_225_Load(null, null);
            uC_ShowDataInput1.UC_2225_3.UC_2225_Load(null, null);

            //uC_ShowDataInput1.UC_225_1.Focus += UC_Minami_Focus;
            //uC_ShowDataInput1.UC_225_2.Focus += UC_Minami_Focus;
            //uC_ShowDataInput1.UC_225_3.Focus += UC_Minami_Focus;

            lb_Batch.Text = BatchName;
            lb_Image.Text = IdImage;
            uc_PictureBox1.LoadImage(Global.Webservice + BatchID + "/" + IdImage, IdImage, Settings.Default.ZoomImage);
             var data = (from w in Global.Db.tbl_DeSos where w.BatchID == BatchID && w.IDImage == IdImage orderby w.Phase descending,w.UserName ascending,w.IDPhieu ascending select w).ToList();
            if (data.Count == 15)
            {
                uC_ShowDataInput1.lb_UserCheck.Text = "Check: " + data[0].UserName;
                uC_ShowDataInput1.lb_User1.Text = "User1: " + data[5].UserName;
                uC_ShowDataInput1.lb_User2.Text = "User2: " + data[10].UserName;
                uC_ShowDataInput1.txt_Truong_01_Check.Text = data[0].Truong_01;
                if (data[0].Truong_01 == "225")
                {
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_09.Text = data[0].Truong_09;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_10.Text = data[0].Truong_10;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_11.Text = data[0].Truong_11;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_12.Text = data[0].Truong_12;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_15.Text = data[0].Truong_15;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_16.Text = data[0].Truong_16;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_17.Text = data[0].Truong_17;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_18.Text = data[0].Truong_18;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_19.Text = data[0].Truong_19;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_21.Text = data[0].Truong_21;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_22.Text = data[0].Truong_22;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_23.Text = data[0].Truong_23;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item1.txt_Truong_24.Text = data[0].Truong_24;

                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_09.Text = data[1].Truong_09;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_10.Text = data[1].Truong_10;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_11.Text = data[1].Truong_11;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_12.Text = data[1].Truong_12;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_15.Text = data[1].Truong_15;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_16.Text = data[1].Truong_16;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_17.Text = data[1].Truong_17;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_18.Text = data[1].Truong_18;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_19.Text = data[1].Truong_19;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_21.Text = data[1].Truong_21;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_22.Text = data[1].Truong_22;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_23.Text = data[1].Truong_23;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item2.txt_Truong_24.Text = data[1].Truong_24;

                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_09.Text = data[2].Truong_09;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_10.Text = data[2].Truong_10;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_11.Text = data[2].Truong_11;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_12.Text = data[2].Truong_12;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_15.Text = data[2].Truong_15;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_16.Text = data[2].Truong_16;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_17.Text = data[2].Truong_17;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_18.Text = data[2].Truong_18;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_19.Text = data[2].Truong_19;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_21.Text = data[2].Truong_21;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_22.Text = data[2].Truong_22;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_23.Text = data[2].Truong_23;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item3.txt_Truong_24.Text = data[2].Truong_24;

                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_09.Text = data[3].Truong_09;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_10.Text = data[3].Truong_10;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_11.Text = data[3].Truong_11;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_12.Text = data[3].Truong_12;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_15.Text = data[3].Truong_15;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_16.Text = data[3].Truong_16;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_17.Text = data[3].Truong_17;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_18.Text = data[3].Truong_18;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_19.Text = data[3].Truong_19;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_21.Text = data[3].Truong_21;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_22.Text = data[3].Truong_22;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_23.Text = data[3].Truong_23;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item4.txt_Truong_24.Text = data[3].Truong_24;

                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_09.Text = data[4].Truong_09;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_10.Text = data[4].Truong_10;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_11.Text = data[4].Truong_11;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_12.Text = data[4].Truong_12;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_15.Text = data[4].Truong_15;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_16.Text = data[4].Truong_16;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_17.Text = data[4].Truong_17;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_18.Text = data[4].Truong_18;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_19.Text = data[4].Truong_19;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_21.Text = data[4].Truong_21;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_22.Text = data[4].Truong_22;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_23.Text = data[4].Truong_23;
                    uC_ShowDataInput1.UC_225_1.uC_225_Item5.txt_Truong_24.Text = data[4].Truong_24;
                    uC_ShowDataInput1.UC_225_1.txt_Truong_Flag.Text = data[4].Truong_Flag;
                }
                else
                {
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_02.Text = data[0].Truong_02;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_03.Text = data[0].Truong_03;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_04_1.Text = data[0].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_04_2.Text = data[0].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_04_3.Text = data[0].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_05_1.Text = data[0].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_05_2.Text = data[0].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_06.Text = data[0].Truong_06;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_07.Text = data[0].Truong_07;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_08.Text = data[0].Truong_08;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_09.Text = data[0].Truong_09;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_10.Text = data[0].Truong_10;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_11.Text = data[0].Truong_11;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_12.Text = data[0].Truong_12;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_13.Text = data[0].Truong_13;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_14.Text = data[0].Truong_14;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_15.Text = data[0].Truong_15;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_16.Text = data[0].Truong_16;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_17.Text = data[0].Truong_17;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_18.Text = data[0].Truong_18;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_19.Text = data[0].Truong_19;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_20.Text = data[0].Truong_20;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_21.Text = data[0].Truong_21;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_22.Text = data[0].Truong_22;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_23.Text = data[0].Truong_23;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_24.Text = data[0].Truong_24;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_25.Text = data[0].Truong_25;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item1.txt_Truong_26.Text = data[0].Truong_26;

                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_02.Text = data[1].Truong_02;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_03.Text = data[1].Truong_03;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_04_1.Text = data[1].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_04_2.Text = data[1].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_04_3.Text = data[1].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_05_1.Text = data[1].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_05_2.Text = data[1].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_06.Text = data[1].Truong_06;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_07.Text = data[1].Truong_07;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_08.Text = data[1].Truong_08;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_09.Text = data[1].Truong_09;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_10.Text = data[1].Truong_10;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_11.Text = data[1].Truong_11;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_12.Text = data[1].Truong_12;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_13.Text = data[1].Truong_13;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_14.Text = data[1].Truong_14;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_15.Text = data[1].Truong_15;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_16.Text = data[1].Truong_16;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_17.Text = data[1].Truong_17;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_18.Text = data[1].Truong_18;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_19.Text = data[1].Truong_19;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_20.Text = data[1].Truong_20;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_21.Text = data[1].Truong_21;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_22.Text = data[1].Truong_22;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_23.Text = data[1].Truong_23;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_24.Text = data[1].Truong_24;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_25.Text = data[1].Truong_25;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item2.txt_Truong_26.Text = data[1].Truong_26;

                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_02.Text = data[2].Truong_02;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_03.Text = data[2].Truong_03;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_04_1.Text = data[2].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_04_2.Text = data[2].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_04_3.Text = data[2].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_05_1.Text = data[2].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_05_2.Text = data[2].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_06.Text = data[2].Truong_06;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_07.Text = data[2].Truong_07;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_08.Text = data[2].Truong_08;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_09.Text = data[2].Truong_09;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_10.Text = data[2].Truong_10;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_11.Text = data[2].Truong_11;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_12.Text = data[2].Truong_12;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_13.Text = data[2].Truong_13;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_14.Text = data[2].Truong_14;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_15.Text = data[2].Truong_15;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_16.Text = data[2].Truong_16;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_17.Text = data[2].Truong_17;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_18.Text = data[2].Truong_18;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_19.Text = data[2].Truong_19;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_20.Text = data[2].Truong_20;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_21.Text = data[2].Truong_21;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_22.Text = data[2].Truong_22;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_23.Text = data[2].Truong_23;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_24.Text = data[2].Truong_24;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_25.Text = data[2].Truong_25;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item3.txt_Truong_26.Text = data[2].Truong_26;

                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_02.Text = data[3].Truong_02;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_03.Text = data[3].Truong_03;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_04_1.Text = data[3].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_04_2.Text = data[3].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_04_3.Text = data[3].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_05_1.Text = data[3].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_05_2.Text = data[3].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_06.Text = data[3].Truong_06;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_07.Text = data[3].Truong_07;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_08.Text = data[3].Truong_08;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_09.Text = data[3].Truong_09;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_10.Text = data[3].Truong_10;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_11.Text = data[3].Truong_11;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_12.Text = data[3].Truong_12;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_13.Text = data[3].Truong_13;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_14.Text = data[3].Truong_14;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_15.Text = data[3].Truong_15;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_16.Text = data[3].Truong_16;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_17.Text = data[3].Truong_17;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_18.Text = data[3].Truong_18;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_19.Text = data[3].Truong_19;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_20.Text = data[3].Truong_20;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_21.Text = data[3].Truong_21;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_22.Text = data[3].Truong_22;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_23.Text = data[3].Truong_23;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_24.Text = data[3].Truong_24;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_25.Text = data[3].Truong_25;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item4.txt_Truong_26.Text = data[3].Truong_26;

                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_02.Text = data[4].Truong_02;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_03.Text = data[4].Truong_03;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_04_1.Text = data[4].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_04_2.Text = data[4].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_04_3.Text = data[4].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_05_1.Text = data[4].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_05_2.Text = data[4].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_06.Text = data[4].Truong_06;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_07.Text = data[4].Truong_07;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_08.Text = data[4].Truong_08;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_09.Text = data[4].Truong_09;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_10.Text = data[4].Truong_10;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_11.Text = data[4].Truong_11;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_12.Text = data[4].Truong_12;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_13.Text = data[4].Truong_13;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_14.Text = data[4].Truong_14;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_15.Text = data[4].Truong_15;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_16.Text = data[4].Truong_16;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_17.Text = data[4].Truong_17;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_18.Text = data[4].Truong_18;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_19.Text = data[4].Truong_19;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_20.Text = data[4].Truong_20;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_21.Text = data[4].Truong_21;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_22.Text = data[4].Truong_22;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_23.Text = data[4].Truong_23;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_24.Text = data[4].Truong_24;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_25.Text = data[4].Truong_25;
                    uC_ShowDataInput1.UC_2225_1.uC_2225_Item5.txt_Truong_26.Text = data[4].Truong_26;
                    uC_ShowDataInput1.UC_2225_1.txt_Truong_Flag.Text = data[4].Truong_Flag;
                }

                uC_ShowDataInput1.txt_Truong_01_User1.Text = data[5].Truong_01;
                if (data[5].Truong_01 == "225")
                {
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_09.Text = data[5].Truong_09;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_10.Text = data[5].Truong_10;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_11.Text = data[5].Truong_11;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_12.Text = data[5].Truong_12;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_15.Text = data[5].Truong_15;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_16.Text = data[5].Truong_16;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_17.Text = data[5].Truong_17;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_18.Text = data[5].Truong_18;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_19.Text = data[5].Truong_19;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_21.Text = data[5].Truong_21;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_22.Text = data[5].Truong_22;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_23.Text = data[5].Truong_23;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item1.txt_Truong_24.Text = data[5].Truong_24;

                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_09.Text = data[6].Truong_09;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_10.Text = data[6].Truong_10;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_11.Text = data[6].Truong_11;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_12.Text = data[6].Truong_12;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_15.Text = data[6].Truong_15;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_16.Text = data[6].Truong_16;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_17.Text = data[6].Truong_17;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_18.Text = data[6].Truong_18;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_19.Text = data[6].Truong_19;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_21.Text = data[6].Truong_21;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_22.Text = data[6].Truong_22;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_23.Text = data[6].Truong_23;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item2.txt_Truong_24.Text = data[6].Truong_24;

                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_09.Text = data[7].Truong_09;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_10.Text = data[7].Truong_10;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_11.Text = data[7].Truong_11;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_12.Text = data[7].Truong_12;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_15.Text = data[7].Truong_15;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_16.Text = data[7].Truong_16;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_17.Text = data[7].Truong_17;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_18.Text = data[7].Truong_18;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_19.Text = data[7].Truong_19;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_21.Text = data[7].Truong_21;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_22.Text = data[7].Truong_22;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_23.Text = data[7].Truong_23;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item3.txt_Truong_24.Text = data[7].Truong_24;

                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_09.Text = data[8].Truong_09;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_10.Text = data[8].Truong_10;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_11.Text = data[8].Truong_11;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_12.Text = data[8].Truong_12;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_15.Text = data[8].Truong_15;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_16.Text = data[8].Truong_16;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_17.Text = data[8].Truong_17;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_18.Text = data[8].Truong_18;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_19.Text = data[8].Truong_19;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_21.Text = data[8].Truong_21;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_22.Text = data[8].Truong_22;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_23.Text = data[8].Truong_23;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item4.txt_Truong_24.Text = data[8].Truong_24;

                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_09.Text = data[9].Truong_09;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_10.Text = data[9].Truong_10;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_11.Text = data[9].Truong_11;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_12.Text = data[9].Truong_12;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_15.Text = data[9].Truong_15;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_16.Text = data[9].Truong_16;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_17.Text = data[9].Truong_17;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_18.Text = data[9].Truong_18;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_19.Text = data[9].Truong_19;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_21.Text = data[9].Truong_21;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_22.Text = data[9].Truong_22;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_23.Text = data[9].Truong_23;
                    uC_ShowDataInput1.UC_225_2.uC_225_Item5.txt_Truong_24.Text = data[9].Truong_24;
                    uC_ShowDataInput1.UC_225_2.txt_Truong_Flag.Text = data[9].Truong_Flag;
                }
                else
                {
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_02.Text = data[5].Truong_02;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_03.Text = data[5].Truong_03;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_04_1.Text = data[5].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_04_2.Text = data[5].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_04_3.Text = data[5].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_05_1.Text = data[5].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_05_2.Text = data[5].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_06.Text = data[5].Truong_06;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_07.Text = data[5].Truong_07;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_08.Text = data[5].Truong_08;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_09.Text = data[5].Truong_09;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_10.Text = data[5].Truong_10;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_11.Text = data[5].Truong_11;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_12.Text = data[5].Truong_12;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_13.Text = data[5].Truong_13;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_14.Text = data[5].Truong_14;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_15.Text = data[5].Truong_15;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_16.Text = data[5].Truong_16;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_17.Text = data[5].Truong_17;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_18.Text = data[5].Truong_18;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_19.Text = data[5].Truong_19;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_20.Text = data[5].Truong_20;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_21.Text = data[5].Truong_21;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_22.Text = data[5].Truong_22;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_23.Text = data[5].Truong_23;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_24.Text = data[5].Truong_24;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_25.Text = data[5].Truong_25;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item1.txt_Truong_26.Text = data[5].Truong_26;

                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_02.Text = data[6].Truong_02;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_03.Text = data[6].Truong_03;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_04_1.Text = data[6].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_04_2.Text = data[6].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_04_3.Text = data[6].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_05_1.Text = data[6].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_05_2.Text = data[6].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_06.Text = data[6].Truong_06;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_07.Text = data[6].Truong_07;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_08.Text = data[6].Truong_08;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_09.Text = data[6].Truong_09;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_10.Text = data[6].Truong_10;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_11.Text = data[6].Truong_11;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_12.Text = data[6].Truong_12;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_13.Text = data[6].Truong_13;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_14.Text = data[6].Truong_14;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_15.Text = data[6].Truong_15;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_16.Text = data[6].Truong_16;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_17.Text = data[6].Truong_17;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_18.Text = data[6].Truong_18;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_19.Text = data[6].Truong_19;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_20.Text = data[6].Truong_20;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_21.Text = data[6].Truong_21;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_22.Text = data[6].Truong_22;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_23.Text = data[6].Truong_23;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_24.Text = data[6].Truong_24;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_25.Text = data[6].Truong_25;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item2.txt_Truong_26.Text = data[6].Truong_26;

                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_02.Text = data[7].Truong_02;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_03.Text = data[7].Truong_03;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_04_1.Text = data[7].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_04_2.Text = data[7].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_04_3.Text = data[7].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_05_1.Text = data[7].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_05_2.Text = data[7].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_06.Text = data[7].Truong_06;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_07.Text = data[7].Truong_07;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_08.Text = data[7].Truong_08;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_09.Text = data[7].Truong_09;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_10.Text = data[7].Truong_10;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_11.Text = data[7].Truong_11;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_12.Text = data[7].Truong_12;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_13.Text = data[7].Truong_13;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_14.Text = data[7].Truong_14;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_15.Text = data[7].Truong_15;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_16.Text = data[7].Truong_16;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_17.Text = data[7].Truong_17;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_18.Text = data[7].Truong_18;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_19.Text = data[7].Truong_19;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_20.Text = data[7].Truong_20;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_21.Text = data[7].Truong_21;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_22.Text = data[7].Truong_22;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_23.Text = data[7].Truong_23;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_24.Text = data[7].Truong_24;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_25.Text = data[7].Truong_25;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item3.txt_Truong_26.Text = data[7].Truong_26;

                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_02.Text = data[8].Truong_02;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_03.Text = data[8].Truong_03;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_04_1.Text = data[8].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_04_2.Text = data[8].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_04_3.Text = data[8].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_05_1.Text = data[8].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_05_2.Text = data[8].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_06.Text = data[8].Truong_06;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_07.Text = data[8].Truong_07;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_08.Text = data[8].Truong_08;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_09.Text = data[8].Truong_09;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_10.Text = data[8].Truong_10;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_11.Text = data[8].Truong_11;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_12.Text = data[8].Truong_12;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_13.Text = data[8].Truong_13;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_14.Text = data[8].Truong_14;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_15.Text = data[8].Truong_15;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_16.Text = data[8].Truong_16;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_17.Text = data[8].Truong_17;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_18.Text = data[8].Truong_18;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_19.Text = data[8].Truong_19;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_20.Text = data[8].Truong_20;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_21.Text = data[8].Truong_21;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_22.Text = data[8].Truong_22;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_23.Text = data[8].Truong_23;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_24.Text = data[8].Truong_24;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_25.Text = data[8].Truong_25;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item4.txt_Truong_26.Text = data[8].Truong_26;

                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_02.Text = data[9].Truong_02;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_03.Text = data[9].Truong_03;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_04_1.Text = data[9].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_04_2.Text = data[9].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_04_3.Text = data[9].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_05_1.Text = data[9].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_05_2.Text = data[9].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_06.Text = data[9].Truong_06;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_07.Text = data[9].Truong_07;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_08.Text = data[9].Truong_08;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_09.Text = data[9].Truong_09;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_10.Text = data[9].Truong_10;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_11.Text = data[9].Truong_11;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_12.Text = data[9].Truong_12;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_13.Text = data[9].Truong_13;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_14.Text = data[9].Truong_14;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_15.Text = data[9].Truong_15;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_16.Text = data[9].Truong_16;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_17.Text = data[9].Truong_17;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_18.Text = data[9].Truong_18;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_19.Text = data[9].Truong_19;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_20.Text = data[9].Truong_20;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_21.Text = data[9].Truong_21;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_22.Text = data[9].Truong_22;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_23.Text = data[9].Truong_23;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_24.Text = data[9].Truong_24;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_25.Text = data[9].Truong_25;
                    uC_ShowDataInput1.UC_2225_2.uC_2225_Item5.txt_Truong_26.Text = data[9].Truong_26;
                    uC_ShowDataInput1.UC_2225_2.txt_Truong_Flag.Text = data[9].Truong_Flag;
                }


                uC_ShowDataInput1.txt_Truong_01_User2.Text = data[10].Truong_01;
                if (data[10].Truong_01 == "225")
                {
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_02.Text = data[10].Truong_02;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_03.Text = data[10].Truong_03;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_04_1.Text = data[10].Truong_04_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_04_2.Text = data[10].Truong_04_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_04_3.Text = data[10].Truong_04_3;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_05_1.Text = data[10].Truong_05_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_05_2.Text = data[10].Truong_05_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_08.Text = data[10].Truong_08;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_09.Text = data[10].Truong_09;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_10.Text = data[10].Truong_10;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_11.Text = data[10].Truong_11;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_12.Text = data[10].Truong_12;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_14.Text = data[10].Truong_14;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_15.Text = data[10].Truong_15;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_16.Text = data[10].Truong_16;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_17.Text = data[10].Truong_17;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_18.Text = data[10].Truong_18;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_19.Text = data[10].Truong_19;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_20.Text = data[10].Truong_20;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_21.Text = data[10].Truong_21;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_22.Text = data[10].Truong_22;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_23.Text = data[10].Truong_23;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item1.txt_Truong_24.Text = data[10].Truong_24;

                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_02.Text = data[11].Truong_02;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_03.Text = data[11].Truong_03;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_04_1.Text = data[11].Truong_04_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_04_2.Text = data[11].Truong_04_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_04_3.Text = data[11].Truong_04_3;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_05_1.Text = data[11].Truong_05_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_05_2.Text = data[11].Truong_05_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_08.Text = data[11].Truong_08;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_09.Text = data[11].Truong_09;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_10.Text = data[11].Truong_10;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_11.Text = data[11].Truong_11;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_12.Text = data[11].Truong_12;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_14.Text = data[11].Truong_14;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_15.Text = data[11].Truong_15;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_16.Text = data[11].Truong_16;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_17.Text = data[11].Truong_17;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_18.Text = data[11].Truong_18;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_19.Text = data[11].Truong_19;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_20.Text = data[11].Truong_20;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_21.Text = data[11].Truong_21;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_22.Text = data[11].Truong_22;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_23.Text = data[11].Truong_23;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item2.txt_Truong_24.Text = data[11].Truong_24;

                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_02.Text = data[12].Truong_02;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_03.Text = data[12].Truong_03;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_04_1.Text = data[12].Truong_04_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_04_2.Text = data[12].Truong_04_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_04_3.Text = data[12].Truong_04_3;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_05_1.Text = data[12].Truong_05_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_05_2.Text = data[12].Truong_05_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_08.Text = data[12].Truong_08;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_09.Text = data[12].Truong_09;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_10.Text = data[12].Truong_10;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_11.Text = data[12].Truong_11;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_12.Text = data[12].Truong_12;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_14.Text = data[12].Truong_14;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_15.Text = data[12].Truong_15;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_16.Text = data[12].Truong_16;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_17.Text = data[12].Truong_17;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_18.Text = data[12].Truong_18;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_19.Text = data[12].Truong_19;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_20.Text = data[12].Truong_20;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_21.Text = data[12].Truong_21;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_22.Text = data[12].Truong_22;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_23.Text = data[12].Truong_23;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item3.txt_Truong_24.Text = data[12].Truong_24;

                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_02.Text = data[13].Truong_02;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_03.Text = data[13].Truong_03;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_04_1.Text = data[13].Truong_04_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_04_2.Text = data[13].Truong_04_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_04_3.Text = data[13].Truong_04_3;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_05_1.Text = data[13].Truong_05_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_05_2.Text = data[13].Truong_05_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_08.Text = data[13].Truong_08;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_09.Text = data[13].Truong_09;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_10.Text = data[13].Truong_10;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_11.Text = data[13].Truong_11;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_12.Text = data[13].Truong_12;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_14.Text = data[13].Truong_14;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_15.Text = data[13].Truong_15;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_16.Text = data[13].Truong_16;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_17.Text = data[13].Truong_17;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_18.Text = data[13].Truong_18;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_19.Text = data[13].Truong_19;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_20.Text = data[13].Truong_20;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_21.Text = data[13].Truong_21;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_22.Text = data[13].Truong_22;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_23.Text = data[13].Truong_23;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item4.txt_Truong_24.Text = data[13].Truong_24;

                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_02.Text = data[14].Truong_02;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_03.Text = data[14].Truong_03;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_04_1.Text = data[14].Truong_04_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_04_2.Text = data[14].Truong_04_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_04_3.Text = data[14].Truong_04_3;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_05_1.Text = data[14].Truong_05_1;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_05_2.Text = data[14].Truong_05_2;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_08.Text = data[14].Truong_08;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_09.Text = data[14].Truong_09;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_10.Text = data[14].Truong_10;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_11.Text = data[14].Truong_11;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_12.Text = data[14].Truong_12;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_14.Text = data[14].Truong_14;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_15.Text = data[14].Truong_15;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_16.Text = data[14].Truong_16;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_17.Text = data[14].Truong_17;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_18.Text = data[14].Truong_18;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_19.Text = data[14].Truong_19;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_20.Text = data[14].Truong_20;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_21.Text = data[14].Truong_21;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_22.Text = data[14].Truong_22;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_23.Text = data[14].Truong_23;
                    uC_ShowDataInput1.UC_225_3.uC_225_Item5.txt_Truong_24.Text = data[14].Truong_24;
                    uC_ShowDataInput1.UC_225_3.txt_Truong_Flag.Text = data[14].Truong_Flag;
                }
                else
                {
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_02.Text = data[10].Truong_02;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_03.Text = data[10].Truong_03;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_04_1.Text = data[10].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_04_2.Text = data[10].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_04_3.Text = data[10].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_05_1.Text = data[10].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_05_2.Text = data[10].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_06.Text = data[10].Truong_06;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_07.Text = data[10].Truong_07;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_08.Text = data[10].Truong_08;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_09.Text = data[10].Truong_09;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_10.Text = data[10].Truong_10;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_11.Text = data[10].Truong_11;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_12.Text = data[10].Truong_12;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_13.Text = data[10].Truong_13;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_14.Text = data[10].Truong_14;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_15.Text = data[10].Truong_15;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_16.Text = data[10].Truong_16;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_17.Text = data[10].Truong_17;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_18.Text = data[10].Truong_18;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_19.Text = data[10].Truong_19;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_20.Text = data[10].Truong_20;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_21.Text = data[10].Truong_21;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_22.Text = data[10].Truong_22;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_23.Text = data[10].Truong_23;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_24.Text = data[10].Truong_24;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_25.Text = data[10].Truong_25;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item1.txt_Truong_26.Text = data[10].Truong_26;

                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_02.Text = data[11].Truong_02;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_03.Text = data[11].Truong_03;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_04_1.Text = data[11].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_04_2.Text = data[11].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_04_3.Text = data[11].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_05_1.Text = data[11].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_05_2.Text = data[11].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_06.Text = data[11].Truong_06;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_07.Text = data[11].Truong_07;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_08.Text = data[11].Truong_08;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_09.Text = data[11].Truong_09;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_10.Text = data[11].Truong_10;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_11.Text = data[11].Truong_11;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_12.Text = data[11].Truong_12;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_13.Text = data[11].Truong_13;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_14.Text = data[11].Truong_14;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_15.Text = data[11].Truong_15;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_16.Text = data[11].Truong_16;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_17.Text = data[11].Truong_17;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_18.Text = data[11].Truong_18;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_19.Text = data[11].Truong_19;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_20.Text = data[11].Truong_20;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_21.Text = data[11].Truong_21;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_22.Text = data[11].Truong_22;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_23.Text = data[11].Truong_23;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_24.Text = data[11].Truong_24;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_25.Text = data[11].Truong_25;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item2.txt_Truong_26.Text = data[11].Truong_26;

                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_02.Text = data[12].Truong_02;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_03.Text = data[12].Truong_03;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_04_1.Text = data[12].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_04_2.Text = data[12].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_04_3.Text = data[12].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_05_1.Text = data[12].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_05_2.Text = data[12].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_06.Text = data[12].Truong_06;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_07.Text = data[12].Truong_07;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_08.Text = data[12].Truong_08;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_09.Text = data[12].Truong_09;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_10.Text = data[12].Truong_10;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_11.Text = data[12].Truong_11;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_12.Text = data[12].Truong_12;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_13.Text = data[12].Truong_13;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_14.Text = data[12].Truong_14;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_15.Text = data[12].Truong_15;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_16.Text = data[12].Truong_16;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_17.Text = data[12].Truong_17;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_18.Text = data[12].Truong_18;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_19.Text = data[12].Truong_19;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_20.Text = data[12].Truong_20;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_21.Text = data[12].Truong_21;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_22.Text = data[12].Truong_22;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_23.Text = data[12].Truong_23;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_24.Text = data[12].Truong_24;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_25.Text = data[12].Truong_25;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item3.txt_Truong_26.Text = data[12].Truong_26;

                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_02.Text = data[13].Truong_02;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_03.Text = data[13].Truong_03;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_04_1.Text = data[13].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_04_2.Text = data[13].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_04_3.Text = data[13].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_05_1.Text = data[13].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_05_2.Text = data[13].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_06.Text = data[13].Truong_06;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_07.Text = data[13].Truong_07;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_08.Text = data[13].Truong_08;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_09.Text = data[13].Truong_09;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_10.Text = data[13].Truong_10;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_11.Text = data[13].Truong_11;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_12.Text = data[13].Truong_12;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_13.Text = data[13].Truong_13;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_14.Text = data[13].Truong_14;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_15.Text = data[13].Truong_15;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_16.Text = data[13].Truong_16;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_17.Text = data[13].Truong_17;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_18.Text = data[13].Truong_18;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_19.Text = data[13].Truong_19;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_20.Text = data[13].Truong_20;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_21.Text = data[13].Truong_21;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_22.Text = data[13].Truong_22;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_23.Text = data[13].Truong_23;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_24.Text = data[13].Truong_24;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_25.Text = data[13].Truong_25;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item4.txt_Truong_26.Text = data[13].Truong_26;

                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_02.Text = data[14].Truong_02;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_03.Text = data[14].Truong_03;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_04_1.Text = data[14].Truong_04_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_04_2.Text = data[14].Truong_04_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_04_3.Text = data[14].Truong_04_3;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_05_1.Text = data[14].Truong_05_1;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_05_2.Text = data[14].Truong_05_2;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_06.Text = data[14].Truong_06;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_07.Text = data[14].Truong_07;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_08.Text = data[14].Truong_08;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_09.Text = data[14].Truong_09;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_10.Text = data[14].Truong_10;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_11.Text = data[14].Truong_11;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_12.Text = data[14].Truong_12;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_13.Text = data[14].Truong_13;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_14.Text = data[14].Truong_14;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_15.Text = data[14].Truong_15;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_16.Text = data[14].Truong_16;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_17.Text = data[14].Truong_17;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_18.Text = data[14].Truong_18;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_19.Text = data[14].Truong_19;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_20.Text = data[14].Truong_20;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_21.Text = data[14].Truong_21;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_22.Text = data[14].Truong_22;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_23.Text = data[14].Truong_23;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_24.Text = data[14].Truong_24;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_25.Text = data[14].Truong_25;
                    uC_ShowDataInput1.UC_2225_3.uC_2225_Item5.txt_Truong_26.Text = data[14].Truong_26;
                    uC_ShowDataInput1.UC_2225_3.txt_Truong_Flag.Text = data[14].Truong_Flag;
                }
                SetColorUC(true);
            }
            //else if (data.Count == 2)
            //{
            //    uC_ShowDataInput1.lb_UserCheck.Text = "User1: " + data[0].UserName;
            //    uC_ShowDataInput1.lb_User1.Text = "User2: " + data[1].UserName;
            //    uC_ShowDataInput1.lb_User2.Text = "";

            //    uC_ShowDataInput1.UC_225_1.txt_Truong01.Text = data[0].Truong_001;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong02.Text = data[0].Truong_002;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong03.Text = data[0].Truong_003;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong04_1.Text = data[0].Truong_004_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong04_2.Text = data[0].Truong_004_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong05_1.Text = data[0].Truong_005_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong05_2.Text = data[0].Truong_005_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong06_1.Text = data[0].Truong_006_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong06_2.Text = data[0].Truong_006_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong07.Text = data[0].Truong_007;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong08.Text = data[0].Truong_008;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong09.Text = data[0].Truong_009;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong10.Text = data[0].Truong_010;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong11_1.Text = data[0].Truong_011_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong11_2.Text = data[0].Truong_011_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong12.Text = data[0].Truong_012;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong13.Text = data[0].Truong_013;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong14.Text = data[0].Truong_014;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong15.Text = data[0].Truong_015;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong16.Text = data[0].Truong_016;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong17.Text = data[0].Truong_017;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_1.Text = data[0].Truong_018_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_2.Text = data[0].Truong_018_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_3.Text = data[0].Truong_018_3;

            //    uC_ShowDataInput1.UC_225_2.txt_Truong01.Text = data[1].Truong_001;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong02.Text = data[1].Truong_002;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong03.Text = data[1].Truong_003;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong04_1.Text = data[1].Truong_004_1;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong04_2.Text = data[1].Truong_004_2;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong05_1.Text = data[1].Truong_005_1;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong05_2.Text = data[1].Truong_005_2;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong06_1.Text = data[1].Truong_006_1;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong06_2.Text = data[1].Truong_006_2;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong07.Text = data[1].Truong_007;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong08.Text = data[1].Truong_008;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong09.Text = data[1].Truong_009;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong10.Text = data[1].Truong_010;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong11_1.Text = data[1].Truong_011_1;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong11_2.Text = data[1].Truong_011_2;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong12.Text = data[1].Truong_012;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong13.Text = data[1].Truong_013;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong14.Text = data[1].Truong_014;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong15.Text = data[1].Truong_015;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong16.Text = data[1].Truong_016;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong17.Text = data[1].Truong_017;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong18_1.Text = data[1].Truong_018_1;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong18_2.Text = data[1].Truong_018_2;
            //    uC_ShowDataInput1.UC_225_2.txt_Truong18_3.Text = data[1].Truong_018_3;
            //    SetColorUC(false);
            //}
            //else if (data.Count == 1)
            //{
            //    uC_ShowDataInput1.lb_UserCheck.Text = "User1: " + data[0].UserName;
            //    uC_ShowDataInput1.lb_User1.Text = "";
            //    uC_ShowDataInput1.lb_User2.Text = "";
            //    uC_ShowDataInput1.UC_225_1.txt_Truong01.Text = data[0].Truong_001;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong02.Text = data[0].Truong_002;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong03.Text = data[0].Truong_003;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong04_1.Text = data[0].Truong_004_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong04_2.Text = data[0].Truong_004_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong05_1.Text = data[0].Truong_005_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong05_2.Text = data[0].Truong_005_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong06_1.Text = data[0].Truong_006_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong06_2.Text = data[0].Truong_006_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong07.Text = data[0].Truong_007;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong08.Text = data[0].Truong_008;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong09.Text = data[0].Truong_009;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong10.Text = data[0].Truong_010;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong11_1.Text = data[0].Truong_011_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong11_2.Text = data[0].Truong_011_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong12.Text = data[0].Truong_012;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong13.Text = data[0].Truong_013;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong14.Text = data[0].Truong_014;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong15.Text = data[0].Truong_015;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong16.Text = data[0].Truong_016;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong17.Text = data[0].Truong_017;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_1.Text = data[0].Truong_018_1;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_2.Text = data[0].Truong_018_2;
            //    uC_ShowDataInput1.UC_225_1.txt_Truong18_3.Text = data[0].Truong_018_3;
            //}
            uC_ShowDataInput1.txt_Truong_01_Check.Focus();
        }
        private void UC_Minami_Focus(string Truong, string Tag)
        {
            //
        }

        public void SetColorUC(bool FlagCheck)
        {
            if (FlagCheck)
            {
                //SoSanhDoiMau_cbb(uC_ShowDataInput1.UC_225_1.txt_Truong01, uC_ShowDataInput1.UC_225_2.txt_Truong01, uC_ShowDataInput1.UC_225_3.txt_Truong01);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong02, uC_ShowDataInput1.UC_225_2.txt_Truong02, uC_ShowDataInput1.UC_225_3.txt_Truong02);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong03, uC_ShowDataInput1.UC_225_2.txt_Truong03, uC_ShowDataInput1.UC_225_3.txt_Truong03);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong04_1, uC_ShowDataInput1.UC_225_2.txt_Truong04_1, uC_ShowDataInput1.UC_225_3.txt_Truong04_1);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong04_2, uC_ShowDataInput1.UC_225_2.txt_Truong04_2, uC_ShowDataInput1.UC_225_3.txt_Truong04_2);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong05_1, uC_ShowDataInput1.UC_225_2.txt_Truong05_1, uC_ShowDataInput1.UC_225_3.txt_Truong05_1);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong05_2, uC_ShowDataInput1.UC_225_2.txt_Truong05_2, uC_ShowDataInput1.UC_225_3.txt_Truong05_2);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong06_1, uC_ShowDataInput1.UC_225_2.txt_Truong06_1, uC_ShowDataInput1.UC_225_3.txt_Truong06_1);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong06_2, uC_ShowDataInput1.UC_225_2.txt_Truong06_2, uC_ShowDataInput1.UC_225_3.txt_Truong06_2);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong07, uC_ShowDataInput1.UC_225_2.txt_Truong07, uC_ShowDataInput1.UC_225_3.txt_Truong07);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong08, uC_ShowDataInput1.UC_225_2.txt_Truong08, uC_ShowDataInput1.UC_225_3.txt_Truong08);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong09, uC_ShowDataInput1.UC_225_2.txt_Truong09, uC_ShowDataInput1.UC_225_3.txt_Truong09);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong10, uC_ShowDataInput1.UC_225_2.txt_Truong10, uC_ShowDataInput1.UC_225_3.txt_Truong10);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong11_1, uC_ShowDataInput1.UC_225_2.txt_Truong11_1, uC_ShowDataInput1.UC_225_3.txt_Truong11_1);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong11_2, uC_ShowDataInput1.UC_225_2.txt_Truong11_2, uC_ShowDataInput1.UC_225_3.txt_Truong11_2);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong12, uC_ShowDataInput1.UC_225_2.txt_Truong12, uC_ShowDataInput1.UC_225_3.txt_Truong12);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong13, uC_ShowDataInput1.UC_225_2.txt_Truong13, uC_ShowDataInput1.UC_225_3.txt_Truong13);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong14, uC_ShowDataInput1.UC_225_2.txt_Truong14, uC_ShowDataInput1.UC_225_3.txt_Truong14);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong15, uC_ShowDataInput1.UC_225_2.txt_Truong15, uC_ShowDataInput1.UC_225_3.txt_Truong15);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong16, uC_ShowDataInput1.UC_225_2.txt_Truong16, uC_ShowDataInput1.UC_225_3.txt_Truong16);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong17, uC_ShowDataInput1.UC_225_2.txt_Truong17, uC_ShowDataInput1.UC_225_3.txt_Truong17);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong18_1, uC_ShowDataInput1.UC_225_2.txt_Truong18_1, uC_ShowDataInput1.UC_225_3.txt_Truong18_1);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong18_2, uC_ShowDataInput1.UC_225_2.txt_Truong18_2, uC_ShowDataInput1.UC_225_3.txt_Truong18_2);
                //SoSanhDoiMau(uC_ShowDataInput1.UC_225_1.txt_Truong18_3, uC_ShowDataInput1.UC_225_2.txt_Truong18_3, uC_ShowDataInput1.UC_225_3.txt_Truong18_3);
            }
            else if (!FlagCheck)
            {
                //SoSanhDoiMau_Nocheck_cbb(uC_ShowDataInput1.UC_225_2.txt_Truong01, uC_ShowDataInput1.UC_225_3.txt_Truong01);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong02, uC_ShowDataInput1.UC_225_3.txt_Truong02);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong03, uC_ShowDataInput1.UC_225_3.txt_Truong03);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong04_1, uC_ShowDataInput1.UC_225_3.txt_Truong04_1);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong04_2, uC_ShowDataInput1.UC_225_3.txt_Truong04_2);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong05_1, uC_ShowDataInput1.UC_225_3.txt_Truong05_1);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong05_2, uC_ShowDataInput1.UC_225_3.txt_Truong05_2);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong06_1, uC_ShowDataInput1.UC_225_3.txt_Truong06_1);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong06_2, uC_ShowDataInput1.UC_225_3.txt_Truong06_2);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong07, uC_ShowDataInput1.UC_225_3.txt_Truong07);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong08, uC_ShowDataInput1.UC_225_3.txt_Truong08);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong09, uC_ShowDataInput1.UC_225_3.txt_Truong09);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong10, uC_ShowDataInput1.UC_225_3.txt_Truong10);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong11_1, uC_ShowDataInput1.UC_225_3.txt_Truong11_1);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong11_2, uC_ShowDataInput1.UC_225_3.txt_Truong11_2);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong12, uC_ShowDataInput1.UC_225_3.txt_Truong12);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong13, uC_ShowDataInput1.UC_225_3.txt_Truong13);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong14, uC_ShowDataInput1.UC_225_3.txt_Truong14);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong15, uC_ShowDataInput1.UC_225_3.txt_Truong15);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong16, uC_ShowDataInput1.UC_225_3.txt_Truong16);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong17, uC_ShowDataInput1.UC_225_3.txt_Truong17);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong18_1, uC_ShowDataInput1.UC_225_3.txt_Truong18_1);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong18_2, uC_ShowDataInput1.UC_225_3.txt_Truong18_2);
                //SoSanhDoiMau_Nocheck(uC_ShowDataInput1.UC_225_2.txt_Truong18_3, uC_ShowDataInput1.UC_225_3.txt_Truong18_3);
            }
        }
        private void lb_Batch_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_Batch.Text);
            XtraMessageBox.Show("Copy batch name Success!");
        }

        private void lb_Image_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_Image.Text);
            XtraMessageBox.Show("Copy batch name Success!");
        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(splitContainerControl1.SplitterPosition + "");
        }

        private void uC_ShowImage_CityO1_Load(object sender, EventArgs e)
        {

        }
    }
}