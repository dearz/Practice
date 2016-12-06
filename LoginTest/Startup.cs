using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LoginTest
{
    public static class Startup
    {
        [STAThread]
        public static void Main(string[] args)
        {          
            Startup.StartNewInstance(args);        
        }

        private static void StartNewInstance(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            mainForm.StartPosition = FormStartPosition.CenterScreen;

            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterParent;
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                goto LABEL_EXIT_APP;
            }
            loginForm.Dispose();
            mainForm.SwitchSystemUser(loginForm.LoginUser);


            if (mainForm == null || mainForm.IsDisposed)
                Application.Exit();
            else
                Application.Run(mainForm);

        LABEL_EXIT_APP:
            mainForm.Dispose();
        }
    }
}
