
using SpartaDungeonBattle;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static SpartaDeonjeonBattle.Player;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;
        private Potion potion;
        private Quest quest = new Quest();
        string playerName = Player.NameInput();
        private Battle battle;
        private List<Item> inventoryitemlist;

        public GameManager()
        {
            StartGame();
        }

        private void InitializeGame()
        {
            quests = InitializeQuest();
            inventoryitemlist = new List<Item>(); // 인벤토리 아이템 리스트 관리
            inventoryitemlist.Add(new Item("개발자의 키보드", "테스트용 무기", ItemType.WEAPON, 100, 0, 0, 500));
            inventoryitemlist.Add(new Item("개발자의 후드티", "테스트용 방어구", ItemType.ARMOR, 0, 100, 0, 500));
            potion = new Potion("힐 포션", "체력 30 회복", 30, 3);
            JobMenu();
        }

        public void StartGame()
        {
            Console.Clear();
            
            InitializeGame();
        }

        private void JobMenu()
        {
            Console.Clear();
            JobList choice = (JobList)Player.JobSelect(0, 4);

            switch (choice)
            {
                case JobList.ReName:
                    StartGame();
                    break;
                case JobList.Warrior:
                    player = new Player(playerName, "전사", 1, 10, 5, 100, 50, 1500, 0, 0);
                    battle = new Battle(player, this, potion, quest, inventoryitemlist);
                    break;
                case JobList.Wizard:
                    player = new Player(playerName, "마법사", 1, 20, 3, 60, 100, 1500, 0, 0);
                    battle = new Battle(player, this, potion, quest, inventoryitemlist);
                    break;
                case JobList.Thieves:
                    player = new Player(playerName, "도적", 1, 15, 4, 80, 50, 1500, 0, 0);
                    battle = new Battle(player, this, potion, quest, inventoryitemlist);
                    break;
            }
            MainMenu();
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
            Console.WriteLine(". 상태 보기");
            ConsoleUtility.HighlightTxt("2", ConsoleColor.Green);
            Console.Write(". 전투 시작"); 
            Console.Write(" (현재 진행 : "); ConsoleUtility.HighlightTxt(battle.dngeonStage.ToString(), ConsoleColor.Green); Console.WriteLine(" 층)");
            ConsoleUtility.HighlightTxt("3", ConsoleColor.Green);
            Console.WriteLine(". 인벤토리");
            ConsoleUtility.HighlightTxt("4", ConsoleColor.Green);
            Console.WriteLine(". 회복 아이템");
            ConsoleUtility.HighlightTxt("5", ConsoleColor.Green); //퀘스트 메뉴
            Console.WriteLine(". 퀘스트목록");

            Stage choice = (Stage)ConsoleUtility.MenuChoice(1, 5);
            switch (choice)
            {
                case Stage.Status:
                    Status();
                    break;
                case Stage.Deonjeon:
                    battle.RandomMonster();  // 메인 메뉴에서 전투 시작을 누를 때만 새로운 몬스터들을 생성합니다.
                    battle.BattleMenu();
                    break;
                case Stage.Inventory:
                    InventoryMenu();
                    break;
                case Stage.Healmenu: 
                    HealMenu();
                    break;
                case Stage.Quest: //퀘스트 메뉴
                    QuestMenu();
                    break;
            }
        }

        private void HealMenu(string? prompt = null) // 회복 메뉴 관리
        {
            if (prompt != null)
            {
                Console.Clear();
                ConsoleUtility.ShowTitle(prompt);
                Thread.Sleep(1000);
            }

            Console.Clear();
            ConsoleUtility.ShowTitle("회복");
            Console.WriteLine();

            potion.PoctionDecription();

            Console.WriteLine();
            Console.WriteLine();

            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
            Console.Write(". 사용하기");
            ConsoleUtility.Getout("나가기");

            int keyInput = ConsoleUtility.ObjectChoice(0, 1);

            switch (keyInput) // 0번 입력시 메인 메뉴 이동, 1번 입력시 포션 사용
            {
                case 0:
                    MainMenu();
                    break;
                default:
                    if (player.Hp >= player.MaxHp && potion.Quantity != 0) // 플레이어 체력이 풀피라면 회복 불가
                    {
                        HealMenu("이미 체력이 Max입니다.");
                    }
                    else if (player.Hp < player.MaxHp && potion.Quantity != 0) // 플레이어 체력이 풀피가 아니면 체력 회복 후 포션 1개씩 감소
                    {
                        potion.Quantity--;
                        player.Hp += 30;
                        if (player.Hp > player.MaxHp) // 최대 체력보다 높게 회복 될시 현재 체력 값 - (최대 체력을 넘어간 수치 만큼)
                        {
                            int minus = 0;
                            minus = player.Hp - player.MaxHp;
                            player.Hp -= minus;
                        }
                        HealMenu("체력을 30 회복하였습니다.");
                    }
                    else if (potion.Quantity == 0) 
                    {
                        HealMenu("포션이 부족합니다.");
                    }
                    break;
            };
        }
        private void QuestMenu()
        {
            UpdateQuest();
            quest.LoadQuestList(quests);
            RewardQuest();
            MainMenu();
        }

        public void InventoryMenu() // 인벤토리 관리 메뉴
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventoryitemlist.Count; i++)
            {
                inventoryitemlist[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            ConsoleUtility.HighlightTxt("1. ", ConsoleColor.Green);
            Console.Write("장착관리");
            ConsoleUtility.Getout("나가기");

            switch (ConsoleUtility.MenuChoice(0, 1))
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        private void EquipMenu() // 인벤토리 장착 관리 메뉴
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventoryitemlist.Count; i++)
            {
                inventoryitemlist[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            ConsoleUtility.HighlightTxt("", ConsoleColor.Green);
            ConsoleUtility.Getout("나가기");

            int keyInput = ConsoleUtility.MenuChoice(0, inventoryitemlist.Count);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    inventoryitemlist[keyInput - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }

        }

        public void PrintStatus()
        {
            Console.Write("Lv. ");
            ConsoleUtility.HighlightLine(player.Level.ToString("00"), ConsoleColor.Green);
            Console.WriteLine($"{player.Name} ( {player.Job} )");

            int bonusAtk = inventoryitemlist.Select(item => item.isEquipped ? item.Atk : 0).Sum();
            int bonusDef = inventoryitemlist.Select(item => item.isEquipped ? item.Def : 0).Sum();
            int bonusHp = inventoryitemlist.Select(item => item.isEquipped ? item.Hp : 0).Sum();

            player.BonusAtk = bonusAtk;
            player.BonusDef = bonusDef;

            Console.Write($"공격력 : ");
            ConsoleUtility.PrintTextHighlights((player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
            Console.WriteLine();

            Console.Write($"방어력 : ");
            ConsoleUtility.PrintTextHighlights((player.Def + bonusDef).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
            Console.WriteLine();

            Console.Write($"체력 : ");
            ConsoleUtility.PrintTextHighlights((player.Hp + bonusHp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");
            Console.WriteLine();

            Console.Write($"마력 : "); 
            Console.Write(player.Mp);
            Console.WriteLine();

            Console.Write($"Gold : ");
            ConsoleUtility.HighlightTxt(player.Gold.ToString(), ConsoleColor.Green);
            Console.WriteLine("G");
        }

        private void Status()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            PrintStatus();
            ConsoleUtility.Getout("나가기");
            ConsoleUtility.MenuChoice(0, 0);
            MainMenu();

        }
        //퀘스트 내역

        List<QuestDB> quests;

        public List<QuestDB> InitializeQuest()
        {//퀘스트 데이터 베이스 
            QuestDB quest1 = new QuestDB();
            QuestDB quest2 = new QuestDB();
            QuestDB quest3 = new QuestDB();
            quest1.Title = "마을을 위협하는 미니언 처치";
            quest1.ExplainText = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
                                 "마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                                 "모험가인 자네가 좀 처치해주게!";
            quest1.Target = Battle.MONSTERTYPE.MINION.ToString();
            quest1.TargetNumber = 5;
            quest1.GoalText = $"미니언 5마리 처치";
            quest1.CurrentNumber = 0;
            quest1.Rewards.Add(new Reward(10,null,"Gold"));
            quest1.Rewards.Add(new Reward(1, new Item("쓸만한방패","아주 아주 쓸만합니다",ItemType.ARMOR,0,10,0,800), null));

            quest2.Title = "장비를 장착해보자";
            quest2.ExplainText = "장비야말로 모험가에게 필수적이지!\n" +
                                 "장비없는 스파르타전사는 어느동네 야만전사만도 못하다네.";
            quest2.GoalText = "아무 장비나 장착해보기";
            quest2.Rewards.Add(new Reward(10, null, "Gold"));
            quest3.Title = "더욱 더 강해지기!";
            quest3.ExplainText = "\'나는 자랑스런 필승의 스파르타군이다.\'\n" +
                                 "\'안되면 되게하라!\'";
            quest3.Target = "Atk";
            quest3.TargetNumber = 25;
            quest3.GoalText = "공격력 25 달성";
            quest3.Rewards.Add(new Reward(10, null, "Gold"));
            // 이후 퀘스트는 CloseQuest = true로 설정
            List<QuestDB> quests = new List<QuestDB>() { quest1, quest2, quest3 };
            return quests;
        }
        public void UpdateQuest()
        {
            //int형 목표치 반영
            List<Monster> monsterlog = battle.ReportMonsterLog();
            for(int i = 0; i < quests.Count; i++)
            {
                switch (quests[i].Target)
                {
                    case "Level":
                        quests[i].CurrentNumber = player.Level;
                        break;
                    case "Atk":
                        quests[i].CurrentNumber = player.Atk+player.BonusAtk;
                        break;
                    case "Def":
                        quests[i].CurrentNumber = player.Def+player.BonusDef;
                        break;
                    case "Hp":
                        quests[i].CurrentNumber = player.Hp;
                        break;
                    case "Gold":
                        quests[i].CurrentNumber = player.Gold;
                        break;
                    case null:
                        break;
                    default:
                        for (int j = 0; j < monsterlog.Count; j++)
                        {
                            if (monsterlog[j].Name == quests[i].Target)
                            {
                                quests[i].CurrentNumber++;
                            }
                        }
                        break;
                }
                if (quests[i].TargetNumber <= quests[i].CurrentNumber)
                {
                    quests[i].ClearQuest = true;
                }
            }
            //bool형 목표 반영
            for (int i = 0; i < inventoryitemlist.Count; i++)
            {
                if (inventoryitemlist[i].isEquipped)
                {
                    quests[1].ClearQuest = true;
                    break;
                }
            }
        }
        public void RewardQuest()
        {
            for(int i = 0;i<quests.Count;i++)
            {
                if (quests[i].CloseQuest && quests[i].ClearQuest && quests[i].AcceptQuest)
                {
                    for(int j = 0; j < quests[i].Rewards.Count;j++) 
                    {
                        if (quests[i].Rewards[j].Item == null&& quests[i].Rewards[j].ItemName =="Gold")
                        {
                            player.Gold += quests[i].Rewards[j].ItemQuantity;
                        }
                        else
                        {
                            inventoryitemlist.Add(quests[i].Rewards[j].Item);
                        }
                    }
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gamemanager = new GameManager();
            gamemanager.StartGame();
        }
    }
    public enum Stage
    {
        Main,
        Status,
        Deonjeon,
        Inventory,
        Healmenu,
        Quest,
    }
}
