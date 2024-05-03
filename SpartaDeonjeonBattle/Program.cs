﻿
using System.Security.Cryptography.X509Certificates;
using static SpartaDeonjeonBattle.Player;

namespace SpartaDeonjeonBattle
{
    public class GameManager
    {
        private Player player;
        private Potion potion;
        string playerName = Player.NameInput();

        public GameManager()
        {
            StartGame();
        }

        private void InitializeGame()
        {
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
            ConsoleUtility.HighlightTxt("3", ConsoleColor.Green); // 회복 아이템 TXT 추가
            Console.WriteLine(". 회복 아이템");

            Stage choice = (Stage)ConsoleUtility.MenuChoice(1, 3);
            switch (choice)
            {
                case Stage.Status:
                    Status();
                    break;
                case Stage.Deonjeon:
                    //전투시작
                    break;
                case Stage.Healmenu: // 회복메뉴로 이동
                    HealMenu();
                    break;
            }
        }

        private void HealMenu() // 회복 메뉴 관리
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("회복");
            Console.WriteLine();

            potion.PoctionDecription();

            Console.WriteLine();
            Console.WriteLine();

            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
            Console.Write(". 사용하기");
            ConsoleUtility.Getout("나가기");

            int keyInput = ConsoleUtility.ObjectChoice(0, potion.Quantity);

            switch (ConsoleUtility.ObjectChoice(0, 1)) // 0번 입력시 메인 메뉴 이동, 1번 입력시 포션 사용
            {
                case 0:
                    MainMenu();
                    break;
                default:
                    //if (potion.Quantity[])
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
