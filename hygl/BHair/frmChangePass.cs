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
    public partial class frmChangePass : Form
    {
        public frmChangePass()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.Trim() == "")
            {
                MessageBox.Show("请重输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (txtOldPass.Text.Trim() == "")
            {
                MessageBox.Show("请重输入原密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (txtnewpass.Text.Trim() == "")
            {
                MessageBox.Show("请重输入新密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmpMoney EmpMoney = new EmpMoney();


            if (EmpMoney.CheckPass(txtUserID.Text,txtOldPass.Text) == true)
            {

                EmpMoney.ChangePass(txtUserID.Text, txtOldPass.Text, txtnewpass.Text);

                MessageBox.Show("密码修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("用户名或密码输入不正确，请重输！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
