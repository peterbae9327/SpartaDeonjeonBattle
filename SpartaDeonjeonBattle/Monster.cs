namespace SpartaDeonjeonBattle
{
    public enum MonsterType
    {
        Nomal, Boss
    }

    internal class Monster
    {
        public string Name { get; }
        public int Atk { get; }
        public int Hp { get; }

        private MonsterType Type;
        public bool IsLife { get; private set; }
        public bool IsDead { get; private set; }

        public Monster(string name, MonsterType type, int atk, int hp, bool isLife = false, bool isDead = false)
        {
            Name = name;
            Type = type;
            Atk = atk;
            Hp = hp;
            IsLife = isLife;
            IsDead = isDead;
        }
        internal void ToggleMonsterStatus()
        {
            IsLife = !IsLife;
        }

        internal void Battle()
        {
            IsLife = true;
        }
    }
}