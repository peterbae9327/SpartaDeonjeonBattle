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

        public Monster(int level, string name, int atk, int hp, bool isLife = false)
        {
            Level = level;
            Name = name;
            Atk = atk;
            Hp = hp;
            IsLife = isLife;
        }

        /// <summary>
        /// 몬스터의 상태를 출력해줍니다.
        /// 예:) IsLife = true => Lv.2 미니언  HP 15 .
        ///      IsLife = false => Lv.2 미니언  Dead (Color : DarkGray) .
        /// </summary>
        internal void MonsterStatuPrint(int idx = 0)
        {
            if (IsLife == true)
            {
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(Level.ToString(), ConsoleColor.Green);
                Console.Write($" {Name} ");
                Console.Write(" HP "); Console.WriteLine(Hp);
            }
            else if(IsLife == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(Level.ToString(), ConsoleColor.Green);
                Console.Write($" {Name} ");
                Console.Write(" HP "); Console.WriteLine(Hp);
                Console.ResetColor();
            }
            
        }

    }
}
