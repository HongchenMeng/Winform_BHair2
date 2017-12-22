using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BHair.Business;
using BHair.Business.Table;
using System.IO;

namespace BHair.Report
{
    public partial class frmDeposit : WinFormsUI.Docking.DockContent
    {
        private bool _bRun = false;

        public frmDeposit()
        {
            InitializeComponent();
        }

        /// <summary>显示行号</summary>
        private void dgvDeposit_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            StaticValue.ShowRows_DataGridView_RowPostPaint(this.dgvDeposit, sender, e);
        }

        private void frmDeposit_Load(object sender, EventArgs e)
        {
            this.LoadDepositList();
            this._bRun = true;
        }

        /// <summary>加载充值记录列表。</summary>
        private void LoadDepositList()
        {
            List<Deposit> lstDeposit = new Deposit().SelectList("", this.cboDate.Year, this.cboDate.Month, this.cboDate.Day);
            this.dgvDeposit.AutoGenerateColumns = false;
            this.dgvDeposit.Rows.Clear();
            decimal dSum = 0;
            decimal dMoney = 0;
            foreach (Deposit objDep in lstDeposit)
            {
                if (objDep.Money < 0)
                {
                    dSum += 0 - objDep.Money;
                }
                else
                {
                    dSum += objDep.Money;
                }
                if (objDep.Mode == 0)
                {
                    dMoney += objDep.Money;
                }
                this.dgvDeposit.Rows.Add(new object[] { objDep.ID, "[" + objDep.MemberInfo.ID + "]" + objDep.MemberInfo.Name, objDep.Money, objDep.Date, objDep.ModeText, objDep.Remark });
            }
            this.lblSum.Text = "交易总金额：￥" + dSum.ToString("f2");
            this.lblSum.Top = this.ClientSize.Height - 18;
            this.lblMoney.Text = "实际收入金额：￥" + dMoney.ToString("f2");
            this.lblMoney.Top = this.ClientSize.Height - 18;
        }

        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._bRun)
            {
                this.LoadDepositList();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           // this.pageSetupDialog1.Document = this.printDocument1;     //设置需要设置页面的文档
           // this.pageSetupDialog1.ShowDialog();                      //显示页面设置对话框开始设置


           // this.printDocument1.OriginAtMargins = true;//启用页边距
            //this.pageSetupDialog1.EnableMetric = true; //以毫米为单位


            this.printPreviewDialog1.Document = this.printDocument1;  //设置需要预览的文档
            this.printPreviewDialog1.ShowDialog();//开始预览


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            PrintDataGridView PrintDataGridView = new PrintDataGridView();
            PrintDataGridView.Print(dgvDeposit, "统计报表");

           // Printer Printer = new Printer(dgvDeposit, printDocument1);
           // Printer.Print(e.Graphics);
        }


        //页面设置
        private void btnSetup_Click(object sender, EventArgs e)
        {
          
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel文件到";

            saveFileDialog.ShowDialog();

            // 这个一定要加上，要不然，点取消按钮就会提示“索引超出了数组界限”的错误。
            //用saveFileDialog.ShowDialog() == DialogResult.Cancle 不管用，会出现2次存储窗口。
            if (saveFileDialog.FileName == "")
            {

                return;
            }


            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            try
            {
                //写标题       
                for (int i = 0; i < dgvDeposit.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dgvDeposit.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //写内容    
                for (int j = 0; j < dgvDeposit.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dgvDeposit.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dgvDeposit.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
                MessageBox.Show("导出成功");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

    

        
      
                 
    }
}
