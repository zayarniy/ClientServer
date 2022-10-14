using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientServerLib
{
    public class Parameters
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }

    public class Server
    {
        int port = 8005; // порт для приема входящих запросов
        string IPAdress = "127.0.0.1";
        public Socket listenSocket { get; private set; }
        public Socket clientSocket { get; private set; }


        public void Close()
        {
            listenSocket?.Close();
            clientSocket?.Close();
        }
        public void CreateServerListener(object parameters)//нужен без аргументов для запуска в потоке
        {
            Parameters parameters1 = parameters as Parameters;

            CreateServerListener(parameters1.IPAddress,parameters1.Port);
        }
        public void CreateServerListener(string IPAdress = "127.0.0.1", int port = 8005)
        {
            this.port = port;
            this.IPAdress = IPAdress;
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAdress), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            while (true)
            {
                // создаем сокет
                try
                {
                    // связываем сокет с локальной точкой, по которой будем принимать данные

                    // начинаем прослушивание
                    listenSocket.Listen(10);

                    Console.WriteLine("Сервер запущен. Ожидание подключений...");

                    while (true)
                    {
                        clientSocket = listenSocket.Accept();
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0; // количество полученных байтов
                        byte[] data = new byte[256]; // буфер для получаемых данных

                        do
                        {
                            bytes = clientSocket.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (clientSocket.Available > 0);

                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                        if (builder.ToString().IndexOf("end") > 0) return;
                        // отправляем ответ
                        string message = "ваше сообщение доставлено";
                        data = Encoding.Unicode.GetBytes(message);
                        clientSocket.Send(data);
                        // закрываем сокет
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //listenSocket.Close();
            }
        }

        public void CreateClientForSend(object parameters)
        {
            Socket socket = null;
            try
            {
                int port = (parameters as Parameters).Port;                
                while (true)
                {
                    string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " ";
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    System.Diagnostics.Debug.WriteLine("Client: Set socket");
                    socket.Connect(IPAdress, port);
                    byte[] buffer = new byte[1024];
                    System.Diagnostics.Debug.WriteLine("Client: Receive buffer");
                    socket.Receive(buffer);
                    string message = Encoding.UTF8.GetString(buffer);
                    socket.Shutdown(SocketShutdown.Receive);
                    //Invoke(new Action(() => tbGetMessage.Text += time + message));
                    //Invoke(new Action(() => tbGetMessage.Text += "\r\n"));
                    socket.Close();
                    System.Diagnostics.Debug.WriteLine("Client: Socket closed");
                }
            }
            catch (ThreadAbortException)
            {
                //прерываем выполнение потока
            }
            finally
            {
                socket?.Close();
            }
        }


        public string SendMessage(string message,string IPAdress="127.0.0.1", int port=8005)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAdress), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Diagnostics.Debug.WriteLine($"Client: Set socket on Port{port}");
                // подключаемся к удаленному хосту
                System.Diagnostics.Debug.WriteLine($"Client: подключаемся к удаленному хосту");
                socket.Connect(ipPoint);
                System.Diagnostics.Debug.WriteLine($"Client: Подключение установлено");
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);
                System.Diagnostics.Debug.WriteLine($"Client: Send");
                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();                
                System.Diagnostics.Debug.WriteLine($"Client: Recievd{builder.ToString()}");
                return builder.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return ex.Message;
            }

        }

    }
}
