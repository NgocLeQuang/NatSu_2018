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

namespace CLS_NatSu.MyUserControl
{
    public partial class UC_2225 : UserControl
    {
        public event AllTextChange Changed;
        public UC_2225()
        {
            InitializeComponent();
        }
        public void ResetData()
        {
            uC_2225_Item1.ResetData();
            uC_2225_Item2.ResetData();
            uC_2225_Item3.ResetData();
            uC_2225_Item4.ResetData();
            uC_2225_Item5.ResetData();
            txt_Truong_Flag.Text = "";
        }

        public bool IsEmpty()
        {
            bool empty = uC_2225_Item1.IsEmpty() &&
                         uC_2225_Item2.IsEmpty() &&
                         uC_2225_Item3.IsEmpty() &&
                         uC_2225_Item4.IsEmpty() &&
                         uC_2225_Item5.IsEmpty() &&
                         string.IsNullOrEmpty(txt_Truong_Flag.Text);
            return empty;
        }

        public void UC_2225_Load(object sender, EventArgs e)
        {
            uC_2225_Item1.UC_2225_Load(null, null);
            uC_2225_Item2.UC_2225_Load(null, null);
            uC_2225_Item3.UC_2225_Load(null, null);
            uC_2225_Item4.UC_2225_Load(null, null);
            uC_2225_Item5.UC_2225_Load(null, null);
            uC_2225_Item1.Changed += UcNatsu_Changed;
            uC_2225_Item2.Changed += UcNatsu_Changed;
            uC_2225_Item3.Changed += UcNatsu_Changed;
            uC_2225_Item4.Changed += UcNatsu_Changed;
            uC_2225_Item5.Changed += UcNatsu_Changed;
        }

