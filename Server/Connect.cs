
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Connect
    {
        const int port = 815; // порт для прослушивания подключений
        public void FunctConn(string message)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                // запуск слушателя
                server.Start();

                //while (true)
                {
                    Thread.Sleep(1000);
                    //System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();
                    // сообщение для отправки клиенту
                    string response = message; // "Server send:" + DateTime.Now.ToString();// form.textBox2.Text;
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }

  

        }

    }
}
