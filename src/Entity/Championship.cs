namespace _5by5_ChampionshipController.src.Entity
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
            StartDate = startDate;
            EndDate = null;
        }

        public Championship(string name, string season, DateOnly startDate, DateOnly endDate)
        {
            Name = name;
            Season = season;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            return $@"
               Championship........... {Name}
               Season................. {Season}
               StartDate.............. {StartDate}
               EndDate................ {(EndDate == null ? "Not ended yet" : EndDate)}";
        }
    }
}