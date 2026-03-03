using System;
using System.Collections.Generic;
using System.Threading;

namespace digit_quest_ULTIMATE
{
    internal class Program
    {


        // ост. ошибки:
        // переделать интерфейс для босса (дать имя и тд) -
        // слишком долго выводит принт -
        // фиксануть шанс процентов (особенно для комнат и мага) -



        static Random random = new Random();
        static int playerAttack = 0;
        static int playerHP = 0;
        static int playerMaxHP = 0;
        static int gold = 0;
        static int potion = 0;
        static int potionHeal = 0;
        static int arrow = 0;
        static List<string> equipments = new List<string>();
        static int bonusAttack = 0;
        static int roomCount = 15;
        static int roomNumber = 0;
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
                    Console.Clear();
                    StartPreGame();
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
            potionHeal = 30; //(how many u want hill with potion)
            arrow = 5;
            //List<string> equipments = new List<string> { "меч", "лук" };
            equipments.Clear();
            equipments.Add("меч");
            equipments.Add("лук");

            bonusAttack = 0;
            roomNumber = 0;
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
        static void StartPreGame()
        //– запускает основной игровой процесс.
        {
            InitializeGame();
            Console.Write("\n");
            string text = "Preparing the monsters";
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
            Console.WriteLine("Press ENTER....</>");
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
        static void StartGame()
        {
            Console.Clear();
            string text="";
            while (roomNumber < roomCount && playerHP > 0)
            {
                Console.WriteLine($@"
        <Currently u in {roomNumber} room>
Select the action you want to perform:
    0. See player stats
    1. Go to the next room
    2. Drink a potion
    3. Character enhancement
    4. Purchase items from a merchant.");
                int choose = -1;
                try
                {
                    choose = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("Write correct nums!!");
                }
                switch (choose)
                {
                    case 0:
                        ShowStats();
                        ENTER();
                        Console.Clear();
                        break;
                    case 1:
                        Console.Clear();
                        ProcessRoom(roomNumber);
                        roomNumber++;
                        text = $@"Wait, the next room is loading..";
                        humanType(text);
                        Thread.Sleep(2500);
                        Console.Clear();
                        break;
                    case 2:
                        UsePotion();
                        break;
                    case 3:
                        Console.Clear();
                        VisitAltar();
                        ENTER();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        VisitMerchant();
                        ENTER();
                        Console.Clear();
                        break;
                }
                //roomNumber++;
            }
            if (playerHP < 0) EndGame(false);
            else if (playerHP > 0 && roomNumber >= roomCount)
            {
                FightBoss();
            }
        }
        static void ProcessRoom(int roomNumber)
        //– обрабатывает событие в комнате.
        {
            Console.WriteLine("U went to next room and...");
            Console.WriteLine("⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬ ⟭⟬");
            int rand = random.Next(0, 4);
            int roomIs = rand / 1; //need change the chance
            int meetDarkMageChance = random.Next(0, 100 + 1);
            //meet Dark Mage B4 went to new room
            if (meetDarkMageChance % 25 == 0) MeetDarkMage();

            // 0 - figth with monster's;
            // 1 - fall into a trap;
            // 2 - empty room;
            // 3(def) - chest
            switch (roomIs)
            {
                case 0:
                    Console.WriteLine("Danger! Monster behind u!!");
                    //maybe needly write code about It was worth choosing weapon here
                    FightMonster(random.Next(20, 50 + 1), random.Next(5, 15 + 1));
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

                    Thread.Sleep(750);
                    string text=("Well, well, well.");
                    humanType(text);
                    Thread.Sleep(500);
                    text = ("Look");
                    humanType(text);
                    Thread.Sleep(500);
                    text = "U found the chest.";
                    humanType(text);
                    Thread.Sleep(250);
                    text = "Do u want open him? (def: 'yes')";
                    humanType(text);
                    string answer = Console.ReadLine();
                    string ans = answer.ToLower();
                    bool boolean = (ans == "y" || ans == "yes" || ans == "" ? true : false);
                    if (boolean)
                    {
                        OpenChest();
                        Console.Write("\n");
                    }
                    break;
            }
            //roomNumber++;
        }

        static void FightMonster(int monsterHP, int monsterAttack)
        //– бой с монстром.
        {
            Console.WriteLine($"Monster have {monsterHP} HP & {monsterAttack} ATK.");
            int monsterMaxHP = monsterHP;
            while (monsterHP > 0 && playerHP > 0)
            {
                Console.WriteLine($@"Choose:
    1. Heal
    2. Attack");
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        UsePotion();
                        playerAttack = 0;
                        monsterHP -= playerAttack;
                        playerHP -= monsterAttack;
                        Console.WriteLine($@"U didn't have time to choose a weapon and missed a hit
PLayer HP: {playerHP}/{playerMaxHP} (-{monsterAttack})
Monster HP: {monsterHP}/{monsterMaxHP} (-{playerAttack})
");
                        break;

                    case "2":
                        playerAttack = PlayerAttack(random.Next(10, 20 + 1), random.Next(5, 15 + 1)) + bonusAttack;
                        // sword/bow attack (10-20/5-15)
                        monsterHP -= playerAttack;
                        playerHP -= monsterAttack;
                        Console.Write($@"   PLayer HP: {playerHP}/{playerMaxHP} (-{monsterAttack})
Monster HP: {monsterHP}/{monsterMaxHP} (-{playerAttack})
");
                        break;
                    default:
                        playerAttack = 0;
                        monsterHP -= playerAttack;
                        playerHP -= monsterAttack;
                        Console.WriteLine($@"U hesitated to choose a weapon and missed a hit
PLayer HP: {playerHP}/{playerMaxHP} (-{monsterAttack})
Monster HP: {monsterHP}/{monsterMaxHP} (-{playerAttack})
");
                        break;
                }
            }
            if (playerHP <= 0) EndGame(false);
            else Console.WriteLine("Congratulation! U win the monster");
            
        }
        static int PlayerAttack(int swordAttack, int bowAttack)
        {
            Console.WriteLine("Choose with what weapon u want attack: ");
            for (int i = 1; i < equipments.Count + 1; i++)
            {
                Console.WriteLine($@"   {i}. {equipments[i - 1]}");
            }
            int choose = int.Parse(Console.ReadLine());
            // 1 - sword
            // 2 - bow
            switch (choose)
            {
                case 1:
                    return swordAttack;
                case 2:
                    {
                        if (arrow > 0)
                        {
                            arrow--;
                            return bowAttack;
                        }
                        else
                        {
                            Console.WriteLine($@"U don't have any arrows left.
U couldn't attack and missed the attack.");
                            return 0;
                        }
                    }
                default:
                    {
                        Console.WriteLine($@"U hesitated to choose a weapon and missed a hit");
                        return 0;
                    }
            }
        }
        static int fallingToTrap()
        //falling up
        {
            return random.Next(5, 20+1);
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
            // -1(def) - common chest (90%)
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
            string text = $@"Hola. What do u want to buy?
U can buy potions (10 gold) or arrows (5 gold for 3 pieces).";
            humanType(text);
            Console.WriteLine($@"
Potion - 'p'/'potion'
Arrows - 'a'/'arrows'
Cancel - 'e'/'exit'
");
            string str = Console.ReadLine();
            string purchase = str.ToLower();
            int boolean = qwe(purchase);
            for (int i = 0; i < 2; i++)
                if (boolean == 0)
                {
                    i = 1;
                    buyPAC(purchase);
                }
                else
                    while (purchase != "" || boolean == 0)
                    {
                        str = Console.ReadLine();
                        purchase = str.ToLower();
                        boolean = qwe(purchase);
                        if (boolean == 0) break;
                    }
        }
        static int qwe(string purchase)
        {
            int boolean;
            boolean = ((purchase == "p" || purchase == "potion") || (purchase == "a" || purchase == "arrow") || (purchase == "e" || purchase == "exit")) ? 0 : 1;
            return boolean;
        }
        static void buyPAC(string purchase)
        {
            if (purchase == "p" || purchase == "potion")
            {
                if (gold >= 10)
                {
                    gold -= 10;
                    potion++;
                    Console.WriteLine($@"Tnks for buying
    Potion: {potion} (+1)");
                }
                else Console.WriteLine($@"Ohh, u poor, gone away!");
            }
            else if (purchase == "a" || purchase == "arrow")
            {
                if (gold >= 5)
                {
                    gold -= 5;
                    arrow += 3;
                    Console.WriteLine($@"Tnks for buying
    Arrow: {arrow} (+3)");
                }
                else Console.WriteLine(($@"Ohh, u poor, gone away!"));
            }
            else
            {
                Console.WriteLine("EXIT");
            }
        }
        static void VisitAltar()
        //– усиление персонажа за золото.
        {
            Console.WriteLine($@"Wow, I see u want to upgrade ur character?
Well, if u provide 10 gold, u can upgrade one of the stats to choose from.");
            if (gold >= 10)
            {
                Console.WriteLine("1. Upgrade");
            }
            Console.WriteLine("2. I'll come back later");
            int choose = int.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    if (gold < 10) Console.WriteLine("Hey, don't cheat. Ok?");
                    else upgrading();
                    break;
                default:
                    break;

            }
        }
        static void upgrading()
        {
            Console.WriteLine($@"Choose what you want to upgrade
    1. STR - stength (+5)
    2. VIT - vitalyty (+10)");
            int choose = int.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    bonusAttack += 5;
                    gold -= 10;
                    break;
                case 2:
                    playerHP += 10;
                    playerMaxHP += 10;
                    gold -= 10;
                    break;
            }
        }
        static void MeetDarkMage()
        //– взаимодействие с таинственным магом.
        {
            string text=($@"Buddy, do you want a deal? (def: no)
Sacrifice 10 HP to get 2 potions and 5 arrows.
");
            humanType(text);
            string ans = Console.ReadLine();
            bool boolean = (ans == "n" || ans == "no" || ans == ""? true : false);

            if (!boolean)
            {
                text = ($@"WoW, and u're not a fool.
Well, get ready");
                humanType(text);
                text = "1... 2... 3...";
                int delay;
                for (int i = 0; i < text.Length; ++i)
                {
                    if (i > 0 && text[i] == text[i - 1]) delay = 400;
                    else delay = random.Next(300, 400);
                    Thread.Sleep(delay);
                    Console.Write(text[i]);
                }
                Console.Write("\n");
                text = ($@"Tadam! Here you go, your potions and arrows.
Well, now do whatever you want. Take care of yourself and goodbye");
                humanType(text);
                // -10HP after the exchange 
                playerHP -= 10;
                playerMaxHP -= 10;
                potion += 2;
                arrow += 5;
                Console.WriteLine($@"U find some potion & arrow.
    Potion: {potion} (+2)
    Arrow: {arrow} (+5)");
            }
            else
            {
                text=("Well, whatever you want. Take care of yourself and goodbye");
                humanType(text);
            }
            Console.Write("\n");
        }
        static void UsePotion()
        //– восстановление здоровья.
        {
            if (playerHP < playerMaxHP)
            {
                playerHP += potionHeal;
                if (playerHP > playerMaxHP)
                {
                    playerHP = playerMaxHP;
                }
                potion--;

                Console.WriteLine($@"You just spent one healing potion.
    PLayer HP: {playerHP}/{playerMaxHP} HP");
            }
            else Console.WriteLine("U already have a full HP");
        }
        static void ShowStats()
        //– вывод характеристик игрока.
        {
            string text = ($@"
    - Health: {playerHP} HP
    - Max HP: {playerMaxHP} HP
    - Gold: {gold}
    - Potion: {potion} (heal {potionHeal} HP)
    - Arrow: {arrow}
    - Weapon: ");
            humanType(text);
            foreach (var equips in equipments)
            {
                text = ($"        ∙{equips}");
                humanType(text);
            }
            //List<string> equipments = new List<string> { "меч", "лук" };
            Console.Write("\n\n");
        }
        static void ENTER()
        {
            Console.WriteLine($@"
Press ENTER to continue");
            string str = Console.ReadLine();
            // game will not start until the player presses the enter key
            for (int i = 0; i < 2; i++)
                if (str == "")
                {
                    i = 1;
                }
                else
                    while (str != "") str = Console.ReadLine();
        }
        static void FightBoss()
        //– битва с финальным боссом.
        {
            Console.Clear();
            int bossHP = 100;
            int bossMaxHP = bossHP;
            int bossHeal = 10;
            int step = 0;
            Thread.Sleep(1000);
            string text = $@"
heh, u currently steel alive?
Maybe u want figth with me?
Lmao, u don't have any choice
So, die..";
            while (bossHP > 0 && playerHP > 0)
            {
                if (step % 3 == 0)
                {
                    int bossHealChance = random.Next(0, 1 + 1);
                    bossHP += (bossHealChance == 1 ? 10 : 0);
                    Console.WriteLine("Boss heal 10 HP");
                }
                Console.WriteLine($@"Choose:
    1. Heal
    2. Attack");
                string choose = Console.ReadLine();
                int bossDoubleAttackChance = random.Next(0, 1 + 1);
                int bossAttack = (bossDoubleAttackChance == 0 ? 1 : 2) * random.Next(15, 25 + 1);
                switch (choose)
                {
                    case "1":
                        UsePotion();
                        playerAttack = 0;
                        bossHP -= playerAttack;
                        playerHP -= bossAttack;
                        Console.WriteLine($@"U didn't have time to choose a weapon and missed a hit
PLayer HP: {playerHP}/{playerMaxHP} (-{bossAttack})
Monster HP: {bossHP}/{bossMaxHP} (-{playerAttack})
");
                        break;
                    case "2":
                        playerAttack = PlayerAttack(random.Next(10, 20 + 1), random.Next(5, 15 + 1)) + bonusAttack;
                        bossHP -= playerAttack;
                        playerHP -= bossAttack;
                        Console.WriteLine($@"   Player HP: {playerHP}/{playerMaxHP} (-{bossAttack})
Boss HP: {bossHP}/{bossMaxHP} (-{playerAttack})");
                        step++;
                        break;
                    default:
                        playerAttack = 0;
                        bossHP -= playerAttack;
                        playerHP -= bossAttack;
                        Console.WriteLine($@"U hesitated to choose a weapon and missed a hit
PLayer HP: {playerHP}/{playerMaxHP} (-{bossAttack})
Monster HP: {bossHP}/{bossMaxHP} (-{playerAttack})
");
                        break;
                }
            }
            if (playerHP <= 0) EndGame(false);
            else EndGame(true);
        }
        static void EndGame(bool isWin)
        //– завершение игры.
        {
            if (isWin == true)
            {
                Console.WriteLine("!!Congratulations. U win!!");
            }
            else
            {
                Console.WriteLine("!!!U DIED!!!");
            }
        }
    }
}