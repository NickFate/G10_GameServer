using GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    public class Connecion
    {

        Socket clientSocket;

        public Connecion(Socket c)
        {
            clientSocket = c;
            byte[] data = new byte[1024];
            string request = "";

            while (true)
            {
                // получение данных от клиента
                try
                {
                    clientSocket.Receive(data);
                }
                catch
                {
                    CloseConnection();
                    return;
                }


                request = Encoding.UTF8.GetString(data);

                Console.WriteLine("request: " + request);

                if (request == "")
                {
                    clientSocket.Close();
                    return;
                }

                int id = Convert.ToInt32(Parser.GetByTag(request, "id"));

                bool check = false;
                for (int i = 0; i < Server.list.Count; i++)
                {
                    if (Server.list[i].id == id)
                    {
                        Server.list[i] = new Player
                        {
                            id = id,
                            posX = Convert.ToInt32(Parser.GetByTag(request, "posX")),
                            posY = Convert.ToInt32(Parser.GetByTag(request, "posY"))
                        };

                        check = true;
                    }
                }
                if (!check)
                {
                    Server.list.Add(new Player
                    {
                        id = id,
                        posX = Convert.ToInt32(Parser.GetByTag(request, "posX")),
                        posY = Convert.ToInt32(Parser.GetByTag(request, "posY"))
                    });
                }

                // обработка
                SendMessage();
            }
        }

        private void CloseConnection()
        {
            clientSocket.Close();
        }

        public void SendError()
        {

            byte[] data = Encoding.UTF8.GetBytes("error");
            clientSocket.Send(data);
            clientSocket.Close();
        }

        public void SendMessage()
        {
            string ms = "";

            foreach (Player p in Server.list)
            {
                ms += Parser.PlayerToString(p);
            }

            byte[] data = Encoding.UTF8.GetBytes(ms);
            clientSocket.Send(data);
            Console.WriteLine(ms);
        }
    }
}
