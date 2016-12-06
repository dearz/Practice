using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LoginTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public bool SwitchSystemUser(UserInfo userInfo)
        {
            if (userInfo == null || string.IsNullOrEmpty(userInfo.Id))
                return false;

            UserInfo currentUser = null;
            if (SystemContext.Instance.LoginUser != null)
                currentUser = SystemContext.Instance.LoginUser;
            if (currentUser != null && currentUser.Equals(userInfo))
                return true;

            if (!SaveCurrentFile())
            {
                return false;
            }

            SystemContext.Instance.LoginUser = userInfo;

            this.labName.Text = userInfo.Name;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchSystemUser(loginForm.LoginUser);
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterParent;
            loginForm.ShowInTaskbar = false;
            if (loginForm.ShowDialog() != DialogResult.OK)
                return;

            loginForm.Dispose();
        }

        private bool SaveCurrentFile()
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
                return true;
            else
            {
                try
                {
                    DataService.Instance.SaveTextFiles(this.textBox1.Text);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