        private void UcNatsu_Changed(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
        }
        public void Save2225(string BatchID,string Image,string Truong_01)
        {
            if (string.IsNullOrEmpty(BatchID) || string.IsNullOrEmpty(Image))
                return;
            //for (int i = 0; i < 4; i++)
            //{
            //    if (uC_2225_Item1.IsEmpty() & !uC_2225_Item2.IsEmpty())
            //    {
            //        uC_2225_Item1.txt_Truong_02.Text = uC_2225_Item2.txt_Truong_02.Text;
            //        uC_2225_Item1.txt_Truong_03.Text = uC_2225_Item2.txt_Truong_03.Text;
            //        uC_2225_Item1.txt_Truong_04_1.Text = uC_2225_Item2.txt_Truong_04_1.Text;
            //        uC_2225_Item1.txt_Truong_04_2.Text = uC_2225_Item2.txt_Truong_04_2.Text;
            //        uC_2225_Item1.txt_Truong_04_3.Text = uC_2225_Item2.txt_Truong_04_3.Text;
            //        uC_2225_Item1.txt_Truong_05_1.Text = uC_2225_Item2.txt_Truong_05_1.Text;
            //        uC_2225_Item1.txt_Truong_05_2.Text = uC_2225_Item2.txt_Truong_05_2.Text;
            //        uC_2225_Item1.txt_Truong_06.Text = uC_2225_Item2.txt_Truong_06.Text;
            //        uC_2225_Item1.txt_Truong_07.Text = uC_2225_Item2.txt_Truong_07.Text;
            //        uC_2225_Item1.txt_Truong_08.Text = uC_2225_Item2.txt_Truong_08.Text;
            //        uC_2225_Item1.txt_Truong_09.Text = uC_2225_Item2.txt_Truong_09.Text;
            //        uC_2225_Item1.txt_Truong_10.Text = uC_2225_Item2.txt_Truong_10.Text;
            //        uC_2225_Item1.txt_Truong_11.Text = uC_2225_Item2.txt_Truong_11.Text;
            //        uC_2225_Item1.txt_Truong_12.Text = uC_2225_Item2.txt_Truong_12.Text;
            //        uC_2225_Item1.txt_Truong_13.Text = uC_2225_Item2.txt_Truong_13.Text;
            //        uC_2225_Item1.txt_Truong_14.Text = uC_2225_Item2.txt_Truong_14.Text;
            //        uC_2225_Item1.txt_Truong_15.Text = uC_2225_Item2.txt_Truong_15.Text;
            //        uC_2225_Item1.txt_Truong_16.Text = uC_2225_Item2.txt_Truong_16.Text;
            //        uC_2225_Item1.txt_Truong_17.Text = uC_2225_Item2.txt_Truong_17.Text;
            //        uC_2225_Item1.txt_Truong_18.Text = uC_2225_Item2.txt_Truong_18.Text;
            //        uC_2225_Item1.txt_Truong_19.Text = uC_2225_Item2.txt_Truong_19.Text;
            //        uC_2225_Item1.txt_Truong_20.Text = uC_2225_Item2.txt_Truong_20.Text;
            //        uC_2225_Item1.txt_Truong_21.Text = uC_2225_Item2.txt_Truong_21.Text;
            //        uC_2225_Item1.txt_Truong_22.Text = uC_2225_Item2.txt_Truong_22.Text;
            //        uC_2225_Item1.txt_Truong_23.Text = uC_2225_Item2.txt_Truong_23.Text;
            //        uC_2225_Item1.txt_Truong_24.Text = uC_2225_Item2.txt_Truong_24.Text;
            //        uC_2225_Item1.txt_Truong_25.Text = uC_2225_Item2.txt_Truong_25.Text;
            //        uC_2225_Item1.txt_Truong_26.Text = uC_2225_Item2.txt_Truong_26.Text;

            //        uC_2225_Item2.txt_Truong_02.Text = "";
            //        uC_2225_Item2.txt_Truong_03.Text = "";
            //        uC_2225_Item2.txt_Truong_04_1.Text = "";
            //        uC_2225_Item2.txt_Truong_04_2.Text = "";
            //        uC_2225_Item2.txt_Truong_04_3.Text = "";
            //        uC_2225_Item2.txt_Truong_05_1.Text = "";
            //        uC_2225_Item2.txt_Truong_05_2.Text = "";
            //        uC_2225_Item2.txt_Truong_06.Text = "";
            //        uC_2225_Item2.txt_Truong_07.Text = "";
            //        uC_2225_Item2.txt_Truong_08.Text = "";
            //        uC_2225_Item2.txt_Truong_09.Text = "";
            //        uC_2225_Item2.txt_Truong_10.Text = "";
            //        uC_2225_Item2.txt_Truong_11.Text = "";
            //        uC_2225_Item2.txt_Truong_12.Text = "";
            //        uC_2225_Item2.txt_Truong_13.Text = "";
            //        uC_2225_Item2.txt_Truong_14.Text = "";
            //        uC_2225_Item2.txt_Truong_15.Text = "";
            //        uC_2225_Item2.txt_Truong_16.Text = "";
            //        uC_2225_Item2.txt_Truong_17.Text = "";
            //        uC_2225_Item2.txt_Truong_18.Text = "";
            //        uC_2225_Item2.txt_Truong_19.Text = "";
            //        uC_2225_Item2.txt_Truong_20.Text = "";
            //        uC_2225_Item2.txt_Truong_21.Text = "";
            //        uC_2225_Item2.txt_Truong_22.Text = "";
            //        uC_2225_Item2.txt_Truong_23.Text = "";
            //        uC_2225_Item2.txt_Truong_24.Text = "";
            //        uC_2225_Item2.txt_Truong_25.Text = "";
            //        uC_2225_Item2.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item2.IsEmpty() & !uC_2225_Item3.IsEmpty())
            //    {
            //        uC_2225_Item2.txt_Truong_02.Text = uC_2225_Item3.txt_Truong_02.Text;
            //        uC_2225_Item2.txt_Truong_03.Text = uC_2225_Item3.txt_Truong_03.Text;
            //        uC_2225_Item2.txt_Truong_04_1.Text = uC_2225_Item3.txt_Truong_04_1.Text;
            //        uC_2225_Item2.txt_Truong_04_2.Text = uC_2225_Item3.txt_Truong_04_2.Text;
            //        uC_2225_Item2.txt_Truong_04_3.Text = uC_2225_Item3.txt_Truong_04_3.Text;
            //        uC_2225_Item2.txt_Truong_05_1.Text = uC_2225_Item3.txt_Truong_05_1.Text;
            //        uC_2225_Item2.txt_Truong_05_2.Text = uC_2225_Item3.txt_Truong_05_2.Text;
            //        uC_2225_Item2.txt_Truong_06.Text = uC_2225_Item3.txt_Truong_06.Text;
            //        uC_2225_Item2.txt_Truong_07.Text = uC_2225_Item3.txt_Truong_07.Text;
            //        uC_2225_Item2.txt_Truong_08.Text = uC_2225_Item3.txt_Truong_08.Text;
            //        uC_2225_Item2.txt_Truong_09.Text = uC_2225_Item3.txt_Truong_09.Text;
            //        uC_2225_Item2.txt_Truong_10.Text = uC_2225_Item3.txt_Truong_10.Text;
            //        uC_2225_Item2.txt_Truong_11.Text = uC_2225_Item3.txt_Truong_11.Text;
            //        uC_2225_Item2.txt_Truong_12.Text = uC_2225_Item3.txt_Truong_12.Text;
            //        uC_2225_Item2.txt_Truong_13.Text = uC_2225_Item3.txt_Truong_13.Text;
            //        uC_2225_Item2.txt_Truong_14.Text = uC_2225_Item3.txt_Truong_14.Text;
            //        uC_2225_Item2.txt_Truong_15.Text = uC_2225_Item3.txt_Truong_15.Text;
            //        uC_2225_Item2.txt_Truong_16.Text = uC_2225_Item3.txt_Truong_16.Text;
            //        uC_2225_Item2.txt_Truong_17.Text = uC_2225_Item3.txt_Truong_17.Text;
            //        uC_2225_Item2.txt_Truong_18.Text = uC_2225_Item3.txt_Truong_18.Text;
            //        uC_2225_Item2.txt_Truong_19.Text = uC_2225_Item3.txt_Truong_19.Text;
            //        uC_2225_Item2.txt_Truong_20.Text = uC_2225_Item3.txt_Truong_20.Text;
            //        uC_2225_Item2.txt_Truong_21.Text = uC_2225_Item3.txt_Truong_21.Text;
            //        uC_2225_Item2.txt_Truong_22.Text = uC_2225_Item3.txt_Truong_22.Text;
            //        uC_2225_Item2.txt_Truong_23.Text = uC_2225_Item3.txt_Truong_23.Text;
            //        uC_2225_Item2.txt_Truong_24.Text = uC_2225_Item3.txt_Truong_24.Text;
            //        uC_2225_Item2.txt_Truong_25.Text = uC_2225_Item3.txt_Truong_25.Text;
            //        uC_2225_Item2.txt_Truong_26.Text = uC_2225_Item3.txt_Truong_26.Text;

            //        uC_2225_Item3.txt_Truong_02.Text = "";
            //        uC_2225_Item3.txt_Truong_03.Text = "";
            //        uC_2225_Item3.txt_Truong_04_1.Text = "";
            //        uC_2225_Item3.txt_Truong_04_2.Text = "";
            //        uC_2225_Item3.txt_Truong_04_3.Text = "";
            //        uC_2225_Item3.txt_Truong_05_1.Text = "";
            //        uC_2225_Item3.txt_Truong_05_2.Text = "";
            //        uC_2225_Item3.txt_Truong_06.Text = "";
            //        uC_2225_Item3.txt_Truong_07.Text = "";
            //        uC_2225_Item3.txt_Truong_08.Text = "";
            //        uC_2225_Item3.txt_Truong_09.Text = "";
            //        uC_2225_Item3.txt_Truong_10.Text = "";
            //        uC_2225_Item3.txt_Truong_11.Text = "";
            //        uC_2225_Item3.txt_Truong_12.Text = "";
            //        uC_2225_Item3.txt_Truong_13.Text = "";
            //        uC_2225_Item3.txt_Truong_14.Text = "";
            //        uC_2225_Item3.txt_Truong_15.Text = "";
            //        uC_2225_Item3.txt_Truong_16.Text = "";
            //        uC_2225_Item3.txt_Truong_17.Text = "";
            //        uC_2225_Item3.txt_Truong_18.Text = "";
            //        uC_2225_Item3.txt_Truong_19.Text = "";
            //        uC_2225_Item3.txt_Truong_20.Text = "";
            //        uC_2225_Item3.txt_Truong_21.Text = "";
            //        uC_2225_Item3.txt_Truong_22.Text = "";
            //        uC_2225_Item3.txt_Truong_23.Text = "";
            //        uC_2225_Item3.txt_Truong_24.Text = "";
            //        uC_2225_Item3.txt_Truong_25.Text = "";
            //        uC_2225_Item3.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item3.IsEmpty() & !uC_2225_Item4.IsEmpty())
            //    {
            //        uC_2225_Item3.txt_Truong_02.Text = uC_2225_Item4.txt_Truong_02.Text;
            //        uC_2225_Item3.txt_Truong_03.Text = uC_2225_Item4.txt_Truong_03.Text;
            //        uC_2225_Item3.txt_Truong_04_1.Text = uC_2225_Item4.txt_Truong_04_1.Text;
            //        uC_2225_Item3.txt_Truong_04_2.Text = uC_2225_Item4.txt_Truong_04_2.Text;
            //        uC_2225_Item3.txt_Truong_04_3.Text = uC_2225_Item4.txt_Truong_04_3.Text;
            //        uC_2225_Item3.txt_Truong_05_1.Text = uC_2225_Item4.txt_Truong_05_1.Text;
            //        uC_2225_Item3.txt_Truong_05_2.Text = uC_2225_Item4.txt_Truong_05_2.Text;
            //        uC_2225_Item3.txt_Truong_06.Text = uC_2225_Item4.txt_Truong_06.Text;
            //        uC_2225_Item3.txt_Truong_07.Text = uC_2225_Item4.txt_Truong_07.Text;
            //        uC_2225_Item3.txt_Truong_08.Text = uC_2225_Item4.txt_Truong_08.Text;
            //        uC_2225_Item3.txt_Truong_09.Text = uC_2225_Item4.txt_Truong_09.Text;
            //        uC_2225_Item3.txt_Truong_10.Text = uC_2225_Item4.txt_Truong_10.Text;
            //        uC_2225_Item3.txt_Truong_11.Text = uC_2225_Item4.txt_Truong_11.Text;
            //        uC_2225_Item3.txt_Truong_12.Text = uC_2225_Item4.txt_Truong_12.Text;
            //        uC_2225_Item3.txt_Truong_13.Text = uC_2225_Item4.txt_Truong_13.Text;
            //        uC_2225_Item3.txt_Truong_14.Text = uC_2225_Item4.txt_Truong_14.Text;
            //        uC_2225_Item3.txt_Truong_15.Text = uC_2225_Item4.txt_Truong_15.Text;
            //        uC_2225_Item3.txt_Truong_16.Text = uC_2225_Item4.txt_Truong_16.Text;
            //        uC_2225_Item3.txt_Truong_17.Text = uC_2225_Item4.txt_Truong_17.Text;
            //        uC_2225_Item3.txt_Truong_18.Text = uC_2225_Item4.txt_Truong_18.Text;
            //        uC_2225_Item3.txt_Truong_19.Text = uC_2225_Item4.txt_Truong_19.Text;
            //        uC_2225_Item3.txt_Truong_20.Text = uC_2225_Item4.txt_Truong_20.Text;
            //        uC_2225_Item3.txt_Truong_21.Text = uC_2225_Item4.txt_Truong_21.Text;
            //        uC_2225_Item3.txt_Truong_22.Text = uC_2225_Item4.txt_Truong_22.Text;
            //        uC_2225_Item3.txt_Truong_23.Text = uC_2225_Item4.txt_Truong_23.Text;
            //        uC_2225_Item3.txt_Truong_24.Text = uC_2225_Item4.txt_Truong_24.Text;
            //        uC_2225_Item3.txt_Truong_25.Text = uC_2225_Item4.txt_Truong_25.Text;
            //        uC_2225_Item3.txt_Truong_26.Text = uC_2225_Item4.txt_Truong_26.Text;

            //        uC_2225_Item4.txt_Truong_02.Text = "";
            //        uC_2225_Item4.txt_Truong_03.Text = "";
            //        uC_2225_Item4.txt_Truong_04_1.Text = "";
            //        uC_2225_Item4.txt_Truong_04_2.Text = "";
            //        uC_2225_Item4.txt_Truong_04_3.Text = "";
            //        uC_2225_Item4.txt_Truong_05_1.Text = "";
            //        uC_2225_Item4.txt_Truong_05_2.Text = "";
            //        uC_2225_Item4.txt_Truong_06.Text = "";
            //        uC_2225_Item4.txt_Truong_07.Text = "";
            //        uC_2225_Item4.txt_Truong_08.Text = "";
            //        uC_2225_Item4.txt_Truong_09.Text = "";
            //        uC_2225_Item4.txt_Truong_10.Text = "";
            //        uC_2225_Item4.txt_Truong_11.Text = "";
            //        uC_2225_Item4.txt_Truong_12.Text = "";
            //        uC_2225_Item4.txt_Truong_13.Text = "";
            //        uC_2225_Item4.txt_Truong_14.Text = "";
            //        uC_2225_Item4.txt_Truong_15.Text = "";
            //        uC_2225_Item4.txt_Truong_16.Text = "";
            //        uC_2225_Item4.txt_Truong_17.Text = "";
            //        uC_2225_Item4.txt_Truong_18.Text = "";
            //        uC_2225_Item4.txt_Truong_19.Text = "";
            //        uC_2225_Item4.txt_Truong_20.Text = "";
            //        uC_2225_Item4.txt_Truong_21.Text = "";
            //        uC_2225_Item4.txt_Truong_22.Text = "";
            //        uC_2225_Item4.txt_Truong_23.Text = "";
            //        uC_2225_Item4.txt_Truong_24.Text = "";
            //        uC_2225_Item4.txt_Truong_25.Text = "";
            //        uC_2225_Item4.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item4.IsEmpty() & !uC_2225_Item5.IsEmpty())
            //    {
            //        uC_2225_Item4.txt_Truong_02.Text = uC_2225_Item5.txt_Truong_02.Text;
            //        uC_2225_Item4.txt_Truong_03.Text = uC_2225_Item5.txt_Truong_03.Text;
            //        uC_2225_Item4.txt_Truong_04_1.Text = uC_2225_Item5.txt_Truong_04_1.Text;
            //        uC_2225_Item4.txt_Truong_04_2.Text = uC_2225_Item5.txt_Truong_04_2.Text;
            //        uC_2225_Item4.txt_Truong_04_3.Text = uC_2225_Item5.txt_Truong_04_3.Text;
            //        uC_2225_Item4.txt_Truong_05_1.Text = uC_2225_Item5.txt_Truong_05_1.Text;
            //        uC_2225_Item4.txt_Truong_05_2.Text = uC_2225_Item5.txt_Truong_05_2.Text;
            //        uC_2225_Item4.txt_Truong_06.Text = uC_2225_Item5.txt_Truong_06.Text;
            //        uC_2225_Item4.txt_Truong_07.Text = uC_2225_Item5.txt_Truong_07.Text;
            //        uC_2225_Item4.txt_Truong_08.Text = uC_2225_Item5.txt_Truong_08.Text;
            //        uC_2225_Item4.txt_Truong_09.Text = uC_2225_Item5.txt_Truong_09.Text;
            //        uC_2225_Item4.txt_Truong_10.Text = uC_2225_Item5.txt_Truong_10.Text;
            //        uC_2225_Item4.txt_Truong_11.Text = uC_2225_Item5.txt_Truong_11.Text;
            //        uC_2225_Item4.txt_Truong_12.Text = uC_2225_Item5.txt_Truong_12.Text;
            //        uC_2225_Item4.txt_Truong_13.Text = uC_2225_Item5.txt_Truong_13.Text;
            //        uC_2225_Item4.txt_Truong_14.Text = uC_2225_Item5.txt_Truong_14.Text;
            //        uC_2225_Item4.txt_Truong_15.Text = uC_2225_Item5.txt_Truong_15.Text;
            //        uC_2225_Item4.txt_Truong_16.Text = uC_2225_Item5.txt_Truong_16.Text;
            //        uC_2225_Item4.txt_Truong_17.Text = uC_2225_Item5.txt_Truong_17.Text;
            //        uC_2225_Item4.txt_Truong_18.Text = uC_2225_Item5.txt_Truong_18.Text;
            //        uC_2225_Item4.txt_Truong_19.Text = uC_2225_Item5.txt_Truong_19.Text;
            //        uC_2225_Item4.txt_Truong_20.Text = uC_2225_Item5.txt_Truong_20.Text;
            //        uC_2225_Item4.txt_Truong_21.Text = uC_2225_Item5.txt_Truong_21.Text;
            //        uC_2225_Item4.txt_Truong_22.Text = uC_2225_Item5.txt_Truong_22.Text;
            //        uC_2225_Item4.txt_Truong_23.Text = uC_2225_Item5.txt_Truong_23.Text;
            //        uC_2225_Item4.txt_Truong_24.Text = uC_2225_Item5.txt_Truong_24.Text;
            //        uC_2225_Item4.txt_Truong_25.Text = uC_2225_Item5.txt_Truong_25.Text;
            //        uC_2225_Item4.txt_Truong_26.Text = uC_2225_Item5.txt_Truong_26.Text;

            //        uC_2225_Item5.txt_Truong_02.Text = "";
            //        uC_2225_Item5.txt_Truong_03.Text = "";
            //        uC_2225_Item5.txt_Truong_04_1.Text = "";
            //        uC_2225_Item5.txt_Truong_04_2.Text = "";
            //        uC_2225_Item5.txt_Truong_04_3.Text = "";
            //        uC_2225_Item5.txt_Truong_05_1.Text = "";
            //        uC_2225_Item5.txt_Truong_05_2.Text = "";
            //        uC_2225_Item5.txt_Truong_06.Text = "";
            //        uC_2225_Item5.txt_Truong_07.Text = "";
            //        uC_2225_Item5.txt_Truong_08.Text = "";
            //        uC_2225_Item5.txt_Truong_09.Text = "";
            //        uC_2225_Item5.txt_Truong_10.Text = "";
            //        uC_2225_Item5.txt_Truong_11.Text = "";
            //        uC_2225_Item5.txt_Truong_12.Text = "";
            //        uC_2225_Item5.txt_Truong_13.Text = "";
            //        uC_2225_Item5.txt_Truong_14.Text = "";
            //        uC_2225_Item5.txt_Truong_15.Text = "";
            //        uC_2225_Item5.txt_Truong_16.Text = "";
            //        uC_2225_Item5.txt_Truong_17.Text = "";
            //        uC_2225_Item5.txt_Truong_18.Text = "";
            //        uC_2225_Item5.txt_Truong_19.Text = "";
            //        uC_2225_Item5.txt_Truong_20.Text = "";
            //        uC_2225_Item5.txt_Truong_21.Text = "";
            //        uC_2225_Item5.txt_Truong_22.Text = "";
            //        uC_2225_Item5.txt_Truong_23.Text = "";
            //        uC_2225_Item5.txt_Truong_24.Text = "";
            //        uC_2225_Item5.txt_Truong_25.Text = "";
            //        uC_2225_Item5.txt_Truong_26.Text = "";
            //    }
            //}

            Global.Db.Insert_DESo(BatchID, Image, Global.StrUserName,
                Truong_01,
                uC_2225_Item1.txt_Truong_02.Text,
                uC_2225_Item1.txt_Truong_03.Text,
                uC_2225_Item1.txt_Truong_04_1.Text,
                uC_2225_Item1.txt_Truong_04_2.Text,
                uC_2225_Item1.txt_Truong_04_3.Text,
                uC_2225_Item1.txt_Truong_05_1.Text,
                uC_2225_Item1.txt_Truong_05_2.Text,
                uC_2225_Item1.txt_Truong_06.Text,
                uC_2225_Item1.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_08.Text,
                uC_2225_Item1.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_13.Text,
                uC_2225_Item1.txt_Truong_14.Text,
                uC_2225_Item1.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_19.Text,
                uC_2225_Item1.txt_Truong_20.Text,
                uC_2225_Item1.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_25.Text,
                uC_2225_Item1.txt_Truong_26.Text,

                uC_2225_Item2.txt_Truong_02.Text,
                uC_2225_Item2.txt_Truong_03.Text,
                uC_2225_Item2.txt_Truong_04_1.Text,
                uC_2225_Item2.txt_Truong_04_2.Text,
                uC_2225_Item2.txt_Truong_04_3.Text,
                uC_2225_Item2.txt_Truong_05_1.Text,
                uC_2225_Item2.txt_Truong_05_2.Text,
                uC_2225_Item2.txt_Truong_06.Text,
                uC_2225_Item2.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_08.Text,
                uC_2225_Item2.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_13.Text,
                uC_2225_Item2.txt_Truong_14.Text,
                uC_2225_Item2.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_19.Text,
                uC_2225_Item2.txt_Truong_20.Text,
                uC_2225_Item2.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_25.Text,
                uC_2225_Item2.txt_Truong_26.Text,

                uC_2225_Item3.txt_Truong_02.Text,
                uC_2225_Item3.txt_Truong_03.Text,
                uC_2225_Item3.txt_Truong_04_1.Text,
                uC_2225_Item3.txt_Truong_04_2.Text,
                uC_2225_Item3.txt_Truong_04_3.Text,
                uC_2225_Item3.txt_Truong_05_1.Text,
                uC_2225_Item3.txt_Truong_05_2.Text,
                uC_2225_Item3.txt_Truong_06.Text,
                uC_2225_Item3.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_08.Text,
                uC_2225_Item3.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_13.Text,
                uC_2225_Item3.txt_Truong_14.Text,
                uC_2225_Item3.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_19.Text,
                uC_2225_Item3.txt_Truong_20.Text,
                uC_2225_Item3.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_25.Text,
                uC_2225_Item3.txt_Truong_26.Text,

                uC_2225_Item4.txt_Truong_02.Text,
                uC_2225_Item4.txt_Truong_03.Text,
                uC_2225_Item4.txt_Truong_04_1.Text,
                uC_2225_Item4.txt_Truong_04_2.Text,
                uC_2225_Item4.txt_Truong_04_3.Text,
                uC_2225_Item4.txt_Truong_05_1.Text,
                uC_2225_Item4.txt_Truong_05_2.Text,
                uC_2225_Item4.txt_Truong_06.Text,
                uC_2225_Item4.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_08.Text,
                uC_2225_Item4.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_13.Text,
                uC_2225_Item4.txt_Truong_14.Text,
                uC_2225_Item4.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_19.Text,
                uC_2225_Item4.txt_Truong_20.Text,
                uC_2225_Item4.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_25.Text,
                uC_2225_Item4.txt_Truong_26.Text,

                uC_2225_Item5.txt_Truong_02.Text,
                uC_2225_Item5.txt_Truong_03.Text,
                uC_2225_Item5.txt_Truong_04_1.Text,
                uC_2225_Item5.txt_Truong_04_2.Text,
                uC_2225_Item5.txt_Truong_04_3.Text,
                uC_2225_Item5.txt_Truong_05_1.Text,
                uC_2225_Item5.txt_Truong_05_2.Text,
                uC_2225_Item5.txt_Truong_06.Text,
                uC_2225_Item5.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_08.Text,
                uC_2225_Item5.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_13.Text,
                uC_2225_Item5.txt_Truong_14.Text,
                uC_2225_Item5.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_19.Text,
                uC_2225_Item5.txt_Truong_20.Text,
                uC_2225_Item5.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_25.Text,
                uC_2225_Item5.txt_Truong_26.Text,
                txt_Truong_Flag.Text
                );
        }
        
