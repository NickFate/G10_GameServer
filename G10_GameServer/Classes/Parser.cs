using GameServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Classes
{
    public static class Parser
    {

        public static bool HasTag(string str, string tag)
        {
            return str.Contains("<" + tag + ">");
        }

        public static string GetByTag(string str, string tag)
        {
            return str.Split(new string[] { "<" + tag + ">" }, StringSplitOptions.None)[1];
        }

        public static string PlayerToString(Player p)
        {
            return "<header>client update<header>" +
                "<id>" + p.id + "<id>" +
                "<posX>" + p.posX + "<posX>" +
                "<posY>" + p.posY + "<posY>";
        }

    }
}
