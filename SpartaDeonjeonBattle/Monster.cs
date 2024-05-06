using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDeonjeonBattle
{

    internal class Monster
    {

        public int Level { get; set; }
        public string Name { get; set; }
        public int Atk { get; set; }
        public int Hp { get; set; }
        public bool IsLife { get; set; }
        public int Gold {  get; set; }

        public Monster(int level, string name, int atk, int hp, bool isLife, int gold)
        {
            Level = level;
            Name = name;
            Atk = atk;
            Hp = hp;
            IsLife = isLife;
            Gold = gold;
        }

        /// <summary>
        /// 몬스터의 상태를 출력해줍니다.
        /// 예:) IsLife = true => Lv.2 미니언  HP 15 .
        ///      IsLife = false => Lv.2 미니언  Dead (Color : DarkGray) .
        /// </summary>
        internal void MonsterStatuPrint()
        {
            if (IsLife == true)
            {
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(Level.ToString(), ConsoleColor.Green);
                Console.Write($" {Name} ");
                Console.Write(" HP "); ConsoleUtility.HighlightLine(Hp.ToString(), ConsoleColor.Green);
            }
            else if(IsLife == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Lv."); Console.Write(Level);
                Console.Write($" {Name} ");
                Console.Write(" HP "); HpPrint(); Console.WriteLine();
                Console.ResetColor();
            }
            
        }

        /// <summary>
        /// 입력된 값 만큼 HP가 감소합니다.
        /// 데미지는 10% 오차를 가집니다.
        /// HP가 바닥나면 => IsLife = false
        /// 받은 데미지를 반환합니다.
        /// </summary>
        public int TakeDamage(int atk)
        {
            Random random = new Random();
            int rand = random.Next(0, 3);
            float errorDamage = ((float)atk / 10);
            if (rand == 0)
            {
                Hp -= atk + (int)Math.Ceiling(errorDamage);
                if (Hp <= 0) IsLife = false;
                return atk + (int)Math.Ceiling(errorDamage);
            }
            else if (rand == 1)
            {
                Hp -= atk - (int)Math.Ceiling(errorDamage);
                if (Hp <= 0) IsLife = false;
                return atk - (int)Math.Ceiling(errorDamage);
            }
            else if (rand == 2)
            {
                Hp -= atk;
                if (Hp <= 0) IsLife = false;
                return atk;
            }

            return atk;
        }

        /// <summary>
        /// 몬스터의 체력을 출력합니다.
        /// 죽었다면 Dead을 출력합니다.
        /// </summary>
        public void HpPrint()
        {
            if (Hp > 0) ConsoleUtility.HighlightTxt(Hp.ToString(), ConsoleColor.Green);
            else if(Hp <= 0) ConsoleUtility.HighlightTxt("Dead", ConsoleColor.DarkGray);
        }
    }
}
