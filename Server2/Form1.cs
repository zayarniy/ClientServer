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
        ClientServerLib.Server server;
        public Form1()
        {
            InitializeComponent();
             server = new ClientServerLib.Server();
        }

        public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Socket client;
        /*
        public void listenServer()
        {
            try
            {
                while (true)
                {
                    string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
                    socket.Listen(5);
                    client = socket.Accept();
                    System.Diagnostics.Debug.WriteLine("Socket accepted");
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
                if (client != null) client.Close();
                //client?.Close();
            }

        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            //string message = tbSendMessage.Text;
            //string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
            //tbGetMessage.Text += time + " " + message + " sent\r\n";
            //byte[] buffer = Encoding.UTF8.GetBytes(message);
            //client.Send(buffer);
            //client.Shutdown(SocketShutdown.Send);
            server.SendMessage(tbSendMessage.Text, "127.0.0.1", 8006);
           

        }

        Thread listen;
        private void Form1_Load(object sender, EventArgs e)
        {            
            //listen = new Thread(listenServer);
            //listen.Start();
            listen=new Thread(server.CreateServerListener);
            listen.Start(new ClientServerLib.Parameters() { IPAddress="127.0.0.1",Port=8005});
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbSendMessage.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbGetMessage.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Close();
            listen.Abort();//Прерываем поток вызывая исключение в потоке
        }
    }
}
