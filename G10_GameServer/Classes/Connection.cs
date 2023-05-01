using GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using G10_GameServer.Classes.SendingClasses;

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
                // удаляем пустые символы 0x00 или \0
                request = request.Replace("\0", string.Empty);
                
                if (request == "")
                {
                    clientSocket.Close();
                    return;
                }

                Console.WriteLine("request: " + request);

                if (request.Contains("SelfData"))
                {
                    request = request.Replace("}}", "}");


                    SelfData rec_data = JsonSerializer.Deserialize<SelfData>(request);

                    Console.WriteLine(rec_data.ID);
                    SendMessage();
                    continue;
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

            foreach (SelfData p in Server.list)
            {
                ms += JsonSerializer.Serialize(p);
            }

            byte[] data = Encoding.UTF8.GetBytes(ms);
            clientSocket.Send(data);
            Console.WriteLine(ms);
        }

        public void SendMessage(string ms)
        {
            byte[] data = Encoding.UTF8.GetBytes(ms);
            clientSocket.Send(data);
            Console.WriteLine(ms);
        }
    }
}
