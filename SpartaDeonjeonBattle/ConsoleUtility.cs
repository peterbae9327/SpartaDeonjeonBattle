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
            Console.WriteLine(highlighted);
            Console.ResetColor();
        }

        public static void HighlightPart(string s1, string s2, ConsoleColor choosecolor, string s3 = "")  //한 줄 내에서 일부만 강조
        {
            Console.Write(s1);
            Console.ForegroundColor = choosecolor;
            Console.Write(s2);
            Console.ResetColor();
            Console.Write(s3);
        }

        public static void JobStatus(int atk, int def, int hp)
        {
            ConsoleUtility.HighlightPart("Atk ", atk.ToString(), ConsoleColor.Green);
            ConsoleUtility.HighlightPart(" | Def ", def.ToString(), ConsoleColor.Green);
            ConsoleUtility.HighlightPart(" | Hp ", hp.ToString(), ConsoleColor.Green);
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