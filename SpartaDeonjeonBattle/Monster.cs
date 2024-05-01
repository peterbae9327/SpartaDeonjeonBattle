using System.Diagnostics;

namespace SpartaDeonjeonBattle
{
    public enum MonsterType
    {
        Nomal, Boss
    }

    internal class Monster
    {
        public int Lv { get; }
        public string Name { get; }
        public int Atk { get; }
        public int Hp { get; }

        private MonsterType Type;
        public bool IsLife { get; private set; }
        public bool IsDead { get; private set; }

        public Monster(int lv, string name, MonsterType type, int atk, int hp, bool isLife = false, bool isDead = false)
        {
            Lv = lv;
            Name = name;
            Type = type;
            Atk = atk;
            Hp = hp;
            IsLife = isLife;
            IsDead = isDead;
        }
        internal void PrintMonsterStatDescription(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (Lv != 0) Console.Write($"Lv. {(Lv >= 0 ? "" : "")}{Lv} ");

            Console.Write(ConsoleUtility.PadRightForMixedText(Name, 11));

            if (Hp != 0) Console.Write($"HP {(Hp >= 0 ? " " : "")}{Hp}");

            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Dead");
                Console.ResetColor();
            }
        }
        internal void ToggleMonsterStatus()
        {
            IsLife = !IsLife;
        }
    }
}