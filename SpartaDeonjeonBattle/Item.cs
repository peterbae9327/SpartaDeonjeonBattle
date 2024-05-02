using SpartaDeonjeonBattle;

namespace SpartaDungeonBattle
{
    public enum ItemType
    {
        WEAPON, ARMOR
    }

    internal class Item
    {
        public string Name { get; }
        public string Desc { get; }
        private ItemType Type;
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int price { get; }
        public bool isEquipped { get; private set; }    //클래스 내에서만 값 설정
        public bool isPurchased { get; private set; }

        public Item(string name, string desc, ItemType type, int atk, int def, int hp, int price, bool isEquipped = false, bool isPurchased = false)    //아이템 기본 상태: 구매, 장착X
        {
            Name = name;
            Desc = desc;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            this.price = price;
            this.isEquipped = isEquipped;
            this.isPurchased = isPurchased;
        }

        internal void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 20));
            }
            else
            {
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 20));
            }

            Console.Write(" | ");

            if (Atk != 0) Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0) Console.Write($"방어력 {(Atk >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"체  력 {(Atk >= 0 ? "+" : "")}{Hp} ");

            Console.Write(" | ");

            Console.WriteLine(Desc);
        }

    }
}