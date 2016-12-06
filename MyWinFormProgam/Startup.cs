using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyProgram
{
    public class Startup
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
