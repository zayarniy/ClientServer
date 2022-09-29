using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Client2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Socket socket;

        public void listenServer()
        {
            try
            {
                while (true)
                {
                    string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect("localhost", 815);
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer);
                    socket.Shutdown(SocketShutdown.Receive);
                    Invoke(new Action(() => tbGetMessage.Text += time + message));
                    Invoke(new Action(() => tbGetMessage.Text += "\r\n"));
                    socket.Close();
                }
            }
            catch(ThreadAbortException)
            {
                //прерываем выполнение потока
            }
            finally
            {
                socket?.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = tbSendMessage.Text;
            string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
            tbGetMessage.Text += time + " " + message + " sent\r\n";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            socket.Send(buffer);
            socket.Shutdown(SocketShutdown.Send);

        }

        Thread listen;
        private void Form1_Load(object sender, EventArgs e)
        {
            listen = new Thread(listenServer);
            listen.Start();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            socket?.Close();
            listen.Abort();//создаем исключение в потоке
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbGetMessage.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbSendMessage.Text = "";
        }
    }
}
