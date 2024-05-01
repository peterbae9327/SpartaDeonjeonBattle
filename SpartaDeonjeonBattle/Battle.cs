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
        List<Monster> monsterType = new List<Monster>
        {
            new Monster(2, "미니언", 10, 15, true),
            new Monster(3, "대포미니언", 20, 10, true),
            new Monster(5, "공허충", 30, 25, true)
        };

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

            int KeyInput = ConsoleUtility.ObjectChoice(0, monsters.Count);

            switch (KeyInput)
            {
                case 0:
                    BattleMenu();
                    break;
                default:
                    UserAttack(KeyInput);
                    break;
            }
        }

        /// <summary>
        /// 유저의 공격 
        /// 몬스터의 HP가 0보다 같거나 작으면 몬스터의 Life를 false 합니다.
        /// 0 입력시 몬스터의 공격이 시작됩니다.
        /// </summary>
        private void UserAttack(int key)
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine();

            Console.WriteLine($"{player.Name} 의 공격!");
            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[key - 1].Name} 을(를) 맞췄습니다. [데미지 : {player.Atk}]");
            Console.WriteLine();

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[key - 1].Name}");
            Console.Write("HP "); ConsoleUtility.HighlightTxt(monsters[key - 1].Hp.ToString(), ConsoleColor.Green);
            Console.Write(" -> ");
            
            int KeyInput = key;
            monsters[key - 1].Hp -= player.Atk;

            if (monsters[key - 1].Hp <= 0)
            {
                Console.WriteLine("Dead");
                monsters[key - 1].IsLife = false;
                KeyInput -= 1;
            }
            else if (monsters[key - 1].Hp > 0)
                ConsoleUtility.HighlightLine(monsters[key - 1].Hp.ToString(), ConsoleColor.Green);

            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            switch (MonsterAttackChoice())
            {
                case 0:
                    MonsterAttack(KeyInput);
                    break;
            }
        }

        /// <summary>
        /// 몬스터의 공격
        /// </summary>
        private void MonsterAttack(int key)
        {
            // 모든 몬스터가 공격했으면 -> 전투 결과
            if (key == 0)
                BattleResult();
            else if(key > 0)
            {
                Console.Clear();

                ConsoleUtility.ShowTitle(" Battle!! ");
                Console.WriteLine();

                // 현재 몬스터가 죽어있다면 -> 몬스터 공격X 
                // 현재 몬스터가 살아있다면 -> 몬스터 공격O
                if (monsters[key - 1].IsLife == false)
                    MonsterAttack(key - 1);
                else if (monsters[key - 1].IsLife == true)
                {
                    Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                    Console.WriteLine($"{monsters[key - 1].Name} 의 공격!");
                    Console.WriteLine($" {player.Name} 을(를) 맞췄습니다. [데미지 : {monsters[key - 1].Atk}]");
                    Console.WriteLine();

                    Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
                    Console.WriteLine($" {player.Name}");
                    Console.Write("HP "); ConsoleUtility.HighlightTxt(player.Hp.ToString(), ConsoleColor.Green);
                    Console.Write(" -> ");

                    //플레이어의 HP감소
                    player.Hp -= monsters[key - 1].Atk;
                    ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
                    Console.WriteLine();

                    ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
                    Console.WriteLine();

                    switch (MonsterAttackChoice())
                    {
                        case 0:
                            MonsterAttack(key - 1);
                            break;
                    }
                }
            }
        }

        private int MonsterAttackChoice(int min = 0, int max = 0)
        {
            Console.WriteLine();
            Console.WriteLine(">>");
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
        /// 전투 결과
        /// </summary>
        private void BattleResult()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("Battle!! - Result");



        }

        /// <summary>
        /// 몬스터를 1~4마리 랜덤 생성합니다.
        /// </summary>
        public void RandomMonster()
        {
            monsters.Clear();
            Random rand = new Random();

            for (int i = 0; i < rand.Next(1, 5); i++)
            {
                monsters.Add(monsterType[rand.Next(monsterType.Count)]);
            }
        }
    }
}
