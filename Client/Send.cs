﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Send
    {
        public const int port = 815;
        public const string server = "127.0.0.1";
        public void SendMes(out string resp)
        {
            resp = "";
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable); // пока данные есть в потоке

                //                form.textBox1.Text = (response.ToString());
                //System.Diagnostics.Debug.WriteLine("Get data:"+response.ToString());
                System.IO.File.AppendAllText("data.txt", "Get data:" + response.ToString());
                resp= response.ToString();
                // Закрываем потоки
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Запрос завершен...");
            Console.Read();

            try
            {
 
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);  
            }

        }

    }
}
