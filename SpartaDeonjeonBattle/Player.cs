namespace SpartaDeonjeonBattle
{
    internal class Player
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        public Player(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        public void PrintStatus()
        {
            Console.Write("Lv. ");
            ConsoleUtility.HighlightLine(Level.ToString("00"),ConsoleColor.Green);
            Console.WriteLine($"\n{Name} ( {Job} )");
            Console.Write($"\n공격력 : ");
            ConsoleUtility.HighlightLine(Atk.ToString(), ConsoleColor.Green);
            Console.Write($"\n방어력 : ");
            ConsoleUtility.HighlightLine(Def.ToString(), ConsoleColor.Green);
            Console.Write($"\n체력 : ");
            ConsoleUtility.HighlightLine(Hp.ToString(), ConsoleColor.Green);
            Console.Write($"\nGold : ");
            ConsoleUtility.HighlightTxt(Gold.ToString(), ConsoleColor.Green);
            Console.WriteLine("G");
        }

        public static string NameInput()  //이름 설정
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.Write("원하시는 이름을 설정해주세요.\n>>");
            return Console.ReadLine();
        }

        public static int JobSelect(int min, int max)
        {
            Console.WriteLine("원하시는 직업을 선택해주세요.");
            ConsoleUtility.Getout("뒤로 가기");

            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
            Console.Write(". 전사: ");
            ConsoleUtility.JobStatus(10, 5, 100);

            ConsoleUtility.HighlightTxt("\n2", ConsoleColor.Green);
            Console.Write(". 마법사: ");
            ConsoleUtility.JobStatus(20, 3, 60);

            ConsoleUtility.HighlightTxt("\n3", ConsoleColor.Green);
            Console.Write(". 도적: ");
            ConsoleUtility.JobStatus(15, 4, 80);
            Console.Write("\n>>");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요");
                ConsoleUtility.HighlightTxt(">>", ConsoleColor.Yellow);
            } 
        }

        public enum JobList
        {
            ReName,
            Warrior,
            Wizard,
            Thieves
        }
    }
}