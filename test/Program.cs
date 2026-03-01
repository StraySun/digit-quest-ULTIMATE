using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        static Random random = new Random();
        static int playerAttack = 0;
        static int playerHP = 100;
        static int playerMaxHP = 100;
        static int gold = 100;
        static int potion = 100;
        static int potionHill = 10;
        static int arrow = 100;
        static List<string> equipments = new List<string>() {"меч","лук"};
        static void Main(string[] args)
        {
            //string text = ("WoW, and u're not a fool.");
            //humanType(text);
            //text = "Well, get ready";
            //humanType(text);
            //text = "1... 2... 3...";
            int bossDoubleAttackChance = random.Next(0, 1 + 1);
            int bossAttack = (bossDoubleAttackChance==0 ? 1 : 2) * 2;
            Console.WriteLine(bossAttack);
        }
        static void humanType(string text)
        {
            int delay;
            for (int i = 0; i < text.Length; ++i)
            {
                if (i > 0 && text[i] == text[i - 1]) delay = 10;
                else delay = random.Next(40, 200);
                Thread.Sleep(delay);
                Console.Write(text[i]);
            }
            Console.Write("\n");
        }
    }
}
