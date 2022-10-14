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
        ClientServerLib.Server client;
        public Form1()
        {
            InitializeComponent();
            client=new ClientServerLib.Server();
        }

        public Socket socket;

  /*
   * public void listenServer()
        {
            try
            {
                while (true)
                {
                    string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    System.Diagnostics.Debug.WriteLine("Set socket");
                    socket.Connect("localhost", 815);
                    byte[] buffer = new byte[1024];
                    System.Diagnostics.Debug.WriteLine("Receive buffer");
                    socket.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer);
                    socket.Shutdown(SocketShutdown.Receive);
                    Invoke(new Action(() => tbGetMessage.Text += time + message));
                    Invoke(new Action(() => tbGetMessage.Text += "\r\n"));
                    socket.Close();
                    System.Diagnostics.Debug.WriteLine("Socket closed");
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
*/
        private void button1_Click(object sender, EventArgs e)
        {
            //string message = tbSendMessage.Text;
            //string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
            //tbGetMessage.Text += time + " " + message + " sent\r\n";
            //byte[] buffer = Encoding.UTF8.GetBytes(message);
            //socket.Send(buffer);
            //System.Diagnostics.Debug.WriteLine("send data");
            //socket.Shutdown(SocketShutdown.Send);
            //System.Diagnostics.Debug.WriteLine("Socket shutdown");
            client.SendMessage(tbSendMessage.Text, port: 8005);

        }

        Thread listenClient, listenServer;
        private void Form1_Load(object sender, EventArgs e)
        {
            tbSendMessage.Text = "Client "+new Random().Next(1,1000);
            //listenClient = new Thread(client.CreateClientForSend);
           // listenClient.Start(new ClientServerLib.Parameters() { IPAddress="127.0.0.1", Port=8006});
            listenServer = new Thread(client.CreateServerListener);
            listenServer.Start(new ClientServerLib.Parameters() { IPAddress = "127.0.0.1", Port = 8006 });

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbGetMessage.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket?.Close();
            listenServer?.Abort();//создаем исключение в потоке
            listenClient?.Abort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbSendMessage.Text = "";
        }
    }
}
