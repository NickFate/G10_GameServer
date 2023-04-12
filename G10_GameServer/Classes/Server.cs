using GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    class Server
    {

        public static List<Player> list = new List<Player>();

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

                Console.WriteLine("[Server]: Server start");
                while (IsActive)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ConnecionThread), listenSocket.Accept());
                }
            }
            else
            {
                Console.WriteLine("[Server]: Server is already running");
            }
        }

        public void Stop()
        {
            if (IsActive)
            {
                listenSocket.Close();
                IsActive = false;

                Console.WriteLine("[Server]: Server stoped");
            }
            else
            {
                Console.WriteLine("[Server]: Server is already stoping");
            }
        }

        public void ConnecionThread(object client)
        {
            new Connecion((Socket)client);
        }
    }
}
