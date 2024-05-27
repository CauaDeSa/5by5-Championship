namespace _5by5_ChampionshipController.src.Entity
{
    public class Team
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public DateOnly CreationDate { get; set; }
        public bool isActive { get; set; }

        public Team(string name, string nickname, DateOnly creationDate)
        {
            Name = name;
            Nickname = nickname;
            CreationDate = creationDate;
            isActive = true;
        }
    }
}