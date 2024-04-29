namespace SpartaDungeonBattle
{
    internal class ConsoleUtility
    {
        public static int PromptMenuChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                //할 것: 잘못된 입력입니다 중복 출력 삭제
                //Console.Clear();
                //GameManager.MainMenu();   //프라이빗이라 접근 불가능
                Console.WriteLine("잘못된 입력입니다.\n");
            }
        }

        internal static void ShowTitle(string title)    //타이틀 빨강색 강조
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        public static void PrintTextHighlights(string s1, string s2, string s3 = "")  //한 줄 내에서 일부만 노랑색 강조
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
    }
}