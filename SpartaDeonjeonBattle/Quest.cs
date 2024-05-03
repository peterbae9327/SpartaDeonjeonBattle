using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpartaDeonjeonBattle
{
    internal class Quest
    {
        QuestDB quest1 = new QuestDB();
        QuestDB quest2 = new QuestDB();
        QuestDB quest3 = new QuestDB();

        public void LoadQuestList(QuestDB[] _quests)
        {
            UpdateQuest(_quests);
            Console.Clear();
            ConsoleUtility.ShowTitle("Quest!!");
            Console.WriteLine();
            int j = 0;
            for (int i = 0; i < _quests.Length; i++)
            {
                if (!_quests[i].CloseQuest)
                {
                    j++;
                    _quests[i].AllocatedNumber = j;
                    ConsoleUtility.HighlightTxt(j.ToString(), ConsoleColor.Green);
                    Console.Write(". " + _quests[i].Title);
                    if (_quests[i].AcceptQuest && _quests[i].ClearQuest)
                    {
                        ConsoleUtility.HighlightLine("[완료]", ConsoleColor.Yellow);
                    }
                    else if (!_quests[i].ClearQuest)
                    {
                        ConsoleUtility.HighlightLine("[진행중]", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            ConsoleUtility.Getout("나가기");
            int choice = ChooseQuest(0, j);
            switch (choice)
            {
                case 0:
                    break;
                default:
                    LoadQuest(_quests,choice);
                    break;
            }
            

        }
        public void LoadQuest(QuestDB[] _quests,int _choice)
        {
            int choice = 0;
            for(int i = 0; i < _quests.Length; i++)
            {
                if (_quests[i].AllocatedNumber == _choice)
                {
                    choice = i;
                    Console.Clear();
                    ConsoleUtility.ShowTitle("Quest!!");
                    Console.WriteLine();
                    Console.WriteLine(_quests[i].Title);
                    Console.WriteLine();
                    Console.WriteLine(_quests[i].ExplainText);
                    Console.WriteLine();
                    Console.Write("- " + _quests[i].GoalText);
                    if (_quests[i].TargetNumber != null)
                    {
                        ConsoleUtility.HighlightTxt(_quests[i].CurrentNumber.ToString(),ConsoleColor.Green);
                        ConsoleUtility.HighlightTxt("/",ConsoleColor.Yellow);
                        ConsoleUtility.HighlightTxt(_quests[i].TargetNumber.ToString(), ConsoleColor.Green);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("- 보상 -");
                    for(int j = 0; j < _quests[i].rewards.Length; j++)
                    {
                        if (_quests[i].rewards[j].ItemName == "Gold")
                        {
                            Console.WriteLine(_quests[i].rewards[j].ItemQuantity.ToString() + "G");
                        }
                        else
                        {
                            Console.WriteLine($"{_quests[i].rewards[j].ItemName} X {_quests[i].rewards[j].ItemQuantity}");
                        }
                    }
                }
            }
            Console.WriteLine();
            int max = 1;
            if (!_quests[choice].AcceptQuest)
            {
                ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
                Console.WriteLine(". 수락");
                ConsoleUtility.Getout("다음에");
            }
            else if (_quests[choice].ClearQuest)
            {
                ConsoleUtility.HighlightTxt("1", ConsoleColor.Green);
                Console.WriteLine(". 보상 받기");
                ConsoleUtility.Getout("돌아가기");
            }
            else
            {
                ConsoleUtility.Getout("돌아가기");
                max = 0;
            }
            _choice = ConsoleUtility.MenuChoice(0, max);
            switch (_choice)
            {
                case 1:
                    if (!_quests[choice].AcceptQuest)
                    {
                        _quests[choice].AcceptQuest = true;
                    }
                    else if (_quests[choice].ClearQuest)
                    {
                        _quests[choice].CloseQuest = true;
                    }
                    LoadQuestList(_quests);
                    break;
                case 0:
                    LoadQuestList(_quests);
                    break;
            }

        }
        public void UpdateQuest(QuestDB[] _quests)
        {
            for (int i = 0; i < _quests.Length; i++)
            {
                _quests[i].AllocatedNumber = 0;
            }
            //퀘스트 조건 달성시 ClearQuest = true
            //신규퀘스트 해금 조건 발생 시 CloseQuest = false
        }
        public int ChooseQuest(int min, int max)
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 퀘스트를 선택해주세요");
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
        //퀘스트 데이터 베이스 
        public QuestDB[] InitializeQuest()
        {
            quest1.Title = "마을을 위협하는 미니언 처치";
            quest1.ExplainText = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
                                 "마을 주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                                 "모험가인 자네가 좀 처치해주게!";
            quest1.GoalText = $"{quest1.Target} {quest1.TargetNumber}마리 처치";
            quest1.Target = "미니언";
            quest1.TargetNumber = 5;
            quest1.CurrentNumber = 0;
            quest1.rewards[0].ItemName = "쓸만한 방패";
            quest1.rewards[0].ItemQuantity = 1;
            quest1.rewards[1].ItemName = "Gold";
            quest1.rewards[1].ItemQuantity = 5;

            quest2.Title = "장비를 장착해보자";
            quest2.ExplainText = "장비야말로 모험가에게 필수적이지!\n" +
                                 "장비없는 스파르타전사는 어느동네 야만전사만도 못하다네.";
            quest2.GoalText = "아무 장비나 장착해보기";
            quest2.rewards[0].ItemName = "Gold";
            quest2.rewards[0].ItemQuantity = 10;

            quest3.Title = "더욱 더 강해지기!";
            quest3.ExplainText = "\'나는 자랑스런 필승의 스파르타군이다.\'\n" +
                                 "\'안되면 되게하라!\'";
            quest3.GoalText = "";//(레벨업 구현시) 레벨 5레벨 달성 or (아니면)공격력 20 달성
            quest3.rewards[0].ItemName = "Gold";
            quest3.rewards[0].ItemQuantity = 10;
            // 이후 퀘스트는 CloseQuest = true로 설정
            QuestDB[] quests = { quest1, quest2, quest3 };
            return quests;
        }
    }
    public class QuestDB
    {
        public string Title;
        public string ExplainText;
        public string GoalText;
        public string Target;
        public Reward[] rewards;
        public bool AcceptQuest = false;
        public bool ClearQuest = false;
        public bool CloseQuest = false;
        public int AllocatedNumber;
        public int? TargetNumber;
        public int? CurrentNumber;
    }
    public class Reward
    {
        public string ItemName;
        public int ItemQuantity;
        
    }
}
