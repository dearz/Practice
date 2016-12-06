using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LoginTest
{
    public partial class LoginForm : Form
    {
        private UserInfo loginUser;

        public UserInfo LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; }
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string szUserID = this.textBox1.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(szUserID))
            {
                MessageBox.Show("请输入您的用户ID!");
                this.textBox1.Focus();
                this.textBox1.SelectAll();
                return;
            }
            short shRet = ReturnValue.OK;

            //获取用户信息
            this.loginUser = null;
            shRet = DataService.Instance.GetUserInfo(szUserID, ref this.loginUser);
            if (shRet == ReturnValue.FAILED)
            {
                MessageBox.Show("您没有权限登录护理电子病历系统!");
                this.textBox1.Focus();
                this.textBox1.SelectAll();
                return;
            }
            if (this.loginUser == null || shRet != ReturnValue.OK)
            {
                MessageBox.Show("登录失败,系统获无法取用户信息!");
                this.textBox1.Focus();
                this.textBox1.SelectAll();
                return;
            }

            //验证用户输入的密码
            shRet = DataService.Instance.IsUserValid(szUserID, this.textBox2.Text);
            if (shRet == ReturnValue.FAILED)
            {
                MessageBox.Show("您输入的登录口令错误!");
                this.textBox2.Focus();
                this.textBox2.SelectAll();
                return;
            }
            if (shRet != ReturnValue.OK)
            {
                MessageBox.Show("登录失败,系统无法验证用户信息!");
                this.textBox2.Focus();
                this.textBox2.SelectAll();
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
