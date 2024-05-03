using System.Reflection.Emit;

namespace SpartaDeonjeonBattle
{
    internal class Potion
    {
        public string Name { get; }
        public string Desc { get; }

        public int Hp { get; }
        public int Quantity { get; set; }

        // 왼쪽부터 포션 이름, 설명, 타입, 타입 별 효과, 현재 수량
        public Potion(string name, string desc, int hp, int quantity) 
        {
            Name = name;
            Desc = desc;
            Hp = hp;
            Quantity = quantity;
        }

        public void PoctionDecription() // 회복 포션에 대한 힐 값과 수량을 불러오기 위한 함수
        {
            Console.Write("포션을 사용하면 체력을 ");
            ConsoleUtility.HighlightTxt(Hp.ToString("00"), ConsoleColor.Green);
            Console.Write(" 회복 할 수 있습니다. ( 남은 포션 : ");
            ConsoleUtility.HighlightTxt(Quantity.ToString("00"), ConsoleColor.Green);
            Console.Write(" )");
        }
    }
}