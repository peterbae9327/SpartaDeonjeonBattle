namespace SpartaDeonjeonBattle
{
    public enum PotionType
    {
        HEAL
    }

    internal class Potion
    {
        public string Name { get; }
        public string Desc { get; }

        private PotionType Type;

        public int Hp { get; }
        public int Quantity { get; set; }

        // 왼쪽부터 포션 이름, 설명, 타입, 타입 별 효과, 현재 수량
        public Potion(string name, string desc, PotionType type, int hp, int quantity) 
        {
            Name = name;
            Desc = desc;
            Type = type;
            Hp = hp;
            Quantity = quantity;
        }
    }
}