using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BHair.Base
{
    public partial class frmPrintBar : Form
    {
        public frmPrintBar(string strBar)
        {
            InitializeComponent();

            BuildBar(strBar);

        }

        //生成条形码
        public void BuildBar(string Bar)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();

            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            b.IncludeLabel = true;
            System.Drawing.Image image = b.Encode(type, Bar, Color.Black, Color.White, 270, 60);

            pictureBox1.Image = image;
        
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
          //  PrintDialog MyPrintDg = new PrintDialog();
           // MyPrintDg.Document = printDocument1;


            this.printPreviewDialog1.Document = printDocument1;  //设置需要预览的文档
            this.printPreviewDialog1.ShowDialog();//开始预览

            //if (MyPrintDg.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        //printDocument1.Print();

            //    }
            //    catch
            //    {   //停止打印
            //        printDocument1.PrintController.OnEndPrint(printDocument1, new System.Drawing.Printing.PrintEventArgs());
            //    }
            //}

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 20, 20);

        }
    }
}
