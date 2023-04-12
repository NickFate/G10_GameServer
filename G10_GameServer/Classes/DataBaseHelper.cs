using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Data.Common;

namespace GameServer.Classes
{
    public static class DataBaseHelper
    {

        static OleDbConnection dbc;

        static string path = "C:\\Users\\Asus\\source\\repos\\G10_GameServer\\G10_GameServer\\DataBase\\GameDataBase.mdb";

        public static void Connect()
        {
            dbc = new OleDbConnection("Provider=" + "Microsoft.Jet.OLEDB.4.0;Data Source=" + path);
            dbc.Open();
        }

        public static void Close()
        {
            dbc.Close();
        }

        public static void GetFromTable()
        {
            string query = "SELECT * FROM PlayersData";
            OleDbCommand command = new OleDbCommand(query, dbc);
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i].ToString() + ' ');
                }
                Console.WriteLine();
            }
            
        }

        public static void AddToTable()
        {
            string query = "INSERT INTO PlayersData (posX, posY) VALUES (0, 0)";

            OleDbCommand command = new OleDbCommand(query, dbc);

            command.ExecuteNonQuery();
        }

        public static void UpdateLine(string table, string set, string where)
        {
            string query = "UPDATE " + table + " SET " + set + " WHERE " + where;

            OleDbCommand command = new OleDbCommand(query, dbc);
            command.ExecuteNonQuery();
        }
        
        public static void Register(string username, string email, string password)
        {
            string query = "INSERT INTO PlayersData (username, email, password) VALUES (\'" + username + "\', \'" + email + "\', \'" + password + "\')";

            OleDbCommand command = new OleDbCommand(query, dbc);
            command.ExecuteNonQuery();
        }

        public static int Login(string login)
        {
            string query = "SELECT password FROM PlayersData WHERE username = \'" + login + "\' OR email = \'" + login + "\'";
            OleDbCommand command = new OleDbCommand(query, dbc);
            if (command.ExecuteScalar() == null)
            {
                return -1;
            }
            return 0;
        }
    }
}
