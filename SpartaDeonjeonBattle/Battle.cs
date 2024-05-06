using SpartaDungeonBattle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpartaDeonjeonBattle
{
    internal class Battle
    {
        List<Monster> monsters = new List<Monster>();
        public List<Monster> monsterlog = new List<Monster>();
        private Player player;
        private GameManager manager;
        private Potion potion;
        private Quest quest;
        private List<Item> inventoryitemlist;
        public int dngeonStage = 1;  // 던전 레벨. 클리어 했을시 +1;
        private bool alphaStrike;
        private int plusExp;
        private int plusGold;
        private bool islevelUp;
        private int plusPotion;
        private List<Item> plusItem;

        /// <summary>
        /// Battle클래스에서 player의 속성값들을 수정하기 위함입니다.
        /// Battle클래스에서 GameManager의 MainMenu로 가기 위함입니다.
        /// </summary>
        public Battle(Player player, GameManager manager, Potion potion, Quest quest, List<Item> inventoryitemlist)
        {
            this.player = player;
            this.manager = manager;
            this.potion = potion;
            this.quest = quest;
            this.inventoryitemlist = inventoryitemlist;
            plusItem = new List<Item>();
            islevelUp = false;
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
            Console.Write("HP "); ConsoleUtility.HighlightTxt(player.MaxHp.ToString(), ConsoleColor.Green);Console.Write("/");
            ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
            Console.Write("MP "); ConsoleUtility.HighlightTxt(player.MaxMp.ToString(), ConsoleColor.Green); Console.Write("/");
            ConsoleUtility.HighlightLine(player.Mp.ToString(), ConsoleColor.Green);

            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("1", ConsoleColor.Green); Console.WriteLine(". 공격");
            ConsoleUtility.HighlightTxt("2", ConsoleColor.Green); Console.WriteLine(". 스킬");

            switch (ConsoleUtility.MenuChoice(1, 2))
            {
                case 1:
                    MonsterSelect();
                    break;
                case 2:
                    SelectSkill();
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
            Console.Write("HP "); ConsoleUtility.HighlightTxt(player.MaxHp.ToString(), ConsoleColor.Green); Console.Write("/");
            ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
            Console.Write("MP "); ConsoleUtility.HighlightTxt(player.MaxMp.ToString(), ConsoleColor.Green); Console.Write("/");
            ConsoleUtility.HighlightLine(player.Mp.ToString(), ConsoleColor.Green);

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

            bool IsCritical = false;
            int damage = 0;
            bool IsAttackMiss = false;
            attackMiss(ref IsAttackMiss);

            if (alphaStrike == true)
            {
                Console.WriteLine($"{player.Name} 의 알파 스트라이크");
                Console.Write("MP "); ConsoleUtility.HighlightTxt(player.Mp.ToString(), ConsoleColor.Green);
                player.Mp -= 10;
                Console.Write(" -> "); ConsoleUtility.HighlightTxt(player.Mp.ToString(), ConsoleColor.Green);
                Console.WriteLine("\n");

                damage = monsters[key - 1].TakeDamage((int)Math.Ceiling((player.Atk + player.BonusAtk) * 2.0));
                compensation(key);

                Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                Console.Write($" {monsters[key - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]"); cirticalPrint(IsCritical);
                Console.WriteLine("\n");

                Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                Console.WriteLine($" {monsters[key - 1].Name}");
                Console.Write("HP "); ConsoleUtility.HighlightTxt((monsters[key - 1].Hp + damage).ToString(), ConsoleColor.Green);
                Console.Write(" -> "); monsters[key - 1].HpPrint();

                alphaStrike = false;
            }
            else if (alphaStrike == false)
            {
                if (IsAttackMiss == true)
                {
                    Console.WriteLine($"{player.Name} 의 공격!");
                    Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                    Console.Write($" {monsters[key - 1].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                }
                else if (IsAttackMiss == false)
                {
                    damage = cirtical(monsters[key - 1].TakeDamage(player.Atk + player.BonusAtk), ref IsCritical);
                    compensation(key);

                    Console.WriteLine($"{player.Name} 의 공격!");
                    Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                    Console.Write($" {monsters[key - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]"); cirticalPrint(IsCritical);
                    Console.WriteLine("\n");

                    Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[key - 1].Level.ToString(), ConsoleColor.Green);
                    Console.WriteLine($" {monsters[key - 1].Name}");
                    Console.Write("HP "); ConsoleUtility.HighlightTxt((monsters[key - 1].Hp + damage).ToString(), ConsoleColor.Green);
                    Console.Write(" -> "); monsters[key - 1].HpPrint();
                }
            }
            Console.WriteLine("\n\n");

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            //만약 몬스터가 죽었다면 MonsterLog에 보내기
            if (!monsters[key - 1].IsLife)
            {
                AddMonsterLog(monsters[key - 1]);
            }
            switch (PlayerAttackChoice(0, 0))
            {
                case 0:
                    MonsterAttackStart();
                    break;
            }
        }

        /// <summary>
        /// 모든 몬스터들의 공격 시작.
        /// 살아있는 몬스터들만 플레이어를 공격합니다.
        /// 플레이어의 체력이 0이면 -> 전투 결과
        /// 모든 몬스터들이 Dead이면 -> 전투 결과
        /// </summary>
        private void MonsterAttackStart()
        {
            List<bool> monstersLife = new List<bool>();

            for(int i = 0; i <= monsters.Count; i++)
            {
                if(i == monsters.Count)
                {
                    if (player.Hp > 0)
                    {
                        BattleMenu();
                        break;
                    }
                    else if (player.Hp <= 0)
                    {
                        player.Hp = 0;
                        BattleResult("You Lose");
                        break;
                    }
                }
                if (monsters[i].IsLife == true && player.Hp > 0)
                {
                    monstersLife.Add(true);
                    MonsterAttack(i); // 해당 몬스터가 살아있고 플레이어의 체력이 0보다 크면 플레이어를 공격합니다.
                }
                else if (monsters[i].IsLife == true && player.Hp <= 0)
                {
                    BattleResult("You Lose"); //플레이어의 체력이 0이면 -> 전투 결과 You Lose
                    break;
                }
                else if (monsters[i].IsLife == false && player.Hp <= 0)
                {
                    BattleResult("You Lose"); //플레이어의 체력이 0이면 -> 전투 결과 You Lose
                    break;
                }
                else if (monsters[i].IsLife == false && player.Hp > 0) //플레이어의 체력이 0보다 크고 몬스터가 죽어있다면 공격x
                {
                    monstersLife.Add(false);
                    if (i == monsters.Count - 1)
                    {
                        if (monstersLife.All(x => !x))
                        {
                            dngeonStage += 1;        // 모든 몬스터를 잡으면 스테이지 +1
                            BattleResult("Victory"); // 모든 몬스터들이 Dead이면 -> 전투 결과 Victory
                            break;
                        }
                        else
                        {
                            BattleMenu(); // 살아있는 몬스터가 존재하면 -> 배틀 메인
                            break;
                        }
                    }
                } 
            }
            monstersLife.Clear();
            manager.MainMenu();  // 전투 결과 까지 끝나면 메인 메뉴로 이동합니다.
        }

        /// <summary>
        /// 몬스터의 공격
        /// </summary>
        private void MonsterAttack(int idx)
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine("\n");

            //플레이어의 HP감소. 방어력이 몬스터의 공격력과 같거나 높다면 -1
            int damage = 0;
            if (monsters[idx].Atk - (player.Def + player.BonusDef) > 0)
            {
                damage = monsters[idx].Atk - (player.Def + player.BonusDef);
                player.Hp -= damage;
            }
            else if (monsters[idx].Atk - (player.Def + player.BonusDef) <= 0)
            {
                damage = 1;
                player.Hp -= damage;
            }

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[idx].Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {monsters[idx].Name} 의 공격!");
            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine("\n");

            Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
            Console.WriteLine($" {player.Name}");
            Console.Write("HP "); ConsoleUtility.HighlightTxt((player.Hp + damage).ToString(), ConsoleColor.Green);
            Console.Write(" -> ");

            if (player.Hp <= 0) player.Hp = 0;
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
        private void BattleResult(string str)
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("Battle!! - Result");
            Console.WriteLine();

            ConsoleUtility.HighlightLine(str, ConsoleColor.Yellow);
            Console.WriteLine();

            if (str == "Victory")
            {
                player.Exp += plusExp;
                player.Gold += plusGold; 

                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine("[캐릭터 정보]");
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
                Console.Write($" {player.Name}");
                levelUp();
                levelUpPrint();

                Console.WriteLine();
                Console.Write("HP "); ConsoleUtility.HighlightTxt(player.MaxHp.ToString(), ConsoleColor.Green); Console.Write("/");
                ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);

                player.Mp += 10;
                Console.Write("MP "); ConsoleUtility.HighlightTxt(player.MaxMp.ToString(), ConsoleColor.Green); Console.Write("/");
                ConsoleUtility.HighlightLine(player.Mp.ToString(), ConsoleColor.Green);

                Console.Write("exp "); ConsoleUtility.HighlightTxt((player.Exp - plusExp).ToString(), ConsoleColor.Green);
                Console.Write(" -> ");
                ConsoleUtility.HighlightTxt(player.Exp.ToString(), ConsoleColor.Green);
                Console.Write(" ( exp +"); ConsoleUtility.HighlightTxt(plusExp.ToString(), ConsoleColor.Green); Console.Write(" )");
                Console.WriteLine("\n");

                Console.WriteLine("[획득 아이템]");
                ConsoleUtility.HighlightTxt(plusGold.ToString(), ConsoleColor.Green);Console.WriteLine(" Gold");
                ItemPrint();

                Console.WriteLine("\n");
                plusExp = 0; plusGold = 0;
                plusItem.Clear();
            }
            else if(str == "You Lose")
            {
                Console.WriteLine();
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
                Console.WriteLine($" {player.Name}");
                Console.Write("HP "); 
                ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
                Console.WriteLine("\n");
                plusExp = 0; plusGold = 0;
                player.Mp += 10;
            }

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            switch (PlayerAttackChoice(0, 0))
            {
                case 0:
                    break;
            }
        }


        /// <summary>
        /// 스킬 선택
        /// </summary>
        private void SelectSkill()
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
            Console.Write("HP "); ConsoleUtility.HighlightTxt(player.MaxHp.ToString(), ConsoleColor.Green); Console.Write("/");
            ConsoleUtility.HighlightLine(player.Hp.ToString(), ConsoleColor.Green);
            Console.Write("MP "); ConsoleUtility.HighlightTxt(player.MaxMp.ToString(), ConsoleColor.Green); Console.Write("/");
            ConsoleUtility.HighlightLine(player.Mp.ToString(), ConsoleColor.Green);
            Console.WriteLine("\n");

            ConsoleUtility.HighlightTxt("1.", ConsoleColor.Green); 
            Console.Write(" 알파 스트라이크 - MP "); ConsoleUtility.HighlightLine("10", ConsoleColor.Green);
            Console.Write(" 공격력 * "); ConsoleUtility.HighlightTxt("2", ConsoleColor.Green); Console.WriteLine(" 로 하나의 적을 공격합니다. ");
            ConsoleUtility.HighlightTxt("2.", ConsoleColor.Green); 
            Console.Write(" 더블 스트라이크 - MP "); ConsoleUtility.HighlightLine("15", ConsoleColor.Green);
            Console.Write(" 공격력 * "); ConsoleUtility.HighlightTxt("1.5", ConsoleColor.Green); Console.Write(" 로 ");
            ConsoleUtility.HighlightTxt("2", ConsoleColor.Green); Console.WriteLine("명의 적을 랜덤으로 공격합니다. \n");
            ConsoleUtility.HighlightLine("0. 취소", ConsoleColor.Red);

            switch (SkillChoice(0, 2))
            {
                case 0:
                    BattleMenu();
                    break;
                case 1:
                    AlphaStrike();
                    break;
                case 2:
                    DoubleAttack();
                    break;
            }
        }

        /// <summary>
        /// 알파 스트라이크
        /// </summary>
        private void AlphaStrike()
        {
            alphaStrike = true;
            MonsterSelect();
        }

        /// <summary>
        /// 더블 스트라이크
        /// </summary>
        private void DoubleAttack()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle(" Battle!! ");
            Console.WriteLine("\n");

            Console.WriteLine($"{player.Name} 의 더블 스트라이크");
            Console.Write("MP");
            Console.Write(" -> ");
            player.Mp -= 15;
            ConsoleUtility.HighlightTxt(player.Mp.ToString(), ConsoleColor.Green);
            Console.WriteLine("\n");

            HashSet<int> index = new HashSet<int>();
            Random rand = new Random();
            int monstersLifeCount = 0;
            
            for(int i = 0; i < monsters.Count; i++) if (monsters[i].IsLife == true) monstersLifeCount++;
            if (monstersLifeCount > 2) monstersLifeCount = 2; 
            while (index.Count < monstersLifeCount)
            {
                int Number = rand.Next(0, monsters.Count);
                if (monsters[Number].IsLife == true) index.Add(Number);
            }
            
            List<int> idx = index.ToList();  // HashSet을 List로 변환

            int damage = 0;

            for (int i = 0; i < idx.Count; i++)
            {
                damage = monsters[idx[i]].TakeDamage((int)Math.Ceiling((player.Atk + player.BonusAtk) * 1.5));
                compensation(i+1);

                Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[idx[0]].Level.ToString(), ConsoleColor.Green);
                Console.Write($" {monsters[idx[i]].Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                Console.WriteLine("\n");

                Console.Write("Lv."); ConsoleUtility.HighlightTxt(monsters[idx[i]].Level.ToString(), ConsoleColor.Green);
                Console.WriteLine($" {monsters[idx[i]].Name}");
                Console.Write("HP "); ConsoleUtility.HighlightTxt((monsters[idx[i]].Hp + damage).ToString(), ConsoleColor.Green);
                Console.Write(" -> "); monsters[idx[i]].HpPrint();
                Console.WriteLine("\n");
                //만약 몬스터가 죽었다면 MonsterLog에 보내기
                if (!monsters[idx[i]].IsLife)
                {
                    AddMonsterLog(monsters[idx[i]]);
                }
            }           

            ConsoleUtility.HighlightTxt("0", ConsoleColor.Green); Console.WriteLine(". 다음");
            Console.WriteLine();

            switch (PlayerAttackChoice(0, 0))
            {
                case 0:
                    MonsterAttackStart();
                    break;
            }
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

        private int PlayerAttackChoice(int min = 0, int max = 0)
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

        private int SkillChoice(int min = 0, int max = 0)
        {
            Console.WriteLine();
            Console.WriteLine("스킬을 선택해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    if (choice == 1)
                    {
                        if (player.Mp - 10 >= 0) return choice;
                    }
                    else if (choice == 2)
                    {
                        if (player.Mp - 15 >= 0) return choice;
                    }
                    else if (choice == 0) return choice;
                }
                Console.WriteLine("마나가 부족합니다");
                ConsoleUtility.HighlightTxt(">>", ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// 몬스터 랜덤 생성
        /// </summary>
        public void RandomMonster()
        {
            monsters.Clear(); 
            MONSTERTYPE MONSTERTYPE;
            Random rand = new Random(); 
            int monsterCount = rand.Next(dngeonStage, dngeonStage + 4);           // 몬스터 수(층 증가시 1마리씩 추가)
            int monsterType;         // 몬스터 타입 지정(층 증가시 강한 몬스터 추가)
            
            for (int i = 0; i < monsterCount; i++)
            {
                monsterType = rand.Next(1, 7);
                switch (monsterType)
                {
                    case 1:
                        monsterReset(dngeonStage, MONSTERTYPE.MINION);
                        break;
                    case 2:
                        monsterReset(dngeonStage, MONSTERTYPE.CANNONMINION);
                        break;
                    case 3:
                        monsterReset(dngeonStage, MONSTERTYPE.VOIDLING);
                        break;
                    case 4:
                        monsterReset(dngeonStage, MONSTERTYPE.ALIEN);
                        break;
                    case 5:
                        monsterReset(dngeonStage, MONSTERTYPE.SCP096);
                        break;
                    case 6:
                        monsterReset(dngeonStage, MONSTERTYPE.ZOMBIE);
                        break;
                }
            }
        }

        /// <summary>
        /// 층에 맞게 몬스터 초기화.
        /// </summary>
        /// <returns></returns>
        private void monsterReset(int stage, MONSTERTYPE MONSTERTYPE)
        {
            Random rand = new Random();
            int Level = rand.Next(stage, stage + 2); // stage 1 : Lv.1 ~ 2, stage 2 : Lv.2 ~ 3, stage 3 : Lv.3 ~ 4 

            switch (MONSTERTYPE)
            {
                case MONSTERTYPE.MINION:
                    monsters.Add(new Monster(Level, "미니언", 10 + ((Level - 1) * 5), 15 + ((Level - 1) * 5), true, 100 + ((Level - 1) * 20)));
                    break;
                case MONSTERTYPE.CANNONMINION:
                    monsters.Add(new Monster(Level, "대포미니언", 20 + ((Level - 1) * 5), 10 + ((Level - 1) * 5), true, 100 + ((Level - 1) * 20)));
                    break;
                case MONSTERTYPE.VOIDLING:
                    monsters.Add(new Monster(Level, "공허충", 30 + ((Level - 1) * 5), 25 + ((Level - 1) * 5), true, 100 + ((Level - 1) * 20)));
                    break;
                case MONSTERTYPE.ALIEN:
                    monsters.Add(new Monster(Level, "에일리언", 20 + ((Level - 1) * 5), 30 + ((Level - 1) * 5), true, 150 + ((Level - 1) * 30)));
                    break;
                case MONSTERTYPE.SCP096:
                    monsters.Add(new Monster(Level, "SCP-096", 30 + ((Level - 1) * 5), 30 + ((Level - 1) * 5), true, 150 + ((Level - 1) * 30)));
                    break;
                case MONSTERTYPE.ZOMBIE:
                    monsters.Add(new Monster(Level, "좀비", 40 + ((Level - 1) * 10), 20 + ((Level - 1) * 10), true, 150 + ((Level - 1) * 50)));
                    break;
            }

        }

        /// <summary>
        /// 15% 확률 160% 치명타
        /// </summary>
        private int cirtical(int damage, ref bool iscritical)
        {
            double Damage = damage;
            int rand = new Random().Next(0, 100);
            if (rand < 15)
            {
                Damage *= 1.6;
                iscritical = !iscritical;
            }

            return (int)Math.Ceiling(Damage);
        }

        private void cirticalPrint(bool iscritical)
        {
            if (iscritical == true) Console.WriteLine(" - 치명타 공격!!");
            else if(iscritical == false) Console.WriteLine();
        }

        /// <summary>
        /// 10% 확률 공격 무효
        /// </summary>
        private void attackMiss(ref bool isattackmiss)
        {
            int rand = new Random().Next(0, 100);
            if (rand < 10) isattackmiss = true;
        }
        // monsterLog
        // 몬스터 잡은 내역을 수집합니다.
        public void AddMonsterLog(Monster monster)
        {
            monsterlog.Add(monster);
        }
        //잡은 내역을 전송합니다.
        public List<Monster> ReportMonsterLog()
        {
            return monsterlog;
        }

        /// <summary>
        /// 보상
        /// 33% = 포션
        /// 33% = 낡은검
        /// </summary>
        private void compensation(int key)
        {
            Random rand = new Random();
            int count = rand.Next(0, 3);

            if (monsters[key - 1].IsLife == false)
            {
                plusGold += monsters[key - 1].Gold;
                plusExp += monsters[key - 1].Level;
                if (count == 1)
                {
                    potion.Quantity++;
                    plusPotion++;
                }
                if (count == 2) 
                { 
                    inventoryitemlist.Add(new Item("낡은검", "근접 무기", ItemType.WEAPON, 10, 0, 0, 50)); 
                    plusItem.Add(new Item("낡은검", "근접 무기", ItemType.WEAPON, 10, 0, 0, 50));
                }
            }
        }

        /// <summary>
        /// 획득 아이템 출력
        /// </summary>
        private void ItemPrint()
        {
            if (plusPotion != 0)
            {
                Console.Write("포션 - ");ConsoleUtility.HighlightLine(plusPotion.ToString(), ConsoleColor.Green);
            }
            if (plusItem.Count != 0)
            {   

                if(plusItem.Exists(p => p.Name == "낡은검"))
                {
                    int count = plusItem.Count(p => p.Name == "낡은검");
                    Console.Write("낡은검 - "); ConsoleUtility.HighlightLine(count.ToString(), ConsoleColor.Green);
                }
                
            }
        }

        /// <summary>
        /// 레벨 업 검사
        /// </summary>
        private void levelUp()
        {
            if (player.Exp >= 10 && player.Level == 1)
            {
                player.Level++;
                player.Atk += 1;
                player.Def += 1;
                player.MaxHp += 10;
                player.MaxMp += 10;
                islevelUp = true;
            }
            else if (player.Exp >= 35 && player.Level == 2)
            {
                player.Level++;
                player.Atk += 1;
                player.Def += 1;
                player.MaxHp += 10;
                player.MaxMp += 10;
                islevelUp = true;
            }
            else if (player.Exp >= 65 && player.Level == 3)
            {
                player.Level++;
                player.Atk += 1;
                player.Def += 1;
                player.MaxHp += 10;
                player.MaxMp += 10;
                islevelUp = true;
            }
            else if (player.Exp >= 100 && player.Level == 4)
            {
                player.Level++;
                player.Atk += 1;
                player.Def += 1;
                player.MaxHp += 10;
                player.MaxMp += 10;
                islevelUp = true;
            }
        }

        private void levelUpPrint()
        {
            if(islevelUp == true)
            {
                Console.Write(" -> ");
                Console.Write("Lv."); ConsoleUtility.HighlightTxt(player.Level.ToString(), ConsoleColor.Green);
                Console.Write($" {player.Name}");
                islevelUp = false;
            }
        }

        public enum MONSTERTYPE
        {
            MINION,         // 미니언
            CANNONMINION,   // 대포 미니언
            VOIDLING,       // 공허충
            ALIEN,          // 에일리언
            SCP096,         // SCP-096
            ZOMBIE          // 좀비
        }
    }
}


