using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        Thread thread;
        StringBuilder sb = new StringBuilder("");




        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            Send s1 = new Send();
            thread = new Thread(new ParameterizedThreadStart(s1.SendMes));
            thread.Start(sb);
            if (sb.Length != 0)
            {

                richTextBox1.AppendText(sb.ToString() + "\r\n");
            }
            sb.Clear();
        }
    }
}