        public void Save2225_Check(string BatchID, string Image, string Truong_01,
                                string User1, bool flag_Phieu1_User1Sai, bool flag_Phieu2_User1Sai, bool flag_Phieu3_User1Sai, bool flag_Phieu4_User1Sai, bool flag_Phieu5_User1Sai,
                                string User2, bool flag_Phieu1_User2Sai, bool flag_Phieu2_User2Sai, bool flag_Phieu3_User2Sai, bool flag_Phieu4_User2Sai, bool flag_Phieu5_User2Sai)
        {
            if (string.IsNullOrEmpty(BatchID) || string.IsNullOrEmpty(Image))
                return;
            //for (int i = 0; i < 4; i++)
            //{
            //    if (uC_2225_Item1.IsEmpty() & !uC_2225_Item2.IsEmpty())
            //    {
            //        uC_2225_Item1.txt_Truong_02.Text = uC_2225_Item2.txt_Truong_02.Text;
            //        uC_2225_Item1.txt_Truong_03.Text = uC_2225_Item2.txt_Truong_03.Text;
            //        uC_2225_Item1.txt_Truong_04_1.Text = uC_2225_Item2.txt_Truong_04_1.Text;
            //        uC_2225_Item1.txt_Truong_04_2.Text = uC_2225_Item2.txt_Truong_04_2.Text;
            //        uC_2225_Item1.txt_Truong_04_3.Text = uC_2225_Item2.txt_Truong_04_3.Text;
            //        uC_2225_Item1.txt_Truong_05_1.Text = uC_2225_Item2.txt_Truong_05_1.Text;
            //        uC_2225_Item1.txt_Truong_05_2.Text = uC_2225_Item2.txt_Truong_05_2.Text;
            //        uC_2225_Item1.txt_Truong_06.Text = uC_2225_Item2.txt_Truong_06.Text;
            //        uC_2225_Item1.txt_Truong_07.Text = uC_2225_Item2.txt_Truong_07.Text;
            //        uC_2225_Item1.txt_Truong_08.Text = uC_2225_Item2.txt_Truong_08.Text;
            //        uC_2225_Item1.txt_Truong_09.Text = uC_2225_Item2.txt_Truong_09.Text;
            //        uC_2225_Item1.txt_Truong_10.Text = uC_2225_Item2.txt_Truong_10.Text;
            //        uC_2225_Item1.txt_Truong_11.Text = uC_2225_Item2.txt_Truong_11.Text;
            //        uC_2225_Item1.txt_Truong_12.Text = uC_2225_Item2.txt_Truong_12.Text;
            //        uC_2225_Item1.txt_Truong_13.Text = uC_2225_Item2.txt_Truong_13.Text;
            //        uC_2225_Item1.txt_Truong_14.Text = uC_2225_Item2.txt_Truong_14.Text;
            //        uC_2225_Item1.txt_Truong_15.Text = uC_2225_Item2.txt_Truong_15.Text;
            //        uC_2225_Item1.txt_Truong_16.Text = uC_2225_Item2.txt_Truong_16.Text;
            //        uC_2225_Item1.txt_Truong_17.Text = uC_2225_Item2.txt_Truong_17.Text;
            //        uC_2225_Item1.txt_Truong_18.Text = uC_2225_Item2.txt_Truong_18.Text;
            //        uC_2225_Item1.txt_Truong_19.Text = uC_2225_Item2.txt_Truong_19.Text;
            //        uC_2225_Item1.txt_Truong_20.Text = uC_2225_Item2.txt_Truong_20.Text;
            //        uC_2225_Item1.txt_Truong_21.Text = uC_2225_Item2.txt_Truong_21.Text;
            //        uC_2225_Item1.txt_Truong_22.Text = uC_2225_Item2.txt_Truong_22.Text;
            //        uC_2225_Item1.txt_Truong_23.Text = uC_2225_Item2.txt_Truong_23.Text;
            //        uC_2225_Item1.txt_Truong_24.Text = uC_2225_Item2.txt_Truong_24.Text;
            //        uC_2225_Item1.txt_Truong_25.Text = uC_2225_Item2.txt_Truong_25.Text;
            //        uC_2225_Item1.txt_Truong_26.Text = uC_2225_Item2.txt_Truong_26.Text;

            //        uC_2225_Item2.txt_Truong_02.Text = "";
            //        uC_2225_Item2.txt_Truong_03.Text = "";
            //        uC_2225_Item2.txt_Truong_04_1.Text = "";
            //        uC_2225_Item2.txt_Truong_04_2.Text = "";
            //        uC_2225_Item2.txt_Truong_04_3.Text = "";
            //        uC_2225_Item2.txt_Truong_05_1.Text = "";
            //        uC_2225_Item2.txt_Truong_05_2.Text = "";
            //        uC_2225_Item2.txt_Truong_06.Text = "";
            //        uC_2225_Item2.txt_Truong_07.Text = "";
            //        uC_2225_Item2.txt_Truong_08.Text = "";
            //        uC_2225_Item2.txt_Truong_09.Text = "";
            //        uC_2225_Item2.txt_Truong_10.Text = "";
            //        uC_2225_Item2.txt_Truong_11.Text = "";
            //        uC_2225_Item2.txt_Truong_12.Text = "";
            //        uC_2225_Item2.txt_Truong_13.Text = "";
            //        uC_2225_Item2.txt_Truong_14.Text = "";
            //        uC_2225_Item2.txt_Truong_15.Text = "";
            //        uC_2225_Item2.txt_Truong_16.Text = "";
            //        uC_2225_Item2.txt_Truong_17.Text = "";
            //        uC_2225_Item2.txt_Truong_18.Text = "";
            //        uC_2225_Item2.txt_Truong_19.Text = "";
            //        uC_2225_Item2.txt_Truong_20.Text = "";
            //        uC_2225_Item2.txt_Truong_21.Text = "";
            //        uC_2225_Item2.txt_Truong_22.Text = "";
            //        uC_2225_Item2.txt_Truong_23.Text = "";
            //        uC_2225_Item2.txt_Truong_24.Text = "";
            //        uC_2225_Item2.txt_Truong_25.Text = "";
            //        uC_2225_Item2.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item2.IsEmpty() & !uC_2225_Item3.IsEmpty())
            //    {
            //        uC_2225_Item2.txt_Truong_02.Text = uC_2225_Item3.txt_Truong_02.Text;
            //        uC_2225_Item2.txt_Truong_03.Text = uC_2225_Item3.txt_Truong_03.Text;
            //        uC_2225_Item2.txt_Truong_04_1.Text = uC_2225_Item3.txt_Truong_04_1.Text;
            //        uC_2225_Item2.txt_Truong_04_2.Text = uC_2225_Item3.txt_Truong_04_2.Text;
            //        uC_2225_Item2.txt_Truong_04_3.Text = uC_2225_Item3.txt_Truong_04_3.Text;
            //        uC_2225_Item2.txt_Truong_05_1.Text = uC_2225_Item3.txt_Truong_05_1.Text;
            //        uC_2225_Item2.txt_Truong_05_2.Text = uC_2225_Item3.txt_Truong_05_2.Text;
            //        uC_2225_Item2.txt_Truong_06.Text = uC_2225_Item3.txt_Truong_06.Text;
            //        uC_2225_Item2.txt_Truong_07.Text = uC_2225_Item3.txt_Truong_07.Text;
            //        uC_2225_Item2.txt_Truong_08.Text = uC_2225_Item3.txt_Truong_08.Text;
            //        uC_2225_Item2.txt_Truong_09.Text = uC_2225_Item3.txt_Truong_09.Text;
            //        uC_2225_Item2.txt_Truong_10.Text = uC_2225_Item3.txt_Truong_10.Text;
            //        uC_2225_Item2.txt_Truong_11.Text = uC_2225_Item3.txt_Truong_11.Text;
            //        uC_2225_Item2.txt_Truong_12.Text = uC_2225_Item3.txt_Truong_12.Text;
            //        uC_2225_Item2.txt_Truong_13.Text = uC_2225_Item3.txt_Truong_13.Text;
            //        uC_2225_Item2.txt_Truong_14.Text = uC_2225_Item3.txt_Truong_14.Text;
            //        uC_2225_Item2.txt_Truong_15.Text = uC_2225_Item3.txt_Truong_15.Text;
            //        uC_2225_Item2.txt_Truong_16.Text = uC_2225_Item3.txt_Truong_16.Text;
            //        uC_2225_Item2.txt_Truong_17.Text = uC_2225_Item3.txt_Truong_17.Text;
            //        uC_2225_Item2.txt_Truong_18.Text = uC_2225_Item3.txt_Truong_18.Text;
            //        uC_2225_Item2.txt_Truong_19.Text = uC_2225_Item3.txt_Truong_19.Text;
            //        uC_2225_Item2.txt_Truong_20.Text = uC_2225_Item3.txt_Truong_20.Text;
            //        uC_2225_Item2.txt_Truong_21.Text = uC_2225_Item3.txt_Truong_21.Text;
            //        uC_2225_Item2.txt_Truong_22.Text = uC_2225_Item3.txt_Truong_22.Text;
            //        uC_2225_Item2.txt_Truong_23.Text = uC_2225_Item3.txt_Truong_23.Text;
            //        uC_2225_Item2.txt_Truong_24.Text = uC_2225_Item3.txt_Truong_24.Text;
            //        uC_2225_Item2.txt_Truong_25.Text = uC_2225_Item3.txt_Truong_25.Text;
            //        uC_2225_Item2.txt_Truong_26.Text = uC_2225_Item3.txt_Truong_26.Text;

            //        uC_2225_Item3.txt_Truong_02.Text = "";
            //        uC_2225_Item3.txt_Truong_03.Text = "";
            //        uC_2225_Item3.txt_Truong_04_1.Text = "";
            //        uC_2225_Item3.txt_Truong_04_2.Text = "";
            //        uC_2225_Item3.txt_Truong_04_3.Text = "";
            //        uC_2225_Item3.txt_Truong_05_1.Text = "";
            //        uC_2225_Item3.txt_Truong_05_2.Text = "";
            //        uC_2225_Item3.txt_Truong_06.Text = "";
            //        uC_2225_Item3.txt_Truong_07.Text = "";
            //        uC_2225_Item3.txt_Truong_08.Text = "";
            //        uC_2225_Item3.txt_Truong_09.Text = "";
            //        uC_2225_Item3.txt_Truong_10.Text = "";
            //        uC_2225_Item3.txt_Truong_11.Text = "";
            //        uC_2225_Item3.txt_Truong_12.Text = "";
            //        uC_2225_Item3.txt_Truong_13.Text = "";
            //        uC_2225_Item3.txt_Truong_14.Text = "";
            //        uC_2225_Item3.txt_Truong_15.Text = "";
            //        uC_2225_Item3.txt_Truong_16.Text = "";
            //        uC_2225_Item3.txt_Truong_17.Text = "";
            //        uC_2225_Item3.txt_Truong_18.Text = "";
            //        uC_2225_Item3.txt_Truong_19.Text = "";
            //        uC_2225_Item3.txt_Truong_20.Text = "";
            //        uC_2225_Item3.txt_Truong_21.Text = "";
            //        uC_2225_Item3.txt_Truong_22.Text = "";
            //        uC_2225_Item3.txt_Truong_23.Text = "";
            //        uC_2225_Item3.txt_Truong_24.Text = "";
            //        uC_2225_Item3.txt_Truong_25.Text = "";
            //        uC_2225_Item3.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item3.IsEmpty() & !uC_2225_Item4.IsEmpty())
            //    {
            //        uC_2225_Item3.txt_Truong_02.Text = uC_2225_Item4.txt_Truong_02.Text;
            //        uC_2225_Item3.txt_Truong_03.Text = uC_2225_Item4.txt_Truong_03.Text;
            //        uC_2225_Item3.txt_Truong_04_1.Text = uC_2225_Item4.txt_Truong_04_1.Text;
            //        uC_2225_Item3.txt_Truong_04_2.Text = uC_2225_Item4.txt_Truong_04_2.Text;
            //        uC_2225_Item3.txt_Truong_04_3.Text = uC_2225_Item4.txt_Truong_04_3.Text;
            //        uC_2225_Item3.txt_Truong_05_1.Text = uC_2225_Item4.txt_Truong_05_1.Text;
            //        uC_2225_Item3.txt_Truong_05_2.Text = uC_2225_Item4.txt_Truong_05_2.Text;
            //        uC_2225_Item3.txt_Truong_06.Text = uC_2225_Item4.txt_Truong_06.Text;
            //        uC_2225_Item3.txt_Truong_07.Text = uC_2225_Item4.txt_Truong_07.Text;
            //        uC_2225_Item3.txt_Truong_08.Text = uC_2225_Item4.txt_Truong_08.Text;
            //        uC_2225_Item3.txt_Truong_09.Text = uC_2225_Item4.txt_Truong_09.Text;
            //        uC_2225_Item3.txt_Truong_10.Text = uC_2225_Item4.txt_Truong_10.Text;
            //        uC_2225_Item3.txt_Truong_11.Text = uC_2225_Item4.txt_Truong_11.Text;
            //        uC_2225_Item3.txt_Truong_12.Text = uC_2225_Item4.txt_Truong_12.Text;
            //        uC_2225_Item3.txt_Truong_13.Text = uC_2225_Item4.txt_Truong_13.Text;
            //        uC_2225_Item3.txt_Truong_14.Text = uC_2225_Item4.txt_Truong_14.Text;
            //        uC_2225_Item3.txt_Truong_15.Text = uC_2225_Item4.txt_Truong_15.Text;
            //        uC_2225_Item3.txt_Truong_16.Text = uC_2225_Item4.txt_Truong_16.Text;
            //        uC_2225_Item3.txt_Truong_17.Text = uC_2225_Item4.txt_Truong_17.Text;
            //        uC_2225_Item3.txt_Truong_18.Text = uC_2225_Item4.txt_Truong_18.Text;
            //        uC_2225_Item3.txt_Truong_19.Text = uC_2225_Item4.txt_Truong_19.Text;
            //        uC_2225_Item3.txt_Truong_20.Text = uC_2225_Item4.txt_Truong_20.Text;
            //        uC_2225_Item3.txt_Truong_21.Text = uC_2225_Item4.txt_Truong_21.Text;
            //        uC_2225_Item3.txt_Truong_22.Text = uC_2225_Item4.txt_Truong_22.Text;
            //        uC_2225_Item3.txt_Truong_23.Text = uC_2225_Item4.txt_Truong_23.Text;
            //        uC_2225_Item3.txt_Truong_24.Text = uC_2225_Item4.txt_Truong_24.Text;
            //        uC_2225_Item3.txt_Truong_25.Text = uC_2225_Item4.txt_Truong_25.Text;
            //        uC_2225_Item3.txt_Truong_26.Text = uC_2225_Item4.txt_Truong_26.Text;

            //        uC_2225_Item4.txt_Truong_02.Text = "";
            //        uC_2225_Item4.txt_Truong_03.Text = "";
            //        uC_2225_Item4.txt_Truong_04_1.Text = "";
            //        uC_2225_Item4.txt_Truong_04_2.Text = "";
            //        uC_2225_Item4.txt_Truong_04_3.Text = "";
            //        uC_2225_Item4.txt_Truong_05_1.Text = "";
            //        uC_2225_Item4.txt_Truong_05_2.Text = "";
            //        uC_2225_Item4.txt_Truong_06.Text = "";
            //        uC_2225_Item4.txt_Truong_07.Text = "";
            //        uC_2225_Item4.txt_Truong_08.Text = "";
            //        uC_2225_Item4.txt_Truong_09.Text = "";
            //        uC_2225_Item4.txt_Truong_10.Text = "";
            //        uC_2225_Item4.txt_Truong_11.Text = "";
            //        uC_2225_Item4.txt_Truong_12.Text = "";
            //        uC_2225_Item4.txt_Truong_13.Text = "";
            //        uC_2225_Item4.txt_Truong_14.Text = "";
            //        uC_2225_Item4.txt_Truong_15.Text = "";
            //        uC_2225_Item4.txt_Truong_16.Text = "";
            //        uC_2225_Item4.txt_Truong_17.Text = "";
            //        uC_2225_Item4.txt_Truong_18.Text = "";
            //        uC_2225_Item4.txt_Truong_19.Text = "";
            //        uC_2225_Item4.txt_Truong_20.Text = "";
            //        uC_2225_Item4.txt_Truong_21.Text = "";
            //        uC_2225_Item4.txt_Truong_22.Text = "";
            //        uC_2225_Item4.txt_Truong_23.Text = "";
            //        uC_2225_Item4.txt_Truong_24.Text = "";
            //        uC_2225_Item4.txt_Truong_25.Text = "";
            //        uC_2225_Item4.txt_Truong_26.Text = "";
            //    }
            //    if (uC_2225_Item4.IsEmpty() & !uC_2225_Item5.IsEmpty())
            //    {
            //        uC_2225_Item4.txt_Truong_02.Text = uC_2225_Item5.txt_Truong_02.Text;
            //        uC_2225_Item4.txt_Truong_03.Text = uC_2225_Item5.txt_Truong_03.Text;
            //        uC_2225_Item4.txt_Truong_04_1.Text = uC_2225_Item5.txt_Truong_04_1.Text;
            //        uC_2225_Item4.txt_Truong_04_2.Text = uC_2225_Item5.txt_Truong_04_2.Text;
            //        uC_2225_Item4.txt_Truong_04_3.Text = uC_2225_Item5.txt_Truong_04_3.Text;
            //        uC_2225_Item4.txt_Truong_05_1.Text = uC_2225_Item5.txt_Truong_05_1.Text;
            //        uC_2225_Item4.txt_Truong_05_2.Text = uC_2225_Item5.txt_Truong_05_2.Text;
            //        uC_2225_Item4.txt_Truong_06.Text = uC_2225_Item5.txt_Truong_06.Text;
            //        uC_2225_Item4.txt_Truong_07.Text = uC_2225_Item5.txt_Truong_07.Text;
            //        uC_2225_Item4.txt_Truong_08.Text = uC_2225_Item5.txt_Truong_08.Text;
            //        uC_2225_Item4.txt_Truong_09.Text = uC_2225_Item5.txt_Truong_09.Text;
            //        uC_2225_Item4.txt_Truong_10.Text = uC_2225_Item5.txt_Truong_10.Text;
            //        uC_2225_Item4.txt_Truong_11.Text = uC_2225_Item5.txt_Truong_11.Text;
            //        uC_2225_Item4.txt_Truong_12.Text = uC_2225_Item5.txt_Truong_12.Text;
            //        uC_2225_Item4.txt_Truong_13.Text = uC_2225_Item5.txt_Truong_13.Text;
            //        uC_2225_Item4.txt_Truong_14.Text = uC_2225_Item5.txt_Truong_14.Text;
            //        uC_2225_Item4.txt_Truong_15.Text = uC_2225_Item5.txt_Truong_15.Text;
            //        uC_2225_Item4.txt_Truong_16.Text = uC_2225_Item5.txt_Truong_16.Text;
            //        uC_2225_Item4.txt_Truong_17.Text = uC_2225_Item5.txt_Truong_17.Text;
            //        uC_2225_Item4.txt_Truong_18.Text = uC_2225_Item5.txt_Truong_18.Text;
            //        uC_2225_Item4.txt_Truong_19.Text = uC_2225_Item5.txt_Truong_19.Text;
            //        uC_2225_Item4.txt_Truong_20.Text = uC_2225_Item5.txt_Truong_20.Text;
            //        uC_2225_Item4.txt_Truong_21.Text = uC_2225_Item5.txt_Truong_21.Text;
            //        uC_2225_Item4.txt_Truong_22.Text = uC_2225_Item5.txt_Truong_22.Text;
            //        uC_2225_Item4.txt_Truong_23.Text = uC_2225_Item5.txt_Truong_23.Text;
            //        uC_2225_Item4.txt_Truong_24.Text = uC_2225_Item5.txt_Truong_24.Text;
            //        uC_2225_Item4.txt_Truong_25.Text = uC_2225_Item5.txt_Truong_25.Text;
            //        uC_2225_Item4.txt_Truong_26.Text = uC_2225_Item5.txt_Truong_26.Text;

            //        uC_2225_Item5.txt_Truong_02.Text = "";
            //        uC_2225_Item5.txt_Truong_03.Text = "";
            //        uC_2225_Item5.txt_Truong_04_1.Text = "";
            //        uC_2225_Item5.txt_Truong_04_2.Text = "";
            //        uC_2225_Item5.txt_Truong_04_3.Text = "";
            //        uC_2225_Item5.txt_Truong_05_1.Text = "";
            //        uC_2225_Item5.txt_Truong_05_2.Text = "";
            //        uC_2225_Item5.txt_Truong_06.Text = "";
            //        uC_2225_Item5.txt_Truong_07.Text = "";
            //        uC_2225_Item5.txt_Truong_08.Text = "";
            //        uC_2225_Item5.txt_Truong_09.Text = "";
            //        uC_2225_Item5.txt_Truong_10.Text = "";
            //        uC_2225_Item5.txt_Truong_11.Text = "";
            //        uC_2225_Item5.txt_Truong_12.Text = "";
            //        uC_2225_Item5.txt_Truong_13.Text = "";
            //        uC_2225_Item5.txt_Truong_14.Text = "";
            //        uC_2225_Item5.txt_Truong_15.Text = "";
            //        uC_2225_Item5.txt_Truong_16.Text = "";
            //        uC_2225_Item5.txt_Truong_17.Text = "";
            //        uC_2225_Item5.txt_Truong_18.Text = "";
            //        uC_2225_Item5.txt_Truong_19.Text = "";
            //        uC_2225_Item5.txt_Truong_20.Text = "";
            //        uC_2225_Item5.txt_Truong_21.Text = "";
            //        uC_2225_Item5.txt_Truong_22.Text = "";
            //        uC_2225_Item5.txt_Truong_23.Text = "";
            //        uC_2225_Item5.txt_Truong_24.Text = "";
            //        uC_2225_Item5.txt_Truong_25.Text = "";
            //        uC_2225_Item5.txt_Truong_26.Text = "";
            //    }
            //}

            Global.Db.Insert_DESo_Check(BatchID, Image, Global.StrUserName,
                User1, flag_Phieu1_User1Sai, flag_Phieu2_User1Sai, flag_Phieu3_User1Sai, flag_Phieu4_User1Sai, flag_Phieu5_User1Sai,
                User2, flag_Phieu1_User2Sai, flag_Phieu2_User2Sai, flag_Phieu3_User2Sai, flag_Phieu4_User2Sai, flag_Phieu5_User2Sai,
                Truong_01,
                uC_2225_Item1.txt_Truong_02.Text,
                uC_2225_Item1.txt_Truong_03.Text,
                uC_2225_Item1.txt_Truong_04_1.Text,
                uC_2225_Item1.txt_Truong_04_2.Text,
                uC_2225_Item1.txt_Truong_04_3.Text,
                uC_2225_Item1.txt_Truong_05_1.Text,
                uC_2225_Item1.txt_Truong_05_2.Text,
                uC_2225_Item1.txt_Truong_06.Text,
                uC_2225_Item1.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_08.Text,
                uC_2225_Item1.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_13.Text,
                uC_2225_Item1.txt_Truong_14.Text,
                uC_2225_Item1.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_19.Text,
                uC_2225_Item1.txt_Truong_20.Text,
                uC_2225_Item1.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item1.txt_Truong_25.Text,
                uC_2225_Item1.txt_Truong_26.Text,

                uC_2225_Item2.txt_Truong_02.Text,
                uC_2225_Item2.txt_Truong_03.Text,
                uC_2225_Item2.txt_Truong_04_1.Text,
                uC_2225_Item2.txt_Truong_04_2.Text,
                uC_2225_Item2.txt_Truong_04_3.Text,
                uC_2225_Item2.txt_Truong_05_1.Text,
                uC_2225_Item2.txt_Truong_05_2.Text,
                uC_2225_Item2.txt_Truong_06.Text,
                uC_2225_Item2.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_08.Text,
                uC_2225_Item2.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_13.Text,
                uC_2225_Item2.txt_Truong_14.Text,
                uC_2225_Item2.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_19.Text,
                uC_2225_Item2.txt_Truong_20.Text,
                uC_2225_Item2.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item2.txt_Truong_25.Text,
                uC_2225_Item2.txt_Truong_26.Text,

                uC_2225_Item3.txt_Truong_02.Text,
                uC_2225_Item3.txt_Truong_03.Text,
                uC_2225_Item3.txt_Truong_04_1.Text,
                uC_2225_Item3.txt_Truong_04_2.Text,
                uC_2225_Item3.txt_Truong_04_3.Text,
                uC_2225_Item3.txt_Truong_05_1.Text,
                uC_2225_Item3.txt_Truong_05_2.Text,
                uC_2225_Item3.txt_Truong_06.Text,
                uC_2225_Item3.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_08.Text,
                uC_2225_Item3.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_13.Text,
                uC_2225_Item3.txt_Truong_14.Text,
                uC_2225_Item3.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_19.Text,
                uC_2225_Item3.txt_Truong_20.Text,
                uC_2225_Item3.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item3.txt_Truong_25.Text,
                uC_2225_Item3.txt_Truong_26.Text,

                uC_2225_Item4.txt_Truong_02.Text,
                uC_2225_Item4.txt_Truong_03.Text,
                uC_2225_Item4.txt_Truong_04_1.Text,
                uC_2225_Item4.txt_Truong_04_2.Text,
                uC_2225_Item4.txt_Truong_04_3.Text,
                uC_2225_Item4.txt_Truong_05_1.Text,
                uC_2225_Item4.txt_Truong_05_2.Text,
                uC_2225_Item4.txt_Truong_06.Text,
                uC_2225_Item4.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_08.Text,
                uC_2225_Item4.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_13.Text,
                uC_2225_Item4.txt_Truong_14.Text,
                uC_2225_Item4.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_19.Text,
                uC_2225_Item4.txt_Truong_20.Text,
                uC_2225_Item4.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item4.txt_Truong_25.Text,
                uC_2225_Item4.txt_Truong_26.Text,

                uC_2225_Item5.txt_Truong_02.Text,
                uC_2225_Item5.txt_Truong_03.Text,
                uC_2225_Item5.txt_Truong_04_1.Text,
                uC_2225_Item5.txt_Truong_04_2.Text,
                uC_2225_Item5.txt_Truong_04_3.Text,
                uC_2225_Item5.txt_Truong_05_1.Text,
                uC_2225_Item5.txt_Truong_05_2.Text,
                uC_2225_Item5.txt_Truong_06.Text,
                uC_2225_Item5.txt_Truong_07.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_08.Text,
                uC_2225_Item5.txt_Truong_09.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_10.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_11.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_12.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_13.Text,
                uC_2225_Item5.txt_Truong_14.Text,
                uC_2225_Item5.txt_Truong_15.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_16.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_17.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_18.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_19.Text,
                uC_2225_Item5.txt_Truong_20.Text,
                uC_2225_Item5.txt_Truong_21.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_22.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_23.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_24.Text.Replace(",", ""),
                uC_2225_Item5.txt_Truong_25.Text,
                uC_2225_Item5.txt_Truong_26.Text,
                txt_Truong_Flag.Text
                );
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
        private void txt_Truong_Flag_EditValueChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(sender, e);
            DoiMau(0, 1, (TextEdit)sender);
        }
    }
}
