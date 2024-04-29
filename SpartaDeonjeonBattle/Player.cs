namespace SpartaDungeonBattle
{
    internal class Player
    {
        //Gold 제외 나머지는 변하지 않음
        //변하지 않는 기본 능력치와 아이템을 통한 추가 능력치로 구분
        //프로퍼티 사용: get은 참조, set은 변경

        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; set; }

        public Player(string name, string job, int level, int atk, int def, int hp, int gold)    //생성자
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
}