namespace SpartaDeonjeonBattle
{
    internal class ConsoleUtility
    {
        public static int MenuChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다");
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
                Console.WriteLine("잘못된 입력입니다");
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
        {//Numbers = Green / DeadMonsters= DarkGray / MonsterNumber= Cyan
            Console.ForegroundColor = choosecolor;
            Console.Write(highlighted);
            Console.ResetColor();
        }
        public static void HighlightLine(string highlighted, ConsoleColor choosecolor)
        {//Numbers = Green / DeadMonsters= DarkGray / MonsterNumber= Cyan
            Console.ForegroundColor = choosecolor;
            Console.WriteLine(highlighted);
            Console.ResetColor();
        }

        public static int GetPrintableLength(string str) // �븘�씠�뀥 紐⑸줉 媛��떆�꽦�쓣 �쐞�븳 湲��옄�닔 �젣�븳 �븿�닔
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength) // �쐞�뿉 �븿�닔�옉 �뿰�룞
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}