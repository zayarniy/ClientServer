using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["ServerPath"] = tbServerPath.Text;
            Properties.Settings.Default["ClientPath"] = tbClientPath.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(tbServerPath.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            tbServerPath.Text = Properties.Settings.Default["ServerPath"]?.ToString();
            tbClientPath.Text= Properties.Settings.Default["ClientPath"]?.ToString();
            System.Diagnostics.Process.Start(tbServerPath.Text);
            System.Diagnostics.Process.Start(tbClientPath.Text);

        }

        private void tbServerPath_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                tbServerPath.Text = ofd.FileName;
            }
        }

        private void tbClientPath_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbClientPath.Text = ofd.FileName;
            }

        }

        private void btnClientStart_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(tbClientPath.Text);
        }
    }
}
