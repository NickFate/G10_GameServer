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
        static OleDbDataAdapter da;

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
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());
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
            string query = "INSERT INTO PlayersData (posX, posY) VALUES (0, 0)";

            OleDbCommand command = new OleDbCommand(query, dbc);
            command.ExecuteNonQuery();
        }

    }
}
