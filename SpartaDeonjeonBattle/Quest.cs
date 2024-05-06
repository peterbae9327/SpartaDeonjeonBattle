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
        public void LoadQuestList(List<QuestDB> _quests)
        {
            for (int i = 0; i < _quests.Count; i++)
            {//번호 초기화
                _quests[i].AllocatedNumber = 0;
            }
            Console.Clear();
            ConsoleUtility.ShowTitle("Quest!!");
            Console.WriteLine();
            int j = 0;
            for (int i = 0; i < _quests.Count; i++)
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
                    else if (!_quests[i].ClearQuest && _quests[i].AcceptQuest)
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
        public void LoadQuest(List<QuestDB> _quests,int _choice)
        {
            int choice = 0;
            for(int i = 0; i < _quests.Count; i++)
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
                        Console.Write(" (");
                        ConsoleUtility.HighlightTxt(_quests[i].CurrentNumber.ToString(),ConsoleColor.Green);
                        ConsoleUtility.HighlightTxt("/",ConsoleColor.Yellow);
                        ConsoleUtility.HighlightTxt(_quests[i].TargetNumber.ToString(), ConsoleColor.Green);
                        Console.Write(")");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("- 보상 -");
                    for(int j = 0; j < _quests[i].Rewards.Count; j++)
                    {
                        if (_quests[i].Rewards[j].ItemName == "Gold")
                        {
                            Console.WriteLine(_quests[i].Rewards[j].ItemQuantity.ToString() + "G");
                        }
                        else
                        {
                            Console.WriteLine($"{_quests[i].Rewards[j].ItemName} X {_quests[i].Rewards[j].ItemQuantity}");
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
        public void UpdateQuest(List<QuestDB> _quests)
        {
            //정보 받아서 달성여부 처리
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
        
        
    }
    public class QuestDB
    {
        public string Title;
        public string ExplainText;
        public string GoalText;
        public string Target;
        public List<Reward> Rewards = new List<Reward>();
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

        public Reward(string itemName, int itemQuantity)
        {
            ItemName = itemName;
            ItemQuantity = itemQuantity; 
        }
        
    }
}
