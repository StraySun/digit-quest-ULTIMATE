using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace digit_quest_ULTIMATE
{
    internal class Program
    {
        static Random random = new Random();
        static int playerAttack = 0;
        static int playerHP = 0;
        static int playerMaxHP = 0;
        static int gold = 0;
        static int potion = 0;
        static int potionHill = 0;
        static int arrow = 0;
        static List<string> equipments = new List<string>();
        static int baseAttack = 0;
        static void Main(string[] args)
        {
            Console.WriteLine($@"
      {randHeader()}

Press ENTER to start
");
            string str = Console.ReadLine();
            // game will not start until the player presses the enter key
            for (int i = 0; i < 2; i++)
                if (str == "")
                {
                    i = 1;
                    StartGame();
                }
                else
                    while (str != "") str = Console.ReadLine();
            
        }
        static string randHeader()
        {
            List<string> header = new List<string>() {
            "Saranheyo <3", "Hola", "ULT1MATE", "1365244", "143", "I Miss U",
            "I Love U", "", "Ultimate", "ULTIMATE", "WS2", "U like Butterfly..",
            "`^‘", "`^’"
            };
            int phrase = random.Next(0, header.Count);
            return header[phrase];
        }

        static void InitializeGame()
            //– устанавливает начальные характеристики игрока.
        {
            int delay = 750;
            Console.WriteLine(@"Setting the initial characteristics of the player");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Здоровье: 100 HP");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Максимальное HP: 100");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Золото: 10");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Зелья: 2 (восстанавливают 30 HP)");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Стрелы: 5");
            Thread.Sleep(delay);
            Console.WriteLine(@"    - Оружие: меч и лук");
            Thread.Sleep(delay);

            playerHP = 100;
            playerMaxHP = 100;
            gold = 10;
            potion = 2;
            potionHill = 30; //(how many u want hill with potion)
            arrow = 5;
            //List<string> equipments = new List<string> { "меч", "лук" };
            equipments.Clear();
            equipments.Add("меч");
            equipments.Add("лук");
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
        static void StartGame()
            //– запускает основной игровой процесс.
        {
            InitializeGame();
            Console.Write("\n");
            string text ="Preparing the monsters";
            humanType(text);
            Thread.Sleep(750);
            text = "Setting traps";
            humanType(text);
            Thread.Sleep(750);
            text = "...";
            humanType(text);
            Thread.Sleep(500);
            text = "Bad things have happened..";
            humanType(text);
            Thread.Sleep(500);
            text = ".....";
            humanType(text);
            Thread.Sleep(1750);
            Console.WriteLine();
            text = "..so that terrible things don't happen...";
            humanType(text);
            Thread.Sleep(1750);
            text = ".........";
            humanType(text);
            Thread.Sleep(1750);
            text = "You can start your journey.";
            humanType(text);
        }
        static void ProcessRoom(int roomNumber)
            //– обрабатывает событие в комнате.
        {
            Console.WriteLine("U went to next room and...");
            Console.WriteLine("⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬");
            int rand = random.Next(0,4);
            // 0 - figth with monster's;
            // 1 - fall into a trap;
            // 2 - empty room;
            // 3(def) - chest
            switch (rand)
            {
                case 0:
                    Console.WriteLine("Danger! Monster behind u!!");
                        //maybe needly write code about It was worth choosing weapon here
                    FightMonster(random.Next(20,50+1), random.Next(5,15+1));
                    break;
                case 1:
                    Console.WriteLine("Ohh, ups! U fall into a trap");
                    int trapLoseHP = fallingToTrap();
                    playerHP -= trapLoseHP;
                    Console.WriteLine($"Player: {playerHP}/{playerMaxHP} HP (-{trapLoseHP})");
                    break;
                case 2:
                    Console.WriteLine("Lmao. U found empty room.. So, maybe it's better that we don't know the details");
                    break;
                default:
                    Console.WriteLine(@"Be carefull & u can look up the chest <3
Well, well, well.
Look
U found the chest.
Do u want open him? (def: 'yes')");
                    string ans = Console.ReadLine();
                    bool boolean = (ans == "y" || ans == "yes" || ans == "" ? true:false);
                    if (boolean)
                    {
                        OpenChest();
                    }
                    break;
            }

        }

        static void FightMonster(int monsterHP, int monsterAttack)
        //– бой с монстром.
        {
            Console.WriteLine($"Monster have {monsterHP} HP & {monsterAttack} ATK.");
            int monsterMaxHP = monsterHP;
            while (monsterHP > 0)
            {
                playerAttack = PlayerAttack(random.Next(10, 20 + 1), random.Next(5, 15 + 1));
                // sword/bow attack (10-20/5-15)
                monsterHP -= playerAttack;
                playerHP -= monsterAttack;
                Console.Write($@"   PLayer HP: {playerHP}/{playerMaxHP} (-{monsterAttack})
    Monster HP: {monsterHP}/{monsterMaxHP} (-{playerAttack})
");
            }
            Console.WriteLine("Congratulation! U win the monster");
        }
        static int PlayerAttack(int swordAttack, int bowAttack)
        {
            Console.WriteLine("Choose with what weapon u want attack: ");
            for (int i = 1; i < equipments.Count + 1; i++)
            {
                Console.WriteLine($@"   {i}. {equipments[i - 1]}");
            }
            int choose = int.Parse(Console.ReadLine());
            if (choose == 1) return swordAttack;
            else if (choose == 2) return bowAttack;
            else return 0;
        }
        static int fallingToTrap()
        //falling up
        {
            return random.Next(5-20);
        }

        static void OpenChest()
        //– открытие сундука (обычного или проклятого || empty).
        {
            int chestChance = random.Next(0, 100 + 1);
            int cursedChestChance = (chestChance % 10 == 0 ? 0 : -1);
            int emptyChestChance = (chestChance % 20 == 0 ? 1 : -1);
            int chest = (emptyChestChance >= 0 ? 1 : (cursedChestChance >= 0 ? 0 : -1));
            // 0 - cursed chest; (5%)
            // 1 - empty chest; (5%)
            // 2(def) - common chest (90%)
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
            int drop = random.Next(0, 7);
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
            return random.Next(1, 4);
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
            return (random.Next(0, 1 + 1) % 2 == 0 ? (5) : (10));
        }
        static void VisitMerchant()
            //– покупка предметов у торговца.
        {
        }
        static void VisitAltar()
            //– усиление персонажа за золото.
        {
        }
        static void MeetDarkMage()
            //– взаимодействие с таинственным магом.
        {
        }
        static void UsePotion()
            //– восстановление здоровья.
        {
            playerHP += potionHill;
        }
        static void ShowStats()
            //– вывод характеристик игрока.
        {
            Console.WriteLine(playerHP);
            Console.WriteLine(playerMaxHP);
            Console.WriteLine(gold);
            Console.WriteLine(potion);
            Console.WriteLine(potionHill);
            Console.WriteLine(arrow);
            foreach (var equips in equipments)
            {
                Console.Write(equips);
            }
            //List<string> equipments = new List<string> { "меч", "лук" };
        }
        static void FightBoss()
            //– битва с финальным боссом.
        {
        }
        static void EndGame(bool isWin)
            //– завершение игры.
        {
        }
    }
}
