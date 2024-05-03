namespace SpartaDeonjeonBattle
{
    internal class ConsoleUtility
    {
        public static int MenuChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("�߸��� �Է��Դϴ�");
                HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }
        public static int ObjectChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("����� �������ּ���");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("�߸��� �Է��Դϴ�");
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

        public static int GetPrintableLength(string str) // 아이템 목록 가시성을 위한 글자수 제한 함수
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

        public static string PadRightForMixedText(string str, int totalLength) // 위에 함수랑 연동
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}