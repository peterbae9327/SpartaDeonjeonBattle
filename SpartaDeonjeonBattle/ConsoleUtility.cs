namespace SpartaDeonjeonBattle
{
    internal class ConsoleUtility
    {
        public static int MenuChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("¿øÇÏ½Ã´Â Çàµ¿À» ÀÔ·ÂÇØÁÖ¼¼¿ä.");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("Àß¸øµÈ ÀÔ·ÂÀÔ´Ï´Ù");
                HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }
        public static int ObjectChoice(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("´ë»óÀ» ¼±ÅÃÇØÁÖ¼¼¿ä");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("Àß¸øµÈ ÀÔ·ÂÀÔ´Ï´Ù");
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

        public static void HighlightPart(string s1, string s2, ConsoleColor choosecolor, string s3 = "")  //í•œ ì¤„ ë‚´ì—ì„œ ì¼ë¶€ë§Œ ê°•ì¡°
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
        public static int GetPrintableLength(string str) // ì•„ì´í…œ ëª©ë¡ ê°€ì‹œì„±ì„ ìœ„í•œ ê¸€ììˆ˜ ì œí•œ í•¨ìˆ˜
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

        public static string PadRightForMixedText(string str, int totalLength) // ìœ„ì— í•¨ìˆ˜ë‘ ì—°ë™
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}