namespace _5by5_ChampionshipController.Entity
{
    internal class Team
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public DateOnly CreationDate { get; set; }

        public Team(string name, string nickname, DateOnly creationDate)
        {
            Name = name;
            Nickname = nickname;
            CreationDate = creationDate;
        }
    }
}