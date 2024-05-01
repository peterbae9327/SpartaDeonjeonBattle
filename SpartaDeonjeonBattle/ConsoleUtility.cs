namespace SpartaDeonjeonBattle
{
    internal class ConsoleUtility
    {
        public static void PrintGameHeader()
        {
            Console.WriteLine("====================================================================================");
            Console.WriteLine("      ___________________   _____  __________ ___________ _____ ");
            Console.WriteLine("     /   _____/\\______   \\ /  _  \\ \\______   \\\\__    ___//  _  \\ ");
            Console.WriteLine("     \\_____  \\  |     ___//  /_\\  \\ |       _/  |    |  /  /_\\  \\ ");
            Console.WriteLine("     /        \\ |    |   /    |    \\|    |   \\  |    | /    |    \\ ");
            Console.WriteLine("    /_______  / |____|   \\____|__  /|____|_  /  |____| \\____|__  / ");
            Console.WriteLine("            \\/                   \\/        \\/                  \\/  ");
            Console.WriteLine("________    ____ ___ _______     ________ ___________________    _______  ");
            Console.WriteLine("\\______ \\  |    |   \\\\      \\   /  _____/ \\_   _____/\\_____  \\   \\      \\ ");
            Console.WriteLine(" |    |  \\ |    |   //   |   \\ /   \\  ___  |    __)_  /   |   \\  /   |   \\ ");
            Console.WriteLine(" |    `   \\|    |  //    |    \\\\    \\_\\  \\ |        \\/    |    \\/    |    \\ ");
            Console.WriteLine("/_______  /|______/ \\____|__  / \\______  //_______  /\\_______  /\\____|__  / ");
            Console.WriteLine("        \\/                  \\/         \\/         \\/         \\/         \\/  ");
            Console.WriteLine("====================================================================================");
            Console.WriteLine("                                PRESS ANYKEY TO START                               ");
            Console.WriteLine("====================================================================================");
            Console.ReadKey();
        }

        public static int PromptMenuChoice(int min, int max) // 번호 입력 함수
        {
            while (true)
            {
                Console.Write("원하시는 번호를 입력해주세요 : ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            }
        }

        internal static void ShowTitle(string title) //타이틀 문자 색상 함수
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        public static void PrintTextHighlights(string s1, string s2, string s3 = "") 
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.Write(s3);
        }

        public static int GetPrintableLength(string str)
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

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}