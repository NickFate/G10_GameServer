using GameServer.Classes;
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
            DataBaseHelper.Connect();
            DataBaseHelper.GetFromTable();

            Console.WriteLine();
            DataBaseHelper.AddToTable();
            DataBaseHelper.GetFromTable();

            Console.WriteLine();
            DataBaseHelper.UpdateTable();
            DataBaseHelper.GetFromTable();


            DataBaseHelper.Close();
            
            //new Server("127.0.0.1", 9000).Start();
        }
    }

    public struct Player
    {
        public int id;
        public int posX;
        public int posY;
    }
}