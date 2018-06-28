using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CLS_NatSu.MyData;
using CLS_NatSu.MyClass;
using System.Linq;

namespace CLS_NatSu.MyForm
{
    public partial class frm_NangSuat : DevExpress.XtraEditors.XtraForm
    {
        private DateTime firstDateTime;
        private DateTime lastDateTime;
        public frm_NangSuat()
        {
            InitializeComponent();
        }
        bool FlagLoag = false;
        private void frm_NangSuat_Load(object sender, EventArgs e)
        {
            FlagLoag = true;
            chk_checker.Checked = false;
            lb_SoLuong.Text = "";
            timeFisrt.EditValue = "00:00";
            timeEnd.EditValue = "23:59";
            string firstdate = dtp_FirstDay.Value.ToString("yyyy-MM-dd ") + timeFisrt.Text + ":00";//" 00:00:00";
            string lastdate = dtp_EndDay.Value.ToString("yyyy-MM-dd ") + timeEnd.Text + ":59";// " 23:59:59";
            
            firstDateTime = DateTime.Parse(firstdate);
            lastDateTime = DateTime.Parse(lastdate);
            FlagLoag = false;
        }
        private void LoadDataGrid(DateTime TuNgay, DateTime DenNgay)
        {
            gridControl1.DataSource = null;
            dataGridView1.DataSource = null;
            if (!chk_checker.Checked)
            {
                List<NangSuatInput_FullResult> ListNangSuat = (from w in Global.Db.NangSuatInput_Full(TuNgay, DenNgay) select w).ToList();
                gridControl1.DataSource = dataGridView1.DataSource = (from w in ListNangSuat select w).ToList();
            }
            else if(chk_checker.Checked)
            {
                List<NangSuatCheck_FullResult> ListNangSuat = (from w in Global.Db.NangSuatCheck_Full(TuNgay, DenNgay) select w).ToList();
                gridControl1.DataSource = dataGridView1.DataSource = (from w in ListNangSuat select w).ToList();
            }
        }
        private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            //doi mau row chan
            if (e.RowHandle >= 0)
            {
                if (e.RowHandle % 2 == 0) e.Appearance.BackColor = Color.LavenderBlush;
                else
                {
                    e.Appearance.BackColor = Color.BlanchedAlmond;
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!chk_checker.Checked)
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity.xlsx"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity.xlsx");
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Productivity.xlsx"), Properties.Resources.Productivity);
                }
                else
                {
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Productivity.xlsx"), Properties.Resources.Productivity);
                }
                TableToExcel(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity.xlsx");
            }
            else if(chk_checker.Checked)
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity_Check.xlsx"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity_Check.xlsx");
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Productivity_Check.xlsx"), Properties.Resources.Productivity_Check);
                }
                else
                {
                    File.WriteAllBytes((Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Productivity_Check.xlsx"), Properties.Resources.Productivity_Check);
                }
                TableToExcel_Check(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity_Check.xlsx");
            }
        }
        Microsoft.Office.Interop.Excel.Application app = null;
        Microsoft.Office.Interop.Excel.Workbook book = null;
        Microsoft.Office.Interop.Excel.Worksheet wrksheet = null;
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        public void TableToExcel(string strfilename)
        {
            try
            {
                lb_SoLuong.Text = "";
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                progressBar1.Maximum = dataGridView1.RowCount;
                progressBar1.Minimum = 0;
                progressBar1.Visible = true;
                ModifyProgressBarColor.SetState(progressBar1, 1);
                app = new Microsoft.Office.Interop.Excel.Application();
                book = app.Workbooks.Open(strfilename, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                wrksheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Sheets["NATSU"];
                int h = 1,n=0;
                wrksheet.Cells[2, 10] = "Thời gian:" + timeFisrt.Text + "/" + dtp_FirstDay.Value.Day + "/" + dtp_FirstDay.Value.Month + "/" + dtp_FirstDay.Value.Year + " đến " + timeEnd.Text + "/" + dtp_EndDay.Value.Day + "/" + dtp_EndDay.Value.Month + "/" + dtp_EndDay.Value.Year;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    wrksheet.Cells[h + 2, 1] = h;
                    wrksheet.Cells[h + 2, 2] = dataGridView1.Rows[i].Cells[0].Value + "";//username
                    wrksheet.Cells[h + 2, 3] = dataGridView1.Rows[i].Cells[1].Value + "";//fullname
                    wrksheet.Cells[h + 2, 4] = dataGridView1.Rows[i].Cells[2].Value + "";//tong
                    wrksheet.Cells[h + 2, 5] = dataGridView1.Rows[i].Cells[3].Value + "";//phieudung
                    wrksheet.Cells[h + 2, 6] = dataGridView1.Rows[i].Cells[4].Value + "";//phieusai
                    wrksheet.Cells[h + 2, 7] = dataGridView1.Rows[i].Cells[5].Value + "";//thoigian
                    wrksheet.Cells[h + 2, 8] = dataGridView1.Rows[i].Cells[6].Value + "";//hieusuat
                    h++;
                    progressBar1.PerformStep();
                    lb_SoLuong.Text = ++n+@"\"+ progressBar1.Maximum;
                }
                string savePath;

                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = @"Save Excel Files";
                saveFileDialog1.Filter = @"Excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = "NangSuat_NatSu_" + dtp_FirstDay.Value.Day + "-" + dtp_EndDay.Value.Day;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    book.SaveCopyAs(saveFileDialog1.FileName);
                    book.Saved = true;
                    savePath = Path.GetDirectoryName(saveFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show(@"Error exporting excel!");
                    return;
                }
                if (savePath != null) Process.Start(savePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                progressBar1.Visible = false;
                lb_SoLuong.Text = "";
                if (book != null)
                    book.Close(false);
                if (app != null)
                    app.Quit();
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity.xlsx"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity.xlsx");
                }
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + saveFileDialog1.FileName))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + saveFileDialog1.FileName);
                }
            }
        }

        public void TableToExcel_Check(string strfilename)
        {
            try
            {
                lb_SoLuong.Text = "";
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                progressBar1.Maximum = dataGridView1.RowCount;
                progressBar1.Minimum = 0;
                progressBar1.Visible = true;
                ModifyProgressBarColor.SetState(progressBar1, 1);
                app = new Microsoft.Office.Interop.Excel.Application();
                book = app.Workbooks.Open(strfilename, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                wrksheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Sheets["NATSU"];
                int h = 1, n = 0;
                wrksheet.Cells[2, 7] = "Thời gian:" + timeFisrt.Text + "/" + dtp_FirstDay.Value.Day + "/" + dtp_FirstDay.Value.Month + "/" + dtp_FirstDay.Value.Year + " đến " + timeEnd.Text + "/" + dtp_EndDay.Value.Day + "/" + dtp_EndDay.Value.Month + "/" + dtp_EndDay.Value.Year;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    wrksheet.Cells[h + 2, 1] = h;
                    wrksheet.Cells[h + 2, 2] = dataGridView1.Rows[i].Cells[0].Value + "";//username
                    wrksheet.Cells[h + 2, 3] = dataGridView1.Rows[i].Cells[1].Value + "";//fullname
                    wrksheet.Cells[h + 2, 4] = dataGridView1.Rows[i].Cells[2].Value + "";//tong
                    wrksheet.Cells[h + 2, 5] = dataGridView1.Rows[i].Cells[3].Value + "";//thoigian
                    h++;
                    progressBar1.PerformStep();
                    lb_SoLuong.Text = ++n + @"\" + progressBar1.Maximum;
                }
                string savePath;

                saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = @"Save Excel Files";
                saveFileDialog1.Filter = @"Excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = "NangSuatCheckerNatSu_" + dtp_FirstDay.Value.Day + "-" + dtp_EndDay.Value.Day;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    book.SaveCopyAs(saveFileDialog1.FileName);
                    book.Saved = true;
                    savePath = Path.GetDirectoryName(saveFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show(@"Error exporting excel!");
                    return;
                }
                if (savePath != null) Process.Start(savePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                progressBar1.Visible = false;
                lb_SoLuong.Text = "";
                if (book != null)
                    book.Close(false);
                if (app != null)
                    app.Quit();
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity_Check.xlsx"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Productivity_Check.xlsx");
                }
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + saveFileDialog1.FileName))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + saveFileDialog1.FileName);
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string firstdate = dtp_FirstDay.Value.ToString("yyyy-MM-dd ") + timeFisrt.Text + ":00";// " 00:00:00";
            string lastdate = dtp_EndDay.Value.ToString("yyyy-MM-dd ") + timeEnd.Text + ":59";// " 23:59:59";
            firstDateTime = DateTime.Parse(firstdate);
            lastDateTime = DateTime.Parse(lastdate);
            if (firstDateTime >= lastDateTime)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc");
                return;
            }
            if (firstDateTime > lastDateTime)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu");
                return;
            }
            if (firstDateTime >= lastDateTime)
            {
                MessageBox.Show("Giờ bắt đầu phải nhỏ hơn hoặc bằng giờ kết thúc");
                return;
            }
            if (firstDateTime > lastDateTime)
            {
                MessageBox.Show("Giờ kết thúc phải lớn hơn hoặc bằng giờ bắt đầu");
                return;
            }
            else
            {
                LoadDataGrid(firstDateTime, lastDateTime);
            }
        }

        private void chk_checker_CheckedChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            dataGridView1.DataSource = null;
        }
    }
}