namespace _5by5_ChampionshipController.Entity
{
    public class Championship
    {
        public string Name { get; set; }
        public string Season { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public Championship(string name, string season, DateOnly startDate)
        {
            Name = name;
            Season = season;
            this.StartDate = startDate;
            this.EndDate = null;
        }

        public Championship(string name, string season, DateOnly startDate, DateOnly endDate)
        {
            Name = name;
            Season = season;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}