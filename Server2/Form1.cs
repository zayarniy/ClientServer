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


namespace Server2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Socket client;

        public void listenServer()
        {
            try
            {
                while (true)
                {
                    string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
                    socket.Listen(5);
                    client = socket.Accept();
                    byte[] buffer = new byte[1024];
                    client.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer);
                    //string[] splitMessage = message.Split(new char[] { '*' });
                    Invoke(new Action(() => tbGetMessage.Text += time + message));
                    Invoke(new Action(() => tbGetMessage.Text += "\r\n"));
                    client.Shutdown(SocketShutdown.Receive);
                    client.Close();
                }
            }
            catch(ThreadAbortException)
            {
                
            }
            finally
            {
                client?.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = tbSendMessage.Text;
            string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
            tbGetMessage.Text += time + " " + message + " sent\r\n";
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            client.Send(buffer);
            client.Shutdown(SocketShutdown.Send);

        }

        Thread listen;
        private void Form1_Load(object sender, EventArgs e)
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, 815));
            listen = new Thread(listenServer);
            listen.Start();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            listen.Abort();//Прерываем поток вызывая исключение в потоке
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbSendMessage.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbGetMessage.Text = "";
        }
    }
}
