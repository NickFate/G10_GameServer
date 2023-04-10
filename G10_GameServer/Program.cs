using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Server("127.0.0.1", 9000).Start();
        }
    }

    class Server
    {

        public EndPoint Ip;
        int listenPort = 9000;
        Socket listenSocket;

        public bool IsActive = false;

        public Server(string ip, int port)
        {

            listenPort = port;
            Ip = new IPEndPoint(IPAddress.Parse(ip), listenPort);

            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (!IsActive)
            {
                listenSocket.Bind(Ip);

                const int connections = 4; // количество подключений в очереди
                listenSocket.Listen(connections);

                IsActive = true;

                Console.WriteLine("::: Server start");
                while (IsActive)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), listenSocket.Accept());
                }
            }
            else
            {
                Console.WriteLine("::: Server is already running");
            }
        }

        public void Stop()
        {
            if (IsActive)
            {
                listenSocket.Close();
                IsActive = false;

                Console.WriteLine("::: Server stoped");
            }
            else
            {
                Console.WriteLine("::: Server is already stoping");
            }
        }

        public void ClientThread(object client)
        {
            new Client((Socket)client);
        }
    }

    public class Client
    {

        Socket clientSocket;

        public Client(Socket c)
        {
            clientSocket = c;
            byte[] data = new byte[1024];
            string request = "";
            clientSocket.Receive(data);
            request = Encoding.UTF8.GetString(data);

            Console.WriteLine("request: " + request);

            if (request == "")
            {
                clientSocket.Close();
                return;
            }

            // обработка
            SendError();
            clientSocket.Close();

        }

        public void SendError()
        {

            byte[] data = Encoding.UTF8.GetBytes("error");
            clientSocket.Send(data);
        }
    }

}