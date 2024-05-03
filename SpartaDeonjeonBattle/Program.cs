
using SpartaDungeonBattle;
using System.Security.Cryptography.X509Certificates;
using static SpartaDeonjeonBattle.Player;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;
        private Potion potion;
        string playerName = Player.NameInput();
        private Battle battle;
        private List<Item> inventoryitemlist;

        public GameManager()
        {
            StartGame();
        }

        private void InitializeGame()
        {
            
            battle = new Battle(player, this);

            inventoryitemlist = new List<Item>(); // 인벤토리 아이템 리스트 관리
            inventoryitemlist.Add(new Item("개발자의 키보드", "테스트용 무기", ItemType.WEAPON, 1000, 0, 0, 500));

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
                    player = new Player(playerName, "전사", 1, 10, 5, 100, 1500);
                    break;
                case JobList.Wizard:
                    player = new Player(playerName, "마법사", 1, 20, 3, 60, 1500);
                    break;
                case JobList.Thieves:
                    player = new Player(playerName, "도적", 1, 15, 4, 80, 1500);
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
            Console.WriteLine(". 회복 아이템");
            ConsoleUtility.HighlightTxt("4", ConsoleColor.Green);
            Console.WriteLine(". 인벤토리");

            Stage choice = (Stage)ConsoleUtility.MenuChoice(1, 4);
            switch (choice)
            {
                case Stage.Status:
                    Status();
                    break;
                case Stage.Deonjeon:
                    battle.RandomMonster();  // 메인 메뉴에서 전투 시작을 누를 때만 새로운 몬스터들을 생성합니다.
                    battle.BattleMenu();
                    break;
                case Stage.Healmenu: 
                    HealMenu();
                    break;
                case Stage.Inventory: 
                    InventoryMenu();
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
                    if (player.Hp == 100 && potion.Quantity != 0) // 플레이어 체력이 100이라면 회복 불가
                    {
                        HealMenu("이미 체력이 100입니다.");
                    }
                    else if (player.Hp < 100 && potion.Quantity != 0) // 플레이어 체력이 100이 아니면 체력 회복 후 포션 1개씩 감소
                    {
                        potion.Quantity--;
                        player.Hp += 30;
                        if (player.Hp > 100) // 최대 체력보다 높게 회복 될시 현재 체력 값 - (최대 체력을 넘어간 수치 만큼)
                        {
                            int minus = 0;
                            minus = player.Hp - 100;
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
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");

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
            Console.WriteLine("0. 나가기");

            int keyInput = ConsoleUtility.MenuChoice(0, inventoryitemlist.Count);

            switch (keyInput)
            {
                case 0:
                    MainMenu();
                    break;
                default:
                    inventoryitemlist[keyInput - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }

        }


        private void Status()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            player.PrintStatus();
            ConsoleUtility.Getout("나가기");
            ConsoleUtility.MenuChoice(0, 0);
            MainMenu();

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
        Healmenu,
        Inventory
    }
}
