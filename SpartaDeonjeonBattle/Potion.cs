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

        public Potion(string name, string desc, PotionType type, int hp)
        {
            Name = name;
            Desc = desc;
            Type = type;
            Hp = hp;
        }
    }
}