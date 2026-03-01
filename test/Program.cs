using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            OpenChest();
        }
        static void OpenChest()
        //– открытие сундука (обычного или проклятого || empty).
        {
            int chestChance = random.Next(0, 100 + 1);
            int cursedChestChance = (chestChance % 10 == 0 ? 0 : -1);
            int empryChestChance = (chestChance % 20 == 0 ? 1 : -1);
            int chest = (empryChestChance >= 0 ? 1 : (cursedChestChance >= 0 ? 0 : -1));

            int plusGolden = FoundGold();
            int minusHP = minusMaxHP();
            switch (chest)
            {
                case 0:
                    gold += plusGolden;
                    playerHP -= minusHP;
                    playerMaxHP -= minusHP;
                    Console.WriteLine($@"So, what do we have in the chest?
U find some money.
However, unfortunately, there were some losses.
    Gold: {gold} (+{plusGolden})
    Player: {playerHP}/{playerMaxHP} HP (-{minusHP})");
                    break;
                case 1:
                    Console.WriteLine("The chest is empty. Go away!");
                    break;
                default:
                    openDefChest();
                    break;
            }
        }
        static void openDefChest()
        {
            Console.WriteLine("So, what do we have in the chest?");
            int plusGolden = FoundGold();
            int plusPotion = FoundPotion();
            int plusArrow = FoundArrow();
            int drop = random.Next(0,7);
            // 0-gold; 1-potion; 2-arrow; 3-(01); 4-(02); 5-(12); 6(def)-(012)
            switch (drop)
            {
                case 0:
                    gold += plusGolden;
                    Console.WriteLine($@"U find some money.
    Gold: {gold} (+{plusGolden})");
                    break;
                case 1:
                    potion += plusPotion;
                    Console.WriteLine($@"U find some potion.
    Potion: {potion} (+{plusPotion})");
                    break;
                case 2:
                    arrow += plusArrow;
                    Console.WriteLine($@"U find some arrow.
    Arrow: {arrow} (+{plusArrow})");
                    break;
                case 3:
                    gold += plusGolden;
                    potion += plusPotion;
                    Console.WriteLine($@"U find some money & potion.
    Gold: {gold} (+{plusGolden})
    Potion: {potion} (+{plusPotion})");
                    break;
                case 4:
                    gold += plusGolden;
                    arrow += plusArrow;
                    Console.WriteLine($@"U find some money & arrow.
    Gold: {gold} (+{plusGolden})
    Arrow: {arrow} (+{plusArrow})");
                    break;
                case 5:
                    potion += plusPotion;
                    arrow += plusArrow;
                    Console.WriteLine($@"U find some potion & arrow.
    Potion: {potion} (+{plusPotion})
    Arrow: {arrow} (+{plusArrow})");
                    break;
                default:
                    gold += plusGolden;
                    potion += plusPotion;
                    arrow += plusArrow;
                    Console.WriteLine($@"Yooo, u so lucky today!
U find some money, potion & arrow.
    Gold: {gold} (+{plusGolden})
    Potion: {potion} (+{plusPotion})
    Arrow: {arrow} (+{plusArrow})");
                    break;
            }
        }
        static int FoundPotion()
        {
            return random.Next(1,4);
        }
        static int FoundGold()
        {
            return random.Next(3, 45 + 1);
        }
        static int FoundArrow()
        {
            return random.Next(3, 7 + 1);
        }
        static int minusMaxHP()
        {
            return (random.Next(0, 1+1) % 2 == 0 ? (5):(10));
        }
    }
}
