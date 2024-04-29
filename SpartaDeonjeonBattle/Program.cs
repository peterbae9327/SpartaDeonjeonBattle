
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
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            int choice = ConsoleUtility.MenuChoice(1, 2);
            switch (choice)
            {
                case 1:
                    //상태보기
                    break;
                case 2:
                    //전투시작
                    break;
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
}
