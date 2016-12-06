using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.Xml.Serialization;

namespace TestProgram
{
    public partial class Form1 : Form
    {
        private bool isCreated = false;

        private const string comboBoxName = "cb1";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            dgv.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isCreated)
            {
                ComboBox cb = new ComboBox();
                cb.Name = comboBoxName;
                cb.Location = new Point(10, 10);
                this.Controls.Add(cb);
                isCreated = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            int count =100000;
            while(count-->0)
            {
                data.Add(count.ToString());
            }

            ComboBox cb = null;
            foreach(Control cl in this.Controls)
            {
                cb = cl as ComboBox;
                if (cb != null)
                {
                    break;
                }
            }

            cb.DataSource = data;
        }

        private void button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult =  MessageBox.Show("是否关闭","", MessageBoxButtons.YesNo);
            if(dialogResult== DialogResult.Yes)
            {
                this.Close();
            }

            List<Form> formList = new List<Form>();

            Form1 f1 = new Form1();           
            formList.Add(f1);

            Form1 f2 = new Form1();
            formList.Add(f2);

            Form1 f3 = new Form1();
            formList.Add(f3);

            f1.Close();
            formList.Remove(f1);

            foreach(Form f in formList)
            {
                f.Close();
            }
        }

    }
}
