using System.Numerics;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;
<<<<<<< Updated upstream
        private List<item> inventory;
        private List<item> storeinventory;
        private List<Monster> monstercostume;
        private List<item> potioncostume;
=======
        private List<Potion> potionList;
>>>>>>> Stashed changes

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
<<<<<<< Updated upstream

            player = new Player("sd", "전사", 1, 10, 5, 100, 10000);

            inventory = new List<item>();
            
            storeinventory = new List<item>(); // 장비 아이템 관리
            storeinventory.Add(new item("나무꾼의 도끼", "산신령 : 자네,, 그만 좀 빠뜨릴 수 없나..?", ItemType.WEAPON, 2, 0, 0, 500));
            storeinventory.Add(new item("전쟁 도끼", "왠지 모르게 이글거린다", ItemType.WEAPON, 7, 0, 0, 3000));
            storeinventory.Add(new item("피의 울음소리", "유서 깊은 도끼입니다.", ItemType.WEAPON, 20, 0, 0, 10000));
            storeinventory.Add(new item("서리한", "서리한이 굶주렸다.", ItemType.WEAPON, 40, 0, 0, 50000));

            storeinventory.Add(new item("천 갑옷", "평범한 갑옷입니다.", ItemType.ARMOR, 0, 5, 10, 1000));
            storeinventory.Add(new item("파수꾼의 갑옷", "전쟁에서 생존한 병사의 갑옷입니다.", ItemType.ARMOR, 0, 15, 30, 4500));
            storeinventory.Add(new item("망자의 갑옷", "죽은 자는 말이 없다.", ItemType.ARMOR, 0, 30, 50, 15000));
            storeinventory.Add(new item("워모그의 갑옷", "하하 회복이다!!", ItemType.ARMOR, 0, 50, 100, 100000));

            monstercostume = new List<Monster>(); // 몬스터 종류 관리
            monstercostume.Add(new Monster(2, "미니언", MonsterType.Nomal, 5, 15));
            monstercostume.Add(new Monster(5, "대포미니언", MonsterType.Nomal, 8, 25));
            monstercostume.Add(new Monster(3, "공허충", MonsterType.Nomal, 9, 10));

            potioncostume = new List<item>(); // 포션 아이템 관리

=======
            player = new Player("CHAD", "전사", 1, 10, 5, 100, 1500);

            potionList = new List<Potion>(); // 포션 종류 관리
            
>>>>>>> Stashed changes
        }

        public void StartGame()
        {
            Console.Clear();
            ConsoleUtility.PrintGameHeader();
            MainMenu();
        }

        //private void CharCreateMenu()
        //{
        //    Console.Clear();

        //    Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        //    Console.WriteLine("원하시는 이름을 설정해주세요.");
        //    Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
        //    Console.WriteLine("");
        //    Console.WriteLine("원하시는 이름을 입력해주세요 : ");

        //    player.Name = Console.ReadLine();
        //    MainMenu();
        //}

        private void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 전투 시작");
            Console.WriteLine("5. 포션 아이템");
            Console.WriteLine("");

            int choice = ConsoleUtility.PromptMenuChoice(1, 5);
            switch (choice)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    inventoryMenu();
                    break;
                case 3:
                    StoreMenu();
                    break;
                case 4:
                    BattleMenu();
                    break;
                case 5:
                    HealMenu();
                    break;
            }
            MainMenu();
        }

        private void HealMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ POTION ■");
            Console.WriteLine("");
            Console.WriteLine("포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : 3 )");

            Console.WriteLine("");
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    
                    break;
            }
        }

        Random random = new Random(); // 랜덤 함수 

        private void BattleMenu() //전투 창 관리
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ Battle!! ■");
            Console.WriteLine("");

            for (int i = 0; i < random.Next(1, 4); i++) // 랜덤하게 몬스터 생성
            {
                monstercostume[i].PrintMonsterStatDescription();
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("[내정보]");
            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.Write($"   {player.Name} ( {player.Job} )");
            Console.WriteLine("");
            ConsoleUtility.PrintTextHighlights("HP. ", player.Hp.ToString());
            ConsoleUtility.PrintTextHighlights("/",player.Hp.ToString());
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    AttackMenu();
                    break;
            }
        }

        private void AttackMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ Battle!! - choice ■");
            Console.WriteLine("");

            for (int i = 0; i < monstercostume.Count; i++) 
            {
                monstercostume[i].PrintMonsterStatDescription(true, i + 1);
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("[내정보]");
            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.Write($"   {player.Name} ( {player.Job} )");
            Console.WriteLine("");
            ConsoleUtility.PrintTextHighlights("HP. ", player.Hp.ToString());
            ConsoleUtility.PrintTextHighlights("/", player.Hp.ToString());
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, monstercostume.Count);

            switch (keyInput)
            {
                case 0:
                    BattleMenu();
                    break;
                default:
                    monstercostume[keyInput - 1].ToggleMonsterStatus();
                    AttackMenu();
                    break;
            }
        }

        private void StatusMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine("");

            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine("");
            Console.WriteLine($"{player.Name} ( {player.Job} )");

            int bonusAtk = inventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
            int bonusDef = inventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
            int bonusHp = inventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();


            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
            Console.WriteLine("");

            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.Def + bonusDef).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
            Console.WriteLine("");

            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.Hp + bonusHp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");
            Console.WriteLine("");

            ConsoleUtility.PrintTextHighlights("Gold : ", player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 0))
            {
                case 0:
                    MainMenu();
                    break;
            }
        }
        private void inventoryMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        private void EquipMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++) 
            {
                inventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, inventory.Count); // 나가기 버튼 0번 고정, 나머지 버튼 > 1부터

            switch (keyInput)
            {
                case 0:
                    MainMenu();
                    break;
                default:
                    inventory[keyInput - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }
        }

        private void StoreMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < storeinventory.Count; i++)
            {
                storeinventory[i].PrintStoreItemDescription();
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    PurchaseMenu();
                    break;
            }
        }

        private void PurchaseMenu(string? prompt = null)
        {
            if (prompt != null)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle(prompt);
                Thread.Sleep(1000);
            }

            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상점 - 아이템 구매■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), "G");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < storeinventory.Count; i++)
            {
                storeinventory[i].PrintStoreItemDescription(true, i + 1);
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, storeinventory.Count);

            switch (keyInput)
            {
                case 0:
                    StoreMenu();
                    break;
                default:
                    if (storeinventory[keyInput - 1].IsPurchased)
                    {
                        PurchaseMenu("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= storeinventory[keyInput - 1].Price)
                    {
                        player.Gold -= storeinventory[keyInput - 1].Price;
                        storeinventory[keyInput - 1].Purchase();
                        inventory.Add(storeinventory[keyInput - 1]);
                        PurchaseMenu();
                    }
                    else
                    {
                        PurchaseMenu("Gold가 부족합니다.");
                    }
                    break;
            }
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
