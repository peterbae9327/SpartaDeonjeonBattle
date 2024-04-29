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
            Console.WriteLine($"{Name} ( {Job} )");
            Console.Write($"공격력 : ");
            ConsoleUtility.HighlightLine(Atk.ToString(), ConsoleColor.Green);
            Console.Write($"방어력 : ");
            ConsoleUtility.HighlightLine(Def.ToString(), ConsoleColor.Green);
            Console.Write($"체력 : ");
            ConsoleUtility.HighlightLine(Hp.ToString(), ConsoleColor.Green);
            Console.Write($"Gold : ");
            ConsoleUtility.HighlightTxt(Gold.ToString(), ConsoleColor.Green);
            Console.WriteLine("G");
        }
    }
}