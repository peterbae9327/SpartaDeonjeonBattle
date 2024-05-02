
using System.Security.Cryptography.X509Certificates;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;
        private Potion potion;
        private Battle battle;

        public GameManager()
        {
            StartGame();
        }

        private void InitializeGame(string playerName)
        {
            player = new Player( playerName, "전사", 1, 10, 5, 100, 1500);
            battle = new Battle(player, this);
            potion = new Potion("힐 포션", "체력 30 회복", 30, 3);
            MainMenu();
        }

        public void StartGame()
        {
            Console.Clear();
            string playerName = Player.NameInput();
            InitializeGame(playerName);
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
            Console.WriteLine(". 전투 시작");
            ConsoleUtility.HighlightTxt("3", ConsoleColor.Green); // 회복 아이템 TXT 추가
            Console.WriteLine(". 회복 아이템");

            Stage choice = (Stage)ConsoleUtility.MenuChoice(1, 3);
            switch (choice)
            {
                case Stage.Status:
                    Status();
                    break;
                case Stage.Deonjeon:
                    battle.RandomMonster();  // 메인 메뉴에서 전투 시작을 누를 때만 새로운 1~4마리의 몬스터들를 생성합니다.
                    battle.BattleMenu();
                    break;
                case Stage.Healmenu: // 회복메뉴로 이동
                    HealMenu();
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
                        if (player.Hp > 100)
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
        Healmenu
    }
}
