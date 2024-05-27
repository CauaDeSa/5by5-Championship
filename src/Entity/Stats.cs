namespace _5by5_ChampionshipController.src.Entity
{
    public class Stats
    {
        public string Tname { get; set; }
        public string Cname { get; set; }
        public string Season { get; set; }
        public int? Pontuation { get; set; }
        public int? ScoredGoals { get; set; }
        public int? SufferedGoals { get; set; }

        public Stats(string tname, string cname, string season)
        {
            Tname = tname;
            Cname = cname;
            Season = season;
            Pontuation = null;
            ScoredGoals = null;
            SufferedGoals = null;
        }

        public Stats(string tname, string cname, string season, int pontuation, int scoredGoals, int sufferedGoals)
        {
            Tname = tname;
            Cname = cname;
            Season = season;
            Pontuation = pontuation;
            ScoredGoals = scoredGoals;
            SufferedGoals = sufferedGoals;
        }
    }
}