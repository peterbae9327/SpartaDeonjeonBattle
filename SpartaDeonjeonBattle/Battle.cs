using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDeonjeonBattle
{
    internal class Battle
    {
        List<Monster> monsters = new List<Monster>();
        private Player player;

        /// <summary>
        /// 전투를 시작하면 1~4마리의 몬스터가 랜덤하게 등장합니다.
        /// </summary>
        public Battle(Player player)
        {
            this.player = player;
            // 전투를 시작하면 1~4마리의 몬스터가 랜덤하게 등장합니다.
            RandomMonster();
        }

        /// <summary>
        /// 전투 시작 메뉴입니다. 
        /// </summary>
        public void BattleMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine();

            //몬스터 상태 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].MonsterStatuPrint();
            }
            Console.WriteLine("\n");

            Console.WriteLine("[내정보]");
            Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
            Console.Write($"  {player.Name} ");
            Console.WriteLine($" ({player.Job})");
            Console.Write("HP "); ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
            
            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green); Console.WriteLine(". 공격");

            switch (ConsoleUtility.MenuChoice(1, 1))
            {
                case 1:
                    MonsterSelect();
                    break;
            }
        }

        /// <summary>
        /// 공격할 몬스터를 선택하는 메뉴입니다.
        /// </summary>
        private void MonsterSelect()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine();

            //몬스터 상태 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsLife == false)
                {
                    ConsoleUtility.HighlightTxt($"{i + 1} ", ConsoleColor.DarkGray); monsters[i].MonsterStatuPrint();
                }
                else if(monsters[i].IsLife == true)
                {
                    Console.Write($"{i + 1} "); monsters[i].MonsterStatuPrint();
                }
            }
            Console.WriteLine("\n");

            Console.WriteLine("[내정보]");
            Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
            Console.Write($"  {player.Name} ");
            Console.WriteLine($" ({player.Job})");
            Console.Write("HP "); ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);

            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 취소");

            int KeyInput = MonsterAttackChoice(0, monsters.Count);

            switch (KeyInput)
            {
                case 0:
                    BattleMenu();
                    break;
                default:
                    PlayerAttack(KeyInput);
                    break;
            }
        }

        /// <summary>
        /// Player의 공격 
        /// 0 입력시 몬스터의 공격이 시작됩니다.
        /// </summary>
        private void PlayerAttack(int key)
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine("\n");

            Console.WriteLine($"{player.Name} 의 공격!");
            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[key - 1].Name} 을(를) 맞췄습니다. [데미지 : {player.Atk}]");
            Console.WriteLine("\n");

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[key - 1].Name}");
            Console.Write("HP "); ConsoleUtility.HighlightTxt(monsters[key - 1].Hp.ToString(), ConsoleColor.Green);
            Console.Write(" -> ");

            monsters[key - 1].TakeDamage(player.Atk);
            monsters[key - 1].HpPrint();

            Console.WriteLine("\n\n");

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            switch (PlayerAttackChoice(0, 0))
            {
                case 0:
                    MonsterAttack();
                    break;
            }
        }

        private void MonsterAttack()
        {
            for(int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsLife == true) MonsterAttack(i);
            }
            MonsterSelect();
        }

        /// <summary>
        /// 몬스터의 공격
        /// </summary>
        private void MonsterAttack(int idx)
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine("\n");

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[idx].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[idx].Name} 의 공격!");
            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {monsters[idx].Atk}]");
            Console.WriteLine("\n");

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {player.Name}");
            Console.Write("HP "); ConsoleUtility.HighlightTxt(player.Hp.ToString(), ConsoleColor.Green);
            Console.Write(" -> ");

            //플레이어의 HP감소
            player.Hp -= monsters[idx].Atk;
            ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            switch (PlayerAttackChoice(0, 0))
            {
                case 0:
                    break;
            }
        }

        /// <summary>
        /// 전투 결과
        /// </summary>
        private void BattleResult()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("Battle!! - Result");



        }

        private int MonsterAttackChoice(int min = 0, int max = 0)
        {
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    if (choice != 0 && monsters[choice - 1].IsLife == true) return choice;
                    else if (choice == 0) return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요");
                ConsoleUtility.HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }

        public int PlayerAttackChoice(int min = 0, int max = 0)
        {
            Console.WriteLine();
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요");
                ConsoleUtility.HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// 몬스터를 1~4마리 랜덤 생성합니다.
        /// </summary>
        public void RandomMonster()
        {
            monsters.Clear();
            Random rand = new Random();
            int monsterCount = rand.Next(1, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                switch (rand.Next(1, 4))
                {
                    case 1:
                        monsters.Add(new Monster(2, "미니언", 10, 15, true));
                        break;
                    case 2:
                        monsters.Add(new Monster(3, "대포미니언", 20, 10, true));
                        break;
                    case 3:
                        monsters.Add(new Monster(5, "공허충", 30, 25, true));
                        break;
                }
            }
        }
    }
}
