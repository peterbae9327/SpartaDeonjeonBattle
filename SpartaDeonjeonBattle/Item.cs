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
    }
}