using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BHair.Business.Table;

namespace BHair
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            EmpMoney EmpMoney=new EmpMoney();

            if (EmpMoney.CheckPass(txtUserID.Text, txtPass.Text) == true)
            {
                this.Visible = false;

                frmMain frmMain = new frmMain();
                frmMain.Show();

            }
            else
            {
                MessageBox.Show("用户名或密码输入不正确，请重输！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
