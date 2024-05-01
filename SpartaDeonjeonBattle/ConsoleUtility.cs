namespace SpartaDeonjeonBattle
{
    internal class ConsoleUtility
    {
        public static int MenuChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요");
                HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }
        public static int ObjectChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요");
                HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }
        public static void Getout(string action)
        {
            Console.WriteLine();
            HighlightTxt("0",ConsoleColor.Green);
            Console.WriteLine(". " + action);
        }
        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(title);
            Console.ResetColor();
        }
        public static void HighlightTxt(string highlighted, ConsoleColor choosecolor)
        {//번호, 숫자 = Green / 몬스터사망 = DarkGray / 타이틀 = Yellow / 몬스터선택 = Cyan
            Console.ForegroundColor = choosecolor;
            Console.Write(highlighted);
            Console.ResetColor();
        }
        public static void HighlightLine(string highlighted, ConsoleColor choosecolor)
        {//번호, 숫자 = Green / 몬스터사망 = DarkGray / 타이틀 = Yellow / 몬스터선택 = Cyan
            Console.ForegroundColor = choosecolor;
            Console.Write(highlighted);
            Console.ResetColor();
        }

    }
}