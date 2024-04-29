
using System.Security.Cryptography.X509Certificates;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            player = new Player("CHAD", "전사", 1, 10, 5, 100, 1500);
        }

        public void StartGame()
        {
            Console.Clear();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
            Console.WriteLine(". 상태 보기");
            ConsoleUtility.HighlightTxt("2", ConsoleColor.Green);
            Console.WriteLine(". 전투 시작");
            int choice = ConsoleUtility.MenuChoice(1, 2);
            switch (choice)
            {
                case 1:
                    Status();
                    break;
                case 2:
                    //전투시작
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
}
