
using System.Numerics;
using System.Xml.Linq;

namespace SpartaDungeonBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }

    public class GameManager    //전반적인 게임 진행 관리
    {
        private Player player;
        private List<Item> inventory;
        //private Monster monster;

        public GameManager()    //생성자
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            player = new Player("Seoyeong", "Wizard", 1, 10, 3, 8, 7000);   //Player 생성자를 통해 player 객체 생성
            inventory = new List<Item>();   //Item 클래스 타입의 리스트
        }

        public void StartGame()
        {
            Console.Clear();
            //ConsoleUtility.PrintGameHeader();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작\n");

            int choice = ConsoleUtility.PromptMenuChoice(1, 2);
            switch (choice)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    Console.Clear();
                    BattleMenu();
                    break;
            }
        }

        private void StatusMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine($"{player.Name} ({player.Job})");
            //할 것: 능력치 강화분을 표현하도록 변경
            ConsoleUtility.PrintTextHighlights("공격력: ", player.Atk.ToString());
            ConsoleUtility.PrintTextHighlights("방어력: ", player.Def.ToString());
            ConsoleUtility.PrintTextHighlights("체  력: ", player.Hp.ToString());
            ConsoleUtility.PrintTextHighlights("Gold: ", player.Gold.ToString(), " G");
            Console.WriteLine("");

            Console.WriteLine("0. 나가기\n");

            switch (ConsoleUtility.PromptMenuChoice(0, 0))
            {
                case 0:
                    MainMenu();
                    break;
            }
        }

        private void BattleMenu()
        {
            Console.Clear();
        }

        //public void Battle()
        //{

        //}
    }
}
