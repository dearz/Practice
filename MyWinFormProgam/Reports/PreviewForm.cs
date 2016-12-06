using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using grproLib;

namespace MyProgram.Reports
{
    public partial class PreviewForm : Form
    {
        public PreviewForm()
        {
            InitializeComponent();
        }

        public PreviewForm(GridppReport report)
            : this()
        {
            this.axGRPrintViewer1.Report = report;
        }

        protected override void OnShown(EventArgs e)
        {
            try
            {
                this.axGRPrintViewer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.OnShown(e);
        }
    }
}
